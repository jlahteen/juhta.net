
using Juhta.Net.LibraryManagement;
using System.Xml.Schema;

namespace AppXLibrary.DynamicCustomXmlConfigurable
{
    public class LibraryHandle : DynamicLibraryHandleBase, IDynamicCustomXmlConfigurableLibrary, IDynamicInitializableLibrary
    {
        #region Public Constructors

        public LibraryHandle() : this("AppXLibrary.dll")
        {}

        public LibraryHandle(string libraryFileName) : base(libraryFileName)
        {}

        #endregion

        #region Public Methods

        public IDefaultLibraryState CreateDefaultLibraryState()
        {
            return(new LibraryState());
        }

        public ICustomXmlConfigurableLibraryState CreateLibraryState()
        {
            return(new LibraryState());
        }

        public XmlSchema[] GetConfigSchemas()
        {
            XmlSchema configSchema;

            configSchema = base.GetEmbeddedConfigSchema(System.Reflection.Assembly.GetExecutingAssembly(), "AppXLibrary.DynamicCustomXmlConfigurable", "Config.xsd");

            return(new XmlSchema[]{configSchema});
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName => "AppXLibrary.config";

        #endregion
    }
}
