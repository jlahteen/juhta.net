
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;

namespace Juhta.Net.Validation
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
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.ValidationLibraryMessages, typeof(LibraryMessages).Namespace);

        #endregion

        #region Internal Properties

        /// <summary>
        /// XML document cannot be validated because the target namespace '{0}' is not present in the schema collection
        /// of the XML validator.
        /// </summary>
        internal static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("XML document cannot be validated because the target namespace '{0}' is not present in the schema collection of the XML validator.");

        /// <summary>
        /// Value '{0}' is not a valid file path.
        /// </summary>
        internal static readonly ErrorMessage Error002 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid file path.");

        /// <summary>
        /// Value '{0}' is not a valid directory path.
        /// </summary>
        internal static readonly ErrorMessage Error003 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid directory path.");

        /// <summary>
        /// XML document is not valid according to the given schema(s).
        /// </summary>
        internal static readonly ErrorMessage Error004 = MessageFactory.CreateErrorMessage("XML document is not valid according to the given schema(s).");

        #endregion
    }
}
