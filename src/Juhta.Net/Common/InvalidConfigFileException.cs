
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Xml.Schema;

namespace Juhta.Net.Common
{
    /// <summary>
    /// This exception will be thrown when an XML configuration file doesn't pass schema validation.
    /// </summary>
    public class InvalidConfigFileException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        /// <param name="exception">Specifies an actual error that has occurred in the schema validation.</param>
        public InvalidConfigFileException(string message, XmlSchemaValidationException exception) : base(message, exception)
        {}

        #endregion
    }
}
