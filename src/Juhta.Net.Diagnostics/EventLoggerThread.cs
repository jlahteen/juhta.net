
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Threading;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a class that performs diagnostic event logging to the configured event streams at the backgound.
    /// </summary>
    internal class EventLoggerThread : ConsumerThread<Event>, IEventLogger
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public EventLoggerThread()
        {
            m_configStateLock = DiagnosticsLibrary.Instance.GetConfigStateLock();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IEventLogger.LogEvent"/>.
        /// </summary>
        public void LogEvent(Event @event)
        {
            Consume(@event);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Writes a specified Event object to the current event streams.
        /// </summary>
        /// <param name="event">Specifies an Event object.</param>
        protected override void ConsumeObject(Event @event)
        {
            m_configStateLock.EnterReadLock();

            try
            {
                foreach (IEventStream eventStream in m_eventStreams)

                    try
                    {
                        if (eventStream.EventFilter != null)
                            if (eventStream.EventFilter.StopsEvent(@event))
                                continue;

                        eventStream.Open();

                        eventStream.WriteEvent(@event);

                        eventStream.Close();
                    }

                    catch (Exception ex)
                    {
                        EventLogger.BasicLogger.WriteToEventLog(DiagnosticMessages.Error007_2x, eventStream.Uri, ex);
                    }
            }

            finally
            {
                m_configStateLock.ExitReadLock();
            }
        }

        /// <summary>
        /// See <see cref="ConsumerThread&lt;T&gt;.OnConsumeObjectFailed"/>.
        /// </summary>
        protected override void OnConsumeObjectFailed(Event @object, Exception exception)
        {
            EventLogger.BasicLogger.WriteToEventLog(DiagnosticMessages.Error008_1x, exception);
        }

        /// <summary>
        /// See <see cref="ConsumerThread&lt;T&gt;.OnWorkerThreadFailed"/>.
        /// </summary>
        protected override void OnWorkerThreadFailed(Exception exception)
        {
            EventLogger.BasicLogger.WriteToEventLog(DiagnosticMessages.Error004_1x, exception);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Stores the configuration state of this EventLoggerThread instance to a specified ConfigState object.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object.</param>
        internal void GetConfigState(ConfigState configState)
        {
            configState.EventStreams = m_eventStreams;
        }

        /// <summary>
        /// Sets the configuration state of this EventLoggerThread instance based on a specified ConfigState object.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object.</param>
        internal void SetConfigState(ConfigState configState)
        {
            m_eventStreams = configState.EventStreams;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies a lock object that manages concurrent access to the configuration state of the library.
        /// </summary>
        private ReaderWriterLockSlim m_configStateLock;

        /// <summary>
        /// Specifies the list of the current IEventStream objects.
        /// </summary>
        private List<IEventStream> m_eventStreams;

        #endregion
    }
}
