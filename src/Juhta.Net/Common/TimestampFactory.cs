
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Diagnostics;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a class for generating unique 21-digit timestamps. Timestamps will be returned in Coordinated Universal
    /// Time (UTC). An example of a timestamp is '201805282026441234567', generated on May 28, 2018 at 20:26:44.
    /// </summary>
    /// <remarks>This class is thread-safe.</remarks>
    public class TimestampFactory
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public TimestampFactory()
        {
            m_created = DateTime.UtcNow;

            m_stopwatch = new Stopwatch();

            m_stopwatch.Start();

            m_syncLock = new object();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a 21-digit unique timestamp.
        /// </summary>
        /// <returns>Returns the unique timestamp that was given to the calling thread.</returns>
        public string GetUniqueTimestamp()
        {
            DateTime now;
            string timestamp;
            bool isUnique;

            do
            {
                now = m_created.Add(m_stopwatch.Elapsed);

                timestamp = String.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                timestamp += String.Format("{0:0000000}", now.Ticks % 10000000);

                lock(m_syncLock)
                {
                    isUnique = String.Compare(timestamp, m_lastTimestamp) > 0;

                    if (isUnique)
                        m_lastTimestamp = timestamp;
                }
            }
            while (!isUnique);

            return(timestamp);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the time when this <see cref="TimestampFactory"/> instance was created.
        /// </summary>
        private DateTime m_created;

        /// <summary>
        /// Specifies the last timestamp that was returned from this <see cref="TimestampFactory"/> instance.
        /// </summary>
        private string m_lastTimestamp;

        /// <summary>
        /// Measures the elapsed time since this <see cref="TimestampFactory"/> instance was created.
        /// </summary>
        private Stopwatch m_stopwatch;

        /// <summary>
        /// Specifies a synchronization object for serializing concurrent access.
        /// </summary>
        private object m_syncLock;

        #endregion
    }
}
