
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;
using System.Xml;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an abstract base class for such factory classes that create configuration state objects, which
    /// implement the <see cref="IConfigState"/> interface.
    /// </summary>
    public abstract class ConfigStateFactoryBase
    {
        #region Public Methods

        /// <summary>
        /// Creates an IConfigState object based on an XML document containing a library configuration.
        /// </summary>
        /// <param name="config">Specifies an XmlDocument object.</param>
        /// <param name="initialize">If true, the created IConfigState object will be initialized prior to the return
        /// of the method.</param>
        /// <returns>Returns an IConfigState object created based on the specified XML document.</returns>
        /// <remarks>This method is overridable, and the default implementation always throws a <see cref="NotSupportedException"/>
        /// exception.</remarks>
        public virtual IConfigState CreateConfigState(XmlDocument config, bool initialize)
        {
            throw new NotSupportedException(CommonMessages.Error009.FormatMessage("CreateConfigState", this.GetType()));
        }

        /// <summary>
        /// Creates an IConfigState object containing the default configuration state for the library.
        /// </summary>
        /// <param name="initialize">If true, the created IConfigState object will be initialized prior to the return
        /// of the method.</param>
        /// <returns>Returns an IConfigState object containing the default configuration state for the library.</returns>
        /// <remarks>This method is overridable, and the default implementation always throws a <see cref="NotSupportedException"/>
        /// exception.</remarks>
        public virtual IConfigState CreateDefaultConfigState(bool initialize)
        {
            throw new NotSupportedException(CommonMessages.Error009.FormatMessage("CreateDefaultConfigState", this.GetType()));
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies an XmlDocument object containing a library configuration. The current configuration state object
        /// will be constructed based on this XmlDocument object.
        /// </summary>
        protected XmlDocument m_config;

        /// <summary>
        /// Specifies an XmlNamespaceManager object to use when querying nodes from the current XmlDocument object.
        /// </summary>
        protected XmlNamespaceManager m_namespaceManager;

        #endregion
    }

    /// <summary>
    /// Defines an abstract generic class that extends the <see cref="ConfigStateFactoryBase"/> class such that the
    /// exact type of configuration state objects to create can be specified with a type parameter.
    /// </summary>
    /// <typeparam name="TConfigState">Specifies the type of configuration state objects to create.</typeparam>
    public abstract class ConfigStateFactoryBase<TConfigState> : ConfigStateFactoryBase where TConfigState : IConfigState
    {
        #region Protected Methods

        /// <summary>
        /// Resets the state of the instance.
        /// </summary>
        /// <param name="configState">Specifies a value for the <see cref="m_configState"/> field.</param>
        /// <param name="config">Specifies a value for the <see cref="ConfigStateFactoryBase.m_config"/> field.</param>
        /// <param name="namespaceManager">Specifies a value for the <see cref="ConfigStateFactoryBase.m_namespaceManager"/>
        /// field.</param>
        protected void ResetFactoryState(TConfigState configState, XmlDocument config, XmlNamespaceManager namespaceManager)
        {
            m_configState = configState;

            m_config = config;

            m_namespaceManager = namespaceManager;
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies the configuration state object that is currently being constructed.
        /// </summary>
        protected TConfigState m_configState;

        #endregion
    }
}
