
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for custom XML configurable libraries. A library is custom XML configurable if it is
    /// configurable and the configuration is done with a custom XML configuration.
    /// </summary>
    public interface ICustomXmlConfigurableLibrary : IConfigurableLibraryBase
    {
        #region Methods

        /// <summary>
        /// Gets the XML schemas to which configuration files must conform.
        /// </summary>
        /// <returns>Returns an array of <see cref="XmlSchema"/> objects.</returns>
        /// <remarks>The return value null indicates that the configuration of the library is not controlled by XML
        /// schemas.</remarks>
        XmlSchema[] GetConfigSchemas();

        /// <summary>
        /// Initializes the library based on a specified XML configuration.
        /// </summary>
        /// <param name="config">Specifies an <see cref="XmlDocument"/> object containing an XML configuration for the
        /// library.</param>
        void InitializeLibrary(XmlDocument config);

        #endregion
    }
}
