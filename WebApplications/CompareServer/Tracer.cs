using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using Microsoft.Practices.EnterpriseLibrary.Logging;
//using CompareServer.Properties;
using System.Globalization;

namespace CompareServer
{
    public sealed class Tracer : IDisposable
    {
        /// <summary>
        ///   Priority value for Trace messages
        /// </summary>
        public const int Priority = 5;

        /// <summary>
        ///   Event id for Trace messages
        /// </summary>
        public const int EventId = 1;

        /// <summary>
        ///   Title for operation start Trace messages
        /// </summary>
        public const string StartTitle = "TracerEnter";

        /// <summary>
        ///   Title for operation end Trace messages
        /// </summary>
        public const string EndTitle = "TracerExit";

        /// <summary>
        ///   Name of the entry in the ExtendedProperties having the activity id
        /// </summary>
        public const string ActivityIdPropertyKey = "TracerActivityId";

        private readonly LogWriter _writer;

        private Stopwatch _stopwatch;
        private bool _tracerDisposed;
        private bool _tracingAvailable;
        private long _tracingStartTicks;
        private bool _logicalOperationStarted;

        private IFormatProvider _stub = new CultureInfo("en-US");
        /// <summary>
        ///   Initializes a new instance of the <see cref = "Tracer" /> class with the given logical operation name.
        /// </summary>
        /// <remarks>
        ///   If an existing activity id is already set, it will be kept. Otherwise, a new activity id will be created.
        /// </remarks>
        /// <param name = "operation">The operation for the <see cref = "Tracer" /></param>
        public Tracer(string operation)
        {
            if (CheckTracingAvailable())
            {
                if (GetActivityId().Equals(Guid.Empty))
                {
                    SetActivityId(Guid.NewGuid());
                }

                Initialize(operation);
            }
        }

        public Tracer(string operation, string format, params string[] args)
        {
            if (CheckTracingAvailable())
            {
                Text = string.Format(format, args);
                if (GetActivityId().Equals(Guid.Empty))
                {
                    SetActivityId(Guid.NewGuid());
                }

                Initialize(operation);
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Tracer" /> class with the given logical operation name and activity id.
        /// </summary>
        /// <remarks>
        ///   The activity id will override a previous activity id
        /// </remarks>
        /// <param name = "operation">The operation for the <see cref = "Tracer" /></param>
        /// <param name = "activityId">The activity id</param>
        public Tracer(string operation, Guid activityId)
        {
            if (CheckTracingAvailable())
            {
                SetActivityId(activityId);

                Initialize(operation);
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Tracer" /> class with the given logical operation name and activity id.
        /// </summary>
        /// <remarks>
        ///   This is meant to be used internally
        /// </remarks>
        /// <param name = "operation">The operation for the <see cref = "Tracer" /></param>
        /// <param name = "activityId">The activity id</param>
        /// <param name = "writer">The <see cref = "LogWriter" /> that is used to write trace messages</param>
        internal Tracer(string operation, Guid activityId, LogWriter writer)
        {
            if (CheckTracingAvailable())
            {
                if (writer == null)
                {
                    throw new ArgumentNullException("writer");
                }

                SetActivityId(activityId);

                _writer = writer;

                Initialize(operation);
            }
        }

        internal Tracer(string operation, Guid activityId, LogWriter writer, string format, params string[] args)
        {
            if (CheckTracingAvailable())
            {
                Text = string.Format(format, args);
                if (writer == null)
                {
                    throw new ArgumentNullException("writer");
                }

                SetActivityId(activityId);

                _writer = writer;

                Initialize(operation);
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Tracer" /> class with the given logical operation name and activity id.
        /// </summary>
        /// <remarks>
        ///   This is meant to be used internally
        /// </remarks>
        /// <param name = "operation">The operation for the <see cref = "Tracer" /></param>
        /// <param name = "writer">The <see cref = "LogWriter" /> that is used to write trace messages</param>
        internal Tracer(string operation, LogWriter writer)
        {
            if (CheckTracingAvailable())
            {
                if (writer == null)
                {
                    throw new ArgumentNullException("writer");
                }

                if (GetActivityId().Equals(Guid.Empty))
                {
                    SetActivityId(Guid.NewGuid());
                }

                _writer = writer;

                Initialize(operation);
            }
        }

        internal Tracer(string operation, LogWriter writer, string format, params string[] args)
        {
            if (CheckTracingAvailable())
            {
                if (writer == null)
                {
                    throw new ArgumentNullException("writer");
                }

                Text = string.Format(format, args);

                if (GetActivityId().Equals(Guid.Empty))
                {
                    SetActivityId(Guid.NewGuid());
                }

                _writer = writer;

                Initialize(operation);
            }
        }

        private string Text { get; set; }

        #region IDisposable Members

        /// <summary>
        ///   Causes the <see cref = "Tracer" /> to output its closing message.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        ///   <para>Releases unmanaged resources and performs other cleanup operations before the <see cref = "Tracer" /> is 
        ///     reclaimed by garbage collection</para>
        /// </summary>
        ~Tracer()
        {
            Dispose(false);
        }

        /// <summary>
        ///   <para>Releases the unmanaged resources used by the <see cref = "Tracer" /> and optionally releases 
        ///     the managed resources.</para>
        /// </summary>
        /// <param name = "disposing">
        ///   <para><see langword = "true" /> to release both managed and unmanaged resources; <see langword = "false" /> 
        ///     to release only unmanaged resources.</para>
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing && !_tracerDisposed)
            {
                if (_tracingAvailable)
                {
                    try
                    {
                        if (IsTracingEnabled())
                        {
                            WriteTraceEndMessage(EndTitle);
                        }
                    }
                    finally
                    {
                        if (_logicalOperationStarted)
                        {
                            try
                            {
                                StopLogicalOperation();
                            }
                            catch (SecurityException)
                            {
                            }
                        }
                    }
                }

                _tracerDisposed = true;
            }
        }

        /// <summary>
        ///   Answers whether tracing is enabled
        /// </summary>
        /// <returns>true if tracing is enabled</returns>
        public bool IsTracingEnabled()
        {
            var writer = GetWriter();
            return writer.IsTracingEnabled();
        }

        internal static bool IsTracingAvailable()
        {
            var tracingAvailable = false;

            try
            {
                tracingAvailable = SecurityManager.IsGranted(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
            }
            catch (SecurityException)
            {
            }

            return tracingAvailable;
        }

        private bool CheckTracingAvailable()
        {
            _tracingAvailable = IsTracingAvailable();

            return _tracingAvailable;
        }

        private void Initialize(string operation)
        {
            if (IsTracingEnabled())
            {
                StartLogicalOperation(operation);
                _logicalOperationStarted = true;
                _stopwatch = Stopwatch.StartNew();
                _tracingStartTicks = Stopwatch.GetTimestamp();

                WriteTraceStartMessage(StartTitle);
            }
        }

        private void WriteTraceStartMessage(string entryTitle)
        {
            var methodName = GetExecutingMethodName();
            string message;
            if (string.IsNullOrEmpty(Text))
            {
                message = string.Format(_stub /*Resources.Culture*/, "Start: '{0}' at {1} ticks", methodName, _tracingStartTicks);
            }
            else
            {
                message = string.Format(_stub /*Resources.Culture*/, "Start: {0} ({1} ticks)", Text, _tracingStartTicks);
            }

            WriteTraceMessage(message, entryTitle, TraceEventType.Start);
        }

        private void WriteTraceEndMessage(string entryTitle)
        {
            var tracingEndTicks = Stopwatch.GetTimestamp();
            var secondsElapsed = GetSecondsElapsed(_stopwatch.ElapsedMilliseconds);

            var methodName = GetExecutingMethodName();
            string message;
            if (string.IsNullOrEmpty(Text))
            {
                message = string.Format(_stub, //Resources.Culture,
                                        "End: '{0}' at {1} ticks (elapsed time: {2} seconds)",
                                        methodName,
                                        tracingEndTicks,
                                        secondsElapsed);
            }
            else
            {
                message = string.Format(_stub, //Resources.Culture,
                                        "End: {0} at {1} ticks (elapsed time: {2} seconds)",
                                        Text,
                                        tracingEndTicks,
                                        secondsElapsed);
            }

            WriteTraceMessage(message, entryTitle, TraceEventType.Stop);
        }

        private void WriteTraceMessage(string message, string entryTitle, TraceEventType eventType)
        {
            var extendedProperties = new Dictionary<string, object>();
            var entry = new LogEntry(message,
                                     PeekLogicalOperationStack() as string,
                                     Priority,
                                     EventId,
                                     eventType,
                                     entryTitle,
                                     extendedProperties);

            var writer = GetWriter();
            writer.Write(entry);
        }

        private string GetExecutingMethodName()
        {
            var result = "Unknown";
            var trace = new StackTrace(false);

            for (var index = 0; index < trace.FrameCount; ++index)
            {
                var frame = trace.GetFrame(index);
                var method = frame.GetMethod();
                if (method.DeclaringType != GetType() && method.DeclaringType != typeof(Logger))
                {
                    result = string.Concat(method.DeclaringType.FullName, ".", method.Name);
                    break;
                }
            }

            return result;
        }

        private decimal GetSecondsElapsed(long milliseconds)
        {
            var result = Convert.ToDecimal(milliseconds) / 1000m;
            return Math.Round(result, 6);
        }

        private LogWriter GetWriter()
        {
            return _writer ?? Logger.Writer;
        }

        private static Guid GetActivityId()
        {
            return Trace.CorrelationManager.ActivityId;
        }

        private static Guid SetActivityId(Guid activityId)
        {
            return Trace.CorrelationManager.ActivityId = activityId;
        }

        private static void StartLogicalOperation(string operation)
        {
            Trace.CorrelationManager.StartLogicalOperation(operation);
        }

        private static void StopLogicalOperation()
        {
            Trace.CorrelationManager.StopLogicalOperation();
        }

        private static object PeekLogicalOperationStack()
        {
            return Trace.CorrelationManager.LogicalOperationStack.Peek();
        }
    }
}