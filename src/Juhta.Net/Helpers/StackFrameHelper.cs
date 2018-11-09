
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Juhta.Net.Extensions;
using System.IO;

namespace Juhta.Net.Helpers
{
    /// <summary>
    /// TODO
    /// </summary>
    public static class StackFrameHelper
    {
        #region Public Methods

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCallStack()
        {
            StackFrame[] stackFrames;
            StackTrace stackTrace = new StackTrace(0, true);
            MethodBase methodInfo;
            StringBuilder callStackLine = new StringBuilder();
            List<string> callStack = new List<string>();

            ParameterInfo[] parameterInfoArray;

            try
            {
                stackFrames = stackTrace.GetFrames();

                for (int i = 1; i < stackFrames.Length; i++)
                {
                    methodInfo = stackFrames[i].GetMethod();

                    callStackLine.Append("at ");

                    callStackLine.Append(methodInfo.DeclaringType.FullName + "." + stackFrames[i].GetMethod().Name);

                    if (methodInfo.IsGenericMethod)
                        AppendGenericTypes(callStackLine, methodInfo.GetGenericArguments());

                    // Append the parameters

                    callStackLine.Append("(");

                    parameterInfoArray = methodInfo.GetParameters();

                    for (int j = 0; j < parameterInfoArray.Length; j++)
                    {
                        // Append the parameter type

                        callStackLine.Append(parameterInfoArray[j].ParameterType.Namespace + ".");

                        callStackLine.Append(parameterInfoArray[j].ParameterType.Name);

                        // Append the generic types if necessary
                        AppendGenericTypes(callStackLine, parameterInfoArray[j].ParameterType.GenericTypeArguments);

                        // Append the parameter name
                        callStackLine.Append(" " + parameterInfoArray[j].Name);

                        // Append a comma if there are more parameters to come
                        if (j < parameterInfoArray.Length - 1)
                            callStackLine.Append(", ");
                    }

                    callStackLine.Append(")");

                    // Append the assembly file name
                    callStackLine.Append(" in " + methodInfo.DeclaringType.Assembly.GetFileName());

                    // Append the source reference if available

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
                callStack.Add(LibraryMessages.Error001.GetMessage());
            }

            return(callStack);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="callStackLine"></param>
        /// <param name="genericTypes"></param>
        private static void AppendGenericTypes(StringBuilder callStackLine, Type[] genericTypes)
        {
            if (genericTypes == null || genericTypes.Length == 0)
                return;

            callStackLine.Append("<");

            for (int j = 0; j < genericTypes.Length; j++)
            {
                callStackLine.Append(genericTypes[j].Namespace + ".");

                callStackLine.Append(genericTypes[j].Name);

                if (genericTypes[j].IsGenericType)
                    AppendGenericTypes(callStackLine, genericTypes[j].GetGenericArguments());

                if (j < genericTypes.Length - 1)
                    callStackLine.Append(", ");
            }

            callStackLine.Append(">");
        }

        #endregion
    }
}
