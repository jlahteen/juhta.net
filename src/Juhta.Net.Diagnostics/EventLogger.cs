
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Xml;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a static event logger class that provides services for diagnostic event logging.
    /// </summary>
    public static class EventLogger
    {
        #region Public Methods

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="message">Specifies an AlertMessage object.</param>
        public static void LogAlert(AlertMessage message)
        {
            LogEvent(EventType.Alert, message, null);
        }

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="message">Specifies an event message.</param>
        public static void LogAlert(string message)
        {
            LogEvent(EventType.Alert, message, null);
        }

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="message">Specifies an AlertMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        public static void LogAlert(AlertMessage message, params object[] args)
        {
            LogEvent(EventType.Alert, message, args);
        }

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="messageFormat">Specifies an event message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        public static void LogAlert(string messageFormat, params object[] args)
        {
            LogEvent(EventType.Alert, messageFormat, args);
        }

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="message">Specifies an ErrorMessage object.</param>
        public static void LogError(ErrorMessage message)
        {
            LogEvent(EventType.Error, message, null);
        }

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="exception">Specifies an Exception object whose string representation determines the event
        /// message.</param>
        public static void LogError(Exception exception)
        {
            LogEvent(exception);
        }

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="message">Specifies an event message.</param>
        public static void LogError(string message)
        {
            LogEvent(EventType.Error, message, null);
        }

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="message">Specifies an ErrorMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        public static void LogError(ErrorMessage message, params object[] args)
        {
            LogEvent(EventType.Error, message, args);
        }

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="messageFormat">Specifies an event message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        public static void LogError(string messageFormat, params object[] args)
        {
            LogEvent(EventType.Error, messageFormat, args);
        }

        /// <summary>
        /// Logs a diagnostic event.
        /// </summary>
        /// <param name="message">Specifies a DiagnosticMessage object.</param>
        public static void LogEvent(DiagnosticMessage message)
        {
            LogEvent((EventType)message.Type, message, null);
        }

        /// <summary>
        /// Logs a diagnostic event.
        /// </summary>
        /// <param name="message">Specifies a DiagnosticMessage object whose Message property contains zero or more
        /// format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        public static void LogEvent(DiagnosticMessage message, params object[] args)
        {
            LogEvent((EventType)message.Type, message, args);
        }

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="message">Specifies an InformationMessage object.</param>
        public static void LogInformation(InformationMessage message)
        {
            LogEvent(EventType.Information, message, null);
        }

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="message">Specifies an event message.</param>
        public static void LogInformation(string message)
        {
            LogEvent(EventType.Information, message, null);
        }

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="message">Specifies an InformationMessage object whose Message property contains zero or more
        /// format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        public static void LogInformation(InformationMessage message, params object[] args)
        {
            LogEvent(EventType.Information, message, args);
        }

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="messageFormat">Specifies an event message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        public static void LogInformation(string messageFormat, params object[] args)
        {
            LogEvent(EventType.Information, messageFormat, args);
        }

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="message">Specifies an event message.</param>
        public static void LogWarning(string message)
        {
            LogEvent(EventType.Warning, message, null);
        }

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="message">Specifies a WarningMessage object.</param>
        public static void LogWarning(WarningMessage message)
        {
            LogEvent(EventType.Warning, message, null);
        }

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="messageFormat">Specifies an event message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        public static void LogWarning(string messageFormat, params object[] args)
        {
            LogEvent(EventType.Warning, messageFormat, args);
        }

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="message">Specifies a WarningMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        public static void LogWarning(WarningMessage message, params object[] args)
        {
            LogEvent(EventType.Warning, message, args);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Closes the event logger.
        /// </summary>
        /// <returns>Returns the number of errors occurred in the closing operation.</returns>
        internal static int Close()
        {
            int errors = 0;

            // Restore the basic logger
            s_eventLogger = s_basicLogger;

            try
            {
                // Stop the event logger thread if necessary
                if (s_eventLoggerThread != null)
                    s_eventLoggerThread.Stop();
            }

            catch (Exception ex)
            {
                // Log the error
                // Note: the basic logger is still on duty
                EventLogger.LogError(CommonDiagnosticMessages.Error013_2x, DiagnosticsLibrary.Instance.LibraryName, ex);

                errors += 1;
            }

            return(errors);
        }

        /// <summary>
        /// Creates an instance of the basic event logger, and sets it as the current event logger.
        /// </summary>
        internal static void CreateBasicLogger()
        {
            s_basicLogger = new BasicLogger();

            s_eventLogger = s_basicLogger;
        }

        /// <summary>
        /// Stores the configuration state of the event logger to a specified ConfigState object.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object.</param>
        internal static void GetConfigState(ConfigState configState)
        {
            s_eventLoggerThread.GetConfigState(configState);
        }

        /// <summary>
        /// Initializes the event logger.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object containing a configuration state.</param>
        internal static void Initialize(ConfigState configState)
        {
            s_eventLoggerThread = new EventLoggerThread();

            s_eventLoggerThread.SetConfigState(configState);

            s_eventLoggerThread.Start();

            s_eventLogger = s_eventLoggerThread;
        }

        /// <summary>
        /// Sets the configuration state of the event logger based on a specified ConfigState object.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object.</param>
        internal static void SetConfigState(ConfigState configState)
        {
            s_eventLoggerThread.SetConfigState(configState);
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets an instance of BasicLogger that is used before initialization and after closing of Diagnostics Library
        /// as well as for internal event logging.
        /// </summary>
        internal static BasicLogger BasicLogger
        {
            get {return(s_basicLogger);}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates an Event object based on specified parameters.
        /// </summary>
        /// <param name="type">Specifies an event type.</param>
        /// <param name="id">Specifies an event ID. Can be null.</param>
        /// <param name="message">Specifies an event message.</param>
        /// <returns>Returns the created Event object.</returns>
        private static Event CreateEvent(EventType type, string id, string message)
        {
            switch (type)
            {
                case EventType.Alert:
                    return(new AlertEvent(message, id));

                case EventType.Error:
                    return(new ErrorEvent(message, id));

                case EventType.Information:
                    return(new InformationEvent(message, id));

                case EventType.Warning:
                    return(new WarningEvent(message, id));

                default:
                    throw new UnimplementedCodeBranchException(type);
            }
        }

        /// <summary>
        /// Logs an error event based on a specified exception.
        /// </summary>
        /// <param name="exception">Specifies an Exception object.</param>
        private static void LogEvent(Exception exception)
        {
            string messageID;
            Event @event;

            try
            {
                if (TryGetMessageID(exception, out messageID))
                    @event = CreateEvent(EventType.Error, messageID, exception.ToString());
                else
                    @event = CreateEvent(EventType.Error, null, exception.ToString());

                s_eventLogger.LogEvent(@event);
            }

            catch (Exception ex)
            {
                s_basicLogger.WriteToEventLog(DiagnosticMessages.Error006_1x, ex);
            }
        }

        /// <summary>
        /// Logs a diagnostic event.
        /// </summary>
        /// <param name="eventType">Specifies an event type.</param>
        /// <param name="message">Specifies a DiagnosticMessage object whose Message property contains zero or more
        /// format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        private static void LogEvent(EventType eventType, DiagnosticMessage message, params object[] args)
        {
            Event @event;

            try
            {
                @event = CreateEvent(eventType, message.ID, message.FormatMessage(args));

                s_eventLogger.LogEvent(@event);
            }

            catch (Exception ex)
            {
                s_basicLogger.WriteToEventLog(DiagnosticMessages.Error006_1x, ex);
            }
        }

        /// <summary>
        /// Logs a diagnostic event.
        /// </summary>
        /// <param name="eventType">Specifies an event type.</param>
        /// <param name="messageFormat">Specifies an event message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>. Can be null in which case messageFormat
        /// will be treated as a 'normal' string.</param>
        private static void LogEvent(EventType eventType, string messageFormat, params object[] args)
        {
            Event @event;

            try
            {
                if (args != null)
                    @event = CreateEvent(eventType, null, String.Format(messageFormat, args));
                else
                    @event = CreateEvent(eventType, null, messageFormat);

                s_eventLogger.LogEvent(@event);
            }

            catch (Exception ex)
            {
                s_basicLogger.WriteToEventLog(DiagnosticMessages.Error006_1x, ex);
            }
        }

        /// <summary>
        /// Tries to get an ID for one of the messages specified by a given exception or its inner exceptions.
        /// </summary>
        /// <param name="exception">Specifies an Exception object.</param>
        /// <param name="messageID">If the function returns true, returns the ID that was found for one of the messages
        /// specified by the given exception or its inner exceptions.</param>
        /// <returns>Returns true, if an ID was found for one of the messages specified by the given exception or its
        /// inner exceptions.</returns>
        /// <remarks>Exceptions will be gone through starting from the innermost exception.</remarks>
        private static bool TryGetMessageID(Exception exception, out string messageID)
        {
            Exception exception2 = exception;
            List<string> messageList = new List<string>();

            messageID = null;

            while (exception2 != null)
            {
                messageList.Add(exception2.Message);

                exception2 = exception2.InnerException;
            }

            messageList.Reverse();

            foreach (string message in messageList)
                if (DiagnosticMessage.TryGetMessageID(message, out messageID))
                    return(true);

            return(false);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="BasicLogger"/> property.
        /// </summary>
        private static BasicLogger s_basicLogger;

        /// <summary>
        /// Specifies the current IEventLogger instance.
        /// </summary>
        private static volatile IEventLogger s_eventLogger;

        /// <summary>
        /// Specifies an EventLoggerThread object that performs actual event logging in the background.
        /// </summary>
        private static EventLoggerThread s_eventLoggerThread;

        #endregion
    }
}
