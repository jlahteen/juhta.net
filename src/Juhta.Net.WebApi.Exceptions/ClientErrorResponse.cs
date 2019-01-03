
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines a class for serializing exceptions derived from <see cref="ClientErrorException"/>.
    /// </summary>
    public class ClientErrorResponse : WebApiErrorResponse
    {
        #region Public Methods

        /// <summary>
        /// Throws this <see cref="ClientErrorResponse"/> as a corresponding exception derived from
        /// <see cref="ClientErrorException"/>.
        /// </summary>
        public override void Throw()
        {
            HttpStatusCode statusCode;

            statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), this.StatusCode.Substring(this.StatusCode.IndexOf('.') + 1));

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(this);

                case HttpStatusCode.Conflict:
                    throw new ConflictException(this);

                case HttpStatusCode.ExpectationFailed:
                    throw new ExpectationFailedException(this);

                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException(this);

                case HttpStatusCode.Gone:
                    throw new GoneException(this);

                case HttpStatusCode.LengthRequired:
                    throw new LengthRequiredException(this);

                case HttpStatusCode.MethodNotAllowed:
                    throw new MethodNotAllowedException(this);

                case HttpStatusCode.NotAcceptable:
                    throw new NotAcceptableException(this);

                case HttpStatusCode.NotFound:
                    throw new NotFoundException(this);

                case HttpStatusCode.PaymentRequired:
                    throw new PaymentRequiredException(this);

                case HttpStatusCode.PreconditionFailed:
                    throw new PreconditionFailedException(this);

                case HttpStatusCode.ProxyAuthenticationRequired:
                    throw new ProxyAuthenticationRequiredException(this);

                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    throw new RequestedRangeNotSatisfiableException(this);

                case HttpStatusCode.RequestEntityTooLarge:
                    throw new RequestEntityTooLargeException(this);

                case HttpStatusCode.RequestTimeout:
                    throw new RequestTimeoutException(this);

                case HttpStatusCode.RequestUriTooLong:
                    throw new RequestUriTooLongException(this);

                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException(this);

                case HttpStatusCode.UnsupportedMediaType:
                    throw new UnsupportedMediaTypeException(this);

                case HttpStatusCode.UpgradeRequired:
                    throw new UpgradeRequiredException(this);

                default:
                    throw new BlockNotImplementedException(this.StatusCode);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets an array of <see cref="ClientError"/> objects.
        /// </summary>
        public ClientError[] Errors {get; set;}

        #endregion
    }
}
