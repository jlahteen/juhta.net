
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
    /// Defines an interface for JSON configurable libraries. A library is JSON configurable if it is configurable and
    /// the configuration is done with a JSON configuration.
    /// </summary>
    public interface IJsonConfigurableLibrary : IConfigurableLibrary
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
