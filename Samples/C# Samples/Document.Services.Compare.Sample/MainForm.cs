using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using Workshare.Document.Services.Compare.Sample.CompareProxy;
using System.Globalization;

namespace Workshare.Document.Services.Compare.Sample
{
	public partial class MainForm : Form
	{
		// Supported input formats for the Original/Modified browse dialogs
		private const string m_sSupportedInputFormats = "Word Documents (*.docx)|*.docx|Word 97-2003 (*.doc)|*.doc|Rich Text (*.rtf)|*.rtf|PDF Documents (*.pdf)|*.pdf|Web Pages (*.htm; *.html)|*.htm;*.html";

		public MainForm()
		{
			InitializeComponent();

			/// -------------------------------------------------------------------------

			textBoxOriginal.Text = ConfigurationManager.AppSettings["OriginalFilePath"];
			textBoxModified.Text = ConfigurationManager.AppSettings["ModifiedFilePath"];
			textBoxOptionSet.Text = ConfigurationManager.AppSettings["OptionSetPath"];

			textRedline.Text = MoveIfNoWriteAccess(ConfigurationManager.AppSettings["RTFPath"]);
			textWDF.Text = MoveIfNoWriteAccess(ConfigurationManager.AppSettings["WDFPath"]);
			textSummary.Text = MoveIfNoWriteAccess(ConfigurationManager.AppSettings["XMLPath"]);
            textRedlineMl.Text = MoveIfNoWriteAccess(ConfigurationManager.AppSettings["MlPath"]);

			textHost.Text = ConfigurationManager.AppSettings["DefaultHost"];

			textUsername.Text = Environment.UserName;
			textDomain.Text = Environment.UserDomainName;

			comboOptionsSets.SelectedIndex = 0;
			cboConvert.SelectedIndex = 0;

			UpdateOriginalDocId();
			UpdateModifiedDocId();
		}

	    private string MoveIfNoWriteAccess(string filePath)
	    {
	        FileInfo fi = new FileInfo(filePath);
            if (HasWritePermission(fi.DirectoryName))
	            return filePath;

	        return Path.Combine(Path.GetTempPath(), fi.Name);
	    }

        private static bool HasWritePermission(string FilePath)
        {
            try
            {
                FileSystemSecurity security;
                if (File.Exists(FilePath))
                {
                    security = File.GetAccessControl(FilePath);
                }
                else
                {
                    security = Directory.GetAccessControl(Path.GetDirectoryName(FilePath));
                }
                var rules = security.GetAccessRules(true, true, typeof(NTAccount));

                var currentuser = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                bool result = false;
                foreach (FileSystemAccessRule rule in rules)
                {
                    if (0 == (rule.FileSystemRights &
                        (FileSystemRights.WriteData | FileSystemRights.Write)))
                    {
                        continue;
                    }

                    if (rule.IdentityReference.Value.StartsWith("S-1-"))
                    {
                        var sid = new SecurityIdentifier(rule.IdentityReference.Value);
                        if (!currentuser.IsInRole(sid))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!currentuser.IsInRole(rule.IdentityReference.Value))
                        {
                            continue;
                        }
                    }

                    if (rule.AccessControlType == AccessControlType.Deny)
                        return false;
                    if (rule.AccessControlType == AccessControlType.Allow)
                        result = true;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }

	    // Gets a string containing the rendering options
		// loaded from the specified .set file.
		private string GetCompareOptions()
		{
			if (!string.IsNullOrEmpty(textBoxOptionSet.Text))
			{
				try
				{
					string result = System.IO.File.ReadAllText(textBoxOptionSet.Text);
					if (checkWDF.Checked)
						result += BuildWDFOptions();
					return result;
				}
				catch (Exception)
				{
				}
			}
			return "";
		}

		private string BuildWDFOptions()
		{
			string result = "\nOriginalDocId=" + textOrigDocId.Text;
			result += "\nModifiedDocId=" + textModDocId.Text;
			return result;
		}

		private void butCompare_Click(object sender, EventArgs e)
		{
			// Check for empty passwords
			if (!PasswordIsValid())
				return;

			// Disable the UI while we run the comparison
			groupUserCredentials.Enabled = false;
			groupDocSelection.Enabled = false;
			groupOutput.Enabled = false;

			this.Cursor = Cursors.WaitCursor;


			// This code demonstrates how to call the BasicHttp legacy service
			// from a Service Reference proxy (This protocol may still be imported
			// as a Web Service Reference if required for legacy language support.
			/*
			CompareProxy.LegacyComparerClient asmxCompare = new Document.Services.Compare.Sample.CompareProxy.LegacyComparerClient();

			if( asmxCompare.Authenticate("DIS", "User", "Pass") )
			{
				CompareProxy.CompareResult cr = asmxCompare.Execute2(   System.IO.File.ReadAllBytes(textBoxOriginal.Text),
																		System.IO.File.ReadAllBytes(textBoxModified.Text),
																		CompareProxy.CompareResponseFlags.Rtf,
																		"");
			}
			*/


			// This code demonstrates connecting to the Compare service via the WSHttp binding
			// which ensures more secure transport and data compression.
			CompareProxy.ComparerClient svcCompare = new CompareProxy.ComparerClient("CompareWebServiceWCF", textHost.Text);
			svcCompare.ClientCredentials.Windows.ClientCredential.UserName = textUsername.Text;
			svcCompare.ClientCredentials.Windows.ClientCredential.Password = textPassword.Text;
			svcCompare.ClientCredentials.Windows.ClientCredential.Domain = textDomain.Text;
            //svcCompare.ClientCredentials.Windows.AllowNtlm = true;
            //svcCompare.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;


			try
			{
				// Always call Authenticate as the first operation and use the same Client Credentials
				if (svcCompare.Authenticate(textDomain.Text, textUsername.Text, textPassword.Text))
				{
					// Determine which option was specified for the
					// result of the comparison.  RTF, XML, WDF etc.
					CompareProxy.ResponseOptions desiredResults = 0;

					if (checkWDF.Checked)
					{
						desiredResults = CompareProxy.ResponseOptions.Wdf;
					}
					else if (checkRedline.Checked)
					{
						//cboConvert index
						// 0 = RTF
						// 1 = Doc
						// 2 = DocX
						// 3 = Pdf
						switch (cboConvert.SelectedIndex)
						{
							case 0:
								desiredResults = CompareProxy.ResponseOptions.Rtf;
								break;
							case 1:
								desiredResults = CompareProxy.ResponseOptions.Doc;
								break;
							case 2:
								desiredResults = CompareProxy.ResponseOptions.DocX;
								break;
							case 3:
								desiredResults = CompareProxy.ResponseOptions.Pdf;
								break;

						}
						
					}


					if (checkSummary.Checked)
					{
						desiredResults |= CompareProxy.ResponseOptions.Xml;
					}

                    if( checkRedlinMl.Checked )
                    {
                        desiredResults |= CompareProxy.ResponseOptions.RedlinMl;
                    }


					string sCompareOptions = "";

					// If a server-side option set has been specifed then call SetOptionsSet
					if (checkUseDefaultOptionsSet.Checked)
					{
						if (comboOptionsSets.SelectedIndex != 0)
						{
							svcCompare.SetOptionsSet(comboOptionsSets.Text);
						}

						//To use the default server-side options set just set the options string to "".
					}
					else
					{
						// If using client-side options set then load
						// the options from the specified file.
						sCompareOptions = GetCompareOptions();
					}

					// Run the comparison
					//var executeParams = new ExecuteParams
					//                        {
					//                            CompareOptions = sCompareOptions,
					//                            Modified = System.IO.File.ReadAllBytes(textBoxModified.Text),
					//                            Original = System.IO.File.ReadAllBytes(textBoxOriginal.Text),
					//                            ResponseOption = desiredResults
					//                        };

					CompareProxy.CompareResults cr = svcCompare.Execute(System.IO.File.ReadAllBytes(textBoxOriginal.Text),
																		System.IO.File.ReadAllBytes(textBoxModified.Text),
																		desiredResults,
																		sCompareOptions);

					//CompareResults cr = svcCompare.ExecuteEx(executeParams);

					// Always close the service connection as soon as possible after use.
					svcCompare.Close();

					if (cr != null)
					{
						// Write out the XML summary and display with the
						// default application for .xml
						if ((desiredResults & CompareProxy.ResponseOptions.Xml) == CompareProxy.ResponseOptions.Xml)
						{
							if (cr.Summary != null)
							{
								System.IO.File.WriteAllText(textSummary.Text, cr.Summary);
								System.Diagnostics.Process.Start(textSummary.Text);
							}
							else
							{
								MessageBox.Show("Execute did not return a summary", "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}

                        if( checkRedlinMl.Checked )
                        {
                            if (cr.RedlineMl != null)
                            {
                                System.IO.File.WriteAllText(textRedlineMl.Text, cr.RedlineMl);
                                System.Diagnostics.Process.Start("Notepad.exe", textRedlineMl.Text);
                            }
                            else
                            {
                                MessageBox.Show("Execute did not return a redline ML", "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
					    // Write out the RTF redline, if selected, and display with the
						// internal viewer
						if ((desiredResults & CompareProxy.ResponseOptions.Rtf) == CompareProxy.ResponseOptions.Rtf)
						{
							if (cr.Redline != null)
							{
								string rtfRedline = FromASCIIByteArray(cr.Redline);

								formCompareResults formResults = new formCompareResults();
								formResults.SetRTFResults(rtfRedline);

								formResults.ShowDialog(this);

								System.IO.File.WriteAllBytes(textRedline.Text, cr.Redline);
								//System.Diagnostics.Process.Start(textRedline.Text);
							}
							else
							{
								MessageBox.Show("Execute did not return a redline", "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}// Write out the WDF composite and display with the default application for .wdf
						else if ((desiredResults & CompareProxy.ResponseOptions.Wdf) == CompareProxy.ResponseOptions.Wdf)
						{
							if (cr.Redline != null)
							{
								System.IO.File.WriteAllBytes(textWDF.Text, cr.Redline);
								System.Diagnostics.Process.Start(textWDF.Text);
							}
							else
							{
								MessageBox.Show("Execute did not return a redline", "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						// Check if we want a different redline convertor then the standard RTF
						else if ((desiredResults & CompareProxy.ResponseOptions.Doc) == CompareProxy.ResponseOptions.Doc
								 || (desiredResults & CompareProxy.ResponseOptions.DocX) == CompareProxy.ResponseOptions.DocX
								 || (desiredResults & CompareProxy.ResponseOptions.Pdf) == CompareProxy.ResponseOptions.Pdf)
						{
							if (cr.Redline != null)
							{
								System.IO.File.WriteAllBytes(textRedline.Text, cr.Redline);
								System.Diagnostics.Process.Start(textRedline.Text);
								MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, "Written results to {0}", textRedline.Text),
								                "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else
							{
								MessageBox.Show("Execute did not return a redline", "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
					else
					{
						MessageBox.Show("Execute did not return any results", "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			catch (System.ServiceModel.ServerTooBusyException)
			{
				MessageBox.Show("Server Too Busy", "Compare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (TimeoutException ex)
			{
				MessageBox.Show(ex.Message, "TimeoutException", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (System.ServiceModel.FaultException ex)
			{
				//svcCompare.Abort();
				MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (System.ServiceModel.CommunicationException ex)
			{
				//svcCompare.Abort();

                if( ex.Message.Contains("request is unauthorized") )
                {
                    MessageBox.Show(@"Unauthorized issue arised, please check User Name and/or Password and/or domain!",
                                    @"CommunicationException",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "CommunicationException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
			}
			catch (System.ComponentModel.Win32Exception ex)
			{
				// When attempting to open the comparison results it is possible
				// that no document is associated with the file extension.
				if (ex.NativeErrorCode == 0x00000483)
				{
					MessageBox.Show("The redline has been saved to the specified location but no application has been associated with this file type in order to display it.", "File Associations", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				//svcCompare.Close();
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			// Re-enable UI
			groupOutput.Enabled = true;
			groupDocSelection.Enabled = true;
			groupUserCredentials.Enabled = true;

			this.Cursor = Cursors.Default;
		}

		private static string FromASCIIByteArray(byte[] characters)
		{
			System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
			string constructedString = encoding.GetString(characters);

			return (constructedString);
		}

		// Select the file to use as the Original document
		private void butSelectOriginal_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = m_sSupportedInputFormats;
			openFileDialog.FileName = textBoxOriginal.Text;

			if (DialogResult.OK == openFileDialog.ShowDialog())
			{
				textBoxOriginal.Text = openFileDialog.FileName;
				UpdateOriginalDocId();
			}
		}

		private void UpdateOriginalDocId()
		{
			textOrigDocId.Text = "file://" + textBoxOriginal.Text;
		}

		// Select the file to use as the Modified document
		private void butSelectModified_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = m_sSupportedInputFormats;
			openFileDialog.FileName = textBoxModified.Text;

			if (DialogResult.OK == openFileDialog.ShowDialog())
			{
				textBoxModified.Text = openFileDialog.FileName;
			}
		}

		private void UpdateModifiedDocId()
		{
			textModDocId.Text = "file://" + textBoxModified.Text;
		}

		// Select the client-side option set
		private void butSelectOptionSet_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = "Option Set files (*.set)|*.set";
			openFileDialog.FileName = textBoxOptionSet.Text;

			if (DialogResult.OK == openFileDialog.ShowDialog())
			{
				textBoxOptionSet.Text = openFileDialog.FileName;
			}
		}

		// Select the location where the redline RTF should be saved.
		private void butSelectRedline_Click(object sender, EventArgs e)
		{
			saveFileDialog.Filter = "Redline files (*.rtf;*.doc;*.docx;*.pdf)|*.rtf;*.doc;*.docx;*.pdf";
			saveFileDialog.FileName = textRedline.Text;

			if (DialogResult.OK == saveFileDialog.ShowDialog())
			{
				textRedline.Text = saveFileDialog.FileName;
			}
		}

		// Select the location where the XML summery should be saved.
		private void butSelectSummary_Click(object sender, EventArgs e)
		{
			saveFileDialog.Filter = "Summary files (*.xml)|*.xml";
			saveFileDialog.FileName = textSummary.Text;

			if (DialogResult.OK == saveFileDialog.ShowDialog())
			{
				textSummary.Text = saveFileDialog.FileName;
			}
		}

		// Select the location where the WDF composite should be saved.
		private void butSelectWDF_Click(object sender, EventArgs e)
		{
			saveFileDialog.Filter = "WDF files (*.wdf)|*.wdf";
			saveFileDialog.FileName = textWDF.Text;

			if (DialogResult.OK == saveFileDialog.ShowDialog())
			{
				textWDF.Text = saveFileDialog.FileName;
			}
		}

		public static bool RemoteCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			// do some validation here...
			return true;
		}

		// This method demonstrates setting user credentials and opening a service session.
		// It is important to ensure that the different exception types which may be thrown
		// are handled individually, in order to ensure that the service runs optimally.
		private void butTestConnection_Click(object sender, EventArgs e)
		{
			// Check for empty passwords
			if (!PasswordIsValid())
				return;

			// Disable UI
			groupUserCredentials.Enabled = false;
			groupDocSelection.Enabled = false;
			this.Enabled = false;
			this.Cursor = Cursors.WaitCursor;

			// Create service proxy object and set user credentials
			

			//System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;

            CompareProxy.ComparerClient svcCompare = null;
            try
            {
                svcCompare = new CompareProxy.ComparerClient("CompareWebServiceWCF", textHost.Text);
                System.Net.NetworkCredential netCredential = new System.Net.NetworkCredential(textUsername.Text, textPassword.Text, textDomain.Text);
                svcCompare.ClientCredentials.Windows.ClientCredential = netCredential;

                if (svcCompare.Authenticate("realm", "user", "password"))
                {
                    string sCompareVersion = svcCompare.GetVersion();
                    string sCompositorVersion = svcCompare.GetCompositorVersion();
                    var results = svcCompare.Ping();
					if (results.Redline == null)
						MessageBox.Show("Successfully connected to service.\n\nVersion: " + sCompareVersion + "\nCompositor: " + sCompositorVersion + "\nPing : " + "Failed - redline NULL!" , "Test User Credentials");
					else
						MessageBox.Show("Successfully connected to service.\n\nVersion: " + sCompareVersion + "\nCompositor: " + sCompositorVersion + "\nPing : " + (results.Redline.Length > 0 ? "Passed" : "Failed!"), "Test User Credentials");
				}
                else
                {
                    MessageBox.Show("Authentication Error");
                }

            }
            catch (TimeoutException ex)
            {
                svcCompare.Abort();
                MessageBox.Show(ex.Message, "TimeoutException");
            }
            catch (System.ServiceModel.FaultException ex)
            {
                svcCompare.Abort();
                MessageBox.Show(ex.Message, "FaultException");
            }
            catch (System.ServiceModel.Security.SecurityNegotiationException ex)
            {
                svcCompare.Abort();
                MessageBox.Show(ex.Message, "SecurityNegotiationException");
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                svcCompare.Abort();
                if (ex.Message.Contains("request is unauthorized"))
                {
                    MessageBox.Show(@"Unauthorized issue arised, please check User Name and/or Password and/or domain!",
                                    @"CommunicationException",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "CommunicationException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message, "Win32Exception");
            }
            catch (Exception ex)
            {
                svcCompare.Close();
                MessageBox.Show(ex.Message, "Exception");
            }
            finally
            {
                if (svcCompare != null && svcCompare.State == System.ServiceModel.CommunicationState.Opened)
                {
                    svcCompare.Close();
                }
            }

			// re-enable UI
			groupDocSelection.Enabled = true;
			groupUserCredentials.Enabled = true;
			this.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Visible = true;
			textPassword.Focus();
		}


		// Determine if we want an RTF redline
		private void checkRedline_CheckedChanged(object sender, EventArgs e)
		{
			if (checkRedline.Checked)
			{
				checkWDF.Checked = false;
			}

            butCompare.Enabled = (checkRedline.Checked || checkSummary.Checked || checkWDF.Checked || checkRedlinMl.Checked);
		}

		// Determine if we want an XML summary
		private void checkSummary_CheckedChanged(object sender, EventArgs e)
		{
            butCompare.Enabled = (checkRedline.Checked || checkSummary.Checked || checkWDF.Checked || checkRedlinMl.Checked);
		}

		// Determine if we want a WDF composite
		private void checkWDF_CheckedChanged(object sender, EventArgs e)
		{
			if (checkWDF.Checked)
			{
				checkRedline.Checked = false;
			    checkRedlinMl.Checked = false;
			}

			UpdateDocIdBoxStatus();

            butCompare.Enabled = (checkRedline.Checked || checkSummary.Checked || checkWDF.Checked || checkRedlinMl.Checked);
		}

		private void UpdateDocIdBoxStatus()
		{
			textOrigDocId.Enabled = checkWDF.Checked && !checkUseDefaultOptionsSet.Checked;
			textModDocId.Enabled = checkWDF.Checked && !checkUseDefaultOptionsSet.Checked;
		}

		// Determine if we want to use a server-side options set
		private void checkUseDefaultOptionsSet_CheckedChanged(object sender, EventArgs e)
		{
			textBoxOptionSet.Enabled = !checkUseDefaultOptionsSet.Checked;
			butSelectOptionSet.Enabled = !checkUseDefaultOptionsSet.Checked;
			comboOptionsSets.Enabled = checkUseDefaultOptionsSet.Checked;

			UpdateDocIdBoxStatus();
		}

		// Check for empty passwords
		private bool PasswordIsValid()
		{
			bool bValid = true;

			if (textPassword.Text.Length <= 0)
			{
				if (DialogResult.No == MessageBox.Show(this, "You have not entered a password.  Are you sure that the server allows blank passwords to be authenticated?\n\nClick Yes to continue or No to enter a password", "Password Is Blank", MessageBoxButtons.YesNo))
				{
					bValid = false;
				}

			}
			return bValid;
		}

		private void OnConvertTo(object sender, EventArgs e)
		{
			if ( string.IsNullOrEmpty(textRedline.Text.Trim())== false
				&& textRedline.Text.Length > 0 )
			{
				if ( Path.HasExtension(textRedline.Text))
				{

					string file = string.Format(CultureInfo.CurrentCulture, "{0}\\{1}.{2}", 
												Path.GetDirectoryName(textRedline.Text),
					                            Path.GetFileNameWithoutExtension(textRedline.Text), 
												cboConvert.SelectedItem.ToString().ToLowerInvariant());
					textRedline.Text = file;
				}
			}
		}

        private void butSelectRelineMl_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Redline ML files (*.xml)|*.xml";
            saveFileDialog.FileName = textRedlineMl.Text;

            if( DialogResult.OK == saveFileDialog.ShowDialog() )
            {
                textRedlineMl.Text = saveFileDialog.FileName;
            }
        }

        private void checkRedlinMl_CheckedChanged(object sender, EventArgs e)
        {
            if( checkRedlinMl.Checked )
            {
                checkWDF.Checked = false;
            }

            butCompare.Enabled = (checkRedline.Checked || checkSummary.Checked || checkWDF.Checked || checkRedlinMl.Checked);
        }
	}
}