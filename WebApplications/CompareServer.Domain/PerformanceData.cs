using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompareServer.Interfaces;

namespace CompareServer.Domain
{
    public class PerformanceData
    {
        private IEnumerable<AuditLogEntry> _logs;
        private DateTime _startDate;
        private DateTime _endDate;

        private Dictionary<DateTime, DayStatistic> _statistics;
        private double _totalNumberOfComparisons;
        private double _totalNumberOfBytes;

        public Dictionary<DateTime, DayStatistic> PerDayStatistics { get { return _statistics; } }
        public double TotalNumberOfComparisons { get { return _totalNumberOfComparisons; } }
        public double TotalNumberOfBytes { get { return _totalNumberOfBytes; } }
                
        public PerformanceData(DateTime startDate, DateTime endDate)
        {
            _startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 23, 59, 59);
            _endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);

            ILogReader<AuditLogEntry> reader = new AuditLog();
            _logs = reader.GetLogs(_startDate, _endDate);

            ProceedData();
        }

        private void ProceedData()
        {
            // This is not supposed to happen
            if (_startDate < _endDate) return;

            _statistics = new Dictionary<DateTime, DayStatistic>();

            DateTime currDate = _startDate;
            while (currDate >= _endDate)
            {
                IEnumerable<AuditLogEntry> dayLogs = _logs.Where(entry => entry.LogTime.Date == currDate.Date);
                DayStatistic day;
                day.Comparisons = dayLogs.Count();
                if (day.Comparisons > 0)
                {
                    day.AvarageComparisonLength = dayLogs.Average(entry => entry.RedlineSize);
                    day.AvarageDocumentLength = dayLogs.Average(entry => entry.OriginalSize);
                    day.AvarageProcessingTime = dayLogs.Average(entry => entry.TotalExecutionTime);
                }
                else
                {
                    day.AvarageComparisonLength = 0;
                    day.AvarageDocumentLength = 0;
                    day.AvarageProcessingTime = 0;
                }
                _statistics.Add(currDate.Date, day);
                currDate = currDate.AddDays(-1);
            }

            _totalNumberOfBytes = _logs.Sum(entry => entry.OriginalSize);
            _totalNumberOfComparisons = _logs.Count();
        }
    }

    public struct DayStatistic
    { 
        public double Comparisons;
        public double AvarageComparisonLength;
        public double AvarageProcessingTime;
        public double AvarageDocumentLength;
    }
}
