
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an abstract base class for application library handles.
    /// </summary>
    public abstract class AppLibraryHandle : LibraryHandle
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="libraryFileName">Specifies a value for the <see cref="LibraryHandle.LibraryFileName"/>
        /// property.</param>
        protected AppLibraryHandle(string libraryFileName) : base(libraryFileName)
        {}

        #endregion
    }
}
