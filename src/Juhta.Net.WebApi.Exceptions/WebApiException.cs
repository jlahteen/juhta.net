
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
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        protected WebApiException(HttpStatusCode statusCode) : this(statusCode, null)
        {}

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

            callStack.AddRange(StackTraceHelper.GetCallStack(1 + 1 + 1 + 1));

            m_callStack = callStack.ToArray();
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
