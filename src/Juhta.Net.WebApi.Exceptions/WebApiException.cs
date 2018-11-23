
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Helpers;
using System;
using System.Collections.Generic;
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
        /// Gets the call stack related to this <see cref="WebApiException"/> instance.
        /// </summary>
        public string[] CallStack
        {
            get {return(m_callStack);}
        }

        /// <summary>
        /// Gets the error message related to this <see cref="WebApiException"/> instance.
        /// </summary>
        public string ErrorMessage
        {
            get {return(m_errorMessage);}
        }

        /// <summary>
        /// Gets the error type related to this <see cref="WebApiException"/> instance.
        /// </summary>
        /// <remarks>This property returns in practice the textual version of <see cref="StatusCode"/>.</remarks>
        public string ErrorType
        {
            get {return(ToErrorType(m_statusCode));}
        }

        /// <summary>
        /// Gets the HTTP status code related to this <see cref="WebApiException"/> instance.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get {return(m_statusCode);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="webApiError">Specifies a Web API error.</param>
        protected WebApiException(WebApiError webApiError) : base(webApiError.ErrorMessage)
        {
            List<string> callStack = new List<string>();

            if (webApiError.CallStack != null)
                callStack.AddRange(webApiError.CallStack);

            callStack.Add($"-- {this.GetType().FullName} deserialized and rethrown --");

            // We must ignore 5 call stack lines to get to the line where the actual throw operation happened
            // - StackTraceHelper.GetCallStack()
            // - This constructor
            // - ClientErrorException or ServerErrorException constructor
            // - Constructor of a class derived from ClientErrorException or ServerErrorException
            // - ClientError.Throw() or ServerError.Throw()
            callStack.AddRange(StackTraceHelper.GetCallStack(1 + 1 + 1 + 1 + 1));

            m_callStack = callStack.ToArray();

            m_errorMessage = webApiError.ErrorMessage;

            m_statusCode = (HttpStatusCode)webApiError.StatusCode;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        protected WebApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            List<string> callStack = new List<string>();

            m_statusCode = statusCode;

            m_errorMessage = message;

            callStack.Add($"-- {this.GetType().FullName} thrown --");

            // We must ignore 4 call stack lines to get to the line where the actual throw operation happened
            // - StackTraceHelper.GetCallStack()
            // - This constructor
            // - ClientErrorException or ServerErrorException constructor
            // - Constructor of a class derived from ClientErrorException or ServerErrorException
            callStack.AddRange(StackTraceHelper.GetCallStack(1 + 1 + 1 + 1));

            m_callStack = callStack.ToArray();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts an HTTP status code to its textual version.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <returns>Returns the textual version of <paramref name="statusCode"/>.</returns>
        private static string ToErrorType(HttpStatusCode statusCode)
        {
            if (statusCode >= HttpStatusCode.InternalServerError)
                return("ServerError." + statusCode.ToString());

            else if (statusCode >= HttpStatusCode.BadRequest)
                return("ClientError." + statusCode.ToString());

            else
                return("HttpError." + statusCode.ToString());
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="CallStack"/> property.
        /// </summary>
        private string[] m_callStack;

        /// <summary>
        /// Stores the <see cref="ErrorMessage"/> property.
        /// </summary>
        private string m_errorMessage;

        /// <summary>
        /// Stores the <see cref="StatusCode"/> property.
        /// </summary>
        private HttpStatusCode m_statusCode;

        #endregion
    }
}
