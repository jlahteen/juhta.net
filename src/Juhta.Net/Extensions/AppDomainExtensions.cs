
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using System.Reflection;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// A static class that contains extension methods for the <see cref="AppDomain"/> class.
    /// </summary>
    public static class AppDomainExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets a loaded assembly by an assembly file name.
        /// </summary>
        /// <param name="appDomain">Specifies the current AppDomain instance.</param>
        /// <param name="assemblyFileName">Specifies an assembly file name.</param>
        /// <returns>Returns the first loaded Assembly instance whose Location property's file name part matches the
        /// specified assembly file name, or null, if no match was found.</returns>
        public static Assembly GetAssemblyByFileName(this AppDomain appDomain, string assemblyFileName)
        {
            assemblyFileName = assemblyFileName.ToLower();

            foreach (Assembly assembly in appDomain.GetAssemblies())
                if (Path.GetFileName(assembly.Location).ToLower() == assemblyFileName)
                    return(assembly);

            return(null);
        }

        /// <summary>
        /// Gets a loaded assembly by an assembly path.
        /// </summary>
        /// <param name="appDomain">Specifies the current AppDomain instance.</param>
        /// <param name="assemblyPath">Specifies an assembly path.</param>
        /// <returns>Returns the first loaded Assembly instance whose Location property ends with the specified
        /// assembly path, or null, if no match was found.</returns>
        /// <remarks>The path specified by <paramref name="assemblyPath"/> does not necessarily have to be absolute.</remarks>
        public static Assembly GetAssemblyByPath(this AppDomain appDomain, string assemblyPath)
        {
            assemblyPath = assemblyPath.ToLower();

            foreach (Assembly assembly in appDomain.GetAssemblies())
                if (assembly.Location.ToLower().EndsWith(assemblyPath))
                    return(assembly);

            return(null);
        }

        /// <summary>
        /// Gets a loaded assembly by a type name.
        /// </summary>
        /// <param name="appDomain">Specifies the current AppDomain instance.</param>
        /// <param name="typeName">Specifies a full type name.</param>
        /// <returns>Returns the first loaded assembly that contains the specified type, or null, if the type was not
        /// found in any loaded assembly.</returns>
        public static Assembly GetAssemblyByTypeName(this AppDomain appDomain, string typeName)
        {
            foreach (Assembly assembly in appDomain.GetAssemblies())
                if (assembly.GetType(typeName) != null)
                    return(assembly);

            return(null);
        }

        #endregion
    }
}
