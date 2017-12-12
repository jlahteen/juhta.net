
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
    /// Represents a rule-based event filter. A rule-based event filter has a rule tree whose first level consists of a
    /// single root rule and all subsequent levels can comprise any combination of predefined rules. In the rule tree,
    /// there are two rule types, andRule and orRule, that can have child rules.
    /// </summary>
    internal class RuleBasedFilter : IEventFilter
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="eventFilterNode">Specifies an eventFilter XML node base on which the instance will be
        /// initialized.</param>
        public RuleBasedFilter(XmlNode eventFilterNode)
        {
            if (eventFilterNode.ChildNodes.Count > 1)
                m_rootRule = new AndRule(eventFilterNode);

            else if (eventFilterNode.FirstChild.LocalName == "orRule")
                m_rootRule = new OrRule(eventFilterNode.FirstChild);

            else if (eventFilterNode.FirstChild.LocalName == "andRule")
                m_rootRule = new AndRule(eventFilterNode.FirstChild);

            else
                m_rootRule = new AndRule(eventFilterNode);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IEventFilter.StopsEvent"/>.
        /// </summary>
        public bool StopsEvent(Event @event)
        {
            return(!m_rootRule.IsValidFor(@event));
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the root rule of this RuleBasedFilter instance.
        /// </summary>
        private IFilterRule m_rootRule;

        #endregion
    }
}
