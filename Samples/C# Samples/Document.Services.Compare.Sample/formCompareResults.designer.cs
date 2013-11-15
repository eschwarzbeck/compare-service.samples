namespace Workshare.Document.Services.Compare.Sample
{
    partial class formCompareResults
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
            if( disposing && ( components != null ) )
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
            this.rtfResults = new System.Windows.Forms.RichTextBox();
            this.butOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtfResults
            // 
            this.rtfResults.BackColor = System.Drawing.Color.White;
            this.rtfResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtfResults.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtfResults.Location = new System.Drawing.Point( 2, 2 );
            this.rtfResults.Name = "rtfResults";
            this.rtfResults.ReadOnly = true;
            this.rtfResults.Size = new System.Drawing.Size( 837, 663 );
            this.rtfResults.TabIndex = 0;
            this.rtfResults.TabStop = false;
            this.rtfResults.Text = "";
            // 
            // butOK
            // 
            this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butOK.Location = new System.Drawing.Point( 764, 671 );
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size( 75, 23 );
            this.butOK.TabIndex = 1;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            // 
            // formCompareResults
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 842, 696 );
            this.ControlBox = false;
            this.Controls.Add( this.butOK );
            this.Controls.Add( this.rtfResults );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formCompareResults";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compare Results";
            this.TopMost = true;
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtfResults;
        private System.Windows.Forms.Button butOK;
    }
}