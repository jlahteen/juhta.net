
using AppXLibrary.Configurable;
using Juhta.Net.LibraryManagement;
using Microsoft.Extensions.Configuration;

namespace AppXLibrary.ConfigurableIni
{
    public class ConfigurableLibrary : LibraryHandleBase, IConfigurableLibrary
    {
        #region Public Constructors

        public ConfigurableLibrary() : base("AppXLibrary.dll")
        {}

        #endregion

        #region Public Methods

        public void InitializeLibrary(IConfigurationRoot config)
        {
            StringCache stringCache;

            if (StringCache.Instance != null)
                StringCache.Instance.Close();

            stringCache = new StringCache();

            for (int i = 0; i < 10; i++)
                stringCache.Add(config[$"stringCache:key{i}"], config[$"stringCache:value{i}"]);
        }

        #endregion

        #region Public Properties

        public override string ConfigFileName
        {
            get {return("AppXLibrary.ini");}
        }

        #endregion
    }
}
