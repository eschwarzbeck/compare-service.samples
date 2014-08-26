using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Diagnostics;
using System.Configuration;
using System.Web.Configuration;
using System.Security.Principal;
using System.Collections.Generic;
using System.ServiceModel.Configuration;

using CompareServer.Interfaces;
using CompareServer.UnityContainer;
using CompareServer.Domain.ComparerProxy;

using Document.Services.Compare.Config;
using System.Reflection;
using Microsoft.Win32;

namespace CompareServer.Domain
{

    public enum ConnectionStatus
    {
        Closed = 0,
        Opened,
        Failed,
        Opening
    }

    public class CompareService
    {
        private const string _connection        = "__connection";
        private const string _lock_object       = "__lockojbject";
        private const string _username          = "__user_name";
        private const string _password          = "__password";
        private const string _domain            = "__domain";
        private const string _service_version   = "__serviceversion";
        private const string _comparison_version = "__comparisonversion";
        private const string _timeout_queue     = "__timeout_queue";

        HttpSessionStateBase _session;
        
#region properites

        public static ILogger Log { get; private set; }

        public object Lock
        {
            get 
            {
                if (_session[_lock_object] == null)
                {
                    _session[_lock_object] = new object();
                }
                return _session[_lock_object];
            }
        }

        public bool StoreClientInSession
        {
            get
            {
                if (_session[_lock_object] != null)
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value == true)
                {
                    _session[_lock_object] = new Object();
                }
                else 
                {
                    _session[_lock_object] = null;
                }
            }
        }

        private List<ServerFile> TimeoutQueue
        {
            get {
                if (_session[_timeout_queue] == null)
                {
                    _session[_timeout_queue] = new List<ServerFile>();
                }
                return (List<ServerFile>)_session[_timeout_queue];
            }
        }

        /// <summary>
        ///     Return current session temporary directory
        /// </summary>
        private string UploadPath
        {
            get
            {
                string path = AppConfig.UploadPath;
                return System.IO.Path.Combine(path, _session.SessionID.ToString());
            }
        }

#endregion

        static CompareService()
        {
            Log = UnityManager.Resolve<ILogger>();
        }

        public CompareService(HttpSessionStateBase session)
        {
            _session = session;
        }

        public ComparerClient GetComparerClient()
        {
            if (StoreClientInSession)
            {
                if (_session[_connection] == null)
                {
                    _session[_connection] = RestoreConnection();
                }
                return (ComparerClient)_session[_connection];
            }
            return RestoreConnection();
        }

        private void StoreConnection(ComparerClient cc)
        {
            _session[_connection] = cc;
        }

        private void StoreVersions(string serviceVersion, string comparisonVersion)
        {
            _session[_service_version] = serviceVersion;
            _session[_comparison_version] = comparisonVersion;
        }

        public bool VersionsIsSet()
        {
            if (_session[_comparison_version] != null &&
                _session[_service_version] != null)
            {
                return true;
            }
            return false;
        }

        private bool UseConfig()
        {
            try
            {
                string uri = Assembly.GetExecutingAssembly().CodeBase;
                string file = new Uri(uri).LocalPath;
                string bin = Path.GetDirectoryName(file);
                DirectoryInfo root = new DirectoryInfo(bin).Parent;

                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                ServiceModelSectionGroup sectionGrp = ServiceModelSectionGroup.GetSectionGroup(config);

                ChannelEndpointElementCollection endpoints = sectionGrp.Client.Endpoints;
                foreach (ChannelEndpointElement endpoint in endpoints)
                {
                    if (endpoint.Name == AppConfig.EndpointConfig)
                    {
                        Log.Write(TraceEventType.Information, "CompareService: service.model section found");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            Log.Write(TraceEventType.Information, "CompareService: service.model section not found");
            return false;
        }

        public ComparerClient OpenConnection()
        {
            Log.Write(TraceEventType.Information, "Opening connection...");
            ComparerClient cc = null;
            try
            {
                if (UseConfig())
                {
                   cc = new ComparerClient(AppConfig.EndpointConfig, AppConfig.EndpointAddress);
                }
                else 
                {
                    DefaultCompareConfig config = new DefaultCompareConfig();
                    cc = new ComparerClient(config.GetBinding(), config.GetEndpointAddress());                
                }
                cc.Open();
                if (cc.Authenticate("Domain", "UserName", "Password"))
                {
                    StoreVersions(cc.GetVersion(), cc.GetCompositorVersion());
                    return cc;
                }
                else
                {
                    Log.Write(TraceEventType.Stop, "Authentication Error");
                    throw new System.ServiceModel.CommunicationException("Authentication Error");
                }

            }
            catch (System.ServiceModel.ServerTooBusyException ex)
            {
                Log.Write(TraceEventType.Stop, "Server Too Busy");
                throw ex;
            }
            catch (TimeoutException ex)
            {
                Log.Write(TraceEventType.Stop, "Faild to connect to server (Timeout)");
                throw ex;
            }
            catch (System.ServiceModel.FaultException ex)
            {
                Log.Write(TraceEventType.Stop, "FaultException: " + ex.Message);
                throw ex;
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                throw new Exception("Could not connect to server.");
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                if (ex.Message.Contains("request is unauthorized"))
                {
                    Log.Write(TraceEventType.Stop, @"Unauthorized issue arised, please check User Name and/or Password and/or domain!");
                } 
                else
                {
                    Log.Write(TraceEventType.Stop, "CommunicationException: " + ex.Message);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                ComparerResult result = new ComparerResult();
                Log.Write(TraceEventType.Stop, ex.Message);
                throw ex;
            }
        }

        public ComparerResult DoComparison(IPrincipal principal, ComparerArguments arguments)
        {
            ComparerResult cResult = new ComparerResult();
            try
            {
                /* Impersonation for connect to WCF using ASP.net Authenticated User */
                System.Security.Principal.WindowsImpersonationContext impersonationContext =
                    ((System.Security.Principal.WindowsIdentity)principal.Identity).Impersonate();

                ComparerClient cc = GetComparerClient();

                /* Uploaded files should be readed by ISS Application Pool User */
                impersonationContext.Undo();

                byte[] original = System.IO.File.ReadAllBytes(Path.Combine(UploadPath, arguments.OriginalDoc.ServerName));
//                string resultPath = System.IO.Directory.GetParent(arguments.OriginalDoc.ServerName).FullName;

                foreach (ServerFile file in arguments.ModifiedDoc)
                {
                    ComparisonResult result = new ComparisonResult();
                    CompareResults cr;
                    try
                    {                        
                        result.File = file.ClientName;
                        byte[] modified = System.IO.File.ReadAllBytes(Path.Combine(UploadPath, file.ServerName));
                        string sOptionSet = System.IO.File.ReadAllText(arguments.RenderingSet);

                        Log.Write(TraceEventType.Information, "Comparing {0} and {1} files", arguments.OriginalDoc.ClientName, file.ClientName);
                        var executeParams = new ExecuteParams()
                            {
                                CompareOptions = sOptionSet,
                                Original = original,
                                Modified = modified,
                                ResponseOption = arguments.OutputFormat,
                                OriginalDocumentInfo = CreateDocInfo(arguments.OriginalDoc.ClientName),
                                ModifiedDocumentInfo = CreateDocInfo(file.ClientName)
                            };

                        cr = cc.ExecuteEx(executeParams);

                        if (cr != null)
                        {
                            string fileName = System.IO.Path.GetFileNameWithoutExtension(result.File);

                            if (cr.Redline != null)
                            {
                                result.Redline = storeFileOnServer(cr.Redline, fileName + ".redline." + getExtension(arguments.OutputFormat));   
                            }
                            if (!string.IsNullOrEmpty(cr.RedlineMl))
                            {
                                result.RedlineMl = storeFileOnServer(cr.RedlineMl, fileName + ".redlineml.xml");
                            }
                            if (!string.IsNullOrEmpty(cr.Summary))
                            {
                                result.Summary = storeFileOnServer(cr.Summary , fileName + ".summary.xml");
                            }

                        }
                        else
                        {
                            Log.Write(TraceEventType.Error, "Null result");
                            result.Error = "No results";
                        }

                    }
                    catch (System.Security.SecurityException ex)
                    {
                        Log.Write(TraceEventType.Error, "{0}", ex);
                        result.Error = "Security Error: " + ex.Message;
                    }
                    catch (FileNotFoundException ex)
                    {
                        Log.Write(TraceEventType.Error, "{0}", ex);
                        result.Error = "File not found on server";
                    }
                    catch (Exception ex)
                    {
                        Log.Write(TraceEventType.Error, "{0}", ex);
                        result.Error = ex.Message;
                    }
                    cResult.Comperisons.Add(result);
                }
                checkTimeOutFiles();
            }
            catch (System.ServiceModel.ServerTooBusyException ex)
            {
                cResult.Errors.Add("Server Too Busy");
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            catch (TimeoutException ex)
            {
                cResult.Errors.Add("Faild to connect to server (Timeout)");
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                cResult.Errors.Add("FaultException: " + ex.Message);
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                if (ex.Message.Contains("request is unauthorized"))
                {
                    cResult.Errors.Add(@"Unauthorized issue arised, please check User Name and/or Password and/or domain!");
                }
                else
                {
                    cResult.Errors.Add("CommunicationException: " + ex.Message);
                }
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            catch (FileNotFoundException ex)
            {
                Log.Write(TraceEventType.Error, "{0}", ex);
                throw new Exception("Original file not found on server");
            }
            catch (Exception ex)
            {
                ComparerResult result = new ComparerResult();
                cResult.Errors.Add(ex.Message);
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            return cResult;
        }

        private DocumentInfo CreateDocInfo(string clientName)
        {
            return new DocumentInfo()
                {
                    DocumentDescription = clientName,
                    DocumentSource = clientName,
                };
        }

        public ComparerClient RestoreConnection()
        {
            return OpenConnection();
        }

        public void Close()
        {
            Log.Write(TraceEventType.Information, "Close connection" );
            lock (_session[_lock_object])
            {
                if (_session[_connection] != null)
                {
                    ComparerClient cp = (ComparerClient)_session[_connection];
                    try
                    {
                        if (cp != null && cp.State == System.ServiceModel.CommunicationState.Opened)
                        {
                            cp.Close();
                        }
                    }
                    finally
                    {
                        _session[_connection] = null;
                    }
                }
            }
        }

        public void GetVersions(out string serviceVersion, out string comparisonVersion)
        {
            Log.Write(TraceEventType.Information, "Get Versions");
            if (VersionsIsSet())
            {
                serviceVersion = (string)_session[_service_version];
                comparisonVersion = (string)_session[_comparison_version];
                return;
            }
            try
            {
                ComparerClient cp = GetComparerClient();
                serviceVersion = cp.GetVersion();
                comparisonVersion = cp.GetCompositorVersion();
                Log.Write(TraceEventType.Information, "Service version: {0}, Compositor version: {1}", serviceVersion, comparisonVersion);
            }
            catch (Exception ex)
            {
                _session[_connection] = null;
                Log.Write(TraceEventType.Error, "{0}", ex);
                throw;
            }
        }

        public void Ping()
        {
            Log.Write(TraceEventType.Information, "Ping");
            try
            {
                ComparerClient cp = GetComparerClient();
                CompareResults cr = cp.Ping();
            }
            catch (Exception ex)
            {
                _session[_connection] = null;
                Log.Write(TraceEventType.Error, "{0}", ex);
                throw ex;
            }

        }

        #region service functions
        /// <summary>
        ///     Return extention string of ComparerArguments object
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        private string getExtension(ResponseOptions ro)
        {
            switch (ro)
            {
                case ResponseOptions.Doc:
                case ResponseOptions.DocWithSummary:
                case ResponseOptions.RedlinMlAndDoc:
                case ResponseOptions.RedlinMlAndDocAndSummary:
                    return "doc";
                case ResponseOptions.DocX:
                case ResponseOptions.DocXWithSummary:
                case ResponseOptions.RedlinMlAndDocX:
                case ResponseOptions.RedlinMlAndDocXAndSummary:
                    return "docx";
                case ResponseOptions.Pdf:
                case ResponseOptions.PdfWithSummary:
                case ResponseOptions.RedlinMlAndPdf:
                case ResponseOptions.RedlinMlAndPdfAndSummary:
                    return "pdf";
                case ResponseOptions.Rtf:
                case ResponseOptions.RtfWithSummary:
                case ResponseOptions.RedlinMlAndRtf:
                case ResponseOptions.RedlinMlAndRtfAndSummary:
                    return "rtf";
                case ResponseOptions.Wdf:
                case ResponseOptions.WdfWithSummary:
                    return "wdf";
                case ResponseOptions.RedlinMl:
                case ResponseOptions.RedlinMlAndSummary:
                    return "rmi";
                case ResponseOptions.Xml:
                    return "xml";
                default:
                    return "doc";
            }
        }

        /// <summary>
        ///     Save file to HardDrive
        ///     TODO: Set Timer to remove file after 20 minutes
        /// </summary>
        /// <param name="path">Full path to store file</param>
        /// <param name="data">File data</param>
        public ServerFile storeFileOnServer(byte[] data, string originalName)
        {
            ServerFile sf = new ServerFile(originalName, Guid.NewGuid().ToString());
            System.IO.File.WriteAllBytes(Path.Combine(UploadPath, sf.ServerName), data);
            addFileToTimeoutQueue(sf);
            return sf;
        }

        public ServerFile storeFileOnServer(string data, string originalName) 
        {
            ServerFile sf = new ServerFile(originalName, Guid.NewGuid().ToString());
            FileInfo file = new FileInfo(Path.Combine(UploadPath, sf.ServerName));
            if (file.Exists)
                file.Delete();

            //xml has utf-16 encoding declared, so we need to save it as utf-16
            using (System.IO.StreamWriter sw = new StreamWriter(file.FullName, false, System.Text.Encoding.Unicode))
            {
                sw.Write(data);           
            }
            addFileToTimeoutQueue(sf);
            return sf;
        }

        private void addFileToTimeoutQueue(ServerFile sf)
        {
            TimeoutQueue.Add(sf);
        }

        private void checkTimeOutFiles()
        {
            try
            {
                lock(Lock)
                {
                    for (int i = TimeoutQueue.Count - 1; i >=0 ; i--)
                    {
                        ServerFile sf = TimeoutQueue[i];
                        if (sf.ExpireTime < DateTime.Now)
                        {
                            File.Delete(Path.Combine(UploadPath, sf.ServerName));
                            TimeoutQueue.Remove(sf);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
        }
        
        #endregion
        


        #region Static Members

        public static int GetServiceStatus()
        {
            if (IISHosted)
            {
                WebSite site = GetInstallWebSite();
                if (site != null)
                {
                    WebsiteState state = IISHelper.GetWebSiteState(site.Name);
                    switch (state)
                    { 
                        case WebsiteState.Stopped:
                            return 1;
                        case WebsiteState.Started:
                            return 4;
                        default:
                            return 0;
                    }
                }
                return 0;
            }
            else
            {
                try
                {
                    System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("Workshare Compare Service");
                    return (int)sc.Status;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static int StartService()
        {
            Log.Write(TraceEventType.Information, "Start Service");

            if (IISHosted)
            {
                WebSite site = GetInstallWebSite();
                if (site != null)
                {
                    WebsiteState state = IISHelper.StartWebSite(site.Name);
                    switch (state)
                    {
                        case WebsiteState.Stopped:
                            return 1;
                        case WebsiteState.Started:
                            return 4;
                        default:
                            return 0;
                    }
                }
                return 0;
            }
            else
            {
                try
                {
                    System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("Workshare Compare Service");
                    sc.Start();
                    sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);

                    return (int)sc.Status;
                }
                catch (Exception ex)
                {
                    Log.Write(TraceEventType.Error, "{0}", ex);
                    return GetServiceStatus();
                }
            }
        }

        public static int StopService()
        {
            Log.Write(TraceEventType.Information, "Stop Service");

            if (IISHosted)
            {
                WebSite site = GetInstallWebSite();
                if (site != null)
                {
                    WebsiteState state = IISHelper.StopWebSite(site.Name);
                    switch (state)
                    {
                        case WebsiteState.Stopped:
                            return 1;
                        case WebsiteState.Started:
                            return 4;
                        default:
                            return 0;
                    }
                }
                return 0;
            }
            else
            {
                try
                {
                    System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("Workshare Compare Service");
                    sc.Stop();
                    sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);

                    return (int)sc.Status;
                }
                catch (Exception ex)
                {
                    Log.Write(TraceEventType.Error, "{0}", ex);
                    return GetServiceStatus();
                }
            }
        }

        private const string _regSubKeyValue = @"SOFTWARE\Workshare\Ecosystem\CompareService";
        
        public static bool IISHosted
        {
            get
            {
                try
                {
                    RegistryView rw = Environment.Is64BitOperatingSystem == true ? RegistryView.Registry64 : RegistryView.Registry32;
                    RegistryKey HKLocalMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, rw);
                    RegistryKey ecoKey = HKLocalMachine.OpenSubKey(_regSubKeyValue);
                    string val = ecoKey.GetValue("InstallIISHost").ToString();
                    if (val.ToUpper() == "TRUE")
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static WebSite GetInstallWebSite()
        {
            try
            {
                RegistryView rw = Environment.Is64BitOperatingSystem == true ? RegistryView.Registry64 : RegistryView.Registry32;
                RegistryKey HKLocalMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, rw);
                RegistryKey ecoKey = HKLocalMachine.OpenSubKey(_regSubKeyValue + @"\IISHost");
                string name = ecoKey.GetValue("WebSite").ToString();
                int port = (int)ecoKey.GetValue("IISPort");
                return new WebSite { Name = name,  Port = port};
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Error, "GetInstallWebSite: {0}", ex);
                return null;
            }        
        }

        #endregion
    }
}