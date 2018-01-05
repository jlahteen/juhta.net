
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
    /// Defines a class that provides a method for creating instances of the configured dependency injection services.
    /// </summary>
    public class ServiceFactory : Singleton<ServiceFactory>
    {
        #region Static Constructor
        #endregion

        #region Public Constructors
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates an instance of the specified service type.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <returns>Returns the created instance of the specified service type.</returns>
        /// <remarks>The method locates the service with the full type name of <typeparamref name="TService"/>.</remarks>
        public TService CreateService<TService>() where TService : class
        {
            return(CreateService<TService>(typeof(TService).FullName));
        }

        /// <summary>
        /// todo
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService CreateService<TService>(string serviceName) where TService : class
        {
            return default(TService);
        }

        #endregion

        #region Public Properties
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
        /// <param name="servicesNode">Specifies a services XML node based on which to initialize the instance.</param>
        internal ServiceFactory(XmlNode servicesNode)
        {
            Service service;

            foreach (XmlNode serviceNode in servicesNode.ChildNodes)
            {
                service = new Service(serviceNode);

                if (m_services.ContainsKey(service.Name))
                    throw new InvalidConfigValueException(LibraryMessages.Error015.FormatMessage(service.Name));

                m_services.Add(service.Name, service);
            }

            SetSingletonInstance(this);
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

        private IDictionary<string, Service> m_services;

        #endregion

        #region Destructor
        #endregion
    }
}
