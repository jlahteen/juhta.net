
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
    /// Defines an exception class for the HTTP error Not Implemented.
    /// </summary>
    public class NotImplementedException : ServerErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public NotImplementedException() : base(HttpStatusCode.NotImplemented)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public NotImplementedException(string message) : base(HttpStatusCode.NotImplemented, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception.</param>
        public NotImplementedException(string message, Exception innerException) : base(HttpStatusCode.BadGateway, message, innerException)
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serverError">Specifies a server error based on which to create the instance.</param>
        internal NotImplementedException(ServerError serverError) : base(serverError)
        {}

        #endregion
    }
}
