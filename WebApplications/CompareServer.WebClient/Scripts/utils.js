
var utils = utils || {};

utils.extend = function (first, second) {
    for (var key in second) {
        first[key] = second[key];
    }
}

utils.pad = function(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}


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