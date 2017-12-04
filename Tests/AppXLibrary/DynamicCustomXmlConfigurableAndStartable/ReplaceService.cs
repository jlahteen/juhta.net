
using Juhta.Net;

namespace AppXLibrary.DynamicCustomXmlConfigurableAndStartable
{
    public static class ReplaceService
    {
        #region Public Methods

        public static string Replace(string s)
        {
            using (var context = Application.Instance.CreateDynamicLibraryContext<LibraryHandle, LibraryState>())
            {
                return(context.LibraryState.ReplaceProcess.Replace(s));
            }
        }

        #endregion
    }
}
