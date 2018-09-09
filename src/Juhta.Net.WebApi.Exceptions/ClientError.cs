
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines a class for serializing instances of <see cref="ClientErrorException"/>.
    /// </summary>
    public abstract class ClientError : WebApiError
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the custom error identifier of the client error.
        /// </summary>
        public string ErrorId {get; set;}

        #endregion
    }
}
