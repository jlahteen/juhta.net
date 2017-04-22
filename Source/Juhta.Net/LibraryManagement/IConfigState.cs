
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for classes that represent such state of a library that is determined by the configuration
    /// of the library. Typically, a configuration state of a library is a collection of reference-type or value-type
    /// objects created or set based on the configuration of the library.
    /// </summary>
    public interface IConfigState
    {
        #region Methods

        /// <summary>
        /// Closes the configuration state specified by this IConfigState instance.
        /// </summary>
        /// <remarks>Closing a configuration state means performing all necessary closing and cleanup actions on the
        /// objects comprising the configuration state.</remarks>
        void Close();

        /// <summary>
        /// Initializes the configuration state specified by this IConfigState instance.
        /// </summary>
        /// <remarks>Initializing a configuration state means performing all necessary actions on the objects
        /// comprising the configuration state such that the objects are ready to be set as the effective configuration
        /// state without any further operations.</remarks>
        void Initialize();

        #endregion

        #region Properties

        /// <summary>
        /// Returns true if the <see cref="Initialize"/> method has been called on this IConfigState instance,
        /// otherwise false.
        /// </summary>
        bool IsInitialized {get;}

        #endregion
    }
}
