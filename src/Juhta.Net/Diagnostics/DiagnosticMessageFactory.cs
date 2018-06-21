
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Represents a class that can be used to create instances of classes deriving from the <see cref="DiagnosticMessage"/>
    /// class. The main benefit of using this class is automatic and consistent message ID generation.
    /// </summary>
    public class DiagnosticMessageFactory
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="messageIdBase">Specifies a message ID base.</param>
        /// <param name="messageNamespace">Specifies a namespace for messages to create.</param>
        public DiagnosticMessageFactory(DiagnosticMessageIdBase messageIdBase, string messageNamespace) :
            this((int)messageIdBase, messageNamespace)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="messageIdBase">Specifies a message ID base.</param>
        /// <param name="messageNamespace">Specifies a namespace for messages to create.</param>
        public DiagnosticMessageFactory(int messageIdBase, string messageNamespace) : 
            this(messageIdBase, messageNamespace + ".Info", messageNamespace + ".Warning", messageNamespace + ".Error", messageNamespace + ".Alert")
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="messageIdBase">Specifies a message ID base.</param>
        /// <param name="informationMessageIdPrefix">Specifies a prefix for information message IDs. Can be null.</param>
        /// <param name="warningMessageIdPrefix">Specifies a prefix for warning message IDs. Can be null.</param>
        /// <param name="errorMessageIdPrefix">Specifies a prefix for error message IDs. Can be null.</param>
        /// <param name="alertMessageIdPrefix">Specifies a prefix for alert message IDs. Can be null.</param>
        /// <remarks>IDs of created diagnostic messages will start from <paramref name="messageIdBase"/> + 1 in
        /// ascending order.</remarks>
        public DiagnosticMessageFactory(int messageIdBase, string informationMessageIdPrefix, string warningMessageIdPrefix, string errorMessageIdPrefix, string alertMessageIdPrefix)
        {
            m_nextMessageId = messageIdBase + 1;

            if (informationMessageIdPrefix != null)
                m_informationMessageIdPrefix = informationMessageIdPrefix;
            else
                m_informationMessageIdPrefix = String.Empty;

            if (warningMessageIdPrefix != null)
                m_warningMessageIdPrefix = warningMessageIdPrefix;
            else
                m_warningMessageIdPrefix = String.Empty;

            if (errorMessageIdPrefix != null)
                m_errorMessageIdPrefix = errorMessageIdPrefix;
            else
                m_errorMessageIdPrefix = String.Empty;

            if (alertMessageIdPrefix != null)
                m_alertMessageIdPrefix = alertMessageIdPrefix;
            else
                m_alertMessageIdPrefix = String.Empty;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates an AlertMessage instance based on a specified message.
        /// </summary>
        /// <param name="message">Specifies a message. The message can contain format items.</param>
        /// <returns>Returns the created AlertMessage instance.</returns>
        public AlertMessage CreateAlertMessage(string message)
        {
            return(new AlertMessage(message, m_alertMessageIdPrefix + (m_nextMessageId++).ToString()));
        }

        /// <summary>
        /// Creates an ErrorMessage instance based on a specified message.
        /// </summary>
        /// <param name="message">Specifies a message. The message can contain format items.</param>
        /// <returns>Returns the created ErrorMessage instance.</returns>
        public ErrorMessage CreateErrorMessage(string message)
        {
            return(new ErrorMessage(message, m_errorMessageIdPrefix + (m_nextMessageId++).ToString()));
        }

        /// <summary>
        /// Creates an InformationMessage instance based on a specified message.
        /// </summary>
        /// <param name="message">Specifies a message. The message can contain format items.</param>
        /// <returns>Returns the created InformationMessage instance.</returns>
        public InformationMessage CreateInformationMessage(string message)
        {
            return(new InformationMessage(message, m_informationMessageIdPrefix + (m_nextMessageId++).ToString()));
        }

        /// <summary>
        /// Creates a WarningMessage instance based on a specified message.
        /// </summary>
        /// <param name="message">Specifies a message. The message can contain format items.</param>
        /// <returns>Returns the created WarningMessage instance.</returns>
        public WarningMessage CreateWarningMessage(string message)
        {
            return(new WarningMessage(message, m_warningMessageIdPrefix + (m_nextMessageId++).ToString()));
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the prefix for alert message IDs.
        /// </summary>
        private string m_alertMessageIdPrefix;

        /// <summary>
        /// Specifies the prefix for error message IDs.
        /// </summary>
        private string m_errorMessageIdPrefix;

        /// <summary>
        /// Specifies the prefix for information message IDs.
        /// </summary>
        private string m_informationMessageIdPrefix;

        /// <summary>
        /// Specifies the next message ID (without a prefix).
        /// </summary>
        private int m_nextMessageId;

        /// <summary>
        /// Specifies the prefix for warning message IDs.
        /// </summary>
        private string m_warningMessageIdPrefix;

        #endregion
    }
}
