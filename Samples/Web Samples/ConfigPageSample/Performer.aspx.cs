using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Document.Services.Compare.Control;
using System.IO;

namespace Workshare.Samples.ConfigPageSample
{
	public partial class Performer : System.Web.UI.Page
	{
		// This page calls into Compare Server, using Control.dll, to perform tansport benchmarking.
		// Its hosted in an IFRAME in Default.aspx page, but doesnt show up any UI. It takes its values
		// from the client side controls on Default.aspx page.

		#region Class Data

		DateTime _startTime = DateTime.Now;
		int _divideFactor = 1;

		#endregion

		#region Event Handler

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				// Only activated if postedback
				// Dont do anything on startup

				if (this.IsPostBack)
				{
					TransportProtocolEnum transportProtocol = (TransportProtocolEnum)Enum.Parse(typeof(TransportProtocolEnum), this.Protocol.Text, true);
					int chunkingSize = 512;
					try
					{
						if (this.Chunksize.Text != null)
							chunkingSize = Convert.ToInt32(this.Chunksize.Text);
					}
					catch
					{
						chunkingSize = 512;
					}

					ICompareService cp = CreateAppropriateService(transportProtocol);

					string user = (string)Session["UserName"];
					string pass = CodePassword((string)Session["Passw"]);
					string domain = (string)Session["Domain"];

					cp.SetClientCredentials(user, pass, domain);

					// We need to hook into this event to calculate the progress and data rate.
					cp.DataSent += new EventHandler<DataSentArgs>(CompareService_OnDataSent);

					//Call into benchmarking with specified chunk size and 40 MB of dummy data
					cp.BenchmarkTransfer(chunkingSize * 1024, 40 * 1024 * 1024);

				}
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		void CompareService_OnDataSent(object sender, DataSentArgs e)
		{
			try
			{
				CalculateDataRate(e);
				CalculatePercentComplete(e);

				if (e.BytesSent == e.BytesTotal)
				{
					MarkComplete();
				}
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		#endregion

		#region Helper Functions

		/// <summary>
		/// Create appropriate service based on protocol specified by the user. Its safe to assume that this
		/// page will always be running on the same machine as the service.
		/// </summary>
		/// <param name="protocol"></param>
		/// <returns></returns>
		private ICompareService CreateAppropriateService(TransportProtocolEnum protocol)
		{
			switch (protocol)
			{
				case TransportProtocolEnum.Tcp:
					_divideFactor = 1;
					return CompareService.CreateTcpService("localhost", Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["tcp_port"]));

				case TransportProtocolEnum.NamedPipe:
					_divideFactor = 10;
					return CompareService.CreateNamedPipeService();

				case TransportProtocolEnum.Http:
				default:
					_divideFactor = 1;
					return CompareService.CreateHttpService("localhost", Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["http_port"]));
			}
		}

		private void CalculateDataRate(DataSentArgs e)
		{
			long bytespersec = 0;
			long mbytesperminute = 0;

			if (e.BytesSent == 0)
			{
				_startTime = DateTime.Now;
				bytespersec = 0;
				mbytesperminute = 0;
			}
			else
			{
				DateTime currentTime = DateTime.Now;
				TimeSpan diffrence = currentTime - _startTime;

				double second = diffrence.TotalSeconds;

				if (second != 0)
				{
					double kbbytes = e.BytesSent / 1024;

					bytespersec = (long)(kbbytes / second);
					mbytesperminute = (bytespersec * 60) / 1024;
				}
				else
				{
					bytespersec = (long)Session["bytesPerSec"];
					mbytesperminute = (long)Session["MbytesPerMin"];
				}
			}

			//To accomodate hight data rate in case of namedPipe protocol
			bytespersec = bytespersec / _divideFactor;
			mbytesperminute = mbytesperminute / _divideFactor;

			Session["bytesPerSec"] = bytespersec;
			Session["MbytesperMin"] = mbytesperminute;

		}
		private void CalculatePercentComplete(DataSentArgs e)
		{
			long soFar = e.BytesSent;
			long total = e.BytesTotal;

			int percentComplete = (int)Math.Ceiling((double)soFar / (double)total * 100);

			Session["percentComplete"] = percentComplete;

		}

		private string CodePassword(string pass)
		{
			if (pass == null || pass == string.Empty)
				throw new Exception("Password cannot be empty.");

			byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);

			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] ^= 0x56;
			}

			return System.Text.Encoding.Unicode.GetString(bytes);
		}

		#endregion

		#region Clientside script helpers

		private void ShowMessage(string message)
		{
			string script = "window.parent.updateStatus('{0}');";
			message = message.Replace(Environment.NewLine, " ");

			script = string.Format(script, message);
			ExecuteScript("errmsgFromPerformer", script);
		}
		private void MarkComplete()
		{
			string script = "window.parent.onComplete();";
			ExecuteScript("markcomplete", script);
		}

		private void ExecuteScript(string id, string script)
		{
			ScriptManager.RegisterStartupScript(this, typeof(Performer), id, script, true);
		}

		#endregion
	}
}
