
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;
using Juhta.Net.Framework;

namespace Juhta.Net.Services
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
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(FrameworkLibrary.GetMessageIdBase(FrameworkLibraryType.Services), typeof(LibraryMessages).Namespace);

        #endregion

        #region Internal Properties

        /// <summary>
        /// No dependency injection service was found with the identifier '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("No dependency injection service was found with the identifier '{0}'.");

        /// <summary>
        /// Constructor parameter '{0}' could not be initialized.
        /// </summary>
        internal static readonly ErrorMessage Error002 = MessageFactory.CreateErrorMessage("Constructor parameter '{0}' could not be initialized.");

        /// <summary>
        /// Value '{0}' of the constructor parameter '{1}' is not a valid '{2}' parameter value.
        /// </summary>
        internal static readonly ErrorMessage Error003 = MessageFactory.CreateErrorMessage("Value '{0}' of the constructor parameter '{1}' is not a valid '{2}' parameter value.");

        /// <summary>
        /// An instance of the dependency injection service '{0}' could not be created.
        /// </summary>
        internal static readonly ErrorMessage Error004 = MessageFactory.CreateErrorMessage("An instance of the dependency injection service '{0}' could not be created.");

        /// <summary>
        /// Dependency injection service '{0}' could not be initialized.
        /// </summary>
        internal static readonly ErrorMessage Error005 = MessageFactory.CreateErrorMessage("Dependency injection service '{0}' could not be initialized.");

        /// <summary>
        /// Dependency injection service '{0}' has a duplicate definition in the configuration.
        /// </summary>
        internal static readonly ErrorMessage Error006 = MessageFactory.CreateErrorMessage("Dependency injection service '{0}' has a duplicate definition in the configuration.");

        #endregion
    }
}
