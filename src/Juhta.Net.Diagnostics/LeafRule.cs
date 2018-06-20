
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
    /// Defines an abstract base class for such filter rules that don't have any child rules.
    /// </summary>
    internal abstract class LeafRule
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="leafRuleNode">Specifies a leafRule XML node based on which the instance will be initialized.</param>
        protected LeafRule(XmlNode leafRuleNode)
        {
            m_field = (EventField)Enum.Parse(typeof(EventField), leafRuleNode.GetAttribute("field"));
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies the event field associated with this LeafRule instance.
        /// </summary>
        protected EventField m_field;

        #endregion
    }
}
