
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines an abstract base class for the Web API exceptions.
    /// </summary>
    public abstract class WebApiException : Exception
    {
        #region Static Constructor

        /// <summary>
        /// Initializes the class.
        /// </summary>
        static WebApiException()
        {
            s_serviceName = Assembly.GetEntryAssembly().GetFileNameWithoutExtension();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the name of the service that the process represents.
        /// </summary>
        /// <param name="serviceName">Specifies a service name.</param>
        /// <remarks>The service name appears in the values of the <see cref="ServiceStack"/> property of the instances
        /// of this class. The default value for the service name is the file name (without extension) of the entry
        /// assembly.</remarks>
        public static void SetServiceName(string serviceName)
        {
            s_serviceName = serviceName;
        }

        /// <summary>
        /// Converts this <see cref="WebApiException"/> instance to a string.
        /// </summary>
        /// <returns>Returns this <see cref="WebApiException"/>instance as a string.</returns>
        public override string ToString()
        {
            StringBuilder value = new StringBuilder(base.ToString());

            value.AppendLine();

            value.AppendLine($"   --- {nameof(WebApiException)} properties ---");

            value.AppendFormat("     \"{0}\": \"{1}\"", nameof(this.StatusCode), m_statusCode.ToString());

            value.AppendLine();

            value.AppendFormat("     \"{0}\": [", nameof(this.ServiceStack));

            for (int i = 0; i < m_serviceStack.Length; i++)
                value.AppendFormat("\"{0}\"{1}", m_serviceStack[i], i < m_serviceStack.Length - 1 ? ", " : "]");

            return(value.ToString());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the service stack related to this <see cref="WebApiException"/> instance.
        /// </summary>
        public string[] ServiceStack
        {
            get {return(m_serviceStack);}
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
        /// <param name="webApiErrorResponse">Specifies a Web API error response.</param>
        /// <param name="message">Specifies an error message.</param>
        protected WebApiException(WebApiErrorResponse webApiErrorResponse, string message) : base(message)
        {
            List<string> serviceStack = new List<string>();

            if (webApiErrorResponse.ServiceStack != null)
                serviceStack.AddRange(webApiErrorResponse.ServiceStack);

            serviceStack.Add(s_serviceName);

            m_serviceStack = serviceStack.ToArray();

            m_statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), webApiErrorResponse.StatusCode.Substring(webApiErrorResponse.StatusCode.IndexOf('.') + 1));
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="statusCode">Specifies an HTTP status code.</param>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="innerException">Specifies an inner exception.</param>
        protected WebApiException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            m_statusCode = statusCode;

            m_serviceStack = new string[]{s_serviceName};
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the name of the service that the process represents.
        /// </summary>
        private static string s_serviceName;

        /// <summary>
        /// Stores the <see cref="ServiceStack"/> property.
        /// </summary>
        private string[] m_serviceStack;

        /// <summary>
        /// Stores the <see cref="StatusCode"/> property.
        /// </summary>
        private HttpStatusCode m_statusCode;

        #endregion
    }
}
