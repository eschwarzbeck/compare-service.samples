<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Workshare Compare Service Sample</title>
	<style type="text/css">
		#form1
		{
			height: 420px;
		}
	</style>
</head>
<body>
	<center>
		<form id="form1" runat="server">
		<table width="80%" border="1" bordercolor="gray" cellpadding="0" cellspacing="0">
			<!-- Main Table-->
			<tr>
				<td align="left">
					<span id="MessageSpan" runat="server" visible="false" />
				</td>
			</tr>
			<tr>
				<td align="center" valign="middle">
					<br />
					<div id="AuthenticateTable" runat="server">
						<table border="1" bordercolor="gray" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table border="0" bordercolor="black" cellpadding="0" cellspacing="0">
										<!--Authenticate Table-->
										<tr>
											<td>
												&nbsp;
											</td>
											<td>
												&nbsp;
											</td>
											<td>
												&nbsp;
											</td>
										</tr>
										<tr>
											<td align="right">
												Username:
											</td>
											<td align="left">
												&nbsp;&nbsp;
												<asp:TextBox ID="UserNameTextBox" runat="server" Width="150px"></asp:TextBox>
											</td>
											<td>
												&nbsp;
											</td>
										</tr>
										<tr>
											<td align="right">
												Password:
											</td>
											<td align="left">
												&nbsp;&nbsp;
												<asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
											</td>
											<td>
												&nbsp;
											</td>
										</tr>
										<tr>
											<td align="right">
												Realm:
											</td>
											<td align="left">
												&nbsp;&nbsp;
												<asp:TextBox ID="RealmTextBox" runat="server" Width="150px"></asp:TextBox>
											</td>
											<td>
												&nbsp;
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td>
												&nbsp;
											</td>
											<td>
												<asp:Button ID="AuthenticateButton" runat="server" Text="Authenticate" OnClick="AuthenticateButton_Click" />
											</td>
										</tr>
										<tr>
											<td colspan="3">
												&nbsp;
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br />
					</div>
					<div id="FilesUploadTable" runat="server">
						<table border="1" bordercolor="gray" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table border="0" bordercolor="green" cellpadding="0" cellspacing="0">
										<!--Files Upload Table-->
										<tr>
											<td colspan="2" align="right">
												<asp:Label ID="WCSVersionLabel" runat="server" Text="Version"></asp:Label>
											</td>
										</tr>
										<tr>
											<td colspan="2">
												&nbsp;
											</td>
										</tr>
										<tr>
											<td align="right">
												Original:
											</td>
											<td align="left">
												<asp:FileUpload ID="OriginalFile" runat="server" size="60" />
											</td>
										</tr>
										<tr>
											<td align="right">
												Modified:
											</td>
											<td align="left">
												<asp:FileUpload ID="ModifiedFile" runat="server" size="60" />
											</td>
										</tr>
										<tr>
											<td align="right">
												Rendering Set:
											</td>
											<td align="left">
												<asp:DropDownList ID="RenderingSetDropDownList" runat="server">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td align="right">
												<asp:Button ID="CompareButton" runat="server" Text="Do Compare" OnClick="CompareButton_Click" /><br />
											</td>
										</tr>
										<tr>
											<td colspan="2">
												&nbsp;
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br />
					</div>
					<div id="ResultPane1" runat="server">
						<table border="1" bordercolor="gray" cellpadding="0" cellspacing="0" width="80%">
							<tr>
								<td>
									<table border="0" width="90%">
										<!-- Result pane 1 -->
										<tr>
											<td align="left">
												Original File:
											</td>
											<td align="left">
												<asp:Label ID="OriginalFilePathLabel" runat="server" Text="Original File"></asp:Label>
											</td>
										</tr>
										<tr>
											<td align="left">
												Rendering Set:
											</td>
											<td align="left">
												<asp:Label ID="RenederingSetLabel" runat="server" Text="RenderingSet"></asp:Label>
											</td>
										</tr>
										<tr>
											<td colspan="2" align="right">
												<asp:Button ID="StartOverButton" runat="server" Text="Start Over" OnClick="StartOverButton_Click" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</div>
					<br />
					<div id="ResultFilesTable" runat="server">
						<!-- Result Files Table-->
						<asp:Table runat="server" ID="ResultFiles" border="1" BorderColor="gray" Width="90%">
						</asp:Table>
					</div>
					<br />
				</td>
			</tr>
		</table>
		</form>
	</center>
</body>
</html>
