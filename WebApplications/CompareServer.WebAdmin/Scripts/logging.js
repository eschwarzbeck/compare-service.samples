

var log = log || {};

log.extend = function (first, second) {
    for (var key in second) {
        first[key] = second[key];
    }
}

/* 
    Log List object 
*/

log.List = function (o) {
    this._options = {
        url: '',
        element: null,
        error: null,
        pajinate: {
            items_per_page : 10,
            item_container_id: '.list-items',
            nav_panel_id: '.list-navigation'
        }
    };

    log.extend(this._options, o);

    this._url = this._options.url;
    this._element = this._options.element;
    this._error = this._options.error;
};


log.List.prototype = {
    _viewLogs: function (startDate, endDate) {

        var _self = this;
        /* - get data -*/
        $("#log-data-load-spinner").show();

        $.ajax({
            async: false,
            type: 'POST',
            url: this._url,
            data: { start: startDate,
                end: endDate
            },
            success: function (result, textStatus) {
                $("#log-data-load-spinner").hide();
                if (result.success) {
                    var data = $.parseJSON(result.data);
                    _self._createList(data);
                } else {
                    _self._viewError(result.message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#log-data-load-spinner").hide();
                _self.showError(errorThrown);
                return false;
            }
        });
    },
    _itemToHtml: function (item) {
        var li = document.createElement('li');
        $(li).text(item);
        return li;
    },
    _createList: function (data) {
        var listclass = this._options.pajinate.item_container_id;
        var children = $(this._element).children('ul' + listclass);
        if (children.length > 0) {
            this._list = children[0];
            $(this._list).empty();
        } else {
            this._list = document.createElement('ul');
            $(this._list).attr({ class: listclass.substring('.', '') });
            this._element.appendChild(this._list);
        }

        /*- fill list -*/
        for (var key in data) {
            this._list.appendChild(this._itemToHtml(data[key]));
        }

        /* Pajinate */
        $(this._element).pajinate(this._options.pajinate);
    },
    _viewError: function (message) {
        utils.showError(message);
    }
}


/* ========= Audit Log List ============== */

log.AuditList = function (o) {
    log.List.apply(this, arguments);

    log.extend(this._options, {
        url: '/Logging/AuditLogs'
    });
    log.extend(this._options, o);

    this._url = this._options.url;
};

log.extend(log.AuditList.prototype, log.List.prototype);

log.extend(log.AuditList.prototype, {
    _itemToHtml: function (item) {
        var logTime = item.LogTime.toDate();
        if (logTime != '' && typeof logTime.format === 'function') {
            logTime = logTime.format('yyyy-MM-dd HH:mm:ss');
        }
        var li = document.createElement('li');
        var left = document.createElement('div');
        $(left).attr({ class: 'log-item-left' });
        var span1 = document.createElement('span');
        $(span1).text(logTime);
        var span2 = document.createElement('span');
        $(span2).text(item.UserName);
        $(left).append(span1, '</br> UserName <br/> ', span2);
        var mid = document.createElement('div');
        $(mid).attr({ class: 'log-item-mid' });
        var text = '';
        for (var key in item) {
            if (key == 'UserName' || key == 'LogTime') continue;
            text += key + " = " + item[key] + ", ";
        }
        $(mid).text(text);
        $(li).append(left, mid);
        return li;
    }
});


/* ========= Host Log List ============== */

log.HostList = function (o) {
    log.List.apply(this, arguments);

    log.extend(this._options, {
        url: '/Logging/HostLogs'
    });
    log.extend(this._options, o);

    this._url = this._options.url;
};

log.extend(log.HostList.prototype, log.List.prototype);

log.extend(log.HostList.prototype, {
    _itemToHtml: function (item) {
        var logTime = item.LogTime.toDate();
        if (logTime != '' && typeof logTime.format === 'function') {
            logTime = logTime.format('yyyy-MM-dd HH:mm:ss');
        }
        var li = document.createElement('li');
        var left = document.createElement('div');
        $(left).attr({ class: 'log-item-left' });
        var span1 = document.createElement('span');
        $(span1).text(logTime);
        $(left).append(span1, '</br> ');
        var mid = document.createElement('div');
        $(mid).attr({ class: 'log-item-mid' });
        var text = '';
        for (var key in item) {
            if (key == 'LogTime') continue;
            text += key + " = " + item[key] + ", ";
        }
        $(mid).text(text);
        $(li).append(left, mid);
        return li;
    }
});


/* ========= System Log List ============== */

log.SystemList = function (o) {
    log.List.apply(this, arguments);

    log.extend(this._options, {
        url: '/Logging/SystemLogs'
    });
    log.extend(this._options, o);

    this._url = this._options.url;
};

log.extend(log.SystemList.prototype, log.List.prototype);

log.extend(log.SystemList.prototype, {
    _itemToHtml: function (item) {
        var logTime = item.LogTime.toDate();
        if (logTime != '' && typeof logTime.format === 'function') {
            logTime = logTime.format('yyyy-MM-dd HH:mm:ss');
        }
        var li = document.createElement('li');
        var left = document.createElement('div');
        $(left).attr({ class: 'log-item-left' });
        var span1 = document.createElement('span');
        $(span1).text(logTime);
        $(left).append(span1, '</br> ');
        var mid = document.createElement('div');
        $(mid).attr({ class: 'log-item-mid' });
        $(mid).text(item.Message);
        $(li).append(left, mid);
        return li;
    }
});

/* ========== System Log Files List ============ */

log.SystemFilesList = function (o) {
    log.List.apply(this, arguments);

    log.extend(this._options, {
        url: '/Logging/SystemLogsList'
    });
    log.extend(this._options, o);

    this._url = this._options.url;
}

log.extend(log.SystemFilesList.prototype, log.List.prototype);

log.extend(log.SystemFilesList.prototype, {
    _itemToHtml: function (item) {
        var li = document.createElement('li');
        var cont = document.createElement('div');
        $(cont).attr({ class: 'log-item-cont' })
        if (item.length == 8 || item == "") {
            var date;
            if (item == "") {
                date = new Date();
            } else {
                var year = item.substr(0, 4);
                var month = item.substr(4, 2);
                var day = item.substr(6, 2);
                date = new Date(year, month - 1, day);
            }
            var a = document.createElement('a');
            $(a).text(date.format("dddd, MMMM d, yyyy"));
            $(a).click(function () {
                var dd = utils.getStringFromDate(date);
                systemlog._viewLogs(dd, null);
            });
            $(cont).append(a);
        } else {
            utils.showError(item + " is not propper Date");
            $(cont).text(item);
        }
        // Override css height value with online property
        $(li).height("15px");
        $(li).append(cont);
        return li;
    }
});