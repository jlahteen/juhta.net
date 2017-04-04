
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a configuration state class for Diagnostics Library.
    /// </summary>
    internal class ConfigState : ConfigStateBase
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="ConfigStateBase.Close"/>.
        /// </summary>
        public override void Close()
        {
            if (this.TraceWriter != null)
                this.TraceWriter.Close();
        }

        /// <summary>
        /// See <see cref="ConfigStateBase.Initialize"/>.
        /// </summary>
        public override void Initialize()
        {
            if (this.TraceWriter != null)
                this.TraceWriter.Open();
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets a value that relates to the <see cref="EventLoggerThread.m_eventStreams"/> field.
        /// </summary>
        internal List<IEventStream> EventStreams
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value that relates to the <see cref="Trace.s_traceDirectory"/> field.
        /// </summary>
        internal string TraceDirectory
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value that relates to the <see cref="Trace.s_traceWriter"/> field.
        /// </summary>
        internal ITraceWriter TraceWriter
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value that relates to the <see cref="Trace.s_tracingOn"/> field.
        /// </summary>
        internal bool TracingOn
        {
            get; set;
        }

        #endregion
    }
}
