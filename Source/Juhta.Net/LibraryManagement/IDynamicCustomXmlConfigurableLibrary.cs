
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Xml.Schema;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for dynamic custom XML configurable libraries. A library is a dynamic custom XML
    /// configurable library if it is dynamic, configurable and the configuration is done with a custom XML
    /// configuration.
    /// </summary>
    public interface IDynamicCustomXmlConfigurableLibrary : IDynamicLibrary, IConfigurableLibrary
    {
        #region Methods

        /// <summary>
        /// Creates an uninitialized library state.
        /// </summary>
        /// <returns>Returns an uninitialized <see cref="ICustomXmlConfigurableLibraryState"/> object.</returns>
        ICustomXmlConfigurableLibraryState CreateLibraryState();

        /// <summary>
        /// Gets the XML schemas to which configuration files must conform.
        /// </summary>
        /// <returns>Returns an array of <see cref="XmlSchema"/> objects.</returns>
        /// <remarks>The return value null indicates that the configuration of the library is not controlled by XML
        /// schemas.</remarks>
        XmlSchema[] GetConfigSchemas();

        #endregion
    }
}
