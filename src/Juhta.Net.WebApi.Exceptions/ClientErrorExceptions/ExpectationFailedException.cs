
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
    /// Defines an exception class for the HTTP error Expectation Failed.
    /// </summary>
    public class ExpectationFailedException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ExpectationFailedException() : base(HttpStatusCode.ExpectationFailed)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        public ExpectationFailedException(Enum errorCode) : base(HttpStatusCode.ExpectationFailed, null, errorCode.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ExpectationFailedException(string message) : base(HttpStatusCode.ExpectationFailed, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public ExpectationFailedException(Enum errorCode, string message) : base(HttpStatusCode.ExpectationFailed, message, errorCode.ToString())
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrorResponse">Specifies a client error response based on which to create the instance.</param>
        internal ExpectationFailedException(ClientErrorResponse clientErrorResponse) : base(clientErrorResponse)
        {}

        #endregion
    }
}
