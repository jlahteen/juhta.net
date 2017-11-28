
using Juhta.Net;

namespace AppXLibrary.DynamicCustomXmlConfigurable
{
    public static class GreetingService
    {
        #region Public Methods

        public static string GetGreeting()
        {
            using (var context = Application.Instance.CreateDynamicLibraryContext<LibraryHandle, LibraryState>())
            {
                return(context.LibraryState.CurrentGreeting);
            }
        }

        #endregion
    }
}
