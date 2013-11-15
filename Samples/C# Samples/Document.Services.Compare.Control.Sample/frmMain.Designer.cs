namespace Document.Services.Compare.Control.Sample
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.butStart = new System.Windows.Forms.Button();
            this.progressOriginal = new System.Windows.Forms.ProgressBar();
            this.progressModified = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelOriginalBytesPerSec = new System.Windows.Forms.Label();
            this.labelModifiedBytesPerSec = new System.Windows.Forms.Label();
            this.dlgOpenOriginalFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgOpenModifiedFile = new System.Windows.Forms.OpenFileDialog();
            this.butSelectOriginal = new System.Windows.Forms.Button();
            this.butSelectModified = new System.Windows.Forms.Button();
            this.textOriginalFile = new System.Windows.Forms.TextBox();
            this.textModifiedFile = new System.Windows.Forms.TextBox();
            this.labelOriginalBytesSent = new System.Windows.Forms.Label();
            this.labelModifiedBytesSent = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelResultsTime = new System.Windows.Forms.Label();
            this.labelComparisonTime = new System.Windows.Forms.Label();
            this.labelModifiedPreTime = new System.Windows.Forms.Label();
            this.labelModifiedConversionTime = new System.Windows.Forms.Label();
            this.labelOriginalPreTime = new System.Windows.Forms.Label();
            this.labelOriginalConversionTime = new System.Windows.Forms.Label();
            this.timerTest = new System.Windows.Forms.Timer( this.components );
            this.labelTotal = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butStart
            // 
            this.butStart.Enabled = false;
            this.butStart.Location = new System.Drawing.Point( 731, 346 );
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size( 75, 23 );
            this.butStart.TabIndex = 0;
            this.butStart.Text = "Start";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler( this.butStart_Click );
            // 
            // progressOriginal
            // 
            this.progressOriginal.Location = new System.Drawing.Point( 12, 37 );
            this.progressOriginal.Maximum = 1000;
            this.progressOriginal.Name = "progressOriginal";
            this.progressOriginal.Size = new System.Drawing.Size( 737, 23 );
            this.progressOriginal.TabIndex = 1;
            // 
            // progressModified
            // 
            this.progressModified.Location = new System.Drawing.Point( 13, 114 );
            this.progressModified.Maximum = 1000;
            this.progressModified.Name = "progressModified";
            this.progressModified.Size = new System.Drawing.Size( 737, 23 );
            this.progressModified.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 13, 13 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 64, 13 );
            this.label1.TabIndex = 3;
            this.label1.Text = "Original File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 13, 89 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 69, 13 );
            this.label2.TabIndex = 4;
            this.label2.Text = "Modified File:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 767, 50 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 45, 13 );
            this.label3.TabIndex = 5;
            this.label3.Text = "KB/Sec";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 767, 127 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 45, 13 );
            this.label4.TabIndex = 6;
            this.label4.Text = "KB/Sec";
            // 
            // labelOriginalBytesPerSec
            // 
            this.labelOriginalBytesPerSec.AutoSize = true;
            this.labelOriginalBytesPerSec.Location = new System.Drawing.Point( 767, 37 );
            this.labelOriginalBytesPerSec.Name = "labelOriginalBytesPerSec";
            this.labelOriginalBytesPerSec.Size = new System.Drawing.Size( 13, 13 );
            this.labelOriginalBytesPerSec.TabIndex = 7;
            this.labelOriginalBytesPerSec.Text = "0";
            // 
            // labelModifiedBytesPerSec
            // 
            this.labelModifiedBytesPerSec.AutoSize = true;
            this.labelModifiedBytesPerSec.Location = new System.Drawing.Point( 767, 114 );
            this.labelModifiedBytesPerSec.Name = "labelModifiedBytesPerSec";
            this.labelModifiedBytesPerSec.Size = new System.Drawing.Size( 13, 13 );
            this.labelModifiedBytesPerSec.TabIndex = 8;
            this.labelModifiedBytesPerSec.Text = "0";
            // 
            // dlgOpenOriginalFile
            // 
            this.dlgOpenOriginalFile.FileName = "original";
            this.dlgOpenOriginalFile.Filter = "Word Documents|*.docx|Word 97-2003|*.doc|PDF Documents|*.pdf|Rich Text Format|*.r" +
                "tf|HTML Documents|*.html";
            this.dlgOpenOriginalFile.FilterIndex = 3;
            this.dlgOpenOriginalFile.Title = "Select Original File";
            // 
            // dlgOpenModifiedFile
            // 
            this.dlgOpenModifiedFile.FileName = "modified";
            this.dlgOpenModifiedFile.Filter = "Word Documents|*.docx|Word 97-2003|*.doc|PDF Documents|*.pdf|Rich Text Format|*.r" +
                "tf|HTML Documents|*.html";
            this.dlgOpenModifiedFile.FilterIndex = 3;
            this.dlgOpenModifiedFile.Title = "Select Modified File";
            // 
            // butSelectOriginal
            // 
            this.butSelectOriginal.Location = new System.Drawing.Point( 719, 9 );
            this.butSelectOriginal.Name = "butSelectOriginal";
            this.butSelectOriginal.Size = new System.Drawing.Size( 29, 23 );
            this.butSelectOriginal.TabIndex = 9;
            this.butSelectOriginal.Text = "...";
            this.butSelectOriginal.UseVisualStyleBackColor = true;
            this.butSelectOriginal.Click += new System.EventHandler( this.butSelectOriginal_Click );
            // 
            // butSelectModified
            // 
            this.butSelectModified.Location = new System.Drawing.Point( 719, 85 );
            this.butSelectModified.Name = "butSelectModified";
            this.butSelectModified.Size = new System.Drawing.Size( 30, 23 );
            this.butSelectModified.TabIndex = 10;
            this.butSelectModified.Text = "...";
            this.butSelectModified.UseVisualStyleBackColor = true;
            this.butSelectModified.Click += new System.EventHandler( this.butSelectModified_Click );
            // 
            // textOriginalFile
            // 
            this.textOriginalFile.Location = new System.Drawing.Point( 83, 11 );
            this.textOriginalFile.Name = "textOriginalFile";
            this.textOriginalFile.ReadOnly = true;
            this.textOriginalFile.Size = new System.Drawing.Size( 630, 20 );
            this.textOriginalFile.TabIndex = 11;
            // 
            // textModifiedFile
            // 
            this.textModifiedFile.Location = new System.Drawing.Point( 84, 86 );
            this.textModifiedFile.Name = "textModifiedFile";
            this.textModifiedFile.ReadOnly = true;
            this.textModifiedFile.Size = new System.Drawing.Size( 629, 20 );
            this.textModifiedFile.TabIndex = 12;
            // 
            // labelOriginalBytesSent
            // 
            this.labelOriginalBytesSent.AutoSize = true;
            this.labelOriginalBytesSent.Location = new System.Drawing.Point( 376, 63 );
            this.labelOriginalBytesSent.Name = "labelOriginalBytesSent";
            this.labelOriginalBytesSent.Size = new System.Drawing.Size( 30, 13 );
            this.labelOriginalBytesSent.TabIndex = 13;
            this.labelOriginalBytesSent.Text = "0 / 0";
            // 
            // labelModifiedBytesSent
            // 
            this.labelModifiedBytesSent.AutoSize = true;
            this.labelModifiedBytesSent.Location = new System.Drawing.Point( 376, 140 );
            this.labelModifiedBytesSent.Name = "labelModifiedBytesSent";
            this.labelModifiedBytesSent.Size = new System.Drawing.Size( 30, 13 );
            this.labelModifiedBytesSent.TabIndex = 14;
            this.labelModifiedBytesSent.Text = "0 / 0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point( 13, 160 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size( 101, 13 );
            this.label5.TabIndex = 15;
            this.label5.Text = "Original Conversion:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point( 13, 224 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size( 115, 13 );
            this.label6.TabIndex = 16;
            this.label6.Text = "Original Preprocessing:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point( 13, 192 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size( 106, 13 );
            this.label7.TabIndex = 17;
            this.label7.Text = "Modified Conversion:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point( 13, 256 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size( 120, 13 );
            this.label8.TabIndex = 18;
            this.label8.Text = "Modified Preprocessing:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point( 13, 288 );
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size( 65, 13 );
            this.label9.TabIndex = 19;
            this.label9.Text = "Comparison:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point( 13, 320 );
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size( 100, 13 );
            this.label10.TabIndex = 20;
            this.label10.Text = "Results Processing:";
            // 
            // labelResultsTime
            // 
            this.labelResultsTime.AutoSize = true;
            this.labelResultsTime.Location = new System.Drawing.Point( 147, 320 );
            this.labelResultsTime.Name = "labelResultsTime";
            this.labelResultsTime.Size = new System.Drawing.Size( 13, 13 );
            this.labelResultsTime.TabIndex = 26;
            this.labelResultsTime.Text = "0";
            // 
            // labelComparisonTime
            // 
            this.labelComparisonTime.AutoSize = true;
            this.labelComparisonTime.Location = new System.Drawing.Point( 147, 288 );
            this.labelComparisonTime.Name = "labelComparisonTime";
            this.labelComparisonTime.Size = new System.Drawing.Size( 13, 13 );
            this.labelComparisonTime.TabIndex = 25;
            this.labelComparisonTime.Text = "0";
            // 
            // labelModifiedPreTime
            // 
            this.labelModifiedPreTime.AutoSize = true;
            this.labelModifiedPreTime.Location = new System.Drawing.Point( 147, 256 );
            this.labelModifiedPreTime.Name = "labelModifiedPreTime";
            this.labelModifiedPreTime.Size = new System.Drawing.Size( 13, 13 );
            this.labelModifiedPreTime.TabIndex = 24;
            this.labelModifiedPreTime.Text = "0";
            // 
            // labelModifiedConversionTime
            // 
            this.labelModifiedConversionTime.AutoSize = true;
            this.labelModifiedConversionTime.Location = new System.Drawing.Point( 147, 192 );
            this.labelModifiedConversionTime.Name = "labelModifiedConversionTime";
            this.labelModifiedConversionTime.Size = new System.Drawing.Size( 13, 13 );
            this.labelModifiedConversionTime.TabIndex = 23;
            this.labelModifiedConversionTime.Text = "0";
            // 
            // labelOriginalPreTime
            // 
            this.labelOriginalPreTime.AutoSize = true;
            this.labelOriginalPreTime.Location = new System.Drawing.Point( 147, 224 );
            this.labelOriginalPreTime.Name = "labelOriginalPreTime";
            this.labelOriginalPreTime.Size = new System.Drawing.Size( 13, 13 );
            this.labelOriginalPreTime.TabIndex = 22;
            this.labelOriginalPreTime.Text = "0";
            // 
            // labelOriginalConversionTime
            // 
            this.labelOriginalConversionTime.AutoSize = true;
            this.labelOriginalConversionTime.Location = new System.Drawing.Point( 147, 160 );
            this.labelOriginalConversionTime.Name = "labelOriginalConversionTime";
            this.labelOriginalConversionTime.Size = new System.Drawing.Size( 13, 13 );
            this.labelOriginalConversionTime.TabIndex = 21;
            this.labelOriginalConversionTime.Text = "0";
            // 
            // timerTest
            // 
            this.timerTest.Tick += new System.EventHandler( this.timerTest_Tick );
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point( 147, 346 );
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size( 13, 13 );
            this.labelTotal.TabIndex = 28;
            this.labelTotal.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point( 13, 346 );
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size( 34, 13 );
            this.label12.TabIndex = 27;
            this.label12.Text = "Total:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 818, 381 );
            this.Controls.Add( this.labelTotal );
            this.Controls.Add( this.label12 );
            this.Controls.Add( this.labelResultsTime );
            this.Controls.Add( this.labelComparisonTime );
            this.Controls.Add( this.labelModifiedPreTime );
            this.Controls.Add( this.labelModifiedConversionTime );
            this.Controls.Add( this.labelOriginalPreTime );
            this.Controls.Add( this.labelOriginalConversionTime );
            this.Controls.Add( this.label10 );
            this.Controls.Add( this.label9 );
            this.Controls.Add( this.label8 );
            this.Controls.Add( this.label7 );
            this.Controls.Add( this.label6 );
            this.Controls.Add( this.label5 );
            this.Controls.Add( this.labelModifiedBytesSent );
            this.Controls.Add( this.labelOriginalBytesSent );
            this.Controls.Add( this.textModifiedFile );
            this.Controls.Add( this.textOriginalFile );
            this.Controls.Add( this.butSelectModified );
            this.Controls.Add( this.butSelectOriginal );
            this.Controls.Add( this.labelModifiedBytesPerSec );
            this.Controls.Add( this.labelOriginalBytesPerSec );
            this.Controls.Add( this.label4 );
            this.Controls.Add( this.label3 );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.progressModified );
            this.Controls.Add( this.progressOriginal );
            this.Controls.Add( this.butStart );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Workshare Compare Service Control Sample";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.ProgressBar progressOriginal;
        private System.Windows.Forms.ProgressBar progressModified;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelOriginalBytesPerSec;
        private System.Windows.Forms.Label labelModifiedBytesPerSec;
        private System.Windows.Forms.OpenFileDialog dlgOpenOriginalFile;
        private System.Windows.Forms.OpenFileDialog dlgOpenModifiedFile;
        private System.Windows.Forms.Button butSelectOriginal;
        private System.Windows.Forms.Button butSelectModified;
        private System.Windows.Forms.TextBox textOriginalFile;
        private System.Windows.Forms.TextBox textModifiedFile;
        private System.Windows.Forms.Label labelOriginalBytesSent;
        private System.Windows.Forms.Label labelModifiedBytesSent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelResultsTime;
        private System.Windows.Forms.Label labelComparisonTime;
        private System.Windows.Forms.Label labelModifiedPreTime;
        private System.Windows.Forms.Label labelModifiedConversionTime;
        private System.Windows.Forms.Label labelOriginalPreTime;
        private System.Windows.Forms.Label labelOriginalConversionTime;
        private System.Windows.Forms.Timer timerTest;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label label12;
    }
}

