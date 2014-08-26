

/* performance namespace */
var pf = pf || {};

pf.caltudateIntervalAndBarWidth = function (stat) {
    var startDate = stat[0][0][0];
    var endDate = stat[0][stat[0].length - 1][0];

    var minInterval = 24 * 60 * 60 * 1000; /* one day */
    var maxBarWidth = 100;
    var minBarWidth = 1;

    var diff = Math.abs(startDate - endDate);

    var intv = diff / 17; /* max 17 days dates can be viewed in chart */

    if (intv < minInterval) {
        intv = minInterval;
    } else {
        var d = Math.ceil(intv / minInterval);
        if (d > 5) d++;
        intv = d * minInterval;
    }

    var days = Math.ceil(diff / minInterval);
    if (days < 1) days = 1;
    var width = Math.floor(500 / days);  /* 500 pixes width of chart */
    if (width > maxBarWidth) width = maxBarWidth;
    if (width < minBarWidth) width = minBarWidth;

    return { interval: intv, barWidth: width };
}

pf.drawCharts = function (arr, o) {
//    var options = pf.caltudateIntervalAndBarWidth(arr);
    var o = o || {};

    if (arr != undefined && arr.length > 0) {
        /* draw comparison chart */
        
        /* select corrent renderer for chart */ 
        if (tswitch != null && tswitch != undefined)
        {
            var value = tswitch._getSelectedValue();
            var renderer = pf.getAxisRenderer(value);
            if (renderer != null) {
                var ropt = {
                   series: [{
                            renderer: renderer
                   }]
                }
                utils.extend_recursive(o, ropt);
            }
        }

        var opt = {
            title: 'Number of comparisons per day'
        };
        utils.extend_recursive(opt, o);
        this.drawChart( 'comparisons-chart',
                        this.getComparisonsData(arr), 
                        opt);

        /* draw Average length of comparison chart */
        var opt = {
            title: 'Average length of comparison per day'
        };
        utils.extend_recursive(opt, o);
        this.drawChart( 'avg-comp-len-chart', 
                        this.getAvgCompLenData(arr), 
                        opt);

        /* draw Average length of processing time  chart */
        var opt = {
            title: 'Average length of processing time per day',
            axes: {
                yaxis: {
                    renderer: $.jqplot.TimeAxisRenderer
                }
            }
        };
        utils.extend_recursive(opt, o);
        this.drawChart( 'avg-proc-time-chart',
                        this.getAvgProcTimeData(arr), 
                        opt);

        /* draw Average length of the document compare chart */
        var opt = {
            title: 'Average length of the document compare per day',
            axes: {
                yaxis: {
                    renderer: $.jqplot.DataSizeAxisRenderer
                }
            }
        };
        utils.extend_recursive(opt, o);
        this.drawChart( 'avg-doc-len-chart',
                        this.getAvgDocLenData(arr),
                        opt);
    }
}

pf.clearCharts = function () {
    $("#comparisons-chart").empty();
    $("#avg-comp-len-chart").empty();
    $("#avg-proc-time-chart").empty();
    $("#avg-doc-len-chart").empty();
}

pf.drawChart = function (element, data, options) {
    var div = document.getElementById(element);
    $(div).empty();

    var opt = pf.caltudateIntervalAndBarWidth(data);
    
    /* - Common options for our charts - */
    var copt = {
        series: [{
//            renderer: $.jqplot.BarRenderer,
            rendererOptions: {
                barWidth: opt.barWidth
            }
        }],
        axesDefaults: {
            tickRenderer: $.jqplot.CanvasAxisRenderer,
            tickOptions: {
                fontSize: '10pt',
                showMark: false,
                markSize: 50
            },
        },
        axes: {
            xaxis: {
                renderer: $.jqplot.DateAxisRenderer,
                /* Set Tick Interval or Number of Ticks but not both */
//                tickInterval: opt.interval,
                numberTicks: 10,
                tickOptions: {
                    formatString: '%d %b'
                }
            },
            yaxis: {
                pad: 0, 
                padMin: 0, 
                padMax: 0
            }
        },
        highlighter: {
            show: true,
            showMarker: false,
            tooltipLocation: 'n',
            sizeAdjust: 10
        },
        cursor: {
            show: false
        },
        grid: {
            background: '#E9E3E3'
        }
    };

    if (options != null && options != undefined)
    {
        utils.extend_recursive(copt, options);
    }
    if (copt.title != undefined){
        copt.title = '<h3>' + copt.title + '</h3>';
    }

    pf.comparisons_chart = $.jqplot(element, data, copt);
}


pf.getEndDate = function(date, range) {
    var end = utils.getDateFromString(date);
    switch (range) {
        case "1":
            end.setDate(date.getDate() - 1);
            return end;
        case "7":
            end.setDate(date.getDate() - 7);
            return end;
        case "30":
            end.setMonth(date.getMonth() - 1);
            return end;
        case "365":
            end.setYear(date.getYear() - 1);
            return end;
        default:
            return end;
    }
}

pf.getComparisonsData = function (arr) {
    var result = Array();
    var ind;
    for (ind = 0; ind < arr.length; ind++) {
        var dd = Math.round(arr[ind].comparisons);
        var nn = Number(arr[ind].comparisons);
        var cc = Math.round(nn);
        result.push([utils.getUTCFromString(arr[ind].date), Math.round(arr[ind].comparisons)]);
    }
    return [result];
}

pf.getAvgCompLenData = function (arr) {
    var result = Array();
    var ind;
    for (ind = 0; ind < arr.length; ind++) {
        result.push([utils.getUTCFromString(arr[ind].date), arr[ind].avgcomplen]);
    }
    return [result];
}

pf.getAvgProcTimeData = function (arr) {
    var result = Array();
    var ind;
    for (ind = 0; ind < arr.length; ind++) {
        result.push([utils.getUTCFromString(arr[ind].date), arr[ind].avgproctime]);
    }
    return [result];
}

pf.getAvgDocLenData = function (arr) {
    var result = Array();
    var ind;
    for (ind = 0; ind < arr.length; ind++) {
        result.push([utils.getUTCFromString(arr[ind].date), arr[ind].avgdoclen]);
    }
    return [result];
}

pf.populateCharts = function (startDate, endDate) {
    var dd = { start: startDate,
        end: endDate
    }

    $("#perf-data-load-spinner").css('visibility', 'visible');
    pf.clearCharts();

    $.ajax({
        type: "POST",
        url: "performance/Statistics",
        data: dd,
        success: function (result) {
            $("#perf-data-load-spinner").css('visibility', 'hidden');
            if (result.success) {
                stat = $.parseJSON(result.statistics);
                var totalComparisons = result.totalcomp;
                var totalBytes = result.totalbytes;
                var opt = {}
                pf.drawCharts(stat, opt);
                pf.drawTotals(totalComparisons, totalBytes);
            } else {
                stat = null;
                utils.showError(result.message);
            }
        },
        error: function (req, status, error) {
            $("#perf-data-load-spinner").css('visibility', 'hidden');
            utils.showError(error);
        }
    });
}

pf.drawTotals = function (totalComp, totalBytes) {
    $("#perf-total-comp-num").text(totalComp);
    $("#perf-total-bytes-num").text($.jqplot.bytesToSize(totalBytes, 2));
}

pf.getAxisRenderer = function(value) {
    if (value == null || value == '0')
    {
        return $.jqplot.BarRenderer;
    }
    return null;
}

pf.updateServiceStatus = function(status){
    switch(status)
    {
        case 1: //stoped
            $(".perf-service-status img").each(function(){
                var path = $(this).attr("src");
                var path = utils.getFilePath(path) + "/status_off.png";
                $(this).attr("src", path);
            });
            $("#service-start-button").removeAttr("disabled");
            $("#service-stop-button").attr("disabled", "");
            break;
        case 4: //running
            $(".perf-service-status img").each(function(){
                var path = $(this).attr("src");
                var path = utils.getFilePath(path) + "/status_on.png";
                $(this).attr("src", path);
            });
            $("#service-start-button").attr("disabled", "");
            $("#service-stop-button").removeAttr("disabled");
            break;
        default:
            $(".perf-service-status img").each(function(){
                var path = $(this).attr("src");
                var path = utils.getFilePath(path) + "/status_unknown.png";
                $(this).attr("src", path);
            });
            $("#service-start-button").attr("disabled", "");
            $("#service-stop-button").attr("disabled", "");            
    
    }
}

pf.startService = function(){
    $(".perf-service-handle input").each(function(){
        $(this).attr({disabled: ""});
    });
    $(".perf-service-status img").each(function(){
        var path = $(this).attr("src");
        var path = utils.getFilePath(path) + "/loading.gif";
        $(this).attr("src", path);
    });
    $.ajax({
        type: "POST",
        url: "performance/StartService",
        data: {},
        success: function (result) {
            pf.updateServiceStatus(result.status)
        },
        error: function (req, status, error) {
            utils.showError(error);
        }
    });
}

pf.stopService = function(){
    $(".perf-service-handle input").each(function(){
        $(this).attr({disabled: ""});
    });
    $(".perf-service-status img").each(function(){
        var path = $(this).attr("src");
        var path = utils.getFilePath(path) + "/loading.gif";
        $(this).attr("src", path);
    });
    $.ajax({
        type: "POST",
        url: "performance/StopServcie",
        data: {},
        success: function (result) {
            pf.updateServiceStatus(result.status)
        },
        error: function (req, status, error) {
            utils.showError(error);
        }
    });
}

pf.getServiceStatus = function () {
        $.ajax({
        type: "POST",
        url: "performance/GetServiceStatus",
        data: {},
        success: function (result) {
            pf.updateServiceStatus(result.status)
        },
        error: function (req, status, error) {
            utils.showError(error);
        }
    });
}

var daterange = daterange || null;
var tswitch = tswitch || null;
var stat = stat || null;

$(function () {

    $(".date-select").each(function () {
        $(this).datepicker({ dateFormat: 'mm-dd-yy' });
    });

    /* -- Create Date Range object -- */
    if (daterange == null) {
        /* Set default Dates range */
        var now = new Date();
        var from = new Date();
        from.setDate(from.getDate() - 120);
        now = utils.getStringFromDate(now);
        from = utils.getStringFromDate(from);

        var daterange = new utils.DateRange({
            element: document.getElementById('perf-range-container'),
            select: {
                week: 7,
                month: 30
            },
            defValueEnd: from,
            defValueStart: now,
            defValFrom: now
        });
    }

    /*-- Create Toggle Switch -- */
    if (tswitch == null){
        tswitch = new utils.ToggleSwitch({
            element: document.getElementById('perf-ts-container'),
            id: 'perf-toggle-switch',
            values: ['Bar', 'Line'],
            onToggleChange: function (event) {
                if (stat == null) return;
                pf.drawCharts(stat);
            }  
        });
    }

    var dates = daterange._getSelectedRange();
    if (dates != false) {
        pf.populateCharts(dates.startDate, dates.endDate);
    }

    $("#perf-view-button").click(function () {
        var dates = daterange._getSelectedRange();
        if (dates != false) {
            pf.populateCharts(dates.startDate, dates.endDate);
        }
    });

    pf.getServiceStatus();
    
    $("#service-start-button").click(function () {
        pf.startService();
    });

    $("#service-stop-button").click(function () {
        pf.stopService();
    });
});