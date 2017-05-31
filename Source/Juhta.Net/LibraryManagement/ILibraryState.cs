
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for classes that represent the state of a library. Typically, a library state is an
    /// aggregate object containing a collection of reference-type or value-type objects created based on the
    /// configuration of the library.
    /// </summary>
    public interface ILibraryState
    {
        #region Methods

        /// <summary>
        /// Closes the library state.
        /// </summary>
        /// <remarks>Closing a libary state means performing all necessary closing and cleanup actions on the objects
        /// comprising the library state.</remarks>
        void Close();

        /// <summary>
        /// Initializes the library state.
        /// </summary>
        /// <remarks>Initializing a library state means performing all necessary startup actions on the objects
        /// comprising the library state so that the objects are ready to be set as the effective library state.</remarks>
        void Initialize();

        #endregion

        #region Properties

        /// <summary>
        /// Returns true if the library state is initialized, that is, the <see cref="Initialize"/> method has been
        /// called on this <see cref="ILibraryState"/> instance, otherwise false.
        /// </summary>
        bool IsInitialized {get;}

        #endregion
    }
}
