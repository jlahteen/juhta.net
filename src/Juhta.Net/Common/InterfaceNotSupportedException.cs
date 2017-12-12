
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
    /// This exception will be thrown when an object doesn't support a requested interface.
    /// </summary>
    public class InterfaceNotSupportedException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an InvalidCastException instance that relates to the failed
        /// interface cast operation.</param>
        public InterfaceNotSupportedException(string message, InvalidCastException innerException) : base(message, innerException)
        {}

        #endregion
    }
}
