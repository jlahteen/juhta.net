
using Juhta.Net.Common;
using System.Collections.Generic;

namespace AppXLibrary.Configurable
{
    public class StringCache : Singleton<StringCache>
    {
        #region Public Constructors

        public StringCache()
        {
            m_cache = new Dictionary<string, string>();

            SetSingletonInstance(this);
        }

        #endregion

        #region Public Methods

        public string Get(string key)
        {
            return(m_cache[key]);
        }

        public void Close()
        {
            m_cache = null;

            ResetSingletonInstance();
        }

        #endregion

        #region Internal Methods

        internal void Add(string key, string value)
        {
            m_cache.Add(key, value);
        }

        #endregion

        #region Private Fields

        private IDictionary<string, string> m_cache;

        #endregion
    }
}
