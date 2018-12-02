
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines a class for serializing exceptions derived from <see cref="ClientErrorException"/>.
    /// </summary>
    public class ClientError : WebApiError
    {
        #region Public Methods

        /// <summary>
        /// Throws this <see cref="ClientError"/> as a corresponding exception derived from <see cref="ClientErrorException"/>.
        /// </summary>
        public void Throw()
        {
            switch (this.StatusCode)
            {
                case (int)HttpStatusCode.BadRequest:
                    throw new BadRequestException(this);

                case (int)HttpStatusCode.Conflict:
                    throw new ConflictException(this);

                case (int)HttpStatusCode.ExpectationFailed:
                    throw new ExpectationFailedException(this);

                case (int)HttpStatusCode.Forbidden:
                    throw new ForbiddenException(this);

                case (int)HttpStatusCode.Gone:
                    throw new GoneException(this);

                case (int)HttpStatusCode.LengthRequired:
                    throw new LengthRequiredException(this);

                case (int)HttpStatusCode.MethodNotAllowed:
                    throw new MethodNotAllowedException(this);

                case (int)HttpStatusCode.NotAcceptable:
                    throw new NotAcceptableException(this);

                case (int)HttpStatusCode.NotFound:
                    throw new NotFoundException(this);

                case (int)HttpStatusCode.PaymentRequired:
                    throw new PaymentRequiredException(this);

                case (int)HttpStatusCode.PreconditionFailed:
                    throw new PreconditionFailedException(this);

                case (int)HttpStatusCode.ProxyAuthenticationRequired:
                    throw new ProxyAuthenticationRequiredException(this);

                case (int)HttpStatusCode.RequestedRangeNotSatisfiable:
                    throw new RequestedRangeNotSatisfiableException(this);

                case (int)HttpStatusCode.RequestEntityTooLarge:
                    throw new RequestEntityTooLargeException(this);

                case (int)HttpStatusCode.RequestTimeout:
                    throw new RequestTimeoutException(this);

                case (int)HttpStatusCode.RequestUriTooLong:
                    throw new RequestUriTooLongException(this);

                case (int)HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException(this);

                case (int)HttpStatusCode.UnsupportedMediaType:
                    throw new UnsupportedMediaTypeException(this);

                case (int)HttpStatusCode.UpgradeRequired:
                    throw new UpgradeRequiredException(this);

                default:
                    throw new BlockNotImplementedException(this.StatusCode);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the custom-defined code of the client error.
        /// </summary>
        public string ErrorCode {get; set;}

        #endregion
    }
}
