
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a class for error messages.
    /// </summary>
    public class ErrorMessage : DiagnosticMessage
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        public ErrorMessage(string message) : base(DiagnosticMessageType.Error, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        /// <param name="id">Specifies a value for the <see cref="DiagnosticMessage.Id"/> property.</param>
        public ErrorMessage(string message, string id) : base(DiagnosticMessageType.Error, message, id)
        {}

        #endregion
    }
}
