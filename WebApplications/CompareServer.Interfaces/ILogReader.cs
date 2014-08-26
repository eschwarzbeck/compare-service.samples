using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareServer.Interfaces
{
    public interface ILogReader<TLogEntry>
    {
        IEnumerable<TLogEntry> GetLogs(DateTime startDate, DateTime endDate);
        IEnumerable<TLogEntry> GetLogs();
    }
}
