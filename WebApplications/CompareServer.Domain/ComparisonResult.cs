using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareServer.Domain
{
    public class ComparisonResult
    {
        public ComparisonResult()
        {
            File = string.Empty;
            Redline = new ServerFile();
            RedlineMl = new ServerFile();
            Summary = new ServerFile();
            Error = string.Empty;
        }
        public string File { get; set; }
        public ServerFile Redline { get; set; }
        public ServerFile RedlineMl { get; set; }
        public ServerFile Summary { get; set; }
        public string Error { get; set; }
    }
}
