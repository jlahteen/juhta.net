
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a class for information messages.
    /// </summary>
    public class InformationMessage : DiagnosticMessage
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        public InformationMessage(string message) : base(DiagnosticMessageType.Information, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a message that will be associated with the instance. The value can contain
        /// format items.</param>
        /// <param name="id">Specifies a value for the <see cref="DiagnosticMessage.Id"/> property.</param>
        public InformationMessage(string message, string id) : base(DiagnosticMessageType.Information, message, id)
        {}

        #endregion
    }
}
