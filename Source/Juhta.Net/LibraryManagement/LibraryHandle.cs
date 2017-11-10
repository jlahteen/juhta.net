
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an abstract base class for library handle classes.
    /// </summary>
    public abstract class LibraryHandle : ILibraryHandle
    {
        #region Public Properties

        /// <summary>
        /// Gets the configuration file name of the library that this <see cref="LibraryHandle"/> instance represents.
        /// </summary>
        public virtual string ConfigFileName
        {
            get {return(Path.GetFileNameWithoutExtension(m_libraryFileName) + ".config");}
        }

        /// <summary>
        /// See <see cref="ILibraryHandle.LibraryFileName"/>.
        /// </summary>
        public virtual string LibraryFileName
        {
            get {return(m_libraryFileName);}
        }

        /// <summary>
        /// Gets the root namespace of the library that this <see cref="LibraryHandle"/> instance represents.
        /// </summary>
        public virtual string LibraryRootNamespace
        {
            get {return(Path.GetFileNameWithoutExtension(m_libraryFileName));}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="libraryFileName">Specifies a value for the <see cref="LibraryFileName"/> property.</param>
        protected LibraryHandle(string libraryFileName)
        {
            m_libraryFileName = libraryFileName;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the embedded schema for the common configuration.
        /// </summary>
        /// <returns>Returns the embedded schema for the common configuration.</returns>
        protected XmlSchema GetCommonConfigSchema()
        {
            return(GetEmbeddedConfigSchema(Assembly.GetExecutingAssembly(), ConfigSchemaInfo.CommonConfigFileNamespace, ConfigSchemaInfo.CommonConfigFileName));
        }

        /// <summary>
        /// Gets an embedded configuration schema plus the common configuration schema.
        /// </summary>
        /// <param name="containingAssembly">Specifies an assembly where the embedded configuration schema will be
        /// searched for.</param>
        /// <param name="configSchemaFileNamespace">Specifies the file namespace of an embedded configuration schema.</param>
        /// <param name="configSchemaFileName">Specifies the file name of an embedded configuration schema.</param>
        /// <returns>Returns an array containing two schemas, the specified embedded configuration schema and the
        /// common configuration schema.</returns>
        protected XmlSchema[] GetEmbeddedConfigAndCommonSchema(Assembly containingAssembly, string configSchemaFileNamespace, string configSchemaFileName)
        {
            List<XmlSchema> schemas = new List<XmlSchema>();

            schemas.Add(GetEmbeddedConfigSchema(containingAssembly, configSchemaFileNamespace, configSchemaFileName));

            schemas.Add(GetCommonConfigSchema());

            return(schemas.ToArray());
        }

        /// <summary>
        /// Gets an embedded configuration schema.
        /// </summary>
        /// <param name="containingAssembly">Specifies an assembly where the embedded configuration schema will be
        /// searched for.</param>
        /// <param name="configSchemaFileNamespace">Specifies the file namespace of an embedded configuration schema.</param>
        /// <param name="configSchemaFileName">Specifies the file name of an embedded configuration schema.</param>
        /// <returns>Returns the embedded configuration schema from the specified assembly corresponding to the
        /// specified file namespace and name.</returns>
        protected XmlSchema GetEmbeddedConfigSchema(Assembly containingAssembly, string configSchemaFileNamespace, string configSchemaFileName)
        {
            string configSchemaContent;
            XmlSchema configSchema = null;

            configSchemaContent = containingAssembly.LoadEmbeddedResourceFile(configSchemaFileName, configSchemaFileNamespace);

            configSchema = XmlSchema.Read(new StringReader(configSchemaContent), null);

            return(configSchema);
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Stores the <see cref="LibraryFileName"/> property.
        /// </summary>
        protected string m_libraryFileName;

        #endregion

        #region Internal Methods

        /// <summary>
        /// Creates an instance of <see cref="ILibraryHandle"/> based on a specified library XML node.
        /// </summary>
        /// <param name="libraryNode">Specifies a library XML node.</param>
        /// <returns>Returns the created <see cref="ILibraryHandle"/> instance.</returns>
        internal static ILibraryHandle CreateInstance(XmlNode libraryNode)
        {
            string libraryFileName, libraryHandleClass;

            libraryFileName = libraryNode.GetAttribute("libraryFileName");

            if (!File.Exists(Application.BinDirectory + Path.DirectorySeparatorChar + libraryFileName))
                throw new FileNotFoundException(LibraryMessages.Error022.FormatMessage(libraryFileName, Application.BinDirectory));

            libraryHandleClass = libraryNode.GetAttribute("libraryHandleClass", ".LibraryHandle");

            return(ObjectFactory.CreateInstance<ILibraryHandle>(Application.BinDirectory + Path.DirectorySeparatorChar + libraryFileName, libraryHandleClass));
        }

        #endregion
    }
}
