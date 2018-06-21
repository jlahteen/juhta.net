
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;
using System.Threading;

namespace Juhta.Net.Processing
{
    /// <summary>
    /// Defines an abstract base class for consumer threads. The class provides a thread-safe method for putting
    /// objects into consumption. Incoming objects will be consumed asynchronously with a single worker thread.
    /// </summary>
    /// <typeparam name="T">Specifies the type of objects to consume.</typeparam>
    public abstract class ConsumerThread<T>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ConsumerThread()
        {
            m_objectQueue = new ObjectQueue<T>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Consumes a specified object asynchronously.
        /// </summary>
        /// <param name="object">Specifies an object to consume.</param>
        /// <remarks>
        /// <para>This method returns immediately after the specified object has been put into an internal queue to
        /// wait for asynchronous consumption.</para>
        /// <para>Multiple threads can access this method concurrently.</para>
        /// </remarks>
        public void Consume(T @object)
        {
            if (m_workerThread == null)
                throw new InvalidOperationException(LibraryMessages.Error027.GetMessage());

            else if (!m_workerThread.IsAlive)
                throw new InvalidOperationException(LibraryMessages.Error028.GetMessage());

            m_objectQueue.PutObject(@object);
        }

        /// <summary>
        /// Starts this <see cref="ConsumerThread{T}"/> instance.
        /// </summary>
        public void Start()
        {
            if (m_workerThread != null)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(nameof(Start), this.GetType()));

            m_workerThread = new Thread(new ThreadStart(WorkerThreadMain));

            m_workerThread.Start();
        }

        /// <summary>
        /// Stops this <see cref="ConsumerThread{T}"/> instance.
        /// </summary>
        public void Stop()
        {
            if (m_workerThread == null)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(nameof(Stop), this.GetType()));

            else if (m_workerThread.ThreadState == ThreadState.Stopped)
                return;

            m_objectQueue.Close();

            m_workerThread.Join();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Consumes a specified object.
        /// </summary>
        /// <param name="object">Specifies an object to consume.</param>
        protected abstract void ConsumeObject(T @object);

        /// <summary>
        /// Performs necessary actions when an error occurs at the time an object is consumed.
        /// </summary>
        /// <param name="object">Specifies an object whose consumption was failed.</param>
        /// <param name="exception">Specifies the occurred exception.</param>
        protected abstract void OnConsumeObjectFailed(T @object, Exception exception);

        /// <summary>
        /// Performs necessary actions when the worker thread is about to exit due to an (unexpected) exception.
        /// </summary>
        /// <param name="exception">Specifies an exception that forces the worker thread to exit.</param>
        protected abstract void OnWorkerThreadFailed(Exception exception);

        #endregion

        #region Private Methods

        /// <summary>
        /// Consumes objects until the queue of incoming objects is closed.
        /// </summary>
        /// <remarks>This method is the 'main' program of the worker thread.</remarks>
        private void WorkerThreadMain()
        {
            T @object;

            try
            {
                while ((@object = m_objectQueue.GetObject()) != null)

                    try
                    {
                        ConsumeObject(@object);
                    }

                    catch (Exception ex)
                    {
                        OnConsumeObjectFailed(@object, ex);
                    }
            }

            catch (Exception ex)
            {
                OnWorkerThreadFailed(ex);
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the queue where incoming objects are put to wait for consumption.
        /// </summary>
        private ObjectQueue<T> m_objectQueue;

        /// <summary>
        /// Specifies the worker thread that actually consumes incoming objects.
        /// </summary>
        private Thread m_workerThread;

        #endregion
    }
}
