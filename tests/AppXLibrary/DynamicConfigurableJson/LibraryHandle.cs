
using Juhta.Net.LibraryManagement;

namespace AppXLibrary.DynamicConfigurableJson
{
    public class LibraryHandle : DynamicLibraryHandleBase, IDynamicConfigurableLibrary, IDynamicInitializableLibrary
    {
        #region Public Constructors

        public LibraryHandle() : base("AppXLibrary.dll")
        {}

        #endregion

        #region Public Methods

        public IDefaultLibraryState CreateDefaultLibraryState()
        {
            return(new LibraryState());
        }

        public IConfigurableLibraryState CreateLibraryState()
        {
            return(new LibraryState());
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName
        {
            get {return("AppXLibrary.json");}
        }

        #endregion
    }
}
