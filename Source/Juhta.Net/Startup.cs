
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using Juhta.Net.LibraryManagement;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net
{
    /// <summary>
    /// A static class that provides methods for initializing and closing the application.
    /// </summary>
    public static class Startup
    {
        #region Public Methods

        /// <summary>
        /// Closes the application by closing all initialized libraries.
        /// </summary>
        public static void CloseApp()
        {
            // Check the state of the application
            if (s_appState == AppState.Uninitialized)
                throw new InvalidOperationException(CommonMessages.Error012.FormatMessage("CloseApp", typeof(Startup)));

            try
            {
                if (s_libraryManager != null)
                {
                    // Stop watching configuration file changes
                    s_libraryManager.StopConfigFileWatching();

                    // Close the libraries
                    s_libraryManager.CloseLibraries();
                }
            }

            catch (Exception ex)
            {
                // We don't expect exceptions but such occurred anyway
                Logger.LogError(ex, LibraryMessages.Error018);
            }

            finally
            {
                // Update the state of the application
                s_appState = AppState.Uninitialized;

                // Reset the rest of the static fields

                s_binDirectory = null;

                s_configDirectory = null;

                s_libraryManager = null;
            }
        }

        /// <summary>
        /// Initializes the application by initializing all configured libraries.
        /// </summary>
        /// <remarks>Log events will be written to the current user's temp directory and the configuration files are
        /// assumed to locate in the binary directory.</remarks>
        public static void InitializeApp()
        {
            InitializeApp(null, null);
        }

        /// <summary>
        /// Initializes the application by initializing all configured libraries.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies somehow an invalid log file.</param>
        /// <remarks>The configuration files are assumed to locate in the binary directory.</remarks>
        public static void InitializeApp(string logFilePath)
        {
            InitializeApp(logFilePath, null);
        }

        /// <summary>
        /// Initializes the application by initializing all configured libraries.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies somehow an invalid log file.</param>
        /// <param name="configDirectory">Specifies a directory to search for the configuration files. Can be null in
        /// which case the configuration files are assumed to locate in the binary directory.</param>
        public static void InitializeApp(string logFilePath, string configDirectory)
        {
            XmlDocument config;
            XmlNamespaceManager namespaceManager;

            // Check the current state of the application
            if (s_appState > AppState.Uninitialized)
                throw new InvalidOperationException(CommonMessages.Error012.FormatMessage("InitializeApp", typeof(Startup)));

            try
            {
                // Create a logger instance
                Logger.SetLogger(new FileLogger(logFilePath));

                // Set the binary directory
                s_binDirectory = Assembly.GetExecutingAssembly().GetDirectory();

                // Use the binary directory as the configuration directory if necessary
                if (String.IsNullOrEmpty(configDirectory))
                    configDirectory = s_binDirectory;

                // Set the configuration directory
                s_configDirectory = configDirectory.TrimEnd(Path.DirectorySeparatorChar);

                // Perform the initialization if necessary

                if ((config = LoadAndValidateRootConfig()) != null)
                {
                    // Create and initialize a namespace manager for the configuration

                    namespaceManager = new XmlNamespaceManager(config.NameTable);

                    namespaceManager.AddNamespace("ns", String.Format("{0}root-{1}.xsd", ConfigSchemaInfo.RootXmlns, ConfigSchemaInfo.RootConfigVersion));

                    // Initialize the libraries

                    s_libraryManager = new LibraryManager();

                    s_libraryManager.InitializeLibraries(config, namespaceManager);

                    // Start watching configuration file changes
                    s_libraryManager.StartConfigFileWatching();
                }

                // Update the state of the application
                s_appState = AppState.Initialized;
            }

            catch (Exception ex)
            {
                // Log the exception
                Logger.LogError(ex, LibraryMessages.Error006);

                // Log an alert
                Logger.LogAlert(LibraryMessages.Alert007);

                // Update the state of the application
                s_appState = AppState.PartlyInitialized;

                // Rethrow the exception
                throw;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the binary directory for the framework and application libraries. The return value is null if the
        /// application is not initialized.
        /// </summary>
        public static string BinDirectory
        {
            get {return(s_binDirectory);}
        }

        /// <summary>
        /// Gets the configuration directory for the framework and application libraries. The return value is null if
        /// the application is not initialized.
        /// </summary>
        public static string ConfigDirectory
        {
            get {return(s_configDirectory);}
        }

        /// <summary>
        /// Returns true if the application has been initialized, otherwise false.
        /// </summary>
        public static bool IsAppInitialized
        {
            get {return(s_appState > AppState.Uninitialized);}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads and validates the root configuration file.
        /// </summary>
        /// <returns>Returns a <see cref="XmlDocument"/> object containing the root configuration. If there is no root
        /// configuration file, the return value is null.</returns>
        private static XmlDocument LoadAndValidateRootConfig()
        {
            string configFilePath;
            XmlDocument config = null;
            XmlValidator validator;

            configFilePath = String.Format("{0}/{1}.config", s_configDirectory, FrameworkInfo.RootNamespace);

            if (!File.Exists(configFilePath))
                return(null);

            config = new XmlDocument();

            config.Load(configFilePath);

            validator = new XmlValidator();

            validator.AddSchema(Assembly.GetExecutingAssembly().LoadEmbeddedResourceFile(ConfigSchemaInfo.RootConfigFileName, ConfigSchemaInfo.RootConfigFileNamespace));

            validator.AddSchema(Assembly.GetExecutingAssembly().LoadEmbeddedResourceFile(ConfigSchemaInfo.CommonConfigFileName, ConfigSchemaInfo.CommonConfigFileNamespace));

            try
            {
                validator.Validate(config);
            }

            catch (XmlSchemaValidationException ex)
            {
                throw new InvalidConfigFileException(LibraryMessages.Error002.FormatMessage(configFilePath, FrameworkInfo.RootNamespace + ".dll"), ex);
            }

            return(config);
        }

        #endregion

        #region Private Types

        /// <summary>
        /// Defines an enumeration for the states of the application.
        /// </summary>
        private enum AppState
        {
            /// <summary>
            /// The application is uninitialized.
            /// </summary>
            Uninitialized,

            /// <summary>
            /// The application is only partly initialized, that is, the initialization has not been completed due to
            /// an error.
            /// </summary>
            PartlyInitialized,

            /// <summary>
            /// The application has been successfully initialized.
            /// </summary>
            Initialized
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the current state of the application.
        /// </summary>
        private static AppState s_appState;

        /// <summary>
        /// Stores the <see cref="BinDirectory"/> property.
        /// </summary>
        private static string s_binDirectory;

        /// <summary>
        /// Stores the <see cref="ConfigDirectory"/> property.
        /// </summary>
        private static string s_configDirectory;

        /// <summary>
        /// Specifies the <see cref="LibraryManager"/> instance that was created when the application was initialized.
        /// </summary>
        private static LibraryManager s_libraryManager;

        #endregion
    }
}
