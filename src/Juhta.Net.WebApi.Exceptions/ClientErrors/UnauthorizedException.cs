
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.ClientErrors
{
    /// <summary>
    /// Defines an exception class for the HTTP error Unauthorized.
    /// </summary>
    public class UnauthorizedException : ClientErrorException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public UnauthorizedException() : base(HttpStatusCode.Unauthorized, null, null, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientError">Specifies a client error.</param>
        public UnauthorizedException(ClientError clientError) : base(HttpStatusCode.Unauthorized, clientError)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        public UnauthorizedException(Enum errorCode) : base(HttpStatusCode.Unauthorized, null, errorCode.ToString(), null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrors">Specifies a collection of client errors.</param>
        public UnauthorizedException(IEnumerable<ClientError> clientErrors) : base(HttpStatusCode.Unauthorized, clientErrors)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message, null, null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        public UnauthorizedException(Enum errorCode, Enum field) : base(HttpStatusCode.Unauthorized, null, errorCode.ToString(), field.ToString(), null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="message">Specifies an error message.</param>
        public UnauthorizedException(Enum errorCode, string message) : base(HttpStatusCode.Unauthorized, message, errorCode.ToString(), null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="helpUrl">Specifies a URL that provides extra information about the error.</param>
        public UnauthorizedException(string message, string helpUrl) : base(HttpStatusCode.Unauthorized, message, null, null, helpUrl)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        /// <param name="message">Specifies an error message.</param>
        public UnauthorizedException(Enum errorCode, Enum field, string message) : base(HttpStatusCode.Unauthorized, message, errorCode.ToString(), field.ToString(), null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        /// <param name="message">Specifies an error message.</param>
        public UnauthorizedException(Enum errorCode, string field, string message) : base(HttpStatusCode.Unauthorized, message, errorCode.ToString(), field, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="helpUrl">Specifies a URL that provides extra information about the error.</param>
        public UnauthorizedException(Enum errorCode, Enum field, string message, string helpUrl) : base(HttpStatusCode.Unauthorized, message, errorCode.ToString(), field.ToString(), helpUrl)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorCode">Specifies a custom-defined error code.</param>
        /// <param name="field">Specifies a field to which the error relates.</param>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="helpUrl">Specifies a URL that provides extra information about the error.</param>
        public UnauthorizedException(Enum errorCode, string field, string message, string helpUrl) : base(HttpStatusCode.Unauthorized, message, errorCode.ToString(), field, helpUrl)
        {}

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="clientErrorResponse">Specifies a client error response based on which to create the instance.</param>
        internal UnauthorizedException(ClientErrorResponse clientErrorResponse) : base(clientErrorResponse)
        {}

        #endregion
    }
}
