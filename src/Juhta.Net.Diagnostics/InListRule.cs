
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
    /// Defines the InListRule filter rule.
    /// </summary>
    internal class InListRule : MultiValueRule, IFilterRule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="inListRuleNode">Specifies an inListRule XML node based on which the instance will be
        /// initialized.</param>
        public InListRule(XmlNode inListRuleNode) : base(inListRuleNode)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IFilterRule.IsValidFor"/>.
        /// </summary>
        public virtual bool IsValidFor(Event @event)
        {
            string fieldValue = @event.GetFieldValue(m_field);

            if (fieldValue != null)
                fieldValue = fieldValue.ToLower();

            return(Array.BinarySearch<string>(m_ruleValues, fieldValue) >= 0);
        }

        #endregion
    }
}
