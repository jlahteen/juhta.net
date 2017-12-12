
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
    /// Defines the IsNullRule filter rule.
    /// </summary>
    internal class IsNullRule : LeafRule, IFilterRule
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="isNullRuleNode">Specifies an isNullRule XML node based on which the instance will be
        /// initialized.</param>
        public IsNullRule(XmlNode isNullRuleNode) : base(isNullRuleNode)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IFilterRule.IsValidFor"/>.
        /// </summary>
        public virtual bool IsValidFor(Event @event)
        {
            string fieldValue = @event.GetFieldValue(m_field);

            return(String.IsNullOrEmpty(fieldValue));
        }

        #endregion
    }
}
