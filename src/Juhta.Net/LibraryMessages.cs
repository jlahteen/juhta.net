
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;

namespace Juhta.Net
{
    /// <summary>
    /// A static class that defines the diagnostic messages for this library.
    /// </summary>
    internal static class LibraryMessages
    {
        #region Private Properties

        /// <summary>
        /// Gets the DiagnosticMessageFactory instance for creating diagnostic messages.
        /// </summary>
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.RootLibraryMessages, typeof(LibraryMessages).Namespace);

        #endregion

        #region Internal Properties

        /// <summary>
        /// Library '{0}' requires a configuration file '{1}' but there is no such file in the configuration directory
        /// '{2}'.
        /// </summary>
        internal static readonly ErrorMessage Error001 = MessageFactory.CreateErrorMessage("Library '{0}' requires a configuration file '{1}' but there is no such file in the configuration directory '{2}'.");

        /// <summary>
        /// XML configuration file '{0}' does not conform to the configuration schema(s) of the custom XML configurable
        /// library '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error002 = MessageFactory.CreateErrorMessage("XML configuration file '{0}' does not conform to the configuration schema(s) of the custom XML configurable library '{1}'.");

        /// <summary>
        /// Initialization of the library '{0}' failed.
        /// </summary>
        internal static readonly ErrorMessage Error003 = MessageFactory.CreateErrorMessage("Initialization of the library '{0}' failed.");

        /// <summary>
        /// An unexpected error occurred when the library '{0}' was being closed.
        /// </summary>
        internal static readonly ErrorMessage Error004 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the library '{0}' was being closed.");

        /// <summary>
        /// Library Manager detected changes in the configuration but failed to update the states of the associated
        /// dynamic libraries. The state of the process may be unstable. Please refer to the log events for more
        /// information.
        /// </summary>
        internal static readonly AlertMessage Alert005 = MessageFactory.CreateAlertMessage("Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information.");

        /// <summary>
        /// An error occurred when the application '{0}' was being started.
        /// </summary>
        internal static readonly ErrorMessage Error006 = MessageFactory.CreateErrorMessage("An error occurred when the application '{0}' was being started.");

        /// <summary>
        /// Application '{0}' failed to start. Please refer to the log events for more information.
        /// </summary>
        internal static readonly AlertMessage Alert007 = MessageFactory.CreateAlertMessage("Application '{0}' failed to start. Please refer to the log events for more information.");

        /// <summary>
        /// Dynamic library context could not be created because no dynamic library was found with the type '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error008 = MessageFactory.CreateErrorMessage("Dynamic library context could not be created because no dynamic library was found with the type '{0}'.");

        /// <summary>
        /// Type of the configuration file '{0}' is not supported. Supported configuration file types are .json, .xml,
        /// .config and .ini.
        /// </summary>
        internal static readonly ErrorMessage Error009 = MessageFactory.CreateErrorMessage("Type of the configuration file '{0}' is not supported. Supported configuration file types are .json, .xml, .config and .ini.");

        /// <summary>
        /// Library Manager detected that it is unable to continue monitoring configuration file changes in the
        /// configuration directory '{0}'. Therefore, from now on no configuration changes will come into effect on the
        /// fly in the process.
        /// </summary>
        internal static readonly WarningMessage Warning010 = MessageFactory.CreateWarningMessage("Library Manager detected that it is unable to continue monitoring configuration file changes in the configuration directory '{0}'. Therefore, from now on no configuration changes will come into effect on the fly in the process.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but no actions were performed
        /// because there were no dynamic libraries associated with this configuration file.
        /// </summary>
        internal static readonly WarningMessage Warning011 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was deleted, but no actions were performed because there were no dynamic libraries associated with this configuration file.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, and the state of the associated
        /// dynamic library '{1}' was initialized successfully.
        /// </summary>
        internal static readonly InformationMessage Information012 = MessageFactory.CreateInformationMessage("Library Manager detected that the configuration file '{0}' was deleted, and the state of the associated dynamic library '{1}' was initialized successfully.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but an unexpected error occurred
        /// when the states of the associated dynamic libraries were being initialized. NOTE: The state of the process
        /// is currently unstable. You should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error013 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was deleted, but an unexpected error occurred when the states of the associated dynamic libraries were being initialized. NOTE: The state of the process is currently unstable. You should restore the configuration file and possibly restart the process.");

        /// <summary>
        /// Configuration file name cannot be null for the configurable library '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error014 = MessageFactory.CreateErrorMessage("Configuration file name cannot be null for the configurable library '{0}'.");

        /// <summary>
        /// Dependency injection service '{0}' has a duplicate definition in the configuration.
        /// </summary>
        internal static readonly ErrorMessage Error015 = MessageFactory.CreateErrorMessage("Dependency injection service '{0}' has a duplicate definition in the configuration.");

        /// <summary>
        /// No dependency injection service was found with the identifier '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error016 = MessageFactory.CreateErrorMessage("No dependency injection service was found with the identifier '{0}'.");

        /// <summary>
        /// At least one error occurred when the library '{0}' was closed. All resources and services of this library
        /// may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning017 = MessageFactory.CreateWarningMessage("At least one error occurred when the library '{0}' was closed. All resources and services of this library may not have been completely released or shutted down.");

        /// <summary>
        /// An unexpected error occurred when the application '{0}' was being closed.
        /// </summary>
        internal static readonly ErrorMessage Error018 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the application '{0}' was being closed.");

        /// <summary>
        /// An error occurred when a pending configuration file event was being created for the configuration file
        /// '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error019 = MessageFactory.CreateErrorMessage("An error occurred when a pending configuration file event was being created for the configuration file '{0}'.");

        /// <summary>
        /// Dynamic configuration changes related to the configuration file '{0}' will probably not come into effect.
        /// </summary>
        internal static readonly WarningMessage Warning020 = MessageFactory.CreateWarningMessage("Dynamic configuration changes related to the configuration file '{0}' will probably not come into effect.");

        /// <summary>
        /// An error occurred when a pending configuration file event related to the configuration file '{0}' was being
        /// raised.
        /// </summary>
        internal static readonly ErrorMessage Error021 = MessageFactory.CreateErrorMessage("An error occurred when a pending configuration file event related to the configuration file '{0}' was being raised.");

        /// <summary>
        /// Library '{0}' does not exist in the directory '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error022 = MessageFactory.CreateErrorMessage("Library '{0}' does not exist in the directory '{1}'.");

        /// <summary>
        /// Library '{0}' has already been initialized. This library exists at least twice under the libraries XML node
        /// in the root library configuration. Please remove duplicate occurrences.
        /// </summary>
        internal static readonly WarningMessage Warning023 = MessageFactory.CreateWarningMessage("Library '{0}' has already been initialized. This library exists at least twice under the libraries XML node in the root library configuration. Please remove duplicate occurrences.");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error024 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error025 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error026 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// Consumer thread cannot consume the specified object because the instance has not been started.
        /// </summary>
        internal static readonly ErrorMessage Error027 = MessageFactory.CreateErrorMessage("Consumer thread cannot consume the specified object because the instance has not been started.");

        /// <summary>
        /// Consumer thread cannot consume the specified object because the internal worker thread has stopped due to
        /// an unexpected error.
        /// </summary>
        internal static readonly ErrorMessage Error028 = MessageFactory.CreateErrorMessage("Consumer thread cannot consume the specified object because the internal worker thread has stopped due to an unexpected error.");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error029 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error030 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// Execution flow entered a block that is not implemented (hint: {0}).
        /// </summary>
        internal static readonly ErrorMessage Error031 = MessageFactory.CreateErrorMessage("Execution flow entered a block that is not implemented (hint: {0}).");

        /// <summary>
        /// XML document cannot be validated because the target namespace '{0}' is not present in the schema collection
        /// of the XML validator.
        /// </summary>
        internal static readonly ErrorMessage Error032 = MessageFactory.CreateErrorMessage("XML document cannot be validated because the target namespace '{0}' is not present in the schema collection of the XML validator.");

        /// <summary>
        /// Scheme of a class file URI must be 'file'.
        /// </summary>
        internal static readonly ErrorMessage Error033 = MessageFactory.CreateErrorMessage("Scheme of a class file URI must be 'file'.");

        /// <summary>
        /// Value '{0}' is not a valid class file URI because the fragment part is missing or empty.
        /// </summary>
        internal static readonly ErrorMessage Error034 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class file URI because the fragment part is missing or empty.");

        /// <summary>
        /// Value '{0}' is not a valid class file URI because it doesn't specify a '.DLL' file.
        /// </summary>
        internal static readonly ErrorMessage Error035 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class file URI because it doesn't specify a '.DLL' file.");

        /// <summary>
        /// Value '{0}' is not a valid class file URI because the fragment part doesn't specify a valid class name.
        /// </summary>
        internal static readonly ErrorMessage Error036 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class file URI because the fragment part doesn't specify a valid class name.");

        /// <summary>
        /// Argument name prefix and option prefix cannot be the same in the command line parser.
        /// </summary>
        internal static readonly ErrorMessage Error037 = MessageFactory.CreateErrorMessage("Argument name prefix and option prefix cannot be the same in the command line parser.");

        /// <summary>
        /// Value '{0}' is not a valid file path.
        /// </summary>
        internal static readonly ErrorMessage Error038 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid file path.");

        /// <summary>
        /// Value '{0}' is not a valid class file URI.
        /// </summary>
        internal static readonly ErrorMessage Error039 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid class file URI.");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error040 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error041 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// Value '{0}' is not a valid directory path.
        /// </summary>
        internal static readonly ErrorMessage Error042 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid directory path.");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error043 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error044 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// Command line argument value '{0}' cannot be converted to the type '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error045 = MessageFactory.CreateErrorMessage("Command line argument value '{0}' cannot be converted to the type '{1}'.");

        /// <summary>
        /// Execution flow entered a statement that is not implemented (hint: {0}).
        /// </summary>
        internal static readonly ErrorMessage Error046 = MessageFactory.CreateErrorMessage("Execution flow entered a statement that is not implemented (hint: {0}).");

        /// <summary>
        /// Command line argument value '{0}' is invalid according to a validator of the type '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error047 = MessageFactory.CreateErrorMessage("Command line argument value '{0}' is invalid according to a validator of the type '{1}'.");

        /// <summary>
        /// Class file URI must be a localhost file URI starting with 'file:///'.
        /// </summary>
        internal static readonly ErrorMessage Error048 = MessageFactory.CreateErrorMessage("Class file URI must be a localhost file URI starting with 'file:///'.");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error049 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error050 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// Regular expression pattern cannot be null.
        /// </summary>
        internal static readonly ErrorMessage Error051 = MessageFactory.CreateErrorMessage("Regular expression pattern cannot be null.");

        /// <summary>
        /// Value '{0}' is not a valid finnish social security number.
        /// </summary>
        internal static readonly ErrorMessage Error052 = MessageFactory.CreateErrorMessage("Value '{0}' is not a valid finnish social security number.");

        /// <summary>
        /// Value '{0}' is invalid according to the regular expression pattern '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error053 = MessageFactory.CreateErrorMessage("Value '{0}' is invalid according to the regular expression pattern '{1}'.");

        /// <summary>
        /// File '{0}' could not be locked within {1} milliseconds.
        /// </summary>
        internal static readonly ErrorMessage Error054 = MessageFactory.CreateErrorMessage("File '{0}' could not be locked within {1} milliseconds.");

        /// <summary>
        /// Range between positions {0} - {1} in the file '{2}' could not be locked within {3} milliseconds.
        /// </summary>
        internal static readonly ErrorMessage Error055 = MessageFactory.CreateErrorMessage("Range between positions {0} - {1} in the file '{2}' could not be locked within {3} milliseconds.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated
        /// dynamic library '{1}' could not be initialized. The state of the library was left unmodified.
        /// </summary>
        internal static readonly ErrorMessage Error056 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated dynamic library '{1}' could not be initialized. The state of the library was left unmodified.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated
        /// dynamic library '{1}' could not be initialized. NOTE: The state of the library is currently unstable. You
        /// should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error057 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated dynamic library '{1}' could not be initialized. NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.");

        /// <summary>
        /// Default library state could not be created because the library '{0}' does not support the required
        /// interface ('{1}').
        /// </summary>
        internal static readonly ErrorMessage Error058 = MessageFactory.CreateErrorMessage("Default library state could not be created because the library '{0}' does not support the required interface ('{1}').");

        /// <summary>
        /// Processes in the {0} state of the library '{1}' could not be completely stopped.
        /// </summary>
        internal static readonly ErrorMessage Error059 = MessageFactory.CreateErrorMessage("Processes in the {0} state of the library '{1}' could not be completely stopped.");

        /// <summary>
        /// Value '{0}' of the constructor parameter '{1}' is not a valid '{2}' parameter value.
        /// </summary>
        internal static readonly ErrorMessage Error060 = MessageFactory.CreateErrorMessage("Value '{0}' of the constructor parameter '{1}' is not a valid '{2}' parameter value.");

        /// <summary>
        /// Processes in the {0} state of the library '{1}' could not be started.
        /// </summary>
        internal static readonly ErrorMessage Error061 = MessageFactory.CreateErrorMessage("Processes in the {0} state of the library '{1}' could not be started.");

        /// <summary>
        /// Constructor parameter '{0}' could not be initialized.
        /// </summary>
        internal static readonly ErrorMessage Error062 = MessageFactory.CreateErrorMessage("Constructor parameter '{0}' could not be initialized.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but no actions were
        /// performed because there were no dynamic libraries associated with this configuration file.
        /// </summary>
        internal static readonly WarningMessage Warning063 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was created or changed, but no actions were performed because there were no dynamic libraries associated with this configuration file.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but the state of the
        /// associated dynamic library '{1}' could not be updated. The state of the library was left unmodified.
        /// </summary>
        internal static readonly ErrorMessage Error064 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was created or changed, but the state of the associated dynamic library '{1}' could not be updated. The state of the library was left unmodified.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, and the state of the
        /// associated dynamic library '{1}' was updated successfully.
        /// </summary>
        internal static readonly InformationMessage Information065 = MessageFactory.CreateInformationMessage("Library Manager detected that the configuration file '{0}' was created or changed, and the state of the associated dynamic library '{1}' was updated successfully.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but the state of the
        /// associated dynamic library '{1}' could not be updated. NOTE: The state of the library is currently
        /// unstable. You should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error066 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was created or changed, but the state of the associated dynamic library '{1}' could not be updated. NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but an unexpected error
        /// occurred when the states of the associated dynamic libraries were being updated. NOTE: The state of the
        /// process is currently unstable. You should restore the configuration file and possibly restart the process.
        /// </summary>
        internal static readonly ErrorMessage Error067 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was created or changed, but an unexpected error occurred when the states of the associated dynamic libraries were being updated. NOTE: The state of the process is currently unstable. You should restore the configuration file and possibly restart the process.");

        /// <summary>
        /// Library state cannot be created for the library '{0}', because the library doesn't implement any of the
        /// required dynamic library interfaces.
        /// </summary>
        internal static readonly ErrorMessage Error068 = MessageFactory.CreateErrorMessage("Library state cannot be created for the library '{0}', because the library doesn't implement any of the required dynamic library interfaces.");

        /// <summary>
        /// Library '{0}' cannot be initialized based on the configuration file '{1}', because the library doesn't
        /// implement any of the required configurable library interfaces.
        /// </summary>
        internal static readonly ErrorMessage Error069 = MessageFactory.CreateErrorMessage("Library '{0}' cannot be initialized based on the configuration file '{1}', because the library doesn't implement any of the required configurable library interfaces.");

        /// <summary>
        /// Processes of the library '{0}' could not be started.
        /// </summary>
        internal static readonly ErrorMessage Error070 = MessageFactory.CreateErrorMessage("Processes of the library '{0}' could not be started.");

        /// <summary>
        /// An unexpected error occurred when the processes of the library '{0}' were being stopped.
        /// </summary>
        internal static readonly ErrorMessage Error071 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the processes of the library '{0}' were being stopped.");

        /// <summary>
        /// At least one error occurred when the processes of the library '{0}' were being stopped. All resources and
        /// services of these processes may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning072 = MessageFactory.CreateWarningMessage("At least one error occurred when the processes of the library '{0}' were being stopped. All resources and services of these processes may not have been completely released or shutted down.");

        /// <summary>
        /// At least one error occurred when the processes in the {0} state of the library '{1}' were being stopped.
        /// All resources and services of these processes may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning073 = MessageFactory.CreateWarningMessage("At least one error occurred when the processes in the {0} state of the library '{1}' were being stopped. All resources and services of these processes may not have been completely released or shutted down.");

        /// <summary>
        /// Dependency injection service '{0}' could not be initialized.
        /// </summary>
        internal static readonly ErrorMessage Error074 = MessageFactory.CreateErrorMessage("Dependency injection service '{0}' could not be initialized.");

        /// <summary>
        /// An unexpected error occurred when the processes in the {0} state of the library '{1}' were being stopped.
        /// </summary>
        internal static readonly ErrorMessage Error075 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the processes in the {0} state of the library '{1}' were being stopped.");

        /// <summary>
        /// At least one error occurred when the {0} state of the library '{1}' was closed. All resources and services
        /// of this library may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning076 = MessageFactory.CreateWarningMessage("At least one error occurred when the {0} state of the library '{1}' was closed. All resources and services of this library may not have been completely released or shutted down.");

        /// <summary>
        /// An unexpected error occurred when the {0} state of the library '{1}' was being closed.
        /// </summary>
        internal static readonly ErrorMessage Error077 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the {0} state of the library '{1}' was being closed.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed but the state of the
        /// associated dynamic library '{1}' could not be updated. NOTE: The library continues running with the current
        /// state.
        /// </summary>
        internal static readonly WarningMessage Warning078 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was created or changed but the state of the associated dynamic library '{1}' could not be updated. NOTE: The library continues running with the current state.");

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted but the state of the associated
        /// dynamic library '{1}' could not be initialized. NOTE: The library continues running with the current state.
        /// </summary>
        internal static readonly WarningMessage Warning079 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was deleted but the state of the associated dynamic library '{1}' could not be initialized. NOTE: The library continues running with the current state.");

        /// <summary>
        /// An instance of the dependency injection service '{0}' could not be created.
        /// </summary>
        internal static readonly ErrorMessage Error080 = MessageFactory.CreateErrorMessage("An instance of the dependency injection service '{0}' could not be created.");

        /// <summary>
        /// No dependency injection service was found with the type '{0}'.
        /// </summary>
        internal static readonly ErrorMessage Error081 = MessageFactory.CreateErrorMessage("No dependency injection service was found with the type '{0}'.");

        /// <summary>
        /// XML document is not valid according to the given schema(s).
        /// </summary>
        internal static readonly ErrorMessage Error082 = MessageFactory.CreateErrorMessage("XML document is not valid according to the given schema(s).");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error083 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// Pure option prefix '{0}' was found in the command line arguments. An option name is expected immediately
        /// after an option prefix.
        /// </summary>
        internal static readonly ErrorMessage Error084 = MessageFactory.CreateErrorMessage("Pure option prefix '{0}' was found in the command line arguments. An option name is expected immediately after an option prefix.");

        /// <summary>
        /// Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is
        /// given but the name part is missing.
        /// </summary>
        internal static readonly ErrorMessage Error085 = MessageFactory.CreateErrorMessage("Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is given but the name part is missing.");

        /// <summary>
        /// Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is
        /// given but the value part is missing.
        /// </summary>
        internal static readonly ErrorMessage Error086 = MessageFactory.CreateErrorMessage("Cannot create an instance of OptionArgument based on the value '{0}' because the name-value separator is given but the value part is missing.");

        /// <summary>
        /// Cannot create an instance of OptionArgument based on the value '{0}' because the name part is invalid
        /// according to the regex '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error087 = MessageFactory.CreateErrorMessage("Cannot create an instance of OptionArgument based on the value '{0}' because the name part is invalid according to the regex '{1}'.");

        /// <summary>
        /// This message is free to be redefined.
        /// </summary>
        internal static readonly ErrorMessage Error088 = MessageFactory.CreateErrorMessage("<undefined>");

        /// <summary>
        /// Pure argument name prefix '{0}' was found in the command line arguments. An argument name is expected
        /// immediately after an argument name prefix.
        /// </summary>
        internal static readonly ErrorMessage Error089 = MessageFactory.CreateErrorMessage("Pure argument name prefix '{0}' was found in the command line arguments. An argument name is expected immediately after an argument name prefix.");

        /// <summary>
        /// Cannot create an instance of NamedArgument because the argument name '{0}' is invalid according to the
        /// regex '{1}'.
        /// </summary>
        internal static readonly ErrorMessage Error090 = MessageFactory.CreateErrorMessage("Cannot create an instance of NamedArgument because the argument name '{0}' is invalid according to the regex '{1}'.");

        /// <summary>
        /// Option argument '{0}' is not specified in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error091 = MessageFactory.CreateErrorMessage("Option argument '{0}' is not specified in the command line arguments.");

        /// <summary>
        /// Command line argument cannot be null or an empty string.
        /// </summary>
        internal static readonly ErrorMessage Error092 = MessageFactory.CreateErrorMessage("Command line argument cannot be null or an empty string.");

        /// <summary>
        /// Named argument '{0}' is not specified in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error093 = MessageFactory.CreateErrorMessage("Named argument '{0}' is not specified in the command line arguments.");

        /// <summary>
        /// Argument name cannot be the last command line argument, it must be followed by an argument value.
        /// </summary>
        internal static readonly ErrorMessage Error094 = MessageFactory.CreateErrorMessage("Argument name cannot be the last command line argument, it must be followed by an argument value.");

        /// <summary>
        /// Plain argument #{0} is not specified in the command line arguments.
        /// </summary>
        internal static readonly ErrorMessage Error095 = MessageFactory.CreateErrorMessage("Plain argument #{0} is not specified in the command line arguments.");

        #endregion
    }
}
