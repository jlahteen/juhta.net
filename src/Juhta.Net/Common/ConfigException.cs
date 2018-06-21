
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Common
{
    /// <summary>
    /// This exception will be thrown in a situation where execution cannot be continued for a reason that somehow
    /// depends on the currently effective configuration. This exception should not be thrown unless the error is
    /// avoidable by changing the configuration.
    /// </summary>
    public class ConfigException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ConfigException(string message) : base(message)
        {}

        #endregion
    }
}
