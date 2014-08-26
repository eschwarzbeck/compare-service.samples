using CompareServer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;


namespace CompareServer.Domain
{
    public class HostLog: SizeDividedLog
    {
        public const string HostLogFile = "compare_service_host.log";

        public HostLog()
            : base(HostLogFile)
        { }

        new public IEnumerable<HostLogEntry> GetLogs(DateTime startDate, DateTime endDate)
        {
            IEnumerable<LogEntry> logs = base.GetLogs(startDate, endDate);
            return logs.Select(entry => new HostLogEntry(entry));
        }
        
        new public IEnumerable<HostLogEntry> GetLogs()
        {
            IEnumerable<LogEntry> logs = base.GetLogs();
            return logs.Select(entry => new HostLogEntry(entry));
        }
    }
}
