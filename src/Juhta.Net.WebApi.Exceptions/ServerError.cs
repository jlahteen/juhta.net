
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.WebApi.Exceptions.ServerErrors;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines a class for serializing instances of <see cref="ServerErrorException"/>.
    /// </summary>
    public abstract class ServerError : WebApiError
    {
        #region Public Methods

        /// <summary>
        /// Throws this <see cref="ServerError"/> as a corresponding exception derived from <see cref="ServerErrorException"/>.
        /// </summary>
        public void Throw()
        {
            HttpStatusCode statusCode;

            statusCode = (HttpStatusCode)System.Enum.Parse(typeof(HttpStatusCode), this.StatusCode.Substring(this.StatusCode.IndexOf('.') + 1));

            switch (statusCode)
            {
                case HttpStatusCode.BadGateway:
                    throw new BadGatewayException(this);

                case HttpStatusCode.GatewayTimeout:
                    throw new GatewayTimeoutException(this);

                case HttpStatusCode.HttpVersionNotSupported:
                    throw new HttpVersionNotSupportedException(this);

                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException(this);

                case HttpStatusCode.NotImplemented:
                    throw new NotImplementedException(this);

                case HttpStatusCode.ServiceUnavailable:
                    throw new ServiceUnavailableException(this);

                default:
                    throw new BlockNotImplementedException(this.StatusCode);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the inner exception that relates to the server error.
        /// </summary>
        /// <remarks>Please note that an inner exception will be set as a string.</remarks>
        public string InnerException {get; set;}

        #endregion
    }
}
