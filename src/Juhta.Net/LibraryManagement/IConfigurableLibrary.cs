
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Microsoft.Extensions.Configuration;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for such configurable libraries whose configuration is done with a JSON, XML or INI
    /// configuration.
    /// </summary>
    public interface IConfigurableLibrary : IConfigurableLibraryBase
    {
        #region Methods

        /// <summary>
        /// Initializes the library based on a specified configuration.
        /// </summary>
        /// <param name="config">Specifies an <see cref="IConfigurationRoot"/> object containing a configuration for
        /// the library.</param>
        void InitializeLibrary(IConfigurationRoot config);

        #endregion
    }
}
