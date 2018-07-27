
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// A static class that defines the diagnostic messages for this library.
    /// </summary>
    internal static class LibraryMessages
    {
        #region Private Properties

        /// <summary>
        /// Gets the <see cref="DiagnosticMessageFactory"/> instance for creating diagnostic messages.
        /// </summary>
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.LibraryManagementLibraryMessages, typeof(LibraryMessages).Namespace);

        #endregion

        #region Internal Properties

        /// <summary>
        /// Library '{0}' does not exist in the directory '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("Library '{0}' does not exist in the directory '{1}'.");

        #endregion
    }
}
