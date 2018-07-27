
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for library handle classes. This interface is the core interface for the library
    /// management.
    /// </summary>
    public interface ILibraryHandle
    {
        #region Properties

        /// <summary>
        /// Gets the file name of the library.
        /// </summary>
        string LibraryFileName {get;}

        #endregion
    }
}
