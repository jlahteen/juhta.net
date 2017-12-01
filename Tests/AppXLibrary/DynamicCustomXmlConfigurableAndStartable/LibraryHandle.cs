
using Juhta.Net.LibraryManagement;
using System.Xml.Schema;

namespace AppXLibrary.DynamicCustomXmlConfigurableAndStartable
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

        public ICustomXmlConfigurableLibraryState CreateLibraryState()
        {
            return(new LibraryState());
        }

        public XmlSchema[] GetConfigSchemas()
        {
            XmlSchema configSchema;

            configSchema = base.GetEmbeddedConfigSchema(System.Reflection.Assembly.GetExecutingAssembly(), "AppXLibrary.DynamicCustomXmlConfigurableAndStartable", "Config.xsd");

            return(new XmlSchema[]{configSchema});
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName => "AppXLibrary.config";

        #endregion
    }
}
