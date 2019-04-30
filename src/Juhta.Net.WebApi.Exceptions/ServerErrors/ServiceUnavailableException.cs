
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ServerErrorExceptions
{
    /// <summary>
    /// Defines an exception class for the HTTP error Service Unavailable.
    /// </summary>
    public class ServiceUnavailableException : ServerErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ServiceUnavailableException() : base(HttpStatusCode.ServiceUnavailable)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ServiceUnavailableException(string message) : base(HttpStatusCode.ServiceUnavailable, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception.</param>
        public ServiceUnavailableException(string message, Exception innerException) : base(HttpStatusCode.ServiceUnavailable, message, innerException)
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serverErrorResponse">Specifies a server error response based on which to create the instance.</param>
        internal ServiceUnavailableException(ServerErrorResponse serverErrorResponse) : base(serverErrorResponse)
        {}

        #endregion
    }
}
