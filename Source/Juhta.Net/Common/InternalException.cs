
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
    /// This exception will be thrown when an internal error occurs.
    /// </summary>
    /// <remarks>An internal error is typically an error that should 'never happen', and it usually means some kind of
    /// a bug in the software.</remarks>
    public class InternalException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies an error message.</param>
        public InternalException(string message) : base(message + InternalErrorInfo)
        {}

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets general information about internal errors.
        /// </summary>
        private static string InternalErrorInfo
        {
            get
            {
                string info;

                info = "This is an internal error and should never happen.";
                info += " You can make this software better by sending this error information to the product manufacturer.";

                return(" (" + info + ")");
            }
        }

        #endregion
    }
}
