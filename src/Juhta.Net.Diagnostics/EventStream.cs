
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Xml;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an abstract base class for event streams.
    /// </summary>
    internal abstract class EventStream
    {
        #region Public Properties

        /// <summary>
        /// See <see cref="IEventStream.EventFilter"/>.
        /// </summary>
        public IEventFilter EventFilter
        {
            get {return(m_eventFilter);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected EventStream()
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="eventStreamNode">Specifies an eventStream XML node based on which the instance will be
        /// initialized.</param>
        protected EventStream(XmlNode eventStreamNode)
        {
            XmlNamespaceManager namespaceManager = DiagnosticsLibrary.Instance.CreateNamespaceManager(eventStreamNode);
            XmlNode eventFilterNode = eventStreamNode.SelectSingleNode("bf:eventFilter", namespaceManager);

            if (eventFilterNode != null)
                m_eventFilter = new RuleBasedFilter(eventFilterNode);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="EventFilter"/> property.
        /// </summary>
        private IEventFilter m_eventFilter;

        #endregion
    }
}
