using System;
using System.Web;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;

namespace Workshare.Samples.AdvWebSample
{
	public class SizeLimitConsumer : IHttpModule
	{
		// This HTTP Module allows us to hook into the HTTP Request before it reachers IIS.
		// We need to do this because if we try to upload a file larger than the maxRequestLenght attribute 
		// in web.config, it throws an uncatchable exception. To avoid that, we intercept the request, and if
		// it is greater than allowed size, consume all of the request and redirect the user to an error page.
		// If request lenght is under limit, we dont do anything.

		public String ModuleName
		{
			get { return "SizeLimitConsumer"; }
		}

		public void Init(HttpApplication application)
		{
			application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
		}

		private void Application_BeginRequest(Object source, EventArgs e)
		{
			IServiceProvider provider = HttpContext.Current as IServiceProvider;
			HttpWorkerRequest worker = provider.GetService(typeof(HttpWorkerRequest)) as HttpWorkerRequest;

			// Get the content length of the request, in bytes
			long nContentLength = Convert.ToInt64(worker.GetKnownRequestHeader(HttpWorkerRequest.HeaderContentLength));

			// Read MaxRequestLength from web.config of current website
			Configuration config = WebConfigurationManager.OpenWebConfiguration("/AdvancedWebSample");
			HttpRuntimeSection configSection = config.GetSection("system.web/httpRuntime") as HttpRuntimeSection;

			// MaxRequestLength returns KB. Convert that to bytes.
			long nMaxContentLength = (long)configSection.MaxRequestLength * 1024;

			// In case of very large content length (e.g. 2 GB or more), HttpWorkerRequest gets mad and
			// returns negative value. So check for < 0 too.
			if (nContentLength < 0 || nContentLength > nMaxContentLength)
			{
				// Consuming the complete HTTP Request, before it reaches IIS, to avoid the Size Exception
				ReadAll(worker);

				//Redirect it to the error page
				(source as HttpApplication).Context.Response.Redirect("SizeErrorHandlerPage.aspx");
			}
		}

		private void ReadAll(HttpWorkerRequest worker)
		{
			// Check if body contains data
			if (!worker.HasEntityBody())
			{
				return;
			}

			// Get the initial bytes loaded
			byte[] initbits = worker.GetPreloadedEntityBody();
			if (worker.IsEntireEntityBodyIsPreloaded())
			{
				// Nothing more to read
				return;
			}

			// Get the total body length
			int requestLength = worker.GetTotalEntityBodyLength();
			int initialBytes = (initbits != null) ? initbits.Length : 4096;
			int receivedBytes = initialBytes;

			byte[] buffer = new byte[512 * 1024];

			// We get a negative length in case of very large files.

			if (requestLength < 0)
			{
				// This code is slower than the else part, although it does the same thing.
				// We need to do it differently to handle negative content length		

				try
				{
					do
					{
						// Read another set of bytes
						initialBytes = worker.ReadEntityBody(buffer, buffer.Length);

						// Update the received bytes
						receivedBytes += initialBytes;
					} while (initialBytes != 0);
				}
				catch
				{
				}
			}
			else
			{
				while (requestLength - receivedBytes >= initialBytes)
				{
					// Read another chunk of bytes
					initialBytes = worker.ReadEntityBody(buffer, buffer.Length);
					receivedBytes += initialBytes;
				}

				// Read remaining part if any
				initialBytes = worker.ReadEntityBody(buffer, (int)(requestLength - receivedBytes));
			}
		}

		public void Dispose()
		{
		}
	}
}