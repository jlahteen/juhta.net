
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhta.Net.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T>
    {
        #region Static Constructor

        static Singleton()
        {
            s_syncLock = new object();
        }

        #endregion

        #region Public Properties

        public static T Instance
        {
            get { return (s_instance); }
        }

        #endregion

        #region Protected Constructors


        protected void SetInstance(T instance)
        {
            lock (s_syncLock)
            {
                if (s_instance != null)
                    throw new ArgumentException("Only one instance is allowed.");

                s_instance = instance;
            }
        }

        #endregion

        #region Private Fields

        protected static T s_instance;

        private static object s_syncLock;

        #endregion
    }
}
