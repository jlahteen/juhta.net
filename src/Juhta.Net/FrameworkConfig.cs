
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

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
        /// <param name="anyConfigNode">Specifies an <see cref="XmlNode"/> object containing in a library
        /// configuration.</param>
        /// <param name="schemaVersion">Specifies the schema version of the library configuration.</param>
        /// <returns>Returns the created <see cref="XmlNamespaceManager"/> object specified by the parameters.</returns>
        public static XmlNamespaceManager CreateNamespaceManager(string libraryFileName, XmlNode anyConfigNode, string schemaVersion)
        {
            XmlNamespaceManager namespaceManager;

            if (anyConfigNode.OwnerDocument != null)
                namespaceManager = new XmlNamespaceManager(anyConfigNode.OwnerDocument.NameTable);
            else
                namespaceManager = new XmlNamespaceManager(((XmlDocument)anyConfigNode).NameTable);

            namespaceManager.AddNamespace("ns", FrameworkConfig.GetLibraryConfigXmlns(libraryFileName, schemaVersion));

            namespaceManager.AddNamespace("common", FrameworkConfig.GetLibraryConfigXmlns("Common", FrameworkConfig.CommonConfigSchemaVersion));

            return(namespaceManager);
        }

        /// <summary>
        /// Creates an <see cref="XmlNamespaceManager"/> object for a specified root library configuration.
        /// </summary>
        /// <param name="anyRootConfigNode">Specifies an <see cref="XmlNode"/> object containing in a root library
        /// configuration.</param>
        /// <returns>Returns the created <see cref="XmlNamespaceManager"/> object for the specified root library
        /// configuration.</returns>
        public static XmlNamespaceManager CreateRootConfigNamespaceManager(XmlNode anyRootConfigNode)
        {
            XmlNamespaceManager namespaceManager;

            if (anyRootConfigNode.OwnerDocument != null)
                namespaceManager = new XmlNamespaceManager(anyRootConfigNode.OwnerDocument.NameTable);
            else
                namespaceManager = new XmlNamespaceManager(((XmlDocument)anyRootConfigNode).NameTable);

            namespaceManager.AddNamespace("ns", String.Format("{0}root-{1}.xsd", FrameworkConfig.RootXmlns, FrameworkConfig.RootConfigSchemaVersion));

            namespaceManager.AddNamespace("common", FrameworkConfig.GetLibraryConfigXmlns("Common", FrameworkConfig.CommonConfigSchemaVersion));

            return(namespaceManager);
        }

        /// <summary>
        /// Gets the embedded schema for the common configuration.
        /// </summary>
        /// <returns>Returns the embedded schema for the common configuration.</returns>
        public static XmlSchema GetEmbeddedCommonConfigSchema()
        {
            return(GetEmbeddedConfigSchema(typeof(FrameworkConfig).Assembly, FrameworkConfig.CommonConfigFileNamespace, FrameworkConfig.CommonConfigFileName));
        }

        /// <summary>
        /// Gets the default embedded configuration schema from a specified assembly.
        /// </summary>
        /// <param name="containingAssembly">Specifies an assembly where the default embedded configuration schema will
        /// be searched for.</param>
        /// <returns>Returns the default embedded configuration schema from the specified assembly.</returns>
        public static XmlSchema GetEmbeddedConfigSchema(Assembly containingAssembly)
        {
            return(GetEmbeddedConfigSchema(containingAssembly, containingAssembly.GetFileNameWithoutExtension(), "Config.xsd"));
        }

        /// <summary>
        /// Gets an embedded configuration schema from a specified assembly.
        /// </summary>
        /// <param name="containingAssembly">Specifies an assembly where an embedded configuration schema will be
        /// searched for.</param>
        /// <param name="configSchemaFileNamespace">Specifies the file namespace of an embedded configuration schema.</param>
        /// <param name="configSchemaFileName">Specifies the file name of an embedded configuration schema.</param>
        /// <returns>Returns the embedded configuration schema from the specified assembly corresponding to the
        /// specified file namespace and name.</returns>
        public static XmlSchema GetEmbeddedConfigSchema(Assembly containingAssembly, string configSchemaFileNamespace, string configSchemaFileName)
        {
            string configSchemaContent;
            XmlSchema configSchema = null;

            configSchemaContent = containingAssembly.LoadEmbeddedResourceFile(configSchemaFileName, configSchemaFileNamespace);

            configSchema = XmlSchema.Read(new StringReader(configSchemaContent), null);

            return(configSchema);
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
                libraryPart = libraryPart.Substring(FrameworkInfo.RootNamespace.Length + 1);

            if (libraryPart.EndsWith(".dll"))
                libraryPart = libraryPart.Substring(0, libraryPart.Length - ".dll".Length);

            libraryPart = libraryPart.Replace('.', '-');

            xmlns = FrameworkConfig.RootXmlns + libraryPart + "-" + schemaVersion + ".xsd";

            return(xmlns);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the file name of the common configuration XML schema.
        /// </summary>
        public static string CommonConfigFileName
        {
            get {return(c_commonConfigFileName);}
        }

        /// <summary>
        /// Gets the file namespace of the common configuration XML schema.
        /// </summary>
        public static string CommonConfigFileNamespace
        {
            get {return(FrameworkInfo.RootNamespace + ".Common");}
        }

        /// <summary>
        /// Gets the version of the common configuration XML schema.
        /// </summary>
        public static string CommonConfigSchemaVersion
        {
            get {return(c_commonConfigSchemaVersion);}
        }

        /// <summary>
        /// Gets the file name of the root configuration XML schema.
        /// </summary>
        public static string RootConfigFileName
        {
            get {return(c_rootConfigFileName);}
        }

        /// <summary>
        /// Gets the file namespace of the root configuration XML schema.
        /// </summary>
        public static string RootConfigFileNamespace
        {
            get {return(FrameworkInfo.RootNamespace);}
        }

        /// <summary>
        /// Gets the version of the root configuration XML schema.
        /// </summary>
        public static string RootConfigSchemaVersion
        {
            get {return(c_rootConfigSchemaVersion);}
        }

        /// <summary>
        /// Gets the root XML namespace for the configuration schemas of the framework libraries.
        /// </summary>
        public static string RootXmlns
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
        /// Defines the version of the common configuration XML schema.
        /// </summary>
        private const string c_commonConfigSchemaVersion = "v1";

        /// <summary>
        /// Defines the file name of the root configuration XML schema.
        /// </summary>
        private const string c_rootConfigFileName = "Config.xsd";

        /// <summary>
        /// Defines the version of the root configuration XML schema.
        /// </summary>
        private const string c_rootConfigSchemaVersion = "v1";

        /// <summary>
        /// Defines the root XML namespace for the configuration schemas of the framework libraries.
        /// </summary>
        private const string c_rootXmlns = "http://schemas.juhta.net/";

        #endregion
    }
}
