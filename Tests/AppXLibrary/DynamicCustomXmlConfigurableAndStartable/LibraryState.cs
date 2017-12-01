
using Juhta.Net.LibraryManagement;
using System;
using System.Xml;

namespace AppXLibrary.DynamicCustomXmlConfigurableAndStartable
{
    public class LibraryState : ICustomXmlConfigurableLibraryState, IStartableLibraryState
    {
        #region Public Methods

        public void Initialize(XmlDocument config)
        {
            XmlNode node;

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(config.NameTable);

            namespaceManager.AddNamespace("ns", "http://tempuri.org/Config.xsd");

            node = config.SelectSingleNode("/ns:settings/ns:stopProcessesReturnValue", namespaceManager);

            if (node != null)
                m_stopProcessesReturnValue = Convert.ToBoolean(node.InnerText);
            else
                m_stopProcessesReturnValue = true;

            node = config.SelectSingleNode("/ns:settings/ns:stopProcessesThrowException", namespaceManager);

            if (node != null)
                m_stopProcessessThrowException = Convert.ToBoolean(node.InnerText);
            else
                m_stopProcessessThrowException = false;
        }

        public void StartProcesses()
        {
            this.LibraryProcess = new LibraryProcess();

            this.LibraryProcess.Start();
        }

        public bool StopProcesses()
        {
            if (m_stopProcessessThrowException)
                throw new Exception("Processes could not be stopped.");

            this.LibraryProcess.Stop();

            this.LibraryProcess = null;

            return(m_stopProcessesReturnValue);
        }

        #endregion

        #region Public Properties

        public LibraryProcess LibraryProcess {get; set;}

        #endregion

        #region Private Fields

        private bool m_stopProcessesReturnValue;

        private bool m_stopProcessessThrowException;

        #endregion
    }
}
