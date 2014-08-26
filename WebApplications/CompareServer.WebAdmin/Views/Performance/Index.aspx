<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="<%= Url.Content("~/Scripts/jqplot/jquery.jqplot.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/excanvas.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.timeAxisRenderer.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.dateAxisRenderer.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.dataSizeAxisRenderer.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.canvasTextRenderer.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.canvasAxisTickRenderer.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.categoryAxisRenderer.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.cursor.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.highlighter.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jqplot/plugins/jqplot.barRenderer.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/utils.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/performance.js") %>" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssPlaceHolder" runat="server">
    <link href="<%= Url.Content("~/Content/Css/utils.css") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/Css/performance.css") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/Css/jqplot/jquery.jqplot.min.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="perf-control">
        <div id="perf-range-container"> 
        </div>
        <div>
            <input type="button" value="View" id="perf-view-button" />
        </div>
        <div class="data-load-spinner" id="perf-data-load-spinner">
            <img src="<%= Url.Content("~/Content/images/loading.gif") %>" alt="" align="middle" />
            <div>Loading data ...</div>
        </div>
        <div id="perf-ts-container">
            
        </div>
        <div class="perf-service-status">
            <div>Service Status: </div>
            <img src="<%= Url.Content("~/Content/images/status_unknown.png") %>" alt="Unknown status" />
        </div>
        <div class="perf-service-handle">
            <input type="button" value="Start" id="service-start-button" />
            <input type="button" value="Stop" id="service-stop-button" />
        </div>
    </div>
    <div id="totalNumbers">
        <table class="info-table" >
            <tr>
                <td>Total Number Of Comparisons</td>
                <td id="perf-total-comp-num"></td>
            </tr>
            <tr>
                <td>Total Number Of Bytes</td>
                <td id="perf-total-bytes-num"></td>
            </tr>
        </table>
    </div>
    <div id="comparisons-chart" class="perf-chart"></div>
    <div id="avg-comp-len-chart" class="perf-chart"></div>
    <div id="avg-proc-time-chart" class="perf-chart"></div>
    <div id="avg-doc-len-chart" class="perf-chart"></div>
    <div id="chart-result-error" class="list-errors"></div>

</asp:Content>
