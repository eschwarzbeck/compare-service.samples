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
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Document.Services.Compare.Control;
using System.Web.Configuration;
using System.Drawing;
using Document.Services.Compare.Control.ComparerProxy;
using Microsoft.Win32;
using CompareResults = Document.Services.Compare.Control.CompareResults;
using ResponseOptions = Document.Services.Compare.Control.ResponseOptions;

namespace Workshare.Samples.AdvWebSample
{

	public partial class Upload : Page
	{
		// This page actually holds all the forms used on page. Default.aspx is just a frame
		// around this page. This page also contains the actual uploading/file processing and comparison
		// logic. Different sets of controls, for login/upload/progress/results are shown, depending upon
		// the state of the page. These sets of controls are placed in <div> tags, which are dynamically
		// shown/hidden from the code.

		#region Data Members

		private string _renderSetPath = "data/renderset";
		private string _tempDataPath = "data/temp";
		private string _host = "localhost";
		private int	   _port = 8080;
		private TransportProtocolEnum _protocol = TransportProtocolEnum.Http;
		private int	   _chunkSize = 1024;
		private string _sessionBasePath;
		private int	   _lastModifiedFileIndex = 0;
		private List<ResultantFiles> _resultantFiles = new List<ResultantFiles>();

		private string	_originalFileName = string.Empty;
		private string	_modifiedFileName = string.Empty;


		#endregion

		#region Event Handlers

		/// <summary>
		/// On form postback, first of all files are locally stored. This is not required by comparison call,
		/// as it expects a stream, but we need these files on disk for the results page, where user can click
		/// on a file link to save it. After storing files to disk, compare service is invoked using the interface DLL. 
		/// By using this DLL we are saved from the hassle of WCF configuration. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			InitConfigVariables();

            if (Session["BeenAuthenticated"] == null)
                Session["BeenAuthenticated"] = false;


			if (IsPostBack)
			{
				bool beenAuthenticated = (bool) Session["BeenAuthenticated"];

				if (beenAuthenticated)
				{
					// Need session based path to save files locally
					_sessionBasePath = Path.Combine(_tempDataPath, Session.SessionID);
					
					// Do the actual file processing and comparison stuff
					ProcessFilesAndDoComparison();
				}
			}
			else
			{
				Session["ShowResults"] = false;

				if (Session["BeenAuthenticated"] == null )
					Session["BeenAuthenticated"] = false;

				// Decide  what to show and update the page UI
				UpdatePageUI();

				InitRenderingSetCombo();
				InitResponseOptionsCombo();
			}
			
		}

		/// <summary>
		/// Fired by CompareService after sending each chunk of data. We use this event to calculate the
		/// progress of data transfer to the Compare Service. It is only for data transfer purposes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CompareService_OnDataSent(object sender, DataSentArgs e)
		{
			UploadInfo uploadInfo = Session["UploadInfo"] as UploadInfo;

			if (uploadInfo == null)
			{			
				return;
			}

			uploadInfo.IsReady = true;

			long soFar = e.BytesSent;
			long total = e.BytesTotal;
			int percentComplete = 0;

			if (total > 0)
			{
				percentComplete = (int)Math.Ceiling((double)soFar / total * 100);
			}

			uploadInfo.PercentComplete = percentComplete;
			string msg = "Chunking {0} - Number of bytes sent: {1} Bytes";

			if (e.IsOriginalFile)
			{
				msg = string.Format(msg, "Original file [ {0} ]", e.BytesSent);
				msg = string.Format(msg, _originalFileName);
			}
			else
			{
				msg = string.Format(msg, "Modified file [ {0} ]", e.BytesSent);
				msg = string.Format(msg, _modifiedFileName);
			}

			uploadInfo.Message = msg;
		}

		/// <summary>
		/// Fired by CompareService when data transfer is complete and actual comparison starts. We use
		/// this to update the user on the status of operation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CompareService_OnComparisonStarted(object sender, EventArgs e)
		{
			UploadInfo uploadInfo = Session["UploadInfo"] as UploadInfo;
			if (uploadInfo == null)
			{
				return;
			}

			uploadInfo.IsReady = true;
			uploadInfo.PercentComplete = 75;
			uploadInfo.Message = "Chunking complete. Comparison started ...";
		}

		/// <summary>
		/// Performs authentication using CompareService
		/// We store the credentials in Session for later use. We also update the UI here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void LoginButton_Click(object sender, EventArgs e)
		{
			try
			{
				ICompareService compareService = CreateAppropriateService();
				compareService.SetClientCredentials(UsernameTextBox.Text, PasswordTextBox.Text, DomainTextBox.Text);

				string serviceVersion;
				string compositorVersion;
				if (compareService.VerifyConnection(out serviceVersion, out compositorVersion))
				{
					// Store the gems for later use
					Session["BeenAuthenticated"] = true;
					Session["Passw"] = CodePassword(PasswordTextBox.Text);
					Session["UserName"] = UsernameTextBox.Text;
					Session["Domain"] = DomainTextBox.Text;

					string versionString = "Service: {0} - Compositor: {1}";
					versionString = string.Format(versionString, serviceVersion, compositorVersion);
					CompositorVersionLabel.Text = versionString;

					ToggleUploadButtonAndStatusPane(true);
					ShowMessage(GenerateScriptID("auth"), "success", " Authentication succeeded.");

					UpdatePageUI();
				}
				else
				{
					Session["BeenAuthenticated"] = false;

					string message = "Authentication failed for user: {0}";
					message = string.Format(message, UsernameTextBox.Text);
					ShowMessage(GenerateScriptID("auth"), "error", message);
				}
			}
			catch (System.Security.SecurityException ex)
			{
				string message = " Credentials provided are either empty or invalid. " + ex.Message;
				ShowMessage(GenerateScriptID("auth"), "error", message);
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				string message = " Either Compare Service is not running or host/port address is invalid. " + ex.Message;
				ShowMessage(GenerateScriptID("auth"), "error", message);
			}
			catch (TimeoutException ex)
			{
				string message = " The connection between server and client was lost because of a timeout. " + ex.Message;
				ShowMessage(GenerateScriptID("auth"), "error", message);
			}
			catch (System.ServiceModel.ServerTooBusyException ex)
			{
				string message = " Too many simultaneous requests. Try again after a while. " + ex.Message;
				ShowMessage(GenerateScriptID("auth"), "error", message);
			}
			catch (System.ServiceModel.FaultException<string> ex)
			{
				string message = " An error occurred while authenticating. " + ex.Detail;
				ShowMessage(GenerateScriptID("auth"), "error", message);
			}
			catch (Exception ex)
			{
				string message = " An error occurred while authenticating. " + ex.Message;
				ShowMessage(GenerateScriptID("auth"), "error", message);
			}
		}


		#endregion

		#region File handling and Comparison

		/// <summary>
		/// Verifies that at least one original file and one modified file is provided by the user.
		/// Files are recognized by their Keys in the collection.
		/// </summary>
		/// <returns></returns>
		private bool IsDataValid()
		{
			bool hasOriginalFile = false;
			bool hasModifiedFile = false;
			HttpFileCollection uploads = Request.Files;

			foreach (string key in uploads.AllKeys)
			{
				HttpPostedFile file = uploads[key];
				// This name based on the html element
				if (file.ContentLength > 0)
				{
					if (key == "OriginalFile")
					{				
						hasOriginalFile = true;					
					}
					else
					{
						hasModifiedFile = true;
					}
				}

				// shortcut to skip looping
				if (hasModifiedFile && hasOriginalFile)
				{
					break;
				}
			}

			// Prepare error message
			string message = string.Empty;
			if (!hasOriginalFile)
			{
				message += "The original file is missing.<br/>";
			}
			else if (!hasModifiedFile)
			{
				message += "A modified file is missing.<br/>";
			}

			// Show message
			if (!string.IsNullOrEmpty(message))
			{
				string message1 = string.Format("Comparison failed. {0}", message);
				ShowMessage(GenerateScriptID("progress"), "error", message1);
				ToggleUploadButtonAndStatusPane(true);
			}

			return hasOriginalFile && hasModifiedFile;
		}

		/// <summary>
		/// Makes calls to process files and do Comparison.
		/// The files are placed in Files collection in the order of appearance on the HTML form.
		/// First of all Original file is saved. Then for each modified file, file is saved and compared
		/// against the original file. Modified files can be more than one. 
		/// </summary>
		private void ProcessFilesAndDoComparison()
		{
			UploadInfo uploadInfo = null;
			try
			{
				// Validate the data.
				if (!IsDataValid())
				{
					UpdatePageUI();
					return;
				}

				// Upload object used by Default.aspx to report progress to the user
				uploadInfo = Session["UploadInfo"] as UploadInfo;

				uploadInfo.IsReady = false;
				uploadInfo.UsePercentComplete = false;

				// Read output format
				ResponseOptions responseOptions = (ResponseOptions)Enum.Parse(typeof(ResponseOptions), ResponseOptionsDropDownList.SelectedValue);
				
				// Read rendering set from file
				string renderingSet = RenderingSetDropDownList.SelectedValue;
				string optionSet = File.ReadAllText(Request.MapPath(Path.Combine(_renderSetPath, renderingSet)));
				string originalFileVirtualPath = string.Empty;
				double originalFileSizeInKB = 0;

				_resultantFiles.Clear();

				// Process original file first.
				HttpPostedFile originalPostedFile = Request.Files["OriginalFile"];
				if (originalPostedFile != null)
				{
					originalFileVirtualPath = ProcessFile(originalPostedFile, true, ref uploadInfo);
					_originalFileName = Path.GetFileName(originalFileVirtualPath);
					originalFileSizeInKB = (double)originalPostedFile.ContentLength / 1024;
				}

				foreach (string postedFile in Request.Files.Keys)
				{
					if (postedFile == "OriginalFile")
					{
						// We have already had it.
						continue;
					}
					// Now the modified ones.
					HttpPostedFile modifiedPostedFile = Request.Files[postedFile];
					if (modifiedPostedFile.ContentLength <= 0)
						continue;

					string modifiedFileVirtualPath = ProcessFile(modifiedPostedFile, false, ref uploadInfo);
					_modifiedFileName = Path.GetFileName(modifiedFileVirtualPath);
					double modifiedFileSizeInKB = (double)modifiedPostedFile.ContentLength / 1024;

					// Update progress
					uploadInfo.IsReady = true;
					uploadInfo.UsePercentComplete = true;
					uploadInfo.PercentComplete = 3;
					uploadInfo.Message = "Starting comparison ...";

					// Perform comparison
					CompareResults results = DoComparison(originalPostedFile, modifiedPostedFile, optionSet, responseOptions, ref uploadInfo);

					if (results != null)
					{
						ShowMessage(GenerateScriptID("comp"), "success", " Comparison completed successfully. Displaying results.");

						// We save results for each modified file during the process, and sum up these
						// intermediate results after we are done with comparing all the modified files
						// provided by user. 
						HandleIntermediateResults(results, responseOptions, modifiedFileVirtualPath, modifiedFileSizeInKB);
						_lastModifiedFileIndex++;

						ShowMessage(GenerateScriptID("comp"), "success", " Comparison completed successfully.");
					}
					else
					{
						ShowMessage(GenerateScriptID("comp"), "error", " Comparison failed. One possible reason is failure of document format conversion.");
					}
				}

				// Prepare final result structs
				ComparisonResult comparisonResult = HandleFinalResults(originalFileVirtualPath, originalFileSizeInKB, renderingSet);
				
				// Show the results.
				DisplayResults(comparisonResult);
			}
			catch (System.Security.SecurityException ex)
			{
				if (uploadInfo != null)
				{
					uploadInfo.IsReady = false;
				}

				string message = " Credentials provided are either empty or invalid. " + ex.Message;
				ShowMessage(GenerateScriptID("comp"), "error", message);
				ToggleUploadButtonAndStatusPane(true);
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				if (uploadInfo != null)
				{
					uploadInfo.IsReady = false;
				}

				string message = " Either Compare Service is not running or host/port address is invalid. " + ex.Message;
				ShowMessage(GenerateScriptID("comp"), "error", message);
				ToggleUploadButtonAndStatusPane(true);
			}
			catch (TimeoutException ex)
			{
				if (uploadInfo != null)
				{
					uploadInfo.IsReady = false;
				}

				string message = " The connection between server and client is lost because of a timeout. " + ex.Message;
				ShowMessage(GenerateScriptID("comp"), "error", message);
				ToggleUploadButtonAndStatusPane(true);
			}
			catch (InvalidDataException ex)
			{
				if (uploadInfo != null)
				{
					uploadInfo.IsReady = false;
				}

				string message = " There was an error processing the data. This might be because a document could not be converted. " + ex.Message;
				ShowMessage(GenerateScriptID("comp"), "error", message);
				ToggleUploadButtonAndStatusPane(true);
			}
			catch (System.ServiceModel.ServerTooBusyException ex)
			{
				if (uploadInfo != null)
				{
					uploadInfo.IsReady = false;
				}

				string message = " Too many simultaneous requests. Try again after a while. " + ex.Message;
				ShowMessage(GenerateScriptID("comp"), "error", message);
				ToggleUploadButtonAndStatusPane(true);
			}
			catch (System.ServiceModel.FaultException<string> ex)
			{
				if (uploadInfo != null)
				{
					uploadInfo.IsReady = false;
				}

				string message = " An error occurred while comparing documents. " + ex.Detail;
				ShowMessage(GenerateScriptID("comp"), "error", message);
				ToggleUploadButtonAndStatusPane(true);
			}
			catch (Exception ex)
			{
				if (uploadInfo != null)
				{
					uploadInfo.IsReady = false;
				}
				
				string message = " An error occurred while comparing documents. " + ex.Message;
				ShowMessage(GenerateScriptID("comp"), "error", message);
				ToggleUploadButtonAndStatusPane(true);
			}
		}

		/// <summary>
		/// Saves the file to web-server disk. Saves Original file on the root of session-based path,
		/// whereas we create an index-based subdirectory for each modified file and its resultant files, e.g.
		/// session-based-path/0 for first modified file and session-based-path/1 for second and so on.
		/// </summary>
		/// <param name="postedFile"></param>
		/// <param name="originalFile"></param>
		/// <param name="uploadInfo"></param>
		/// <returns></returns>
		private string ProcessFile(HttpPostedFile postedFile, bool originalFile, ref UploadInfo uploadInfo)
		{
			// Do some basic validation
			if (postedFile == null || postedFile.ContentLength == 0)
			{
				ShowMessage(GenerateScriptID("progress"), "error", "There was a problem with the file.");
				uploadInfo.IsReady = false;
				return string.Empty;	
			}			

			string virtualFilePath = string.Empty;
			
			//  build the local path
			if (originalFile)
			{
				virtualFilePath = Path.Combine(_sessionBasePath, Path.GetFileName(postedFile.FileName));
			}
			else
			{
				virtualFilePath = Path.Combine(_sessionBasePath, _lastModifiedFileIndex.ToString());
				virtualFilePath = Path.Combine(virtualFilePath, Path.GetFileName(postedFile.FileName));
			}
			virtualFilePath = virtualFilePath.Replace('\\', '/');

			string filepath = Server.MapPath(virtualFilePath);

			//  build the structure and stuff it into session
			uploadInfo.ContentLength = postedFile.ContentLength;
			uploadInfo.FileName = Path.GetFileName(filepath);
			uploadInfo.UploadedLength = 0;

			// Let the polling process know that we are done initializing ...
			uploadInfo.IsReady = true;

			int bufferSize = 1024;
			byte[] buffer = new byte[bufferSize];

			string directoryPath = Path.GetDirectoryName(filepath);
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
			if (File.Exists(filepath))
			{
				File.Delete(filepath);
			}
			// Write the bytes to disk
			using (FileStream fs = new FileStream(filepath, FileMode.Create))
			{
				// As long as we haven't written everything ...
				while (uploadInfo.UploadedLength < uploadInfo.ContentLength)
				{
					// Fill the buffer from the input stream
					int bytes = postedFile.InputStream.Read(buffer, 0, bufferSize);
					// Write the bytes to the file stream
					fs.Write(buffer, 0, bytes);
					// Update the number the webservice is polling on
					uploadInfo.UploadedLength += bytes;

					// Its not a good idea to move Flush in the loop but in case of very large files,
					// if Flush-ing is deffered untill end, it occupies a lot of memory, and takes quite 
					// long to Flush-out the complete buffer, hence giving an un-explainable delay when the
					// progress bar is showing 100%. So user is unable to understand whats taking so long.
					// So we keep on flushing. This will cause frequent H/D hits, and can increase the overall
					// time, but will give a comparatively better user-experience.
					fs.Flush();
				}

				
			}

			// Let the parent page know we have processed the upload
			string message = "{0} has been uploaded successfully.";
			message = string.Format(message, Path.GetFileName(filepath));
			ShowMessage(GenerateScriptID("progress"), "success", message);

			uploadInfo.IsReady = false;
			return virtualFilePath;	
		}

		/// <summary>
		/// Actually calls into Control.dll to perform comparison. This call fires DataSent event and 
		/// ComparisonStarted event. We need to set the client credentials for the call before it's executed.
		/// </summary>
		/// <param name="originalFile"></param>
		/// <param name="modifiedFile"></param>
		/// <param name="optionSet"></param>
		/// <param name="responseOptions"></param>
		/// <param name="uploadInfo"></param>
		/// <returns></returns>
		private CompareResults DoComparison(HttpPostedFile originalPostedFile, HttpPostedFile modifiedPostedFile, string optionSet, ResponseOptions responseOptions, ref UploadInfo uploadInfo)
		{
			ICompareService compareService = CreateAppropriateService();
			compareService.ComparisonStarted += new EventHandler(CompareService_OnComparisonStarted);
			compareService.DataSent += new EventHandler<DataSentArgs>(CompareService_OnDataSent);

			uploadInfo.IsReady = true;
			uploadInfo.PercentComplete = 10;
			uploadInfo.Message = "Authenticating request ...";

			string username = (string)Session["UserName"];
			string domain = (string)Session["Domain"];
			string password = (string)Session["Passw"];
			password = CodePassword(password);
			compareService.SetClientCredentials(username, password, domain);

			string serviceVersion;
			string compositorVersion;

			if (!compareService.VerifyConnection(out serviceVersion, out compositorVersion))
			{
				uploadInfo.IsReady = false;
				return null;
			}

			uploadInfo.PercentComplete = 15;
			uploadInfo.Message = "Authentication was successful ... reading files";

			uploadInfo.PercentComplete = 20;
			uploadInfo.Message = "Chunking files ...";

			compareService.CompareOptions = optionSet;
			compareService.ComparisonOutput = responseOptions;
			compareService.UseChunking = true;
			compareService.ChunkSize = _chunkSize;

		    var originalFile = originalPostedFile.InputStream;
		    var modifiedFile = modifiedPostedFile.InputStream;

			originalFile.Seek(0, SeekOrigin.Begin);
			modifiedFile.Seek(0, SeekOrigin.Begin);

			// As _lastModifiedFileIndex is zero-based, so increment it befor displaying
			uploadInfo.PairInfo = "Comparing File Pair " + (_lastModifiedFileIndex + 1).ToString();

			CompareResults results = compareService.CompareEx(originalFile, modifiedFile, CreateDocumentInfo(originalPostedFile.FileName), CreateDocumentInfo(modifiedPostedFile.FileName));

			uploadInfo.PercentComplete = 100;
			uploadInfo.Message = "Comparison completed ...";
			uploadInfo.IsReady = false;

			return results;
		}

	    private DocumentInfo CreateDocumentInfo(string fileName)
	    {
	        return new DocumentInfo() {DocumentSource = fileName, DocumentDescription = fileName};
	    }

	    #endregion

		#region Show Results 
		
		// Results are shown in an ASP Table, which is dynamically populated based on the number
		// of modified files provided. For each set of result files, which includes the modified file itself, 
		// output file (.rtf/.wdf etc) and summary file (.xml), a separate row is created in the table.

		/// <summary>
		/// Prepares and stores the result objects for use by HandleFinalResults, based on output format
		/// selected by user, e.g. .rtf or .wdf etc.
		/// </summary>
		/// <param name="results"></param>
		/// <param name="responseOptions"></param>
		/// <param name="modifiedFileVirtualPath"></param>
		/// <param name="modifiedFileSizeInKB"></param>
		private void HandleIntermediateResults(CompareResults results, ResponseOptions responseOptions, string modifiedFileVirtualPath, double modifiedFileSizeInKB)
		{
			string outputFileVirtualPath = string.Empty;
			string summaryFileVirtualPath = string.Empty;

			double outputFileSizeInKB = 0;
			double summaryFileSizeInKB = 0;

			if (results.Redline != null)
			{
				outputFileVirtualPath = Path.GetDirectoryName(modifiedFileVirtualPath);
				outputFileSizeInKB = (double)results.Redline.Length / 1024;
				switch (responseOptions)
				{
					case ResponseOptions.Rtf:
					case ResponseOptions.RtfWithSummary:
						outputFileVirtualPath = Path.Combine(outputFileVirtualPath, "redline.rtf");
						break;

					case ResponseOptions.Wdf:
					case ResponseOptions.WdfWithSummary:
						outputFileVirtualPath = Path.Combine(outputFileVirtualPath, "redline.wdf");
						break;

					case ResponseOptions.Doc:
					case ResponseOptions.DocWithSummary:
						outputFileVirtualPath = Path.Combine(outputFileVirtualPath, "redline.doc");
						break;
					case ResponseOptions.DocX:
					case ResponseOptions.DocXWithSummary:
						outputFileVirtualPath = Path.Combine(outputFileVirtualPath, "redline.docx");
						break;
					case ResponseOptions.Pdf:
					case ResponseOptions.PdfWithSummary:
						outputFileVirtualPath = Path.Combine(outputFileVirtualPath, "redline.pdf");
						break;
				}

				string filePhysicalPath = Server.MapPath(outputFileVirtualPath);
				if (File.Exists(filePhysicalPath))
				{
					File.Delete(filePhysicalPath);
				}

				File.WriteAllBytes(filePhysicalPath, results.Redline);
			}

			if ((responseOptions & ResponseOptions.Xml) == ResponseOptions.Xml)
			{
				if (results.ChangeSummary != null)
				{
					summaryFileVirtualPath = Path.GetDirectoryName(modifiedFileVirtualPath);
					summaryFileVirtualPath = Path.Combine(summaryFileVirtualPath, "redline.xml");
					summaryFileSizeInKB = (double)results.ChangeSummary.Length / 1024;

					string filePhysicalPath = Server.MapPath(summaryFileVirtualPath);
					if (File.Exists(filePhysicalPath))
					{
						File.Delete(filePhysicalPath);
					}

					File.WriteAllText(filePhysicalPath, results.ChangeSummary);
				}
			}

			modifiedFileVirtualPath = Server.HtmlEncode(modifiedFileVirtualPath);
			outputFileVirtualPath = Server.HtmlEncode(outputFileVirtualPath);
			summaryFileVirtualPath = Server.HtmlEncode(summaryFileVirtualPath);

			ResultantFiles resultantFiles = new ResultantFiles(modifiedFileVirtualPath, outputFileVirtualPath, summaryFileVirtualPath,
											modifiedFileSizeInKB, outputFileSizeInKB, summaryFileSizeInKB);
			
			_resultantFiles.Add(resultantFiles);
		}

		/// <summary>
		/// Takes the intermediate results, appends the header information that includes Original file name,
		/// size and the rendering set, and returns the finalized result object to be used by 
		/// DisplayResults.
		/// </summary>
		/// <param name="originalFileVirtualPath"></param>
		/// <param name="originalFileSizeInKB"></param>
		/// <param name="renderingSet"></param>
		/// <returns></returns>
		private ComparisonResult HandleFinalResults(string originalFileVirtualPath, double originalFileSizeInKB, string renderingSet)
		{
			ComparisonResult comparisonResult = new ComparisonResult(originalFileVirtualPath, renderingSet, originalFileSizeInKB);
			comparisonResult._resultantFiles = _resultantFiles.ToList();
			return comparisonResult;
		}		
		
		/// <summary>
		/// Prepares results header pane and creates hyperlinks etc. For each set of resultant files, it calls
		/// into UpdateResultsTable to add the set of rows.
		/// </summary>
		/// <param name="comparisonResult"></param>
		private void DisplayResults(ComparisonResult comparisonResult)
		{
			Session["ShowResults"] = true;
			UpdatePageUI();

			// Prepare hyperlink for Original file
			string fileName = Path.GetFileName(comparisonResult._originalFile);
			string fileSize = string.Format("({0} KB)", Math.Round(comparisonResult._originalFileSizeInKB, 2));
			OriginalFilePathLabel.Text = "<a href='" + comparisonResult._originalFile + "'>" + fileName + "</a> " + fileSize ;

			// Remove the extension from rendering-set name, if present
			RenderingSetLabel.Text = Path.GetFileNameWithoutExtension(comparisonResult._renderingSet);

			ResultFiles.Rows.Clear();
			foreach (ResultantFiles files in comparisonResult._resultantFiles)
			{
				// Actually add the rows to results table
				UpdateResultsTable(files);
			}

			// Hide Upload button & progress bar
			ToggleUploadButtonAndStatusPane(false);

			// Resize iFrame
			ResizeFrame();
		}

		/// <summary>
		/// Calls into helper functions to add different rows for a resultant files set
		/// </summary>
		/// <param name="files"></param>
		private void UpdateResultsTable(ResultantFiles files)
		{
			ResultFiles.Rows.Add(CreateHeaderRow(files._modifiedFile));
			ResultFiles.Rows.Add(CreateModifiedFileRow(files._modifiedFile, files._modifiedFileSizeInKB));
			ResultFiles.Rows.Add(CreateOutputFileRow(files._outputFile, files._outputFileSizeInKB));
			ResultFiles.Rows.Add(CreateSummaryRow(files._summaryFile, files._summaryFileSizeInKB));
		}

		/// <summary>
		/// Creates a header row for a set of files, includes name of Modified file in a bigger font
		/// </summary>
		/// <param name="sModifiedFilePath"></param>
		/// <returns></returns>
		private TableRow CreateHeaderRow(string modifiedFilePath)
		{
			string modifiedFileName = Path.GetFileName(modifiedFilePath);

			TableRow headerRow = new TableRow();

			TableCell headerCell = new TableCell();
			headerCell.Text = "<strong>" + modifiedFileName + "</strong>";
			headerCell.ColumnSpan = 2;
			headerCell.BackColor = Color.FromArgb(230, 233, 235);
			headerCell.Height = 20;
			headerRow.Cells.Add(headerCell);

			return headerRow;
		}

		private TableRow CreateModifiedFileRow(string modifiedFilePath, double sizeInKB)
		{
			string modifiedFileName = Path.GetFileName(modifiedFilePath);
			TableRow modifiedFileRow = new TableRow();

			TableCell modifiedFileKeyCell = new TableCell();
			modifiedFileKeyCell.Text = "Modified file:";
			modifiedFileKeyCell.HorizontalAlign = HorizontalAlign.Left;
			modifiedFileKeyCell.Width = 150;
			modifiedFileKeyCell.Height = 20;

			TableCell modifiedFileValueCell = new TableCell();
			string fileSize = string.Format("({0} KB)", Math.Round(sizeInKB, 2));
			modifiedFileValueCell.Text = "<a href='" + modifiedFilePath + "'>" + modifiedFileName + "</a> " + fileSize;
			modifiedFileValueCell.HorizontalAlign = HorizontalAlign.Left;

			modifiedFileRow.Cells.Add(modifiedFileKeyCell);
			modifiedFileRow.Cells.Add(modifiedFileValueCell);

			return modifiedFileRow;
		}

		private TableRow CreateOutputFileRow(string outputFilePath, double sizeInKB)
		{
			string outputFileName = Path.GetFileName(outputFilePath);
			TableRow outputFileRow = new TableRow();

			TableCell outputFileKeyCell = new TableCell();
			outputFileKeyCell.Text = "Output file:";
			outputFileKeyCell.HorizontalAlign = HorizontalAlign.Left;
			outputFileKeyCell.Height = 20;

			TableCell outputFileValueCell = new TableCell();

			if (!string.IsNullOrEmpty(outputFilePath))
			{
				string fileSize = string.Format("({0} KB)", Math.Round(sizeInKB, 2));
				outputFileValueCell.Text = "<a href='" + outputFilePath + "?1'>" + outputFileName + "</a> " + fileSize;
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

		private TableRow CreateSummaryRow(string summaryFilePath, double sizeInKB)
		{
			TableRow summaryFileRow = new TableRow();

			TableCell summaryFileKeyCell = new TableCell();
			summaryFileKeyCell.Text = "Comparison Summary:";
			summaryFileKeyCell.HorizontalAlign = HorizontalAlign.Left;
			summaryFileKeyCell.Height = 20;

			TableCell summaryFileValueCell = new TableCell();

			if (!string.IsNullOrEmpty(summaryFilePath) )
			{
				string fileSize = string.Format("({0} KB)", Math.Round(sizeInKB, 2));
				summaryFileValueCell.Text = "<a href='" + summaryFilePath + "' target='_blank'> Summary.xml </a> " + fileSize;
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

		#endregion

		#region Helper Functions

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
		/// Populates output formats dropdown list. We are using different strings for values, so that
		/// it's easier to parse them into Enum values later on.
		/// </summary>
		private void InitResponseOptionsCombo()
		{
			ResponseOptionsDropDownList.Items.Add(new ListItem("RTF", Enum.GetName( typeof(ResponseOptions), ResponseOptions.Rtf)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("RTF With Summary", Enum.GetName(typeof(ResponseOptions), ResponseOptions.RtfWithSummary)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("DOC", Enum.GetName(typeof(ResponseOptions), ResponseOptions.Doc)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("DOC With Summary", Enum.GetName(typeof(ResponseOptions), ResponseOptions.DocWithSummary)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("DOCX", Enum.GetName(typeof(ResponseOptions), ResponseOptions.DocX)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("DOCX With Summary", Enum.GetName(typeof(ResponseOptions), ResponseOptions.DocXWithSummary)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("PDF", Enum.GetName(typeof(ResponseOptions), ResponseOptions.Pdf)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("PDF With Summary", Enum.GetName(typeof(ResponseOptions), ResponseOptions.PdfWithSummary)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("WDF", Enum.GetName(typeof(ResponseOptions), ResponseOptions.Wdf)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("WDF With Summary", Enum.GetName(typeof(ResponseOptions), ResponseOptions.WdfWithSummary)));
			ResponseOptionsDropDownList.Items.Add(new ListItem("XML", Enum.GetName(typeof(ResponseOptions), ResponseOptions.Xml)));
			

			ResponseOptionsDropDownList.Items[0].Selected = true;
			ResponseOptionsDropDownList.Width = 200;
		}

		/// <summary>
		/// Checks what rendering sets are available in the data folder, and populates the dropdown 
		/// based on names of files present there.
		/// </summary>
		private void InitRenderingSetCombo()
		{
			// Render the combo box item based on a given path

			DirectoryInfo directoryInfo = new DirectoryInfo(Request.MapPath(_renderSetPath));

			if (directoryInfo.Exists)
			{
				FileInfo[] fileInfoArr = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in fileInfoArr)
				{
					ListItem listItem = new ListItem();
					listItem.Text = Path.GetFileNameWithoutExtension(fileInfo.Name);
					listItem.Value = fileInfo.Name;
					RenderingSetDropDownList.Items.Add(listItem);
				}
			}

			RenderingSetDropDownList.Width = 200;
		}

		/// <summary>
		/// Reads build version from registry
		/// </summary>
		/// <returns></returns>
		private string GetVersionString()
		{
			try
			{
				RegistryKey uninstallKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{E5E5C159-0A74-4FBF-83E7-491F376360B4}");
				if (uninstallKey != null)
				{
					string versionString = (string)uninstallKey.GetValue("DisplayVersion");
					return versionString;
				}
			}
			catch
			{
			}

			return string.Empty;
		}

		/// <summary>
		/// Updates the page UI depending upon different flags. 
		/// </summary>
		private void UpdatePageUI()
		{
			bool beenAuthenticated = (bool) Session["BeenAuthenticated"];

            if (Session["ShowResults"] == null)
                Session["ShowResults"] = false;


			// If the user is authenticated, we either show upload screen, or results screen
			// depending upon whats the value for Session["ShowResults"];
			if (beenAuthenticated)
			{
				bool showResults = (bool) Session["ShowResults"];
				if (showResults)
				{
					ResultsDiv.Style.Add("display", "block");
					LoginDiv.Style.Add("display", "none");
					UploadFilesDiv.Style.Add("display", "none");

					Session["ShowResults"] = false;
				}
				else
				{
					UploadFilesDiv.Style.Add("display", "block");
					ResultsDiv.Style.Add("display", "none");
					LoginDiv.Style.Add("display", "none");

					ToggleUploadButtonAndStatusPane(true);
				}
			}
			else
			{
				// If not authenticated, show login screen.
				LoginDiv.Style.Add("display", "block");
				UploadFilesDiv.Style.Add("display", "none");
				ResultsDiv.Style.Add("display", "none");

				ToggleUploadButtonAndStatusPane(false);

			}
		}

		/// <summary>
		/// Reads variables from web.config file's appSettings section 
		/// Reads httpRuntime section for maxRequestLength
		/// Reads registry for version string.
		/// </summary>
		private void InitConfigVariables()
		{
			try
			{
				_renderSetPath = ConfigurationSettings.AppSettings["renderset.path"];
			}
			catch
			{
				_renderSetPath = "data/renderset";
			}
			try
			{
				_tempDataPath = ConfigurationSettings.AppSettings["tempdata.path"];
			}
			catch
			{
				_tempDataPath = "data/temp";
			}
			try
			{
				_host = ConfigurationSettings.AppSettings["cs.host"];
			}
			catch
			{
				_host = "localhost";
			}
			try
			{
				_port = Int32.Parse(ConfigurationSettings.AppSettings["cs.port"]);
			}
			catch
			{
				_port = 8080;
			}
			try
			{
				_chunkSize = Int32.Parse(ConfigurationSettings.AppSettings["chunk.size"]);
			}
			catch
			{
				_chunkSize = 1024;
			}
			try
			{
				string transportProtocol = ConfigurationSettings.AppSettings["transport.protocol"];
				_protocol = (TransportProtocolEnum)Enum.Parse(typeof(TransportProtocolEnum), transportProtocol, true);
			}
			catch
			{
				_protocol = TransportProtocolEnum.Http;			
			}

			try
			{
				// Update maxRequestLength in the message to user.
				Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
				HttpRuntimeSection configSection = config.GetSection("system.web/httpRuntime") as HttpRuntimeSection;

				// MaxRequestLength returns KB. Convert that to MB.
				double nMaxContentLength = (double)configSection.MaxRequestLength / 1024;

				string message = "Please note: The combined size limit for all files being compared is {0} MB.";
				message = string.Format(message, Math.Round(nMaxContentLength, 2));

				FileSizeWarningLabel.Text = message;
			}
			catch(Exception ex)
			{
				ShowMessage(GenerateScriptID("configSectionError"), "error", ex.Message);
			}


			// Update version
			VersionLabel.Text = GetVersionString();
		}

		/// <summary>
		/// Create and returns appropriate service object based upon protocol selected in config file
		/// </summary>
		/// <returns></returns>
		private ICompareService CreateAppropriateService()
		{
			var serviceUrl = ConfigurationManager.AppSettings["url.address"];
			var binding =  ConfigurationManager.AppSettings["transport.binding"];
			var security =  ConfigurationManager.AppSettings["transport.security"];
			var transport =  ConfigurationManager.AppSettings["transport.clientsecurity"];
			var message =  ConfigurationManager.AppSettings["message.clientsecurity"];
			switch (_protocol)
			{
				default:
				case TransportProtocolEnum.Http:
					return CompareService.CreateHttpService(_host, _port, serviceUrl, binding, security, transport, message );

				case TransportProtocolEnum.NamedPipe:
					return CompareService.CreateNamedPipeService();

				case TransportProtocolEnum.Tcp:
					return CompareService.CreateTcpService(_host, _port);
			}
		}

		/// <summary>
		/// A simple hash of the password, instead of storing it plain in the session.
		/// </summary>
		/// <param name="pass"></param>
		/// <returns></returns>
		private string CodePassword(string pass)
		{
			if (string.IsNullOrEmpty(pass))
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

		#endregion

		#region Client-side Scriptlets

		// Functions used to call into clientside script, mostly to perform Page UI
		// updates e.g. toggling different screens and resizing iFrame

		private void ShowMessage(string id, string type, string message)
		{
			message = message.Replace(Environment.NewLine, " ");

			ToggleStatusPane(true);

			string js = "window.parent.onComplete('{0}', '{1}');";
			js = string.Format(js, type, message);
			ExecuteScript(id, js);
		}

		private void ToggleUploadButtonAndStatusPane(bool show)
		{
			string js = "window.parent.toggleUploadButtonAndStatusPane({0});";
			js = string.Format(js, show ? "true" : "false");
			ExecuteScript(GenerateScriptID("toggle"), js);
		}

		private void ToggleStatusPane(bool show)
		{
			string script = "window.parent.toggleStatusPane({0});";
			script = string.Format(script, show ? "true" : "false");
			ExecuteScript(GenerateScriptID("statuspane"), script);
		}

		private void ResizeFrame()
		{
			string script = "window.parent.resizeFrame();";
			ExecuteScript(GenerateScriptID("resize"), script);
		}

		private void ExecuteScript(string id, string script)
		{
			ScriptManager.RegisterStartupScript(this, typeof(Upload), id, script, true);
		}

		#endregion

	}

	#region Helper Classes

	public class ComparisonResult
	{
		public string _originalFile;
		public string _renderingSet;
		public double _originalFileSizeInKB;

		public List<ResultantFiles> _resultantFiles;

		public ComparisonResult(string originalFile, string renderingSet, double originalFileSizeInKB)
		{
			_originalFile = originalFile;
			_renderingSet = renderingSet;
			_originalFileSizeInKB = originalFileSizeInKB;
		}
	}

	public class ResultantFiles
	{
		public string _modifiedFile;
		public string _outputFile;
		public string _summaryFile;
		public double _modifiedFileSizeInKB;
		public double _outputFileSizeInKB;
		public double _summaryFileSizeInKB;

		public ResultantFiles(string modifiedFile, string outputFile, string summaryFile, double modifiedFileSizeInKB, double outputFileSizeInKB, double summaryFileSizeInKB)
		{
			_modifiedFile = modifiedFile;
			_outputFile = outputFile;
			_summaryFile = summaryFile;

			_modifiedFileSizeInKB = modifiedFileSizeInKB;
			_outputFileSizeInKB = outputFileSizeInKB;
			_summaryFileSizeInKB = summaryFileSizeInKB;
		}
	}

	#endregion
}