using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Workshare.Document.Services.Compare.Sample
{
    public partial class formCompareResults : Form
    {
        public formCompareResults()
        {
            InitializeComponent();
        }

        public void SetRTFResults(string rtfCompareResult)
        {
            this.rtfResults.Rtf = rtfCompareResult;
        }
    }
}