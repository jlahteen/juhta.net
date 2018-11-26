
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests
{
    public abstract class WebApiExceptionTests
    {
        #region Protected Methods

        protected void AssertException(ClientErrorException exception, string methodName, string errorId, string errorType, string errorMessage, HttpStatusCode statusCode)
        {
            Assert.IsNotNull(exception.CallStack);

            Assert.IsTrue(exception.CallStack[1].Contains(methodName));

            Assert.AreEqual<string>(errorId, exception.ErrorId);

            Assert.AreEqual<string>(errorType, exception.ErrorType);

            Assert.AreEqual<string>(errorMessage, exception.Message);

            Assert.AreEqual<HttpStatusCode>(statusCode, exception.StatusCode);
        }

        protected void AssertExceptions(ClientErrorException exception1, ClientErrorException exception2)
        {
            for (int i = 0; i < c_maxCallStackLinesToAssert; i++)
                Assert.AreEqual(exception1.CallStack[i], exception2.CallStack[i]);

            Assert.AreEqual<string>(exception1.ErrorId, exception2.ErrorId);

            Assert.AreEqual<string>(exception1.ErrorMessage, exception2.ErrorMessage);

            Assert.AreEqual<string>(exception1.ErrorType, exception2.ErrorType);

            Assert.AreEqual<HttpStatusCode>(exception1.StatusCode, exception2.StatusCode);
        }

        #endregion

        #region Protected Constants

        protected const int c_maxCallStackLinesToAssert = 20;

        #endregion
    }
}
