
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Runtime.InteropServices;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a static class that provides information about the underlying operating system.
    /// </summary>
    public static class OperatingSystemInfo
    {
        #region Public Properties

        /// <summary>
        /// Returns true if the application is running on Linux, otherwise false.
        /// </summary>
        public static bool IsLinux
        {
            get {return(RuntimeInformation.IsOSPlatform(OSPlatform.Linux));}
        }

        /// <summary>
        /// Returns true if the application is running on MacOS, otherwise false.
        /// </summary>
        public static bool IsMacOS
        {
            get {return(RuntimeInformation.IsOSPlatform(OSPlatform.OSX));}
        }

        /// <summary>
        /// Returns true if the application is running on Windows, otherwise false.
        /// </summary>
        public static bool IsWindows
        {
            get {return(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));}
        }

        #endregion
    }
}
