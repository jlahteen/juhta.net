
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
    /// Defines an exception class for the HTTP error Requested Range Not Satisfiable.
    /// </summary>
    public class RangeNotSatisfiableException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public RangeNotSatisfiableException() : base(HttpStatusCode.RequestedRangeNotSatisfiable)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public RangeNotSatisfiableException(string message) : base(HttpStatusCode.RequestedRangeNotSatisfiable, message)
        {}

        #endregion
    }
}
