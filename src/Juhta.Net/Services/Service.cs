
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using System.Collections.Generic;
using System.Xml;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines a class that encapsulates a dependency injection service.
    /// </summary>
    public class Service
    {
        #region Public Methods

        /// <summary>
        /// Creates an instance of the encapsulated dependency injection service.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        public TService CreateInstance<TService>() where TService : class
        {
            return(ObjectFactory.CreateInstance<TService>(m_libraryFileName, m_libraryClass, m_constructorParams));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name
        {
            get {return(m_name);}
        }

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serviceNode">Specifies a service XML node based on which to initialize the instance.</param>
        internal Service(XmlNode serviceNode)
        {
            XmlNode constructorNode;
            XmlNamespaceManager namespaceManager = FrameworkConfig.CreateRootConfigNamespaceManager(serviceNode.OwnerDocument);
            List<Param> constructorParams = new List<Param>();

            m_name = serviceNode.GetAttribute("name");

            m_libraryFileName = serviceNode.GetAttribute("libraryFileName");

            m_libraryClass = serviceNode.GetAttribute("libraryClass");

            constructorNode = serviceNode.SelectSingleNode("//ns:constructor", namespaceManager);

            if (constructorNode == null)
                return;

            foreach (XmlNode paramNode in constructorNode.ChildNodes)
                constructorParams.Add(new Param(paramNode));

            m_constructorParams = new object[constructorParams.Count];

            for (int i = 0; i < m_constructorParams.Length; i++)
                m_constructorParams[i] = constructorParams[i].Value;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies an array of the constructor parameters for creating instances of the service. Can be null.
        /// </summary>
        private object[] m_constructorParams;

        /// <summary>
        /// Specifies the library class that implements the service.
        /// </summary>
        private string m_libraryClass;

        /// <summary>
        /// Specifies the file name of the library that contains the service.
        /// </summary>
        private string m_libraryFileName;

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        #endregion
    }
}
