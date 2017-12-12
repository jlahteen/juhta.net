
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
    /// Defines the EqualsRule filter rule.
    /// </summary>
    internal class EqualsRule : SingleValueRule, IFilterRule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="equalsRuleNode">Specifies an equalsRule XML node based on which the instance will be
        /// initialized.</param>
        public EqualsRule(XmlNode equalsRuleNode) : base(equalsRuleNode)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IFilterRule.IsValidFor"/>.
        /// </summary>
        public virtual bool IsValidFor(Event @event)
        {
            string fieldValue = @event.GetFieldValue(m_field);

            if (fieldValue == null)
                return(false);
            else
                return(fieldValue.ToLower() == m_ruleValue);
        }

        #endregion
    }
}
