
using Juhta.Net.WebApi.Exceptions.ServerErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ServerErrors
{
    [TestClass]
    public class GatewayTimeoutExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_GatewayTimeoutException1_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            GatewayTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new GatewayTimeoutException();
            }

            catch (GatewayTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_GatewayTimeoutException1_ShouldReturn",
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrorExceptions.GatewayTimeoutException' was thrown.",
                    HttpStatusCode.GatewayTimeout
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (GatewayTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_GatewayTimeoutException2_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            GatewayTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new GatewayTimeoutException("This is a server exception of the type GatewayTimeoutException!");
            }

            catch (GatewayTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_GatewayTimeoutException2_ShouldReturn",
                    "This is a server exception of the type GatewayTimeoutException!",
                    HttpStatusCode.GatewayTimeout
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (GatewayTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_GatewayTimeoutException3_ShouldReturn()
        {
            Exception innerException = null;
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            GatewayTimeoutException exception1 = null, exception2 = null;

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
                throw new GatewayTimeoutException("This is a server exception of the type GatewayTimeoutException!", innerException);
            }

            catch (GatewayTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_GatewayTimeoutException3_ShouldReturn",
                    "This is a server exception of the type GatewayTimeoutException!",
                    HttpStatusCode.GatewayTimeout
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (GatewayTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
