
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System.Reflection;

namespace Juhta.Net
{
    /// <summary>
    /// A static class that provides read-only information about the framework.
    /// </summary>
    public static class FrameworkInfo
    {
        #region Public Properties

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
            get {return(typeof(Startup).Namespace);}
        }

        #endregion
    }
}
