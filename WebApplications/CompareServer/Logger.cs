using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompareServer.Interfaces;
using System.Diagnostics;

namespace CompareServer
{
    public sealed class Logger : ILogger
    {
        public Logger(string sourceName)
        {
            Source = new TraceSource(sourceName);
        }

        public TraceSource Source { get; private set; }

        #region ILogger Members

        public void Write(string format, params object[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            Write(TraceEventType.Verbose, format, args);
        }

        public void Write(System.Diagnostics.TraceEventType type, string format, params object[] args)
        {
            if (args == null)
            { 
                throw new ArgumentNullException("args");
            }
            Source.TraceEvent(type, 0, format, args);
        }
       
        #endregion
    }
}
