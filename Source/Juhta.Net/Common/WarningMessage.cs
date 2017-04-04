
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
    /// Defines a class for warning messages.
    /// </summary>
    public class WarningMessage : DiagnosticMessage
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        public WarningMessage(string message) : base(DiagnosticMessageType.Warning, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        /// <param name="id">Specifies a value for the <see cref="DiagnosticMessage.Id"/> property.</param>
        public WarningMessage(string message, string id) : base(DiagnosticMessageType.Warning, message, id)
        {}

        #endregion
    }
}
