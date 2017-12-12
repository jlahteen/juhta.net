
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Xml;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an abstract base class for such filter rules that have child rules and which process the overall result
    /// based on the child rule results and an operator between them.
    /// </summary>
    internal abstract class OperatorRule
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="operatorRuleNode">Specifies an operatorRule XML node based on which the instance will be
        /// initialized.</param>
        protected OperatorRule(XmlNode operatorRuleNode)
        {
            m_childRules = new List<IFilterRule>();

            foreach (XmlNode childRuleNode in operatorRuleNode.ChildNodes)

                switch (childRuleNode.LocalName)
                {
                    case "andRule":
                        m_childRules.Add(new AndRule(childRuleNode));

                        break;

                    case "containsRule":
                        m_childRules.Add(new ContainsRule(childRuleNode));

                        break;

                    case "endsWithRule":
                        m_childRules.Add(new EndsWithRule(childRuleNode));

                        break;

                    case "equalsRule":
                        m_childRules.Add(new EqualsRule(childRuleNode));

                        break;

                    case "inListRule":
                        m_childRules.Add(new InListRule(childRuleNode));

                        break;

                    case "isNullRule":
                        m_childRules.Add(new IsNullRule(childRuleNode));

                        break;

                    case "notContainsRule":
                        m_childRules.Add(new NotContainsRule(childRuleNode));

                        break;

                    case "notEndsWithRule":
                        m_childRules.Add(new NotEndsWithRule(childRuleNode));

                        break;

                    case "notEqualsRule":
                        m_childRules.Add(new NotEqualsRule(childRuleNode));

                        break;

                    case "notInListRule":
                        m_childRules.Add(new NotInListRule(childRuleNode));

                        break;

                    case "notIsNullRule":
                        m_childRules.Add(new NotIsNullRule(childRuleNode));

                        break;

                    case "notStartsWithRule":
                        m_childRules.Add(new NotStartsWithRule(childRuleNode));

                        break;

                    case "orRule":
                        m_childRules.Add(new OrRule(childRuleNode));

                        break;

                    case "startsWithRule":
                        m_childRules.Add(new StartsWithRule(childRuleNode));

                        break;

                    default:
                        throw new UnimplementedCodeBranchException(childRuleNode.LocalName);
                }
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies the child rules of this OperatorRule instance.
        /// </summary>
        protected List<IFilterRule> m_childRules;

        #endregion
    }
}
