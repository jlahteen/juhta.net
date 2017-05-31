
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for dynamic startable processes library. A library is a dynamic startable processes
    /// library if it contains at least one process that has to be started prior to the library's services can be used
    /// and those processes can be started and stopped at the runtime.
    /// </summary>
    interface IDynamicStartableProcessesLibrary : IStartableProcessesLibrary, IDynamicLibrary
    {
        #region Methods

        /// <summary>
        /// Starts the processes in the specified library state.
        /// </summary>
        /// <param name="libraryState">Specifies an <see cref="ILibraryState"/> object.</param>
        void StartProcesses(ILibraryState libraryState);

        /// <summary>
        /// Stops the processes in the specified library state.
        /// </summary>
        /// <param name="libraryState">Specifies an <see cref="ILibraryState"/> object.</param>
        void StopProcesses(ILibraryState libraryState);

        #endregion
    }
}
