using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Document.Services.Compare.Control
{
    public enum TransportProtocolEnum { Http, Tcp, NamedPipe };

	public enum ResponseOptions
	{
		Rtf = 1,
		Xml = 2,
		RtfWithSummary = 3,
		Wdf = 4,
		WdfWithSummary = 6,
		Doc = 8,
		DocWithSummary = 10,
		DocX = 16,
		DocXWithSummary = 18,
		Pdf = 32,
		PdfWithSummary = 34
	};

    public interface ICompareService
    {
        bool                    UseChunking { get; set; }
        int                     ChunkSize { get; set; }
        ResponseOptions         ComparisonOutput { get; set; }
        string                  CompareOptions { get; set; }
        TransportProtocolEnum   TransportProtocol { get; }

        void    SetClientCredentials( string user, string password, string domain );
        void    SetTimeouts( int minsOpen, int minsClose, int minsSend, int minsReceive );
        bool    VerifyConnection( out string serviceVersion, out string compositorVersion );

        CompareResults      Compare( Stream original, Stream modified );
        CompareResults[]    Compare( Stream original, Stream[] modified );

        int                 BenchmarkTransfer( int chunkSize, long sizeInputBytes );
        void                BenchmarkTransferAsync( int chunkTestSize, long sizeInputBytes );
        PerformanceResults  BenchmarkCompare( Stream original, Stream modified );

        event EventHandler<DataSentArgs> DataSent;
        event EventHandler ComparisonStarted;
    }

    public class CompareResults
    {
        byte[] dataRedline;
        string changeSummary;

        internal CompareResults( ComparerProxy.CompareResults results)
        {
            this.dataRedline = results.Redline;
            this.changeSummary = results.Summary;
        }

        // Byte array containing either an RTF redline or WDF composite file      
        public byte[] Redline
        {
            get { return dataRedline; }
            set { dataRedline = value; }
        }

        // String buffer containing XML change summary data     
        public string ChangeSummary
        {
            get { return changeSummary; }
            set { changeSummary = value; }
        }
    }

    public class PerformanceResults
    {
        TimeSpan originalConversionTime;
        TimeSpan modifiedConversionTime;
        TimeSpan originalPreProcessingTime;
        TimeSpan modifiedPreProcessingTime;
        TimeSpan comparisonTime;
        TimeSpan resultsProcessingTime;      
        TimeSpan totalExecutionTime;

        internal PerformanceResults( ComparerProxy.PerformanceResults results )
        {
            originalConversionTime = results.OriginalConversionTime;
            modifiedConversionTime = results.ModifiedConversionTime;
            originalPreProcessingTime = results.OriginalPreProcessingTime;
            modifiedPreProcessingTime = results.ModifiedPreProcessingTime;
            comparisonTime = results.ComparisonTime;
            resultsProcessingTime = results.ResultsProcessingTime;
            totalExecutionTime = results.TotalExecutionTime;
        }

        public TimeSpan OriginalConversionTime
        {
            get { return originalConversionTime; }
            set { originalConversionTime = value; }
        }

        public TimeSpan ModifiedConversionTime
        {
            get { return modifiedConversionTime; }
            set { modifiedConversionTime = value; }
        }

        public TimeSpan OriginalPreProcessingTime
        {
            get { return originalPreProcessingTime; }
            set { originalPreProcessingTime = value; }
        }

        public TimeSpan ModifiedPreProcessingTime
        {
            get { return modifiedPreProcessingTime; }
            set { modifiedPreProcessingTime = value; }
        }

        public TimeSpan ComparisonTime
        {
            get { return comparisonTime; }
            set { comparisonTime = value; }
        }

        public TimeSpan ResultsProcessingTime
        {
            get { return resultsProcessingTime; }
            set { resultsProcessingTime = value; }
        }

        public TimeSpan TotalExecutionTime
        {
            get { return totalExecutionTime; }
            set { totalExecutionTime = value; }
        }
    }

    public class DataSentArgs : System.EventArgs
    {
        private bool isOriginalFile;
        private long bytesSent;
        private long bytesTotal;

        internal DataSentArgs( bool originalFile, long bytesSent, long bytesTotal )
        {
            this.isOriginalFile = originalFile;
            this.bytesSent = bytesSent;
            this.bytesTotal = bytesTotal;
        }

        public bool IsOriginalFile
        {
            get
            {
                return this.isOriginalFile;
            }
            set
            {
                this.isOriginalFile = value;
            }
        }
        public long BytesSent
        {
            get
            {
                return this.bytesSent;
            }
            set
            {
                this.bytesSent = value;
            }
        }
        public long BytesTotal
        {
            get
            {
                return this.bytesTotal;
            }
            set
            {
                this.bytesTotal = value;
            }
        }
    }

    public class CompareService : ICompareService
    {
    #region private members
        private int chunkSize = 1024 * 1024;       
        private bool useChunking = true;
        private TimeSpan timeOpen = new TimeSpan(0,1,0);
        private TimeSpan timeClose = new TimeSpan( 0, 1, 0 );
        private TimeSpan timeSend = new TimeSpan( 0, 10, 0 );
        private TimeSpan timeReceive = new TimeSpan( 0, 10, 0 );
        private string server = "";
        private int port = -1;     
        private System.Net.NetworkCredential networkCredential = null;       
        private TransportProtocolEnum transportProtocol = TransportProtocolEnum.Http;
        private ResponseOptions responseOptions = ResponseOptions.Rtf;
        private string compareOptions = "";

        private delegate void AppendDelegate( byte[] b, int i );

		#region Property to change protocol, service, security etc

		private readonly string _serviceUrl = "";
    	private readonly string _secuity = "Message";
		private string _binding = "WSHttpBinding";
    	private readonly string _transportClientSecurity = "Ntlm";
		private readonly MessageCredentialType _messageClientSecurity = MessageCredentialType.Windows;


		#endregion
	#endregion

		#region events
		public event EventHandler<DataSentArgs> DataSent;
        public event EventHandler ComparisonStarted;
    #endregion

    #region properties        
        public TransportProtocolEnum TransportProtocol { get { return this.transportProtocol; } }
        public ResponseOptions ComparisonOutput { get { return this.responseOptions; } set { this.responseOptions = value; } }
        public string CompareOptions { get { return this.compareOptions; } set { this.compareOptions = value; } }
        public bool UseChunking { get { return this.useChunking; } set { this.useChunking = value; } }
        public int ChunkSize { get { return this.chunkSize; } set { this.chunkSize = value; } }
    #endregion

    #region public methods
        static public ICompareService CreateHttpService( string host )
        {
            return CreateHttpService( host, -1 );
        }

        static public ICompareService CreateHttpService( string host, int port )
        {
            return new CompareService( host, port, TransportProtocolEnum.Http );
        }

		static public ICompareService CreateHttpService(string host, int port, string url, string binding, string securityMode, string transportSecurity, string messageSecurity)
		{
			try
			{
				var message = MessageCredentialType.Windows;

				if (string.IsNullOrEmpty(messageSecurity) == false)
				{
					message = (MessageCredentialType) Enum.Parse(typeof(MessageCredentialType), messageSecurity);
				}

				return new CompareService(host, port, TransportProtocolEnum.Http, url, binding, securityMode, transportSecurity, message);
			}
			catch(Exception)
			{
				return null;
			}
		}

        static public ICompareService CreateTcpService( string host, int port )
        {
            return new CompareService( host, port, TransportProtocolEnum.Tcp );
        }

        static public ICompareService CreateNamedPipeService()
        {
            return new CompareService( "localhost", -1, TransportProtocolEnum.NamedPipe );
        }

        public void SetClientCredentials( string user, string password, string domain )
        {
            if( String.IsNullOrEmpty( user ) || String.IsNullOrEmpty( password ) || String.IsNullOrEmpty( domain ) )
            {
                throw new System.Security.SecurityException( "SetClientCredentials cannot be called with empty values" );
            }

            if( this.networkCredential == null )
            {
                this.networkCredential = new System.Net.NetworkCredential();
            }

            this.networkCredential.UserName = user;
            this.networkCredential.Password = password;
            this.networkCredential.Domain = domain;
        }

        public void SetTimeouts( int minsOpen, int minsClose, int minsSend, int minsReceive )
        {
            this.timeOpen = new TimeSpan( 0, minsOpen, 0 );
            this.timeClose = new TimeSpan( 0, minsClose, 0 );
            this.timeSend = new TimeSpan( 0, minsSend, 0 );
            this.timeReceive = new TimeSpan( 0, minsReceive, 0 );
        }

        public bool VerifyConnection( out string serviceVersion, out string compositorVersion )
        {
            CheckInitialised();

            if( this.useChunking )
            {
                return TestConnectionInternalChunked( out serviceVersion, out compositorVersion );
            }
            else
            {
                return TestConnectionInternal( out serviceVersion, out compositorVersion );
            }
        }

        public CompareResults Compare( Stream original, Stream modified )
        {
            CheckInitialised();

            if( useChunking )
            {
                return CompareInternalChunked( original, modified );
            }
            else
            {
                return CompareInternal( original, modified );
            }
        }

        public CompareResults[] Compare( Stream original, Stream[] modified )
        {
            CheckInitialised();

            return CompareInternalMultiple( original, modified);
        }

        public int BenchmarkTransfer( int chunkTestSize, long sizeInputBytes )
        {
            CheckInitialised();
          
            return BenchmarkTransferWorker( chunkTestSize, sizeInputBytes );
        }

        public void BenchmarkTransferAsync( int chunkTestSize, long sizeInputBytes )
        {
            CheckInitialised();

            System.ComponentModel.BackgroundWorker benchmarkTransferThread = new System.ComponentModel.BackgroundWorker();
            benchmarkTransferThread.DoWork += new System.ComponentModel.DoWorkEventHandler( benchmarkTransferThread_DoWork );
            BenchmarkTransferAsyncData data;
            data.chunkTestSize = chunkTestSize;
            data.sizeInputBytes = sizeInputBytes;

            benchmarkTransferThread.RunWorkerAsync(data);       
        }

        void benchmarkTransferThread_DoWork( object sender, System.ComponentModel.DoWorkEventArgs e )
        {
            BenchmarkTransferAsyncData data = (BenchmarkTransferAsyncData)e.Argument;
            e.Result = BenchmarkTransferWorker( data.chunkTestSize, data.sizeInputBytes );
        }     

        public PerformanceResults BenchmarkCompare( Stream streamOriginal, Stream streamModified )
        {
            try
            {
                ComparerProxy.ComparerChunkedClient comparer = new ComparerProxy.ComparerChunkedClient( GetBinding(), GetEndpointAddress() );
                comparer.ClientCredentials.Windows.ClientCredential = this.networkCredential;

                comparer.Open();

                System.Guid guidOriginal = comparer.InitialiseFile( "Original", streamOriginal.Length );
                System.Guid guidModified = comparer.InitialiseFile( "Modified", streamModified.Length );

                SendData( comparer, guidOriginal, streamOriginal, true );
                SendData( comparer, guidModified, streamModified, false );

                if( this.ComparisonStarted != null )
                {
                    this.ComparisonStarted( this, new EventArgs() );
                }

                ComparerProxy.PerformanceResults results = comparer.Benchmark( guidOriginal, guidModified, (ComparerProxy.ResponseOptions)this.responseOptions, this.compareOptions );

                comparer.ReleaseAll();
                comparer.Close();

                if( results != null )
                {
                    return new PerformanceResults( results );
                }
            }
            catch( System.Exception ex )
            {
                System.Diagnostics.Trace.WriteLine( ex.Message );
                throw;
            }

            return null;           
        }

    #endregion

    #region private methods
        private CompareService( string host, int port, TransportProtocolEnum transportProtocol )
        {
            this.transportProtocol = transportProtocol;            
            this.server = host;
            this.port = port;            
        }

		private CompareService(string host, int port, TransportProtocolEnum transportProtocol, string url, string binding, string mode, string transport, MessageCredentialType message)
		{
			this.transportProtocol = transportProtocol;
			this.server = host;
			this.port = port;
			this._serviceUrl = url;
			_secuity = mode;
			_binding = binding;
			_transportClientSecurity = transport;
			_messageClientSecurity = message;
		}

        private struct BenchmarkTransferAsyncData
        {
            public int chunkTestSize;
            public long sizeInputBytes;
        }

        private int BenchmarkTransferWorker( int chunkTestSize, long sizeInputBytes )
        {
            try
            {
                this.useChunking = true;
                ComparerProxy.ComparerChunkedClient comparer = new ComparerProxy.ComparerChunkedClient( GetBinding(), GetEndpointAddress() );
                comparer.ClientCredentials.Windows.ClientCredential = this.networkCredential;

                comparer.Open();

                System.Guid guidFile = comparer.InitialiseFile( "Test File", sizeInputBytes );

                long sentBytes = 0;

                if( this.DataSent != null )
                {
                    DataSent( this, new DataSentArgs( true, sentBytes, sizeInputBytes ) );
                }

                DateTime timeStart = DateTime.Now;

                while( sentBytes < sizeInputBytes )
                {
                    long actualChunkSize = Math.Min( chunkTestSize, sizeInputBytes - sentBytes );
                    byte[] dataChunk = new byte[actualChunkSize];

                    comparer.AppendFile( guidFile, dataChunk, sentBytes );

                    sentBytes += actualChunkSize;

                    if( this.DataSent != null )
                    {
                        DataSent( this, new DataSentArgs( true, sentBytes, sizeInputBytes ) );
                    }
                }

                int milliseconds = (int)(DateTime.Now - timeStart).TotalMilliseconds;

                comparer.ReleaseAll();
                comparer.Close();

                return milliseconds;
            }
            catch( System.Exception ex )
            {
                System.Diagnostics.Trace.WriteLine( ex.Message );
                throw;
            }
        }

        private bool TestConnectionInternal( out string serviceVersion, out string compositorVersion )
        {           
            ComparerProxy.ComparerClient comparer = null;

            try
            {
                comparer = new ComparerProxy.ComparerClient( GetBinding(), GetEndpointAddress() );
                comparer.ClientCredentials.Windows.ClientCredential = this.networkCredential;
             
                comparer.Open();

                if( comparer.Authenticate( "Realm", "User", "Password" ) )
                {
                    serviceVersion = comparer.GetVersion();
                    compositorVersion = comparer.GetCompositorVersion();
                    return true;
                }
            }
            catch( System.InvalidOperationException ioe )
            {
                System.Diagnostics.Trace.WriteLine( ioe.Message );
                if( ioe.Message.Contains( "Could not find default endpoint element" ) )
                {
                    throw new System.Exception( "Unable to create the ComparerClient.  Please check that your Web.Config file is correctly configured." );
                }
            }
            catch( System.TimeoutException te )
            {
                System.Diagnostics.Trace.WriteLine( te.Message );
                if( comparer != null && comparer.State != System.ServiceModel.CommunicationState.Opened )
                {
                    throw new System.Exception( "Unable to connect to server host " + this.server );
                }
            }
            catch( System.ServiceModel.EndpointNotFoundException enfe )
            {
                System.Diagnostics.Trace.WriteLine( enfe.Message );
                throw new System.Exception( "Unable to find service endpoint.  Please check the host/port address is correct " + this.server );
            }
            catch( System.Exception ex )
            {
                System.Diagnostics.Trace.WriteLine( ex.Message );
            }
            finally
            {
                if( comparer != null && comparer.State != System.ServiceModel.CommunicationState.Faulted )
                {
                    comparer.Close();
                }
            }

            serviceVersion = "";
            compositorVersion = "";
            return false;
        }

        private bool TestConnectionInternalChunked( out string serviceVersion, out string compositorVersion )
        {
            ComparerProxy.ComparerChunkedClient comparer = null;

            try
            {
                comparer = new ComparerProxy.ComparerChunkedClient( GetBinding(), GetEndpointAddress() );
                comparer.ClientCredentials.Windows.ClientCredential = this.networkCredential;

                comparer.Open();

                if( comparer.Authenticate( "Realm", "User", "Password" ) )
                {
                    serviceVersion = comparer.GetVersion();
                    compositorVersion = comparer.GetCompositorVersion();
                    return true;
                }
            }
            catch( System.InvalidOperationException ioe )
            {
                System.Diagnostics.Trace.WriteLine( ioe.Message );
                if( ioe.Message.Contains( "Could not find default endpoint element" ) )
                {
                    throw new System.Exception( "Unable to create the ComparerClient.  Please check that your Web.Config file is correctly configured." );
                }
            }
            catch( System.TimeoutException te )
            {
                System.Diagnostics.Trace.WriteLine( te.Message );
                if( comparer != null && comparer.State != System.ServiceModel.CommunicationState.Opened )
                {
                    throw new System.Exception( "Unable to connect to server host " + this.server );
                }
            }
            catch( System.ServiceModel.EndpointNotFoundException enfe )
            {
                System.Diagnostics.Trace.WriteLine( enfe.Message );
                throw new System.Exception( "Unable to find service endpoint.  Please check the host/port address is correct " + this.server );
            }
            catch( System.Exception ex )
            {
                System.Diagnostics.Trace.WriteLine( ex.Message );
            }
            finally
            {
                if( comparer != null && comparer.State != System.ServiceModel.CommunicationState.Faulted )
                {
                    comparer.Close();
                }
            }

            serviceVersion = "";
            compositorVersion = "";
            return false;
        }

        private CompareResults CompareInternalChunked( Stream streamOriginal, Stream streamModified )
        {
            try
            {
                ComparerProxy.ComparerChunkedClient comparer = new ComparerProxy.ComparerChunkedClient( GetBinding(), GetEndpointAddress() );
                comparer.ClientCredentials.Windows.ClientCredential = this.networkCredential;

                comparer.Open();

                System.Guid guidOriginal = comparer.InitialiseFile( "Original", streamOriginal.Length );
                System.Guid guidModified = comparer.InitialiseFile( "Modified", streamModified.Length );

                SendData( comparer, guidOriginal, streamOriginal, true );
                SendData( comparer, guidModified, streamModified, false );

                if( this.ComparisonStarted != null )
                {
                    this.ComparisonStarted( this, new EventArgs() );
                }

                ComparerProxy.CompareResults results = comparer.Compare( guidOriginal, guidModified, (ComparerProxy.ResponseOptions)this.responseOptions, this.compareOptions );

                comparer.ReleaseAll();
                comparer.Close();

                if( results != null )
                {
                    return new CompareResults( results );
                }
            }            
            catch( System.ServiceModel.CommunicationException comex )
            {
                System.Diagnostics.Trace.WriteLine( comex.Message );
                throw;
            }
            catch( System.Exception ex )
            {
                System.Diagnostics.Trace.WriteLine( ex.Message );
                throw;
            }

            return null;
        }

        private CompareResults[] CompareInternalMultiple( Stream streamOriginal, Stream[] streamsModified )
        {
            try
            {
                ComparerProxy.ComparerChunkedClient comparer = new ComparerProxy.ComparerChunkedClient( GetBinding(), GetEndpointAddress() );
                comparer.ClientCredentials.Windows.ClientCredential = this.networkCredential;

                CompareResults[] results = new CompareResults[streamsModified.Length];

                comparer.Open();

                System.Guid guidOriginal = comparer.InitialiseFile( "Original", streamOriginal.Length );
                SendData( comparer, guidOriginal, streamOriginal, true );

                int resultIndex = 0;

                foreach( System.IO.Stream streamModified in streamsModified )
                {
                    System.Guid guidModified = comparer.InitialiseFile( "Modified" + (resultIndex+1), streamModified.Length );

                    SendData( comparer, guidModified, streamModified, false );

                    if( this.ComparisonStarted != null )
                    {
                        this.ComparisonStarted( this, new EventArgs() );
                    }

                    ComparerProxy.CompareResults result = comparer.Compare( guidOriginal, guidModified, (ComparerProxy.ResponseOptions)this.responseOptions, this.compareOptions );
                    if( result != null )
                    {
                        results[resultIndex] = new CompareResults( result );
                    }

                    comparer.ReleaseFile( guidModified );

                    resultIndex++;
                }

                comparer.ReleaseAll();
                comparer.Close();

                return results;
            }
            catch( System.Exception ex )
            {
                System.Diagnostics.Trace.WriteLine( ex.Message );
                throw;
            }           
        }

        private CompareResults CompareInternal( Stream streamOriginal, Stream streamModified )
        {
            try
            {
                ComparerProxy.CompareResults results = null;

                ComparerProxy.ComparerClient comparer = new ComparerProxy.ComparerClient( GetBinding(), GetEndpointAddress() );
                comparer.ClientCredentials.Windows.ClientCredential = this.networkCredential;

                comparer.Open();

                if( comparer.Authenticate( "Realm", "User", "Password" ) )
                {
                    int sizeOriginal = (int)streamOriginal.Length;
                    int sizeModified = (int)streamModified.Length;
                    byte[] dataOriginal = new byte[sizeOriginal];
                    byte[] dataModified = new byte[sizeModified];

                    if( sizeOriginal != streamOriginal.Read( dataOriginal, 0, sizeOriginal ) )
                    {
                        throw new System.Exception( "Unable to completely read Original stream" );
                    }

                    if( sizeModified != streamModified.Read( dataModified, 0, sizeModified ) )
                    {
                        throw new System.Exception( "Unable to completely read Modified stream" );
                    }

                    results = comparer.Execute( dataOriginal, dataModified, (ComparerProxy.ResponseOptions)this.responseOptions, this.compareOptions );

                    dataOriginal = null;
                    dataModified = null;
                }

                comparer.Close();

                if( results != null )
                {
                    return new CompareResults( results );
                }
            }
            catch( System.Exception ex )
            {
                System.Diagnostics.Trace.WriteLine( ex.Message );
                throw;
            }

            return null;
        }             

        private void CheckInitialised()
        {
            if( this.transportProtocol != TransportProtocolEnum.NamedPipe &&
                this.networkCredential == null )
            {
                throw new System.Exception( "You must SetClientCredentials before calling other methods." );
            }

            if( this.transportProtocol != TransportProtocolEnum.NamedPipe &&
                String.IsNullOrEmpty( this.server ) )
            {
                throw new System.Exception( "Server name is empty or invalid." );
            }
        }

        private void SendData( ComparerProxy.ComparerChunkedClient comparer, Guid guidFile, Stream streamInput, bool isOriginalFile )
        {
            long sizeInput = streamInput.Length;
            long sentBytes = 0;

            if( this.DataSent != null )
            {
                DataSent( this, new DataSentArgs( isOriginalFile, sentBytes, sizeInput ) );
            }

            while( sentBytes < sizeInput )
            {              
                long actualChunkSize = Math.Min( this.chunkSize, sizeInput - sentBytes );
                byte[] dataChunk = new byte[actualChunkSize];

                streamInput.Read( dataChunk, 0, dataChunk.Length );
                            
                comparer.AppendFile( guidFile, dataChunk, sentBytes );
               
                sentBytes += actualChunkSize;

                if( this.DataSent != null )
                {
                    DataSent( this, new DataSentArgs( isOriginalFile, sentBytes, sizeInput ) );
                }
            }
        }

        private Binding GetBinding()
        {
            switch( this.transportProtocol )
            {
                case TransportProtocolEnum.Http:
					if (string.Equals("WSHttpBinding", _binding, StringComparison.OrdinalIgnoreCase))
					{
						return CreateWsHttpBinding();
					}
					return CreateBasicHttpBinding();
            	case TransportProtocolEnum.NamedPipe:
                    return CreateNamedPipeBinding();

            	case TransportProtocolEnum.Tcp:
                    return CreateTcpBinding();

            	default:
                    throw new System.NotImplementedException( transportProtocol.ToString() + " is not currently supported" );                   
            }           
        }

    	private Binding CreateTcpBinding() 
		{
			var security = SecurityMode.Message;

			if (string.IsNullOrEmpty(_secuity) == false)
			{
				security = (SecurityMode) Enum.Parse(typeof(SecurityMode), _secuity);
			}
    		var tcpBinding = new NetTcpBinding
    		                 	{
    		                 		OpenTimeout = timeOpen,
    		                 		CloseTimeout = timeClose,
    		                 		SendTimeout = timeSend,
    		                 		ReceiveTimeout = timeReceive,
    		                 		MaxBufferPoolSize = Int32.MaxValue,
    		                 		MaxReceivedMessageSize = Int32.MaxValue,
    		                 		ReaderQuotas =
    		                 			{
    		                 				MaxArrayLength = Int32.MaxValue,
    		                 				MaxStringContentLength = Int32.MaxValue
    		                 			},
    		                 		TransferMode = TransferMode.Buffered
    		                 	};

    		tcpBinding.ReliableSession.Enabled = true;
    		tcpBinding.ReliableSession.InactivityTimeout = timeReceive;
    		tcpBinding.ReliableSession.Ordered = true;
			tcpBinding.Security.Mode = security;
    		return tcpBinding;
    	}

    	private Binding CreateNamedPipeBinding() 
		{
    		var pipeBinding = new NetNamedPipeBinding
    		                  	{
    		                  		OpenTimeout = timeOpen,
    		                  		CloseTimeout = timeClose,
    		                  		SendTimeout = timeSend,
    		                  		ReceiveTimeout = timeReceive,
    		                  		MaxBufferPoolSize = Int32.MaxValue,
    		                  		MaxReceivedMessageSize = Int32.MaxValue,
    		                  		ReaderQuotas =
    		                  			{
    		                  				MaxArrayLength = Int32.MaxValue,
    		                  				MaxStringContentLength = Int32.MaxValue
    		                  			}
    		                  	};

    		return pipeBinding;
    	}

    	private Binding CreateBasicHttpBinding() 
		{
			var security = BasicHttpSecurityMode.TransportCredentialOnly;
    		var transport = HttpClientCredentialType.Ntlm;
    		var message = BasicHttpMessageCredentialType.UserName;

			if (string.IsNullOrEmpty(_secuity) == false)
			{
				security = (BasicHttpSecurityMode) Enum.Parse(typeof(BasicHttpSecurityMode), _secuity);
			}

			if (string.IsNullOrEmpty(_transportClientSecurity) == false)
			{
				transport = (HttpClientCredentialType) Enum.Parse(typeof(HttpClientCredentialType), _transportClientSecurity);
			}
    		if ( _messageClientSecurity == MessageCredentialType.Certificate)
    		{
   				message = BasicHttpMessageCredentialType.Certificate;
    		}
			
			var binding = new BasicHttpBinding
			              	{
			              		OpenTimeout = timeOpen,
			              		CloseTimeout = timeClose,
			              		SendTimeout = timeSend,
			              		ReceiveTimeout = timeReceive,
                                MaxBufferPoolSize = 167772160,
                                MaxReceivedMessageSize = 167772160,
			              		ReaderQuotas =
			              			{
                                        MaxArrayLength = 134217728,
                                        MaxStringContentLength = 134217728,
                                        MaxBytesPerRead = 134217728,
                                        MaxNameTableCharCount = 134217728,
                                        MaxDepth = 4096
			              			},
			              		MessageEncoding = WSMessageEncoding.Mtom
			              	};

    		binding.Security.Mode = security;
    		binding.Security.Transport.ClientCredentialType = transport;
			binding.Security.Message.ClientCredentialType = message;

    		return binding;
    	}

    	private Binding CreateWsHttpBinding() 
		{
    		var security = SecurityMode.Message;
    		var transport = HttpClientCredentialType.Windows;

			if (string.IsNullOrEmpty(_secuity) == false)
			{
				security = (SecurityMode) Enum.Parse(typeof(SecurityMode), _secuity);
			}

			if (string.IsNullOrEmpty(_transportClientSecurity) == false)
			{
				transport = (HttpClientCredentialType) Enum.Parse(typeof(HttpClientCredentialType), _transportClientSecurity);
			}

    		var wsBinding = new WSHttpBinding
    		                	{
    		                		OpenTimeout = timeOpen,
    		                		CloseTimeout = timeClose,
    		                		SendTimeout = timeSend,
    		                		ReceiveTimeout = timeReceive,
                                    MaxBufferPoolSize = 167772160,
                                    MaxReceivedMessageSize = 167772160,
    		                		ReaderQuotas =
    		                			{
                                            MaxArrayLength = 134217728,
                                            MaxStringContentLength = 134217728,
                                            MaxBytesPerRead = 134217728,
                                            MaxNameTableCharCount = 134217728,
                                            MaxDepth = 4096
    		                			},
    		                		MessageEncoding = WSMessageEncoding.Mtom
    		                	};

    		wsBinding.ReliableSession.Enabled = true;
    		wsBinding.ReliableSession.InactivityTimeout = timeReceive;
    		wsBinding.ReliableSession.Ordered = true;
			wsBinding.Security.Mode = security;
			wsBinding.Security.Transport.ClientCredentialType = transport;
    		wsBinding.Security.Message.ClientCredentialType = _messageClientSecurity;

    		return wsBinding;
    	}

    	private EndpointAddress GetEndpointAddress()
        {        
            string scheme = "";

            switch( this.transportProtocol )
            {
                case TransportProtocolEnum.Http:
                    scheme = "http";
                    break;

                case TransportProtocolEnum.NamedPipe:
                    scheme = "net.pipe";
                    break;

                case TransportProtocolEnum.Tcp:
                    scheme = "net.tcp";
                    break;

                default:
                    throw new System.NotImplementedException( "GetUri failed: " + transportProtocol + " is not currently supported" );
            }

            string servicePart = this.useChunking ? "Chunked" : "Compare5";

        	var uriPath = string.Format(CultureInfo.CurrentCulture,
        	                            "{0}/{1}",
        	                            string.IsNullOrEmpty(_serviceUrl) ? "Comparer" : _serviceUrl,
        	                            servicePart);

            UriBuilder uriBuilder = new UriBuilder( scheme, this.server, this.port, uriPath );
            return new EndpointAddress( uriBuilder.Uri );
        }
    #endregion
    }
}
