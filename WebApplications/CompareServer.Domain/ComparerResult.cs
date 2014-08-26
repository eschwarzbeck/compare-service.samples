using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareServer.Domain
{
    public class ComparerResult
    {
        public ComparerResult()
        {
            Comperisons = new List<ComparisonResult>();
            Errors = new List<string>();
        }
        public List<ComparisonResult> Comperisons { get; set; }
        public List<string> Errors { get; set; }
    }
}
