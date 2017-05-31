
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for startable processes library. A library is a startable processes library if it contains
    /// at least one process that has to be started prior to the library's services can be used.
    /// </summary>
    interface IStartableProcessesLibrary
    {
        #region Methods

        /// <summary>
        /// Starts the library processes.
        /// </summary>
        void StartProcesses();

        /// <summary>
        /// Stops the library processes.
        /// </summary>
        void StopProcesses();

        #endregion
    }
}
