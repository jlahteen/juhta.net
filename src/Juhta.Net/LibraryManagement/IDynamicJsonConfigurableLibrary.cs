
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for dynamic JSON configurable libraries. A library is a dynamic JSON configurable library
    /// if it is dynamic, configurable and the configuration is done with a JSON configuration.
    /// </summary>
    public interface IDynamicJsonConfigurableLibrary : IDynamicLibrary, IConfigurableLibrary
    {
        #region Methods

        /// <summary>
        /// Creates an uninitialized library state.
        /// </summary>
        /// <returns>Returns an uninitialized <see cref="IJsonConfigurableLibraryState"/> object.</returns>
        IJsonConfigurableLibraryState CreateLibraryState();

        #endregion
    }
}
