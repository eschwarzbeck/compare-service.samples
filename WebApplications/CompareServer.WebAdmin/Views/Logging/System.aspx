<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Logging/Logging.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="LoggingScriptPlaceHolder" runat="server" >
    <script src="<%= Url.Content("~/Scripts/logging_system.js") %>" type="text/javascript" ></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="LoggingMain" runat="server">

    <div class="list-control" id="log-control">
        <div>
            <input type="text" value="<%= DateTime.Now.ToString("MM-dd-yyyy") %>" id="log-list-arg-date" class="date-select" />
        </div>
        <div>
            <input type="button" value="View" id="log-list-button"/>
        </div>
        <div class="data-load-spinner" id="log-data-load-spinner">
            <img src="<%= Url.Content("~/Content/images/loading.gif") %>" alt="" align="middle" />
            <div>loading data ...</div>
        </div>
    </div>

    <div class="list-container log-list-container" id="log-view">
        <div class="list-navigation"> </div>
        <ul class="list-items">
        
        </ul>
        <div class="list-navigation"> </div>
    </div>

    <div class="list-errors">

    </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LoggingTitle" runat="server">

</asp:Content>
