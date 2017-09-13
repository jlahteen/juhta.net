
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for dynamic startable libraries. A library is a dynamic startable library if it is dynamic
    /// and contains at least one process that has to be started prior to the library's services can be used.
    /// </summary>
    interface IDynamicStartableLibrary : IDynamicLibrary
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
        /// <returns>Returns true if the processes in the library state were stopped without errors, or false if at
        /// least one error occurred in the stopping process.</returns>
        /// <remarks>
        /// <para>This method should not throw exceptions. It is recommended that, in case of an error, the error is
        /// logged and the stopping process is continued for the rest of the processes. In other words, the method
        /// should stop the processes as completely as possible.</para>
        /// <para>This method will be called even if the <see cref="StartProcesses"/> method has failed. This means
        /// that the method should prepare for such situation where the processes have not been started at all or
        /// started only partially.</para>
        /// </remarks>
        bool StopProcesses(ILibraryState libraryState);

        #endregion
    }
}
