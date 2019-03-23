
using Juhta.Net.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Reflection;

namespace Juhta.Net.WebApi.Exceptions.Tests
{
    public abstract class WebApiExceptionTests
    {
        #region Protected Methods

        protected void AssertException(ClientErrorException exception, string notUsed, string errorCode, string errorMessage, HttpStatusCode statusCode)
        {
            Assert.IsNotNull(exception.ServiceStack);

            Assert.AreEqual<string>(Assembly.GetEntryAssembly().GetFileNameWithoutExtension(), exception.ServiceStack[0]);

            Assert.AreEqual<string>(errorCode, exception.Error?.Code);

            Assert.AreEqual<string>(errorMessage, exception.Message);

            Assert.AreEqual<HttpStatusCode>(statusCode, exception.StatusCode);
        }

        protected void AssertException(ServerErrorException exception, string notUsed, string errorMessage, HttpStatusCode statusCode)
        {
            Assert.IsNotNull(exception.ServiceStack);

            Assert.AreEqual<string>(Assembly.GetEntryAssembly().GetFileNameWithoutExtension(), exception.ServiceStack[0]);

            Assert.AreEqual<string>(errorMessage, exception.Message);

            Assert.AreEqual<HttpStatusCode>(statusCode, exception.StatusCode);
        }

        protected void AssertException(ClientErrorException exception, string notUsed, HttpStatusCode statusCode, string errorCode, string errorMessage, string field, string helpUrl)
        {
            Assert.IsNotNull(exception.ServiceStack);

            Assert.AreEqual<string>(Assembly.GetEntryAssembly().GetFileNameWithoutExtension(), exception.ServiceStack[0]);

            Assert.AreEqual<HttpStatusCode>(statusCode, exception.StatusCode);

            Assert.AreEqual<string>(errorCode, exception.Errors[0].Code);

            Assert.AreEqual<string>(errorMessage, exception.Errors[0].Message);

            Assert.AreEqual<string>(field, exception.Errors[0].Field);

            Assert.AreEqual<string>(helpUrl, exception.Errors[0].HelpUrl);
        }

        protected void AssertException(ClientErrorException exception, string notUsed, HttpStatusCode statusCode, string errorCode1, string errorMessage1, string field1, string helpUrl1, string errorCode2, string errorMessage2, string field2, string helpUrl2)
        {
            Assert.IsNotNull(exception.ServiceStack);

            Assert.AreEqual<string>(Assembly.GetEntryAssembly().GetFileNameWithoutExtension(), exception.ServiceStack[0]);

            Assert.AreEqual<HttpStatusCode>(statusCode, exception.StatusCode);

            Assert.AreEqual<string>(errorCode1, exception.Errors[0].Code);

            Assert.AreEqual<string>(errorMessage1, exception.Errors[0].Message);

            Assert.AreEqual<string>(field1, exception.Errors[0].Field);

            Assert.AreEqual<string>(helpUrl1, exception.Errors[0].HelpUrl);

            Assert.AreEqual<string>(errorCode2, exception.Errors[1].Code);

            Assert.AreEqual<string>(errorMessage2, exception.Errors[1].Message);

            Assert.AreEqual<string>(field2, exception.Errors[1].Field);

            Assert.AreEqual<string>(helpUrl2, exception.Errors[1].HelpUrl);
        }

        protected void AssertExceptions(ClientErrorException exception1, ClientErrorException exception2)
        {
            Assert.IsTrue(exception1.ServiceStack.Length <= exception2.ServiceStack.Length);

            if (exception1.ServiceStack.Length > 0)
                for (int i = 0; i < exception1.ServiceStack.Length; i++)
                    Assert.AreEqual(exception1.ServiceStack[i], exception2.ServiceStack[i]);

            Assert.AreEqual<string>(exception1.Error?.Code, exception2.Error?.Code);

            Assert.AreEqual<HttpStatusCode>(exception1.StatusCode, exception2.StatusCode);

            Assert.AreEqual<int?>(exception1.Errors?.Length, exception2.Errors?.Length);

            if (exception1.Errors != null)

                for (int i = 0; i < exception1.Errors.Length; i++)
                {
                    Assert.AreEqual<string>(exception1.Errors[i].Code, exception2.Errors[i].Code);

                    Assert.AreEqual<string>(exception1.Errors[i].Field, exception2.Errors[i].Field);

                    Assert.AreEqual<string>(exception1.Errors[i].HelpUrl, exception2.Errors[i].HelpUrl);

                    Assert.AreEqual<string>(exception1.Errors[i].Message, exception2.Errors[i].Message);
                }
        }

        protected void AssertExceptions(ServerErrorException exception1, ServerErrorException exception2)
        {
            Assert.IsTrue(exception1.ServiceStack.Length <= exception2.ServiceStack.Length);

            if (exception1.ServiceStack.Length > 0)
                for (int i = 0; i < exception1.ServiceStack.Length; i++)
                    Assert.AreEqual(exception1.ServiceStack[i], exception2.ServiceStack[i]);

            Assert.AreEqual<string>(exception1.ErrorMessage, exception2.ErrorMessage);

            Assert.AreEqual<HttpStatusCode>(exception1.StatusCode, exception2.StatusCode);
        }

        protected void ThrowException()
        {
            DoThrowException();
        }

        #endregion

        #region Private Methods

        private void DoThrowException()
        {
            throw new ArgumentException("Wrong argument was passed in!");
        }

        #endregion
    }
}
