using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Document.Services.Compare.Control.Sample
{
    public partial class frmMain : Form
    {
        private enum TransportProtocolEnum { Http, Tcp, NamedPipe };

        private DateTime timeStartSendingOriginal;
        private DateTime timeStartSendingModified;

        public frmMain()
        {
            InitializeComponent();

            this.progressOriginal.Minimum = 0;
            this.progressOriginal.Maximum = 100;
            this.progressOriginal.Step = 1;

            this.progressModified.Minimum = 0;
            this.progressModified.Maximum = 100;
            this.progressModified.Step = 1;
        }

        private ICompareService CreateComparer( TransportProtocolEnum transport )
        {
            switch( transport )
            {
                case TransportProtocolEnum.Http:
                    return CompareService.CreateHttpService( "localhost", 8080 );

                case TransportProtocolEnum.NamedPipe:
                    return CompareService.CreateNamedPipeService();

                case TransportProtocolEnum.Tcp:
                    return CompareService.CreateTcpService( "localhost", 8090 );

                default:
                    return null;
            }
        }

        private void BenchmarkCompare()
        {
            System.IO.FileStream fsOriginal = System.IO.File.OpenRead( this.textOriginalFile.Text );
            System.IO.FileStream fsModified = System.IO.File.OpenRead( this.textModifiedFile.Text );

            ICompareService comparer = CreateComparer( TransportProtocolEnum.Http );
            comparer.SetClientCredentials( "lnpair", "lnpair", "wsdev" );
            comparer.SetTimeouts( 1, 1, 10, 10 );

            comparer.DataSent += new EventHandler<DataSentArgs>( comparer_CompareProgress );

            PerformanceResults result = comparer.BenchmarkCompare( fsOriginal, fsModified );

            this.labelOriginalConversionTime.Text = result.OriginalConversionTime.TotalMilliseconds + " ms";
            this.labelModifiedConversionTime.Text = result.ModifiedConversionTime.TotalMilliseconds + " ms";
            this.labelOriginalPreTime.Text = result.OriginalPreProcessingTime.TotalMilliseconds + " ms";
            this.labelModifiedPreTime.Text = result.ModifiedPreProcessingTime.TotalMilliseconds + " ms";
            this.labelComparisonTime.Text = result.ComparisonTime.TotalMilliseconds + " ms";
            this.labelResultsTime.Text = result.ResultsProcessingTime.TotalMilliseconds + " ms";

            this.labelTotal.Text = result.TotalExecutionTime.TotalMilliseconds + " ms";
        }

        private void BenchmarkTransfer()
        {
            this.butStart.Enabled = false;

            {
                ICompareService comparer = CreateComparer( TransportProtocolEnum.Http );
                comparer.SetClientCredentials( "lnpair", "lnpair", "wsdev" );
                comparer.DataSent += new EventHandler<DataSentArgs>( comparer_CompareProgress );

                comparer.BenchmarkTransferAsync( 1024 * 1024, 50 * 1024 * 1024 );
            }

            //{
            //    ICompareService comparer = CreateComparer( TransportProtocolEnum.Tcp );
            //    comparer.SetClientCredentials( "lnpair", "lnpair", "wsdev" );
            //    comparer.DataSent += new EventHandler<DataSentArgs>( comparer_CompareProgress );

            //    comparer.BenchmarkTransferAsync( 1024 * 1024, 50 * 1024 * 1024 );
            //}

            //{
            //    ICompareService comparer = CreateComparer( TransportProtocolEnum.NamedPipe );
            //    comparer.SetClientCredentials( "lnpair", "lnpair", "wsdev" );
            //    comparer.DataSent += new EventHandler<DataSentArgs>( comparer_CompareProgress );

            //    comparer.BenchmarkTransferAsync( 1024 * 1024, 50 * 1024 * 1024 );
            //}
        }

       private delegate void CompareProgressDelegate(object sender, DataSentArgs e);

        private void comparer_CompareProgress( object sender, DataSentArgs e )
        {
            if( this.InvokeRequired )
            {
                this.Invoke( new CompareProgressDelegate( comparer_CompareProgress ), new object[]{sender,e} );
                return;
            }

            if( e.IsOriginalFile )
            {
                if( e.BytesSent == 0 )
                {
                    this.timeStartSendingOriginal = System.DateTime.Now;
                    this.labelOriginalBytesPerSec.Text = "0";
                    this.labelOriginalBytesSent.Text = "0 / 0";

                    System.Diagnostics.Trace.WriteLine( "Starting to send original file" );
                }
                else
                {
                    double numMSecondsEllapsed = (System.DateTime.Now - this.timeStartSendingOriginal).TotalMilliseconds;
                    System.Diagnostics.Trace.WriteLine( "Send original file: " + e.BytesSent + " bytes in " + numMSecondsEllapsed + " ms");

                    if( numMSecondsEllapsed > 0 )
                    {
                        int bytesPerMSecond = (int)((double)e.BytesSent / numMSecondsEllapsed);

                        this.labelOriginalBytesPerSec.Text = (bytesPerMSecond * 1000 / 1024).ToString();
                    }
                    else
                    {
                        this.labelOriginalBytesPerSec.Text = "~";
                    }

                    this.labelOriginalBytesSent.Text = e.BytesSent + " / " + e.BytesTotal;
                    this.progressOriginal.Value = (int)(this.progressOriginal.Maximum * e.BytesSent / e.BytesTotal);

                    if( e.BytesSent == e.BytesTotal )
                    {
                        this.butStart.Enabled = true;
                    }
                }
            }
            else
            {
                if( e.BytesSent == 0 )
                {
                    this.progressModified.Value = this.progressModified.Minimum;
                    this.timeStartSendingModified = System.DateTime.Now;
                    this.labelModifiedBytesPerSec.Text = "0";
                    this.labelModifiedBytesSent.Text = "0 / 0";

                    System.Diagnostics.Trace.WriteLine( "Starting to send modified file" );
                }
                else
                {
                    double numMSecondsEllapsed = (System.DateTime.Now - this.timeStartSendingModified).TotalMilliseconds;
                    System.Diagnostics.Trace.WriteLine( "Send modified file: " + e.BytesSent + " bytes in " + numMSecondsEllapsed + " ms" );

                    if( numMSecondsEllapsed > 0 )
                    {
                        int bytesPerMSecond = (int)((double)e.BytesSent / numMSecondsEllapsed);

                        this.labelModifiedBytesPerSec.Text = (bytesPerMSecond * 1000 / 1024).ToString();
                    }
                    else
                    {
                        this.labelModifiedBytesPerSec.Text = "~";
                    }

                    this.labelModifiedBytesSent.Text = e.BytesSent + " / " + e.BytesTotal;
                    this.progressModified.Value = (int)(this.progressModified.Maximum * e.BytesSent / e.BytesTotal);
                }
            }
        }

        private void butStart_Click( object sender, EventArgs e )
        {
            try
            {
                //BenchmarkCompare();
                BenchmarkTransfer();
            }
            catch( System.Exception ex )
            {
                MessageBox.Show( ex.Message, "Exception" );
            }
        }

        private void butSelectOriginal_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == dlgOpenOriginalFile.ShowDialog( this ) )
            {
                this.textOriginalFile.Text = dlgOpenOriginalFile.FileName;
                EnableStartButton();
            }
        }

        private void butSelectModified_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == dlgOpenModifiedFile.ShowDialog( this ) )
            {
                this.textModifiedFile.Text = dlgOpenModifiedFile.FileName;
                EnableStartButton();
            }
        }

        private void EnableStartButton()
        {
            butStart.Enabled = System.IO.File.Exists( this.textOriginalFile.Text ) && System.IO.File.Exists( this.textModifiedFile.Text );
        }

        private void timerTest_Tick( object sender, EventArgs e )
        {
            timerTest.Stop();

            this.Enabled = false;

            try
            {
                BenchmarkCompare();
                //BenchmarkTransfer();
            }
            catch( System.Exception ex )
            {
                MessageBox.Show( ex.Message, "Exception" );
            }
            finally
            {
                this.Enabled = true;
                timerTest.Start();
            }
        }
    }
}
