using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CompareServer.Domain
{
    public class AppConfig
    {
        private static AppConfig _instance;

        static AppConfig()
        {
            _instance = new AppConfig();
        }

        #region config varibales
        private string _endpointConfig  = null;
        private string _endpointAddress = null;
        private string _uploadPath      = null;
        private string _rendersetPath   = null;
        private string _logsDirecotry   = null;
        private int? _recordsPerPage    = null;
        private string _adminRole       = null;
        private int? _tempFilesTimeout  = null;
        #endregion

        #region properties
        /// <summary>
        ///     WCF Service Config Record
        /// </summary>
        public static string EndpointConfig
        {
            get
            {
                if (_instance._endpointConfig == null)
                {
                    _instance._endpointConfig = ConfigurationManager.AppSettings.Get("EndpointConfig");
                    if (_instance._endpointConfig == null)
                    {
                        _instance._endpointConfig = "WSHttpBinding_IComparer";  // Default Value
                    }
                }
                return _instance._endpointConfig;
            }
        }

        /// <summary>
        ///     Server name of IP address of WCF Server
        /// </summary>
        public static string EndpointAddress
        {
            get
            {
                if (_instance._endpointAddress == null)
                {
                    _instance._endpointAddress = ConfigurationManager.AppSettings.Get("EndpointAddress");
                    if (_instance._endpointAddress == null)
                    {
                        _instance._endpointAddress = @"http://localhost:8080/Comparer/Compare5";
                    }
                }
                return _instance._endpointAddress;
            }
        }

        /// <summary>
        ///     Directory to store temporary Uploaded and Result files
        /// </summary>
        public static string UploadPath
        {
            get
            {
                if (_instance._uploadPath == null)
                {
                    _instance._uploadPath = ConfigurationManager.AppSettings.Get("UploadPath");
                    if (_instance._uploadPath == null)
                    {
                        _instance._uploadPath = @"Upload";
                    }
                    //if (!System.IO.Directory.Exists(_instance._uploadPath))
                    //{
                    //    throw new Exception("Error: Check your configuration for UploadPath");
                    //}
                }
                return _instance._uploadPath;
            }
        }

        /// <summary>
        ///     Diretory with renderset files
        /// </summary>
        public static string RendersetPath
        {
            get
            {
                if (_instance._rendersetPath == null)
                {
                    _instance._rendersetPath = ConfigurationManager.AppSettings.Get("renderset.path");
                    if (_instance._rendersetPath == null)
                    {
                        _instance._rendersetPath = @"D:\data\renderset";
                    }
                    if (!System.IO.Directory.Exists(_instance._rendersetPath))
                    {
                        throw new Exception("Error: Can't find rendering path");
                    }
                }
                return _instance._rendersetPath;
            }
        }

        /// <summary>
        ///     Compare Server Log Directory
        /// </summary>
        public static string LogsDirectory
        {
            get
            {
                if (_instance._logsDirecotry == null)
                {
                    _instance._logsDirecotry = ConfigurationManager.AppSettings.Get("LogsDirectory");
                    if (_instance._logsDirecotry == null)
                    {
                        _instance._logsDirecotry = @"C:\Program Files\Workshare\Compare Service\bin\logs";
                    }
                }
                return _instance._logsDirecotry;
            }
        }

        /// <summary>
        ///   Amount of list items in one page of List View
        /// </summary>
        public static int RecordsPerPage
        {
            get
            {
                if (_instance._recordsPerPage == null)
                {
                    string value = ConfigurationManager.AppSettings.Get("RecordsPerPage");
                    int number;
                    if (value != null && Int32.TryParse(value, out number))
                    {
                        _instance._recordsPerPage = number;
                    }
                    else
                    {
                        _instance._recordsPerPage = 10;
                    }
                }
                return (int)_instance._recordsPerPage;
            }
        }


        /// <summary>
        ///     Name of Groups to act as Administrators for Web Admin
        /// </summary>
        public static string AdministratorRole
        {
            get
            {
                if (_instance._adminRole == null)
                {
                    _instance._adminRole = ConfigurationManager.AppSettings.Get("AdminRole");
                    if (string.IsNullOrEmpty(_instance._adminRole))
                    {
                        _instance._adminRole = "Administrators";    // Default value
                    }
                }
                return _instance._adminRole;
            }
        }

        /// <summary>
        ///     Number of minutes to consider temporary file 'timeout'
        /// </summary>
        public static int TempFilesTimeout
        {
            get {
                if (_instance._tempFilesTimeout == null)
                {
                    string value = ConfigurationManager.AppSettings.Get("TempFilesTimeout");
                    int number;
                    if (value != null && Int32.TryParse(value, out number))
                    {
                        _instance._tempFilesTimeout = number;
                    }
                    else
                    {
                        _instance._tempFilesTimeout = 20;
                    }
                }
                return (int)_instance._tempFilesTimeout;
            
            }
        }

        #endregion


    }
}
