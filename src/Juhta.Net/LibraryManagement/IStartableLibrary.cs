
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for startable libraries. A library is a startable library if it contains at least one
    /// process that has to be started prior to the library services can be used.
    /// </summary>
    public interface IStartableLibrary
    {
        #region Methods

        /// <summary>
        /// Starts the library processes.
        /// </summary>
        void StartProcesses();

        /// <summary>
        /// Stops the library processes.
        /// </summary>
        /// <returns>Returns true if the library processes were stopped without errors, or false if at least one error
        /// occurred in the stopping process.</returns>
        /// <remarks>
        /// <para>This method should not throw exceptions. It is recommended that, in case of an error, the error is
        /// logged and the stopping process is continued for the rest of the processes. In other words, the method
        /// should stop the library processes as completely as possible.</para>
        /// <para>This method will be called even if the initialization process of the library has failed. This means
        /// that the method should prepare for such situation where the library processes have not been started at all
        /// or started only partially.</para>
        /// </remarks>
        bool StopProcesses();

        #endregion
    }
}
