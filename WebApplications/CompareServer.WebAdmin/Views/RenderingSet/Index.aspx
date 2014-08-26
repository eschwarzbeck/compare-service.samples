<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="CssContent1" ContentPlaceHolderID="CssPlaceHolder" runat="server">
    <link href="<%= Url.Content("~/Content/Css/utils.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/Css/renderingset.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="<%= Url.Content("~/Scripts/utils.js")  %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.color.js")  %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/fileuploader.js")  %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/renderingset.js")  %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="rs-content">
        <noscript>          
           <p>Please enable JavaScript to view rendering sets.</p>
        </noscript>

        <ul id="rs-list">
    
        </ul>
    </div>
    
    <div id="rs-uploader">
        <div id="rs-upload-button-container">
            <a id="rs-upload-button" class="button"><span>Add</span></a>
        </div>
        <ul id="rs-upload-list">
            
        </ul>
    </div>

    <div class="list-errors">
    </div>

</asp:Content>
