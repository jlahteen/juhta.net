
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a trace element that stores trace data related to a single execution of any operation.
    /// </summary>
    internal class OperationTrace : TraceElement
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parentElement">Specifies a TraceElement object that will be set as the parent element for the
        /// instance.</param>
        /// <param name="description">Specifies a description for trace data that will be written to the instance.</param>
        public OperationTrace(TraceElement parentElement, string description) : base(parentElement)
        {
            m_description = description;

            m_startLine = "Operation Start: " + m_description;

            m_endLine = "Operation End: " + m_description;
        }

        #endregion
    }
}
