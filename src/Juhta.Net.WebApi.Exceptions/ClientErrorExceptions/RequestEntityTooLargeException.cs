
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
    /// Defines an exception class for the HTTP error Request Entity Too Large.
    /// </summary>
    public class RequestEntityTooLargeException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public RequestEntityTooLargeException() : base(HttpStatusCode.RequestEntityTooLarge)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        public RequestEntityTooLargeException(Enum errorCode) : base(HttpStatusCode.RequestEntityTooLarge, null, errorCode.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public RequestEntityTooLargeException(string message) : base(HttpStatusCode.RequestEntityTooLarge, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public RequestEntityTooLargeException(Enum errorCode, string message) : base(HttpStatusCode.RequestEntityTooLarge, message, errorCode.ToString())
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientError">Specifies a client error based on which to create the instance.</param>
        internal RequestEntityTooLargeException(ClientError clientError) : base(clientError)
        {}

        #endregion
    }
}
