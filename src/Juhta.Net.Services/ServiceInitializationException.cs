
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Services
{
    /// <summary>
    /// This exception will be thrown when a dependency injection service cannot be initialized.
    /// </summary>
    public class ServiceInitializationException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ServiceInitializationException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an <see cref="Exception"/> object that relates to the error.</param>
        public ServiceInitializationException(string message, Exception innerException) : base(message, innerException)
        {}

        #endregion
    }
}
