using System;
using System.Diagnostics;

namespace CompareServer.Interfaces
{
    public interface ILogger
    {
        void Write(string format, params object[] args);
        void Write(TraceEventType type, string format, params object[] args);
    }
}
