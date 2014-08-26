
var hostlog = hostlog || null;
var daterange = daterange || null;

$(function () {

    /* create Log object */
    if (hostlog == null) {
        hostlog = new log.HostList({
            element: document.getElementById('log-view')
        });
    }

    /* -- Create Date Range object -- */
    if (daterange == null) {
        /* Set default Dates range */
        var now = new Date();
        var from = new Date();
        from.setDate(from.getDate() - 60);
        now = utils.getStringFromDate(now);
        from = utils.getStringFromDate(from);

        var daterange = new utils.DateRange({
            element: document.getElementById('range-container'),
            select: {
                week: 7,
                month: 30,
                year: 365
            },
            defValueEnd: from,
            defValueStart: now,
            defValFrom: now
        });
    }

    /*-- Get Logs for selected range --*/
    var dates = daterange._getSelectedRange();
    if (dates != false) {
        hostlog._viewLogs(dates.startDate, dates.endDate);
    }

    $("#log-list-button").click(function () {
        var dates = daterange._getSelectedRange();
        if (dates != false) {
            hostlog._viewLogs(dates.startDate, dates.endDate);
        }
    });

});    