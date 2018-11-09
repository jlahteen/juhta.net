
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;
using Juhta.Net.Framework;

namespace Juhta.Net
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
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.Core, typeof(LibraryMessages).Namespace);

        #endregion

        #region Internal Properties

        /// <summary>
        /// The call stack could not be listed.
        /// </summary>
        internal static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("The call stack could not be listed.");

        /// <summary>
        /// Separate library directory '{0}' cannot be specified to construct an instance of ClassId because the value
        /// '{1}' already contains a library directory part.
        /// </summary>
        internal static readonly ErrorMessage Error024 = MessageFactory.CreateErrorMessage("Separate library directory '{0}' cannot be specified to construct an instance of ClassId because the value '{1}' already contains a library directory part.", 24);

        /// <summary>
        /// Execution flow entered a block that is not implemented (hint: {0}).
        /// </summary>
        internal static readonly ErrorMessage Error031 = MessageFactory.CreateErrorMessage("Execution flow entered a block that is not implemented (hint: {0}).", 31);

        /// <summary>
        /// Value '{0}' is not a valid class identifier because the fragment part of the file URI is missing or empty.
        /// </summary>
        internal static readonly ErrorMessage Error034 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class identifier because the fragment part of the file URI is missing or empty.", 34);

        /// <summary>
        /// Value '{0}' is not a valid class identifier because the file URI doesn't specify a '.DLL' file.
        /// </summary>
        internal static readonly ErrorMessage Error035 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class identifier because the file URI doesn't specify a '.DLL' file.", 35);

        /// <summary>
        /// Value '{0}' is not a valid class identifier because the fragment part of the file URI doesn't specify a
        /// valid class name.
        /// </summary>
        internal static readonly ErrorMessage Error036 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class identifier because the fragment part of the file URI doesn't specify a valid class name.", 36);

        /// <summary>
        /// Value '{0}' is not a valid class identifier because its file path is invalid.
        /// </summary>
        internal static readonly ErrorMessage Error038 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class identifier because its file path is invalid.", 38);

        /// <summary>
        /// Execution flow entered a statement that is not implemented (hint: {0}).
        /// </summary>
        internal static readonly ErrorMessage Error046 = MessageFactory.CreateErrorMessage("Execution flow entered a statement that is not implemented (hint: {0}).", 46);

        /// <summary>
        /// Class identifier must be a localhost file URI.
        /// </summary>
        internal static readonly ErrorMessage Error048 = MessageFactory.CreateErrorMessage("Class identifier must be a localhost file URI.", 48);

        /// <summary>
        /// File '{0}' could not be locked within {1} milliseconds.
        /// </summary>
        internal static readonly ErrorMessage Error054 = MessageFactory.CreateErrorMessage("File '{0}' could not be locked within {1} milliseconds.", 54);

        /// <summary>
        /// Range between positions {0} - {1} in the file '{2}' could not be locked within {3} milliseconds.
        /// </summary>
        internal static readonly ErrorMessage Error055 = MessageFactory.CreateErrorMessage("Range between positions {0} - {1} in the file '{2}' could not be locked within {3} milliseconds.", 55);

        #endregion
    }
}
