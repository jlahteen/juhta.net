
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
    /// Defines an exception class for the HTTP error Internal Server Error.
    /// </summary>
    public class InternalServerErrorException : ServerErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public InternalServerErrorException() : base(HttpStatusCode.InternalServerError)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public InternalServerErrorException(string message) : base(HttpStatusCode.InternalServerError, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception.</param>
        public InternalServerErrorException(string message, Exception innerException) : base(HttpStatusCode.BadGateway, message, innerException)
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serverErrorResponse">Specifies a server error response based on which to create the instance.</param>
        internal InternalServerErrorException(ServerErrorResponse serverErrorResponse) : base(serverErrorResponse)
        {}

        #endregion
    }
}
