
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines a class that encapsulates the metadata of a dependency injection service.
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
            try
            {
                return(ObjectFactory.CreateInstance<TService>(m_libraryFileName, m_libraryClass, m_constructorParamObjs));
            }

            catch (Exception ex)
            {
                throw new ServiceCreationException(LibraryMessages.Error080.FormatMessage(this.Key), ex);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets an array of <see cref="ConstructorParam"/> objects specifying the constructor parameters for the
        /// dependency injection service. Can be null.
        /// </summary>
        public ConstructorParam[] ConstructorParams
        {
            get {return(m_constructorParams);}
        }

        /// <summary>
        /// Gets the key of the dependency injection service. The key is the combination of the service type and
        /// service name separated by a slash.
        /// </summary>
        public string Key
        {
            get {return(m_type + (m_name != null ? "/" + m_name : String.Empty));}
        }

        /// <summary>
        /// Gets the library class that implements the dependency injection service.
        /// </summary>
        public string LibraryClass
        {
            get {return(m_libraryClass);}
        }

        /// <summary>
        /// Gets the file name of the library that contains the dependency injection service.
        /// </summary>
        public string LibraryFileName
        {
            get {return(m_libraryFileName);}
        }

        /// <summary>
        /// Gets the name of the dependency injection service. Can be null.
        /// </summary>
        public string Name
        {
            get {return(m_name);}
        }

        /// <summary>
        /// Gets the type of the dependency injection service. Can be null.
        /// </summary>
        public string Type
        {
            get {return(m_type);}
        }

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serviceNode">Specifies a service XML node based on which to initialize the instance.</param>
        internal Service(XmlNode serviceNode)
        {
            XmlNode constructorParamsNode;
            XmlNamespaceManager namespaceManager = FrameworkConfig.CreateRootConfigNamespaceManager(serviceNode.OwnerDocument);
            List<ConstructorParam> constructorParams = new List<ConstructorParam>();

            if (serviceNode.HasAttribute("name"))
                m_name = serviceNode.GetAttribute("name");

            m_type = serviceNode.GetAttribute("type");

            try
            {
                m_libraryFileName = serviceNode.GetAttribute("libraryFileName");

                m_libraryClass = serviceNode.GetAttribute("libraryClass");

                constructorParamsNode = serviceNode.SelectSingleNode("ns:constructorParams", namespaceManager);

                if (constructorParamsNode == null)
                    return;

                foreach (XmlNode paramNode in constructorParamsNode.ChildNodes)
                    constructorParams.Add(new ConstructorParam(paramNode));

                if (constructorParams.Count == 0)
                    return;

                m_constructorParamObjs = new object[constructorParams.Count];

                for (int i = 0; i < m_constructorParamObjs.Length; i++)
                    m_constructorParamObjs[i] = constructorParams[i].Value;

                m_constructorParams = constructorParams.ToArray();
            }

            catch (Exception ex)
            {
                throw new ServiceInitializationException(LibraryMessages.Error074.FormatMessage(this.Key), ex);
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ConstructorParams"/> property.
        /// </summary>
        private ConstructorParam[] m_constructorParams;

        /// <summary>
        /// Specifies an array of the constructor parameters for creating instances of the service. Can be null.
        /// </summary>
        private object[] m_constructorParamObjs;

        /// <summary>
        /// Stores the <see cref="LibraryClass"/> property.
        /// </summary>
        private string m_libraryClass;

        /// <summary>
        /// Stores the <see cref="LibraryFileName"/> property.
        /// </summary>
        private string m_libraryFileName;

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        /// <summary>
        /// Stores the <see cref="Type"/> property.
        /// </summary>
        private string m_type;

        #endregion
    }
}
