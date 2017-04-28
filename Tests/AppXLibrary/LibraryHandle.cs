
using Juhta.Net.LibraryManagement;

namespace AppXLibrary
{
    public class LibraryHandle : AppLibraryHandle, IInitializableLibrary
    {
        #region Public Constructors

        public LibraryHandle() : base("AppXLibrary.dll")
        {}

        #endregion

        #region Public Methods

        public void InitializeLibrary()
        {
            LibraryConfig.IntSetting = 121213;

            LibraryConfig.StringSetting = "This is the default value for the StringSetting set by the default LibraryHandle class.";
        }

        #endregion
    }
}
