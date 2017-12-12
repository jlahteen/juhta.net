
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines a class that watches configuration file changes. This class raises an appropriate event every time a
    /// configuration file is created, updated or deleted in the configuration directory.
    /// </summary>
    /// <remarks>
    /// <para>The implementation relies on the <see cref="FileSystemWatcher"/> class. The reason why this class isn't
    /// used as such is the fact that it raises multiple events per a single file system operation causing unnecessary
    /// configuration updates to take place.</para>
    /// <para>This class queues all file events caused by a single file system operation, which is done by waiting for
    /// a certain time period for all these events to occur. After that the class determines which one of the three
    /// events, <see cref="ConfigFileCreated"/>, <see cref="ConfigFileChanged"/> or <see cref="ConfigFileDeleted"/>,
    /// has actually occurred. Finally, that event will be raised for outside subscribers.</para>
    /// </remarks>
    internal class ConfigFileWatcher
    {
        #region Public Methods

        /// <summary>
        /// Starts an asynchronous watching of configuration file changes.
        /// </summary>
        /// <param name="configDirectory">Specifies a configuration directory to watch.</param>
        public void StartWatching(string configDirectory)
        {
            // Create a synchronization object
            m_syncLock = new object();

            // Create a list for configuration file events
            m_configFileEvents = new Dictionary<string, FileSystemEventArgs>();

            // Create a list for timers
            m_timers = new Dictionary<string, Timer>();

            // Create a configuration file watcher
            m_configFileWatcher = new FileSystemWatcher(configDirectory, "*.config");

            // Subscribe to the Created, Changed and Deleted events
            m_configFileWatcher.Created += OnConfigFileEvent;
            m_configFileWatcher.Changed += OnConfigFileEvent;
            m_configFileWatcher.Deleted += OnConfigFileEvent;

            // Enable the configuration file watcher
            m_configFileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Stops the asynchronous watching of configuration file changes.
        /// </summary>
        public void StopWatching()
        {
            // Return if there is no synchronization object
            if (m_syncLock == null)
                return;

            lock(m_syncLock)
            {
                if (m_configFileWatcher != null)
                {
                    // Disable the configuration file watcher
                    m_configFileWatcher.EnableRaisingEvents = false;

                    // Release the configuration file watcher
                    m_configFileWatcher.Dispose();
                    m_configFileWatcher = null;
                }

                // Release the collection of pending configuration file events
                m_configFileEvents = null;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when a configuration file is changed in the configuration directory associated with this
        /// ConfigFileWatcher instance.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> ConfigFileChanged;

        /// <summary>
        /// Occurs when a configuration file is created in the configuration directory associated with this
        /// ConfigFileWatcher instance.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> ConfigFileCreated;

        /// <summary>
        /// Occurs when a configuration file is deleted in the configuration directory associated with this
        /// ConfigFileWatcher instance.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> ConfigFileDeleted;

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the file system events raised by an internal <see cref="FileSystemWatcher"/> object.
        /// </summary>
        /// <param name="source">Specifies the source of the event.</param>
        /// <param name="e">Specifies the event data.</param>
        /// <remarks>This method does nothing if the watching has been stopped.</remarks>
        private void OnConfigFileEvent(object source, FileSystemEventArgs e)
        {
            Timer timer;

            try
            {
                lock(m_syncLock)
                {
                    // Return if the watching has been stopped
                    if (m_configFileEvents == null)
                        return;

                    if (!m_configFileEvents.ContainsKey(e.Name))
                    {
                        // There is no pending event for the configuration file

                        // Add the event to the list of pending configuration file events
                        m_configFileEvents.Add(e.Name, e);

                        // Create a timer to raise the pending configuration file event
                        timer = new Timer(RaiseConfigFileEvent, e.Name, c_waitTimePeriod, Timeout.Infinite);

                        // Add the timer to the list of timers
                        m_timers.Add(e.Name, timer);
                    }
                    else if (e.ChangeType == WatcherChangeTypes.Created || e.ChangeType == WatcherChangeTypes.Deleted)
                    {
                        // The event type is Created or Deleted, override the pending event for the configuration file

                        m_configFileEvents.Remove(e.Name);

                        m_configFileEvents.Add(e.Name, e);
                    }
                }
            }

            catch (Exception ex)
            {
                // An error occurred when a pending configuration file event was being created

                Logger.LogError(ex, LibraryMessages.Error019, e.Name);

                Logger.LogWarning(LibraryMessages.Warning020, e.Name);
            }
        }

        /// <summary>
        /// Raises the pending configuration file event for a specified configuration file.
        /// </summary>
        /// <param name="configFileName">Specifies a configuration file name.</param>
        /// <remarks>This method does nothing if the watching has been stopped or there is no pending configuration
        /// file event for the specified configuration file.</remarks>
        private void RaiseConfigFileEvent(object configFileName)
        {
            string configFileNameAsString;
            FileSystemEventArgs eventArgs;

            try
            {
                configFileNameAsString = (string)configFileName;

                lock(m_syncLock)
                {
                    // Return if the watching has been stopped
                    if (m_configFileEvents == null)
                        return;

                    // Return if there is no pending configuration file event for the specified configuration file
                    if (!m_configFileEvents.TryGetValue(configFileNameAsString, out eventArgs))
                        return;

                    // Remove the pending configuration file event from the list
                    m_configFileEvents.Remove(configFileNameAsString);

                    // Raise the appropriate configuration file event

                    if (eventArgs.ChangeType == WatcherChangeTypes.Created)
                        this.ConfigFileCreated.RaiseEvent<FileSystemEventArgs>(this, eventArgs);

                    else if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
                        this.ConfigFileChanged.RaiseEvent<FileSystemEventArgs>(this, eventArgs);

                    else if (eventArgs.ChangeType == WatcherChangeTypes.Deleted)
                        this.ConfigFileDeleted.RaiseEvent<FileSystemEventArgs>(this, eventArgs);

                    // Dispose the timer that called this method
                    m_timers[configFileNameAsString].Dispose();

                    // Remove the timer from the list
                    m_timers.Remove(configFileNameAsString);
                }
            }

            catch (Exception ex)
            {
                // An error occurred when a pending configuration file event was being raised

                Logger.LogError(ex, LibraryMessages.Error021, configFileName);

                Logger.LogWarning(LibraryMessages.Warning020, configFileName);
            }
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the time period in milliseconds to wait for all file events to occur caused by a single file
        /// system operation.
        /// </summary>
        private const int c_waitTimePeriod = 1500;

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies a collection of pending configuration file events. The collection is indexed by the configuration
        /// file name.
        /// </summary>
        private Dictionary<string, FileSystemEventArgs> m_configFileEvents;

        /// <summary>
        /// Specifies a FileSystemWatcher object that watches configuration file changes in the configuration
        /// directory.
        /// </summary>
        private FileSystemWatcher m_configFileWatcher;

        /// <summary>
        /// Specifies a synchronization object to handle concurrent access to the fields of this class.
        /// </summary>
        private object m_syncLock;

        /// <summary>
        /// Specifies a collection of timers corresponding to pending configuration file events. The collection is
        /// indexed by the configuration file name.
        /// </summary>
        private Dictionary<string, Timer> m_timers;

        #endregion
    }
}
