
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
    /// Defines the EndsWithRule filter rule.
    /// </summary>
    internal class EndsWithRule : SingleValueRule, IFilterRule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="endsWithRuleNode">Specifies an endsWithRule XML node based on which the instance will be
        /// initialized.</param>
        public EndsWithRule(XmlNode endsWithRuleNode) : base(endsWithRuleNode)
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
                return(fieldValue.ToLower().EndsWith(m_ruleValue));
        }

        #endregion
    }
}
