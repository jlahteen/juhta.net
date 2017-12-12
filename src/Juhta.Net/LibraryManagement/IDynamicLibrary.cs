
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Threading;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines a base interface for dynamic libraries. A dynamic library is a library whose state can be dynamically
    /// changed at the runtime e.g. by modifying the configuration of the library.
    /// </summary>
    public interface IDynamicLibrary
    {
        #region Methods

        /// <summary>
        /// Goes live with a specified <see cref="ILibraryState"/> instance.
        /// </summary>
        /// <param name="libraryState">Specifies an <see cref="ILibraryState"/> instance to go live with.</param>
        /// <remarks>
        /// <para>It can be assumed that the specified <see cref="ILibraryState"/> instance is fully initialized.</para>
        /// <para>This method makes it possible to encapsulate the final steps that are required to set an initialized
        /// library state as the effective library state.</para>
        /// </remarks>
        void GoLive(ILibraryState libraryState);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current state of the library.
        /// </summary>
        ILibraryState LibraryState {get; set;}

        /// <summary>
        /// Gets a <see cref="ReaderWriterLockSlim"/> object managing concurrent access to the current state of the
        /// library.
        /// </summary>
        ReaderWriterLockSlim LibraryStateLock {get;}

        #endregion
    }
}
