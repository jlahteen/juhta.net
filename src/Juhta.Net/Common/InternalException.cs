
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
    /// This exception can be thrown when an error occurs in an application but no technical details are wanted to be
    /// exposed to the caller.
    /// </summary>
    public class InternalException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public InternalException() : base(GetErrorMessage())
        {}

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the general internal error message.
        /// </summary>
        /// <returns>Returns the general internal error message.</returns>
        private static string GetErrorMessage()
        {
            string errorMessage = null;

            errorMessage += "An internal error occurred in the application. ";

            errorMessage += "If this error occurs repeatedly, please consider reporting this error with the appropriate context to the software vendor or author(s) to help them improve the quality of this software. ";

            errorMessage += "Thank you.";

            return(errorMessage);
        }

        #endregion
    }
}
