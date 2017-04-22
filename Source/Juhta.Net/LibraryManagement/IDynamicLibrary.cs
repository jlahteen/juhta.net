
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
    /// changed at the runtime by modifying the configuration file of the library.
    /// </summary>
    public interface IDynamicLibrary
    {
        #region Methods

        /// <summary>
        /// Gets a lock object that manages concurrent access to the configuration state of the library.
        /// </summary>
        /// <returns>Returns a ReaderWriterLockSlim object managing concurrent access to the configuration state of the
        /// library.</returns>
        ReaderWriterLockSlim GetConfigStateLock();

        /// <summary>
        /// Gets the current configuration state of the library.
        /// </summary>
        /// <returns>Returns an IConfigState object containing the current configuration state of the library.</returns>
        IConfigState GetCurrentConfigState();

        /// <summary>
        /// Sets a specified configuration state as the current configuration state of the library.
        /// </summary>
        /// <param name="configState">Specifies an IConfigState object.</param>
        /// <remarks>
        /// <para>This method is expected not to throw exceptions. The method should be implemented by using just
        /// assignments from the properties of the specified IConfigState object to the fields of the corresponding
        /// library classes. However, if the implementation of this method contains error-prone functionality, in case
        /// of an error the method is responsible for leaving the library in a consistent state in order to allow the
        /// process to continue running.</para>
        /// <para>The method can assume that the specified IConfigState object is initialized.</para>
        /// </remarks>
        void SetConfigState(IConfigState configState);

        #endregion
    }
}
