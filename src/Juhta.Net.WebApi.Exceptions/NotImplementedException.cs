
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Net;

namespace Juhta.Net.WebApi.Exceptions
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

        #endregion
    }
}
