using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompareServer.Interfaces;
using System.IO;

namespace CompareServer.Domain
{
    public class AuditLog : SizeDividedLog, ILogReader<AuditLogEntry>
    {
        public const string AuditLogFile = "compare_service_audit.log";

        public AuditLog() : base(AuditLogFile) { }
                
        public new IEnumerable<AuditLogEntry> GetLogs(DateTime startDate, DateTime endDate)
        {
            IEnumerable<LogEntry> logs = base.GetLogs(startDate, endDate);
            return logs.Select(entry => new AuditLogEntry(entry));
        }

        public new IEnumerable<AuditLogEntry> GetLogs()
        {
            IEnumerable<LogEntry> logs = base.GetLogs();
            return logs.Select(entry => new AuditLogEntry(entry));
        }

        public IEnumerable<UserActivity> GetUsersActivity()
        {
            IEnumerable<AuditLogEntry> logs = this.GetLogs();
            int cc = logs.Count();
            IEnumerable<UserActivity> result = logs.GroupBy(entry => entry.UserName)
                                                   .Select(list => new UserActivity {   UserName = list.Key,
                                                                                        UsedNumber = list.Count(),
                                                                                        TotalBytesCompared = list.Sum(item => item.OriginalSize),
                                                                                        TotalProcessingTime = list.Sum(item => item.ResultsProcessingTime)
            });
            return result;
        }
    }

    public struct UserActivity
    {
        public string UserName;
        public double UsedNumber;
        public double TotalBytesCompared;
        public double TotalProcessingTime;
    }

}
