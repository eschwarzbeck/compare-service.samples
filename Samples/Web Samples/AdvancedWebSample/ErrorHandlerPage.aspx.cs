using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CompareServiceWeb
{
	public partial class ErrorHandlerPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			const string js = "window.parent.resetUIForErrorPage();";
			ScriptManager.RegisterStartupScript(this, typeof(ErrorHandlerPage), "resetProgressBarScript", js, true);
		}
	}
}
