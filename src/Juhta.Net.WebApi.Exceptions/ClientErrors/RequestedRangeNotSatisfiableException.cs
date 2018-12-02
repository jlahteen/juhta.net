
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
    /// Defines an exception class for the HTTP error Requested Range Not Satisfiable.
    /// </summary>
    public class RequestedRangeNotSatisfiableException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public RequestedRangeNotSatisfiableException() : base(HttpStatusCode.RequestedRangeNotSatisfiable)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        public RequestedRangeNotSatisfiableException(Enum errorCode) : base(HttpStatusCode.RequestedRangeNotSatisfiable, null, errorCode.ToString())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public RequestedRangeNotSatisfiableException(string message) : base(HttpStatusCode.RequestedRangeNotSatisfiable, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public RequestedRangeNotSatisfiableException(Enum errorCode, string message) : base(HttpStatusCode.RequestedRangeNotSatisfiable, message, errorCode.ToString())
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientError">Specifies a client error based on which to create the instance.</param>
        internal RequestedRangeNotSatisfiableException(ClientError clientError) : base(clientError)
        {}

        #endregion
    }
}
