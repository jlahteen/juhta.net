
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a trace element that represents a trace block. A trace block can in practice contain any kind of trace
    /// data but it's recommended to divide single and independent tracing entities into separate trace blocks. Trace
    /// blocks cannot have a parent element but are owned by trace writers.
    /// </summary>
    internal class TraceBlock : TraceElement
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="traceWriter">Specifies a TraceWriter object that will be set as the owner for the instance.</param>
        public TraceBlock(TraceWriter traceWriter) : base(null)
        {
            m_traceWriter = traceWriter;

            m_startLine = "Block Start";

            m_endLine = "Block End";

            m_threadID = Thread.CurrentThread.ManagedThreadId;

            m_traceData = new StringBuilder();
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="traceWriter">Specifies a TraceWriter object that will be set as the owner for the instance.</param>
        /// <param name="description">Specifies a description for trace data that will be written to the instance.</param>
        public TraceBlock(TraceWriter traceWriter, string description) : this(traceWriter)
        {
            m_description = description;

            m_startLine += ": " + m_description;

            m_endLine += ": " + m_description;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends an empty line to this TraceBlock instance.
        /// </summary>
        public void AppendEmptyLine()
        {
            m_traceData.AppendLine();
        }

        /// <summary>
        /// Appends a trace line to this TraceBlock instance.
        /// </summary>
        /// <param name="traceLine">Specifies a trace line.</param>
        public void AppendLine(string traceLine)
        {
            if (traceLine.Length <= c_maxTraceLineLength)
                m_traceData.AppendLine(traceLine);
            else
                m_traceData.AppendLine(traceLine.Substring(0, c_maxTraceLineLength));
        }

        /// <summary>
        /// Clears the trace data stored in this TraceBlock instance.
        /// </summary>
        public void ClearTraceData()
        {
            m_traceData.Length = 0;
        }

        /// <summary>
        /// See <see cref="TraceElement.Close"/>.
        /// </summary>
        public override void Close()
        {
            base.Close();

            AppendEmptyLine();
        }

        /// <summary>
        /// Gets the trace data stored in this TraceBlock instance.
        /// </summary>
        /// <returns>Returns the trace data stored in this TraceBlock instance.</returns>
        public string GetTraceData()
        {
            return(m_traceData.ToString());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the ID of the managed thread that has created this TraceBlock instance.
        /// </summary>
        public int ThreadID
        {
            get {return(m_threadID);}
        }

        /// <summary>
        /// Gets the TraceWriter object that owns this TraceBlock instance.
        /// </summary>
        public TraceWriter TraceWriter
        {
            get {return(m_traceWriter);}
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the maximum length for a trace line.
        /// </summary>
        private const int c_maxTraceLineLength = 0x1000;

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ThreadID"/> property.
        /// </summary>
        private int m_threadID;

        /// <summary>
        /// Specifies a StringBuilder object that contains the trace data stored in this TraceBlock instance.
        /// </summary>
        private StringBuilder m_traceData;

        /// <summary>
        /// Stores the <see cref="TraceWriter"/> property.
        /// </summary>
        private TraceWriter m_traceWriter;

        #endregion
    }
}
