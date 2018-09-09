
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ServerErrors
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

        #endregion
    }
}
