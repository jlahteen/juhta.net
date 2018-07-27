
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Startup
{
    /// <summary>
    /// This exception will be thrown in case of library state related errors.
    /// </summary>
    public class LibraryStateException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public LibraryStateException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception that is the actual cause for the library state
        /// error.</param>
        public LibraryStateException(string message, Exception innerException) : base(message, innerException)
        {}

        #endregion
    }
}
