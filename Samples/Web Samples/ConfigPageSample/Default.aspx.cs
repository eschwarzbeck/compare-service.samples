using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.DataVisualization.Charting.Utilities;
using System.Web.UI.WebControls;
using Document.Services.Compare.Control;
using Microsoft.Win32;
using System.Globalization;

namespace Workshare.Samples.ConfigPageSample
{
	public partial class _Default : System.Web.UI.Page
	{
		#region Class Data

		private Dictionary<long, CompareServiceAuditLogEntry> _csAuditLogEntries = new Dictionary<long, CompareServiceAuditLogEntry>();
		private List<CompareServiceHostLogEntry> _csHostLogEntries = new List<CompareServiceHostLogEntry>();
		private DataFrequency _dataFrequency;
		private DataViewType _dataViewType;
        private string LogTimeFormat = "yyyyMMdd HH:mm:ss,fff";

		#endregion

		#region Event Handlers

		/// <summary>
		/// From code point of view, the login screen and the config screen, both are a single page.
		/// They both are placed in separate &lt;div&gt; tags, and are toggled depending upon whether the
		/// user is authenticated or not. If it's not a postback, we can safely assume the user
		/// has not yet logged in, because this is the first time the page is loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				VersionLabel.Text = GetVersionString();

				if (!this.IsPostBack)
				{
					// Show login screen
					ToggleLoginScreen(true);
					return;
				}

				// These Session variables will contain values if we are here as a result of an
				// UpdatePanel postback. We populate these variables immediately after a successful
				// login.
				object logEntries = Session["LogEntries"];
				object dataFrequency = Session["DataFrequency"];
				if (logEntries != null && dataFrequency != null)
				{
					_csAuditLogEntries = logEntries as Dictionary<long, CompareServiceAuditLogEntry>;
					_dataFrequency = (DataFrequency) dataFrequency;
				}
				UpdatePageUI();

				// To transmit file via Response, we need to do this.
				scriptManager.RegisterPostBackControl(this.DownloadSystemLogButton);
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		/// <summary>
		/// Fired when you change the data frequency between Monthly and Daily
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DateFilterList_SelectionChanged(object sender, EventArgs e)
		{
			string selectedValue = this.DateFilterList.SelectedValue;
			this._dataViewType = GetDataViewType();

			// We have different textboxes for both the frequencies. For daily frequency, the textbox
			// is bound to the calendar control, but for monthly frequency it isn't

			if (selectedValue == "Daily")
			{
				this._dataFrequency = DataFrequency.Daily;
				this.EndTimeCalendar.Text = DateTime.Now.ToShortDateString();
				this.EndTimeCalendar.Visible = true;
				this.EndTimeCalendarYear.Visible = false;
				Session["DataFrequency"] = DataFrequency.Daily;
			}
			else if (selectedValue == "Monthly")
			{
				this._dataFrequency = DataFrequency.Monthly;
				this.EndTimeCalendarYear.Text = DateTime.Now.Year.ToString();
				this.EndTimeCalendar.Visible = false;
				this.EndTimeCalendarYear.Visible = true;
				Session["DataFrequency"] = DataFrequency.Monthly;
			}

			// Update only the view which is visible. The other one will get updated on activation.
			if (this._dataViewType == DataViewType.GraphView)
			{
				UpdateChartSettingsAndData();
			}
			else if (this._dataViewType == DataViewType.GridView)
			{
				ResetCurrentPageIndex();	// We implement paging/on-demand loading only for GridView
				UpdateGridSettingsAndData();
			}
		}

		/// <summary>
		/// Fired when you select a user on GridView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void GridUsersList_SelectionChanged(object sender, EventArgs e)
		{
			//Update View
			ResetCurrentPageIndex();
			UpdateGridSettingsAndData();
		}

		/// <summary>
		/// Fired when you select a user on GraphView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void GraphUsersList_SelectionChanged(object sender, EventArgs e)
		{
			UpdateChartSettingsAndData();
		}

		/// <summary>
		/// Fired when select "Grid Display Format" or "Graph Display Format"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void FormatList_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				// Graph view and Grid view are mutually exclusive. You can only have only one of these
				// active at a time. These are placed in separate <div> tags, and are toggled depending
				// upon user's selection.

				string value = this.FormatList.SelectedValue;

				if (value == "Grid Format")
				{
					this._dataViewType = DataViewType.GridView;
					ShowGraph(false);
					ResetCurrentPageIndex();
					UpdateGridSettingsAndData();
				}
				else if (value == "Graphical Format")
				{
					this._dataViewType = DataViewType.GraphView;
					ShowGraph(true);
					UpdateChartSettingsAndData();
				}

				Session["DataViewType"] = this._dataViewType;
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		/// <summary>
		/// Fired when the user presses Enter in the date text box.
		/// The calendar control is a client-side control, so it doesn't send a date-change event to the server.
		/// We respond to the OnChange event of the textbox it's bound to, to update the server data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Calendar_DateChanged(object sender, EventArgs e)
		{
			RefreshViewOnDateChange();
		}

		/// <summary>
		/// Fired when the user clicks the Refresh Date button.
		/// The calendar control is a client-side control, so it doesn't send a date-change event to the server.
		/// We respond to the Click event of the button, to update the server data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RefreshDateButton_Click(object sender, EventArgs e)
		{
			RefreshViewOnDateChange();
		}

		/// <summary>
		/// Reads the data from file, discarding previous data.
		/// Re-populates everything (in grid view).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RefreshTabularDataButton_Click(object sender, EventArgs e)
		{
			try
			{
				// Update results from log file
				ReadAuditLogDataFromFile();	//Fills up _cslogentries
				PopulateUsersList();

				ResetCurrentPageIndex();
				PopulateComparisonDataTable(_csAuditLogEntries);
				PopulatePageUIFields();
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		/// <summary>
		/// Reads the data from file, discarding previous data.
		/// Re-populates everything (in graph view).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RefreshGraphicalDataButton_Click(object sender, EventArgs e)
		{
			try
			{
				ReadAuditLogDataFromFile();	//Fills up _cslogentries
				PopulateUsersList();

				UpdateChartSettingsAndData();
				PopulatePageUIFields();
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		/// <summary>
		/// Performs authentication on Compare Service.
		/// We need the user's login credentials to perform benchmarking and to impersonate the user
		/// while controlling the Service itself (Start/Stop/Restart)
		/// The configuration screen is initialized after the user is authenticated.
		/// This sample uses Document.Service.Compare.Control.dll to connect to Compare Service.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void LoginButton_Click(object sender, EventArgs e)
		{
			try
			{
                if ((InstallWSHost && GetServiceStatus() == ServiceControllerStatus.Running) || 
                   (InstallIISHost && IsIISHostRunning()))
                {
					string host = "localhost";
                    string serviceurl = System.Configuration.ConfigurationManager.AppSettings["url.address"];
                    string binding = System.Configuration.ConfigurationManager.AppSettings["transport.binding"];
                    string securityMode = System.Configuration.ConfigurationManager.AppSettings["transport.security"];
                    string transportSecurity = System.Configuration.ConfigurationManager.AppSettings["transport.clientsecurity"];
                    string messageSecurity = System.Configuration.ConfigurationManager.AppSettings["message.clientsecurity"];
                    int port = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["http_port"]);

                    ICompareService compareService = CompareService.CreateHttpService(host, port, serviceurl, binding, securityMode, transportSecurity, messageSecurity);
                    compareService.UseChunking = false;
					compareService.SetClientCredentials(UsernameTextBox.Text, PasswordTextBox.Text, DomainTextBox.Text);

					string serviceVersion;
					string compositorVersion;
					if (compareService.VerifyConnection(out serviceVersion, out compositorVersion))
					{
						this.Session["BeenAuthenticatedByService"] = true;
						Initialize(compositorVersion, serviceVersion);
					}
					else
					{
						this.Session["BeenAuthenticated"] = false;
						this.Session["BeenAuthenticatedByService"] = false;

						string message = "Authentication failed for user: {0}";
						message = string.Format(message, UsernameTextBox.Text);
						ShowMessage(message);
					}
				}
				else
				{
					Initialize();
				}
			}
			catch (System.Security.SecurityException ex)
			{
				string message = "Credentials provided are either empty or invalid. " + ex.Message;
				ShowMessage(message);
			}
			catch (System.TimeoutException ex)
			{
				string message = "The connection between server and client was lost because of a timeout. " + ex.Message;
				ShowMessage(message);
			}
			catch (Exception ex)
			{
				string message = "An error occurred while authenticating. " + ex.Message;
				ShowMessage(message);
			}
		}


		/// <summary>
		/// Allows user to download system log (&lt;logs-folder&gt;\compare_service_system.log)
		/// A convenient alternative to browsing the disk for the log file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DownloadSystemLogButton_Click(object sender, EventArgs e)
		{
			try
			{
				string syslogPath = GetSystemLogFilePath();
				if (syslogPath == string.Empty)
				{
					return;
				}

				string header = "attachment; filename={0}";
				header = string.Format(header, Path.GetFileName(syslogPath));

				Response.ContentType = "text/html";
				Response.AppendHeader("Content-Disposition", header);
				Response.TransmitFile(syslogPath);
				Response.End();
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		/// <summary>
		/// Navigation control for GridView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void PreviousDataPageButton_Click(object sender, EventArgs e)
		{
			object indexVar = Session["CurrentPageIndex"];
			int currentPageIndex = 0;
			if (indexVar != null)
			{
				currentPageIndex = (int) indexVar;
				currentPageIndex--;
				if (currentPageIndex < 0)
				{
					currentPageIndex = 0;
				}
			}

			Session["CurrentPageIndex"] = currentPageIndex;

			UpdateGridSettingsAndData();
		}

		/// <summary>
		/// Navigation control for GridView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void NextDataPageButton_Click(object sender, EventArgs e)
		{
			object indexVar = Session["CurrentPageIndex"];
			int currentPageIndex = 0;
			if (indexVar != null)
			{
				currentPageIndex = (int) indexVar;
				currentPageIndex++;
			}

			Session["CurrentPageIndex"] = currentPageIndex;

			UpdateGridSettingsAndData();
		}

		/// <summary>
		/// Navigation control for GridView, allows the user to navigate to a specific page,
		/// specified in the text box provided.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void GoToPageButton_Click(object sender, EventArgs e)
		{
			int pageIndex = 0;
			if (Int32.TryParse(PageIndexTextBox.Text, out pageIndex) && pageIndex > 0)
			{
				// PageIndex is zero-based, so decrement before use
				--pageIndex;
			}

			Session["CurrentPageIndex"] = pageIndex;
			UpdateGridSettingsAndData();
		}

		/// <summary>
		/// Service activation control: calls into an internal function
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void StartServiceButton_Click(object sender, EventArgs e)
		{
			StartService();
		}

		/// <summary>
		/// Service activation control: calls into an internal function
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void StopServiceButton_Click(object sender, EventArgs e)
		{
			StopService();
		}

		/// <summary>
		/// Service activation control: calls into an internal function
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RestartServiceButton_Click(object sender, EventArgs e)
		{
			RestartService();
		}

		/// <summary>
		/// Reloads comparison data from files and Recalculates statistics based on that data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RefreshStatisticsButton_Click(object sender, EventArgs e)
		{
			RefreshStatisticsButton.Enabled = false;

			DataViewType dataViewType = GetDataViewType();

			if (dataViewType == DataViewType.GraphView)
			{
				RefreshGraphicalDataButton_Click(sender, e);
			}
			else
			{
				RefreshTabularDataButton_Click(sender, e);
			}

			RefreshStatisticsButton.Enabled = true; 
		}

		#endregion

		#region Utility Functions

		/// <summary>
		/// Called on change of date, either from the textbox bound to the calendar,
		/// or by the Refresh Date button
		/// </summary>
		private void RefreshViewOnDateChange()
		{
			this._dataViewType = GetDataViewType();

			if (this._dataViewType == DataViewType.GraphView)
			{
				UpdateChartSettingsAndData();
			}
			else
			{
				ResetCurrentPageIndex();
				UpdateGridSettingsAndData();
			}
		}

		private DataViewType GetDataViewType()
		{
			object dataViewVar = Session["DataViewType"];
			if (dataViewVar != null)
			{
				return (DataViewType) dataViewVar;
			}

			return DataViewType.GraphView;
		}
		private void ResetCurrentPageIndex()
		{
			Session["CurrentPageIndex"] = 0;
		}

		/// <summary>
		/// Gets the host log entry for the specified binding and chunking option.
		/// Some bindings, such as basicHttpBinding, do not support chunking;
		/// others have different interfaces/URIs for chunking or not chunking.
		/// </summary>
		/// <param name="binding"></param>
		/// <param name="chunking"></param>
		/// <returns></returns>
		private CompareServiceHostLogEntry GetHostEntry(string binding, bool chunking)
		{
			foreach (CompareServiceHostLogEntry logentry in _csHostLogEntries)
			{
				if (logentry.Transport == binding && logentry.ChunkingEnabled == chunking)
				{
					return logentry;
				}
			}

			return null;
		}

		/// <summary>
		/// Gets the install path of Compare Service from the registry.
		/// </summary>
		/// <returns></returns>
		private static string GetInstallDir()
		{
			try
			{
				RegistryKey logFileKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Workshare\EcoSystem\CompareService");
				if (logFileKey != null)
				{
					return (string) logFileKey.GetValue("InstallDir");
				}
			}
			catch
			{
			}

			return string.Empty;
		}

		/// <summary>
		/// Returns the paths of all the audit log files in the logs folder.
		/// There can be multiple audit log files, whose names are based on the date.
		/// This helps in keeping log files' size to a reasonable limit.
		/// </summary>
		/// <returns></returns>
		private string[] GetAuditLogFilePaths()
		{
			List<string> files = new List<string>();

			string filepath = ConfigurationSettings.AppSettings["LogsFolder"];
			try
			{
				DirectoryInfo dir = new DirectoryInfo(filepath);
				string filename = ConfigurationSettings.AppSettings["AuditLogFileName"];

				// Use a wild-card filter to find all the logfiles in the directory
				foreach (FileInfo file in dir.GetFiles(filename + "*"))
				{
					files.Add(file.FullName);
				}
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}

			return files.ToArray();
		}

		/// <summary>
		/// Gets the path of the host log file. There will be only one.
		/// The host log file is a subset of the system log, containing information specific to host.
		/// </summary>
		/// <returns></returns>
		private string GetHostLogFilePath()
		{
			try
			{
				string filepath = ConfigurationSettings.AppSettings["LogsFolder"];
				string filename = ConfigurationSettings.AppSettings["HostLogFileName"];
				return Path.Combine(filepath, filename);
			}
			catch
			{
			}

			return string.Empty;

		}

		/// <summary>
		/// Gets the path of the system log file. There will be only one.
		/// </summary>
		/// <returns></returns>
		private string GetSystemLogFilePath()
		{
			try
			{
				string filepath = ConfigurationSettings.AppSettings["LogsFolder"];
				string filename = ConfigurationSettings.AppSettings["SystemLogFileName"];
				return Path.Combine(filepath, filename);
			}
			catch
			{
			}

			return string.Empty;

		}

		/// <summary>
		/// A simple hash for password.
		/// </summary>
		/// <param name="pass"></param>
		/// <returns></returns>
		private string CodePassword(string pass)
		{
			if (pass == null || pass == string.Empty)
			{
				throw new Exception("Password cannot be empty.");
			}

			byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);

			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] ^= 0x56;
			}

			return System.Text.Encoding.Unicode.GetString(bytes);
		}

		/// <summary>
		/// Reads version string from registery
		/// </summary>
		/// <returns></returns>
		private string GetVersionString()
		{
			object versionVar = Session["VersionString"];
			if (versionVar != null)
			{
				return versionVar.ToString();
			}

			try
			{
				RegistryKey uninstallKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{E5E5C159-0A74-4FBF-83E7-491F376360B4}");
				if (uninstallKey != null)
				{
					string versionString = uninstallKey.GetValue("DisplayVersion").ToString();
					Session["VersionString"] = versionString;
					return versionString;
				}
			}
			catch
			{
			}

			return string.Empty;
		}

		/// <summary>
		/// Reads distinct users from logs and fills both the dropdown lists
		/// </summary>
		private void PopulateUsersList()
		{
			GraphUsersList.Items.Clear();
			GridUsersList.Items.Clear();

			StringCollection users = GetUsersFromLog();

			GraphUsersList.Items.Add(string.Empty);
			GridUsersList.Items.Add(string.Empty);

			foreach (string user in users)
			{
				GraphUsersList.Items.Add(user);
				GridUsersList.Items.Add(user);
			}
		}

		/// <summary>
		/// Reads distinct users from logs
		/// </summary>
		/// <returns></returns>
		private StringCollection GetUsersFromLog()
		{
			StringCollection users = new StringCollection();

			foreach (CompareServiceAuditLogEntry logentry in _csAuditLogEntries.Values)
			{
				if (!users.Contains(logentry.UserName))
				{
					users.Add(logentry.UserName);
				}
			}

			return users;
		}

		/// <summary>
		/// Returns audit log entries filtered on userName
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		private List<CompareServiceAuditLogEntry> GetLogsForUser(string userName)
		{
			List<CompareServiceAuditLogEntry> userLogs = new List<CompareServiceAuditLogEntry>();

			foreach (CompareServiceAuditLogEntry logentry in _csAuditLogEntries.Values)
			{
				if (logentry.UserName == userName)
				{
					userLogs.Add(logentry);
				}
			}

			return userLogs;
		}

		/// <summary>
		/// Filters log entries on time. We need an index-based dictionary here as this will also be used
		/// by GridView, which needs random access to data, for paged-navigation
		/// </summary>
		/// <param name="logentries"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		private Dictionary<long, CompareServiceAuditLogEntry> FilterLogEntriesOnTime(List<CompareServiceAuditLogEntry> logentries, DateTime startTime, DateTime endTime)
		{
			Dictionary<long, CompareServiceAuditLogEntry> filteredEntries = new Dictionary<long, CompareServiceAuditLogEntry>();
			long index = 0;

			foreach (CompareServiceAuditLogEntry logentry in logentries)
			{
				if (logentry.LogTime >= startTime && logentry.LogTime <= endTime)
				{
					filteredEntries.Add(index++, logentry);
				}
			}

			return filteredEntries;
		}

		/// <summary>
		/// Initialize the UI
		/// </summary>
		/// <param name="compositorVersion"></param>
		/// <param name="serviceVersion"></param>
		private void Initialize()
		{
			Initialize(string.Empty, string.Empty);
		}

		/// <summary>
		/// Initialize the UI
		/// </summary>
		/// <param name="compositorVersion"></param>
		/// <param name="serviceVersion"></param>
		private void Initialize(string compositorVersion, string serviceVersion)
		{
			// Save gems for later use
			this.Session["BeenAuthenticated"] = true;
			this.Session["Passw"] = CodePassword(PasswordTextBox.Text);
			this.Session["UserName"] = UsernameTextBox.Text;
			this.Session["Domain"] = DomainTextBox.Text;

			CompositorVersionLabel.Text = compositorVersion;
			ServiceVersionLabel.Text = serviceVersion;

			UpdatePageUI();

			// Init config screen
			InitDashboardData();
		}

		/// <summary>
		/// This is the main entry point for config screen.
		/// </summary>
		private void InitDashboardData()
		{
			ReadHostLogDataFromFile(); // Fills up _csHostLogEntries
			PopulateEndpointsData();

			ReadAuditLogDataFromFile();	//Fills up _cslogentries
			PopulateUsersList();

			ResetCurrentPageIndex();
			UpdateGridSettingsAndData();

			PopulatePageUIFields();

			_dataFrequency = DataFrequency.Daily;
			Session["DataFrequency"] = _dataFrequency;

			UpdateChartSettingsAndData();

			Session["LogEntries"] = _csAuditLogEntries;
			ShowGraph(true);
			_dataViewType = DataViewType.GraphView;
			Session["DataViewType"] = _dataViewType;

			UpdateServiceStatus();

		}

		/// <summary>
		/// Enables/disables navigation controls based on current index.
		/// We don't want to show Previous on the first page or Next on the last page.
		/// </summary>
		/// <param name="startIndex"></param>
		/// <param name="stopIndex"></param>
		/// <param name="count"></param>
		private void UpdateNavigationControls(long startIndex, long stopIndex, long count)
		{
			NextDataPageButton.Enabled = (stopIndex < count);
			PreviousDataPageButton.Enabled = (startIndex > 0);
		}

		/// <summary>
		/// Checks for the registry entry, and returns true/false based on whatever is found/not-found.
		/// Used to activate "Update Configuration" button. We only show this button if Advanced Web Sample
		/// is installed on the same machine as this Config Sample, as we cannot update configuration on a
		/// remote machine.
		/// </summary>
		/// <returns></returns>
		private bool IsAdvSampleInstalled()
		{
			object installedVar = Session["IsAdvSampleInstalled"];
			if (installedVar != null)
			{
				return (bool) installedVar;
			}

			try
			{
				RegistryKey compareService = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Workshare\Ecosystem\CompareService");
				if (compareService != null)
				{
					bool installed = Boolean.Parse((string) compareService.GetValue("InstallAdvancedASPClient"));
					Session["IsAdvSampleInstalled"] = installed;
					return installed;
				}
			}
			catch
			{
			}

			return false;
		}

		#endregion

		#region Service Status Helpers

		// The following section handles Start/Stop/Restart for the service.
		// We are using Win32 APIs to impersonate the logged-in user, as ASPNET/NETWORK SERVICE user does
		// not have permissions to control the Service.

		WindowsImpersonationContext _impersonationContext;

		private const int LOGON32_LOGON_INTERACTIVE = 2;
		private const int LOGON32_PROVIDER_DEFAULT = 0;

		[DllImport("advapi32.dll")]
		private static extern int LogonUserA(String lpszUserName, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool RevertToSelf();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool CloseHandle(IntPtr handle);


		private bool ImpersonateValidUser(String userName, String domain, String password)
		{
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;

			try
			{
				if (RevertToSelf())
				{
					if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
						LOGON32_PROVIDER_DEFAULT, ref token) != 0)
					{
						if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
						{
							WindowsIdentity tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
							_impersonationContext = tempWindowsIdentity.Impersonate();
							if (_impersonationContext != null)
							{
								return true;
							}
						}
					}
				}
			}
			finally
			{
				if (tokenDuplicate != IntPtr.Zero)
				{
					CloseHandle(tokenDuplicate);
				}
				if (token != IntPtr.Zero)
				{
					CloseHandle(token);
				}
			}
			return false;
		}

		private void UndoImpersonation()
		{
			if (_impersonationContext != null)
			{
				_impersonationContext.Undo();
			}
		}

		private void DoImpersonation()
		{
			string userName = (string)Session["UserName"];
			string password = CodePassword((string)Session["Passw"]);
			string domain = (string)Session["Domain"];

			ImpersonateValidUser(userName, domain, password);
		}

		private void StartService()
		{
			try
			{
				DoImpersonation();

				ServiceController sc = new ServiceController("Workshare Compare Service");
				sc.Start();
				sc.WaitForStatus(ServiceControllerStatus.Running);

				UndoImpersonation();

				UpdateServiceUI(ServiceControllerStatus.Running, string.Empty);
			}
			catch (Exception ex)
			{
				UpdateServiceUI(ServiceControllerStatus.StartPending, ex.Message);
			}
		}
		private void StopService()
		{
			try
			{
				DoImpersonation();

				System.ServiceProcess.ServiceController sc = new ServiceController("Workshare Compare Service");
				sc.Stop();
				sc.WaitForStatus(ServiceControllerStatus.Stopped);

				UndoImpersonation();

				UpdateServiceUI(ServiceControllerStatus.Stopped, string.Empty);
			}
			catch (Exception ex)
			{
				UpdateServiceUI(ServiceControllerStatus.StopPending, ex.Message);
			}
		}
		private void RestartService()
		{
			StopService();
			StartService();
		}

		private ServiceControllerStatus GetServiceStatus()
		{
			try
			{
				System.ServiceProcess.ServiceController sc = new ServiceController("Workshare Compare Service");
				return sc.Status;
			}
			catch
			{
				return ServiceControllerStatus.Stopped;
			}
		}

		private void UpdateServiceStatus()
		{
			try
			{
				System.ServiceProcess.ServiceController sc = new ServiceController("Workshare Compare Service");

				if (sc != null)
				{
					UpdateServiceUI(sc.Status, string.Empty);
				}
			}
			catch (Exception ex)
			{
				UpdateServiceUI(ServiceControllerStatus.StartPending, ex.Message);
			}
		}
		private void UpdateServiceUI(ServiceControllerStatus status, string message)
		{
			switch (status)
			{
			case ServiceControllerStatus.Running:
				this.ServiceStatusLabel.Text = "Service is running ...";
				this.StartServiceButton.Enabled = false;
				this.StopServiceButton.Enabled = true;
				this.RestartServiceButton.Enabled = true;
				break;
			case ServiceControllerStatus.Stopped:
				this.ServiceStatusLabel.Text = "Service is stopped.";
				this.StartServiceButton.Enabled = true;
				this.StopServiceButton.Enabled = false;
				this.RestartServiceButton.Enabled = false;
				break;
			default:
				this.ServiceStatusLabel.Text = message;
				this.StartServiceButton.Enabled = false;
				this.StopServiceButton.Enabled = false;
				this.RestartServiceButton.Enabled = false;
				break;
			}
		}

        /* New methods to work with IIS Service Host*/
        private const string _regSubKeyValue = @"SOFTWARE\Workshare\Ecosystem\CompareService";
        private static bool m_bInstallIISHost = false;

        private bool InstallIISHost
        {
            get
            {
                try
                {
                    RegistryView rw = Environment.Is64BitOperatingSystem == true ? RegistryView.Registry64 : RegistryView.Registry32;
                    RegistryKey HKLocalMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, rw);
                    RegistryKey ecoKey = HKLocalMachine.OpenSubKey(_regSubKeyValue);
                    string val = ecoKey.GetValue("InstallIISHost").ToString();
                    if (val.ToUpper() == "TRUE")
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private bool InstallWSHost  // InstallWindowsServiceHost
        {
            get 
            {
                try
                {
                    RegistryView rw = Environment.Is64BitOperatingSystem == true ? RegistryView.Registry64 : RegistryView.Registry32;
                    RegistryKey HKLocalMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, rw);
                    RegistryKey ecoKey = HKLocalMachine.OpenSubKey(_regSubKeyValue);                    
                    string value = ecoKey.GetValue("InstallWindowsServiceHost").ToString();
                    if (value.ToUpper() == "TRUE")
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private bool IsTCPPortUsed(int port)
        {
            System.Net.IPEndPoint[] tcpListeners = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            foreach (System.Net.IPEndPoint endpoint in tcpListeners)
            {
                if (port == endpoint.Port)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsIISHostRunning()
        {
            try
            {
                string httpPort = System.Configuration.ConfigurationManager.AppSettings["http_port"];
                int port = -1;
                if (!string.IsNullOrEmpty(httpPort) && Int32.TryParse(httpPort, out port))
                {
                    if (IsTCPPortUsed(port))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

		#endregion

		#region Client Script Helpers


		// Page Methods. These are exposed as WebServices to client-side Javascript, although no special
		// notation is required on the client side to call these functions. ASP AJAX handles all the complexities.

		/// <summary>
		/// Returns the rate at which data is being transferred.
		/// Data rate is calculated in Perfomer.aspx, which is hosted in an IFRAME in the main page.
		/// Performer calls into Compare Service for benchmarking, and updates the data rate Session
		/// variables, then we read them here. This method is called every 1 second, from client-side Javascript.
		/// </summary>
		/// <returns></returns>
		[System.Web.Script.Services.ScriptMethod]
		[System.Web.Services.WebMethod]
		public static object GetDataRate()
		{
			long kbPerSecond = 0;
			long mbPerMinute = 0;
			try
			{
				kbPerSecond = (long) HttpContext.Current.Session["bytesPerSec"];
				mbPerMinute = (long) HttpContext.Current.Session["MbytesPerMin"];
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
				// These are the minimum settings for the gauges
				kbPerSecond = 2000;
				mbPerMinute = 100;
			}

			return new { KBPS = kbPerSecond, MBPM = mbPerMinute };
		}

		/// <summary>
		/// Returns the current data transfer progress as a percentage.
		/// This value is computed by Performer. This method is called every 1 second from
		/// client-side Javascript.
		/// </summary>
		/// <returns></returns>
		[System.Web.Script.Services.ScriptMethod]
		[System.Web.Services.WebMethod]
		public static object GetPercentComplete()
		{
			int percentComplete = 0;
			try
			{
				percentComplete = (int) HttpContext.Current.Session["percentComplete"];
			}
			catch
			{
			}

			return new { percentComplete = percentComplete };
		}

		/// <summary>
		/// Saves settings to the configuration file for the Advanced Web Sample.
		/// The config file can be updated with the desired protocol and chunk size.
		/// The Advanced Web Sample will then start using the updated settings.
		/// </summary>
		/// <param name="protocol"></param>
		/// <param name="chunksize"></param>
		[System.Web.Script.Services.ScriptMethod]
		[System.Web.Services.WebMethod]
		public static void UpdateConfigFile(object protocol, object chunksize)
		{
			try
			{
				string configFilePath = GetInstallDir();
				if (configFilePath == string.Empty)
				{
					return;
				}

				configFilePath = Path.Combine(configFilePath, @"AdvancedWebSample\Web.config");

				// WebConfigurationManager only accepts virtual paths, and we need to access
				// a config file outside the current application's virtual directory, using a physical path.
				System.Xml.XmlDocument config = new System.Xml.XmlDocument();

				config.Load(configFilePath);
				string protocolname = (string)protocol;
				string protocolport = string.Empty;
				switch (protocolname)
				{
					case "TCP":
						protocolport = System.Configuration.ConfigurationManager.AppSettings["tcp_port"];
						break;
					case "HTTP":
						protocolport = System.Configuration.ConfigurationManager.AppSettings["http_port"];
						break;

				}

				config.SelectSingleNode("configuration/appSettings/add[@key='transport.protocol']/@value").Value = protocolname;
				config.SelectSingleNode("configuration/appSettings/add[@key='chunk.size']/@value").Value = (string) chunksize;

				if (protocolport != string.Empty)
				{
					config.SelectSingleNode("configuration/appSettings/add[@key='cs.port']/@value").Value = protocolport;
				}

				config.Save(configFilePath);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Updates page UI using client-side code, based on two parameters:
		/// - Authentication
		/// - Advanced Web Sample installed or not
		/// </summary>
		private void UpdatePageUI()
		{
			bool beenAuthenticated = false;
			bool beenAuthenticatedByService = false;
			try
			{
				beenAuthenticated = (bool) this.Session["BeenAuthenticated"];
			}
			catch
			{
				beenAuthenticated = false;
			}

			ToggleLoginScreen(!beenAuthenticated);

			try
			{
				beenAuthenticatedByService = (bool)this.Session["BeenAuthenticatedByService"];
			}
			catch
			{
				beenAuthenticatedByService = false;
			}

			EnablePerformer(beenAuthenticatedByService);

			if (IsAdvSampleInstalled())
			{
				ToggleUpdateConfigButton(true);
			}
			else
			{
				ToggleUpdateConfigButton(false);
			}
		}

		/// <summary>
		/// Returns a new id for a script.
		/// ScriptManager needs a different ID for every script it executes.
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		private string GenerateScriptID(string prefix)
		{
			return prefix + Guid.NewGuid().ToString().Replace('-', '_');
		}

		/// <summary>
		/// Generates and executes a script to show or hide the Update Config button
		/// </summary>
		/// <param name="show">Whether the show or hide the button</param>
		private void ToggleUpdateConfigButton(bool show)
		{
			string script = "window.toggleUpdateConfigButton({0});";
			script = string.Format(script, show ? "true" : "false");
			ExecuteScript(GenerateScriptID("toggleUpdateConfig"), script);
		}
		/// <summary>
		/// Generates and executes a script to show or hide the Login screen
		/// </summary>
		/// <param name="show">Whether the show or hide the screen</param>
		private void ToggleLoginScreen(bool show)
		{
			string script = "window.toggleLoginSection({0});";
			script = string.Format(script, show ? "true" : "false");
			ExecuteScript(GenerateScriptID("toggleLogin"), script);
		}
		/// <summary>
		/// Generates and executes a script to show a message on the page
		/// </summary>
		/// <param name="show">The message to show</param>
		private void ShowMessage(string message)
		{
			string script = "window.updateStatus('{0}');";
			message = message.Replace(Environment.NewLine, " ");

			script = string.Format(script, message);
			ExecuteScript(GenerateScriptID("errmsg"), script);
		}
		/// <summary>
		/// Generates and executes a script to show a message in a pop-up messsage box
		/// </summary>
		/// <param name="show">The message to show</param>
		private void ShowMessageBox(string message)
		{
			string script = "window.alert('{0}');";
			message = message.Replace(Environment.NewLine, " ");

			script = string.Format(script, message);
			ExecuteScript(GenerateScriptID("errmsg"), script);
		}
		/// <summary>
		/// Generates and executes a script to toggle between the graph and the grid view
		/// </summary>
		/// <param name="show">Whether the show the graph view</param>
		private void ShowGraph(bool showGraph)
		{
			string script = "window.toggleGraphPanel({0});";
			script = string.Format(script, showGraph ? "true" : "false");
			ExecuteScript(GenerateScriptID("graph"), script);
		}

		/// <summary>
		/// Generates and executes a script to enable or disable the "Perform" button
		/// This would normally be called as a result of Compare Service running or not running.
		/// </summary>
		/// <param name="enable"></param>
		private void EnablePerformer(bool enable)
		{
			string script = "window.enablePerformer({0});";
			script = string.Format(script, enable ? "true" : "false");
			ExecuteScript(GenerateScriptID("enablePerformer"), script);
		}

		/// <summary>
		/// Execute the given Javascript.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="script"></param>
		private void ExecuteScript(string id, string script)
		{
			ScriptManager.RegisterStartupScript(this, typeof(_Default), id, script, true);
		}

		#endregion

		#region Data Manipulators

		/// <summary>
		/// Returns the number of comparisons that have been recorded in the log
		/// </summary>
		/// <returns></returns>
		private int GetNoOfComparisons()
		{
			return _csAuditLogEntries.Count;
		}

		/// <summary>
		/// Returns the number of comparisons run in the last hour
		/// </summary>
		/// <returns></returns>
		private int GetNoOfComparisonsInLastHour()
		{
			TimeSpan oneHour = new TimeSpan(1, 0, 0);
			DateTime oneHourBefore = DateTime.Now - oneHour;

			return _csAuditLogEntries.Values.Count(entry => entry.LogTime > oneHourBefore);
		}

		/// <summary>
		/// Returns the total number of kilobytes of data for all comparisons
		/// </summary>
		/// <returns>Number of bytes in KB</returns>
		private long GetNoOfKBytesProcessed()
		{
			// Sum bytes for all comparisons to find out total number of bytes for all comparisons
			double noOfBytes = _csAuditLogEntries.Values.Sum(entry => (double) (entry.OriginalSize + entry.ModifiedSize));
			return (long) (noOfBytes / 1024);
		}

		/// <summary>
		/// Returns the total number of kilobytes of data for all comparisons in the last hour
		/// </summary>
		/// <returns>Number of bytes in KB</returns>
		private long GetNoOfKBytesProcessedInLastHour()
		{
			TimeSpan oneHour = new TimeSpan(1, 0, 0);
			DateTime oneHourBefore = DateTime.Now - oneHour;

			IEnumerable<CompareServiceAuditLogEntry> comparisons = _csAuditLogEntries.Values.Where(
				entry => entry.LogTime > oneHourBefore);
			double noOfBytes = comparisons.Sum(entry => (double) (entry.OriginalSize + entry.ModifiedSize));
			return (long) (noOfBytes / 1024);
		}

		/// <summary>
		/// Returns the mean data processing speed for comparisons,
		/// excluding comparisons that involved conversion of files.
		/// </summary>
		/// <returns>Average comparison speed in kilobytes per second, to the nearest integer</returns>
		private double GetAverageComparisonRate()
		{
			IEnumerable<CompareServiceAuditLogEntry> comparisons = _csAuditLogEntries.Values.Where(
				entry => entry.OriginalConversionTime == 0
					&& entry.ModifiedConversionTime == 0);
			return GetAverageComparisonSpeed(comparisons, false);
		}

		/// <summary>
		/// Returns the mean data processing speed for comparisons in the last hour,
		/// excluding comparisons that involved conversion of files.
		/// </summary>
		/// <returns>Average comparison speed in kilobytes per second, to the nearest integer</returns>
		private double GetAverageComparisonRateInLastHour()
		{
			TimeSpan oneHour = new TimeSpan(1, 0, 0);
			DateTime oneHourBefore = DateTime.Now - oneHour;

			IEnumerable<CompareServiceAuditLogEntry> comparisons = _csAuditLogEntries.Values.Where(
				entry => entry.OriginalConversionTime == 0
					&& entry.ModifiedConversionTime == 0
					&& entry.LogTime > oneHourBefore);
			return GetAverageComparisonSpeed(comparisons, false);
		}

		/// <summary>
		/// Returns the mean data processing speed for comparisons, including conversion of files.
		/// </summary>
		/// <returns>Average comparison speed in kilobytes per second, to the nearest integer</returns>
		private double GetAverageComparisonRateWithConversion()
		{
			return GetAverageComparisonSpeed(_csAuditLogEntries.Values, true);
		}

		/// <summary>
		/// Returns the mean data processing speed for comparisons in the last hour,
		/// including conversion of files.
		/// </summary>
		/// <returns>Average comparison speed in kilobytes per second, to the nearest integer</returns>
		private double GetAverageComparisonRateInLastHourWithConversion()
		{
			TimeSpan oneHour = new TimeSpan(1, 0, 0);
			DateTime oneHourBefore = DateTime.Now - oneHour;

			IEnumerable<CompareServiceAuditLogEntry> comparisons = _csAuditLogEntries.Values.Where(
				entry => entry.LogTime > oneHourBefore);
			return GetAverageComparisonSpeed(comparisons, true);
		}

		/// <summary>
		/// Returns the mean data processing speed for the given logged comparisons
		/// </summary>
		/// <returns>Average comparison speed in kilobytes per second, to the nearest integer</returns>
		private double GetAverageComparisonSpeed(IEnumerable<CompareServiceAuditLogEntry> comparisons, bool withConversion)
		{
			// Compute the average comparison speed by dividing total execution time for all log entries
			// by total number of bytes for all log entries
			double totalSize = 0.0;		// in bytes
			double totalTime = 0.0;		// in ms

			foreach (CompareServiceAuditLogEntry logEntry in comparisons)
			{
				totalSize += (logEntry.OriginalSize + logEntry.ModifiedSize);
				totalTime += logEntry.TotalExecutionTime;
				if (withConversion)
				{
					totalTime += logEntry.OriginalConversionTime + logEntry.ModifiedConversionTime;
				}
			}

			totalSize /= 1024;	// bytes to KBs
			totalTime /= 1000;	// milliseconds to seconds

			if (totalTime == 0)
			{
				return 0.0;
			}
			return Math.Round(totalSize / totalTime);
		}

		/// <summary>
		/// Gets chart data for the specified calendar year (January 1 to December 31).
		/// For charting purposes, we need to manipulate the data a bit, to align on a single time.
		/// </summary>
		/// <param name="logentries"></param>
		/// <returns></returns>
		private List<ChartData> Get12MonthsChartData(List<CompareServiceAuditLogEntry> logentries)
		{
			Dictionary<long, CompareServiceAuditLogEntry> filteredLogEntries = Get12MonthsData(logentries);
			return ComputeChartData(filteredLogEntries.Values.ToList(), DataFrequency.Monthly);
		}

		/// <summary>
		/// Gets grid data for the specified calendar year (January 1 to December 31).
		/// </summary>
		/// <param name="logentries"></param>
		/// <returns></returns>
		private Dictionary<long, CompareServiceAuditLogEntry> Get12MonthsData(List<CompareServiceAuditLogEntry> logentries)
		{
			int year = 0;
			if (!Int32.TryParse(this.EndTimeCalendarYear.Text, out year))
			{
				year = DateTime.Now.Year;
			}

			DateTime startTime = new DateTime(year, 1, 1);	// 1st of Jan for the specified year
			DateTime endTime = new DateTime(year, 12, 31,23, 59, 59);	// 31st of Dec for the specified year

			return FilterLogEntriesOnTime(logentries, startTime, endTime);
		}

		/// <summary>
		/// Gets chart data for the 30 days up to the end of the specified date.
		/// </summary>
		/// <param name="logentries"></param>
		/// <returns></returns>
		private List<ChartData> Get30DaysChartData(List<CompareServiceAuditLogEntry> logentries)
		{
			Dictionary<long, CompareServiceAuditLogEntry> filteredLogEntries = Get30DaysData(logentries);
			return ComputeChartData(filteredLogEntries.Values.ToList(), DataFrequency.Daily);
		}

		/// <summary>
		/// Gets grid data for the 30 days up to the end of the specified date.
		/// </summary>
		/// <param name="logentries"></param>
		/// <returns></returns>
		private Dictionary<long, CompareServiceAuditLogEntry> Get30DaysData(List<CompareServiceAuditLogEntry> logentries)
		{
			DateTime endTime = DateTime.Now;
			if (!DateTime.TryParse(this.EndTimeCalendar.Text, out endTime))
			{
				endTime = DateTime.Now;
			}
			else
			{
				// As we read endTime for filtering log entries from a textbox that contains only date, and not time.
				// If its current date, we need to append time to it, so that it shows comparisons performed today.
				if (endTime.Date == DateTime.Now.Date)
				{
					endTime = DateTime.Now;
				}
			}

			TimeSpan span = new TimeSpan(30, 0, 0, 0);
			DateTime startTime = endTime - span;

			return FilterLogEntriesOnTime(logentries, startTime, endTime);
		}

		/// <summary>
		/// Returns data for comparisons, to be plotted on a graph.
		/// Aligns the data on a standard time of day/month,
		/// for a smooth line and better visibility of data on the graph.
		/// </summary>
		/// <param name="filteredLogEntries"></param>
		/// <param name="dataFrequency"></param>
		/// <returns></returns>
		private List<ChartData> ComputeChartData(List<CompareServiceAuditLogEntry> filteredLogEntries, DataFrequency dataFrequency)
		{
			SortedDictionary<DateTime, ChartData> data = new SortedDictionary<DateTime, ChartData>();

			foreach (CompareServiceAuditLogEntry logentry in filteredLogEntries)
			{
				DateTime logTime = logentry.LogTime;
				if (dataFrequency == DataFrequency.Daily)
				{
					logTime = CoerceTimeOfDay(logTime);
				}
				else if (dataFrequency == DataFrequency.Monthly)
				{
					logTime = CoerceTimeOfMonth(logTime);
				}

				ChartData point = null;
				if (data.TryGetValue(logTime, out point))
				{
					++point.NoOfComparisons;
				}
				else
				{
					point = new ChartData(logTime, 1);
					data.Add(logTime, point);
				}
			}

			return data.Values.ToList();
		}

		/// <summary>
		/// Returns the date/time with the day changed to mid-month,
		/// for consistency of monthly graphed data
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		private DateTime CoerceTimeOfMonth(DateTime date)
		{
			return new DateTime(date.Year, date.Month, 15);
		}

		/// <summary>
		/// Returns the date/time with the time of day changed to just before midnight,
		/// for consistency of daily graphed data
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		private DateTime CoerceTimeOfDay(DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
		}

		#endregion

		#region Chart Functions

		/// <summary>
		/// Initialize the chart, setting its labels, and providing data to it
		/// </summary>
		/// <param name="chartData"></param>
		private void PopulateChartData(List<ChartData> chartData)
		{
			Series chartSeries = timeChart.Series["Series1"];
			chartSeries.BorderWidth = 3;
			chartSeries.MarkerStyle = MarkerStyle.Circle;

			// Set series chart type
			chartSeries.ChartType = SeriesChartType.Line;

			DataPointCollection points = chartSeries.Points;
			foreach (ChartData dp in chartData)
			{
				points.AddXY(dp.ComparisonDate, dp.NoOfComparisons);
			}

			ChartArea chartArea = timeChart.ChartAreas["ChartArea1"];
			chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

			// Set X axis margin
			chartArea.AxisX.IsMarginVisible = true;
			chartArea.Area3DStyle.Enable3D = false;

			chartArea.AxisY.Title = "No of comparisons";

			// Format the string displayed on x-axis, based on selected data frequency
			if (_dataFrequency == DataFrequency.Daily)
			{
				LabelStyle ls = new LabelStyle();
				ls.Format = "dd/MM";
				chartArea.AxisX.LabelStyle = ls;
				chartArea.AxisX.Interval = 1;

				// Set x-axis labels
				chartArea.AxisX.Title = "Days";
			}
			else if (_dataFrequency == DataFrequency.Monthly)
			{
				LabelStyle ls = new LabelStyle();
				ls.Format = "MMM";
				chartArea.AxisX.LabelStyle = ls;
				chartArea.AxisX.Interval = 30;

				// Set x-axis labels
				chartArea.AxisX.Title = "Months";
			}
		}

		/// <summary>
		/// Re-calculates the data, updates the charts, and refreshes the view
		/// </summary>
		private void UpdateChartSettingsAndData()
		{
			try
			{
				timeChart.Series["Series1"].Points.Clear();
				List<CompareServiceAuditLogEntry> logentries = null;

				string userName = this.GraphUsersList.SelectedValue;
				List<ChartData> chartData = null;

				// If a user is specified, filter on that, get all log entries otherwise
				if (userName != string.Empty)
				{
					logentries = GetLogsForUser(userName);
				}
				else
				{
					logentries = _csAuditLogEntries.Values.ToList();
				}

				// Filter on time
				if (_dataFrequency == DataFrequency.Daily)
				{
					chartData = Get30DaysChartData(logentries);
				}
				else if (_dataFrequency == DataFrequency.Monthly)
				{
					chartData = Get12MonthsChartData(logentries);
				}

				//Re-populate the chart
				PopulateChartData(chartData);

				// Refresh the view
				GraphPanel.Update();
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		#endregion

		#region Grid Functions

		/// <summary>
		/// Re-calculates the data, updates the grid, and refreshes the view
		/// </summary>
		private void UpdateGridSettingsAndData()
		{
			try
			{
				List<CompareServiceAuditLogEntry> logentries = null;

				string userName = this.GridUsersList.SelectedValue;
				Dictionary<long, CompareServiceAuditLogEntry> filteredLogEntries = null;

				// If a user is specified, filter on that, get all log enties otherwise
				if (userName != string.Empty)
				{
					logentries = GetLogsForUser(userName);
				}
				else
				{
					logentries = _csAuditLogEntries.Values.ToList();
				}

				// Filter on time.
				if (_dataFrequency == DataFrequency.Daily)
				{
					filteredLogEntries = Get30DaysData(logentries);
				}
				else if (_dataFrequency == DataFrequency.Monthly)
				{
					filteredLogEntries = Get12MonthsData(logentries);
				}

				// Re-populate the grid
				PopulateComparisonDataTable(filteredLogEntries);

				// Refresh the view
				GridPanel.Update();
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		/// <summary>
		/// Actually populates the grid, handles paging and updates the navigation controls
		/// </summary>
		/// <param name="logentries"></param>
		private void PopulateComparisonDataTable(Dictionary<long, CompareServiceAuditLogEntry> logentries)
		{
			object indexVar = Session["CurrentPageIndex"];
			int currentPageIndex = 0;
			if (indexVar != null)
			{
				currentPageIndex = (int) indexVar;
			}

			int recordsPerPage = 100;
			Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["records_per_page"], out recordsPerPage);

			// Validate curent Page Index
			if (currentPageIndex < 0)
			{
				currentPageIndex = 0;
				Session["CurrentPageIndex"] = 0;
			}
			if (currentPageIndex > (logentries.Count / recordsPerPage))
			{
				currentPageIndex = (logentries.Count / recordsPerPage);
				Session["CurrentPageIndex"] = currentPageIndex;
			}

			// PageIndex is zero-based, so increment before displaying to user
			PageIndexTextBox.Text = (currentPageIndex + 1).ToString();

			// Calculate indices for current page
			long startIndex = (long) (currentPageIndex * recordsPerPage);
			long stopIndex = startIndex + recordsPerPage;

			if (stopIndex > logentries.Count)
			{
				stopIndex = logentries.Count;
			}

			// Update navigation controls based on indices
			UpdateNavigationControls(startIndex, stopIndex, logentries.Count);

			PageStatusLabel.Text = string.Format("Showing [ {0} - {1} ] of {2}", startIndex + 1, stopIndex, logentries.Count);

			// Populate the data
			for (long i = startIndex; i < stopIndex; i++)
			{
				CompareServiceAuditLogEntry logentry = logentries[i];
				TableRow dataRow = new TableRow();

				TableCell userName = new TableCell();
				userName.Text = logentry.UserName;

				TableCell logTime = new TableCell();
				logTime.HorizontalAlign = HorizontalAlign.Center;
				logTime.Text = string.Format("{0} {1}", logentry.LogTime.ToShortDateString(), logentry.LogTime.ToShortTimeString());

				TableCell redline = new TableCell();
				redline.HorizontalAlign = HorizontalAlign.Center;
				redline.Text = logentry.RedlineSize.ToString();

				TableCell summary = new TableCell();
				summary.HorizontalAlign = HorizontalAlign.Center;
				summary.Text = logentry.SummarySize.ToString();

				TableCell totalExecution = new TableCell();
				totalExecution.HorizontalAlign = HorizontalAlign.Center;
				totalExecution.Text = logentry.TotalExecutionTime.ToString();

				dataRow.Cells.Add(userName);
				dataRow.Cells.Add(logTime);
				dataRow.Cells.Add(redline);
				dataRow.Cells.Add(summary);
				dataRow.Cells.Add(totalExecution);

				ComparisonDataTable.Rows.Add(dataRow);
			}
		}

		#endregion

		#region Logs Data Functions

		/// <summary>
		/// Reads data from the audit log file(s) and populates _csAuditLogEntries.
		/// We sort the data here, on LogTime, so we don't need to do anything later on.
		/// </summary>
		private void ReadAuditLogDataFromFile()
		{
			try
			{
				_csAuditLogEntries.Clear();
				long logIndex = 0;
				string[] filenames = GetAuditLogFilePaths();

				List<CompareServiceAuditLogEntry> interimLogList = new List<CompareServiceAuditLogEntry>();

				foreach (string filename in filenames)
				{
					string logs = File.ReadAllText(filename);

					if (logs == string.Empty)
					{
						continue;
					}

					string[] logentries = logs.Split('\n');

					foreach (string logentry in logentries)
					{
						if (logentry == string.Empty)
						{
							continue;
						}

						CompareServiceAuditLogEntry logentryItem = new CompareServiceAuditLogEntry();
                        string[] entryparts = logentry.Split('|');
                        if (entryparts.Count() != 3 && entryparts[1].Trim() != "INFO")
                        {
                            continue;
                        }
                        logentryItem.LogTime = DateTime.ParseExact(entryparts[0], LogTimeFormat, CultureInfo.CurrentCulture);


                        string[] logattribs = entryparts[2].Split(',');
						foreach (string attrib in logattribs)
						{
							string[] keyval = attrib.Split('=');
							if (keyval.Length < 2)
							{
								continue;
							}
							keyval[0] = keyval[0].Trim();
							keyval[1] = keyval[1].Trim();
							switch (keyval[0])
							{
                            //case "datetime":
                            //    logentryItem.LogTime = DateTime.Parse(keyval[1]);
                            //    break;
							case "user":
								logentryItem.UserName = keyval[1];
								break;
							case "redline size":
								logentryItem.RedlineSize = Int32.Parse(keyval[1]);
								break;
							case "summary":
								logentryItem.SummarySize = Int32.Parse(keyval[1]);
								break;
							case "original size":
								logentryItem.OriginalSize = Double.Parse(keyval[1]);
								break;
							case "modified size":
								logentryItem.ModifiedSize = Double.Parse(keyval[1]);
								break;
							case "original conversion":
								logentryItem.OriginalConversionTime = Double.Parse(keyval[1]);
								break;
							case "modified conversion":
								logentryItem.ModifiedConversionTime = Double.Parse(keyval[1]);
								break;
							case "original preprocess":
								logentryItem.OriginalPreProcessTime = Double.Parse(keyval[1]);
								break;
							case "modified preprocess":
								logentryItem.ModifiedPreProcessTime = Double.Parse(keyval[1]);
								break;
							case "comparison execute":
								logentryItem.ComparisonExecutionTime = Double.Parse(keyval[1]);
								break;
							case "results processing":
								logentryItem.ResultsProcessingTime = Double.Parse(keyval[1]);
								break;
							case "total execute":
								logentryItem.TotalExecutionTime = Double.Parse(keyval[1]);
								break;
							}
						}

						interimLogList.Add(logentryItem);
					}
				}

				// This is in essence running the same loop twice, but we need to sort the values, before
				// they are inserted into dictionary, because we don't want to disturb the indices after
				// Dictionary<...> has been populated.

				logIndex = 0;
				interimLogList.Sort(new AuditLogEntryComparer(ListSortDirection.Descending, SortField.LogTime));
				foreach (CompareServiceAuditLogEntry log in interimLogList)
				{
					_csAuditLogEntries.Add(logIndex++, log);
				}

			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}

		}

		/// <summary>
		/// Reads data from the host log file and populates _csHostLogEntries,
		/// using data from the latest session (delimited by entries containing
		/// CompareHost:START and CompareHost:END).
		/// This data contains information for different bindings exposed by
		/// Compare Service, e.g which interface and URI to use when trying to connect to the service.
		/// </summary>
		private void ReadHostLogDataFromFile()
		{
			// Currently it reads the complete log file, to reach the end, and find the latest
			// set of log entries. This is inefficient, but it keeps things simpler.
			try
			{
				_csHostLogEntries.Clear();

				string filename = GetHostLogFilePath();
				if (filename == string.Empty)
				{
					return;
				}

				string logs = File.ReadAllText(filename);
				if (logs == string.Empty)
				{
					return;
				}

				string[] logentries = logs.Split('\n');

				foreach (string logentry in logentries)
				{
					if (logentry == string.Empty)
					{
						continue;
					}

                    string[] entryparts = logentry.Split('|');
                    if (entryparts.Count() != 3 || entryparts[1].Trim() != "INFO")
                    {
                        continue;
                    }

                    string[] logattribs = entryparts[2].Split(new char[] {':'}, 2);
                    if (logattribs.Count() != 2 || logattribs[0].Trim() != "CompareHost")
                    {
                        continue;
                    }
                    logattribs = logattribs[1].Trim().Split(',');
                    
                    if (logattribs.Count() > 1 && logattribs[0].StartsWith("AddServiceEndpoint"))
                    {
                        CompareServiceHostLogEntry logentryItem = new CompareServiceHostLogEntry();
                        logentryItem.LogTime = DateTime.ParseExact(entryparts[0], LogTimeFormat, CultureInfo.CurrentCulture);
                        foreach (string attrib in logattribs)
                        {
                            string[] keyval = attrib.Split('=');
                            if (keyval.Length >= 2)
                            {
                                keyval[0] = keyval[0].Trim();
                                keyval[1] = keyval[1].Trim();

                                if (keyval[0].StartsWith("AddServiceEndpoint"))
                                {
                                    int length = "AddServiceEndpoint".Length;
                                    keyval[0] = keyval[0].Substring(length, keyval[0].Length - length).Trim();
                                }

                                switch (keyval[0])
                                {
                                    case "datetime":
                                        logentryItem.LogTime = DateTime.Parse(keyval[1]);
                                        break;
                                    case "Transport":
                                        logentryItem.Transport = keyval[1];
                                        break;
                                    case "Chunking":
                                        logentryItem.ChunkingEnabled = Boolean.Parse(keyval[1]);
                                        break;
                                    case "Contract":
                                        logentryItem.ContractInterface = keyval[1];
                                        break;
                                    case "Uri":
                                        logentryItem.Uri = keyval[1];
                                        break;
                                }
                            }
                        }
                        _csHostLogEntries.Add(logentryItem);
                    }
                    else if( logattribs.Count() > 0)
                    {
					    // There may be some entries without an '=' sign. These are START
						// and END entries that delimit a set of host entries. We need to
						// handle these, to avoid any disruptions.

                        string attrib = logattribs[0].Trim();
						switch (attrib)
						{
						    case "START":
								break;
							case "END":
								_csHostLogEntries.Clear();
								break;
						}						
					}											
				}
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}

		}

		/// <summary>
		/// After reading the host data, this function is called. It calls GetHostEntry to get information
		/// on a specific entry, e.g. HTTP binding with Chunking enabled, and then dispays that information in
		/// the "Settings" tab of the config screen
		/// </summary>
		private void PopulateEndpointsData()
		{
			try
			{
				CompareServiceHostLogEntry legacyHttp = GetHostEntry("LegacyHttp", false);
				if (legacyHttp != null)
				{
					LegacyHttpContractInterface.Text = legacyHttp.ContractInterface;
					LegacyHttpURI.Text = legacyHttp.Uri;
				}

				CompareServiceHostLogEntry http = GetHostEntry("Http", false);
				if (http != null)
				{
					HttpContractInterface.Text = http.ContractInterface;
					HttpURI.Text = http.Uri;
				}

				CompareServiceHostLogEntry httpChunking = GetHostEntry("Http", true);
				if (httpChunking != null)
				{
					HttpChunkingInterface.Text = httpChunking.ContractInterface;
					HttpChunkingURI.Text = httpChunking.Uri;
				}

				CompareServiceHostLogEntry tcp = GetHostEntry("Tcp", false);
				if (tcp != null)
				{
					TcpContractInterface.Text = tcp.ContractInterface;
					TcpURI.Text = tcp.Uri;
				}

				CompareServiceHostLogEntry tcpChunking = GetHostEntry("Tcp", true);
				if (tcpChunking != null)
				{
					TcpChunkingInterface.Text = tcpChunking.ContractInterface;
					TcpChunkingURI.Text = tcpChunking.Uri;
				}

				CompareServiceHostLogEntry namedPipe = GetHostEntry("NamedPipe", false);
				if (namedPipe != null)
				{
					NamedPipeContractInterface.Text = namedPipe.ContractInterface;
					NamedPipeURI.Text = namedPipe.Uri;
				}

				CompareServiceHostLogEntry namedPipeChunking = GetHostEntry("NamedPipe", true);
				if (namedPipeChunking != null)
				{
					NamedPipeChunkingInterface.Text = namedPipeChunking.ContractInterface;
					NamedPipeChunkingURI.Text = namedPipeChunking.Uri;
				}
			}
			catch (System.Exception ex)
			{
				ShowMessage(ex.Message);
			}
		}

		/// <summary>
		/// Updates values on the config screen
		/// </summary>
		private void PopulatePageUIFields()
		{
			NoOfComparisons.Text = GetNoOfComparisons().ToString();
			NoOfBytesProcessed.Text = GetNoOfKBytesProcessed().ToString();
			AverageComparisonRate.Text = GetAverageComparisonRate().ToString();
			AverageComparisonRateWithConversion.Text = GetAverageComparisonRateWithConversion().ToString();			

			NoOfComparisonsInLastHour.Text = GetNoOfComparisonsInLastHour().ToString();
			NoOfBytesProcessInLastHour.Text = GetNoOfKBytesProcessedInLastHour().ToString();
			AverageComparisonRateInLastHour.Text = GetAverageComparisonRateInLastHour().ToString();
			AverageComparisonRateInLastHourWithConversion.Text = GetAverageComparisonRateInLastHourWithConversion().ToString();

			ReadAndPopulateServiceHostInfo();

			EndTimeCalendar.Text = DateTime.Now.ToShortDateString();
			EndTimeCalendarYear.Text = DateTime.Now.Year.ToString();
		}

		/// <summary>
		/// Opens Service Configuration file to read connections settings.
		/// Currently only HTTP and TCP ports are displayed.
		/// </summary>
		private void ReadAndPopulateServiceHostInfo()
		{
			string filename = ConfigurationSettings.AppSettings["HostFolder"];

			filename = Path.Combine(filename, "Workshare.CompareService.ServiceHost.exe");

			Configuration config = ConfigurationManager.OpenExeConfiguration(filename);
			HttpPortTextBox.Text = config.AppSettings.Settings["http_port"].Value;
			TcpPortTextBox.Text = config.AppSettings.Settings["tcp_port"].Value;
		}

		#endregion
	}

	#region Class Structs

	class CompareServiceAuditLogEntry
	{
		public string UserName { get; set; }
		public DateTime LogTime { get; set; }
		public int RedlineSize { get; set; }
		public int SummarySize { get; set; }
		public double OriginalConversionTime { get; set; }
		public double OriginalPreProcessTime { get; set; }
		public double OriginalSize { get; set; }
		public double ModifiedSize { get; set; }
		public double ModifiedConversionTime { get; set; }
		public double ModifiedPreProcessTime { get; set; }
		public double ComparisonExecutionTime { get; set; }
		public double ResultsProcessingTime { get; set; }
		public double TotalExecutionTime { get; set; }
	}

	class CompareServiceHostLogEntry
	{
		public DateTime LogTime { get; set; }
		public string Transport { get; set; }
		public bool ChunkingEnabled { get; set; }
		public string ContractInterface { get; set; }
		public string Uri { get; set; }
	}

	class ChartData
	{
		public ChartData(DateTime comparisonDate, long noOfComparisons)
		{
			ComparisonDate = comparisonDate;
			NoOfComparisons = noOfComparisons;
		}

		public DateTime ComparisonDate { get; set; }
		public long NoOfComparisons { get; set; }
	}

	enum DataFrequency
	{
		Daily,
		Monthly,
		Yearly
	}

	enum DataViewType
	{
		GraphView = 0,
		GridView
	}

	enum SortField
	{
		LogTime = 0,
		RedlineSize,
		SummarySize,
		TotalExecutionTime,
		UserName
	}

	class AuditLogEntryComparer : IComparer<CompareServiceAuditLogEntry>
	{
		private ListSortDirection _sortOrder;
		private SortField _sortField;

		public AuditLogEntryComparer(ListSortDirection sortOrder, SortField sortField)
		{
			_sortOrder = sortOrder;
			_sortField = sortField;
		}

		#region IComparer<CompareServiceAuditLogEntry> Members

		int IComparer<CompareServiceAuditLogEntry>.Compare(CompareServiceAuditLogEntry x, CompareServiceAuditLogEntry y)
		{
			switch (_sortField)
			{
			case SortField.LogTime:
			default:
				return CompareDateTime(x, y);

			case SortField.RedlineSize:
				return CompareRedlineSize(x, y);

			case SortField.SummarySize:
				return CompareSummarySize(x, y);

			case SortField.TotalExecutionTime:
				return CompareExecutionTime(x, y);

			case SortField.UserName:
				return CompareUserName(x, y);

			}
		}

		#endregion

		#region Private Comparison Methods

		private int CompareDateTime(CompareServiceAuditLogEntry x, CompareServiceAuditLogEntry y)
		{
			if (_sortOrder == ListSortDirection.Ascending)
			{
				if (x.LogTime > y.LogTime)
				{
					return 1;
				}
				if (x.LogTime < y.LogTime)
				{
					return -1;
				}
			}
			else	// Descending
			{
				if (x.LogTime < y.LogTime)
				{
					return 1;
				}
				if (x.LogTime > y.LogTime)
				{
					return -1;
				}
			}
			return 0;
		}

		private int CompareUserName(CompareServiceAuditLogEntry x, CompareServiceAuditLogEntry y)
		{
			if (_sortOrder == ListSortDirection.Ascending)
			{
				return x.UserName.CompareTo(y.UserName);
			}
			else
			{
				return y.UserName.CompareTo(x.UserName);
			}
		}

		private int CompareRedlineSize(CompareServiceAuditLogEntry x, CompareServiceAuditLogEntry y)
		{
			if (_sortOrder == ListSortDirection.Ascending)
			{
				if (x.RedlineSize > y.RedlineSize)
				{
					return 1;
				}
				if (x.RedlineSize < y.RedlineSize)
				{
					return -1;
				}
			}
			else
			{
				if (x.RedlineSize < y.RedlineSize)
				{
					return 1;
				}
				if (x.RedlineSize > y.RedlineSize)
				{
					return -1;
				}
			}
			return 0;
		}

		private int CompareSummarySize(CompareServiceAuditLogEntry x, CompareServiceAuditLogEntry y)
		{
			if (_sortOrder == ListSortDirection.Ascending)
			{
				if (x.SummarySize > y.SummarySize)
				{
					return 1;
				}
				if (x.SummarySize < y.SummarySize)
				{
					return -1;
				}
			}
			else
			{
				if (x.SummarySize < y.SummarySize)
				{
					return 1;
				}
				if (x.SummarySize > y.SummarySize)
				{
					return -1;
				}
			}
			return 0;
		}

		private int CompareExecutionTime(CompareServiceAuditLogEntry x, CompareServiceAuditLogEntry y)
		{
			if (_sortOrder == ListSortDirection.Ascending)
			{
				if (x.TotalExecutionTime > y.TotalExecutionTime)
				{
					return 1;
				}
				if (x.TotalExecutionTime < y.TotalExecutionTime)
				{
					return -1;
				}
			}
			else
			{
				if (x.TotalExecutionTime < y.TotalExecutionTime)
				{
					return 1;
				}
				if (x.TotalExecutionTime > y.TotalExecutionTime)
				{
					return -1;
				}
			}
			return 0;
		}

		#endregion
	}

	#endregion
}
