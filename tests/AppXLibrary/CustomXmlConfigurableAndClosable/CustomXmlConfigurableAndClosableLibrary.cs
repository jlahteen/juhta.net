
using AppXLibrary.CustomXmlConfig;
using AppXLibrary.CustomXmlConfigurable;
using Juhta.Net.LibraryManagement;
using System;

namespace AppXLibrary.CustomXmlConfigurableAndClosable
{
    public class CustomXmlConfigurableAndClosableLibrary : CustomXmlConfigurableLibrary, IClosableLibrary
    {
        #region Public Methods

        public bool CloseLibrary()
        {
            LibraryConfig.IntSetting = Int32.MaxValue;

            LibraryConfig.StringSetting = "<closed>";

            if (LibraryConfig.CloseLibraryThrowException)
                throw new Exception("Something went wrong in the closing of AppXLibrary.");

            return(LibraryConfig.CloseLibraryReturnValue);
        }

        #endregion
    }
}
