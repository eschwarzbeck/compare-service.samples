using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompareServer.Interfaces;
using System.IO;

namespace CompareServer.Domain
{
    public abstract class CompareServerLog<TArgs> : ILogReader<LogEntry>
    {
 
        public abstract IEnumerable<LogEntry> GetLogs(DateTime startDate, DateTime endDate);
        public abstract IEnumerable<LogEntry> GetLogs();

        #region Tmp funcs
        public abstract FileStream DownloadLog(TArgs args);
        public abstract void PurgeLog(TArgs args);
        #endregion
    }
}
