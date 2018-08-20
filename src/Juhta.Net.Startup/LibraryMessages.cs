
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;
using Juhta.Net.Framework;

namespace Juhta.Net.Startup
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
        private static readonly DiagnosticMessageFactory MessageFactory = new DiagnosticMessageFactory(DiagnosticMessageIdBase.Startup, typeof(LibraryMessages).Namespace);

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

        // Note: Message identifiers 15 and 16 are free.

        /// <summary>
        /// At least one error occurred when the library '{0}' was closed. All resources and services of this library
        /// may not have been completely released or shutted down.
        /// </summary>
        internal static readonly WarningMessage Warning017 = MessageFactory.CreateWarningMessage("At least one error occurred when the library '{0}' was closed. All resources and services of this library may not have been completely released or shutted down.", 17);

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

        // Note: Message identifiers 24 - 55 are free.

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated
        /// dynamic library '{1}' could not be initialized. The state of the library was left unmodified.
        /// </summary>
        internal static readonly ErrorMessage Error056 = MessageFactory.CreateErrorMessage("Library Manager detected that the configuration file '{0}' was deleted, but the state of the associated dynamic library '{1}' could not be initialized. The state of the library was left unmodified.", 56);

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

        // Note: Message identifier 60 is free.

        /// <summary>
        /// Processes in the {0} state of the library '{1}' could not be started.
        /// </summary>
        internal static readonly ErrorMessage Error061 = MessageFactory.CreateErrorMessage("Processes in the {0} state of the library '{1}' could not be started.", 61);

        // Note: Message identifier 62 is free.

        /// <summary>
        /// Library Manager detected that the configuration file '{0}' was created or changed, but no actions were
        /// performed because there were no dynamic libraries associated with this configuration file.
        /// </summary>
        internal static readonly WarningMessage Warning063 = MessageFactory.CreateWarningMessage("Library Manager detected that the configuration file '{0}' was created or changed, but no actions were performed because there were no dynamic libraries associated with this configuration file.", 63);

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

        // Note: Message identifier 74 is free.

        /// <summary>
        /// An unexpected error occurred when the processes in the {0} state of the library '{1}' were being stopped.
        /// </summary>
        internal static readonly ErrorMessage Error075 = MessageFactory.CreateErrorMessage("An unexpected error occurred when the processes in the {0} state of the library '{1}' were being stopped.", 75);

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

        #endregion
    }
}
