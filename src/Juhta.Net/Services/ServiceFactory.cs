
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System.Collections.Generic;
using System.Xml;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines a class that provides methods for getting metadata and creating instances of the configured dependency
    /// injection services.
    /// </summary>
    public class ServiceFactory : Singleton<ServiceFactory>
    {
        #region Public Methods

        /// <summary>
        /// Creates an instance of a dependency injection service corresponding to a specified service type.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        public TService CreateService<TService>() where TService : class
        {
            return(CreateService<TService>(new ServiceId("type", typeof(TService).FullName)));
        }

        /// <summary>
        /// Creates an instance of a dependency injection service corresponding to a specified service identifier.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <param name="serviceId">Specifies a service identifier.</param>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        public TService CreateService<TService>(ServiceId serviceId) where TService : class
        {
            Service service;

            if (m_servicesById.TryGetValue(serviceId.Value, out service))
                return(service.CreateInstance<TService>());
            else
                throw new KeyNotFoundException(LibraryMessages.Error016.FormatMessage(serviceId.Value));
        }

        /// <summary>
        /// Creates an instance of a dependency injection service corresponding to a specified service name.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <param name="serviceName">Specifies a service name.</param>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        public TService CreateService<TService>(string serviceName) where TService : class
        {
            return(CreateService<TService>(new ServiceId("name", serviceName)));
        }

        /// <summary>
        /// Creates an instance of a dependency injection service corresponding to a specified service identifier
        /// scheme and specifier.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <param name="serviceIdScheme">Specifies a service identifier scheme.</param>
        /// <param name="serviceIdSpecifier">Specifies a service identifier specifier.</param>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        public TService CreateService<TService>(string serviceIdScheme, string serviceIdSpecifier) where TService : class
        {
            return(CreateService<TService>(new ServiceId(serviceIdScheme, serviceIdSpecifier)));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets an array of the <see cref="Service"/> instances created based on the configuration. The array is empty
        /// if there are no configured dependency injection services.
        /// </summary>
        public Service[] Services
        {
            get {return(m_services);}
        }

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="servicesNode">Specifies a services XML node based on which to initialize the instance. Can be
        /// null.</param>
        internal ServiceFactory(XmlNode servicesNode)
        {
            List<XmlNode> serviceNodes = new List<XmlNode>();
            Service service;

            m_servicesById = new Dictionary<string, Service>();

            if (servicesNode == null)
                return;

            CollectServiceNodes(servicesNode, serviceNodes);

            foreach (XmlNode serviceNode in serviceNodes)
            {
                service = new Service(serviceNode);

                if (m_servicesById.ContainsKey(service.Id.Value))
                    throw new InvalidConfigValueException(LibraryMessages.Error015.FormatMessage(service.Id.Value));

                m_servicesById.Add(service.Id.Value, service);
            }

            m_services = new Service[m_servicesById.Count];

            if (m_services.Length > 0)
                m_servicesById.Values.CopyTo(m_services, 0);

            SetSingletonInstance(this);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Closes this <see cref="ServiceFactory"/> instance by releasing all the metadata related to the configured
        /// dependency injection services.
        /// </summary>
        internal void Close()
        {
            m_servicesById.Clear();

            ResetSingletonInstance();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Collects recursively service XML nodes from a specified serviceGroup XML node.
        /// </summary>
        /// <param name="serviceGroupNode">Specifies a serviceGroup XML node.</param>
        /// <param name="serviceNodes">Specifies a list of <see cref="XmlNode"/> objects for storing the found service
        /// XML nodes.</param>
        private static void CollectServiceNodes(XmlNode serviceGroupNode, List<XmlNode> serviceNodes)
        {
            foreach (XmlNode node in serviceGroupNode)

                if (node.LocalName == "service")
                    serviceNodes.Add(node);

                else if (node.LocalName == "serviceGroup")
                    CollectServiceNodes(node, serviceNodes);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Services"/> property.
        /// </summary>
        private Service[] m_services;

        /// <summary>
        /// Specifies a collection of the <see cref="Service"/> instances indexed by identifier. The collection has
        /// been created based on the configuration.
        /// </summary>
        private IDictionary<string, Service> m_servicesById;

        #endregion
    }
}
