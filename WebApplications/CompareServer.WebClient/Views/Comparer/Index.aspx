<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CompareServer.WebClient.Models.ComparerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>

<asp:Content ID="CssContent1" ContentPlaceHolderID="CssContent" runat="server">
    <link href="<%= Url.Content("~/Content/css/utils.css") %>" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="<%= Url.Content("~/Content/css/comparer.css") %>" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/utils.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/comparer.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" >

    </script>
    
    <% using (Html.BeginForm("results", "comparer", FormMethod.Post, new object { }))
       { %>
       <fieldset>
            <div id="step1" class="wizard-step">
                <h3>Select original document</h3>
                <div id="original-doc-uploader">
                    <%= Html.TextBoxFor(m => m.OriginalDoc, new { @class = "qq-upload-input required", @readonly = "readonly" }) %>
                    <br />
                    <%= Html.ValidationMessageFor(m => m.OriginalDoc) %>
                    <%= Html.HiddenFor(m => m.OriginalDocFile) %> 
                    <a id="original-doc-button" class="qq-upload-button button" ><span>Select File</span></a>
                </div>
            </div>
            <div id="step2" class="wizard-step">
                <h3>Select modified documents</h3>
                <div id="modified-doc-uploader">
                    <ul class="qq-upload-list">
                        <li><%= Html.TextBoxFor(m => m.ModifiedDoc1, new { @class = "qq-upload-input required", @readonly = "readonly" })%>
                            <%= Html.HiddenFor(m => m.ModifiedDocFile1) %> </li>
                        <li><%= Html.TextBoxFor(m => m.ModifiedDoc2, new { @class = "qq-upload-input", @readonly = "readonly" })%>
                            <%= Html.HiddenFor(m => m.ModifiedDocFile2) %> </li>
                        <li><%= Html.TextBoxFor(m => m.ModifiedDoc3, new { @class = "qq-upload-input", @readonly = "readonly" })%>
                            <%= Html.HiddenFor(m => m.ModifiedDocFile3) %> </li>
                        <li><%= Html.TextBoxFor(m => m.ModifiedDoc4, new { @class = "qq-upload-input", @readonly = "readonly" })%>
                            <%= Html.HiddenFor(m => m.ModifiedDocFile4) %> </li>
                        <li><%= Html.TextBoxFor(m => m.ModifiedDoc5, new { @class = "qq-upload-input", @readonly = "readonly" })%>
                            <%= Html.HiddenFor(m => m.ModifiedDocFile5) %> </li>
                    </ul>
                    <%=Html.ValidationMessageFor(m => m.ModifiedDoc1) %>
                     <a id="modified-doc-button" class="qq-upload-button button" ><span>Add File</span></a>
                </div>
            </div>
            <div id="step3" class="wizard-step">
                <h3>Select a rendering set</h3>
                <%= Html.DropDownListFor(m => m.RenderingSet, Model.GetRenderingSets(Request)) %>
               
            </div>
            <div id="step4" class="wizard-step final">
                <h3>Select output type</h3>
                <%= Html.DropDownListFor(m => m.OutputFormat, Model.GetResponseOptions()) %>
                
            </div>
            <div id="step5" class="wizard-step">
                <h3>Comparing ...</h3>
                <ul style="list-style-type:none;">
                    <li><%= Html.ValidationMessageFor(m => m.OriginalDocFile) %></li>
                    <li><%= Html.ValidationMessageFor(m => m.ModifiedDocFile1) %></li>
                    <li><%= Html.ValidationMessageFor(m => m.RenderingSet) %></li>
                    <li><%= Html.ValidationMessageFor(m => m.OutputFormat) %></li>
                </ul>
                <div id="result-progressbar" class="result-progressbar" >
                </div>
            </div>
            <br /><hr style="width:900px; float:left;display:block;margin: 5px;" />
            <p>
                <a class="button" id="back-step" name="back-step" ><span>< Back</span></a>
                <a class="button" id="next-step" name="next-step" ><span>Next ></span></a>
            </p>
        </fieldset>
        <div id="problems">
        </div>
    <%    } %>
</asp:Content>
