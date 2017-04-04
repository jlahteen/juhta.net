
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
    /// Defines an abstract base class for such leaf rules that can have multiple (one or more) values.
    /// </summary>
    internal abstract class MultiValueRule : LeafRule
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="multiValueRuleNode">Specifies a multiValueRule XML node based on which the instance will be
        /// initialized.</param>
        protected MultiValueRule(XmlNode multiValueRuleNode) : base(multiValueRuleNode)
        {
            m_ruleValues = new string[multiValueRuleNode.ChildNodes.Count];

            for (int i = 0; i < multiValueRuleNode.ChildNodes.Count; i++)
                m_ruleValues[i] = multiValueRuleNode.ChildNodes[i].InnerText.ToLower();

            Array.Sort<string>(m_ruleValues);
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies the values that relate to this MultiValueRule instance.
        /// </summary>
        protected string[] m_ruleValues;

        #endregion
    }
}
