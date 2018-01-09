
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using Juhta.Net.Common;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines a class that encapsulates a dependency injection service.
    /// </summary>
    public class Service
    {
        #region Static Constructor
        #endregion

        #region Public Constructors
        #endregion

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

        #region Public Indexers
        #endregion

        #region Public Types
        #endregion

        #region Public Constants
        #endregion

        #region Protected Constructors
        #endregion

        #region Protected Methods
        #endregion

        #region Protected Properties
        #endregion

        #region Protected Types
        #endregion

        #region Protected Fields
        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="serviceNode">Specifies a service XML node based on which to initialize the instance.</param>
        internal Service(XmlNode serviceNode)
        {
            XmlNamespaceManager namespaceManager = FrameworkConfig.CreateRootConfigNamespaceManager(serviceNode.OwnerDocument);


        }

        #endregion

        #region Internal Methods
        #endregion

        #region Internal Properties
        #endregion

        #region Internal Types
        #endregion

        #region Private Methods
        #endregion

        #region Private Types
        #endregion

        #region Private Constants
        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies an array of constructor parameters for creating instances of the service. Can be null.
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

        #region Destructor
        #endregion
    }
}
