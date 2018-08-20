
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines a base interface for configurable libraries. A library is configurable if it requires specific startup
    /// operations and those operations need configuration.
    /// </summary>
    public interface IConfigurableLibraryBase
    {
        #region Properties

        /// <summary>
        /// Gets the name of the configuration file.
        /// </summary>
        /// <remarks>This property can return null, but in this case the configuration file name must be specified in
        /// the core library's configuration (in the application node or in the corresponding library node).</remarks>
        string ConfigFileName {get;}

        #endregion
    }
}
