
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Xml;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for custom XML configurable library states.
    /// </summary>
    /// <remarks>Custom XML configurable library states relate to dynamic custom XML configurable libraries.</remarks>
    public interface ICustomXmlConfigurableLibraryState : ILibraryState
    {
        #region Methods

        /// <summary>
        /// Initializes the library state based on a specified XML configuration.
        /// </summary>
        /// <param name="config">Specifies an <see cref="XmlDocument"/> object containing an XML configuration for the
        /// library state.</param>
        /// <remarks>Initializing a library state means performing all necessary actions on the objects comprising the
        /// library state so that the state is ready to be set as the effective library state.</remarks>
        void Initialize(XmlDocument config);

        #endregion
    }
}
