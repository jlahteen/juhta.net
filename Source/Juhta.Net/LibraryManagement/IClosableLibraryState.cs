
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for closable library states. A library state is closable if it requires specific closing
    /// actions.
    /// </summary>
    public interface IClosableLibraryState
    {
        /// <summary>
        /// Closes the library state, that is, performs required closing actions to release the library state resources
        /// gracefully.
        /// </summary>
        /// <returns>Returns true if the library state was closed without errors, or false if at least one error
        /// occurred in the closing process.</returns>
        /// <remarks>
        /// <para>This method should not throw exceptions. It is recommended that, in case of an error, the error is
        /// logged and the closing process is continued for the rest of the closing actions. In other words, the method
        /// should close the library state as completely as possible.</para>
        /// <para>This method will be called even if the initialization process of the library state has failed. This
        /// means that the method should prepare for such situation where the library state is not initialized at all
        /// or initialized only partially.</para>
        /// </remarks>
        bool Close();
    }
}
