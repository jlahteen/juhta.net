
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for dynamic initializable libraries. A library is a dynamic initializable library if it is
    /// initializable, configurable and its configuration can be initialized at the runtime.
    /// </summary>
    public interface IDynamicInitializableLibrary : IInitializableLibrary, IConfigurableLibrary, IDynamicLibrary
    {
        #region Methods

        /// <summary>
        /// Creates the default state for the library.
        /// </summary>
        /// <returns>Returns an <see cref="ILibraryState"/> object containing the default state for the library.
        /// </returns>
        ILibraryState CreateDefaultLibraryState();

        #endregion
    }
}
