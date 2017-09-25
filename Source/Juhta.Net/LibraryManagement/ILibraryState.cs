
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines a base interface for classes that represent the state of a library. Typically, a library state is an
    /// aggregate object containing a collection of reference-type or value-type objects created based on the
    /// configuration of the library.
    /// </summary>
    public interface ILibraryState
    {
        #region Properties

        /// <summary>
        /// Gets the handle of the library that owns this library state.
        /// </summary>
        ILibraryHandle LibraryHandle {get;}

        #endregion
    }
}
