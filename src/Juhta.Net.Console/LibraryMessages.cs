
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;

namespace Juhta.Net.Console
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
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.ConsoleLibraryMessages, typeof(LibraryMessages).Namespace);

        #endregion

        #region Internal Properties

        /// <summary>
        /// Command line argument value '{0}' cannot be converted to the type '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("Command line argument value '{0}' cannot be converted to the type '{1}'.");

        /// <summary>
        /// Command line argument value '{0}' is invalid according to a validator of the type '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error002 = MessageFactory.CreateErrorMessage("Command line argument value '{0}' is invalid according to a validator of the type '{1}'.");

        /// <summary>
        /// Pure option prefix '{0}' was found in the command line arguments. An option name is expected immediately
        /// after an option prefix.
        /// </summary>
        internal static readonly ErrorMessage Error003 = MessageFactory.CreateErrorMessage("Pure option prefix '{0}' was found in the command line arguments. An option name is expected immediately after an option prefix.");

        /// <summary>
        /// Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is
        /// given but the name part is missing.
        /// </summary>
        internal static readonly ErrorMessage Error004 = MessageFactory.CreateErrorMessage("Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is given but the name part is missing.");

        /// <summary>
        /// Argument name prefix and option prefix cannot be the same in the command line parser.
        /// </summary>
        internal static readonly ErrorMessage Error005 = MessageFactory.CreateErrorMessage("Argument name prefix and option prefix cannot be the same in the command line parser.");

        /// <summary>
        /// Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is
        /// given but the value part is missing.
        /// </summary>
        internal static readonly ErrorMessage Error006 = MessageFactory.CreateErrorMessage("Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is given but the value part is missing.");

        /// <summary>
        /// Cannot create an instance of OptionArgument based on the value '{0}' because the name part is invalid
        /// according to the regex '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error007 = MessageFactory.CreateErrorMessage("Cannot create an instance of OptionArgument based on the value '{0}' because the name part is invalid according to the regex '{1}'.");

        /// <summary>
        /// Pure argument name prefix '{0}' was found in the command line arguments. An argument name is expected
        /// immediately after an argument name prefix.
        /// </summary>
        internal static readonly ErrorMessage Error008 = MessageFactory.CreateErrorMessage("Pure argument name prefix '{0}' was found in the command line arguments. An argument name is expected immediately after an argument name prefix.");

        /// <summary>
        /// Cannot create an instance of NamedArgument because the argument name '{0}' is invalid according to the
        /// regex '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error009 = MessageFactory.CreateErrorMessage("Cannot create an instance of NamedArgument because the argument name '{0}' is invalid according to the regex '{1}'.");

        /// <summary>
        /// Option argument '{0}' is not specified in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error010 = MessageFactory.CreateErrorMessage("Option argument '{0}' is not specified in the command line arguments.");

        /// <summary>
        /// Command line argument cannot be null or an empty string.
        /// </summary>
        internal static readonly ErrorMessage Error011 = MessageFactory.CreateErrorMessage("Command line argument cannot be null or an empty string.");

        /// <summary>
        /// Named argument '{0}' is not specified in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error012 = MessageFactory.CreateErrorMessage("Named argument '{0}' is not specified in the command line arguments.");

        /// <summary>
        /// Argument name cannot be the last command line argument, it must be followed by an argument value.
        /// </summary>
        internal static readonly ErrorMessage Error013 = MessageFactory.CreateErrorMessage("Argument name cannot be the last command line argument, it must be followed by an argument value.");

        /// <summary>
        /// Plain argument #{0} is not specified in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error014 = MessageFactory.CreateErrorMessage("Plain argument #{0} is not specified in the command line arguments.");

        #endregion
    }
}
