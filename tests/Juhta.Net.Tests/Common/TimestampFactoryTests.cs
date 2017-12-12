
using Juhta.Net.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace Juhta.Net.Tests.Common
{
    [TestClass]
    public class TimestampFactoryTests
    {
        #region Test Methods

        [TestMethod]
        public void GetUniqueTimestamp_MultipleThreadStress_ShouldReturn()
        {
            Thread[] threads;
            int threadCount = c_threadCount;
            ThreadParam threadParam = new ThreadParam
            {
                LoopCount = c_loopCount,
                SyncLock = new object(),
                TimestampFactory = new TimestampFactory(),
                Timestamps = new Dictionary<string, string>()
            };

            threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
                threads[i] = new Thread(new ParameterizedThreadStart(ThreadMain));

            for (int i = 0; i < threadCount; i++)
                threads[i].Start(threadParam);

            for (int i = 0; i < threadCount; i++)
                threads[i].Join();

            Assert.AreEqual<int>(c_threadCount * c_loopCount, threadParam.Timestamps.Count);
        }

        #endregion

        #region Private Methods

        private static void ThreadMain(object threadParamObj)
        {
            ThreadParam threadParam = (ThreadParam)threadParamObj;
            string timestamp;

            for (int i = 0; i < threadParam.LoopCount; i++)
            {
                timestamp = threadParam.TimestampFactory.GetUniqueTimestamp();

                lock (threadParam.SyncLock)
                {
                    threadParam.Timestamps.Add(timestamp, timestamp);
                }
            }
        }

        #endregion

        #region Private Types

        private class ThreadParam
        {
            #region Public Properties

            public int LoopCount {get; set;}

            public object SyncLock {get; set;}

            public TimestampFactory TimestampFactory {get; set;}

            public Dictionary<string, string> Timestamps {get; set;}

            #endregion
        }

        #endregion

        #region Private Constants

        private const int c_loopCount = 10000;

        private const int c_threadCount = 200;

        #endregion
    }
}
