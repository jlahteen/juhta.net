
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Xml;

namespace Juhta.Net
{
    /// <summary>
    /// A static class providing common methods and properties related to configuration files of the framework
    /// libraries.
    /// </summary>
    public static class FrameworkConfig
    {
        #region Public Methods

        /// <summary>
        /// Creates an <see cref="XmlNamespaceManager"/> object corresponding to a specified framework library, XML
        /// configuration and schema version.
        /// </summary>
        /// <param name="libraryFileName">Specifies a framework library file name.</param>
        /// <param name="config">Specifies an <see cref="XmlDocument"/> object containing a library configuration.</param>
        /// <param name="schemaVersion">Specifies the schema version of the library configuration.</param>
        /// <returns>Returns the created <see cref="XmlNamespaceManager"/> object.</returns>
        public static XmlNamespaceManager CreateNamespaceManager(string libraryFileName, XmlDocument config, string schemaVersion)
        {
            XmlNamespaceManager namespaceManager;

            namespaceManager = new XmlNamespaceManager(config.NameTable);

            namespaceManager.AddNamespace("ns", FrameworkConfig.GetLibraryConfigXmlns(libraryFileName, schemaVersion));

            namespaceManager.AddNamespace("common", FrameworkConfig.GetLibraryConfigXmlns("Common", schemaVersion));

            return(namespaceManager);
        }

        /// <summary>
        /// Gets the configuration schema namespace for a framework library.
        /// </summary>
        /// <param name="libraryFileName">Specifies a framework library file name.</param>
        /// <param name="schemaVersion">Specifies a schema version.</param>
        /// <returns>Returns the configuration schema namespace for the specified framework library.</returns>
        public static string GetLibraryConfigXmlns(string libraryFileName, string schemaVersion)
        {
            string libraryPart, xmlns;

            libraryPart = libraryFileName.ToLower();

            if (libraryPart.StartsWith(FrameworkInfo.RootNamespace.ToLower()))
                libraryPart = libraryPart.Substring(FrameworkInfo.RootNamespace.Length);

            if (libraryPart.EndsWith(".dll"))
                libraryPart = libraryPart.Substring(0, libraryPart.Length - ".dll".Length);

            libraryPart = libraryPart.Replace('.', '-');

            xmlns = FrameworkConfig.RootXmlns + libraryPart + "-" + schemaVersion + ".xsd";

            return(xmlns);
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the file name of the common configuration XML schema.
        /// </summary>
        internal static string CommonConfigFileName
        {
            get {return(c_commonConfigFileName);}
        }

        /// <summary>
        /// Gets the file namespace of the common configuration XML schema.
        /// </summary>
        internal static string CommonConfigFileNamespace
        {
            get {return(FrameworkInfo.RootNamespace + ".Common");}
        }

        /// <summary>
        /// Gets the file name of the root configuration XML schema.
        /// </summary>
        internal static string RootConfigFileName
        {
            get {return(c_rootConfigFileName);}
        }

        /// <summary>
        /// Gets the file namespace of the root configuration XML schema.
        /// </summary>
        internal static string RootConfigFileNamespace
        {
            get {return(FrameworkInfo.RootNamespace);}
        }

        /// <summary>
        /// Gets the version of the root configuration XML schema.
        /// </summary>
        internal static string RootConfigVersion
        {
            get {return(c_rootConfigVersion);}
        }

        /// <summary>
        /// Gets the root XML namespace for the configuration schemas of the framework libraries.
        /// </summary>
        internal static string RootXmlns
        {
            get {return(c_rootXmlns);}
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Defines the file name of the common configuration XML schema.
        /// </summary>
        private const string c_commonConfigFileName = "CommonConfig.xsd";

        /// <summary>
        /// Defines the file name of the root configuration XML schema.
        /// </summary>
        private const string c_rootConfigFileName = "Config.xsd";

        /// <summary>
        /// Defines the version of the root configuration XML schema.
        /// </summary>
        private const string c_rootConfigVersion = "v1";

        /// <summary>
        /// Defines the root XML namespace for the configuration schemas of the framework libraries.
        /// </summary>
        private const string c_rootXmlns = "http://schemas.juhta.net/";

        #endregion
    }
}
