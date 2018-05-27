
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using System.Reflection;

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
        /// <param name="assembly">Specifies an <see cref="Assembly"/> object.</param>
        /// <param name="className">Specifies a class name. The value can begin with a '.' character in which case the
        /// root namespace for the class will be taken from the file name part of the <see cref="Assembly.Location"/>
        /// property.</param>
        /// <returns>Returns the created instance casted to the specified type.</returns>
        public static T CreateInstance<T>(Assembly assembly, string className)
        {
            return(CreateInstance<T>(assembly, className, null));
        }

        /// <summary>
        /// Creates an instance of a specified class.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="assembly">Specifies an <see cref="Assembly"/> object.</param>
        /// <param name="className">Specifies a class name. The value can begin with a '.' character in which case the
        /// root namespace for the class will be taken from the file name part of the <see cref="Assembly.Location"/>
        /// property.</param>
        /// <param name="parameters">Specifies an array of parameters that will be passed to the appropriate
        /// constructor. Can be null causing the default constructor to be called.</param>
        /// <returns>Returns the created instance casted to the specified type.</returns>
        public static T CreateInstance<T>(Assembly assembly, string className, params object[] parameters)
        {
            object @object;

            if (className[0] == '.')
                className = Path.GetFileNameWithoutExtension(assembly.Location) + className;

            @object = assembly.CreateInstance(className, false, BindingFlags.CreateInstance, null, parameters, null, null);

            if (@object == null)
                throw new ArgumentException(CommonMessages.Error017.FormatMessage(className, assembly.Location));

            return((T)@object);
        }

        /// <summary>
        /// Creates an instance of a specified class by using the default constructor.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="classId">Specifies a <see cref="ClassId"/> object.</param>
        /// <returns>Returns the created instance casted to the specified type.</returns>
        public static T CreateInstance<T>(ClassId classId)
        {
            return(CreateInstance<T>(classId, null));
        }

        /// <summary>
        /// Creates an instance of a specified class.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="classId">Specifies a <see cref="ClassId"/> object.</param>
        /// <param name="parameters">Specifies an array of parameters that will be passed to the appropriate
        /// constructor. Can be null causing the default constructor to be called.</param>
        /// <returns>Returns the created instance casted to the specified type.</returns>
        public static T CreateInstance<T>(ClassId classId, params object[] parameters)
        {
            return(CreateInstance<T>(classId.LibraryFilePath, classId.FullClassName, parameters));
        }

        /// <summary>
        /// Creates an instance of a specified class by using the default constructor.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="libraryFile">Specifies a library file. The value can have a directory part, either relative or
        /// absolute.</param>
        /// <param name="className">Specifies a class name. The value can begin with a '.' character in which case the
        /// root namespace for the class will be taken from the file name part of <paramref name="libraryFile"/>.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(string libraryFile, string className)
        {
            return(CreateInstance<T>(libraryFile, className, null));
        }

        /// <summary>
        /// Creates an instance of a specified class.
        /// </summary>
        /// <typeparam name="T">Specifies a type for the return value. An instance to create must be castable to this
        /// type.</typeparam>
        /// <param name="libraryFile">Specifies a library file. The value can have a directory part, either relative or
        /// absolute.</param>
        /// <param name="className">Specifies a class name. The value can begin with a '.' character in which case the
        /// root namespace for the class will be taken from the file name part of <paramref name="libraryFile"/>.</param>
        /// <param name="parameters">Specifies an array of parameters that will be passed to the appropriate
        /// constructor. Can be null causing the default constructor to be called.</param>
        /// <returns>Returns the created instance casted to the requested type.</returns>
        public static T CreateInstance<T>(string libraryFile, string className, params object[] parameters)
        {
            if (className[0] == '.')
                className = Path.GetFileNameWithoutExtension(libraryFile) + className;

            return(CreateInstance<T>(Assembly.LoadFrom(libraryFile), className, parameters));
        }

        #endregion
    }
}
