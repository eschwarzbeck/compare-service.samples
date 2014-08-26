
var utils = utils || {};

utils.extend = function (first, second) {
    for (var key in second) {
        first[key] = second[key];
    }
}

utils.extend_recursive = function (first, second) {
    for (var key in second) {
        var val = second[key];
        if ("object" == typeof val) {
            if (first[key] == undefined) {
                first[key] = second[key];
            } else {
                utils.extend_recursive(first[key], second[key]);
            }
        } else {
            first[key] = second[key];
        }
    }
}

utils.pad = function(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

utils.getFilePath = function (file) {
    if (file == "" ) {
        return file
    }
    var path = "";
    var ind = file.lastIndexOf("/");
    if (ind == -1) {
        ind = file.lastIndexOf("\\");
    }
    if (ind != -1) {
        path = file.substring(0, ind);
    }
    return path;
}

/* 
    Date functions 
*/

utils.getEndDate = function (date, range) {
    var end = utils.getDateFromString(date);
    switch (range) {
        case "1":
            end.setDate(end.getDate() - 1);
            return end;
        case "7":
            end.setDate(end.getDate() - 7);
            return end;
        case "30":
            end.setMonth(end.getMonth() - 1);
            return end;
        case "365":
            end.setYear(end.getYear() - 1);
            return end;
        default:
            return end;
    }
}

utils.getDateFromString = function (s) {
    var arr = s.split("-");
    return new Date(arr[2], arr[0] - 1, arr[1]);
}

utils.getUTCFromString = function (s) {
    var date = this.getDateFromString(s);
    var time = date.getTime()
    return time;
}

utils.getStringFromDate = function (date) {
    return date.format('MM-dd-yyyy');
}


utils.isValidDate = function (s) {
    // format M(M)/D(D)/(YY)YY
    var dateFormat = /^\d{1,2}[\.|\/|-]\d{1,4}[\.|\/|-]\d{1,4}$/;
    if (dateFormat.test(s)) {
        // remove any leading zeros from date values
        s = s.replace(/0*(\d*)/gi, "$1");
        var dateArray = s.split(/[\.|\/|-]/);
        // correct month value
        dateArray[0] = dateArray[0] - 1;
        // correct year value
        if (dateArray[2].length < 4) {
        // correct year value
            dateArray[2] = (parseInt(dateArray[2]) < 50) ? 2000 + parseInt(dateArray[2]) : 1900 + parseInt(dateArray[2]);
        }
        var testDate = new Date(dateArray[2], dateArray[0], dateArray[1]);
        if (testDate.getDate() != dateArray[1] || testDate.getMonth() != dateArray[0] || testDate.getFullYear() != dateArray[2]) {
            return false;
        } else {
            return true;
        }
    } else {
        return false;
    }
}

// Serialized string to date
String.prototype.toDate = function () {
    "use strict";

    var match = /\/Date\((\d{13})\)\//.exec(this);

    return match === null ? null : new Date(parseInt(match[1], 10));
};

utils.Spinner = function () {
    var obj = document.createElement('img');
    $(obj).attr({ class: 'spinner' });
    return obj;
}

utils.showError = function (message) {
    var cont = $('.list-errors');
    if (cont.length > 0) {
        cont = cont[0];
        var list = $(cont).children("ul");
        if (list.length > 0) {
            list = list[0];
        } else {
            list = document.createElement("ul");
            $(cont).append(list);
        }
        var li = document.createElement("li");
        $(list).append(li);
        $(li).text(message).delay(8000).fadeOut(4000).queue(function () { $(this).remove(); });
    }
}


/* 
Range Object
*/

utils.DateRange = function (o) {
    this._options = {
        select: {
            week: 7,
            month: 30
        },
        defValueEnd: '',
        defValueStart: '',
        defValFrom: ''
    }
    utils.extend(this._options, o);

    this._driven = false;
    this._select = this._options.select;

    if (this._options.element != undefined) {
        this._element = this._options.element;
        this._drawObject();
    }
};

utils.DateRange.prototype = {
    _getSelectedRange: function () {
        var select = $(this._element).children('select');
        if (select.length < 1) return false;
        select = select[0];
        if (select.value == 'p') {
            var inputs = $(this._opt2).children('input');
            if (inputs.length >= 2) {
                var end = $(inputs[0]).val();
                var start = $(inputs[1]).val();
                if (utils.isValidDate(end)) {
                    if (utils.isValidDate(start)) {
                        var s = utils.getDateFromString(start);
                        var n = utils.getDateFromString(end);
                        if (s < n) {
                            alert("Range Error");
                            return false;
                        }
                        return { startDate: start, endDate: end };
                    } else {
                        alert("End date format is invalid");
                    }
                } else {
                    alert("Start date format is invalid");
                }
            }
            return false;
        } else {
            var inputs = $(this._opt1).children('input');
            if (inputs.length >= 1) {
                var date = $(inputs[0]).val();
                if (utils.isValidDate(date)) {
                    var start = date;
                    var end = utils.getEndDate(start, select.value);
                    end = utils.getStringFromDate(end);
                    return { startDate: date, endDate: end }
                } else {
                    alert("Date format is invalid");
                }
            }
            return false;
        }
    },
    _drawObject: function () {
        /* draw container */
        var select = document.createElement('select');
        select.setAttribute('class', 'range-select');
        var option = document.createElement('option');
        $(option).attr({ value: 'p', selected: 'selected' });
        $(option).text("period");
        $(select).append(option);
        for (var key in this._select) {
            option = document.createElement('option');
            $(option).attr({ value: this._select[key] });
            $(option).text(key);
            $(select).append(option);
        }

        var opt_container = document.createElement('div');
        opt_container.setAttribute('class', 'range-opt-container');
        this._opt1 = document.createElement('div');
        this._opt2 = document.createElement('div');
        var input1 = document.createElement('input');
        $(input1).attr({ type: 'text',
            class: 'date-select',
            id: 'range-date-from',
            value: this._options.defValFrom
        }).datepicker({ dateFormat: 'mm-dd-yy' });
        var input2 = document.createElement('input');
        $(input2).attr({ type: 'text',
            class: 'date-select',
            id: 'range-date-end',
            value: this._options.defValueEnd
        }).datepicker({ dateFormat: 'mm-dd-yy' });
        var input3 = document.createElement('input');
        $(input3).attr({ type: 'text',
            class: 'date-select',
            id: 'range-date-start',
            value: this._options.defValueStart
        }).datepicker({ dateFormat: 'mm-dd-yy' });
        $(this._opt1).text("up to");
        $(this._opt1).append(input1);
        $(this._opt1).hide();
        $(this._opt2).text("from");
        $(this._opt2).append(input2);
        $(this._opt2).append(" to ");
        $(this._opt2).append(input3);
        $(opt_container).append(this._opt1, this._opt2);
        $(this._element).empty();
        $(this._element).append(select, opt_container);

        $(select).change({ opt1: this._opt1, opt2: this._opt2 }, function (event) {
            var opt1 = event.data.opt1;
            var opt2 = event.data.opt2;
            if (this.value == 'p' && $(opt2).is(":hidden")) {
                $(opt1).hide();
                $(opt2).css({ left: -100, position: "relative" });
                $(opt2).show().animate({ left: 0 }, function () { $(opt2).css({ position: "static" }) });

            } else if ($(opt1).is(":hidden")) {
                $(opt2).hide();
                var width = $(opt1).width();
                $(opt1).css({ left: -100, position: "relative" });
                $(opt1).show().animate({ left: 0 }, function () { $(opt1).css({ position: "static" }) });
            }
        });

        this._driven = true;
    }
};

/*
    Toggle Object
*/

utils.ToggleSwitch = function (o) {
    this._options = {
        id: 'toggle-switch',
        label: 'Toggle Switch',
        hidelable: true,
        element: null,
        values: ['true', 'false'],
        onToggleChange: function (event) {}
    };
    utils.extend(this._options, o);

    this._element = this._options.element;
    this._values = this._options.values;

    this._createElement();
};

utils.ToggleSwitch.prototype = {
    _createElement: function () {

        if (this._element != null && this._values.length >= 2) {
            var label = document.createElement('lable');
            var select = document.createElement('select');
            $(label).text(this._options.label).attr({ 'for': this._options.id });
            for (var key in this._values) {
                var option = document.createElement('option');
                $(option).text(this._values[key]).attr({ value: key });
                if (key == 0) {
                    $(option).attr({ selected: 'selected' });
                }
                $(select).append(option);
            }
            $(select).attr({ id: this._options.id });
            $(this._element).append(label, select);
            if (this._options.hidelable) {
                $(label).hide();
            }
            this._select = select;

            $(select).change(this._options.onToggleChange);
        }
    },
    _getSelectedValue: function () {
        if (this._select != null) {
            return this._select.value;
        }
        return null;
    }
};


