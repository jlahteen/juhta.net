
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
        #region Properties

        /// <summary>
        /// Gets or sets the current state of the library.
        /// </summary>
        /// <remarks>
        /// <para>The setter can assume that the specified <see cref="ILibraryState"/> object is initialized.</para>
        /// <para>The setter is expected not to throw exceptions. The setter should be implemented by just using
        /// assignments from the properties of the specified <see cref="ILibraryState"/> object to the properties of
        /// the corresponding library classes.</para>
        /// <para>A recommended design pattern is that a library state object is an aggregate object for the objects
        /// comprising the library state.</para>
        /// </remarks>
        ILibraryState LibraryState {get; set;}

        /// <summary>
        /// Gets a <see cref="ReaderWriterLockSlim"/> object managing concurrent access to the current state of the
        /// library.
        /// </summary>
        ReaderWriterLockSlim LibraryStateLock {get;}

        #endregion
    }
}
