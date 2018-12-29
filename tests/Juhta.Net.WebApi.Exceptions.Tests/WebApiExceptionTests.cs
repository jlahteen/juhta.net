
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests
{
    public abstract class WebApiExceptionTests
    {
        #region Protected Methods

        protected void AssertException(ClientErrorException exception, string methodName, string errorCode, string errorMessage, HttpStatusCode statusCode)
        {
            Assert.IsNotNull(exception.CallStack);

            Assert.IsTrue(exception.CallStack[1].Contains(methodName));

            Assert.AreEqual<string>(errorCode, exception.ErrorCode);

            Assert.AreEqual<string>(errorMessage, exception.Message);

            Assert.AreEqual<HttpStatusCode>(statusCode, exception.StatusCode);
        }

        protected void AssertException(ServerErrorException exception, string methodName, string innerException, string errorMessage, HttpStatusCode statusCode)
        {
            Assert.IsNotNull(exception.CallStack);

            Assert.IsTrue(exception.CallStack[1].Contains(methodName));

            Assert.AreEqual<string>(innerException, exception.InnerException);

            Assert.AreEqual<string>(errorMessage, exception.Message);

            Assert.AreEqual<HttpStatusCode>(statusCode, exception.StatusCode);
        }

        protected void AssertExceptions(ClientErrorException exception1, ClientErrorException exception2)
        {
            for (int i = 0; i < c_maxCallStackLinesToAssert; i++)
                Assert.AreEqual(exception1.CallStack[i], exception2.CallStack[i]);

            Assert.AreEqual<string>(exception1.ErrorCode, exception2.ErrorCode);

            Assert.AreEqual<string>(exception1.ErrorMessage, exception2.ErrorMessage);

            Assert.AreEqual<HttpStatusCode>(exception1.StatusCode, exception2.StatusCode);
        }

        protected void AssertExceptions(ServerErrorException exception1, ServerErrorException exception2)
        {
            for (int i = 0; i < c_maxCallStackLinesToAssert; i++)
                Assert.AreEqual(exception1.CallStack[i], exception2.CallStack[i]);

            Assert.AreEqual<string>(exception1.InnerException, exception2.InnerException);

            Assert.AreEqual<string>(exception1.ErrorMessage, exception2.ErrorMessage);

            Assert.AreEqual<HttpStatusCode>(exception1.StatusCode, exception2.StatusCode);
        }

        protected void ThrowException()
        {
            DoThrowException();
        }

        #endregion

        #region Protected Constants

        protected const int c_maxCallStackLinesToAssert = 20;

        #endregion

        #region Private Methods

        private void DoThrowException()
        {
            throw new ArgumentException("Wrong argument was passed in!");
        }

        #endregion
    }
}
