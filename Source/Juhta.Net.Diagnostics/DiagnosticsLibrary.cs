
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// A class that represents Diagnostics Library, which is a built-in library.
    /// </summary>
    internal class DiagnosticsLibrary : BuiltInLibrary, IInitializableLibrary, IDynamicallyConfigurableLibrary, IDynamicallyInitializableLibrary, IClosableLibrary
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DiagnosticsLibrary() : base(ProductLibraryType.Diagnostics)
        {
            m_configStateFactory = new ConfigStateFactory();

            m_configStateLock = new ReaderWriterLockSlim();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IClosableLibrary.Close"/>.
        /// </summary>
        public bool Close()
        {
            int errors = 0;

            errors += Trace.Close();

            errors += EventLogger.Close();

            return(errors == 0);
        }

        /// <summary>
        /// See <see cref="IDynamicallyConfigurableLibrary.CreateConfigState"/>.
        /// </summary>
        public IConfigState CreateConfigState(XmlDocument config)
        {
            return(m_configStateFactory.CreateConfigState(config, false));
        }

        /// <summary>
        /// See <see cref="IDynamicallyInitializableLibrary.CreateDefaultConfigState"/>.
        /// </summary>
        public IConfigState CreateDefaultConfigState()
        {
            return(m_configStateFactory.CreateDefaultConfigState(false));
        }

        /// <summary>
        /// Gets the embedded configuration schema for Diagnostics Library.
        /// </summary>
        /// <returns>Returns the embedded configuration schema for Diagnostics Library as an XmlSchema object.</returns>
        public XmlSchema GetConfigSchema()
        {
            return(DiagnosticsLibrary.GetConfigSchema(ProductLibraryType.Diagnostics));
        }

        /// <summary>
        /// See <see cref="IConfigurableLibrary.GetConfigSchemas"/>.
        /// </summary>
        public XmlSchema[] GetConfigSchemas()
        {
            return(DiagnosticsLibrary.GetConfigSchemas(ProductLibraryType.Diagnostics));
        }

        /// <summary>
        /// See <see cref="IDynamicLibrary.GetConfigStateLock"/>.
        /// </summary>
        public ReaderWriterLockSlim GetConfigStateLock()
        {
            return(m_configStateLock);
        }

        /// <summary>
        /// See <see cref="IDynamicLibrary.GetCurrentConfigState"/>.
        /// </summary>
        public IConfigState GetCurrentConfigState()
        {
            ConfigState configState = new ConfigState();

            EventLogger.GetConfigState(configState);

            Trace.GetConfigState(configState);

            return(configState);
        }

        /// <summary>
        /// See <see cref="IInitializableLibrary.Initialize"/>.
        /// </summary>
        public void Initialize()
        {
            Initialize(null);
        }

        /// <summary>
        /// See <see cref="IConfigurableLibrary.Initialize"/>.
        /// </summary>
        public void Initialize(XmlDocument config)
        {
            ConfigState configState;

            Event.Initialize();

            if (config == null)
                configState = (ConfigState)CreateDefaultConfigState();
            else
                configState = (ConfigState)CreateConfigState(config);

            if (!configState.IsInitialized)
                configState.Initialize();

            EventLogger.Initialize(configState);

            Trace.Initialize(configState);
        }

        /// <summary>
        /// See <see cref="IDynamicLibrary.SetConfigState"/>.
        /// </summary>
        public void SetConfigState(IConfigState configState)
        {
            EventLogger.SetConfigState((ConfigState)configState);

            Trace.SetConfigState((ConfigState)configState);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// See <see cref="IConfigurableLibrary.ConfigFileName"/>.
        /// </summary>
        public string ConfigFileName
        {
            get {return(DiagnosticsLibrary.GetLibraryConfigFileName(ProductLibraryType.Diagnostics));}
        }

        /// <summary>
        /// Gets the singleton instance of the DiagnosticsLibrary class.
        /// </summary>
        public static DiagnosticsLibrary Instance
        {
            get {return(DiagnosticsLibrary.GetLibraryInstance<DiagnosticsLibrary>(ProductLibraryType.Diagnostics));}
        }

        #endregion
    }
}
