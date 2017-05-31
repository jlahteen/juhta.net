
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
    /// Defines an interface for dynamic custom XML configurable libraries. A library is a dynamic custom XML
    /// configurable library if it is custom XML configurable and its configuration can be changed at the runtime.
    /// </summary>
    public interface IDynamicCustomXmlConfigurableLibrary : ICustomXmlConfigurableLibrary, IDynamicLibrary
    {
        #region Methods

        /// <summary>
        /// Creates a library state based on an XML document containing a configuration for the library.
        /// </summary>
        /// <param name="config">Specifies an <see cref="XmlDocument"/> object containing an XML configuration for the
        /// library.</param>
        /// <returns>Returns an <see cref="ILibraryState"/> object containing the library state created based on the
        /// specified <see cref="XmlDocument"/> object.</returns>
        ILibraryState CreateLibraryState(XmlDocument config);

        #endregion
    }
}
