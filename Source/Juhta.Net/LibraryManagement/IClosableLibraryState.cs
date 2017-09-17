
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for closable library states.
    /// </summary>
    public interface IClosableLibraryState
    {
        /// <summary>
        /// Closes the library state.
        /// </summary>
        /// <remarks>Closing a library state means performing all necessary closing and cleanup actions on the objects
        /// comprising the library state.</remarks>
        void Close();
    }
}
