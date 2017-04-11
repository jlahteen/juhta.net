
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a logger class that writes log events to a file.
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Static Constructor
        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public FileLogger() : this(null) {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. The path can be relative or absolute. Can be null in
        /// which case the default log file path will be used.</param>
        /// <remarks>
        /// <para>The default log file will be written to the current user's temp folder with the process name.</para>
        /// <para>If the log file already exists, new rows will be appended to the end of the file.</para>
        /// </remarks>
        public FileLogger(string logFilePath)
        {
            Process process = Process.GetCurrentProcess();

            m_logFilePath = logFilePath;

            m_processId = process.Id;

            m_processName = process.ProcessName;

            m_logTitle = String.Format(c_logTitleFormat, m_processName, m_processId);

            if (m_logFilePath == null)
                m_logFilePath = Path.GetTempPath() + m_processName + ".log";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="ILogger.LogAlert(AlertMessage)"/>.
        /// </summary>
        public void LogAlert(AlertMessage message)
        {
            WriteLogEvent(DiagnosticMessageType.Alert, message.Id, message.Message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogAlert(string)"/>.
        /// </summary>
        public void LogAlert(string message)
        {
            WriteLogEvent(DiagnosticMessageType.Alert, null, message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogAlert(AlertMessage, object[])"/>.
        /// </summary>
        public void LogAlert(AlertMessage message, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Alert, message.Id, message.Message, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogAlert(string, object[])"/>.
        /// </summary>
        public void LogAlert(string messageFormat, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Alert, null, messageFormat, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(ErrorMessage)"/>.
        /// </summary>
        public void LogError(ErrorMessage message)
        {
            WriteLogEvent(DiagnosticMessageType.Error, message.Id, message.Message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(Exception)"/>.
        /// </summary>
        public void LogError(Exception exception)
        {
            WriteLogEvent(DiagnosticMessageType.Error, null, exception.ToString(), null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(string)"/>.
        /// </summary>
        public void LogError(string message)
        {
            WriteLogEvent(DiagnosticMessageType.Error, null, message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(ErrorMessage, object[])"/>.
        /// </summary>
        public void LogError(ErrorMessage message, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Error, message.Id, message.Message, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(string, object[])"/>.
        /// </summary>
        public void LogError(string messageFormat, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Error, null, messageFormat, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogEvent(DiagnosticMessage)"/>.
        /// </summary>
        public void LogEvent(DiagnosticMessage message)
        {
            WriteLogEvent(message.Type, message.Id, message.Message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogEvent(DiagnosticMessage, object[])"/>.
        /// </summary>
        public void LogEvent(DiagnosticMessage message, params object[] args)
        {
            WriteLogEvent(message.Type, message.Id, message.Message, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(InformationMessage)"/>.
        /// </summary>
        public void LogInformation(InformationMessage message)
        {
            WriteLogEvent(DiagnosticMessageType.Information, message.Id, message.Message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(string)"/>.
        /// </summary>
        public void LogInformation(string message)
        {
            WriteLogEvent(DiagnosticMessageType.Information, null, message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(InformationMessage, object[])"/>.
        /// </summary>
        public void LogInformation(InformationMessage message, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Information, message.Id, message.Message, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(string, object[])"/>.
        /// </summary>
        public void LogInformation(string messageFormat, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Information, null, messageFormat, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(string)"/>.
        /// </summary>
        public void LogWarning(string message)
        {
            WriteLogEvent(DiagnosticMessageType.Warning, null, message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(WarningMessage)"/>.
        /// </summary>
        public void LogWarning(WarningMessage message)
        {
            WriteLogEvent(DiagnosticMessageType.Warning, message.Id, message.Message, null);
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(string, object[])"/>.
        /// </summary>
        public void LogWarning(string messageFormat, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Warning, null, messageFormat, args);
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(WarningMessage, object[])"/>.
        /// </summary>
        public void LogWarning(WarningMessage message, params object[] args)
        {
            WriteLogEvent(DiagnosticMessageType.Warning, message.Id, message.Message, args);
        }

        #endregion

        #region Public Properties
        #endregion

        #region Public Indexers
        #endregion

        #region Public Events
        #endregion

        #region Public Exception Classes
        #endregion

        #region Public Types
        #endregion

        #region Public Constants
        #endregion

        #region Protected Constructors
        #endregion

        #region Protected Methods
        #endregion

        #region Protected Properties
        #endregion

        #region Protected Indexers
        #endregion

        #region Protected Events
        #endregion

        #region Protected Exception Classes
        #endregion

        #region Protected Types
        #endregion

        #region Protected Constants
        #endregion

        #region Protected Fields
        #endregion

        #region Internal Constructors
        #endregion

        #region Internal Methods
        #endregion

        #region Internal Properties
        #endregion

        #region Internal Indexers
        #endregion

        #region Internal Events
        #endregion

        #region Internal Exception Classes
        #endregion

        #region Internal Types
        #endregion

        #region Internal Constants
        #endregion

        #region Private Constructors
        #endregion

        #region Private Methods

        private void WriteLogEvent(DiagnosticMessageType messageType, string messageId, string messageFormat, params object[] args)
        {
            Utf8FileWriter logFile = new Utf8FileWriter(m_logFilePath, FileMode.Append);
            StringBuilder logEventInfo = new StringBuilder();
            string message;

            try
            {
                if (m_logTitle != null)
                {
                    logFile.WriteLine();

                    logFile.WriteLine(m_logTitle);
                }

                if (messageType == DiagnosticMessageType.Alert)
                    logEventInfo.Append("!");

                else if (messageType == DiagnosticMessageType.Information)
                    logEventInfo.Append("?");

                else if (messageType == DiagnosticMessageType.Warning)
                    logEventInfo.Append("#");

                else
                    logEventInfo.Append("X");

                logEventInfo.AppendFormat(" {0} {1} ", DateTime.Now.ToTimestamp('T', true, true), messageType.ToString().ToUpper());

                if (messageId != null)
                    logEventInfo.AppendFormat("'{0}' ", messageId);

                logEventInfo.AppendFormat("in {0} ({1}.{2}:", m_processName, m_processId, Thread.CurrentThread.ManagedThreadId);

                logFile.WriteLine();

                logFile.WriteLine(logEventInfo.ToString());

                logFile.TabSize = c_tabSize;

                logFile.IndentLevel = 1;

                message = String.Format(messageFormat, args);

                foreach (string line in Regex.Split(message, "\r\n|\r|\n"))
                    logFile.WriteLine(line);
            }

            finally
            {
                logFile.Close();
            }
        }

        #endregion

        #region Private Properties
        #endregion

        #region Private Indexers
        #endregion

        #region Private Events
        #endregion

        #region Private Exception Classes
        #endregion

        #region Private Types
        #endregion

        #region Private Constants

        /// <summary>
        /// Defines the log title format.
        /// </summary>
        private const string c_logTitleFormat = "* PROCESS LOG {0} ({1}) *";

        /// <summary>
        /// Defines the tab size for the log event messages.
        /// </summary>
        private const int c_tabSize = 2;

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the log file path. The path can be relative or absolute.
        /// </summary>
        private string m_logFilePath;

        /// <summary>
        /// Specifies the log title. The value null means that the title has been written to the log.
        /// </summary>
        private string m_logTitle;

        /// <summary>
        /// Specifies the ID of the process.
        /// </summary>
        private int m_processId;

        /// <summary>
        /// Specifies the name of the process.
        /// </summary>
        private string m_processName;

        #endregion

        public bool IsThreadSafe => throw new NotImplementedException();


    }
}
