
using Juhta.Net.Startup;

namespace AppXLibrary.DynamicCustomXmlConfigurable
{
    public static class GreetingService
    {
        #region Public Methods

        public static string GetGreeting()
        {
            using (var context = Application.Instance.GetDynamicLibraryContext<LibraryHandle, LibraryState>())
            {
                return(context.LibraryState.CurrentGreeting);
            }
        }

        #endregion
    }
}
