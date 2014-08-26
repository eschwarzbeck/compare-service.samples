using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.IO.Compression;
using Ionic.Zip;

namespace CompareServer.Domain
{
    public abstract class DateDividedLog : CompareServerLog<DateTime?>
    {
        private string _basicFileName;

        public DateDividedLog(string fileName)
        {
            _basicFileName = fileName;
        }

        #region Private Methods

        private string GetLogFileName(DateTime day)
        {
            DateTime now = DateTime.Now;
            string filePath = System.IO.Path.Combine(AppConfig.LogsDirectory, _basicFileName);
            if (day.Year != now.Year || day.Month != now.Month || day.Day != now.Day)
            {
                filePath += day.ToString("yyyyMMdd");
            }
            return filePath;
        }

        private StreamReader GetLogFileStream(DateTime day)
        {
            string filePath = GetLogFileName(day);
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                try
                {
                    return file.OpenText();
                }
                catch (Exception)
                {
                    throw new Exception("Cannot open Log file for the day");
                }
            }
            return null;
        }

        #endregion

        #region Interface Methods
        
        public override IEnumerable<LogEntry> GetLogs(DateTime startDate, DateTime endDate)
        {
            StreamReader stream = null;

            if ((stream = GetLogFileStream(startDate)) != null)
            {
                string all = stream.ReadToEnd();
                string[] lines = all.Split(Environment.NewLine.ToArray());
                return lines.Where(line => line != string.Empty).Select(line => new LogEntry(line));
            }
            else
            {
                throw new Exception("Cannot find file for the day");
            }
        }

        public override IEnumerable<LogEntry> GetLogs()
        {
            return this.GetLogs(DateTime.Now, DateTime.Now);
        }

        #endregion

        #region Public Methods

        public override FileStream DownloadLog(DateTime? args)
        {
            string logFileName = GetLogFileName((DateTime)args);
            string zipFileName = Path.Combine(AppConfig.LogsDirectory, Guid.NewGuid() + ".zip");
            ZipFile zip = new ZipFile(zipFileName);
            if (File.Exists(logFileName))
            {
                zip.AddFile(logFileName);
                zip.Save();
                return new FileStream(zipFileName, FileMode.Open);
            }
            throw new Exception(string.Format("Log file \"{0}\" not found", Path.GetFileName(logFileName)));
        }

        public override void PurgeLog(DateTime? args)
        {
            string filePath = GetLogFileName((DateTime)args);
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        #endregion
    }
}
