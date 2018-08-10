
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
