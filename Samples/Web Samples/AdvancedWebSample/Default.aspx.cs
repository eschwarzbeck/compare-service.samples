using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Configuration;

namespace Workshare.Samples.AdvWebSample
{	
	public partial class _Default : Page
	{
		public _Default()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected void Page_Load(object sender, EventArgs args)
		{
			if (!this.IsPostBack)
			{
				// Reserve a spot in Session for the UploadInfo object
				this.Session["UploadInfo"] = new UploadInfo();
			}
		}

		/// <summary>
		/// Actual upload is performed by Upload.aspx. This PageMethod collects the update from
		/// Session and conveys it back to client side scripts. If progress is provided as percent complete,
		/// it is used as is, and if it is not percent complete, we calculate it ourselves.
		/// </summary>
		[System.Web.Services.WebMethod]
		[System.Web.Script.Services.ScriptMethod]
		public static object GetUploadStatus()
		{
			UploadInfo info = HttpContext.Current.Session["UploadInfo"] as UploadInfo;

			if (info == null || !info.IsReady)
			{
				//  not ready yet ...
				return null;
			}
			
			int percentComplete;
			string message;
			string pairInfo = info.PairInfo;

			if (info.UsePercentComplete == true)
			{
				percentComplete = info.PercentComplete;
				message = info.Message;
			}
			else
			{
				// Get the length of the file and divide that by the total length 

				int soFar = info.UploadedLength;
				int total = info.ContentLength;

				if (total == 0)
				{
					percentComplete = 0;
				}
				else
				{
					percentComplete = (int)Math.Ceiling((double)soFar / (double)total * 100);
				}

				message = string.Format("Uploading {0} ... {1} of {2} Bytes", info.FileName, soFar, total);
			}

			//  return the percentage
			return new { percentComplete = percentComplete, message = message, pairInfo = pairInfo };
		}
	}



	public class UploadInfo
	{
		public UploadInfo()
		{
			IsReady = false;
			PairInfo = string.Empty;
		}

		/// <summary>
		/// Tells whether this object has valid progress update
		/// </summary>
		public bool IsReady { get; set; }

		/// <summary>
		/// If UsePrecentComplete is set to True, we use PercentComplete property to get the progress
		/// If it is set to False, we use ContentLenght and UploadedLength to calculate the percent progress
		/// </summary>
		public bool UsePercentComplete { get; set; }

		/// <summary>
		/// If UsePercentComeplete is set to True, use this property to get the progress
		/// </summary>
		public int PercentComplete { get; set; }

		/// <summary>
		/// If UserPercentComplete is set to False, use ContentLenght in conjunction with UploadedLength
		/// to calculate progress.
		/// </summary>
		public int ContentLength { get; set; }

		/// <summary>
		/// If UserPercentComplete is set to False, use UploadedLength in conjunction with ContentLenght
		/// to calculate progress.
		/// </summary>
		public int UploadedLength { get; set; }

		/// <summary>
		/// Filename to show the progress against.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Any status messages to show as the progress continues
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// This string is used to show which pair of files is being processed by Compare Server, in case
		/// of multiple modified files.
		/// </summary>
		public string PairInfo { get; set; }
	}
}