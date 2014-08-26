
/********************************************
            Rendering set list
********************************************/
var rs = rs || {};

rs.extend = function (first, second) {
    for (var prop in second) {
        first[prop] = second[prop];
    }
}

rs.RSList = function (o) {
    this._options = {
        dummy: ''
    }
    rs.extend(this._options, o);

    /*
    Get List item color
    */
    var tmp_li = document.createElement('li');
    $(this._list).append(tmp_li);
    this._item_color = $(tmp_li).css("background-color");
    $(tmp_li).remove();

    this._element = this._options.element;
}

rs.RSList.prototype = {
    _addItem: function (fileName, animate, before) {
        if (this._element) {
            var li = document.createElement('li');
            var file = document.createElement('div');
            file.setAttribute('class', 'rs-list-file');
            $(file).text(this._getFileNameWithoutExtention(fileName));
            var btn = document.createElement('div');
            btn.setAttribute('class', 'rs-list-button');
            $(btn).click(function () {
                var _item = li;
                var _self = this;
                var _btn = btn;
                var _file = file;
                $(_btn).click(null);            // ---- Button Delete Event 
                _file.style.backgroundColor = '#fef1f1';
                $.ajax({
                    type: "POST",
                    url: "/Upload/Delete",
                    data: { fileName: fileName },
                    success: function (result) {
                        $(_item).remove();
                        if (result.success == false) {
                            $(_file).removeAttr('style');
                            _self._showError(result.message);
                        } 
                    },
                    error: function (req, status, error) {
                        $(_file).removeAttr('style');
                        _self._showError(error);
                    }
                });
            })
            file.appendChild(btn);
            li.appendChild(file);
            if (before) {
                this._element.insertBefore(li, before);
            } else {
                this._element.appendChild(li);
            }
            if (animate) {
                file.style.backgroundColor = "#e1fee1";
                $(file).delay(1000).animate({
                    backgroundColor: this._item_color
                }, {
                    duration: 2000,
                    complete: function () {
                        $(this).removeAttr('style');
                    }
                });
            }
        }
    },
    _insertItem: function (fileName) {
        if (this._element) {
            var before = undefined;
            var items = $(this._element).children('li');
            for (var ind = 0; ind < items.length; ind++) {
                var item = items[ind];
                var text = $(item).text();
                if (fileName.localeCompare(text) == -1) {
                    before = item;
                    break;
                }
            }
            this._addItem(fileName, true, before);
        }
    },
    _getFileNameWithoutExtention: function (fileName) {
        var ind = fileName.lastIndexOf('.');
        if (ind != -1) {
            return fileName.substring(0, ind);
        }
        return fileName;
    },
    _fillList: function (data) {
        $(this._element).children('li').each(function (item) { $(item).remove(); })
        for (var ind in data) {
            this._addItem(data[ind], false);
        }
    },
    _loadData: function () {
        var _self = this;
        $.ajax({
            type: "POST",
            url: "/RenderingSet/Load",
            data: {},
            success: function (result) {
                if (result.success == true) {
                    var dd = $.parseJSON(result.data);
                    _self._fillList(dd);
                } else {
                    _self._showError(result.message);
                }
            },
            error: function (req, status, error) {
                _self._showError(error);
            }
        });
    },
    _showError: function (message) {
        utils.showError(message);
    },
    _onDeleteItem: function (element, fname) {

    }
}


/**********************************************
        Rendering Set uploader
***********************************************/

qq.RSFileUploader = function (o) {
    // call parent constructor
    qq.FileUploaderBasic.apply(this, arguments);

    // additional options
    qq.extend(this._options, {
        /* 
        Add custom options here
        */
        dummy: ''

    });

    qq.extend(this._options, o);
    /*
    Add custom properties here
    */
    this._list = this._options.list;
    this._rslist = this._options.rslist;
    
    if (this._options.button) {
        this._button = this._createUploadButton(this._options.button);
    }
}

qq.extend(qq.RSFileUploader.prototype, qq.FileUploaderBasic.prototype);

qq.extend(qq.RSFileUploader.prototype, {
    /*
    Add custom methods here
    */
    _onSubmit: function (id, fileName) {
        qq.FileUploaderBasic.prototype._onSubmit.apply(this, arguments);
        this._addListItem(id, fileName);
    },
    _onProgress: function (id, fileName, loaded, total) {
        qq.FileUploaderBasic.prototype._onProgress.apply(this, arguments);
        var text = Math.round(loaded / total * 100) + '%';
        var item = this._getItemByFileId(id);
        var divs = $(item).children("div.rs-upload-size");
        if (divs.length > 0) {
            $(divs[0]).text(text);
        }
    },
    _onComplete: function (id, fileName, result) {
        qq.FileUploaderBasic.prototype._onComplete.apply(this, arguments);
        this._removeListItem(id);
        if (result.success == true) {
            if (this._rslist) {
                this._rslist._insertItem(fileName);
            }
        } else {
            this._showError(result.message);
        }
    },
    _addListItem: function (id, fileName) { /* This function create upload list element */
        if (this._list) {
            var li = document.createElement('li');
            li.rsFileId = id;
            var spinner = document.createElement('div');
            spinner.setAttribute('class', 'rs-upload-spinner rs-upload-item');
            var size = document.createElement('div');
            size.setAttribute('class', 'rs-upload-size rs-upload-item');
            var file = document.createElement('div');
            file.setAttribute('class', 'rs-upload-file rs-upload-item');
            $(file).text(fileName);
            li.appendChild(spinner);
            li.appendChild(size);
            li.appendChild(file);
            this._list.appendChild(li);
        }
    },
    _removeListItem: function (id) {
        if (this._list) {
            var item = this._getItemByFileId(id);
            $(item).remove();
        }
    },
    _getItemByFileId: function (id) {
        var item = this._list.firstChild;

        while (item) {
            if (item.rsFileId == id) return item;
            item = item.nextSibling;
        }
    },
    _showError: function (message) {
        utils.showError(message);
    }
});


/***************************************/

var uploader = uploader || null;
var rslist = rslist || null;

$(function () {

    if (rslist == null) {
        rslist = new rs.RSList({
            element: document.getElementById('rs-list')
        });
        rslist._loadData();
    }

    if (uploader == null) {
        uploader = new qq.RSFileUploader({
            element: document.getElementById('rs-uploader'),
            button: document.getElementById('rs-upload-button'),
            list: document.getElementById('rs-upload-list'),
            allowedExtensions: ['set'],
            sizeLimit: 41943040,
            rslist: rslist,
            action: 'Upload/File',
            multiple: true
        });
    }

});