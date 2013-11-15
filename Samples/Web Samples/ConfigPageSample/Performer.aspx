<%@ Page Language="C#" CodeBehind="Performer.aspx.cs" Inherits="Workshare.Samples.ConfigPageSample.Performer"
	EnableSessionState="ReadOnly" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body bgcolor="#666666">
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scriptManager" runat="server" />

	<script type="text/javascript">
		function pageLoad() {
			window.parent.register(
				$get('<%= this.form1.ClientID %>'),
				$get('<%= this.Protocol.ClientID %>'),
				$get('<%= this.Chunksize.ClientID %>')
			);
		}
	</script>

	<div>
		<asp:TextBox ID="Protocol" runat="server" />
		<asp:TextBox ID="Chunksize" runat="server" />
	</div>
	</form>
</body>
</html>
