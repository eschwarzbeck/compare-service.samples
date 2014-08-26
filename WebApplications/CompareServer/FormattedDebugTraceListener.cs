//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Logging Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace CompareServer
{
    [ConfigurationElementType(typeof(FormattedDebugTraceListenerData))]
    public class FormattedDebugTraceListener : DefaultTraceListener
    {
        public string Footer { get; private set; }
        public string Header { get; private set; }
        public ILogFormatter Formatter { get; set; }

        public FormattedDebugTraceListener() {
            Footer = string.Empty;
            Header = string.Empty;
        }

        public FormattedDebugTraceListener(string footer, string header)
        {
            Footer = footer;
            Header = header;
        }

        public FormattedDebugTraceListener(ILogFormatter formatter, string footer, string header)
        {
            Formatter = formatter;
            Footer = footer;
            Header = header;
        }

        /// <summary>
        ///   Delivers the trace data to the underlying file.
        /// </summary>
        /// <param name = "eventCache">The context information provided by <see cref = "System.Diagnostics" />.</param>
        /// <param name = "source">The name of the trace source that delivered the trace data.</param>
        /// <param name = "eventType">The type of event.</param>
        /// <param name = "id">The id of the event.</param>
        /// <param name = "data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (Header.Length > 0)
                {
                    base.WriteLine(Header);
                }

                if (data is LogEntry && Formatter != null)
                {
                    base.WriteLine(Formatter.Format(data as LogEntry));
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }

                if (Footer.Length > 0)
                {
                    base.WriteLine(Footer);
                }
            }
        }

        protected override string[] GetSupportedAttributes()
        {
            return new [] {"formatter", "header", "footer"};
        }
    }
}
