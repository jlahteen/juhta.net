
using Juhta.Net.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Juhta.Net.Tests.Helpers
{
    [TestClass]
    public class StackTraceHelperTests
    {
        #region Test Methods

        [TestMethod]
        public void GetCallStack_GenericTypeArgument_ShouldReturnListOfString()
        {
            CallMe1(null);
        }

        [TestMethod]
        public void GetCallStack_GenericTypeMethodAndArgument_ShouldReturnListOfString()
        {
            CallMe2<int, string>(null);
        }

        [TestMethod]
        public void GetCallStack_GenericTypeMethodAndArgument2_ShouldReturnListOfString()
        {
            CallMe3<Guid, object>(null);
        }

        #endregion

        #region Private Methods

        private List<string> CallMe1(SortedList<int, string> sortedList)
        {
            string[] callStack;

            callStack = StackTraceHelper.GetCallStack(1);

            Assert.AreEqual<string>("at Juhta.Net.Tests.Helpers.StackTraceHelperTests.CallMe1(System.Collections.Generic.SortedList`2<System.Int32, System.String> sortedList) in Juhta.Net.Tests.dll:StackTraceHelperTests.cs:40:13", callStack[0]);

            Assert.AreEqual<string>("at Juhta.Net.Tests.Helpers.StackTraceHelperTests.GetCallStack_GenericTypeArgument_ShouldReturnListOfString() in Juhta.Net.Tests.dll:StackTraceHelperTests.cs:17:13", callStack[1]);

            return(null);
        }

        private List<string> CallMe2<TKey, TValue>(SortedList<TKey, TValue> sortedList)
        {
            string[] callStack;

            callStack = StackTraceHelper.GetCallStack(1);

            Assert.AreEqual<string>("at Juhta.Net.Tests.Helpers.StackTraceHelperTests.CallMe2<Juhta.Net.Tests.Helpers.TKey, Juhta.Net.Tests.Helpers.TValue>(System.Collections.Generic.SortedList`2<Juhta.Net.Tests.Helpers.TKey, Juhta.Net.Tests.Helpers.TValue> sortedList) in Juhta.Net.Tests.dll:StackTraceHelperTests.cs:53:13", callStack[0]);

            Assert.AreEqual<string>("at Juhta.Net.Tests.Helpers.StackTraceHelperTests.GetCallStack_GenericTypeMethodAndArgument_ShouldReturnListOfString() in Juhta.Net.Tests.dll:StackTraceHelperTests.cs:23:13", callStack[1]);

            return(null);
        }

        private List<string> CallMe3<TKey, TValue>(SortedList<TKey, List<TValue>> sortedList)
        {
            string[] callStack;

            callStack = StackTraceHelper.GetCallStack(1);

            Assert.AreEqual<string>("at Juhta.Net.Tests.Helpers.StackTraceHelperTests.CallMe3<Juhta.Net.Tests.Helpers.TKey, Juhta.Net.Tests.Helpers.TValue>(System.Collections.Generic.SortedList`2<Juhta.Net.Tests.Helpers.TKey, System.Collections.Generic.List`1<Juhta.Net.Tests.Helpers.TValue>> sortedList) in Juhta.Net.Tests.dll:StackTraceHelperTests.cs:66:13", callStack[0]);

            Assert.AreEqual<string>("at Juhta.Net.Tests.Helpers.StackTraceHelperTests.GetCallStack_GenericTypeMethodAndArgument2_ShouldReturnListOfString() in Juhta.Net.Tests.dll:StackTraceHelperTests.cs:29:13", callStack[1]);

            return(null);
        }

        #endregion
    }
}
