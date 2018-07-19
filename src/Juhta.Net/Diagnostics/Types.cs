
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Diagnostics
{
    #region Public Types

    /// <summary>
    /// An enumeration that defines the message identifier bases for the diagnostic messages used in the framework
    /// libraries.
    /// </summary>
    public enum DiagnosticMessageIdBase
    {
        /// <summary>
        /// Defines the message identifier base for the common messages.
        /// </summary>
        CommonMessages = 100000,

        /// <summary>
        /// Defines the message identifier base for the Root Library messages.
        /// </summary>
        RootLibraryMessages = 101000,

        /// <summary>
        /// Defines the message identifier base for the Validation Library messages.
        /// </summary>
        ValidationLibraryMessages = 102000
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
