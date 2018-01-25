
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
        /// Creates an instance of a dependency injection service.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        /// <remarks>The method locates the dependency injection service with the full type name of
        /// <typeparamref name="TService"/>.</remarks>
        public TService CreateService<TService>() where TService : class
        {
            string serviceType = typeof(TService).FullName;
            Service service;

            if (m_services.TryGetValue(serviceType, out service))
                return(service.CreateInstance<TService>());
            else
                throw new KeyNotFoundException(LibraryMessages.Error081.FormatMessage(serviceType));
        }

        /// <summary>
        /// Creates an instance of a dependency injection service corresponding to a specified service name.
        /// </summary>
        /// <typeparam name="TService">Specifies a service type.</typeparam>
        /// <param name="serviceName">Specifies a service name.</param>
        /// <returns>Returns the created instance casted to the specified service type.</returns>
        public TService CreateService<TService>(string serviceName) where TService : class
        {
            Service service;

            if (m_services.TryGetValue(serviceName, out service))
                return(service.CreateInstance<TService>());
            else
                throw new KeyNotFoundException(LibraryMessages.Error016.FormatMessage(serviceName));
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
            Service service;

            m_services = new Dictionary<string, Service>();

            if (servicesNode == null)
                return;

            foreach (XmlNode serviceNode in servicesNode.ChildNodes)
            {
                service = new Service(serviceNode);

                if (m_services.ContainsKey(service.Key))
                    throw new InvalidConfigValueException(LibraryMessages.Error015.FormatMessage(service.Key));

                m_services.Add(service.Key, service);
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

        #region Private Fields

        /// <summary>
        /// Specifies a collection of the <see cref="Service"/> instances created based on the configuration.
        /// </summary>
        private IDictionary<string, Service> m_services;

        #endregion
    }
}
