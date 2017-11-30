
using Juhta.Net.LibraryManagement;
using System.Xml.Schema;

namespace AppXLibrary.DynamicCustomXmlConfigurable
{
    public class LibraryHandle : DynamicLibraryHandleBase, IDynamicCustomXmlConfigurableLibrary
    {
        #region Public Constructors

        public LibraryHandle() : this("AppXLibrary.dll")
        {}

        public LibraryHandle(string libraryFileName) : base(libraryFileName)
        {}

        #endregion

        #region Public Methods

        public XmlSchema[] GetConfigSchemas()
        {
            XmlSchema configSchema;

            configSchema = base.GetEmbeddedConfigSchema(System.Reflection.Assembly.GetExecutingAssembly(), "AppXLibrary.DynamicCustomXmlConfigurable", "Config.xsd");

            return(new XmlSchema[]{configSchema});
        }

        public ICustomXmlConfigurableLibraryState CreateLibraryState()
        {
            return(new LibraryState());
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName => "AppXLibrary.config";

        #endregion
    }
}
