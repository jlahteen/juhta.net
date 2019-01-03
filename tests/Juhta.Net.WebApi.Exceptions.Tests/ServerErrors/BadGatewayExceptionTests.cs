
using Juhta.Net.WebApi.Exceptions.ServerErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ServerErrors
{
    [TestClass]
    public class BadGatewayExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_BadGatewayException1_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            BadGatewayException exception1 = null, exception2 = null;

            try
            {
                throw new BadGatewayException();
            }

            catch (BadGatewayException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadGatewayException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrorExceptions.BadGatewayException' was thrown.",
                    HttpStatusCode.BadGateway
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (BadGatewayException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadGatewayException2_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            BadGatewayException exception1 = null, exception2 = null;

            try
            {
                throw new BadGatewayException("This is a server exception of the type BadGatewayException!");
            }

            catch (BadGatewayException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadGatewayException2_ShouldReturn",
                    null,
                    "This is a server exception of the type BadGatewayException!",
                    HttpStatusCode.BadGateway
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (BadGatewayException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadGatewayException3_ShouldReturn()
        {
            Exception innerException = null;
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            BadGatewayException exception1 = null, exception2 = null;

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
                throw new BadGatewayException("This is a server exception of the type BadGatewayException!", innerException);
            }

            catch (BadGatewayException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadGatewayException3_ShouldReturn",
                    innerException.ToString(),
                    "This is a server exception of the type BadGatewayException!",
                    HttpStatusCode.BadGateway
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (BadGatewayException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
