
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.IO.Compression;
using Ionic.Zip;

namespace CompareServer.Domain
{
    public class SizeDividedLog : CompareServerLog<Range>
    {
        private string          _startFileName;
        private int             _fileIndex = 0;  
       
        public SizeDividedLog(string startFile)
        {
            _startFileName = System.IO.Path.Combine(AppConfig.LogsDirectory, startFile);
        }

        #region Protected methods

        protected string GetLogFileName()
        {
            string fileName = _startFileName;
            if (_fileIndex > 0)
            {
                fileName += "." + _fileIndex.ToString();
            }
            if (File.Exists(fileName))
            {
                _fileIndex++;
                return fileName;
            }
            else if (_fileIndex == 0)
            {
                throw new System.Exception("Cannot Open Log file");
            }
            return null;
        }

        protected StreamReader GetLogFileStream()
        {
            string fileName = GetLogFileName();
            if (fileName != null)
            {
                return new FileInfo(fileName).OpenText();
            }
            return null;
        }


        protected List<LogEntry> GetFileResult()
        {
            StreamReader stream = null;
            try
            {
                stream = GetLogFileStream();
                if (stream != null)
                {
                    List<LogEntry> results = new List<LogEntry>();
                    string content = stream.ReadToEnd();
                    string[] lines = content.Split(Environment.NewLine.ToCharArray());
                    foreach (string line in lines)
                    {
                        if (line != string.Empty)
                        {
                            results.Add(new LogEntry(line));
                        }
                    }
                    return results;
                }
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return null;
        }


        protected List<LogEntry> GetFileResult(DateTime startDate, DateTime endDate, ref bool needNextFile)
        {
            StreamReader stream = null;
            try
            {
                stream = GetLogFileStream();
                if (stream != null)
                {
                    List<LogEntry> results = new List<LogEntry>();
                    string line = null;
                    while ((line = stream.ReadLine()) != null)
                    {
                        LogEntry entry = new LogEntry(line);
                        if (entry.LogTime <= startDate)
                        {
                            if (entry.LogTime >= endDate)
                            {
                                results.Add(entry);
                            }
                            else
                            {
                                needNextFile = false;
                            }
                        }
                    }
                    return results;
                }
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            needNextFile = false;
            return null;
        }

        #endregion


        #region Interface methods

        public override IEnumerable<LogEntry> GetLogs(DateTime startDate, DateTime endDate)
        {
            List<LogEntry> result = new List<LogEntry>();
            bool needNextFile = true;

            List<LogEntry> logs = null;
            while (needNextFile && (logs = GetFileResult(startDate, endDate, ref needNextFile)) != null)
            {
                if (logs.Count > 0)
                {
                    result.AddRange(logs.OrderByDescending(entry => entry.LogTime));
                }
            }
            return result;
        }

        public override IEnumerable<LogEntry> GetLogs()
        {
            List<LogEntry> result = new List<LogEntry>();
            List<LogEntry> logs = null;
            while ((logs = GetFileResult()) != null)
            {
                if (logs.Count > 0)
                {
                    result.AddRange(logs.OrderByDescending(entry => entry.LogTime));
                }
            }
            return result;
        }

        #endregion


        #region Public Methods

        public override void PurgeLog(Range args)
        {
            _fileIndex = 0;
            string fileName;
            while ((fileName = GetLogFileName()) != null)
            {
                File.Delete(fileName);
            }
        }

        public override FileStream DownloadLog(Range args)
        {
            string logFileName;
            string zipFileName = Path.Combine(AppConfig.LogsDirectory, Guid.NewGuid() + ".zip");
            ZipFile zip = new ZipFile(zipFileName);
            while ((logFileName = GetLogFileName()) != null)
            {
                zip.AddFile(logFileName);
            }
            zip.Save();
            return new FileStream(zipFileName, FileMode.Open);
        }

        #endregion

    }
}
