using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace CompareServer.Domain
{
    public class LogEntry
    {
        private const string _dateFormat = "yyyyMMdd HH:mm:ss,fff";

        public LogEntry(string line)
        {
            if (line != null)
            {
                string[] arr = line.Split('|');
                if (arr.Length >= 2)
                {
                    this.LogTime = LogEntry.DateFromString(arr[0].Trim());
                    this.Type = arr[1];
                    this.Message = arr[2];
                }
            }
        }

        public DateTime LogTime { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public static DateTime DateFromString(string s)
        {
            return System.DateTime.ParseExact(s, _dateFormat, CultureInfo.CurrentCulture);
        }

    }

    public class HostLogEntry
    {
        private const string StartString = "CompareHost: AddServiceEndpoint";

        public HostLogEntry() { }

        public HostLogEntry(string line) : this(new LogEntry(line)) { }

        public HostLogEntry(LogEntry entry)
        {
            this.LogTime = entry.LogTime;

            string message = entry.Message.Trim();
            if (message.StartsWith(StartString))
            {
                message = message.Substring(StartString.Length).TrimStart();
                string[] attributes = message.Split(',');
                foreach (string attribute in attributes)
                {
                    string[] keyVal = attribute.Split('=');
                    if (keyVal.Length >= 2)
                    {
                        string key = keyVal[0].Trim();
                        switch (key)
                        {
                            case "Transport":
                                this.Transport = keyVal[1].Trim();
                                break;
                            case "Chunking":
                                this.ChunkingEnabled = Boolean.Parse(keyVal[1]);
                                break;
                            case "Contract":
                                this.ContractInterface = keyVal[1];
                                break;
                            case "Uri":
                                this.Uri = keyVal[1];
                                break;
                        }
                    }
                }
            }
            else
            {
                Transport = string.Empty;
                ChunkingEnabled = false;
                ContractInterface = string.Empty;
                Uri = string.Empty;
            }            
        }

        public DateTime LogTime { get; set; }
        public string Transport { get; set; }
        public bool ChunkingEnabled { get; set; }
        public string ContractInterface { get; set; }
        public string Uri { get; set; }
    }

    public class AuditLogEntry
    {
        public AuditLogEntry() { }

        public AuditLogEntry(string line) : this(new LogEntry(line)) { }

        public AuditLogEntry(LogEntry entry)
        {
            this.LogTime = entry.LogTime;

            if (entry.Message != string.Empty)
            {
                string[] attributes = entry.Message.Split(',');
                foreach (string attribute in attributes)
                {
                    string[] keyVal = attribute.Split('=');
                    if (keyVal.Length >= 2)
                    {
                        string key = keyVal[0].Trim();
                        switch (key)
                        {
                            case "user":
                                this.UserName = keyVal[1].Trim();
                                break;
                            case "redline size":
                                this.RedlineSize = Int32.Parse(keyVal[1].Trim());
                                break;
                            case "summary":
                                this.SummarySize = Int32.Parse(keyVal[1].Trim());
                                break;
                            case "original size":
                                this.OriginalSize = Double.Parse(keyVal[1].Trim());
                                break;
                            case "modified size":
                                this.ModifiedSize = Double.Parse(keyVal[1].Trim());
                                break;
                            case "original conversion":
                                this.OriginalConversionTime = Double.Parse(keyVal[1].Trim());
                                break;
                            case "modified conversion":
                                this.ModifiedConversionTime = Double.Parse(keyVal[1].Trim());
                                break;
                            case "original preprocess":
                                this.OriginalPreProcessTime = Double.Parse(keyVal[1].Trim());
                                break;
                            case "modified preprocess":
                                this.ModifiedPreProcessTime = Double.Parse(keyVal[1].Trim());
                                break;
                            case "comparison execute":
                                this.ComparisonExecutionTime = Double.Parse(keyVal[1].Trim());
                                break;
                            case "results processing":
                                this.ResultsProcessingTime = Double.Parse(keyVal[1].Trim());
                                break;
                            case "total execute":
                                this.TotalExecutionTime = Double.Parse(keyVal[1].Trim());
                                break;
                        }
                    }
                }
            }
            else
            {
                this.LogTime = DateTime.MinValue;
            }
        }

        public DateTime LogTime { get; set; }
        public string UserName { get; set; }
        public int RedlineSize { get; set; }
        public int SummarySize { get; set; }
        public double OriginalConversionTime { get; set; }
        public double OriginalPreProcessTime { get; set; }
        public double OriginalSize { get; set; }
        public double ModifiedSize { get; set; }
        public double ModifiedConversionTime { get; set; }
        public double ModifiedPreProcessTime { get; set; }
        public double ComparisonExecutionTime { get; set; }
        public double ResultsProcessingTime { get; set; }
        public double TotalExecutionTime { get; set; }
    }

    public enum Range
    {
        Day = 1,
        Week = 7,
        Month = 30,
        Year = 365
    }
}