
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
    /// Defines an operator rule that uses the logical AND operator between child rules.
    /// </summary>
    internal class AndRule : OperatorRule, IFilterRule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="andRuleNode">Specifies an andRule XML node based on which the instance will be initialized.</param>
        public AndRule(XmlNode andRuleNode) : base(andRuleNode)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IFilterRule.IsValidFor"/>.
        /// </summary>
        public bool IsValidFor(Event @event)
        {
            foreach (IFilterRule filterRule in m_childRules)
                if (!filterRule.IsValidFor(@event))
                    return(false);

            return(true);
        }

        #endregion
    }
}
