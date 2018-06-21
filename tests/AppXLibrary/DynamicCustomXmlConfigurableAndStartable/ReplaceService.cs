
using Juhta.Net;

namespace AppXLibrary.DynamicCustomXmlConfigurableAndStartable
{
    public class ReplaceService : AppXLibrary.DynamicCustomXmlConfigurableAndStartable.IReplaceService
    {
        #region Public Methods

        public string Replace(string s)
        {
            using (var context = Application.Instance.GetDynamicLibraryContext<LibraryHandle, LibraryState>())
            {
                return(context.LibraryState.ReplaceProcess.Replace(s));
            }
        }

        #endregion
    }
}
