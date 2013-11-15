namespace Workshare.Document.Services.Compare.MSMQSample
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.butCompare = new System.Windows.Forms.Button();
            this.textBoxOriginal = new System.Windows.Forms.TextBox();
            this.textBoxModified = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butSelectModified = new System.Windows.Forms.Button();
            this.butSelectOriginal = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSelectOutput = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textQName = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(6, 9);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(527, 19);
            label3.TabIndex = 7;
            label3.Text = "This sample demonstrates how to perform asynchronous calls via the Workshare Comp" +
    "are MSMQ Service.";
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(6, 40);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(527, 27);
            label5.TabIndex = 8;
            label5.Text = "Messages can be posted to the Workshare Compare MSMQ Service and processed using " +
    "a custom CompareQEventHandler assembly in order to allow server-side integration" +
    " with existing DMS components.";
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(6, 83);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(527, 27);
            label6.TabIndex = 9;
            label6.Text = resources.GetString("label6.Text");
            // 
            // butCompare
            // 
            this.butCompare.Location = new System.Drawing.Point(464, 369);
            this.butCompare.Name = "butCompare";
            this.butCompare.Size = new System.Drawing.Size(75, 23);
            this.butCompare.TabIndex = 0;
            this.butCompare.Text = "&Compare";
            this.butCompare.UseVisualStyleBackColor = true;
            this.butCompare.Click += new System.EventHandler(this.butCompare_Click);
            // 
            // textBoxOriginal
            // 
            this.textBoxOriginal.Location = new System.Drawing.Point(51, 19);
            this.textBoxOriginal.Name = "textBoxOriginal";
            this.textBoxOriginal.Size = new System.Drawing.Size(437, 20);
            this.textBoxOriginal.TabIndex = 1;
            // 
            // textBoxModified
            // 
            this.textBoxModified.Location = new System.Drawing.Point(51, 45);
            this.textBoxModified.Name = "textBoxModified";
            this.textBoxModified.Size = new System.Drawing.Size(437, 20);
            this.textBoxModified.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Modified";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butSelectModified);
            this.groupBox1.Controls.Add(this.butSelectOriginal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxModified);
            this.groupBox1.Controls.Add(this.textBoxOriginal);
            this.groupBox1.Location = new System.Drawing.Point(12, 202);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 85);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Document Selection";
            // 
            // butSelectModified
            // 
            this.butSelectModified.Location = new System.Drawing.Point(493, 43);
            this.butSelectModified.Name = "butSelectModified";
            this.butSelectModified.Size = new System.Drawing.Size(28, 23);
            this.butSelectModified.TabIndex = 6;
            this.butSelectModified.Text = "...";
            this.butSelectModified.UseVisualStyleBackColor = true;
            this.butSelectModified.Click += new System.EventHandler(this.butSelectModified_Click);
            // 
            // butSelectOriginal
            // 
            this.butSelectOriginal.Location = new System.Drawing.Point(493, 17);
            this.butSelectOriginal.Name = "butSelectOriginal";
            this.butSelectOriginal.Size = new System.Drawing.Size(28, 23);
            this.butSelectOriginal.TabIndex = 7;
            this.butSelectOriginal.Text = "...";
            this.butSelectOriginal.UseVisualStyleBackColor = true;
            this.butSelectOriginal.Click += new System.EventHandler(this.butSelectOriginal_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Word Documents|*.doc";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSelectOutput);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxOutput);
            this.groupBox2.Location = new System.Drawing.Point(12, 293);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 55);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output Path Selection";
            // 
            // buttonSelectOutput
            // 
            this.buttonSelectOutput.Location = new System.Drawing.Point(493, 17);
            this.buttonSelectOutput.Name = "buttonSelectOutput";
            this.buttonSelectOutput.Size = new System.Drawing.Size(28, 23);
            this.buttonSelectOutput.TabIndex = 7;
            this.buttonSelectOutput.Text = "...";
            this.buttonSelectOutput.UseVisualStyleBackColor = true;
            this.buttonSelectOutput.Click += new System.EventHandler(this.buttonSelectOutput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Output";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(51, 19);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(437, 20);
            this.textBoxOutput.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textQName);
            this.groupBox3.Location = new System.Drawing.Point(12, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(527, 55);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Workshare Compare MSMQ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Name";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textQName
            // 
            this.textQName.Location = new System.Drawing.Point(51, 19);
            this.textQName.Name = "textQName";
            this.textQName.Size = new System.Drawing.Size(437, 20);
            this.textQName.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 404);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(label6);
            this.Controls.Add(label5);
            this.Controls.Add(label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butCompare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workshare Compare Service MSMQ Sample v5.2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butCompare;
        private System.Windows.Forms.TextBox textBoxOriginal;
        private System.Windows.Forms.TextBox textBoxModified;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butSelectModified;
        private System.Windows.Forms.Button butSelectOriginal;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSelectOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textQName;
    }
}

