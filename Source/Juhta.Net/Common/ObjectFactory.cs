
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml;

namespace Juhta.Net.Common
{
    /// <summary>
    /// A static class that provides services for creating instances of such classes whose type is not directly
    /// referencable in the current programming context. A typical scenario for using this class is to create instances
    /// of classes that are not available at the build time but are known to implement a specific interface.
    /// </summary>
    public static class ObjectFactory
    {
        #region Public Methods

        /// <summary>
        /// Creates an instance of a specified class by using the default constructor.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="assembly">Specifies an Assembly object.</param>
        /// <param name="className">Specifies an assembly class name. The value can begin with a '.' character in which
        /// case the root namespace for the class will be taken from the file name part of the Assembly object's
        /// Location property.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(Assembly assembly, string className)
        {
            return(CreateInstance<T>(assembly, className, null));
        }

        /// <summary>
        /// Creates an instance of a specified class.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="assembly">Specifies an Assembly object.</param>
        /// <param name="className">Specifies an assembly class name. The value can begin with a '.' character in which
        /// case the root namespace for the class will be taken from the file name part of the Assembly object's
        /// Location property.</param>
        /// <param name="parameters">Specifies an array of parameters that will be passed to the appropriate
        /// constructor. Can be null causing the default constructor to be called.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(Assembly assembly, string className, params object[] parameters)
        {
            if (className[0] == '.')
                className = Path.GetFileNameWithoutExtension(assembly.Location) + className;

            return((T)assembly.CreateInstance(className, false, BindingFlags.CreateInstance, null, parameters, null, null));
        }

        /// <summary>
        /// Creates an instance of a specified class by using the default constructor.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="assemblyClassUri">Specifies an AssemblyClassUri object.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(AssemblyClassUri assemblyClassUri)
        {
            return(CreateInstance<T>(assemblyClassUri, null));
        }

        /// <summary>
        /// Creates an instance of a specified class.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="assemblyClassUri">Specifies an AssemblyClassUri object.</param>
        /// <param name="parameters">Specifies an array of parameters that will be passed to the appropriate
        /// constructor. Can be null causing the default constructor to be called.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        /// <remarks>An exception will be thrown if the assembly referenced by <paramref name="assemblyClassUri"/> has
        /// not been downloaded to the local machine.</remarks>
        public static T CreateInstance<T>(AssemblyClassUri assemblyClassUri, params object[] parameters)
        {
            if (!assemblyClassUri.IsAssemblyDownloaded)
                throw new ArgumentException(LibraryMessages.Error030.FormatMessage(assemblyClassUri.OriginalString));

            return(CreateInstance<T>(assemblyClassUri.LocalAssemblyPath, assemblyClassUri.FullClassName, parameters));
        }

        /// <summary>
        /// Creates an instance of a specified class by using the default constructor.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="assemblyFile">Specifies an assembly file.</param>
        /// <param name="className">Specifies an assembly class. The value can begin with a '.' character in which case
        /// the root namespace for the class will be taken from the file name of the assembly.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(string assemblyFile, string className)
        {
            return(CreateInstance<T>(assemblyFile, className, null));
        }

        /// <summary>
        /// Creates an instance of a specified class.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="assemblyFile">Specifies an assembly file.</param>
        /// <param name="className">Specifies an assembly class. The value can begin with a '.' character in which case
        /// the root namespace for the class will be taken from the file name of the assembly.</param>
        /// <param name="parameters">Specifies an array of parameters that will be passed to the appropriate
        /// constructor. Can be null causing the default constructor to be called.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(string assemblyFile, string className, params object[] parameters)
        {
            ObjectHandle objectHandle;

            if (className[0] == '.')
                className = Path.GetFileNameWithoutExtension(assemblyFile) + className;

            objectHandle = Activator.CreateInstanceFrom(assemblyFile, className, false, BindingFlags.CreateInstance, null, parameters, null, null);

            return((T)(objectHandle.Unwrap()));
        }

        /// <summary>
        /// Creates an instance based on a specified object XML node.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="objectNode">Specifies an object XML node.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(XmlNode objectNode)
        {
            AssemblyClassUri assemblyClassUri = new AssemblyClassFileUri(objectNode.GetAttribute("assemblyClassFileUri"));

            if (objectNode.HasChildNodes)
                return(CreateInstance<T>(assemblyClassUri, objectNode.FirstChild.ToBuiltInTypeObjectArray()));
            else
                return(CreateInstance<T>(assemblyClassUri));
        }

        #endregion
    }
}
