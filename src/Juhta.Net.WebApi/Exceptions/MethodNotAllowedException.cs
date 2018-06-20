
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines an exception class for the HTTP error Method Not Allowed.
    /// </summary>
    public class MethodNotAllowedException : ClientFaultException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public MethodNotAllowedException() : base(HttpStatusCode.MethodNotAllowed)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public MethodNotAllowedException(string message) : base(HttpStatusCode.MethodNotAllowed, message)
        {}

        #endregion
    }
}
