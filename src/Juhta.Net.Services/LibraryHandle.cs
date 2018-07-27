
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.LibraryManagement;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines the library handle of Services Library.
    /// </summary>
    public class LibraryHandle : LibraryHandleBase, ICustomXmlConfigurableLibrary, IClosableLibrary
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="IClosableLibrary.CloseLibrary"/>.
        /// </summary>
        public bool CloseLibrary()
        {
            if (ServiceFactory.Instance != null)
                ServiceFactory.Instance.Close();

            return(true);
        }

        /// <summary>
        /// See <see cref="ICustomXmlConfigurableLibrary.GetConfigSchemas"/>.
        /// </summary>
        public XmlSchema[] GetConfigSchemas()
        {
            return(GetEmbeddedConfigAndCommonSchema(Assembly.GetExecutingAssembly()));
        }

        /// <summary>
        /// See <see cref="ICustomXmlConfigurableLibrary.InitializeLibrary(XmlDocument)"/>.
        /// </summary>
        public void InitializeLibrary(XmlDocument config)
        {
            XmlNamespaceManager namespaceManager;
            ServiceFactory serviceFactory;

            namespaceManager = FrameworkConfig.CreateNamespaceManager(this.LibraryFileName, config, "v1");

            serviceFactory = new ServiceFactory(config.SelectSingleNode("//ns:services", namespaceManager));
        }

        #endregion
    }
}
