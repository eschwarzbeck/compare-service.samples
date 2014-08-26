<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="<%= Url.Content("~/Scripts/utils.js")    %>" type="text/javascript" ></script>
    <script src="<%= Url.Content("~/Scripts/home.js")    %>" type="text/javascript" ></script>
</asp:Content>

<asp:Content ID="CssContent" ContentPlaceHolderID="CssPlaceHolder" runat="server">
    <link href="<%= Url.Content("~/Content/Css/home.css") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= Url.Content("~/Content/Css/utils.css") %>" rel="Stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="spinner">
    </div>
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
            <tr>
                <td>Last Access</td>
                <td id="last-access"></td>
            </tr>
            <tr>
                <td>Rendering Sets Available</td>
                <td id="rsets-available"></td>
            </tr>
            <tr>
                <td><a href="#" id="ping-button">PING</a></td>
                <td id="ping-result"></td>
            </tr>
        </table>
    </div>

    <div class="list-errors">
    </div>

</asp:Content>
