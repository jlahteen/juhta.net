
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
    /// Defines an abstract base class for trace elements that store trace data related to a single execution of a
    /// class member.
    /// </summary>
    internal abstract class MemberTrace : TraceElement
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the class to which this MemberTrace instance relates.
        /// </summary>
        public string ClassName
        {
            get {return(m_className);}
        }

        /// <summary>
        /// Gets the name of the class member to which this MemberTrace instance relates.
        /// </summary>
        public string MemberName
        {
            get {return(m_memberName);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parentElement">Specifies a TraceElement object that will be set as the parent element for the
        /// instance.</param>
        /// <param name="className">Specifies the name of a class to which this MemberTrace instance relates.</param>
        /// <param name="memberName">Specifies the name of a class member to which this MemberTrace instance relates.</param>
        protected MemberTrace(TraceElement parentElement, string className, string memberName) : base(parentElement)
        {
            m_className = className;

            m_memberName = memberName;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ClassName"/> property.
        /// </summary>
        private string m_className;

        /// <summary>
        /// Stores the <see cref="MemberName"/> property.
        /// </summary>
        private string m_memberName;

        #endregion
    }
}
