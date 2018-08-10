
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Framework
{
    /// <summary>
    /// Defines a class that provides common methods related to the framework libraries.
    /// </summary>
    public static class FrameworkLibrary
    {
        #region Public Methods

        /// <summary>
        /// Gets the message identifier base for the common messages.
        /// </summary>
        /// <returns>Returns the message identifier base for the common messages.</returns>
        public static int GetCommonMessageIdBase()
        {
            return(c_commonMessageIdBase);
        }

        /// <summary>
        /// Gets the message identifier base for a specified framework library.
        /// </summary>
        /// <param name="libraryType">Specifies a framework library type.</param>
        /// <returns>Returns the message identifier base for the specified framework library.</returns>
        public static int GetMessageIdBase(FrameworkLibraryType libraryType)
        {
            return(c_commonMessageIdBase + (((int)libraryType + 1) * 1000));
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Defines the message identifier base for the common messages.
        /// </summary>
        private const int c_commonMessageIdBase = 100000;

        #endregion
    }
}
