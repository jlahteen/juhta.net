
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an abstract base class for configuration state classes.
    /// </summary>
    /// <seealso cref="IConfigState"/>
    public abstract class ConfigStateBase : IConfigState
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="IConfigState.Close"/>.
        /// </summary>
        public virtual void Close()
        {}

        /// <summary>
        /// See <see cref="IConfigState.Initialize"/>.
        /// </summary>
        public virtual void Initialize()
        {
            m_isInitialized = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// See <see cref="IConfigState.IsInitialized"/>.
        /// </summary>
        public bool IsInitialized
        {
            get {return(m_isInitialized);}
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Stores the <see cref="IsInitialized"/> property.
        /// </summary>
        protected bool m_isInitialized;

        #endregion
    }
}
