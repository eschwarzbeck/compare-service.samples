<%@ Page Language="C#" Inherits="Workshare.Samples.AdvWebSample.Upload" CodeBehind="Upload.aspx.cs"
    EnableSessionState="ReadOnly" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Page</title>
    <style type="text/css">
        BODY
        {
            margin: 15px;
            padding: 0;
            background-color: #FFFFFF;
            font-family: Arial,sans-serif;
            font-size: 12px;
            color: #333333;
            line-height: 1.166;
        }
        .loginMainHeading
        {
            font-family: Arial;
            font-size: 16px;
            color: #800000;
        }
        .loginHeading2
        {
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: #000000;
        }
        .loginNormal
        {
            font-family: Arial;
            font-size: 12px;
            color: #000000;
        }
    </style>
</head>
<body link="#003366">
    <form id="form" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="scriptManager" runat="server" />

    <script type="text/javascript">
        function pageLoad() {
            //  register the form and upload elements
            window.parent.register(
                document.getElementById('<%= this.form.ClientID %>'),
                document.getElementById('<%= this.OriginalFile.ClientID %>'),
                document.getElementById('<%= this.ModifiedFile.ClientID %>'),
                document.getElementById('<%= this.UploadFilesDiv.ClientID %>')
            );
        }


        //Dynamically add FileUpload control on click of Add File button       
        function addFileUploadBox() {
            if (!document.getElementById || !document.createElement) {
                return false;
            }

            var uploadArea = document.getElementById("ModifiedFileDiv");

            if (!uploadArea) {
                return;
            }

            var newLine = document.createElement("br");
            uploadArea.appendChild(newLine);

            var newUploadBox = document.createElement("input");

            // Set up the new input for file uploads
            newUploadBox.type = "file";
            newUploadBox.size = "40";
            newUploadBox.setAttribute("height", "22px");

            // The new box needs a name and an ID
            if (!addFileUploadBox.lastAssignedId) {
                addFileUploadBox.lastAssignedId = 100;
            }

            newUploadBox.setAttribute("id", "ModifiedFile" + addFileUploadBox.lastAssignedId);
            newUploadBox.setAttribute("name", "ModifiedFile" + addFileUploadBox.lastAssignedId);
            uploadArea.appendChild(newUploadBox);
            addFileUploadBox.lastAssignedId++;

            window.parent.registerUploadControl(newUploadBox);
            window.parent.resizeFrame();
        }

        // Reset page UI to Compare again.
        function onStartOver() {
            var fileUploadDiv = document.getElementById('UploadFilesDiv');
            fileUploadDiv.style.display = 'block';

            var resultsDiv = document.getElementById('ResultsDiv');
            resultsDiv.style.display = 'none';

            window.parent.toggleUploadButtonAndStatusPane(true);
            window.parent.resizeFrame();
        }       

        
    </script>

    <!-- Login Section-->
    <div id="LoginDiv" runat="server">
        <table border="0" cellpadding="4" cellspacing="0">
            <tr>
                <td colspan="2">
                    <div class="loginMainHeading">
                        Welcome to Workshare Compare Server
                    </div>
                    <div class="loginNormal">
                        <asp:Label ID="VersionLabel" runat="server" /></div>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="loginHeading2">
                        Please enter your login details below.</div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Username:
                </td>
                <td align="left">
                    <asp:TextBox ID="UsernameTextBox" runat="server" Width="250px" Height="17px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Password:
                </td>
                <td align="left">
                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" Width="250px"
                        Height="17px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Domain:
                </td>
                <td align="left">
                    <asp:TextBox ID="DomainTextBox" runat="server" Width="250px" Height="17px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="LoginButton" runat="server" Text="Log in" OnClick="LoginButton_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <!--Upload Files Section-->
    <div id="UploadFilesDiv" runat="server">
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
            <tr>
                <td colspan="2" align="right">
                    <div class="loginNormal">
                        <asp:Label ID="CompositorVersionLabel" runat="server" /></div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="loginHeading2">
                        Select the documents to compare:</div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="loginNormal">
                        <asp:Label ID="FileSizeWarningLabel" runat="server" /></div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Original Document:
                </td>
                <td align="left">
                    <asp:FileUpload ID="OriginalFile" runat="server" size="40" Height="22px" />
                </td>
            </tr>
            <tr>
                <td>
                    Modified Document:
                </td>
                <td align="left">
                    <div id="ModifiedFileDiv">
                        <asp:FileUpload ID="ModifiedFile" runat="server" size="40" Height="22px" />
                        <input type="button" value="Add File" onclick="addFileUploadBox()" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Select Rendering Set:
                </td>
                <td align="left">
                    <asp:DropDownList ID="RenderingSetDropDownList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Select Output Format:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ResponseOptionsDropDownList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <!-- Results Section-->
    <div id="ResultsDiv" runat="server">
        <table width="100%" cellpadding="4" border="0">
            <tr>
                <td valign="top">
                    <div class="loginHeading2">
                        Comparison Results</div>
                </td>
                <td align="right" valign="bottom">
                    <input type="button" value="Start Again" onclick="onStartOver()" />
                </td>
            </tr>
            <tr>
                <td style="width: 23%;">
                    Original Document:
                </td>
                <td align="left">
                    <asp:Label ID="OriginalFilePathLabel" runat="server" Text="Original File"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Rendering Set used:
                </td>
                <td align="left">
                    <asp:Label ID="RenderingSetLabel" runat="server" Text="Rendering Set"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Table runat="server" ID="ResultFiles" border="1" Width="100%" CellPadding="4"
                        CellSpacing="0" BorderColor="#c2cad1">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
