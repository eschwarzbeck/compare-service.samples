

var audit = audit || {};


audit.viewUserActivity = function () {

    audit.clearList();
    $("#audit-data-load-spinner").show();

    $.ajax({
        url: '/Auditing/UsersActivity',
        type: 'POST',
        data: {},
        success: function (result, textStatus) {
            $("#audit-data-load-spinner").hide();
            if (result.success) {
                var data = $.parseJSON(result.data);
                audit.drawList(data);
            } else {
                audit.viewError(result.message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#audit-data-load-spinner").hide();
            audit.viewError(errorThrown);
        }
    });
}

audit.drawList = function (data) {
    var list = $(".list-items");
    if (data.length > 0 && list.length > 0) {
        list = list[0];
        for (var key in data) {
            var item = data[key];
            /* Create List Item */
            var li = document.createElement('li');
            var container = document.createElement('div');
            var name = document.createElement('div');
            $(name).attr({ class: 'auditing-list-item-name' }).text(item.UserName);
            var num = document.createElement('div');
            $(num).attr({ class: 'auditing-list-item-num' }).text(item.UsedNumber);
            var bytes = document.createElement('div');
            $(bytes).attr({ class: 'auditing-list-item-bytes' }).text(item.TotalBytesCompared);
            var time = document.createElement('div');
            $(time).attr({ class: 'auditing-list-item-time' }).text(item.TotalProcessingTime);
            $(container).append(name, num, bytes, time).attr({ class: 'auditing-list-item-container' });
            $(li).append(container);
            $(list).append(li);
        }

        $("#audit-list-container").pajinate({
            items_per_page: 10,
            nav_panel_id: '.list-navigation'
        });
    }
}

audit.clearList = function () {
    $(".list-items").each(function () {
        $(this).empty();
    });
    $(".list-mavigation").each(function () {
        $(this).empty();
    });
}

audit.viewError = function (message) {
    utils.showError(message);
}


$(function () {

    audit.viewUserActivity();

});