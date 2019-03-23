
using Juhta.Net.WebApi.Exceptions.ServerErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ServerErrors
{
    [TestClass]
    public class HttpVersionNotSupportedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_HttpVersionNotSupportedException1_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            HttpVersionNotSupportedException exception1 = null, exception2 = null;

            try
            {
                throw new HttpVersionNotSupportedException();
            }

            catch (HttpVersionNotSupportedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_HttpVersionNotSupportedException1_ShouldReturn",
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrorExceptions.HttpVersionNotSupportedException' was thrown.",
                    HttpStatusCode.HttpVersionNotSupported
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (HttpVersionNotSupportedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_HttpVersionNotSupportedException2_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            HttpVersionNotSupportedException exception1 = null, exception2 = null;

            try
            {
                throw new HttpVersionNotSupportedException("This is a server exception of the type HttpVersionNotSupportedException!");
            }

            catch (HttpVersionNotSupportedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_HttpVersionNotSupportedException2_ShouldReturn",
                    "This is a server exception of the type HttpVersionNotSupportedException!",
                    HttpStatusCode.HttpVersionNotSupported
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (HttpVersionNotSupportedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_HttpVersionNotSupportedException3_ShouldReturn()
        {
            Exception innerException = null;
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            HttpVersionNotSupportedException exception1 = null, exception2 = null;

            try
            {
                ThrowException();
            }

            catch (Exception ex)
            {
                innerException = ex;
            }

            try
            {
                throw new HttpVersionNotSupportedException("This is a server exception of the type HttpVersionNotSupportedException!", innerException);
            }

            catch (HttpVersionNotSupportedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_HttpVersionNotSupportedException3_ShouldReturn",
                    "This is a server exception of the type HttpVersionNotSupportedException!",
                    HttpStatusCode.HttpVersionNotSupported
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (HttpVersionNotSupportedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
