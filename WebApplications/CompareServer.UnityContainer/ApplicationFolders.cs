using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace CompareServer.UnityContainer
{
    internal static class ApplicationFolders
    {
        #region public properties
        /// <summary>
        ///     Gets the base Application Data Folder
        /// </summary>
        public static string ApplicationDataForlder
        {
            get {
                string uri = Assembly.GetExecutingAssembly().CodeBase;
                string file = new Uri(uri).LocalPath;
                return Path.GetDirectoryName(file);
            }
        }

        public static string ConfigurationFolder
        {
            get
            {
                DirectoryInfo di = new DirectoryInfo(ApplicationDataForlder);
                return Path.Combine(di.Parent.FullName, "Configuration");
            }
        }

        #endregion
    }
}
