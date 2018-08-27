
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines an abstract base class for the Web API exceptions.
    /// </summary>
    public abstract class WebApiException : Exception
    {
        #region Public Properties

        /// <summary>
        /// Gets the HTTP status code related to this <see cref="WebApiException"/> instance.
        /// </summary>
        public HttpStatusCode HttpStatusCode
        {
            get {return(m_httpStatusCode);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        protected WebApiException(HttpStatusCode httpStatusCode) : this(httpStatusCode, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        protected WebApiException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            m_httpStatusCode = httpStatusCode;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="HttpStatusCode"/> property.
        /// </summary>
        private HttpStatusCode m_httpStatusCode;

        #endregion
    }
}
