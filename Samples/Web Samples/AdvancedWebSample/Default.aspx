<%@ Page Language="C#" Inherits="Workshare.Samples.AdvWebSample._Default" CodeBehind="Default.aspx.cs" %>

<%@ Register Assembly="MattBerseth.WebControls.AJAX" Namespace="MattBerseth.WebControls.AJAX.Progress"
    TagPrefix="mb" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head runat="server">
    <title>Workshare Compare Service Client</title>
    <link rel="Stylesheet" href="_assets/css/default.css" />
    <link rel="Stylesheet" href="_assets/css/progress.css" />
    <link rel="Stylesheet" href="_assets/css/upload.css" />
    <style type="text/css">
        BODY
        {
            font-family: Arial, Sans-Serif;
            font-size: 12px;
        }
    </style>
</head>
<body style="height: 100%;">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 100%">
        <tr valign="top" style="height: 1%;">
            <td background="_assets/img/grey-banner.png" style="width: auto;">
                &nbsp;
            </td>
            <td background="_assets/img/grey-banner.png" align="center" style="width: 707px;"><img alt="Workshare" src="_assets/img/main-banner.png" /></td>
            <td background="_assets/img/grey-banner.png" style="width: auto;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: auto">
            </td>
            <td background="_assets/img/middle-gradient.png" valign="top" style="width: 707px;background-repeat: repeat-x;">
                <form id="form1" runat="server" enctype="multipart/form-data">
                <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />

                <script type="text/javascript">
                    var intervalID = 0;
                    var progressBar;
                    var originalFile;
                    var uploadFilesDiv;
                    var uploadControls = [];
                    var uploadControlsCount = 0;
                    var form;
                    var regexElement;
                    var existentControlIndex = -1;

                    function pageLoad() {
                        // Attach to the upload click event and grab a reference to the progress bar
                        $addHandler(document.getElementById('upload'), 'click', onUploadClick);
                        progressBar = $find('progress');
                    }

                    // Called from Upload.aspx to register different parts of the UI
                    // These are dynamically submitted/updated depending upon different events/conditions.
                    function register(form, originalFile, modifiedFile, uploadFilesDiv) {
                        // Register the form
                        this.form = form;
                        this.uploadFilesDiv = uploadFilesDiv;

                        removeAll();    // from upload controls' array
                        registerUploadControl(originalFile);
                        registerUploadControl(modifiedFile);

                        resizeFrame();  // just a convenient place to call this func, has nothing to do with 'register' otherwise.
                    }

                    // This function is called from Upload.aspx to register the IDs of all upload controls.
                    // These IDs are used to validate the filepaths for each of them, before submitting.
                    function registerUploadControl(control) {
                        var controlID = control.getAttribute("id");
                        if (contains(controlID)) {
                            uploadControls[existentControlIndex] = control;
                        }
                        else {
                            uploadControls[uploadControlsCount] = control;
                            uploadControlsCount++;
                        }
                    }
                    
                    // A utility function to remove all Control IDs from the collection of fileUpload controls
                    function removeAll() {
                        uploadControls.splice(0, uploadControlsCount + 1);
                        uploadControlsCount = 0;
                    }

                    // A utility function to check if an upload Control's ID already exists in collection
                    function contains(controlID) {
                        for (index in uploadControls) {
                            var controlIDInternal = uploadControls[index].getAttribute("id");
                            if (controlID == controlIDInternal) {
                                existentControlIndex = index;
                                return true;
                            }
                        }

                        return false;
                    }


                    // FileUpload control in IE expects a fully qualified physical path, and throws an
                    // uncatchable exception othewise, so we need to validate it befor submitting.        
                    function validateFilePaths() {
                        regexElement = document.getElementById('regex');

                        for (index in uploadControls) {
                            if (!isFilePathValid(uploadControls[index].value)) {
                                return false;
                            }

                            if (!isFileExtensionValid(uploadControls[index].value)) {
                                return false;
                            }
                        }

                        return true;
                    }

                    function isFilePathValid(path) {
                        if (path.length == 0) {
                            return true;
                        }
                        
                        var reg = new RegExp(regexElement.value);

                        if (path.match(reg)) {
                            return true;
                        }

                        var msg = '"' + path + '" is not a valid file path.'
                        alert(msg);
                        return false;
                    }

                    // Unfortunately 'accept' attribute of FileUpload control is unpredictable in behavior, so
                    // we need to check the extensions manually.

                    function isFileExtensionValid(filePath) {

                        if (filePath.length == 0) {
                            return true;
                        }
                        
                        if (filePath.indexOf('.') == -1) {
                            return false;
                        }

                        var validExtensions = new Array();                        

                        validExtensions[0] = 'doc';
                        validExtensions[1] = 'docx';
                        validExtensions[2] = 'rtf';
                        validExtensions[3] = 'pdf';
                        validExtensions[4] = 'html';

                        var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
                        
                        for (i in validExtensions) {
                            if (ext == validExtensions[i]) {
                                return true;
                            }
                        }

                        alert('The file extension ' + ext.toUpperCase() + ' is not allowed.');
                        return false;
                    }

                    // Called on 'Compare Now' button's click. Submits the form which is present in Upload.aspx, and
                    // was registered with this page on startup. Update the page UI to hide upload controls, and show
                    // progress controls.
                    function onUploadClick() {
                        var valid = validateFilePaths();

                        if (!valid) {
                            //  error message for the validation that was done.
                            onComplete('error', 'You need to provide a valid file type, with a correct path.');
                            return;
                        }                       

                        document.getElementById('upload').style.display = 'none';

                        //  update the message
                        updateMessage('info', 'Please wait while your documents are uploaded to the server ...');

                        // Update UI on Upload.aspx
                        uploadFilesDiv.style.display = 'none';

                        toggleLoader(true);

                        //  submit the form containing the fileupload
                        form.submit();

                        //  start polling to check on the progress ...
                        intervalID = window.setInterval(function() {
                            PageMethods.GetUploadStatus(function(result) {
                                if (result) {
                                    //  update the progressbar to the new value
                                    progressBar.set_percentage(result.percentComplete);
                                    //  update the message
                                    updateMessage('info', result.message);
                                    updatePairInfo(result.pairInfo);

                                    if (result.percentComplete >= 100) {
                                        //  clear the interval
                                        window.clearInterval(intervalID);
                                    }
                                }
                            });
                        }, 800);
                    }

                    // Called from Upload.aspx.cs after the Upload/Comparison of files is complete. Resets Page UI
                    // and stops polling for progress.
                    function onComplete(type, msg) {

                        try {
                            //  clear the interval
                            window.clearInterval(intervalID);
                            //  display the message
                            updateMessage(type, msg);
                            updatePairInfo('');
                            //  hide the progress bar
                            progressBar.hide();
                            progressBar.set_percentage(0);
                            //  re-enable the button
                            document.getElementById('upload').disabled = '';
                        }
                        catch (err) {
                            alert(err);
                        }
                    }

                    function clearMessage() {

                        var status = document.getElementById('status');
                        status.innerHTML = '';
                    }

                    // Update the message pane with any update from Upload.aspx.cs
                    function updateMessage(type, value) {

                        resetProgressUI();
                        var status = document.getElementById('status');

                        var msg;
                        if (type == 'error') {
                            msg = '<b>Failure:</b>' + value;
                        }
                        else if (type == 'success') {
                            msg = '<b>Success:</b>' + value;
                        }
                        else {
                            msg = value;
                        }

                        status.innerHTML = msg;
                    }

                    function resetProgressUI() {

                        var loader = document.getElementById('loader');

                        if (loader.style.display == 'block') {
                            loader.style.display = 'none';
                            progressBar.set_percentage(0);
                            progressBar.show();
                        }
                    }
                    
                    // Updates the pane to display more than one pair of files in case there are more than one modified
                    // file.
                    function updatePairInfo(pairInfo) {
                        var filespair = document.getElementById('filepair');
                        filespair.innerHTML = pairInfo;
                    }

                    // Resizes the upload iframe, containing Upload.aspx.cs, according to its contents
                    function resizeFrame() {
                        var uploadFrame = document.getElementById('uploadFrame');
                        try {
                            uploadFrame.style.height = uploadFrame.contentWindow.document.body.scrollHeight + "px";
                        }
                        catch (err) {
                        }
                    }

                    // Show/hide the progess-reporting and Compare button UI
                    function toggleUploadButtonAndStatusPane(show) {
                        var statusPane = document.getElementById('status');
                        var compareButton = document.getElementById('compareButtonDiv');

                        if (show == true) {
                            compareButton.style.display = 'block';
                            statusPane.style.display = 'block';
                            document.getElementById('upload').style.display = 'block';
                        }
                        else {
                            compareButton.style.display = 'none';
                            statusPane.style.display = 'none';
                            document.getElementById('upload').style.display = 'none';
                        }
                    }

                    // Called from SizeErrorHandlerPage.aspx/ErrorHandlerPage.aspx
                    function resetUIForErrorPage() {

                        progressBar.set_percentage(0);
                        progressBar.hide();
                        // reset the message
                        clearMessage();
                        toggleLoader(false);
                    }

                    function toggleStatusPane(show) {
                        try {
                            var statusPane = document.getElementById('status');
                            if (show) {
                                statusPane.style.display = 'block';
                            }
                            else {
                                statusPane.style.display = 'none';
                            }
                        }
                        catch (err) {
                            alert(err);
                        }
                    }

                    // Show/hide the small animated gif that is shown when there is no other progress available,
                    // e.g. immediately after user clicks 'Compare Now'
                    function toggleLoader(show) {
                        var loader = document.getElementById('loader');
                        if (show) {
                            loader.style.display = 'block';
                        }
                        else {
                            loader.style.display = 'none';
                        }
                    }
    
                </script>

                <center>
                    <input id="regex" name="regex" type="hidden" value='^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>"|]*))+)$' />
                    <div>
                        <!-- This page is a frame holder for the Upload.aspx page. This pages hosts Upload.aspx in an IFRAME
                        Upload.aspx handles all the UI and logic for upload/comparison  of files, while this page handles
                        showing of progress etc. -->
                        <br />
                        <br />
                        <img src="_assets/img/white-top.png" /><br />
                        <div class="upload">
                            <iframe id="uploadFrame" name="uploadFrame" frameborder="0" scrolling="no" src="Upload.aspx"
                                width="632"></iframe>
                            <div>
                                <div id="loader" class="loader" style="display: none">
                                    <img src="_assets/img/loader.gif" />
                                </div>
                                <mb:ProgressControl ID="progress" runat="server" CssClass="vista" Style="display: none"
                                    Value="0" Mode="Manual" Speed=".4" Width="95%" />
                                <div id="filepair" class="pairinfo" align="center">
                                    &nbsp;</div>
                                <div id="compareButtonDiv" class="commands" align="left">
                                    <input id="upload" type="button" value="Compare Now" />
                                </div>
                                <br />
                                <div id="status" class="infopane" align="left">
                                    Please select files to compare</div>

                                <br />
                            </div>
                        </div>
                        <img src="_assets/img/white-bottom.png" />
                    </div>
                </center>
                </form>
            </td>
            <td style="width: auto">
                &nbsp;
            </td>
        </tr>
    </table>
</body>
</html>
