

/***************************************************
                Comparer Object
****************************************************/

var comparer = comparer || {};

comparer.compare = function () {
    /* Gethering data */

    $('form').submit();

    //    var _data = $('form').serializeArray();
    //  
    //    $.ajax({
    //        type: "POST",
    //        url: "Comparer/Results",
    //        data: _data,
    //        success: comparer.processResult,
    //        error: comparer.error
    //    });
}

comparer.processResult = function (result) {
    $("#result-progressbar").hide();
    $step = $("#result-progressbar").parent();
    $step.children("h3").text("Results");
    var results = $.parseJSON(result.results);
    var errors = results.Errors;
    if (errors.length > 0) {
        var div = document.getElementById('result-errors');
        var ul = document.createElement('ul');
        for (var key in errors) {
            var li = document.createElement('li');
            $(li).text(errors[key]);
            ul.appendChild(li);
        }
        div.appendChild(ul);
    }
    var comparisons = results.Comparisons;
    if (comparisons.length > 0) {
        var div = document.getElementById('result-files');
        var ul = document.createElement('ul');
        for (var key in comparisons) {
            var comparison = comparisons[key];
            var li = document.createElement('li');
            var table = document.createElement('table');

            for (var resultName in comparison) {
                var row = document.createElement('tr');
                var thead = document.createElement('th');
                var tdata = document.createElement('td');

                if (resultName == "Error") {
                    if (comparison.Error == "") {
                        continue;
                    } else {
                        $(row).attr({ class: 'error-row' });
                        $(tdata).text(comparison.Error);
                    }
                } else if (resultName == "File") {
                    $(tdata).text(comparison.File);
                } else {
                    var result = comparison[resultName];
                    if (result.ServerName == null && result.ClientName == null) {
                        continue;
                    } else {
                        var anchor = document.createElement('a');
                        $(anchor).attr({ href: '/Comparer/Result/' + result.ServerName + '/' + result.ClientName,
                                         target: '_blank' });
                        $(anchor).text(result.ClientName);
                        $(tdata).append(anchor);
                    }
                }
                $(thead).text(resultName);

                $(row).append(thead, tdata);
                $(table).append(row);
            }

            $(li).append(table);
            $(ul).append(li);
        }
        div.appendChild(ul);
    }
}

comparer.error = function (req, status, error) {
    $("#result-progressbar").hide();
    var div = document.getElementById('result-errors');
    var ul = document.createElement('ul');
    var li = document.createElement('li');
    $(li).text(error);
    ul.appendChild(li);
    div.appendChild(ul);
}


comparer.tryCompare = function () {
    /* validate */
    var validator = $("form").validate();
    var anyError = false;

    if (!validator.element(uploader1._input.fileElement)) {
        anyError = true;
    }
    if (!validator.element(uploader2._inputs[0].fileElement)) {
        anyError = true;
    }

    /* Compare */
    if (!anyError) {
        comparer.compare();
    }
}

/*****************************************************
            Uploader Progress Bar
        (available only in firefox and Chrome)
******************************************************/

qq.UploaderProgress = function (element) {
    this._element = element;
    this._xhrSupported = qq.UploadHandlerXhr.isSupported();
};

qq.UploaderProgress.prototype = {
    create: function () {
        if (this._xhrSupported) {
            $(this._element).progressbar({ value: 0 });
        }
    },
    destroy: function () {
        if (this._xhrSupported) {
            $(this._element).progressbar("destroy");
        }
    },
    onProgress: function (loaded, total) {
        if (this._xhrSupported) {
            var percentLoaded = (loaded / total) * 100;
            $(this._element).progressbar({ value: percentLoaded });
        }
    },
    _getElement: function (){
        return this._element;
    }
}

/****************************************************
          -- Some changes to Button --              */

qq.UploadButton.prototype._show = function () {
    this._element.style.visibility = 'visible';
}

qq.UploadButton.prototype._hide = function () {
    this._element.style.visibility = 'hidden';
}



/*****************************************************
            -- Some Common Functions --             */

qq.setStatus = function (element, status) {
    switch (status) {
        case 'none':
            element.setAttribute('class', 'qq-upload-status');            
            break;
        case 'ready':
            element.setAttribute('class', 'qq-upload-ready');
            break;
        case 'loading':
            element.setAttribute('class', 'qq-upload-spinner');
            break;
        default:
            element.setAttribute('class', 'qq-upload-status');  
    }
}

qq.setInput = function (input, value) {
    input.value = value;
    $(input).trigger('blur');
}

qq.showBlock = function (element) {
    element.style.display = 'inline-block';
    element.style.visibility = 'visible';
}

qq.hideBlock = function (element) {
    element.style.display = 'none';
    element.style.visibility = 'hidden';
}

qq.showInput = function (input) {
    var li = input.docElement.parentNode;
    if (li.tagName == "LI") {
        li.style.display = 'block';
    }
}

qq.hideInput = function (input) {
    var li = input.docElement.parentNode;
    if (li.tagName == "LI") {
        li.style.display = 'none';
    }
}

qq.freezButton = function (button) {
    /* TODO: freez button */
    button._hide();
}

qq.unfreezButton = function (button) {
    /* TODO */
    button._show();
}

qq.buttonChangeName = function (button) {
    var container = button._element;
    var span = $(container).children("span:first");
    var text = $(span).html('Change File');
}


/*****************************************************
            Custom File Uploader Base Class
*****************************************************/
qq.WSHFileUploader = function (o) {
    // call parent constructor
    qq.FileUploaderBasic.apply(this, arguments);

    // additional options    
    qq.extend(this._options, {
        /* 
        Add custom options here 
        */
        dummy: ''

    });
    // overwrite options with user supplied
    qq.extend(this._options, o);
    /* 
    Add custom properties here
    */
    this._button  = this._options.button;
    this._element = this._options.element;
    
    /* Create uploadeable button at the bottom */
    if (this._options.button){ 
        this._button = this._createUploadButton(this._options.button);
    }    
}

qq.extend(qq.WSHFileUploader.prototype, qq.FileUploaderBasic.prototype);

qq.extend(qq.WSHFileUploader.prototype, {
    /* 
    Add custom methods here 
    */
    _onSubmit: function (id, fileName) {
        qq.FileUploaderBasic.prototype._onSubmit.apply(this, arguments);
    },
    _onProgress: function (id, fileName, loaded, total) {
        qq.FileUploaderBasic.prototype._onProgress.apply(this, arguments);
    },
    _onComplete: function (id, fileName, result) {
        qq.FileUploaderBasic.prototype._onComplete.apply(this, arguments);
    },
    _onCancel: function (id, fileName) {
        qq.FileUploaderBasic.prototype._onCancel.apply(this, arguments);
    },
    _createProgressBar: function () {
        var progress = document.createElement('div');
        progress.setAttribute('class', 'qq-upload-progressbar');
        return new qq.UploaderProgress(progress);
    },
    _createCancelButton: function () {
        var button = document.createElement('div');
        button.setAttribute('class', 'qq-upload-cancel');
        button.setAttribute('title', 'Cancel or Delete');
        qq.hideBlock(button);
        return button;
    },
    _createStatusBox: function () {
        var box = document.createElement('div');
        qq.setStatus(box, 'none');
        box.setAttribute('title', 'Status');
        return box;
    },
    _deleteFile: function (file) {
        var _data = { fileName: file };
        $.ajax({
            type: "POST",
            url: "Upload/Delete",
            data: _data,
            success: function (result) {
                //  alert(result.message); 
            },
            error: function () { }
        });
    }
});



/***********************************************
            Multi file uploader 
************************************************/

qq.MultiFileUploader = function (o) {
    // call parent constructor
    qq.WSHFileUploader.apply(this, arguments);

    // additional options    
    qq.extend(this._options, {
        /* 
        Add custom options here 
        */
        inputs: []

    });
    // overwrite options with user supplied
    qq.extend(this._options, o);
    /* 
    Add custom properties here
    */
    this._inputs = this._options.inputs;

    /* Fill array of inputs */
    this._initInputs();

}

qq.extend(qq.MultiFileUploader.prototype, qq.WSHFileUploader.prototype);

qq.extend(qq.MultiFileUploader.prototype, {
    /* 
    Add custom methods here 
    */
    _onSubmit: function (id, fileName) {
        qq.WSHFileUploader.prototype._onSubmit.apply(this, arguments);
        var self = this;
        var input = this._findInputById(undefined);
        if (input != null) {
            qq.showInput(input);
            input.id = id;
            qq.setInput(input.docElement, fileName);
            input.progress.create();
            /* set event on cancel button */
            input.cancel.onclick = function (e) {
                if (input.id != undefined) {
                    self._handler.cancel(input.id);
                    self._deleteInput(input);
                }
            };
            qq.showBlock(input.cancel);
            qq.setStatus(input.status, 'loading');
            if (!this._findInputById(undefined)) {
                qq.freezButton(this._button);
            }
        } else {
            return false;
        }
    },
    _onProgress: function (id, fileName, loaded, total) {
        qq.WSHFileUploader.prototype._onProgress.apply(this, arguments);
        var input = this._findInputById(id);
        if (input != null) {
            input.progress.onProgress(loaded, total);
        }
    },
    _onComplete: function (id, fileName, result) {
        qq.WSHFileUploader.prototype._onComplete.apply(this, arguments);
        var self = this;
        var input = this._findInputById(id);
        if (input != null) {
            if (result.success) {
                qq.setInput(input.fileElement, result.file);
                input.progress.destroy();
                qq.setStatus(input.status, 'ready');
                input.cancel.onclick = function (e) {
                    /* TODO: must delete file from server */
                    self._deleteFile(input.fileElement.value);
                    self._deleteInput(input);
                };
            } else {
                alert(result.message);
                qq.setStatus(input.status, 'none');
                input.progress.destroy();
                self._deleteInput(input);
            }
        }
    },
    _initInputs: function () {
        for (key in this._inputs) {
            var input = this._inputs[key];

            input.docElement = document.getElementById(input.client);
            input.fileElement = document.getElementById(input.server);
            input.status = this._createStatusBox();
            input.progress = this._createProgressBar();
            input.cancel = this._createCancelButton();

            /* --- Add before input ---- */
            input.docElement.parentNode.insertBefore(input.status, input.docElement);
            input.docElement.parentNode.insertBefore(input.cancel, input.docElement);
            /* --- Append after input ---- */
            input.docElement.parentNode.insertBefore(input.progress._getElement(), input.fileElement.nextSibling);

            input.id = undefined;
            qq.hideInput(input);
        }
    },
    _findInputById: function (id) {
        for (key in this._inputs) {
            var input = this._inputs[key];
            if (input.id == id) {
                return input;
            }
        }
        return null;
    },
    _deleteInput: function (input) {
        var curr = this._inputs.indexOf(input);
        next = curr + 1;
        while (this._inputs[next] && this._inputs[next].id != undefined) {
            this._moveInput(this._inputs[next], this._inputs[curr]);
            next = ++curr + 1;
        }
        this._clearInput(this._inputs[curr]);
        qq.unfreezButton(this._button);
    },
    _moveInput: function (from, to) {
        to.id = from.id;
        to.docElement.value = from.docElement.value;
        to.fileElement.value = from.fileElement.value;
    },
    _clearInput: function (input) {
        input.id = undefined;
        input.docElement.value = '';
        input.fileElement.value = '';
        qq.hideBlock(input.cancel);
        input.cancel.onclick = null;
        qq.setStatus(input.status, 'none');
        qq.hideInput(input);
    }
});


/***********************************************
            Single file uploader 
************************************************/

qq.SingleFileUploader = function (o) {
    // call parent constructor
    qq.WSHFileUploader.apply(this, arguments);

    // additional options    
    qq.extend(this._options, {
        /* 
        Add custom options here 
        */
        input: {}

    });
    // overwrite options with user supplied
    qq.extend(this._options, o);
    /* 
    Add custom properties here
    */
    this._input = this._options.input;

    /* Fill input */
    this._initInput();
}

qq.extend(qq.SingleFileUploader.prototype, qq.WSHFileUploader.prototype);

qq.extend(qq.SingleFileUploader.prototype, {
    /* 
    Add custom methods here 
    */
    _onSubmit: function (id, fileName) {
        qq.WSHFileUploader.prototype._onSubmit.apply(this, arguments);
        var self = this;
        var input = this._input;
        input.id = id;
        qq.setInput(input.docElement, fileName);
        input.progress.create();
        /* set event on cancel button */
        input.cancel.onclick = function (e) {
            self._handler.cancel(input.id);
            qq.hideBlock(input.cancel);
        };
        qq.showBlock(input.cancel);
        qq.setStatus(input.status, 'loading');
        qq.freezButton(this._button);
    },
    _onProgress: function (id, fileName, loaded, total) {
        qq.WSHFileUploader.prototype._onProgress.apply(this, arguments);
        this._input.progress.onProgress(loaded, total);
    },
    _onComplete: function (id, fileName, result) {
        qq.WSHFileUploader.prototype._onComplete.apply(this, arguments);
        var self = this;
        var input = this._input;
        input.progress.destroy();
        input.cancel.onclick = null;
        qq.hideBlock(input.cancel);
        qq.unfreezButton(this._button);
        if (result.success) {
            if (input.fileElement.value != '') {
                self._deleteFile(input.fileElement.value);
            }
            qq.setInput(input.fileElement, result.file);
            qq.setStatus(input.status, 'ready');
            qq.buttonChangeName(this._button);
        } else {
            /* Show Error */
            alert(result.message);
            /* Clear input */
            qq.setStatus(input.status, 'none');
            qq.setInput(input.docElement, '');
        }
    },
    _initInput: function () {
        var input = this._input;

        input.docElement = document.getElementById(input.client);
        input.fileElement = document.getElementById(input.server);
        input.status = this._createStatusBox();
        input.progress = this._createProgressBar();
        input.cancel = this._createCancelButton();

        /* --- Add before input ---- */
        input.docElement.parentNode.parentElement.insertBefore(input.status, input.docElement.parentNode);
        input.docElement.parentNode.parentElement.insertBefore(input.cancel, input.docElement.parentNode);
        /* --- Append after input ---- */
        input.docElement.parentNode.insertBefore(input.progress._getElement(), input.fileElement.nextSibling);

        input.id = undefined;
    }
});


var uploader1 = uploader1 || null;
var uploader2 = uploader1 || null;

$(function () {

    if (uploader1 == null) {
        uploader1 = new qq.SingleFileUploader({
            debug: true,
            element: document.getElementById('original-doc-uploader'),
            allowedExtensions: ['doc', 'docx', 'rtf', 'xml', 'pdf'],
            button: $("#original-doc-button")[0],
            sizeLimit: 41943040, // max size
            action: '/Upload/File',
            multiple: false,
            input: { client: 'OriginalDoc', server: 'OriginalDocFile' }
        });
    }

    if (uploader2 == null) {
        uploader2 = new qq.MultiFileUploader({
            debug: true,
            element: document.getElementById('modified-doc-uploader'),
            allowedExtensions: ['doc', 'docx', 'rtf', 'xml', 'pdf'],
            button: $("#modified-doc-button")[0],
            sizeLimit: 41943040, // max size
            action: '/Upload/File',
            multiple: true,
            inputs: [{ client: 'ModifiedDoc1', server: 'ModifiedDocFile1' },
                     { client: 'ModifiedDoc2', server: 'ModifiedDocFile2' },
                     { client: 'ModifiedDoc3', server: 'ModifiedDocFile3' },
                     { client: 'ModifiedDoc4', server: 'ModifiedDocFile4' },
                     { client: 'ModifiedDoc5', server: 'ModifiedDocFile5'}]
        });
    }

    /******************************************
    Wizard
    ******************************************/
    var validator = $("form").validate();

    $(".wizard-step:first").fadeIn();   // show first step

    $("#back-step").attr('disabled', 'disabled');
    $("#back-step").click(function () {
        // get current step
        var $step = $(".wizard-step:visible");
        if ($step.hasClass("final")) {
            $("#next-step").children("span").text("Next >");
        }
        if ($step.prev().hasClass("wizard-step")) {
            var $prev = $step.prev();
            $step.hide();
            $prev.fadeIn();

            if (!$prev.prev().hasClass("wizard-step")) {
                $("#back-step").attr('disabled', 'disabled');
            }
        }
    });

    $("#next-step").click(function () {
        var $curr = $(".wizard-step:visible");
        var anyError = false;

        $curr.find("input").each(function () {
            if (!validator.element(this)) {
                anyError = true;
            }
        });

        if (anyError) return false;

        if ($curr.next().hasClass("wizard-step")) {
            var $next = $curr.next();
            $curr.hide();
            $next.fadeIn();
            if ($next.hasClass("final")) {
                $("#next-step").children("span").text("Compare");
            }
            $("#back-step").removeAttr('disabled');
            if ($curr.hasClass("final")) {
                $("#result-progressbar").show();
                $("#back-step").unbind('click').hide();
                $("#next-step").unbind('click').hide();
                comparer.tryCompare();
                uploader1._input.fileElement.onBlur = comparer.tryCompare;
                uploader2._inputs[0].fileElement.onBlur = comparer.tryCompare;
            }
        }
    });
});