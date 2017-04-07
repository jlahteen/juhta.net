
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for configurable libraries. A library is configurable if it requires specific startup
    /// operations and those operations can be configured.
    /// </summary>
    public interface IConfigurableLibrary
    {
        #region Methods

        /// <summary>
        /// Gets the XML schemas that the configuration file of the library must conform to.
        /// </summary>
        /// <returns>Returns an array of XmlSchema objects.</returns>
        /// <remarks>The return value null indicates that the configuration of the library is not XML schema
        /// controlled.</remarks>
        XmlSchema[] GetConfigSchemas();

        /// <summary>
        /// Initializes the library based on a specified configuration.
        /// </summary>
        /// <param name="config">Specifies an XmlDocument object containing a configuration for the library.</param>
        void InitializeLibrary(XmlDocument config);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the configuration file for the library.
        /// </summary>
        string ConfigFileName {get;}

        #endregion
    }
}
