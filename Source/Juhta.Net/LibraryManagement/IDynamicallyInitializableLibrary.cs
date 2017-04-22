
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for dynamically initializable libraries. A library is dynamically initializable, if it is
    /// configurable and its configuration can be initialized at the runtime.
    /// </summary>
    public interface IDynamicallyInitializableLibrary : IConfigurableLibrary, IDynamicLibrary
    {
        #region Methods

        /// <summary>
        /// Creates the default configuration state for the library.
        /// </summary>
        /// <returns>Returns an IConfigState object containing the default configuration state for the library.
        /// </returns>
        IConfigState CreateDefaultConfigState();

        #endregion
    }
}
