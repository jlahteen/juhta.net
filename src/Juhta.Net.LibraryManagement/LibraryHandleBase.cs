
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Schema;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an abstract base class for library handle classes.
    /// </summary>
    public abstract class LibraryHandleBase : ILibraryHandle
    {
        #region Public Properties

        /// <summary>
        /// Gets the configuration file name of the library that this <see cref="LibraryHandleBase"/> instance
        /// represents.
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
        /// Gets the root namespace of the library that this <see cref="LibraryHandleBase"/> instance represents.
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
        protected LibraryHandleBase()
        {
            m_libraryFileName = this.GetType().Namespace + ".dll";
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="libraryFileName">Specifies a value for the <see cref="LibraryFileName"/> property.</param>
        protected LibraryHandleBase(string libraryFileName)
        {
            m_libraryFileName = libraryFileName;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the default embedded configuration schema plus the common configuration schema.
        /// </summary>
        /// <param name="containingAssembly">Specifies an assembly where an default embedded configuration schema will
        /// be searched for.</param>
        /// <returns>Returns an array containing two schemas, the default embedded configuration schema and the common
        /// configuration schema.</returns>
        protected XmlSchema[] GetEmbeddedConfigAndCommonSchema(Assembly containingAssembly)
        {
            List<XmlSchema> schemas = new List<XmlSchema>();

            schemas.Add(FrameworkConfig.GetEmbeddedConfigSchema(containingAssembly));

            schemas.Add(FrameworkConfig.GetEmbeddedCommonConfigSchema());

            return(schemas.ToArray());
        }

        /// <summary>
        /// Gets an embedded configuration schema plus the common configuration schema.
        /// </summary>
        /// <param name="containingAssembly">Specifies an assembly where an embedded configuration schema will be
        /// searched for.</param>
        /// <param name="configSchemaFileNamespace">Specifies the file namespace of an embedded configuration schema.</param>
        /// <param name="configSchemaFileName">Specifies the file name of an embedded configuration schema.</param>
        /// <returns>Returns an array containing two schemas, the specified embedded configuration schema and the
        /// common configuration schema.</returns>
        protected XmlSchema[] GetEmbeddedConfigAndCommonSchema(Assembly containingAssembly, string configSchemaFileNamespace, string configSchemaFileName)
        {
            List<XmlSchema> schemas = new List<XmlSchema>();

            schemas.Add(FrameworkConfig.GetEmbeddedConfigSchema(containingAssembly, configSchemaFileNamespace, configSchemaFileName));

            schemas.Add(FrameworkConfig.GetEmbeddedCommonConfigSchema());

            return(schemas.ToArray());
        }

        /// <summary>
        /// Gets the default embedded configuration schema from a specified assembly.
        /// </summary>
        /// <param name="containingAssembly">Specifies an assembly where an default embedded configuration schema will
        /// be searched for.</param>
        /// <returns>Returns the default embedded configuration schema from the specified assembly.</returns>
        public static XmlSchema GetEmbeddedConfigSchema(Assembly containingAssembly)
        {
            return(FrameworkConfig.GetEmbeddedConfigSchema(containingAssembly));
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
        protected XmlSchema GetEmbeddedConfigSchema(Assembly containingAssembly, string configSchemaFileNamespace, string configSchemaFileName)
        {
            return(FrameworkConfig.GetEmbeddedConfigSchema(containingAssembly, configSchemaFileNamespace, configSchemaFileName));
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="LibraryFileName"/> property.
        /// </summary>
        private string m_libraryFileName;

        #endregion
    }
}
