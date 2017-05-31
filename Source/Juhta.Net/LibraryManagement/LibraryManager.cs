
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// A class that represents a library manager. The library manager is responsible for managing the whole life cycle
    /// of libraries including initialization, dynamic configuration changes and closing.
    /// </summary>
    internal class LibraryManager
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public LibraryManager()
        {
            m_dynamicLibraries = new Dictionary<string, List<IDynamicLibrary>>();

            m_libraryHandles = new Stack<ILibraryHandle>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes all libraries.
        /// </summary>
        /// <remarks>
        /// <para>After this method has been called, libraries are no longer properly initialized.</para>
        /// <para>This method will be called by the framework manager when the framework is closed.</para>
        /// </remarks>
        public void CloseLibraries()
        {
            // Close all libraries one by one

            while (m_libraryHandles.Count > 0)
            {
                try
                {
                    CloseLibrary(m_libraryHandles.Pop());
                }

                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            // Clear all library collections stored by this class
            ClearLibraryCollections();
        }

        /// <summary>
        /// Initializes the libraries listed in a specified root configuration.
        /// </summary>
        /// <param name="rootConfig">Specifies an <see cref="XmlDocument"/> object containing the root configuration.</param>
        /// <param name="namespaceManager">Specifies an <see cref="XmlNamespaceManager"/> object for selecting nodes in
        /// <paramref name="rootConfig"/>.</param>
        public void InitializeLibraries(XmlDocument rootConfig, XmlNamespaceManager namespaceManager)
        {
            XmlNode librariesNode;
            ILibraryHandle libraryHandle;

            // Select the libraries XML node
            librariesNode = rootConfig.SelectSingleNode("//ns:libraries", namespaceManager);

            // Initialize the libraries listed in the libraries XML node

            foreach (XmlNode libraryNode in librariesNode)
            {
                libraryHandle = LibraryHandle.CreateInstance(libraryNode);

                InitializeLibrary(libraryHandle);
            }
        }

        /// <summary>
        /// Starts an asynchronous watching of configuration file changes.
        /// </summary>
        public void StartConfigFileWatching()
        {
            // Create a configuration file watcher
            m_configFileWatcher = new ConfigFileWatcher();

            // Subscribe to the ConfigFileCreated, ConfigFileChanged and ConfigFileDeleted events
            m_configFileWatcher.ConfigFileCreated += OnConfigFileChanged;
            m_configFileWatcher.ConfigFileChanged += OnConfigFileChanged;
            m_configFileWatcher.ConfigFileDeleted += OnConfigFileDeleted;

            // Start watching configuration file changes
            m_configFileWatcher.StartWatching(Startup.ConfigDirectory);
        }

        /// <summary>
        /// Stops the asynchronous watching of configuration file changes.
        /// </summary>
        public void StopConfigFileWatching()
        {
            // Return if there is no configuration file watcher
            if (m_configFileWatcher == null)
                return;

            // Stop watching configuration file changes
            m_configFileWatcher.StopWatching();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the configuration state of a specified dynamic library.
        /// </summary>
        /// <param name="library">Specifies an <see cref="IDynamicLibrary"/> instance.</param>
        /// <param name="config">Specifies an <see cref="XmlDocument"/> object containing a new configuration for the
        /// library. Can be null in which case the default configuration will be used.</param>
        /// <remarks>If <paramref name="config"/> is null, <paramref name="library"/> must implement the
        /// <see cref="IDynamicallyInitializableLibrary"/> interface, otherwise <see cref="IDynamicallyCustomXmlConfigurableLibrary"/>
        /// must be implemented.</remarks>
        private static void ChangeConfigState(IDynamicLibrary library, XmlDocument config)
        {
            IConfigState currentConfigState, newConfigState;

            // Lock the configuration state
            library.ConfigStateLock.EnterWriteLock();

            try
            {
                // Get the current configuration state
                currentConfigState = library.ConfigState;

                // Create a new configuration state
                if (config == null)
                    newConfigState = ((IDynamicallyInitializableLibrary)library).CreateDefaultConfigState();
                else
                    newConfigState = ((IDynamicallyCustomXmlConfigurableLibrary)library).CreateConfigState(config);

                // Initialize the new configuration state if necessary
                if (!newConfigState.IsInitialized)
                    newConfigState.Initialize();

                // Set the new configuration state

                try
                {
                    library.ConfigState = newConfigState;
                }

                catch (Exception ex)
                {
                    Logger.LogAlert(LibraryMessages.Alert015, ((ILibraryHandle)library).LibraryFileName, ex);
                }

                // Close the "current" configuration state that was replaced with the new configuration state

                try
                {
                    currentConfigState.Close();
                }

                catch (Exception ex)
                {
                    Logger.LogError(LibraryMessages.Error016, ((ILibraryHandle)library).LibraryFileName, ex);
                }
            }

            finally
            {
                // Unlock the configuration state
                library.ConfigStateLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Clears all library collections of the class.
        /// </summary>
        private void ClearLibraryCollections()
        {
            m_dynamicLibraries.Clear();

            m_libraryHandles.Clear();
        }

        /// <summary>
        /// Closes a specified library.
        /// </summary>
        /// <param name="libraryHandle">Specifies a library handle.</param>
        /// <remarks>Nothing will be done if the specified library is not closable.</remarks>
        private static void CloseLibrary(ILibraryHandle libraryHandle)
        {
            IClosableLibrary closableLibrary;

            try
            {
                closableLibrary = libraryHandle as IClosableLibrary;

                if (closableLibrary == null)
                    return;

                else if (!closableLibrary.CloseLibrary())
                    Logger.LogWarning(LibraryMessages.Warning017, libraryHandle.LibraryFileName);
            }

            catch (Exception ex)
            {
                throw new LibraryClosingException(LibraryMessages.Error004.FormatMessage(libraryHandle.LibraryFileName), ex);
            }
        }

        /// <summary>
        /// Initializes the library specified by a library handle.
        /// </summary>
        /// <param name="libraryHandle">Specifies a library handle.</param>
        private void InitializeLibrary(ILibraryHandle libraryHandle)
        {
            bool requiresConfigFile;
            ICustomXmlConfigurableLibrary customXmlConfigurableLibrary;
            string configFilePath;
            XmlDocument config;

            try
            {
                // Return immediately if the library is already initialized

                if (IsLibraryInitialized(libraryHandle))
                {
                    Logger.LogWarning(LibraryMessages.Warning023.FormatMessage(libraryHandle.LibraryFileName));

                    return;
                }

                // Push the library to the stack of library handles
                // Note: This is done before the initialization, because if the initialization process fails, the
                // closing process can clean up such initialization actions that were successfully accomplished prior
                // to the error in the initialization
                m_libraryHandles.Push(libraryHandle);

                // Initialize the library if necessary

                if (libraryHandle is IInitializableLibrary || libraryHandle is ICustomXmlConfigurableLibrary)
                {
                    // Check if the library requires a configuration file
                    requiresConfigFile = !(libraryHandle is IInitializableLibrary);

                    if (libraryHandle is ICustomXmlConfigurableLibrary)
                    {
                        // The library is custom XML configurable

                        customXmlConfigurableLibrary = (ICustomXmlConfigurableLibrary)libraryHandle;

                        configFilePath = Startup.ConfigDirectory + Path.DirectorySeparatorChar + customXmlConfigurableLibrary.ConfigFileName;

                        if (File.Exists(configFilePath))
                        {
                            // A configuration file exists for the library

                            // Load and validate the configuration file
                            config = LoadAndValidateConfigFile(customXmlConfigurableLibrary, configFilePath);

                            // Initialize the library
                            customXmlConfigurableLibrary.InitializeLibrary(config);
                        }

                        else if (requiresConfigFile)
                            // The library requires a configuration file but such doesn't exist
                            throw new ConfigException(LibraryMessages.Error001.FormatMessage(libraryHandle.LibraryFileName, customXmlConfigurableLibrary.ConfigFileName, Startup.ConfigDirectory));

                        else
                            // There is no configuration file but the library is also initializable, initialize it
                            ((IInitializableLibrary)libraryHandle).InitializeLibrary();

                        // Add the library to the list of dynamically configurable libraries if necessary
                        if (libraryHandle is IDynamicallyCustomXmlConfigurableLibrary)
                            m_dynamicallyConfigurableLibraries.Add(customXmlConfigurableLibrary.ConfigFileName, (IDynamicallyCustomXmlConfigurableLibrary)libraryHandle);

                        // Add the library to the list of dynamically initializable libraries if necessary
                        if (libraryHandle is IDynamicallyInitializableLibrary)
                            m_dynamicallyInitializableLibraries.Add(customXmlConfigurableLibrary.ConfigFileName, (IDynamicallyInitializableLibrary)libraryHandle);
                    }
                    else
                        // The library is only initializable, initialize it
                        ((IInitializableLibrary)libraryHandle).InitializeLibrary();
                }
            }

            catch (Exception ex)
            {
                throw new LibraryInitializationException(LibraryMessages.Error003.FormatMessage(libraryHandle.LibraryFileName), ex);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="library"></param>
        private void InitializeLibraryState(IDynamicInitializableLibrary library)
        {
            ILibraryState newlibraryState, currentLibraryState;

            newlibraryState = library.CreateDefaultLibraryState();

            newlibraryState.Initialize();

            library.LibraryStateLock.EnterWriteLock();

            currentLibraryState = library.LibraryState;

            if (library is IDynamicStartableProcessesLibrary)
                ((IDynamicStartableProcessesLibrary)library).StopProcesses(currentLibraryState);

            currentLibraryState.Close();

            if (library is IDynamicStartableProcessesLibrary)
                ((IDynamicStartableProcessesLibrary)library).StartProcesses(newlibraryState);

            library.LibraryState = newlibraryState;

            library.LibraryStateLock.ExitWriteLock();
        }

        /// <summary>
        /// Checks whether the library specified by a library handle is already initialized.
        /// </summary>
        /// <param name="libraryHandle">Specifies a library handle.</param>
        /// <returns>Returns true if the library specified by the library handle is already initialized, otherwise
        /// false.</returns>
        private bool IsLibraryInitialized(ILibraryHandle libraryHandle)
        {
            string libraryFileName = libraryHandle.LibraryFileName.ToLower();

            foreach (ILibraryHandle libraryHandle2 in m_libraryHandles)
                if (libraryHandle2.LibraryFileName.ToLower() == libraryFileName)
                    return(true);

            return(false);
        }

        /// <summary>
        /// Loads and validates a configuration file againts the XML schema(s) of a custom XML configurable library.
        /// </summary>
        /// <param name="library">Specifies an <see cref="ICustomXmlConfigurableLibrary"/> object.</param>
        /// <param name="configFile">Specifies the name or path of a configuration file.</param>
        /// <returns>Returns an <see cref="XmlDocument"/> object containing the validated configuration.</returns>
        private static XmlDocument LoadAndValidateConfigFile(ICustomXmlConfigurableLibrary library, string configFile)
        {
            XmlDocument config;
            XmlSchema[] configSchemas;
            XmlValidator validator;

            config = new XmlDocument();

            config.Load(configFile);

            if ((configSchemas = library.GetConfigSchemas()) == null)
                return(config);

            try
            {
                validator = new XmlValidator();

                foreach (XmlSchema schema in configSchemas)
                    validator.AddSchema(schema);

                validator.Validate(config);

                return(config);
            }

            catch (XmlSchemaValidationException ex)
            {
                throw new InvalidConfigFileException(LibraryMessages.Error002.FormatMessage(configFile, ((ILibraryHandle)library).LibraryFileName), ex);
            }
        }

        /// <summary>
        /// Defines a event handler that will be fired when a configuration file is changed or created.
        /// </summary>
        /// <param name="source">Specifies an event source.</param>
        /// <param name="e">Specifies event arguments.</param>
        private void OnConfigFileChanged(object source, FileSystemEventArgs e)
        {
            XmlDocument config;
            IDynamicallyCustomXmlConfigurableLibrary library;

            try
            {
                // Check that there is a dynamically configurable library corresponding to the changed configuration
                // file

                if (!m_dynamicallyConfigurableLibraries.TryGetValue(e.Name, out library))
                {
                    if (e.ChangeType == WatcherChangeTypes.Changed)
                        Logger.LogEvent(LibraryMessages.Warning005, e.FullPath);
                    else
                        Logger.LogEvent(LibraryMessages.Warning008, e.FullPath);

                    return;
                }

                // Load and validate the configuration file
                config = LoadAndValidateConfigFile(library, e.FullPath);

                // Change the configuration state of the library
                ChangeConfigState(library, config);

                if (e.ChangeType == WatcherChangeTypes.Changed)
                    Logger.LogEvent(LibraryMessages.Information006, e.FullPath);
                else
                    Logger.LogEvent(LibraryMessages.Information009, e.FullPath);
            }

            catch (Exception ex)
            {
                if (e.ChangeType == WatcherChangeTypes.Changed)
                    Logger.LogEvent(LibraryMessages.Error007, e.FullPath, ex);
                else
                    Logger.LogEvent(LibraryMessages.Error010, e.FullPath, ex);
            }
        }

        /// <summary>
        /// Defines a event handler that will be fired when a configuration file is deleted.
        /// </summary>
        /// <param name="source">Specifies an event source.</param>
        /// <param name="e">Specifies event arguments.</param>
        private void OnConfigFileDeleted(object source, FileSystemEventArgs e)
        {
            IDynamicallyInitializableLibrary library;

            try
            {
                // Check that there is a dynamically initializable library corresponding to the deleted configuration
                // file

                if (!m_dynamicallyInitializableLibraries.TryGetValue(e.Name, out library))
                {
                    Logger.LogWarning(LibraryMessages.Warning011, e.FullPath);

                    return;
                }

                // Change the configuration state of the library
                ChangeConfigState(library, null);

                Logger.LogInformation(LibraryMessages.Information012, e.FullPath);
            }

            catch (Exception ex)
            {
                Logger.LogError(LibraryMessages.Error013, e.FullPath, ex);
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// A ConfigFileWatcher object that watches configuration file changes in the configuration directory.
        /// </summary>
        private ConfigFileWatcher m_configFileWatcher;

        /// <summary>
        /// Specifies the collection of the dynamic libraries with configuration file names as key values.
        /// </summary>
        private Dictionary<string, List<IDynamicLibrary>> m_dynamicLibraries;

        /// <summary>
        /// Specifies the stack of the initialized libraries.
        /// </summary>
        private Stack<ILibraryHandle> m_libraryHandles;

        #endregion
    }
}
