
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
    /// Defines an abstract base class for the client error Web API exceptions.
    /// </summary>
    public abstract class ClientErrorException : WebApiException
    {
        #region Public Properties

        /// <summary>
        /// Gets the custom defined identifier of the client error.
        /// </summary>
        public string ErrorId
        {
            get {return(m_errorId);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        protected ClientErrorException(HttpStatusCode httpStatusCode) : base(httpStatusCode)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        protected ClientErrorException(HttpStatusCode httpStatusCode, string message) : base(httpStatusCode, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="httpStatusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="errorId">Specifies a custom defined identifier for the client error.</param>
        protected ClientErrorException(HttpStatusCode httpStatusCode, string message, string errorId) : base(httpStatusCode, message)
        {
            m_errorId = errorId;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ErrorId"/> property.
        /// </summary>
        private string m_errorId;

        #endregion
    }
}
