using System;
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
using System.IO;
using Workshare.Samples.BasicWebSample.CompareProxy;

public partial class _Default : System.Web.UI.Page
{

	#region Data Members

    private string sRenderSetPath = "App_Data/renderset";
    private string sTempDataPath = "App_Data/temp";
	private string sSessionBasePath;

	#endregion

	#region File Processing and Compare


	/// <summary>
	/// Saves the file to web-server H/D. Saves Original file on the root of session-based path,
	/// whereas we create an index-based directory for each modified file and its resultant files, e.g.
	/// session-based-path/0 for first modified file and session-based-path/1 for 2nd and so on.
	/// </summary>
	/// <param name="uploadedFile"></param>
	/// <param name="sFullMappedPath"></param>
	/// <returns></returns>
	private bool ProcessFile(HttpPostedFile uploadedFile, ref string sFullMappedPath)
	{
		if (uploadedFile.ContentLength == 0)
			return false;

		sFullMappedPath = Path.Combine(sSessionBasePath, Path.GetFileName(uploadedFile.FileName)).Replace('\\', '/');
		sFullMappedPath = Request.MapPath(sFullMappedPath);

		string sDirectoryPath = Path.GetDirectoryName(sFullMappedPath);
		if (!Directory.Exists(sDirectoryPath))
			Directory.CreateDirectory(sDirectoryPath);

		try
		{
			uploadedFile.SaveAs(sFullMappedPath);
			return true;
		}
		catch (Exception ex)
		{
			ShowMessage("Error: " + ex.Message);
			return false;
		}
	}


	/// <summary>
	/// Actually calls into Compare Service to perform comparison. Currently it used a hardcoded
	/// binding wsHttpBinding. After the call has returned successfully, it calls the results
	/// routine to display the results.
	/// </summary>
	/// <param name="sOriginalFile"></param>
	/// <param name="sModifiedFile"></param>
	/// <param name="sVirtualPath"></param>
	private void DoCompare(string sOriginalFile, string sModifiedFile, string sVirtualPath)
	{
		ResponseOptions responseOptions = ResponseOptions.Rtf;

		string password = (string) Session["Passw"];
		password = CodePassword(password);

		// Hardcoded wsHttpBinding. Host info is picked from config file
		ComparerClient cp = new ComparerClient("CompareWebServiceWCF");
		cp.ClientCredentials.Windows.ClientCredential.UserName = UserNameTextBox.Text;
		cp.ClientCredentials.Windows.ClientCredential.Password = password;
		cp.ClientCredentials.Windows.ClientCredential.Domain = RealmTextBox.Text;

		// Authenticate first.
		if (cp.Authenticate(RealmTextBox.Text, UserNameTextBox.Text, password))
		{
			byte[] original = File.ReadAllBytes(sOriginalFile);
			byte[] modified = File.ReadAllBytes(sModifiedFile);
			string sRenderingSet = RenderingSetDropDownList.SelectedValue;
			string sOptionSet = File.ReadAllText(Request.MapPath(Path.Combine(sRenderSetPath, sRenderingSet)));

		    ExecuteParams executeParams = new ExecuteParams()
		        {
		            CompareOptions = sOptionSet,
		            ResponseOption = responseOptions,
		            Original = original,
		            Modified = modified,
		            OriginalDocumentInfo = new DocumentInfo() {DocumentDescription = Path.GetFileName(sOriginalFile), DocumentSource = Path.GetFileName(sOriginalFile)},
		            ModifiedDocumentInfo = new DocumentInfo() {DocumentDescription = Path.GetFileName(sModifiedFile), DocumentSource = Path.GetFileName(sModifiedFile)},
		        };
			// Peform comparison
			CompareResults results = cp.ExecuteEx(executeParams);

			// Prepare and Display results.
			HandleResults(results, responseOptions, sOriginalFile, sModifiedFile, sRenderingSet, sVirtualPath);
		}
		else
		{
			ShowMessage("Authentication failed.");
		}
	}


	#endregion

	#region Event Handlers

	/// <summary>
	/// Its in essence a single page with showing different screens depending upon the state of
	/// the page. There are 3 screens, login, compare and results. SetupPageUI does the magic.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
			InitRenderingSetCombo();
			SetupPageUI();
		}
	}

	/// <summary>
	/// Authenticated the user on Compare Service and stores the password for later use.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void AuthenticateButton_Click(object sender, EventArgs e)
	{
		try
		{
			ShowMessage("");
			ComparerClient cp = new ComparerClient("CompareWebServiceWCF");

			cp.ClientCredentials.Windows.ClientCredential.UserName = UserNameTextBox.Text;
			cp.ClientCredentials.Windows.ClientCredential.Password = PasswordTextBox.Text;
			cp.ClientCredentials.Windows.ClientCredential.Domain = RealmTextBox.Text;

			if (cp.Authenticate(RealmTextBox.Text, UserNameTextBox.Text, PasswordTextBox.Text))
			{
				string sServiceVersion = cp.GetVersion();
				string sCompositorVersion = cp.GetCompositorVersion();

				string sVersionString = "Compare Service Version: " + sServiceVersion + " ";
				sVersionString += "Compositor Version: " + sCompositorVersion;

				WCSVersionLabel.Text = sVersionString;

				Session["sVersionString"] = sVersionString;
				Session["Authenticated"] = true;
				Session["Passw"] = CodePassword(PasswordTextBox.Text);

				SetupPageUI();
			}
			else
			{
				WCSVersionLabel.Text = "Authentication failed.";
				ShowMessage("Authentication failed.");
			}
		}
		catch (TimeoutException ex)
		{
			ShowMessage("The connection to Compare Service has timed out. The service may be down or unavailable for some other reason. \nDetailed description: " + ex.Message);
		}
		catch (Exception ex)
		{
			ShowMessage("Error: " + ex.Message);
		}
	}

	/// <summary>
	/// Prepares and starts the Comparison for the specified pair of files. This sample only supports
	/// comparing a single pair of files. First we process Original file and then Modified file.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void CompareButton_Click(object sender, EventArgs e)
	{
		try
		{
			// Reset the status message
			ShowMessage("");

			// Validate the data first.
			if (!IsDataValid())
			{
				return;
			}

			sSessionBasePath = Path.Combine(sTempDataPath, Session.SessionID);

			HttpFileCollection files = Request.Files;
			if (files.AllKeys.Length <= 1)
			{
				// Do nothing. We have already taken care of this case.
				return;
			}

			string sOriginalFileFullPath = string.Empty;
			string sModifiedFileFullPath = string.Empty;
			// Read original file first.
			bool succeeded = ProcessFile(files["OriginalFile"], ref sOriginalFileFullPath);
			if (succeeded)	// we must have original file
			{
				// Read modified file
				succeeded = ProcessFile(files["ModifiedFile"], ref sModifiedFileFullPath);

				// Perform comparison and show results.
				DoCompare(sOriginalFileFullPath, sModifiedFileFullPath, sSessionBasePath);

				Session["ShowResults"] = true;
				SetupPageUI();
			}
		}
		catch (TimeoutException ex)
		{
			ShowMessage("The connection to Compare Service has timed out. The service may be down or unavailable for some other reason. \nDetailed description: " + ex.Message);
		}
		catch (Exception ex)
		{
			ShowMessage("Error: " + ex.Message);
		}
	}

	/// <summary>
	/// Update Page UI to Compare again.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void StartOverButton_Click(object sender, EventArgs e)
	{
		Session["ShowResults"] = false;
		SetupPageUI();
	}

	#endregion

	#region Helper Functions

	/// <summary>
	/// A simple hash for the password, instead of storing it in plain text, in session.
	/// </summary>
	/// <param name="pass"></param>
	/// <returns></returns>
	private string CodePassword(string pass)
	{
		byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);

		for (int i = 0; i < bytes.Length; i++)
		{
			bytes[i] ^= 0x56;
		}

		return System.Text.Encoding.Unicode.GetString(bytes);
	}

	/// <summary>
	/// Checks what rendering sets are availble in the data folder, and populates the dropdown
	/// based on names of files present there.
	/// </summary>
	private void InitRenderingSetCombo()
	{
		// Render the combo box item based on a given path

		DirectoryInfo directoryInfo = new DirectoryInfo(Request.MapPath(sRenderSetPath));

		if (directoryInfo.Exists)
		{
			FileInfo[] fileInfoArr = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in fileInfoArr)
			{
				ListItem listItem = new ListItem();
				if (fileInfo.Extension != string.Empty)
					listItem.Text = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf(fileInfo.Extension));
				else
					listItem.Text = fileInfo.Name;
				listItem.Value = fileInfo.Name;
				RenderingSetDropDownList.Items.Add(listItem);
			}
		}
	}

	/// <summary>
	/// Reads variables from web.config file's appSettings section
	/// </summary>
	private void InitConfigVariables()
	{
		try
		{
			sRenderSetPath = ConfigurationSettings.AppSettings["renderset.path"];
			sTempDataPath = ConfigurationSettings.AppSettings["tempdata.path"];
		}
		catch
		{
		}
	}

	/// <summary>
	/// Verifies if at least one original file and one modified file is provided by the user.
	/// Files are recognized by their Keys in the collection.
	/// </summary>
	/// <returns></returns>
	private bool IsDataValid()
	{
		bool hasOriginalFile = false;
		bool hasModifiedFile = false;
		HttpFileCollection uploads = HttpContext.Current.Request.Files;
		foreach (string key in uploads.AllKeys)
		{
			HttpPostedFile file = uploads[key];
			// This name based on the html element
			if (key == "OriginalFile")
			{
				if (file.ContentLength > 0)
				{
					hasOriginalFile = true;
				}
			}
			else
			{
				// Need at least one modified file
				if (!hasModifiedFile)
				{
					if (file.ContentLength > 0)
						hasModifiedFile = true;
				}

				// shortcut to skip looping
				if (hasModifiedFile && hasOriginalFile)
					break;
			}
		}

		// Prepare error message
		string message = string.Empty;
		if (!hasOriginalFile)
		{
			message += "An original file is required.<br/>";
		}
		if (!hasModifiedFile)
		{
			message += "A modified file is required.<br/>";
		}

		// Show message
		if (!string.IsNullOrEmpty(message))
		{
			ShowMessage(message);
		}

		return hasOriginalFile && hasModifiedFile;
	}

	/// <summary>
	/// Shows the specified message on client side.
	/// </summary>
	/// <param name="message"></param>
	private void ShowMessage(string message)
	{
		// MesssageSpan is a clientside container.
		MessageSpan.Visible = true;
		MessageSpan.InnerHtml = message;
	}

	/// <summary>
	/// /// Calls into helper functions to add different rows for resultant files
	/// </summary>
	/// <param name="sModifiedFilePath"></param>
	/// <param name="sOutputFilePath"></param>
	/// <param name="sSummaryFilePath"></param>
	private void UpdateResultsTable(string sModifiedFilePath, string sOutputFilePath, string sSummaryFilePath)
	{
		this.ResultFiles.Rows.Add(AddHeaderRow(sModifiedFilePath));
		this.ResultFiles.Rows.Add(AddModifiedFileRow(sModifiedFilePath));
		this.ResultFiles.Rows.Add(AddOutputFileRow(sOutputFilePath));
		this.ResultFiles.Rows.Add(AddSummaryRow(sSummaryFilePath));
	}

	/// <summary>
	/// Adds header row for a set of files, includes named of Modified file in a bigger font
	/// </summary>
	/// <param name="sModifiedFilePath"></param>
	/// <returns></returns>
	private TableRow AddHeaderRow(string sModifiedFilePath)
	{
		string sModifiedFileName = Path.GetFileName(sModifiedFilePath);

		TableRow headerRow = new TableRow();

		TableCell headerCell = new TableCell();
		headerCell.Text = "<strong>" + sModifiedFileName + "</strong>";
		headerCell.ColumnSpan = 2;

		headerRow.Cells.Add(headerCell);

		return headerRow;
	}

	private TableRow AddModifiedFileRow(string sModifiedFilePath)
	{
		string sModifiedFileName = Path.GetFileName(sModifiedFilePath);
		TableRow modifiedFileRow = new TableRow();

		TableCell modifiedFileKeyCell = new TableCell();
		modifiedFileKeyCell.Text = "Modified File";
		modifiedFileKeyCell.HorizontalAlign = HorizontalAlign.Left;

		TableCell modifiedFileValueCell = new TableCell();
		modifiedFileValueCell.Text = "<a href='" + sModifiedFilePath + "' target='_blank'>" + sModifiedFileName + "</a>";
		modifiedFileValueCell.HorizontalAlign = HorizontalAlign.Left;

		modifiedFileRow.Cells.Add(modifiedFileKeyCell);
		modifiedFileRow.Cells.Add(modifiedFileValueCell);

		return modifiedFileRow;
	}

	private TableRow AddOutputFileRow(string sOutputFilePath)
	{
		string sOutputFileName = Path.GetFileName(sOutputFilePath);
		TableRow outputFileRow = new TableRow();

		TableCell outputFileKeyCell = new TableCell();
		outputFileKeyCell.Text = "Output File";
		outputFileKeyCell.HorizontalAlign = HorizontalAlign.Left;

		TableCell outputFileValueCell = new TableCell();

		if (sOutputFilePath != string.Empty)
		{
			outputFileValueCell.Text = "<a href='" + sOutputFilePath + "' target='_blank'>" + sOutputFileName + "</a>";
			outputFileValueCell.HorizontalAlign = HorizontalAlign.Left;
		}
		else
		{
			outputFileValueCell.Text = "&nbsp";
		}

		outputFileRow.Cells.Add(outputFileKeyCell);
		outputFileRow.Cells.Add(outputFileValueCell);

		return outputFileRow;
	}

	private TableRow AddSummaryRow(string sSummaryFilePath)
	{
		TableRow summaryFileRow = new TableRow();

		TableCell summaryFileKeyCell = new TableCell();
		summaryFileKeyCell.Text = "Summary";
		summaryFileKeyCell.HorizontalAlign = HorizontalAlign.Left;

		TableCell summaryFileValueCell = new TableCell();

		if (sSummaryFilePath != string.Empty)
		{
			summaryFileValueCell.Text = "<a href='" + sSummaryFilePath + "'> Summary.xml </a>";
			summaryFileValueCell.HorizontalAlign = HorizontalAlign.Left;
		}
		else
		{
			summaryFileValueCell.Text = "&nbsp;";
		}

		summaryFileRow.Cells.Add(summaryFileKeyCell);
		summaryFileRow.Cells.Add(summaryFileValueCell);

		return summaryFileRow;
	}

	/// <summary>
	/// Updates the page UI depending upon different flags from Session state.
	/// </summary>
	private void SetupPageUI()
	{
		bool authenticated;

		try
		{
			// Do we have authentication enabld in the first place?
			bool enableAuthentication = (bool) Boolean.Parse(ConfigurationSettings.AppSettings["enable.authentication"]);
			if (enableAuthentication)
			{
				authenticated = (bool) Session["Authenticated"];
			}
			else
			{
				authenticated = true;
			}
		}
		catch (Exception)
		{
			authenticated = false;
		}

		if (!authenticated)
		{
			// If not authenticated, show login screen.

			this.AuthenticateTable.Style.Add("display", "block");
			this.FilesUploadTable.Style.Add("display", "none");
			this.ResultPane1.Style.Add("display", "none");
			this.ResultFilesTable.Style.Add("display", "none");
		}
		else
		{
			// If the user is authenticated, we either show upload screen, or results screen
			// depending upon whats the value for Session["ShowResults"];

			bool showResults;
			try
			{
				showResults = (bool) Session["ShowResults"];
			}
			catch (Exception)
			{
				showResults = false;
			}

			try
			{
				this.WCSVersionLabel.Text = (string) Session["sVersionString"];
			}
			catch
			{
				this.WCSVersionLabel.Text = "Not Authenticated";
			}

			if (!showResults)
			{
				this.FilesUploadTable.Style.Add("display", "block");
				this.AuthenticateTable.Style.Add("display", "none");
				this.ResultPane1.Style.Add("display", "none");
				this.ResultFilesTable.Style.Add("display", "none");
			}
			else
			{
				this.ResultPane1.Style.Add("display", "block");
				this.ResultFilesTable.Style.Add("display", "block");
				this.FilesUploadTable.Style.Add("display", "none");
				this.AuthenticateTable.Style.Add("display", "none");
			}

		}
	}

	/// <summary>
	/// Prepare and stores the result objects for use by HandleFinalResults, based on output format
	/// selected by user, e.g. .rtf or .wdf etc.
	/// </summary>
	/// <param name="results"></param>
	/// <param name="responseOptions"></param>
	/// <param name="sOriginalFilePath"></param>
	/// <param name="sModifiedFilePath"></param>
	/// <param name="sRenderingSet"></param>
	/// <param name="sVirtualPath"></param>
	private void HandleResults(CompareResults results, ResponseOptions responseOptions, string sOriginalFilePath, string sModifiedFilePath, string sRenderingSet, string sVirtualPath)
	{
		string sFileName = string.Empty;
		string sOutputFileVirtualPath = string.Empty;
		string sSummaryFileVirtualPath = string.Empty;
		string sOriginalFileVirtualPath = string.Empty;
		string sModifiedFileVirtualPath = string.Empty;

		// Prepare the virtual path first.
		sVirtualPath = sVirtualPath.Replace('\\', '/');
		string sBasePath = Request.MapPath(sVirtualPath);

		// Prepare labels for Original File
		sFileName = Path.GetFileName(sOriginalFilePath);
		sOriginalFileVirtualPath = sVirtualPath.Replace('\\', '/');
		sOriginalFileVirtualPath = Path.Combine(sOriginalFileVirtualPath, sFileName);

        sOriginalFilePath = Encode(sOriginalFilePath);
		OriginalFilePathLabel.Text = "<a href='" + Encode(sOriginalFileVirtualPath) + "' target='_blank'>" + sFileName + "</a>";
		RenederingSetLabel.Text = sRenderingSet;

		// Prepare paths/labels for Output file
		if (responseOptions == ResponseOptions.Rtf)
		{
			if (results.Redline != null)
			{
				sFileName = "redline.rtf";
				sOutputFileVirtualPath = Path.Combine(sVirtualPath, sFileName);

				File.WriteAllBytes(Request.MapPath(sOutputFileVirtualPath), results.Redline);
			}
		}

		if (responseOptions == ResponseOptions.Xml ||
				responseOptions == ResponseOptions.RtfWithSummary ||
				responseOptions == ResponseOptions.WdfWithSummary)
		{
			if (results.Summary != null)
			{
				sFileName = "redline.xml";
				sSummaryFileVirtualPath = Path.Combine(sVirtualPath, sFileName);

				File.WriteAllText(Request.MapPath(sSummaryFileVirtualPath), results.Summary);
			}
		}

		// Prepare path/labels for Modified file
		if (sModifiedFilePath != string.Empty)
		{
			sFileName = Path.GetFileName(sModifiedFilePath);
			sModifiedFileVirtualPath = Path.Combine(sVirtualPath, sFileName);
            sModifiedFileVirtualPath = Encode(sModifiedFileVirtualPath);
		}

		if (sOutputFileVirtualPath != string.Empty)
			sOutputFileVirtualPath = Encode(sOutputFileVirtualPath);

		if (sSummaryFileVirtualPath != string.Empty)
            sSummaryFileVirtualPath = Encode(sSummaryFileVirtualPath);


		// Prepare and show the results table
		UpdateResultsTable(sModifiedFileVirtualPath, sOutputFileVirtualPath, sSummaryFileVirtualPath);

	}

    private string Encode(string path)
    {
        return Server.HtmlEncode(path.Replace('\\', '/'));
    }

    #endregion

}
