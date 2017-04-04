
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Juhta.Net.Common;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a factory class that creates instances of the <see cref="ConfigState"/> class.
    /// </summary>
    internal class ConfigStateFactory : ConfigStateFactoryBase<ConfigState>
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="ConfigStateFactoryBase.CreateConfigState"/>.
        /// </summary>
        public override IConfigState CreateConfigState(XmlDocument config, bool initialize)
        {
            ResetFactoryState(new ConfigState(), config, DiagnosticsLibrary.Instance.CreateNamespaceManager(config));

            CreateEventLoggerConfigState();

            CreateTraceConfigState();

            if (initialize)
                m_configState.Initialize();

            return(m_configState);
        }

        /// <summary>
        /// See <see cref="ConfigStateFactoryBase.CreateDefaultConfigState"/>.
        /// </summary>
        public override IConfigState CreateDefaultConfigState(bool initialize)
        {
            ResetFactoryState(new ConfigState(), null, null);

            CreateDefaultEventLoggerConfigState();

            CreateDefaultTraceConfigState();

            if (initialize)
                m_configState.Initialize();

            return(m_configState);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the default configuration state in respect of the event logger settings.
        /// </summary>
        private void CreateDefaultEventLoggerConfigState()
        {
            m_configState.EventStreams = CreateDefaultEventStreams();
        }

        /// <summary>
        /// Creates the default event streams.
        /// </summary>
        /// <returns>Returns a list of IEventStream objects comprising the default event streams.</returns>
        private static List<IEventStream> CreateDefaultEventStreams()
        {
            return(new List<IEventStream>(){new EventLogStream()});
        }

        /// <summary>
        /// Creates the default configuration state in respect of the trace settings.
        /// </summary>
        private void CreateDefaultTraceConfigState()
        {
            m_configState.TraceDirectory = GetDefaultTraceDirectory();

            m_configState.TraceWriter = null;

            m_configState.TracingOn = false;
        }

        /// <summary>
        /// Creates the configuration state in respect of the event logger settings.
        /// </summary>
        private void CreateEventLoggerConfigState()
        {
            XmlNode eventStreamsNode;
            List<IEventStream> eventStreams;

            // Create the default state if necessary

            if ((eventStreamsNode = m_config.SelectSingleNode("//bf:eventStreams", m_namespaceManager)) == null)
            {
                CreateDefaultEventLoggerConfigState();

                return;
            }

            // Create the state based on the configuration

            eventStreams = new List<IEventStream>();

            foreach (XmlNode eventStreamNode in eventStreamsNode.ChildNodes)

                switch (eventStreamNode.LocalName)
                {
                    case "emailStream":
                        eventStreams.Add(new EmailStream(eventStreamNode));

                        break;

                    case "eventLogStream":
                        eventStreams.Add(new EventLogStream(eventStreamNode));

                        break;

                    case "htmlFileStream":
                        eventStreams.Add(new HtmlFileStream(eventStreamNode));

                        break;

                    case "textFileStream":
                        eventStreams.Add(new TextFileStream(eventStreamNode));

                        break;

                    case "xmlFileStream":
                        eventStreams.Add(new XmlFileStream(eventStreamNode));

                        break;

                    default:
                        throw new UnimplementedCodeBranchException(eventStreamNode.LocalName);
                }

            m_configState.EventStreams = eventStreams;
        }

        /// <summary>
        /// Creates the configuration state in respect of the trace settings.
        /// </summary>
        private void CreateTraceConfigState()
        {
            XmlNode traceNode, traceWriterNode;
            XmlSchema configSchema;

            // Create the default state if necessary

            if ((traceNode = m_config.SelectSingleNode("//bf:trace", m_namespaceManager)) == null)
            {
                CreateDefaultTraceConfigState();

                return;
            }

            // Create the state based on the configuration

            configSchema = DiagnosticsLibrary.Instance.GetConfigSchema();

            if (traceNode.HasAttribute("tracingOn"))
                m_configState.TracingOn = traceNode.GetAttribute("tracingOn").ToBoolean();
            else
                m_configState.TracingOn = configSchema.GetAttributeDefaultValue("traceType", "tracingOn").ToBoolean();

            if (traceNode.HasAttribute("traceDirectory"))
                m_configState.TraceDirectory = traceNode.GetAttribute("traceDirectory").EnsureEnd("\\");
            else
                m_configState.TraceDirectory = GetDefaultTraceDirectory();

            if (!Directory.Exists(m_configState.TraceDirectory))
                throw new DirectoryNotFoundException(DiagnosticMessages.Error005_1x.FormatMessage(m_configState.TraceDirectory));

            if (!m_configState.TracingOn)
                m_configState.TraceWriter = null;

            else if ((traceWriterNode = traceNode.SelectSingleNode("bf:traceWriter", m_namespaceManager)) != null)
                m_configState.TraceWriter = ObjectFactory.CreateInstance<ITraceWriter>(traceWriterNode);

            else
                m_configState.TraceWriter = new TraceWriter(m_configState.TraceDirectory);
        }

        /// <summary>
        /// Gets the default directory for trace files.
        /// </summary>
        /// <returns>Returns the default directory for trace files, ending with a backslash.</returns>
        private static string GetDefaultTraceDirectory()
        {
            return(Path.GetTempPath().EnsureEnd("\\"));
        }

        #endregion
    }
}
