
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
    /// Defines an exception class for the HTTP error Not Acceptable.
    /// </summary>
    public class NotAcceptableException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public NotAcceptableException() : base(HttpStatusCode.NotAcceptable)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        public NotAcceptableException(Enum errorCode) : base(HttpStatusCode.NotAcceptable, null, errorCode.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public NotAcceptableException(string message) : base(HttpStatusCode.NotAcceptable, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public NotAcceptableException(Enum errorCode, string message) : base(HttpStatusCode.NotAcceptable, message, errorCode.ToString())
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrorResponse">Specifies a client error response based on which to create the instance.</param>
        internal NotAcceptableException(ClientErrorResponse clientErrorResponse) : base(clientErrorResponse)
        {}

        #endregion
    }
}
