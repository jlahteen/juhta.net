
using AppXLibrary.Configurable;
using Juhta.Net.LibraryManagement;
using Microsoft.Extensions.Configuration;

namespace AppXLibrary.ConfigurableJson
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
                stringCache.Add(config[$"stringCache:{i}:key"], config[$"stringCache:{i}:value"]);
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
