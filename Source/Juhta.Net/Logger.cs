
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using Juhta.Net.Common;

namespace Juhta.Net
{
    /// <summary>
    /// Defines a static wrapper class for enabling easy logging through the encapsulated <see cref="ILogger"/>
    /// instance.
    /// </summary>
    /// <remarks>This class is also capable to serialize concurrent access to non-thread-safe <see cref="ILogger"/>
    /// instances. In other words, this class is thread-safe excluding the <see cref="SetLogger(ILogger)"/> method.</remarks>
    public static class Logger
    {
        #region Static Constructor

        /// <summary>
        /// Initializes the class.
        /// </summary>
        static Logger()
        {
            s_syncLock = new object();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="ILogger.LogAlert(AlertMessage)"/>.
        /// </summary>
        public static void LogAlert(AlertMessage message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogAlert(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogAlert(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogAlert(string)"/>.
        /// </summary>
        /// <param name="message"></param>
        public static void LogAlert(string message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogAlert(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogAlert(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogAlert(AlertMessage, object[])"/>.
        /// </summary>
        public static void LogAlert(AlertMessage message, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogAlert(message, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogAlert(message, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogAlert(string, object[])"/>.
        /// </summary>
        public static void LogAlert(string messageFormat, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogAlert(messageFormat, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogAlert(messageFormat, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(ErrorMessage)"/>.
        /// </summary>
        public static void LogError(ErrorMessage message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogError(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogError(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(Exception)"/>.
        /// </summary>
        public static void LogError(Exception exception)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogError(exception);

            else
                lock(s_syncLock)
                {
                    s_logger.LogError(exception);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(string)"/>.
        /// </summary>
        public static void LogError(string message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogError(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogError(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(ErrorMessage, object[])"/>.
        /// </summary>
        public static void LogError(ErrorMessage message, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogError(message, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogError(message, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogError(string, object[])"/>.
        /// </summary>
        public static void LogError(string messageFormat, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogError(messageFormat, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogError(messageFormat, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogEvent(DiagnosticMessage)"/>.
        /// </summary>
        public static void LogEvent(DiagnosticMessage message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogEvent(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogEvent(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogEvent(DiagnosticMessage, object[])"/>.
        /// </summary>
        public static void LogEvent(DiagnosticMessage message, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogEvent(message, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogEvent(message, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(InformationMessage)"/>.
        /// </summary>
        public static void LogInformation(InformationMessage message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogInformation(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogInformation(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(string)"/>.
        /// </summary>
        public static void LogInformation(string message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogInformation(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogInformation(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(InformationMessage, object[])"/>.
        /// </summary>
        public static void LogInformation(InformationMessage message, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogInformation(message, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogInformation(message, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogInformation(string, object[])"/>.
        /// </summary>
        public static void LogInformation(string messageFormat, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogInformation(messageFormat, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogInformation(messageFormat, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(string)"/>.
        /// </summary>
        public static void LogWarning(string message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogWarning(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogWarning(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(WarningMessage)"/>.
        /// </summary>
        public static void LogWarning(WarningMessage message)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogWarning(message);

            else
                lock(s_syncLock)
                {
                    s_logger.LogWarning(message);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(string, object[])"/>.
        /// </summary>
        public static void LogWarning(string messageFormat, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogWarning(messageFormat, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogWarning(messageFormat, args);
                }
        }

        /// <summary>
        /// See <see cref="ILogger.LogWarning(WarningMessage, object[])"/>.
        /// </summary>
        public static void LogWarning(WarningMessage message, params object[] args)
        {
            if (s_logger == null)
                return;

            else if (s_logger.IsThreadSafe)
                s_logger.LogWarning(message, args);

            else
                lock(s_syncLock)
                {
                    s_logger.LogWarning(message, args);
                }
        }

        /// <summary>
        /// Sets an <see cref="ILogger"/> instance into the class.
        /// </summary>
        /// <param name="logger">Specifies an <see cref="ILogger"/> object.</param>
        public static void SetLogger(ILogger logger)
        {
            s_logger = logger;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the encapsulated <see cref="ILogger"/> instance.
        /// </summary>
        private static ILogger s_logger;

        /// <summary>
        /// Specifies a synchronization object to serialize access to non-thread-safe logger instances.
        /// </summary>
        private static object s_syncLock;

        #endregion
    }
}
