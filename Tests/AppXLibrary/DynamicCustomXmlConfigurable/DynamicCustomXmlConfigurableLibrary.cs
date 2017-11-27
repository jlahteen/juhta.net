
using Juhta.Net.Common;
using Juhta.Net.LibraryManagement;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Threading;

namespace AppXLibrary.DynamicCustomXmlConfigurable
{
    public class DynamicCustomXmlConfigurableLibrary : LibraryHandleBase, IDynamicCustomXmlConfigurableLibrary
    {
        #region Public Constructors

        public DynamicCustomXmlConfigurableLibrary() : this("AppXLibrary.dll")
        {}

        public DynamicCustomXmlConfigurableLibrary(string libraryFileName) : base(libraryFileName)
        {
            m_libraryStateLock = new ReaderWriterLockSlim();
        }

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

        public ILibraryState LibraryState 
        {
            get { return (m_libraryState); }

            set { m_libraryState = value; }
        }

        public ReaderWriterLockSlim LibraryStateLock
        {
            get {return(m_libraryStateLock);}
        }

        Juhta.Net.LibraryManagement.ILibraryState IDynamicLibrary.LibraryState
        {
            get { return (m_libraryState); }

            set { m_libraryState = value; }
        }

        #endregion

        #region Private Fields

        private ILibraryState m_libraryState;

        private ReaderWriterLockSlim m_libraryStateLock;

        #endregion
    }
}
