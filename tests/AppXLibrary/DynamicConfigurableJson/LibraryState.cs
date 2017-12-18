
using AppXLibrary.DynamicConfigurable;
using Juhta.Net.LibraryManagement;
using Microsoft.Extensions.Configuration;

namespace AppXLibrary.DynamicConfigurableJson
{
    public class LibraryState : IConfigurableLibraryState
    {
        #region Public Methods

        public void Initialize(IConfigurationRoot config)
        {
            StringCache stringCache = new StringCache();

            for (int i = 0; i < 10; i++)
                stringCache.Add(config[$"stringCache:{i}:key"], config[$"stringCache:{i}:value"]);

            this.StringCache = stringCache;
        }

        #endregion

        #region Public Properties

        public StringCache StringCache {get; internal set;}

        #endregion
    }
}
