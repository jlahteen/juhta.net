
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ClientErrorExceptions
{
    /// <summary>
    /// Defines an exception class for the HTTP error Proxy Authentication Required.
    /// </summary>
    public class ProxyAuthenticationRequiredException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ProxyAuthenticationRequiredException() : base(HttpStatusCode.ProxyAuthenticationRequired)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        public ProxyAuthenticationRequiredException(Enum errorCode) : base(HttpStatusCode.ProxyAuthenticationRequired, null, errorCode.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ProxyAuthenticationRequiredException(string message) : base(HttpStatusCode.ProxyAuthenticationRequired, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public ProxyAuthenticationRequiredException(Enum errorCode, string message) : base(HttpStatusCode.ProxyAuthenticationRequired, message, errorCode.ToString())
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrorResponse">Specifies a client error response based on which to create the instance.</param>
        internal ProxyAuthenticationRequiredException(ClientErrorResponse clientErrorResponse) : base(clientErrorResponse)
        {}

        #endregion
    }
}
