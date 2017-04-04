
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Common
{
    #region Public Types

    /// <summary>
    /// An enumeration that defines the diagnostic message ID bases used in the framework libraries.
    /// </summary>
    public enum DiagnosticMessageIdBase
    {
        /// <summary>
        /// Defines the message ID base for the root library messages.
        /// </summary>
        RootLibraryMessages = 10000,

        /// <summary>
        /// Defines the message ID base for the common messages.
        /// </summary>
        CommonMessages = 11000
    }

    /// <summary>
    /// Defines an enumeration for the diagnostic message types.
    /// </summary>
    public enum DiagnosticMessageType
    {
        /// <summary>
        /// The diagnostic message is an alert message.
        /// </summary>
        Alert,

        /// <summary>
        /// The diagnostic message is an error message.
        /// </summary>
        Error,

        /// <summary>
        /// The diagnostic message is an information message.
        /// </summary>
        Information,

        /// <summary>
        /// The diagnostic message is a warning message.
        /// </summary>
        Warning
    }

    #endregion
}
