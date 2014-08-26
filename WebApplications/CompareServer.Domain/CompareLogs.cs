using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CompareServer.Domain
{
    public class CompareLogs
    {

        public static DateTime GetEndDate(DateTime start, Range range)
        {
            switch (range)
            {
                case Range.Day:
                    return start.AddDays(-1);
                case Range.Week:
                    return start.AddDays(-7);
                case Range.Month:
                    return start.AddMonths(-1);
                case Range.Year:
                    return start.AddYears(-1);
                default:
                    return start.AddDays(-1);
            }
        }
    }
}
