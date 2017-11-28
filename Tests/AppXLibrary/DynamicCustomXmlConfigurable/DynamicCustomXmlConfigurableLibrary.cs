
using Juhta.Net.LibraryManagement;
using System.Xml.Schema;

namespace AppXLibrary.DynamicCustomXmlConfigurable
{
    public class DynamicCustomXmlConfigurableLibrary : DynamicLibraryHandleBase, IDynamicCustomXmlConfigurableLibrary
    {
        #region Public Constructors

        public DynamicCustomXmlConfigurableLibrary() : this("AppXLibrary.dll")
        {}

        public DynamicCustomXmlConfigurableLibrary(string libraryFileName) : base(libraryFileName)
        {}

        #endregion

        #region Public Methods

        public XmlSchema[] GetConfigSchemas()
        {
            XmlSchema configSchema;

            configSchema = base.GetEmbeddedConfigSchema(System.Reflection.Assembly.GetExecutingAssembly(), "AppXLibrary.DynamicCustomXmlConfigurable", "Config.xsd");

            return(new XmlSchema[]{configSchema});
        }

        public ICustomXmlConfigurableLibraryState CreateCustomXmlConfigurableLibraryState()
        {
            return(new CustomXmlConfigurableLibraryState(this));
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName => "AppXLibrary.config";

        #endregion
    }
}
