
using System.Collections.Generic;

namespace AppXLibrary.DynamicConfigurable
{
    public class StringCache
    {
        #region Public Constructors

        public StringCache()
        {
            m_cache = new Dictionary<string, string>();
        }

        #endregion

        #region Public Methods

        public string Get(string key)
        {
            return(m_cache[key]);
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
