
namespace AppXLibrary
{
    public class LibraryConfig
    {
        #region Public Methods

        public int GetIntSetting()
        {
            return(LibraryConfig.IntSetting);
        }

        public string GetStringSetting()
        {
            return (LibraryConfig.StringSetting);
        }

        #endregion

        #region Public Properties

        public static bool CloseLibraryReturnValue {get; set;} = true;

        public static bool CloseLibraryThrowException {get; set;} = false;

        public static int IntSetting {get; set;}

        public static string StringSetting {get; set;}

        #endregion
    }
}
