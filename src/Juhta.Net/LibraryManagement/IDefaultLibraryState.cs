
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for default library states.
    /// </summary>
    /// <remarks>Default library states relate to dynamic initializable libraries.</remarks>
    public interface IDefaultLibraryState : ILibraryState
    {
        #region Methods

        /// <summary>
        /// Initializes the library state.
        /// </summary>
        /// <remarks>
        /// <para>Initializing a library state means performing all necessary actions on the objects comprising the
        /// library state so that the state is ready to be set as the effective library state.</para>
        /// <para>Default library states don't require any configuration for initialization.</para>
        /// </remarks>
        void Initialize();

        #endregion
    }
}
