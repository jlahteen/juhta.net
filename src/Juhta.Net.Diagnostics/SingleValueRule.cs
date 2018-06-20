
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Xml;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an abstract base class for such leaf rules that have only one value.
    /// </summary>
    internal abstract class SingleValueRule : LeafRule
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="singleValueRuleNode">Specifies a singleValueRule XML node based on which the instance will be
        /// initialized.</param>
        protected SingleValueRule(XmlNode singleValueRuleNode) : base(singleValueRuleNode)
        {
            m_ruleValue = singleValueRuleNode.GetAttribute("value").ToLower();
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies the value that relates to this SingleValueRule instance.
        /// </summary>
        protected string m_ruleValue;

        #endregion
    }
}
