
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines an abstract base class for the client error Web API exceptions.
    /// </summary>
    public abstract class ClientErrorException : WebApiException
    {
        #region Public Methods

        /// <summary>
        /// Converts this <see cref="ClientErrorException"/> object to a <see cref="ClientErrorResponse"/> object.
        /// </summary>
        /// <returns>Returns the resulting <see cref="ClientErrorResponse"/> object.</returns>
        public ClientErrorResponse ToClientErrorResponse()
        {
            ClientErrorResponse clientErrorResponse = new ClientErrorResponse();

            clientErrorResponse.CallStack = this.CallStack;

            clientErrorResponse.Errors = m_errors;

            clientErrorResponse.StatusCode = "ClientError." + this.StatusCode.ToString();

            return(clientErrorResponse);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the first <see cref="ClientError"/> object that relates to this <see cref="ClientErrorException"/>
        /// instance. Returns null if there are no <see cref="ClientError"/> objects.
        /// </summary>
        public ClientError Error
        {
            get {return(m_errors?[0]);}
        }

        /// <summary>
        /// Gets an array of the <see cref="ClientError"/> objects that relate to this <see cref="ClientErrorException"/>
        /// instance. Returns null if there are no <see cref="ClientError"/> objects.
        /// </summary>
        public ClientError[] Errors
        {
            get {return(m_errors);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrorResponse">Specifies a client error response.</param>
        protected ClientErrorException(ClientErrorResponse clientErrorResponse) : base(clientErrorResponse, GetErrorMessage(clientErrorResponse.Errors))
        {
            m_errors = clientErrorResponse.Errors;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        protected ClientErrorException(HttpStatusCode statusCode) : this(statusCode, null, null, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="clientError">Specifies a client error.</param>
        protected ClientErrorException(HttpStatusCode statusCode, ClientError clientError) : base(statusCode, clientError?.Message, null)
        {
            m_errors = new ClientError[1]{clientError};
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="errorMessage">Specifies an error message.</param>
        protected ClientErrorException(HttpStatusCode statusCode, string errorMessage) : this(statusCode, errorMessage, null, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="clientErrors">Specifies a collection of client errors.</param>
        protected ClientErrorException(HttpStatusCode statusCode, IEnumerable<ClientError> clientErrors) : base(statusCode, GetErrorMessage(clientErrors), null)
        {
            m_errors = (ClientError[])clientErrors;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="errorMessage">Specifies an error message.</param>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        protected ClientErrorException(HttpStatusCode statusCode, string errorMessage, string errorCode) : this(statusCode, errorMessage, errorCode, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="errorMessage">Specifies an error message.</param>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field in the incoming request to which the error relates.</param>
        protected ClientErrorException(HttpStatusCode statusCode, string errorMessage, string errorCode, string field) : this(statusCode, errorMessage, errorCode, field, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="errorMessage">Specifies an error message.</param>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field in the incoming request to which the error relates.</param>
        /// <param name="helpUrl">Specifies a URL that provides extra information about the error.</param>
        protected ClientErrorException(HttpStatusCode statusCode, string errorMessage, string errorCode, string field, string helpUrl) : base(statusCode, errorMessage, null)
        {
            ClientError clientError;

            if (String.IsNullOrEmpty(errorMessage + errorCode + field + helpUrl))
                return;

            clientError = new ClientError();

            clientError.Message = errorMessage;

            clientError.Code = errorCode;

            clientError.Field = field;

            clientError.HelpUrl = helpUrl;

            m_errors = new ClientError[1]{clientError};
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an error message from a specified client error collection.
        /// </summary>
        /// <param name="clientErrors">Specifies a client error collection.</param>
        /// <returns>Returns the error message of the first client error in <paramref name="clientErrors"/>. If the
        /// collection is empty, the return value is null.</returns>
        private static string GetErrorMessage(IEnumerable<ClientError> clientErrors)
        {
            ClientError[] clientErrors2 = (ClientError[])clientErrors;

            if (clientErrors2 == null || clientErrors2.Length == 0)
                return(null);
            else
                return(clientErrors2[0].Message);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Errors"/> property.
        /// </summary>
        private ClientError[] m_errors;

        #endregion
    }
}
