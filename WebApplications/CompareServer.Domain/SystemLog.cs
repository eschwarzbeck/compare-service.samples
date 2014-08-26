using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CompareServer.Domain
{
    public class SystemLog : DateDividedLog
    {
        public const string SystemLogFile = "compare_service_system.log";

        public SystemLog() : base(SystemLogFile) { }

        public static IEnumerable<string> GetFiles()
        {
            try
            {
                string searchPattern = SystemLogFile + "*";
                DirectoryInfo directory = new DirectoryInfo(AppConfig.LogsDirectory);
                FileInfo[] files = directory.GetFiles(searchPattern);

                return files.Select(file => file.Name);
            }
            catch (Exception)
            {
                throw new Exception("Faild to read list of System log files");
            }
        }

        private static string GetAccessRecord(string path)
        {
            StreamReader fs = File.OpenText(path);
            string[] lines = fs.ReadToEnd().Split(Environment.NewLine.ToCharArray());
            foreach (string line in lines)
            {
                string[] arr = line.Split('|');
                if (arr.Length >= 3 && arr[2].StartsWith("Authenticate:"))
                {
                    return arr[0];
                }
            }
            return null;
        }

        public static string GetLastAccess()
        {
            bool fileFound = false;
            string path = Path.Combine(AppConfig.LogsDirectory, SystemLogFile);
            if (File.Exists(path)) {
                fileFound = true;
                string access = GetAccessRecord(path);
                if (access != null)
                {
                    return access;
                }
            }
                
            IEnumerable<string> files = GetFiles().OrderByDescending(file => file);
            if (files.Count() > 0)
            {
                foreach (string file in files)
                {
                    string access = GetAccessRecord(Path.Combine(AppConfig.LogsDirectory, file));
                    if (access != null)
                        return access;
                }
            }
            else if (!fileFound)
            {
                throw new Exception("Cann't open log file");
            }
            throw new Exception("Cann't find authenticate record in log files");
        }
    }
}
