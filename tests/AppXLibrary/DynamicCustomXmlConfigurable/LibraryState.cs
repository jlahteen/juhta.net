
using Juhta.Net.LibraryManagement;
using System.Xml;

namespace AppXLibrary.DynamicCustomXmlConfigurable
{
    public class LibraryState : ICustomXmlConfigurableLibraryState, IDefaultLibraryState
    {
        #region Public Methods

        public void Initialize()
        {
            this.CurrentGreeting = "Hello there, what's up?";
        }

        public void Initialize(XmlDocument config)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(config.NameTable);

            namespaceManager.AddNamespace("ns", "http://tempuri.org/Config.xsd");

            this.CurrentGreeting = config.SelectSingleNode("/ns:settings/ns:currentGreeting", namespaceManager).InnerText;
        }

        #endregion

        #region Public Properties

        public string CurrentGreeting {get; set;}

        #endregion
    }
}
