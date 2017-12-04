
using System;

namespace AppXLibrary.DynamicCustomXmlConfigurableAndStartable
{
    public class ReplaceProcess
    {
        #region Public Constructors

        public ReplaceProcess() : this("a", "A")
        {}

        public ReplaceProcess(string search, string replace)
        {
            m_search = search;

            m_replace = replace;
        }

        #endregion

        #region Public Methods

        public string Replace(string s)
        {
            if (!m_started)
                throw new InvalidOperationException();

            return(s.Replace(m_search, m_replace));
        }

        public void Start()
        {
            if (m_started)
                throw new InvalidOperationException();

            m_started = true;
        }

        public void Stop()
        {
            if (!m_started)
                throw new InvalidOperationException();

            m_started = false;
        }

        #endregion

        #region Public Properties

        public bool IsStarted
        {
            get {return(m_started);}
        }

        #endregion

        #region Private Fields

        private string m_replace;

        private string m_search;

        private bool m_started;

        #endregion
    }
}
