
using System;

namespace AppXLibrary.DynamicCustomXmlConfigurableAndStartable
{
    public class LibraryProcess
    {
        #region Public Methods

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

        private bool m_started;

        #endregion
    }
}
