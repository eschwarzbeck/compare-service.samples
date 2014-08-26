<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CompareServer.WebClient.Models.ComparerResultModel>" %>

<asp:Content ID="CssContent1" ContentPlaceHolderID="CssContent" runat="server">
    <link href="<%= Url.Content("~/Content/css/utils.css") %>" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="<%= Url.Content("~/Content/css/comparer.css") %>" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/utils.js") %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	WorkShare: Compare Server Result
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Results</h3>
    <% if (Model.Errors.Count > 0)
       { %>
        <div class="result-errors">
            <ul>
            <% foreach (string error in Model.Errors)
               { %>
                <li><%= error %></li>
            <% } %>
            </ul>
        </div>
    <%  }
        if (Model.Comparisons.Count > 0)
        { %>
        <div class="result-files">
            <ul>
            <%  foreach (CompareServer.Domain.ComparisonResult cr in Model.Comparisons)
                { %>
                <li>
                    <table>
                        <tr>
                            <th>File</th>
                            <td><%= cr.File %></td>
                        </tr>
                    <%  if (!string.IsNullOrEmpty(cr.Redline.ServerName))
                        {
                            string url = "/Comparer/Download/" + cr.Redline.ServerName + "/" + cr.Redline.ClientName; %>
                        <tr>
                            <th>Redline</th>
                            <td><a href="<%= url %>" target="_blank"><%= cr.Redline.ClientName %></a></td>
                        </tr>
                    <%  }
                        if (!string.IsNullOrEmpty(cr.RedlineMl.ServerName))
                        {
                            string url = "/Comparer/Download/" + cr.RedlineMl.ServerName + "/" + cr.RedlineMl.ClientName; %> 
                        <tr>
                            <th>RedlineMl</th>
                            <td><a href="<%= url %>" target="_blank"><%= cr.RedlineMl.ClientName %></a></td>
                        </tr> 
                    <%  }
                        if (!string.IsNullOrEmpty(cr.Summary.ServerName))
                        {
                            string url = "/Comparer/Download/" + cr.Summary.ServerName + "/" + cr.Summary.ClientName; %>
                        <tr>
                            <th>Summary</th>
                            <td><a href="<%= url %>" target="_blank"><%= cr.Summary.ClientName %></a></td>
                        </tr>
                    <%  }
                        if (!string.IsNullOrEmpty(cr.Error))
                        { %>
                        <tr class="error-row">
                            <th>Error</th>
                            <td><%= cr.Error %></td>
                        </tr>
                    <%  } %>
                    </table>
                </li>  
            <%  } %>
            </ul>
        </div>

    <%  } %>
</asp:Content>
