using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Configuration;


public class SizeLimitConsumer : IHttpModule
{
	public String ModuleName
	{
		get { return "SizeLimitConsumer"; }
	}

	public void Init(HttpApplication application)
	{
		application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
	}

	/// <summary>
	/// Clears the HTTP request by reading all its data and throwing it away
	/// </summary>
	/// <param name="application"></param>
	/// <param name="worker"></param>
	private void ReadAll(HttpApplication application, HttpWorkerRequest worker)
	{
		// Check if body contains data
		if (!worker.HasEntityBody())
		{
			return;		// nothing to read
		}

		// Get the initial bytes loaded
		byte[] initbits = worker.GetPreloadedEntityBody();
		if (worker.IsEntireEntityBodyIsPreloaded())
		{
			return;		// nothing more to read
		}

		int initialBytes = (initbits == null ? 0 : initbits.Length);
		byte[] buffer = new byte[500 * 1024];

		// Get the total body length
		int requestLength = worker.GetTotalEntityBodyLength();
		// Set the received bytes to initial bytes before start reading
		int receivedBytes = initialBytes;
		while (requestLength - receivedBytes >= buffer.Length)
		{
			// Read another set of bytes
			initialBytes = worker.ReadEntityBody(buffer, buffer.Length);
			receivedBytes += initialBytes;
		}
		initialBytes = worker.ReadEntityBody(buffer, requestLength - receivedBytes);
	}

	private void Application_BeginRequest(Object source, EventArgs e)
	{
		HttpApplication application = (HttpApplication) source;
		HttpWorkerRequest worker = (HttpWorkerRequest) application.Context.GetType().GetProperty("WorkerRequest", (BindingFlags) 36).GetValue(application.Context, null);

		// Get the content length of the request, in bytes
		int nContentLength = Convert.ToInt32(worker.GetKnownRequestHeader(HttpWorkerRequest.HeaderContentLength));

		Configuration config = WebConfigurationManager.OpenWebConfiguration("/CompareServiceWeb");
		HttpRuntimeSection configSection = config.GetSection("system.web/httpRuntime") as HttpRuntimeSection;

		// MaxRequestLength returns KB. Convert that to bytes.
		int nMaxContentLength = configSection.MaxRequestLength * 1024;

		if (nContentLength > nMaxContentLength)
		{
			ReadAll(application, worker);
			application.Context.Response.Redirect("SizeErrorHandlerPage.aspx");
		}
	}

	public void Dispose()
	{
	}
}
