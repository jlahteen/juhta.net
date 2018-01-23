
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines an abstract base class singleton classes.
    /// </summary>
    /// <typeparam name="T">Specifies a type to make as singleton.</typeparam>
    public abstract class Singleton<T>
    {
        #region Static Constructor

        /// <summary>
        /// Initializes the class.
        /// </summary>
        static Singleton()
        {
            s_syncLock = new object();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the singleton instance of <typeparamref name="T"/>.
        /// </summary>
        public static T Instance
        {
            get
            {
                lock(s_syncLock)
                {
                    return(s_instance);
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets a null value as the singleton instance.
        /// </summary>
        protected void ResetSingletonInstance()
        {
            lock(s_syncLock)
            {
                s_instance = default(T);
            }
        }

        /// <summary>
        /// Sets a specified value as the singleton instance.
        /// </summary>
        /// <param name="instance">Specifies an instance of <typeparamref name="T"/> to be set as the singleton
        /// instance.</param>
        protected void SetSingletonInstance(T instance)
        {
            lock(s_syncLock)
            {
                if (s_instance != null)
                    throw new ArgumentException(CommonMessages.Error014.FormatMessage(typeof(T).FullName));

                s_instance = instance;
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Instance"/> property.
        /// </summary>
        protected static T s_instance;

        /// <summary>
        /// Specifies a synchronization object for accessing the singleton instance.
        /// </summary>
        private static object s_syncLock;

        #endregion
    }
}
