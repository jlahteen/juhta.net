
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Validation
{
    /// <summary>
    /// This exception will be thrown by instances of the <see cref="IValidator&lt;T&gt;"/> interface when they
    /// encounter invalid data.
    /// </summary>
    public class ValidationException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public ValidationException(string message) : base(message)
        {}

        #endregion
    }
}
