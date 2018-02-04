
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Juhta.Net.Helpers
{
    /// <summary>
    /// Defines a static helper class that provides methods for simplifying common argument operations such as
    /// null-checking and value validations. These helper methods enable replacing of repetitive code blocks with
    /// one-liners.
    /// </summary>
    public static class ArgumentHelper
    {
        #region Public Methods

        /// <summary>
        /// Checks that a parameter value is not null.
        /// </summary>
        /// <param name="paramName">Specifies a parameter name.</param>
        /// <param name="paramValue">Specifies a parameter value.</param>
        public static void CheckNotNull(string paramName, object paramValue)
        {
            StackFrame caller = new StackFrame(1);

            if (paramValue == null)
                throw new ArgumentNullException(paramName, CommonMessages.Error001.FormatMessage(paramName, GetCallingMethod(caller)));
        }

        /// <summary>
        /// Checks that a parameter value conforms to a regex pattern.
        /// </summary>
        /// <param name="paramName">Specifies a parameter name.</param>
        /// <param name="paramValue">Specifies a parameter value.</param>
        /// <param name="regexPattern">Specifies a regex pattern.</param>
        public static void CheckValue(string paramName, string paramValue, string regexPattern)
        {
            StackFrame caller = new StackFrame(1);

            CheckNotNull(paramName, paramValue);

            if (!Regex.IsMatch(paramValue, regexPattern))
                throw new ArgumentException(CommonMessages.Error005.FormatMessage(paramName, GetCallingMethod(caller), paramValue, regexPattern));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the full method name related to a specified <see cref="StackFrame"/> object.
        /// </summary>
        /// <param name="stackFrame">Specifies a <see cref="StackFrame"/> object.</param>
        /// <returns>Returns the full method name related to the specified <see cref="StackFrame"/> object.</returns>
        /// <remarks>Full method name means a full type name plus a method name in this context.</remarks>
        private static string GetCallingMethod(StackFrame stackFrame)
        {
            MethodBase method = stackFrame.GetMethod();

            return(method.DeclaringType.FullName + "." + method.Name);
        }

        #endregion
    }
}
