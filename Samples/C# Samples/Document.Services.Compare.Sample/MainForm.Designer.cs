namespace Workshare.Document.Services.Compare.Sample
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.butCompare = new System.Windows.Forms.Button();
            this.textBoxOriginal = new System.Windows.Forms.TextBox();
            this.textBoxModified = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupDocSelection = new System.Windows.Forms.GroupBox();
            this.comboOptionsSets = new System.Windows.Forms.ComboBox();
            this.checkUseDefaultOptionsSet = new System.Windows.Forms.CheckBox();
            this.butSelectOptionSet = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxOptionSet = new System.Windows.Forms.TextBox();
            this.butSelectModified = new System.Windows.Forms.Button();
            this.butSelectOriginal = new System.Windows.Forms.Button();
            this.groupUserCredentials = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textHost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textDomain = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.butTestConnection = new System.Windows.Forms.Button();
            this.groupOutput = new System.Windows.Forms.GroupBox();
            this.checkRedlinMl = new System.Windows.Forms.CheckBox();
            this.butSelectRelineMl = new System.Windows.Forms.Button();
            this.textRedlineMl = new System.Windows.Forms.TextBox();
            this.cboConvert = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textModDocId = new System.Windows.Forms.TextBox();
            this.textOrigDocId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkWDF = new System.Windows.Forms.CheckBox();
            this.checkSummary = new System.Windows.Forms.CheckBox();
            this.checkRedline = new System.Windows.Forms.CheckBox();
            this.butSelectWDF = new System.Windows.Forms.Button();
            this.textWDF = new System.Windows.Forms.TextBox();
            this.butSelectSummary = new System.Windows.Forms.Button();
            this.butSelectRedline = new System.Windows.Forms.Button();
            this.textSummary = new System.Windows.Forms.TextBox();
            this.textRedline = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupDocSelection.SuspendLayout();
            this.groupUserCredentials.SuspendLayout();
            this.groupOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // butCompare
            // 
            this.butCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butCompare.Location = new System.Drawing.Point(287, 582);
            this.butCompare.Name = "butCompare";
            this.butCompare.Size = new System.Drawing.Size(112, 23);
            this.butCompare.TabIndex = 1;
            this.butCompare.Text = "&Compare";
            this.butCompare.UseVisualStyleBackColor = true;
            this.butCompare.Click += new System.EventHandler(this.butCompare_Click);
            // 
            // textBoxOriginal
            // 
            this.textBoxOriginal.Location = new System.Drawing.Point(77, 29);
            this.textBoxOriginal.Name = "textBoxOriginal";
            this.textBoxOriginal.Size = new System.Drawing.Size(411, 20);
            this.textBoxOriginal.TabIndex = 0;
            // 
            // textBoxModified
            // 
            this.textBoxModified.Location = new System.Drawing.Point(77, 55);
            this.textBoxModified.Name = "textBoxModified";
            this.textBoxModified.Size = new System.Drawing.Size(411, 20);
            this.textBoxModified.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Modified";
            // 
            // groupDocSelection
            // 
            this.groupDocSelection.Controls.Add(this.comboOptionsSets);
            this.groupDocSelection.Controls.Add(this.checkUseDefaultOptionsSet);
            this.groupDocSelection.Controls.Add(this.butSelectOptionSet);
            this.groupDocSelection.Controls.Add(this.label6);
            this.groupDocSelection.Controls.Add(this.textBoxOptionSet);
            this.groupDocSelection.Controls.Add(this.butSelectModified);
            this.groupDocSelection.Controls.Add(this.butSelectOriginal);
            this.groupDocSelection.Controls.Add(this.label2);
            this.groupDocSelection.Controls.Add(this.label1);
            this.groupDocSelection.Controls.Add(this.textBoxModified);
            this.groupDocSelection.Controls.Add(this.textBoxOriginal);
            this.groupDocSelection.Location = new System.Drawing.Point(12, 186);
            this.groupDocSelection.Name = "groupDocSelection";
            this.groupDocSelection.Size = new System.Drawing.Size(527, 147);
            this.groupDocSelection.TabIndex = 10;
            this.groupDocSelection.TabStop = false;
            this.groupDocSelection.Text = "Document Selection";
            // 
            // comboOptionsSets
            // 
            this.comboOptionsSets.DropDownHeight = 306;
            this.comboOptionsSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOptionsSets.Enabled = false;
            this.comboOptionsSets.FormattingEnabled = true;
            this.comboOptionsSets.IntegralHeight = false;
            this.comboOptionsSets.Items.AddRange(new object[] {
            "Default",
            "NoColourSet",
            "NoMovesSet",
            "CaretForDeletesSet",
            "ChangeNumbersSet",
            "FontChangesSet",
            "ShadingSet"});
            this.comboOptionsSets.Location = new System.Drawing.Point(236, 107);
            this.comboOptionsSets.Name = "comboOptionsSets";
            this.comboOptionsSets.Size = new System.Drawing.Size(252, 21);
            this.comboOptionsSets.TabIndex = 7;
            // 
            // checkUseDefaultOptionsSet
            // 
            this.checkUseDefaultOptionsSet.AutoSize = true;
            this.checkUseDefaultOptionsSet.Location = new System.Drawing.Point(77, 109);
            this.checkUseDefaultOptionsSet.Name = "checkUseDefaultOptionsSet";
            this.checkUseDefaultOptionsSet.Size = new System.Drawing.Size(153, 17);
            this.checkUseDefaultOptionsSet.TabIndex = 6;
            this.checkUseDefaultOptionsSet.Text = "Use server-side options set";
            this.checkUseDefaultOptionsSet.UseVisualStyleBackColor = true;
            this.checkUseDefaultOptionsSet.CheckedChanged += new System.EventHandler(this.checkUseDefaultOptionsSet_CheckedChanged);
            // 
            // butSelectOptionSet
            // 
            this.butSelectOptionSet.Location = new System.Drawing.Point(493, 79);
            this.butSelectOptionSet.Name = "butSelectOptionSet";
            this.butSelectOptionSet.Size = new System.Drawing.Size(28, 23);
            this.butSelectOptionSet.TabIndex = 5;
            this.butSelectOptionSet.Text = "...";
            this.butSelectOptionSet.UseVisualStyleBackColor = true;
            this.butSelectOptionSet.Click += new System.EventHandler(this.butSelectOptionSet_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Options Set";
            // 
            // textBoxOptionSet
            // 
            this.textBoxOptionSet.Location = new System.Drawing.Point(77, 81);
            this.textBoxOptionSet.Name = "textBoxOptionSet";
            this.textBoxOptionSet.Size = new System.Drawing.Size(411, 20);
            this.textBoxOptionSet.TabIndex = 4;
            // 
            // butSelectModified
            // 
            this.butSelectModified.Location = new System.Drawing.Point(493, 53);
            this.butSelectModified.Name = "butSelectModified";
            this.butSelectModified.Size = new System.Drawing.Size(28, 23);
            this.butSelectModified.TabIndex = 3;
            this.butSelectModified.Text = "...";
            this.butSelectModified.UseVisualStyleBackColor = true;
            this.butSelectModified.Click += new System.EventHandler(this.butSelectModified_Click);
            // 
            // butSelectOriginal
            // 
            this.butSelectOriginal.Location = new System.Drawing.Point(493, 27);
            this.butSelectOriginal.Name = "butSelectOriginal";
            this.butSelectOriginal.Size = new System.Drawing.Size(28, 23);
            this.butSelectOriginal.TabIndex = 1;
            this.butSelectOriginal.Text = "...";
            this.butSelectOriginal.UseVisualStyleBackColor = true;
            this.butSelectOriginal.Click += new System.EventHandler(this.butSelectOriginal_Click);
            // 
            // groupUserCredentials
            // 
            this.groupUserCredentials.Controls.Add(this.label7);
            this.groupUserCredentials.Controls.Add(this.textHost);
            this.groupUserCredentials.Controls.Add(this.label5);
            this.groupUserCredentials.Controls.Add(this.textDomain);
            this.groupUserCredentials.Controls.Add(this.label4);
            this.groupUserCredentials.Controls.Add(this.textPassword);
            this.groupUserCredentials.Controls.Add(this.label3);
            this.groupUserCredentials.Controls.Add(this.textUsername);
            this.groupUserCredentials.Location = new System.Drawing.Point(12, 12);
            this.groupUserCredentials.Name = "groupUserCredentials";
            this.groupUserCredentials.Size = new System.Drawing.Size(527, 159);
            this.groupUserCredentials.TabIndex = 3;
            this.groupUserCredentials.TabStop = false;
            this.groupUserCredentials.Text = "User Credentials";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Host";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textHost
            // 
            this.textHost.Location = new System.Drawing.Point(77, 123);
            this.textHost.Name = "textHost";
            this.textHost.Size = new System.Drawing.Size(411, 20);
            this.textHost.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Domain";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textDomain
            // 
            this.textDomain.Location = new System.Drawing.Point(77, 83);
            this.textDomain.Name = "textDomain";
            this.textDomain.Size = new System.Drawing.Size(411, 20);
            this.textDomain.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(77, 57);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(411, 20);
            this.textPassword.TabIndex = 1;
            this.textPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Username";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(77, 30);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(411, 20);
            this.textUsername.TabIndex = 0;
            // 
            // butTestConnection
            // 
            this.butTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butTestConnection.Location = new System.Drawing.Point(150, 582);
            this.butTestConnection.Name = "butTestConnection";
            this.butTestConnection.Size = new System.Drawing.Size(112, 23);
            this.butTestConnection.TabIndex = 0;
            this.butTestConnection.Text = "Test Connection";
            this.butTestConnection.UseVisualStyleBackColor = true;
            this.butTestConnection.Click += new System.EventHandler(this.butTestConnection_Click);
            // 
            // groupOutput
            // 
            this.groupOutput.Controls.Add(this.checkRedlinMl);
            this.groupOutput.Controls.Add(this.butSelectRelineMl);
            this.groupOutput.Controls.Add(this.textRedlineMl);
            this.groupOutput.Controls.Add(this.cboConvert);
            this.groupOutput.Controls.Add(this.label10);
            this.groupOutput.Controls.Add(this.textModDocId);
            this.groupOutput.Controls.Add(this.textOrigDocId);
            this.groupOutput.Controls.Add(this.label9);
            this.groupOutput.Controls.Add(this.label8);
            this.groupOutput.Controls.Add(this.checkWDF);
            this.groupOutput.Controls.Add(this.checkSummary);
            this.groupOutput.Controls.Add(this.checkRedline);
            this.groupOutput.Controls.Add(this.butSelectWDF);
            this.groupOutput.Controls.Add(this.textWDF);
            this.groupOutput.Controls.Add(this.butSelectSummary);
            this.groupOutput.Controls.Add(this.butSelectRedline);
            this.groupOutput.Controls.Add(this.textSummary);
            this.groupOutput.Controls.Add(this.textRedline);
            this.groupOutput.Location = new System.Drawing.Point(12, 339);
            this.groupOutput.Name = "groupOutput";
            this.groupOutput.Size = new System.Drawing.Size(527, 226);
            this.groupOutput.TabIndex = 11;
            this.groupOutput.TabStop = false;
            this.groupOutput.Text = "Output Files";
            // 
            // checkRedlinMl
            // 
            this.checkRedlinMl.AutoSize = true;
            this.checkRedlinMl.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkRedlinMl.Checked = true;
            this.checkRedlinMl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRedlinMl.Location = new System.Drawing.Point(2, 87);
            this.checkRedlinMl.Name = "checkRedlinMl";
            this.checkRedlinMl.Size = new System.Drawing.Size(80, 17);
            this.checkRedlinMl.TabIndex = 15;
            this.checkRedlinMl.Text = "Redline ML";
            this.checkRedlinMl.UseVisualStyleBackColor = true;
            this.checkRedlinMl.CheckedChanged += new System.EventHandler(this.checkRedlinMl_CheckedChanged);
            // 
            // butSelectRelineMl
            // 
            this.butSelectRelineMl.Location = new System.Drawing.Point(493, 83);
            this.butSelectRelineMl.Name = "butSelectRelineMl";
            this.butSelectRelineMl.Size = new System.Drawing.Size(28, 23);
            this.butSelectRelineMl.TabIndex = 17;
            this.butSelectRelineMl.Text = "...";
            this.butSelectRelineMl.UseVisualStyleBackColor = true;
            this.butSelectRelineMl.Click += new System.EventHandler(this.butSelectRelineMl_Click);
            // 
            // textRedlineMl
            // 
            this.textRedlineMl.Location = new System.Drawing.Point(88, 85);
            this.textRedlineMl.Name = "textRedlineMl";
            this.textRedlineMl.Size = new System.Drawing.Size(400, 20);
            this.textRedlineMl.TabIndex = 16;
            // 
            // cboConvert
            // 
            this.cboConvert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConvert.FormattingEnabled = true;
            this.cboConvert.Items.AddRange(new object[] {
            "Rtf",
            "Doc",
            "DocX",
            "Pdf"});
            this.cboConvert.Location = new System.Drawing.Point(194, 58);
            this.cboConvert.Name = "cboConvert";
            this.cboConvert.Size = new System.Drawing.Size(121, 21);
            this.cboConvert.TabIndex = 14;
            this.cboConvert.SelectedIndexChanged += new System.EventHandler(this.OnConvertTo);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Redline Converted To ";
            // 
            // textModDocId
            // 
            this.textModDocId.Enabled = false;
            this.textModDocId.Location = new System.Drawing.Point(135, 171);
            this.textModDocId.Name = "textModDocId";
            this.textModDocId.Size = new System.Drawing.Size(353, 20);
            this.textModDocId.TabIndex = 12;
            // 
            // textOrigDocId
            // 
            this.textOrigDocId.Enabled = false;
            this.textOrigDocId.Location = new System.Drawing.Point(135, 144);
            this.textOrigDocId.Name = "textOrigDocId";
            this.textOrigDocId.Size = new System.Drawing.Size(353, 20);
            this.textOrigDocId.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(47, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Modified Doc Id";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Original Doc Id";
            // 
            // checkWDF
            // 
            this.checkWDF.AutoSize = true;
            this.checkWDF.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkWDF.Location = new System.Drawing.Point(31, 117);
            this.checkWDF.Name = "checkWDF";
            this.checkWDF.Size = new System.Drawing.Size(51, 17);
            this.checkWDF.TabIndex = 3;
            this.checkWDF.Text = "WDF";
            this.checkWDF.UseVisualStyleBackColor = true;
            this.checkWDF.CheckedChanged += new System.EventHandler(this.checkWDF_CheckedChanged);
            // 
            // checkSummary
            // 
            this.checkSummary.AutoSize = true;
            this.checkSummary.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkSummary.Location = new System.Drawing.Point(13, 199);
            this.checkSummary.Name = "checkSummary";
            this.checkSummary.Size = new System.Drawing.Size(69, 17);
            this.checkSummary.TabIndex = 6;
            this.checkSummary.Text = "Summary";
            this.checkSummary.UseVisualStyleBackColor = true;
            this.checkSummary.CheckedChanged += new System.EventHandler(this.checkSummary_CheckedChanged);
            // 
            // checkRedline
            // 
            this.checkRedline.AutoSize = true;
            this.checkRedline.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkRedline.Checked = true;
            this.checkRedline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRedline.Location = new System.Drawing.Point(21, 32);
            this.checkRedline.Name = "checkRedline";
            this.checkRedline.Size = new System.Drawing.Size(62, 17);
            this.checkRedline.TabIndex = 0;
            this.checkRedline.Text = "Redline";
            this.checkRedline.UseVisualStyleBackColor = true;
            this.checkRedline.CheckedChanged += new System.EventHandler(this.checkRedline_CheckedChanged);
            // 
            // butSelectWDF
            // 
            this.butSelectWDF.Location = new System.Drawing.Point(493, 115);
            this.butSelectWDF.Name = "butSelectWDF";
            this.butSelectWDF.Size = new System.Drawing.Size(28, 23);
            this.butSelectWDF.TabIndex = 5;
            this.butSelectWDF.Text = "...";
            this.butSelectWDF.UseVisualStyleBackColor = true;
            this.butSelectWDF.Click += new System.EventHandler(this.butSelectWDF_Click);
            // 
            // textWDF
            // 
            this.textWDF.Location = new System.Drawing.Point(89, 117);
            this.textWDF.Name = "textWDF";
            this.textWDF.Size = new System.Drawing.Size(399, 20);
            this.textWDF.TabIndex = 4;
            // 
            // butSelectSummary
            // 
            this.butSelectSummary.Location = new System.Drawing.Point(493, 195);
            this.butSelectSummary.Name = "butSelectSummary";
            this.butSelectSummary.Size = new System.Drawing.Size(28, 23);
            this.butSelectSummary.TabIndex = 8;
            this.butSelectSummary.Text = "...";
            this.butSelectSummary.UseVisualStyleBackColor = true;
            this.butSelectSummary.Click += new System.EventHandler(this.butSelectSummary_Click);
            // 
            // butSelectRedline
            // 
            this.butSelectRedline.Location = new System.Drawing.Point(493, 27);
            this.butSelectRedline.Name = "butSelectRedline";
            this.butSelectRedline.Size = new System.Drawing.Size(28, 23);
            this.butSelectRedline.TabIndex = 2;
            this.butSelectRedline.Text = "...";
            this.butSelectRedline.UseVisualStyleBackColor = true;
            this.butSelectRedline.Click += new System.EventHandler(this.butSelectRedline_Click);
            // 
            // textSummary
            // 
            this.textSummary.Location = new System.Drawing.Point(88, 197);
            this.textSummary.Name = "textSummary";
            this.textSummary.Size = new System.Drawing.Size(400, 20);
            this.textSummary.TabIndex = 7;
            // 
            // textRedline
            // 
            this.textRedline.Location = new System.Drawing.Point(89, 29);
            this.textRedline.Name = "textRedline";
            this.textRedline.Size = new System.Drawing.Size(399, 20);
            this.textRedline.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AcceptButton = this.butTestConnection;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 617);
            this.Controls.Add(this.groupOutput);
            this.Controls.Add(this.groupUserCredentials);
            this.Controls.Add(this.groupDocSelection);
            this.Controls.Add(this.butTestConnection);
            this.Controls.Add(this.butCompare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workshare Compare Service Sample - v7.10";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupDocSelection.ResumeLayout(false);
            this.groupDocSelection.PerformLayout();
            this.groupUserCredentials.ResumeLayout(false);
            this.groupUserCredentials.PerformLayout();
            this.groupOutput.ResumeLayout(false);
            this.groupOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butCompare;
        private System.Windows.Forms.TextBox textBoxOriginal;
        private System.Windows.Forms.TextBox textBoxModified;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupDocSelection;
        private System.Windows.Forms.Button butSelectModified;
        private System.Windows.Forms.Button butSelectOriginal;
        private System.Windows.Forms.GroupBox groupUserCredentials;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textDomain;
        private System.Windows.Forms.Button butTestConnection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.Button butSelectOptionSet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxOptionSet;
        private System.Windows.Forms.GroupBox groupOutput;
        private System.Windows.Forms.CheckBox checkWDF;
        private System.Windows.Forms.CheckBox checkSummary;
        private System.Windows.Forms.CheckBox checkRedline;
        private System.Windows.Forms.Button butSelectWDF;
        private System.Windows.Forms.TextBox textWDF;
        private System.Windows.Forms.Button butSelectSummary;
        private System.Windows.Forms.Button butSelectRedline;
        private System.Windows.Forms.TextBox textSummary;
        private System.Windows.Forms.TextBox textRedline;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textHost;
        private System.Windows.Forms.CheckBox checkUseDefaultOptionsSet;
        private System.Windows.Forms.ComboBox comboOptionsSets;
        private System.Windows.Forms.TextBox textModDocId;
        private System.Windows.Forms.TextBox textOrigDocId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cboConvert;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkRedlinMl;
        private System.Windows.Forms.Button butSelectRelineMl;
        private System.Windows.Forms.TextBox textRedlineMl;
    }
}

