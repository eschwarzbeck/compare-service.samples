<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorHandlerPage.aspx.cs"
    Inherits="CompareServiceWeb.ErrorHandlerPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error occurred</title>
    <link rel="stylesheet" type="text/css" href="stylesheet.css" />
</head>
<body class="body">
    <form id="form1" runat="server">
	<table border="0" width="100%" bordercolor="#666666" cellpadding="0" cellspacing="0"
		height="90%">
		<tr>
			<td valign="top">
				<div id="LoginDiv">
					<table border="0" width="100%" cellpadding="0" cellspacing="0">
						<!-- Header Image -->
						<tr>
							<td style="background-color: #293C57; height: 1%;">
							</td>
							<td>
								<img src="img/admin-banner.gif" alt="Workshare" /><br />
							</td>
							<td background="img/admin-banner-grey.gif" width="60%">
								&nbsp;
							</td>
						</tr>
						<tr>
							<td colspan="3" style="width: 100%; " background="img/nav-spacer.gif">
							    <img src="img/nav-spacer.gif"> 
							</td>
						</tr>
						<tr>
							<td colspan="3">
                                <br />
                                <div>
                                    <center>
                                        Oops. It seems there has been some error. Click <a href='default.aspx' target='_top'>
                                            here</a> to get back.</center>
                                </div>
							</td>
						</tr>
						<tr>
							<td colspan="3">
								&nbsp;
							</td>
						</tr>
					</table>
				</div>
			</td>
		</tr>
	</table>
	<!-- Copyright Notice -->
	<table border="0" cellpadding="4" cellspacing="0" width="100%">
		<tr>
			<td>
				&nbsp;
			</td>
		</tr>
		<tr>
			<td class="copyrightNotice" style="width: 35px; height: 20px;">
				&nbsp;
			</td>
			<td class="copyrightNotice" style="width: 250px; height: 20px;">
				&#169;2009 Workshare, Inc. All rights reserved.
			</td>
			<td class="copyrightNotice" style="width: 10px; height: 20px;">
				|
			</td>
			<td class="copyrightNotice" style="height: 20px;">
				<a href="http://www.workshare.com" target="_blank">www.workshare.com</a>
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
