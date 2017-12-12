
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
    /// Defines a trace element that stores trace data related to a single execution of a class constructor.
    /// </summary>
    internal class ConstructorTrace : MemberTrace
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parentElement">Specifies a TraceElement object that will be set as the parent element for the
        /// instance.</param>
        /// <param name="className">Specifies a class name.</param>
        public ConstructorTrace(TraceElement parentElement, string className) : base(parentElement, className, "Constructor")
        {
            m_description = BuildDescription(className);

            m_startLine = "Constructor Start: " + className;

            m_endLine = "Constructor End: " + className;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds a description for an instance of the ConstructorTrace class.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <returns>Returns the built description.</returns>
        public static string BuildDescription(string className)
        {
            return(String.Format("Execution of a {0} constructor", className));
        }

        #endregion
    }
}
