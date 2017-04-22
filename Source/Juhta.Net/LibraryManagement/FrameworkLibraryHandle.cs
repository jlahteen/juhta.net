
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System.Threading;
using System.Xml;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an abstract base class for framework library handles.
    /// </summary>
    public abstract class FrameworkLibraryHandle : LibraryHandle
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="libraryFileName">Specifies a value for the <see cref="LibraryHandle.LibraryFileName"/>
        /// property.</param>
        protected FrameworkLibraryHandle(string libraryFileName) : base(libraryFileName)
        {}

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates a <see cref="XmlNamespaceManager"/> object for an XML configuration of the library represented by
        /// this <see cref="FrameworkLibraryHandle"/> instance.
        /// </summary>
        /// <param name="config">Specifies a <see cref="XmlDocument"/> object containing a library configuration.</param>
        /// <param name="schemaVersion">Specifies the schema version of the library configuration.</param>
        /// <returns>Returns the created <see cref="XmlNamespaceManager"/> object.</returns>
        protected XmlNamespaceManager CreateNamespaceManager(XmlDocument config, string schemaVersion)
        {
            string xmlns;
            XmlNamespaceManager namespaceManager;

            xmlns = ConfigSchemaInfo.GetFrameworkLibraryConfigXmlns(m_libraryFileName, schemaVersion);

            namespaceManager = new XmlNamespaceManager(config.NameTable);

            namespaceManager.AddNamespace("ns", xmlns);

            return(namespaceManager);
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies a <see cref="ConfigStateFactoryBase"/> object for creating <see cref="IConfigState"/> objects for
        /// the framework library determined by this <see cref="FrameworkLibraryHandle"/> instance.
        /// </summary>
        protected ConfigStateFactoryBase m_configStateFactory;

        /// <summary>
        /// Specifies a lock object for managing concurrent access to the configuration state of the framework library
        /// determined by this <see cref="FrameworkLibraryHandle"/> instance.
        /// </summary>
        protected ReaderWriterLockSlim m_configStateLock;

        #endregion
    }
}
