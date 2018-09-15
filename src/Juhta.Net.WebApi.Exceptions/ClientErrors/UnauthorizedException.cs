
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ClientErrors
{
    /// <summary>
    /// Defines an exception class for the HTTP error Unauthorized.
    /// </summary>
    public class UnauthorizedException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public UnauthorizedException() : base(HttpStatusCode.Unauthorized)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorId">Specifies a custom defined error identifier.</param>
        public UnauthorizedException(Enum errorId) : base(HttpStatusCode.BadRequest, null, errorId.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorId">Specifies a custom defined error identifier.</param>
        /// <param name="message">Specifies an error message.</param>
        public UnauthorizedException(Enum errorId, string message) : base(HttpStatusCode.BadRequest, message, errorId.ToString())
        {}

        #endregion
    }
}
