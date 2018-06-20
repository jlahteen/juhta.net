
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Represents an event logger that is used before initialization and after closing of Diagnostics Library. The
    /// second role of this class is to act as an internal log for Diagnostics Library, that is, to provide a solid and
    /// robust log for such errors that relate to actual event logging.
    /// </summary>
    internal class BasicLogger : IEventLogger
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public BasicLogger()
        {
            if (EventLog.TryEnsureSource(ProductInfo.Name))
                m_eventLog = new EventLog(ProductInfo.Name);
            else
                m_eventLog = new EventLog(EventLog.DefaultSourceName);

            m_eventLogStream = new EventLogStream();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IEventLogger.LogEvent"/>.
        /// </summary>
        public void LogEvent(Event @event)
        {
            m_eventLogStream.Open();

            m_eventLogStream.WriteEvent(@event);

            m_eventLogStream.Close();
        }

        /// <summary>
        /// Writes an error directly to Windows Event Log.
        /// </summary>
        /// <param name="message">Specifies an ErrorMessage object.</param>
        public void WriteToEventLog(ErrorMessage message)
        {
            try
            {
                m_eventLog.WriteError(message.GetIntegerID(), message.GetMessage());
            }

            catch
            {}
        }

        /// <summary>
        /// Writes an error directly to Windows Event Log.
        /// </summary>
        /// <param name="message">Specifies an ErrorMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        public void WriteToEventLog(ErrorMessage message, params object[] args)
        {
            try
            {
                m_eventLog.WriteError(message.GetIntegerID(), message.FormatMessage(args));
            }

            catch
            {}
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies an EventLog object for direct Windows Event Log writing.
        /// </summary>
        private EventLog m_eventLog;

        /// <summary>
        /// Specifies a Windows Event Log stream where incoming diagnostic events will be written.
        /// </summary>
        private EventLogStream m_eventLogStream;

        #endregion
    }
}
