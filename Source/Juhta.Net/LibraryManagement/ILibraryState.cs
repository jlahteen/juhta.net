
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines a base interface for classes that represent the state of a library. A recommended design pattern is
    /// that a library state class is an aggregate class for the objects comprising the state of the library. These
    /// objects are typically reference-type or value-type objects that have been created based on the configuration of
    /// the library.
    /// </summary>
    public interface ILibraryState
    {}
}
