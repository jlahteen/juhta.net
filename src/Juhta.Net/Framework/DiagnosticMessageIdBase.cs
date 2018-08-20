
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Framework
{
    /// <summary>
    /// A static class that provides properties to get diagnostic message identifier bases for the common messages and
    /// framework libraries.
    /// </summary>
    public static class DiagnosticMessageIdBase
    {
        #region Public Properties

        /// <summary>
        /// Gets the message identifier base for the common messages.
        /// </summary>
        public static int Common
        {
            get {return(c_commonMessageIdBase);}
        }

        /// <summary>
        /// Gets the message identifier base for the Console library.
        /// </summary>
        public static int Console
        {
            get {return(GetMessageIdBase(FrameworkLibraryType.Console));}
        }

        /// <summary>
        /// Gets the message identifier base for the Core library.
        /// </summary>
        public static int Core
        {
            get {return(GetMessageIdBase(FrameworkLibraryType.Core));}
        }

        /// <summary>
        /// Gets the message identifier base for the LibraryManagement library.
        /// </summary>
        public static int LibraryManagement
        {
            get {return(GetMessageIdBase(FrameworkLibraryType.LibraryManagement));}
        }

        /// <summary>
        /// Gets the message identifier base for the Services library.
        /// </summary>
        public static int Services
        {
            get {return(GetMessageIdBase(FrameworkLibraryType.Services));}
        }

        /// <summary>
        /// Gets the message identifier base for the Startup library.
        /// </summary>
        public static int Startup
        {
            get {return(GetMessageIdBase(FrameworkLibraryType.Startup));}
        }

        /// <summary>
        /// Gets the message identifier base for the Validation library.
        /// </summary>
        public static int Validation
        {
            get {return(GetMessageIdBase(FrameworkLibraryType.Validation));}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the message identifier base for a specified framework library.
        /// </summary>
        /// <param name="libraryType">Specifies a framework library type.</param>
        /// <returns>Returns the message identifier base for the specified framework library.</returns>
        private static int GetMessageIdBase(FrameworkLibraryType libraryType)
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
