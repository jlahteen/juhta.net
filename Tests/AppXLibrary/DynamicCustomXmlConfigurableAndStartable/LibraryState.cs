
using Juhta.Net.LibraryManagement;
using System;
using System.Xml;
using Juhta.Net.Extensions;

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

            node = config.SelectSingleNode("/ns:settings/ns:replaceService", namespaceManager);

            if (node == null)
                this.ReplaceProcess = new ReplaceProcess();
            else
                this.ReplaceProcess = new ReplaceProcess(node.GetAttribute("search"), node.GetAttribute("replace"));
        }

        public void StartProcesses()
        {
            this.ReplaceProcess.Start();
        }

        public bool StopProcesses()
        {
            if (m_stopProcessessThrowException)
                throw new Exception("Processes could not be stopped.");

            this.ReplaceProcess.Stop();

            if (m_stopProcessesReturnValue)
                this.ReplaceProcess = null;

            return(m_stopProcessesReturnValue);
        }

        #endregion

        #region Public Properties

        public ReplaceProcess ReplaceProcess {get; set;}

        #endregion

        #region Private Fields

        private bool m_stopProcessesReturnValue;

        private bool m_stopProcessessThrowException;

        #endregion
    }
}
