<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home Page
</asp:Content>

<asp:Content ID="CssContent1" ContentPlaceHolderID="CssContent" runat="server">
    <link href="<%= Url.Content("~/Content/css/utils.css") %>" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="<%= Url.Content("~/Content/css/home.css") %>" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/utils.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/home.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table class="info-table">
            <tr>
                <td>Service Version</td>
                <td id="service-version"></td>
            </tr>
            <tr>
                <td>Comparison Version</td>
                <td id="comparison-version"></td>
            </tr>
        </table>
    </div>
    <div class="list-errors">
    </div>

</asp:Content>
