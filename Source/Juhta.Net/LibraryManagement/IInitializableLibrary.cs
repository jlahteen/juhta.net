
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an interface for initializable libraries. A library is initializable if it requires specific startup
    /// operations.
    /// </summary>
    public interface IInitializableLibrary
    {
        #region Methods

        /// <summary>
        /// Initializes this library, that is, performs required startup operations to make library services properly
        /// available.
        /// </summary>
        void Initialize();

        #endregion
    }
}
