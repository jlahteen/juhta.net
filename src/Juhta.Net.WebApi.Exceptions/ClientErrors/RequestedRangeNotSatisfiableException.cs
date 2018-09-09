
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

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
        /// <param name="message">Specifies an error message.</param>
        public RequestedRangeNotSatisfiableException(string message) : base(HttpStatusCode.RequestedRangeNotSatisfiable, message)
        {}

        #endregion
    }
}
