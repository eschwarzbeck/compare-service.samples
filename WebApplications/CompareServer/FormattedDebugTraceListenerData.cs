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

using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Diagnostics;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System;
using System.Linq.Expressions;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Container = Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Container;

namespace CompareServer
{
    [Description("Logs stuff to the debug view")]
    [DisplayName("Compare Server Debug Log")]
    class FormattedDebugTraceListenerData : TraceListenerData
    {
        private const string HeaderProperty = "header";
        private const string FooterProperty = "footer";
        private const string FormatterNameProperty = "formatter";


        public FormattedDebugTraceListenerData()
            : base(typeof(FlatFileTraceListener))
        {
            ListenerDataType = typeof(FormattedDebugTraceListenerData);
        }

        public FormattedDebugTraceListenerData(string formatterName)
            : this("unnamed", formatterName)
        { }

        public FormattedDebugTraceListenerData(string name, string formatterName) 
            : this(name, typeof(FlatFileTraceListener), formatterName) 
        { }

        public FormattedDebugTraceListenerData(string name, string header, string footer, string formatterName)
            : this(name, header, footer, formatterName, TraceOptions.None)
        { }

        public FormattedDebugTraceListenerData(string name, string header, string footer, string formatterName, TraceOptions traceOutputOptions)
            : this(name, typeof(FlatFileTraceListener), formatterName, traceOutputOptions)
        {
            Header = header;
            Footer = footer;
        }

        public FormattedDebugTraceListenerData(string name, Type listenerType, string formatterName)
            : this(name, listenerType, formatterName, TraceOptions.None)
        { }

        public FormattedDebugTraceListenerData(string name, Type listenerType, string formatterName, TraceOptions traceOutputOptions)
            : base(name, listenerType, traceOutputOptions)
        {
            Formatter = formatterName;
        }

        [ConfigurationProperty(HeaderProperty, IsRequired=false, DefaultValue="----------------------------------------------")]
        public string Header
        {
            get 
            {
                return (string)base[HeaderProperty];
            }
            set 
            {
                base[HeaderProperty] = value;
            }
        }

        [ConfigurationProperty(FooterProperty, IsRequired = false, DefaultValue = "----------------------------------------")]
        public string Footer
        {
            get
            {
                return (string)base[FooterProperty];
            }
            set
            {
                base[FooterProperty] = value;
            }
        }

        [ConfigurationProperty(FormatterNameProperty, IsRequired=false)]
        [Reference(typeof(NameTypeConfigurationElementCollection<FormatterData, CustomFormatterData>), typeof(FormatterData))]
        public string Formatter
        {
            get
            {
                return (string)base[FormatterNameProperty];
            }
            set
            {
                base[FormatterNameProperty] = value;
            }
        }

        protected override Expression<Func<TraceListener>> GetCreationExpression()
        {
            return () => new FormattedDebugTraceListener(Container.ResolvedIfNotNull<ILogFormatter>(Formatter), Header, Footer);
        }
    }
}
