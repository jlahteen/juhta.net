
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
    /// Defines an interface for configurable library states.
    /// </summary>
    /// <remarks>Configurable library states relate to dynamic configurable libraries.</remarks>
    public interface IConfigurableLibraryState : ILibraryState
    {
        #region Methods

        /// <summary>
        /// Initializes the library state based on a specified configuration.
        /// </summary>
        /// <param name="config">Specifies an <see cref="IConfigurationRoot"/> object containing a configuration for
        /// the library state.</param>
        /// <remarks>Initializing a library state means performing all necessary actions on the objects comprising the
        /// library state so that the state is ready to be set as the effective library state.</remarks>
        void Initialize(IConfigurationRoot config);

        #endregion
    }
}
