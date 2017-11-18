
using Juhta.Net.LibraryManagement;
using System;

namespace AppXLibrary
{
    public class ClosableLibrary : ApplicationLibraryHandle, IClosableLibrary
    {
        #region Public Constructors

        public ClosableLibrary() : base("AppXLibrary.dll")
        {}

        #endregion

        #region Public Methods

        public bool CloseLibrary()
        {
            LibraryConfig.IntSetting = Int32.MaxValue;

            LibraryConfig.StringSetting = "<null>";

            return(true);
        }

        #endregion
    }
}
