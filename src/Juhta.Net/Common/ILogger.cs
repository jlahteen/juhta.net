
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines an interface for the loggers to be used with the framework.
    /// </summary>
    public interface ILogger
    {
        #region Methods

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="message">Specifies an AlertMessage object.</param>
        void LogAlert(AlertMessage message);

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="message">Specifies an alert message.</param>
        void LogAlert(string message);

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="message">Specifies an AlertMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        void LogAlert(AlertMessage message, params object[] args);

        /// <summary>
        /// Logs an alert event.
        /// </summary>
        /// <param name="messageFormat">Specifies an alert message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        void LogAlert(string messageFormat, params object[] args);

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="message">Specifies an ErrorMessage object.</param>
        void LogError(ErrorMessage message);

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="exception">Specifies an Exception object whose string representation determines the error
        /// message.</param>
        void LogError(Exception exception);

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        void LogError(string message);

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="message">Specifies an ErrorMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        void LogError(ErrorMessage message, params object[] args);

        /// <summary>
        /// Logs an error event consisting of an error message and exception.
        /// </summary>
        /// <param name="exception">Specifies an Exception object.</param>
        /// <param name="message">Specifies an ErrorMessage object.</param>
        void LogError(Exception exception, ErrorMessage message);

        /// <summary>
        /// Logs an error event consisting of an error message and exception.
        /// </summary>
        /// <param name="exception">Specifies an Exception object.</param>
        /// <param name="message">Specifies an error message.</param>
        void LogError(Exception exception, string message);

        /// <summary>
        /// Logs an error event.
        /// </summary>
        /// <param name="messageFormat">Specifies an error message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        void LogError(string messageFormat, params object[] args);

        /// <summary>
        /// Logs an error event consisting of an error message and exception.
        /// </summary>
        /// <param name="exception">Specifies an Exception object.</param>
        /// <param name="message">Specifies an ErrorMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        void LogError(Exception exception, ErrorMessage message, params object[] args);

        /// <summary>
        /// Logs an error event consisting of an error message and exception.
        /// </summary>
        /// <param name="exception">Specifies an Exception object.</param>
        /// <param name="messageFormat">Specifies an error message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        void LogError(Exception exception, string messageFormat, params object[] args);

        /// <summary>
        /// Logs a diagnostic event.
        /// </summary>
        /// <param name="message">Specifies a DiagnosticMessage object.</param>
        void LogEvent(DiagnosticMessage message);

        /// <summary>
        /// Logs a diagnostic event.
        /// </summary>
        /// <param name="message">Specifies a DiagnosticMessage object whose Message property contains zero or more
        /// format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        void LogEvent(DiagnosticMessage message, params object[] args);

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="message">Specifies an InformationMessage object.</param>
        void LogInformation(InformationMessage message);

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="message">Specifies an information message.</param>
        void LogInformation(string message);

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="message">Specifies an InformationMessage object whose Message property contains zero or more
        /// format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        void LogInformation(InformationMessage message, params object[] args);

        /// <summary>
        /// Logs an information event.
        /// </summary>
        /// <param name="messageFormat">Specifies an information message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        void LogInformation(string messageFormat, params object[] args);

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="message">Specifies a warning message.</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="message">Specifies a WarningMessage object.</param>
        void LogWarning(WarningMessage message);

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="messageFormat">Specifies a warning message format containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="messageFormat"/>.</param>
        void LogWarning(string messageFormat, params object[] args);

        /// <summary>
        /// Logs a warning event.
        /// </summary>
        /// <param name="message">Specifies a WarningMessage object whose Message property contains zero or more format
        /// items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in the Message property of <paramref name="message"/>.</param>
        void LogWarning(WarningMessage message, params object[] args);

        #endregion

        #region Properties

        /// <summary>
        /// Returns true if this <see cref="ILogger"/> instance is thread-safe, otherwise false.
        /// </summary>
        bool IsThreadSafe {get;}

        #endregion
    }
}
