
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections;
using System.Threading;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Represents a first-in, first-out queue for a generic object type. The queue implemented by this class is
    /// thread-safe, and it also blocks readers in case of an empty queue. This queue has especially been designed to
    /// act as a work queue in multi-threaded applications.
    /// </summary>
    /// <typeparam name="T">Specifies an object type.</typeparam>
    public class ObjectQueue<T>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ObjectQueue()
        {
            m_eventWaitHandles = new EventWaitHandle[Enum.GetValues(typeof(WaitEvent)).GetLength(0)];

            m_eventWaitHandles[(int)WaitEvent.Close] = new ManualResetEvent(false);

            m_eventWaitHandles[(int)WaitEvent.Put] = new AutoResetEvent(false);

            m_mutex = new Mutex();

            m_objectQueue = new Queue();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes the object queue specified by this ObjectQueue instance. The operation releases all threads that are
        /// blocked by the current instance.
        /// </summary>
        public void Close()
        {
            m_mutex.WaitOne();

            m_eventWaitHandles[(int)WaitEvent.Close].Set();

            m_closed = true;

            m_mutex.ReleaseMutex();
        }

        /// <summary>
        /// Gets the next object from the object queue specified by this ObjectQueue instance.
        /// </summary>
        /// <returns>Returns the next instance of <i>T</i> from the queue. The return value is null only, if the queue
        /// was initially empty and the queue had been closed prior to the call or no object became available before
        /// the queue was closed during the call.</returns>
        public T GetObject()
        {
            T @object;

            while (true)
            {
                m_mutex.WaitOne();

                if (m_objectQueue.Count > 0)
                {
                    // The object queue is non-empty

                    @object = (T)m_objectQueue.Dequeue();

                    if (m_objectQueue.Count > 0)
                        m_eventWaitHandles[(int)WaitEvent.Put].Set();

                    m_mutex.ReleaseMutex();

                    // Return
                    return(@object);
                }
                else if (m_closed)
                {
                    // The object queue has been closed

                    m_mutex.ReleaseMutex();

                    // Return
                    return(default(T));
                }
                else
                {
                    // The object queue is currently empty

                    // We must wait until a new element is put to the queue or the queue is closed

                    m_mutex.ReleaseMutex();

                    WaitHandle.WaitAny(m_eventWaitHandles);
                }
            }
        }

        /// <summary>
        /// Puts an object to the object queue specified by this ObjectQueue instance.
        /// </summary>
        /// <param name="object">Specifies an object.</param>
        public void PutObject(T @object)
        {
            m_mutex.WaitOne();

            try
            {
                m_objectQueue.Enqueue(@object);

                m_eventWaitHandles[(int)WaitEvent.Put].Set();
            }

            catch
            {
                throw;
            }

            finally
            {
                m_mutex.ReleaseMutex();
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of elements contained in the object queue specified by this ObjectQueue instance.
        /// </summary>
        public int Count
        {
            get
            {
                int count;

                m_mutex.WaitOne();

                count = m_objectQueue.Count;

                m_mutex.ReleaseMutex();

                return(count);
            }
        }

        #endregion

        #region Private Types

        /// <summary>
        /// Defines an enumeration for wait events.
        /// </summary>
        private enum WaitEvent
        {
            /// <summary>
            /// Specifies the Close event.
            /// </summary>
            Close,

            /// <summary>
            /// Specifies the Put event.
            /// </summary>
            Put
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies whether the object queue has been closed.
        /// </summary>
        private bool m_closed;

        /// <summary>
        /// Specifies an array of EventWaitHandle objects related to this ObjectQueue instance.
        /// </summary>
        private EventWaitHandle[] m_eventWaitHandles;

        /// <summary>
        /// Specifies a Mutex object for synchronizing concurrent access to the object queue.
        /// </summary>
        private Mutex m_mutex;

        /// <summary>
        /// Specifies the underlying Queue instance.
        /// </summary>
        private Queue m_objectQueue;

        #endregion
    }
}
