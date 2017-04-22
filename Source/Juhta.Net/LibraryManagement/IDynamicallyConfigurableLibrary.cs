
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
    /// Defines an interface for dynamically configurable libraries. A library is dynamically configurable, if it is
    /// configurable and its configuration can be changed at the runtime.
    /// </summary>
    public interface IDynamicallyConfigurableLibrary : IConfigurableLibrary, IDynamicLibrary
    {
        #region Methods

        /// <summary>
        /// Creates a configuration state based on an XML document containing a configuration for the library.
        /// </summary>
        /// <param name="config">Specifies an XmlDocument object containing a configuration for the library.</param>
        /// <returns>Returns an IConfigState object containing a configuration state created based on the specified
        /// XmlDocument object.</returns>
        IConfigState CreateConfigState(XmlDocument config);

        #endregion
    }
}
