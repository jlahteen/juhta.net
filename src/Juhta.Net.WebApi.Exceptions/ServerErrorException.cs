
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
    /// Defines an abstract base class for the server error Web API exceptions.
    /// </summary>
    public abstract class ServerErrorException : WebApiException
    {
        #region Public Methods

        /// <summary>
        /// Converts this <see cref="ServerErrorException"/> object to a <see cref="ServerError"/> object.
        /// </summary>
        /// <returns>Returns the resulting <see cref="ServerError"/> object.</returns>
        public ServerError ToServerError()
        {
            ServerError serverError = new ServerError();

            serverError.CallStack = this.CallStack;

            serverError.ErrorMessage = this.ErrorMessage;

            serverError.InnerException = this.InnerException;

            serverError.StatusCode = "ServerError." + this.StatusCode.ToString();

            return(serverError);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the inner exception that relates to the server error.
        /// </summary>
        public new string InnerException
        {
            get {return(m_innerException);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        protected ServerErrorException(HttpStatusCode httpStatusCode) : this(httpStatusCode, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serverError">Specifies a server error.</param>
        protected ServerErrorException(ServerError serverError) : base(serverError)
        {
            m_innerException = serverError.InnerException;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        protected ServerErrorException(HttpStatusCode httpStatusCode, string message) : this(httpStatusCode, message, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception.</param>
        protected ServerErrorException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(httpStatusCode, message)
        {
            if (innerException != null)
                m_innerException = innerException.ToString();
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="InnerException"/> property.
        /// </summary>
        private string m_innerException;

        #endregion
    }
}
