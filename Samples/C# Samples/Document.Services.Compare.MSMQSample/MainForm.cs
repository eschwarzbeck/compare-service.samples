using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using Workshare.Document.Services.Compare.MSMQSample.CompareMSMQProxy;

namespace Workshare.Document.Services.Compare.MSMQSample
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			textBoxOriginal.Text = ConfigurationManager.AppSettings["OriginalFilePath"];
			textBoxModified.Text = ConfigurationManager.AppSettings["ModifiedFilePath"];
			textBoxOutput.Text = ConfigurationManager.AppSettings["OutputPath"];
			textQName.Text = ConfigurationManager.AppSettings["MSMQName"];
		}

		private void butCompare_Click(object sender, EventArgs e)
		{
			// Disable UI while we post the comparison request
			this.Enabled = false;
			this.Cursor = Cursors.WaitCursor;

			try
			{
				// Create the queued client proxy
				ComparerQueuedClient qComparer = new ComparerQueuedClient("CompareWebServiceQ", textQName.Text);

				// Set up string pairs for all of the parameters we wish to use in
				// our CompareQEventHandler on the server.
				// NB these paraneters are specific to the handler DLL which you write.
				Dictionary<string, string> dicParams = new Dictionary<string, string>();
				dicParams.Add("OriginalDocument", textBoxOriginal.Text);
				dicParams.Add("ModifiedDocument", textBoxModified.Text);
				dicParams.Add("RenderingSet", "");
				string sGuid = Guid.NewGuid().ToString();
				dicParams.Add("RedlineDocument", textBoxOutput.Text + "\\redline" + sGuid + ".rtf");
				dicParams.Add("RedlineXML", textBoxOutput.Text + "\\summary" + sGuid + ".xml");

				qComparer.ExecuteQueued(dicParams);

				MessageBox.Show("Request posted successfully.\n\nCheck the output folder for results.", "Compare Service MSMQ");
			}
			catch (System.ServiceModel.EndpointNotFoundException mex)
			{
				if ("MsmqException" == mex.InnerException.GetType().Name)
				{
					MessageBox.Show(this, "The MSMQ name is not valid", "Service Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show(this, mex.Message, "Service Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Service Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			}
		}

		private void butSelectOriginal_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{
				textBoxOriginal.Text = openFileDialog1.FileName;
			}
		}

		private void butSelectModified_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{
				textBoxModified.Text = openFileDialog1.FileName;
			}
		}

		private void buttonSelectOutput_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
			{
				textBoxOutput.Text = folderBrowserDialog1.SelectedPath;
			}
		}
	}
}