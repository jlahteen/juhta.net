
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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net
{
    /// <summary>
    /// A class that represents an application built on the top of the framework. The class provides basic information
    /// about the application and methods for initializing and closing the application.
    /// </summary>
    public class Application : Singleton<Application>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>Log events will be written to the current user's temp directory, and the configuration files are
        /// assumed to locate in the binary directory.</remarks>
        public Application() : this(null, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <remarks>The configuration files are assumed to locate in the binary directory.</remarks>
        public Application(string logFilePath) : this(logFilePath, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <param name="configDirectory">Specifies a directory to search for configuration files. Can be null in which
        /// case the configuration files are assumed to locate in the binary directory.</param>
        public Application(string logFilePath, string configDirectory)
        {
            // Set the singleton instance
            SetSingletonInstance(this);

            // Create a logger instance
            Logger.SetLogger(new FileLogger(logFilePath));

            // Set the binary directory
            m_binDirectory = Assembly.GetExecutingAssembly().GetDirectory();

            // Use the binary directory as the configuration directory if necessary
            if (String.IsNullOrEmpty(configDirectory))
                configDirectory = m_binDirectory;

            // Set the configuration directory
            m_configDirectory = configDirectory.TrimEnd(Path.DirectorySeparatorChar);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes the application by closing all configured startup libraries and the core of the framework.
        /// </summary>
        public void Close()
        {
            // Check the state of the application
            if (m_state == State.Uninitialized)
                throw new InvalidOperationException(CommonMessages.Error012.FormatMessage("Close", typeof(Application)));

            try
            {
                if (m_libraryManager != null)
                {
                    // Stop watching configuration file changes
                    m_libraryManager.StopConfigFileWatching();

                    // Close the libraries
                    m_libraryManager.CloseLibraries();
                }

                // Clear the singleton instance
                ClearSingletonInstance();
            }

            catch (Exception ex)
            {
                // We don't expect exceptions but such occurred anyway
                Logger.LogError(ex, LibraryMessages.Error018, this.Name);
            }

            finally
            {
                // Update the state of the application
                m_state = State.Uninitialized;

                // Reset the rest of the static fields

                m_binDirectory = null;

                m_configDirectory = null;

                m_libraryManager = null;
            }
        }

        /// <summary>
        /// Closes the possibly created singleton <see cref="Application"/> instance.
        /// </summary>
        public static void CloseInstance()
        {
            if (Application.Instance == null)
                return;

            else if (Application.Instance.IsInitialized)
                Application.Instance.Close();

            else
                Application.Instance.ClearSingletonInstance();
        }

        /// <summary>
        /// Creates an instance of <see cref="DynamicLibraryContext{TDynamicLibrary, TLibraryState}"/> corresponding to
        /// specified dynamic library type and library state type.
        /// </summary>
        /// <typeparam name="TDynamicLibrary">Specifies a dynamic library type.</typeparam>
        /// <typeparam name="TLibraryState">Specifies a library state type.</typeparam>
        /// <returns>Returns the created <see cref="DynamicLibraryContext{TDynamicLibrary, TLibraryState}"/> instance.</returns>
        public DynamicLibraryContext<TDynamicLibrary, TLibraryState> CreateDynamicLibraryContext<TDynamicLibrary, TLibraryState>()
            where TDynamicLibrary : IDynamicLibrary
            where TLibraryState : ILibraryState
        {
            return(m_libraryManager.CreateDynamicLibraryContext<TDynamicLibrary, TLibraryState>());
        }

        /// <summary>
        /// Starts the application by initializing the core of the framework and all configured startup libraries.
        /// </summary>
        public void Start()
        {
            XmlDocument rootConfig;
            XmlNamespaceManager namespaceManager;

            // Check the current state of the application
            if (m_state > State.Uninitialized)
                throw new InvalidOperationException(CommonMessages.Error012.FormatMessage("Start", typeof(Application)));

            try
            {
                // Perform the initialization if necessary

                if ((rootConfig = LoadAndValidateRootConfig()) != null)
                {
                    // Create and initialize a namespace manager for the configuration

                    namespaceManager = new XmlNamespaceManager(rootConfig.NameTable);

                    namespaceManager.AddNamespace("ns", String.Format("{0}root-{1}.xsd", FrameworkConfig.RootXmlns, FrameworkConfig.RootConfigVersion));

                    // Initialize the name of the application
                    InitializeName(rootConfig, namespaceManager);

                    // Initialize the libraries

                    m_libraryManager = new LibraryManager(this);

                    m_libraryManager.InitializeLibraries(rootConfig, namespaceManager);

                    // Start watching configuration file changes
                    m_libraryManager.StartConfigFileWatching();
                }

                // Update the state of the application
                m_state = State.Initialized;
            }

            catch (Exception ex)
            {
                // Log the exception
                Logger.LogError(ex, LibraryMessages.Error006, this.Name);

                // Log an alert
                Logger.LogAlert(LibraryMessages.Alert007, this.Name);

                // Update the state of the application
                m_state = State.PartlyInitialized;

                // Rethrow the exception
                throw;
            }
        }

        /// <summary>
        /// Creates and starts a new singleton <see cref="Application"/> instance.
        /// </summary>
        /// <remarks>Log events will be written to the current user's temp directory, and the configuration files are
        /// assumed to locate in the binary directory.</remarks>
        public static void StartInstance()
        {
            StartInstance(null, null);
        }

        /// <summary>
        /// Creates and starts a new singleton <see cref="Application"/> instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <remarks>The configuration files are assumed to locate in the binary directory.</remarks>
        public static void StartInstance(string logFilePath)
        {
            StartInstance(logFilePath, null);
        }

        /// <summary>
        /// Creates and starts a new singleton <see cref="Application"/> instance.
        /// </summary>
        /// <param name="logFilePath">Specifies a log file path. Can be null in which case the log file will be written
        /// to the current user's temp directory. This default location will also be used if <paramref name="logFilePath"/>
        /// specifies an invalid log file.</param>
        /// <param name="configDirectory">Specifies a directory to search for configuration files. Can be null in which
        /// case the configuration files are assumed to locate in the binary directory.</param>
        public static void StartInstance(string logFilePath, string configDirectory)
        {
            Application application = new Application(logFilePath, configDirectory);

            application.Start();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the binary directory for the framework and application libraries. The return value is null if the
        /// application is not initialized.
        /// </summary>
        public string BinDirectory
        {
            get {return(m_binDirectory);}
        }

        /// <summary>
        /// Gets the configuration directory for the framework and application libraries. The return value is null if
        /// the application is not initialized.
        /// </summary>
        public string ConfigDirectory
        {
            get {return(m_configDirectory);}
        }

        /// <summary>
        /// Returns true if the application has been initialized, otherwise false.
        /// </summary>
        public bool IsInitialized
        {
            get {return(m_state > State.Uninitialized);}
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        public string Name
        {
            get
            {
                if (m_name == null)
                    m_name = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

                return(m_name);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the name of the application based on a specified configuration.
        /// </summary>
        /// <param name="rootConfig">Specifies an <see cref="XmlDocument"/> object containing the root configuration.</param>
        /// <param name="namespaceManager">Specifies an <see cref="XmlNamespaceManager"/> object for selecting nodes in
        /// <paramref name="rootConfig"/>.</param>
        private void InitializeName(XmlDocument rootConfig, XmlNamespaceManager namespaceManager)
        {
            XmlNode applicationNode = rootConfig.SelectSingleNode("//ns:application", namespaceManager);

            if (applicationNode.HasAttribute("name"))
                m_name = applicationNode.GetAttribute("name");
            else
                m_name = this.Name;
        }

        /// <summary>
        /// Loads and validates the root configuration file.
        /// </summary>
        /// <returns>Returns an <see cref="XmlDocument"/> object containing the root configuration. If there is no root
        /// configuration file, the return value is null.</returns>
        private XmlDocument LoadAndValidateRootConfig()
        {
            string configFilePath;
            XmlDocument config = null;
            XmlValidator validator;

            configFilePath = String.Format("{0}/{1}.config", m_configDirectory, FrameworkInfo.RootNamespace);

            if (!File.Exists(configFilePath))
                return(null);

            config = new XmlDocument();

            config.Load(configFilePath);

            validator = new XmlValidator();

            validator.AddSchema(Assembly.GetExecutingAssembly().LoadEmbeddedResourceFile(FrameworkConfig.RootConfigFileName, FrameworkConfig.RootConfigFileNamespace));

            validator.AddSchema(Assembly.GetExecutingAssembly().LoadEmbeddedResourceFile(FrameworkConfig.CommonConfigFileName, FrameworkConfig.CommonConfigFileNamespace));

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
        private enum State
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
        /// Stores the <see cref="BinDirectory"/> property.
        /// </summary>
        private string m_binDirectory;

        /// <summary>
        /// Stores the <see cref="ConfigDirectory"/> property.
        /// </summary>
        private string m_configDirectory;

        /// <summary>
        /// Specifies the <see cref="LibraryManager"/> instance that was created when the application was initialized.
        /// </summary>
        private LibraryManager m_libraryManager;

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        /// <summary>
        /// Specifies the current state of the application.
        /// </summary>
        private State m_state;

        #endregion
    }
}
