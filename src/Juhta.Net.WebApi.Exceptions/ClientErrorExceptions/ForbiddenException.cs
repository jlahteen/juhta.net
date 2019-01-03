
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
    /// Defines an exception class for the HTTP error Forbidden.
    /// </summary>
    public class ForbiddenException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ForbiddenException() : base(HttpStatusCode.Forbidden)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        public ForbiddenException(Enum errorCode) : base(HttpStatusCode.Forbidden, null, errorCode.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ForbiddenException(string message) : base(HttpStatusCode.Forbidden, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public ForbiddenException(Enum errorCode, string message) : base(HttpStatusCode.Forbidden, message, errorCode.ToString())
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrorResponse">Specifies a client error response based on which to create the instance.</param>
        internal ForbiddenException(ClientErrorResponse clientErrorResponse) : base(clientErrorResponse)
        {}

        #endregion
    }
}
