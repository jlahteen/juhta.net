
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

                default:
                    throw new BlockNotImplementedException(this.StatusCode);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the custom-defined identifier of the client error.
        /// </summary>
        public string ErrorId {get; set;}

        #endregion
    }
}
