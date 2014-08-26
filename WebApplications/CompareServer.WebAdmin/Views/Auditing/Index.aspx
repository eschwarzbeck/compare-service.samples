<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="<%= Url.Content("~/Scripts/pajinate/jquery.pajinate.js")    %>" type="text/javascript" ></script>
    <script src="<%= Url.Content("~/Scripts/utils.js")    %>" type="text/javascript" ></script>
    <script src="<%= Url.Content("~/Scripts/auditing.js")    %>" type="text/javascript" ></script>
</asp:Content>


<asp:Content ID="CssConetnt1" ContentPlaceHolderID="CssPlaceHolder" runat="server">
    <link href="<%= Url.Content("~/Content/Css/pajinate/styles.css") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/Css/utils.css") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/Css/list.css") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/Css/auditing.css") %>" rel="Stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="list-control" id="audit-control">
        <div>
            <input type="button" value="Refresh" id="audit-list-button" onclick="javascript:audit.viewUserActivity()"/>
        </div>
        <div class="data-load-spinner" id="audit-data-load-spinner">
            <img src="<%= Url.Content("~/Content/images/loading.gif") %>" alt="" align="middle" />
            <div>loading data ...</div>
        </div>
    </div>

    <div class="list-container" id="audit-list-container">
        <div class="list-navigation"></div>
        <div class="list-header" id="audit-list-header">
            <div class="auditing-list-item-name">User Name</div>
            <div class="auditing-list-item-num">Num. of times used</div>
            <div class="auditing-list-item-bytes">Total bytes compared</div>
            <div class="auditing-list-item-time">Total processing time</div>
        </div>
        <ul class="list-items">
        
        </ul>
        <div class="list-navigation"> </div>
    </div>

    <div class="list-errors">
    </div>

</asp:Content>
