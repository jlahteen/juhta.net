
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Diagnostics;
using Juhta.Net.Common;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Represents a trace file. This class provides services for writing trace data to UTF8-encoded text files.
    /// </summary>
    public class TraceFile : Utf8FileWriter
    {
        #region Static Constructor

        /// <summary>
        /// Initializes the class.
        /// </summary>
        static TraceFile()
        {
            s_lastDigitTimestamp = DateTime.Now.ToDigitTimestamp();

            s_syncLock = new object();
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public TraceFile() : base(Trace.TraceDirectory.EnsureEnd("\\") + BuildTraceFileName())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="traceDirectory">Specifies a directory where the trace file will be stored.</param>
        public TraceFile(string traceDirectory) : base(traceDirectory.EnsureEnd("\\") + BuildTraceFileName())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="traceDirectory">Specifies a directory where the trace file will be stored.</param>
        /// <param name="traceFile">Specifies a name for the trace file.</param>
        public TraceFile(string traceDirectory, string traceFile) : base(traceDirectory.EnsureEnd("\\") + traceFile)
        {}

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds a unique trace file name at the process level.
        /// </summary>
        /// <returns>Returns the built trace file name.</returns>
        private static string BuildTraceFileName()
        {
            string digitTimestamp;
            Process currentProcess = Process.GetCurrentProcess();
            string traceFileName;

            // Get a unique digit timestamp at the process level

            lock(s_syncLock)
            {
                do
                    digitTimestamp = DateTime.Now.ToDigitTimestamp();
                while (digitTimestamp == s_lastDigitTimestamp);

                s_lastDigitTimestamp = digitTimestamp;
            }

            // Build the trace file name
            traceFileName = String.Format("Trace-{0}-{1}-{2}.txt", currentProcess.MainModule.ModuleName, currentProcess.Id, digitTimestamp);

            // Return
            return(traceFileName);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the last digit timestamp used in a trace file name.
        /// </summary>
        private static string s_lastDigitTimestamp;

        /// <summary>
        /// Specifies a synchronization object.
        /// </summary>
        private static object s_syncLock;

        #endregion
    }
}
