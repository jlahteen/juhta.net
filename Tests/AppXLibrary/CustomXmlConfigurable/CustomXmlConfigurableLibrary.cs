
using AppXLibrary.CustomXmlConfig;
using Juhta.Net.Common;
using Juhta.Net.LibraryManagement;
using System;
using System.Xml;
using System.Xml.Schema;

namespace AppXLibrary.CustomXmlConfigurable
{
    public class CustomXmlConfigurableLibrary : LibraryHandleBase, ICustomXmlConfigurableLibrary
    {
        #region Public Constructors

        public CustomXmlConfigurableLibrary() : base("AppXLibrary.dll")
        {}

        public CustomXmlConfigurableLibrary(string libraryFileName) : base(libraryFileName)
        {}

        #endregion

        #region Public Methods

        public XmlSchema[] GetConfigSchemas()
        {
            XmlSchema configSchema;

            configSchema = base.GetEmbeddedConfigSchema(System.Reflection.Assembly.GetExecutingAssembly(), "AppXLibrary.CustomXmlConfig", "Config.xsd");

            return(new XmlSchema[]{configSchema});
        }

        public virtual void InitializeLibrary(XmlDocument config)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(config.NameTable);
            XmlNode node;

            namespaceManager.AddNamespace("ns", "http://tempuri.org/Config.xsd");

            LibraryConfig.IntSetting = Convert.ToInt32(config.SelectSingleNode("/ns:settings/ns:intSetting", namespaceManager).InnerText);

            LibraryConfig.StringSetting = Convert.ToString(config.SelectSingleNode("/ns:settings/ns:stringSetting", namespaceManager).InnerText);

            node = config.SelectSingleNode("/ns:settings/ns:closeLibraryReturnValue", namespaceManager);

            if (node != null)
                LibraryConfig.CloseLibraryReturnValue = Convert.ToBoolean(node.InnerText);
            else
                LibraryConfig.CloseLibraryReturnValue = true;

            node = config.SelectSingleNode("/ns:settings/ns:closeLibraryThrowException", namespaceManager);

            if (node != null)
                LibraryConfig.CloseLibraryThrowException = Convert.ToBoolean(node.InnerText);
            else
                LibraryConfig.CloseLibraryThrowException = false;

            if (LibraryConfig.IntSetting == 1234567)
                throw new InvalidConfigValueException("IntSetting 1234567 is invalid. Please use any other integer value but not this one!");
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName => "AppXLibrary.config";

        #endregion
    }
}
