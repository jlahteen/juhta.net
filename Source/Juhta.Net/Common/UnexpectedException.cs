
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
    /// This exception will be thrown when an unexpected error occurs.
    /// </summary>
    /// <remarks>An error is considered as unexpected when it occurs in a code context that doesn't contain error-
    /// sensitive functionality such as network communication or file I/O operations.</remarks>
    public class UnexpectedException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception that determines the actual unexpected error.</param>
        public UnexpectedException(string message, Exception innerException) : base(message, innerException)
        {}

        #endregion
    }
}
