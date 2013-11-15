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
using System.Web.Configuration;

namespace CompareServiceWeb
{
	public partial class SizeErrorHandlerPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			const string js = "window.parent.resetUIForErrorPage();";
			ScriptManager.RegisterStartupScript(this, typeof(SizeErrorHandlerPage), "SizeErrorScript", js, true);

			string sErrorMessage = "It seems your file size limit has been reached. The limit for the combined size of all files is {0} MB. Click <a href='default.aspx' target='_top'>here</a> to get back and load a smaller file.";

			Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
			HttpRuntimeSection configSection = config.GetSection("system.web/httpRuntime") as HttpRuntimeSection;

			// MaxRequestLength returns KB. Convert that to MB.
			int nMaxContentLength = configSection.MaxRequestLength / 1024;

			sErrorMessage = string.Format(sErrorMessage, nMaxContentLength);

			ErrorMessageSpan.InnerHtml = sErrorMessage;
		}
	}
}
