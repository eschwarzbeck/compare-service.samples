<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SizeErrorHandlerPage.aspx.cs"
    Inherits="CompareServiceWeb.SizeErrorHandlerPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Size limit exceeded</title>
    <link rel="Stylesheet" href="_assets/css/default.css" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <center>
            <span id="ErrorMessageSpan" runat="server">It seems your file size limit has been reached.
                Click <a href='' target="_top">here</a> to get back to Comparison page.</span></center>
    </div>
    </form>
</body>
</html>
