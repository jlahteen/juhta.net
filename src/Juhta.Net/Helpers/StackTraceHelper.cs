
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Juhta.Net.Helpers
{
    /// <summary>
    /// Defines a static helper class that facilitates getting call stacks through <see cref="StackTrace"/>.
    /// </summary>
    public static class StackTraceHelper
    {
        #region Public Methods

        /// <summary>
        /// Gets the current call stack.
        /// </summary>
        /// <param name="skipFrames">Specifies the number of stack frames to skip.</param>
        /// <returns>Returns an array of string each of which representing one line in the call stack from top to
        /// bottom.</returns>
        /// <remarks>If you want to ignore the first line in the call stack caused by the call to this method, pass 1
        /// as a value of <paramref name="skipFrames"/>.</remarks>
        public static string[] GetCallStack(int skipFrames)
        {
            StackTrace stackTrace;
            StackFrame[] stackFrames;
            MethodBase methodInfo;
            StringBuilder callStackLine = new StringBuilder();
            ParameterInfo[] parameters;
            List<string> callStack = new List<string>();

            try
            {
                // Get the stack frames

                stackTrace = new StackTrace(skipFrames, true);

                stackFrames = stackTrace.GetFrames();

                for (int i = 0; i < stackFrames.Length; i++)
                {
                    callStackLine.Append("at ");

                    // Append the full method name

                    methodInfo = stackFrames[i].GetMethod();

                    callStackLine.Append(methodInfo.DeclaringType.FullName + "." + stackFrames[i].GetMethod().Name);

                    if (methodInfo.IsGenericMethod)
                        AppendGenericTypes(callStackLine, methodInfo.GetGenericArguments());

                    // Append the parameters

                    callStackLine.Append("(");

                    parameters = methodInfo.GetParameters();

                    for (int j = 0; j < parameters.Length; j++)
                    {
                        // Append the parameter type

                        callStackLine.Append(parameters[j].ParameterType.Namespace + ".");

                        callStackLine.Append(parameters[j].ParameterType.Name);

                        // Append the generic types if necessary
                        AppendGenericTypes(callStackLine, parameters[j].ParameterType.GenericTypeArguments);

                        // Append the parameter name
                        callStackLine.Append(" " + parameters[j].Name);

                        // Append a comma if there are more parameters to come
                        if (j < parameters.Length - 1)
                            callStackLine.Append(", ");
                    }

                    callStackLine.Append(")");

                    // Append the assembly file name
                    callStackLine.Append(" in " + methodInfo.DeclaringType.Assembly.GetFileName());

                    // Append the source code reference if available

                    if (stackFrames[i].GetFileName() != null)
                    {
                        callStackLine.Append(":" + Path.GetFileName(stackFrames[i].GetFileName()));

                        callStackLine.Append(":" + stackFrames[i].GetFileLineNumber());

                        callStackLine.Append(":" + stackFrames[i].GetFileColumnNumber());
                    }

                    // Add the line to the call stack

                    callStack.Add(callStackLine.ToString());

                    callStackLine.Clear();
                }
            }

            catch (Exception)
            {
                callStack.Add("<" + LibraryMessages.Error001.GetMessage() + ">");
            }

            return(callStack.ToArray());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Appends a list of generic types to a call stack line.
        /// </summary>
        /// <param name="callStackLine">Specifies a call stack line.</param>
        /// <param name="genericTypes">Specifies an array of generic types. Can be null or an empty array in which case
        /// the method returns immediately.</param>
        private static void AppendGenericTypes(StringBuilder callStackLine, Type[] genericTypes)
        {
            if (genericTypes == null || genericTypes.Length == 0)
                return;

            callStackLine.Append("<");

            for (int i = 0; i < genericTypes.Length; i++)
            {
                callStackLine.Append(genericTypes[i].Namespace + ".");

                callStackLine.Append(genericTypes[i].Name);

                if (genericTypes[i].IsGenericType)
                    AppendGenericTypes(callStackLine, genericTypes[i].GetGenericArguments());

                if (i < genericTypes.Length - 1)
                    callStackLine.Append(", ");
            }

            callStackLine.Append(">");
        }

        #endregion
    }
}
