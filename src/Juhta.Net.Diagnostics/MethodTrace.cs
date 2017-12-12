
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
    /// Defines a trace element that stores trace data related to a single execution of a class method.
    /// </summary>
    internal class MethodTrace : MemberTrace
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parentElement">Specifies a TraceElement object that will be set as the parent element for the
        /// instance.</param>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="methodName">Specifies a method name.</param>
        public MethodTrace(TraceElement parentElement, string className, string methodName) : base(parentElement, className, methodName)
        {
            m_description = BuildDescription(className, methodName);

            m_startLine = String.Format("Method Start: {0}.{1}", className, methodName);

            m_endLine = String.Format("Method End: {0}.{1}", className, methodName);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds a description for an instance of the MethodTrace class.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="methodName">Specifies a method name.</param>
        /// <returns>Returns the built description.</returns>
        public static string BuildDescription(string className, string methodName)
        {
            return(String.Format("Execution of the method {0}.{1}", className, methodName));
        }

        #endregion
    }
}
