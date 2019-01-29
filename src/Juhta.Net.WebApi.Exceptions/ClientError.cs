
//
// Juhta.NET, Copyright (c) 2017-2019 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.WebApi.Exceptions
{
    /// <summary>
    /// Defines a class for storing information about client errors.
    /// </summary>
    public class ClientError
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the custom-defined code of the client error.
        /// </summary>
        public string Code {get; set;}

        /// <summary>
        /// Gets or sets the field in the incoming request to which the client error relates.
        /// </summary>
        public string Field {get; set;}

        /// <summary>
        /// Gets or sets a URL that provides extra information about the client error.
        /// </summary>
        public string HelpUrl {get; set;}

        /// <summary>
        /// Gets or sets the message of the client error.
        /// </summary>
        public string Message {get; set;}

        #endregion
    }
}
