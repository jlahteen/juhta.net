
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System.Reflection;

namespace Juhta.Net.Framework
{
    /// <summary>
    /// A static class that provides read-only information about the framework.
    /// </summary>
    public static class FrameworkInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets the directory of the framework binaries.
        /// </summary>
        public static string BinDirectory
        {
            get {return(Assembly.GetExecutingAssembly().GetDirectory());}
        }

        /// <summary>
        /// Gets the copyright of the framework.
        /// </summary>
        public static string Copyright
        {
            get {return(Assembly.GetExecutingAssembly().GetCopyright());}
        }

        /// <summary>
        /// Gets the name of the framework.
        /// </summary>
        public static string FrameworkName
        {
            get {return(Assembly.GetExecutingAssembly().GetProduct());}
        }

        /// <summary>
        /// Gets the root namespace of the framework.
        /// </summary>
        public static string RootNamespace
        {
            get {return(typeof(LibraryMessages).Namespace);}
        }

        #endregion
    }
}
