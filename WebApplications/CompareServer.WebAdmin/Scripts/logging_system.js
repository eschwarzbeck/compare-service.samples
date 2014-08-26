
var systemlog = systemlog || null;
var systemfiles = systemfiles || null;
//var daterange = daterange || null;


function getDateToView() {
    var date = $("#log-list-arg-date").val();
    if (utils.isValidDate(date)) {
        return date;
    }
    return null;
}

$(function () {
    /* create Log object */
    if (systemlog == null) {
        systemlog = new log.SystemList({
            element: document.getElementById('log-view')
        });
    }

    if (systemfiles == null) {
        systemfiles = new log.SystemFilesList({
            element: document.getElementById('log-view')
        })
    }

    /*-- Get Logs for selected range --*/
    //var dateToView = getDateToView();
    //if (dateToView != null) {
    //    systemlog._viewLogs(dateToView, null);
    //}
    systemfiles._viewLogs();

    $("#log-list-button").click(function () {
        var dateToView = getDateToView();
        if (dateToView != null) {
            systemlog._viewLogs(dateToView, null);
        }
    });

    $(".date-select").each(function () {
        $(this).datepicker({ dateFormat: 'mm-dd-yy' });
    });
});    