
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

            if (m_services.TryGetValue(serviceId.Value, out service))
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

        /// <summary>
        /// Gets the <see cref="Service"/> instances created based on the configuration.
        /// </summary>
        /// <returns>Returns an array of the <see cref="Service"/> instances created based on the configuration. The
        /// array is empty if there are no configured dependency injection services.</returns>
        public Service[] GetServices()
        {
            Service[] services;

            if (m_services.Count == 0)
                return(new Service[]{});

            services = new Service[m_services.Count];

            m_services.Values.CopyTo(services, 0);

            return(services);
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

            m_services = new Dictionary<string, Service>();

            if (servicesNode == null)
                return;

            CollectServiceNodes(servicesNode, serviceNodes);

            foreach (XmlNode serviceNode in serviceNodes)
            {
                service = new Service(serviceNode);

                if (m_services.ContainsKey(service.Id.Value))
                    throw new InvalidConfigValueException(LibraryMessages.Error015.FormatMessage(service.Id.Value));

                m_services.Add(service.Id.Value, service);
            }

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
            m_services.Clear();

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
        /// Specifies a collection of the <see cref="Service"/> instances created based on the configuration.
        /// </summary>
        private IDictionary<string, Service> m_services;

        #endregion
    }
}
