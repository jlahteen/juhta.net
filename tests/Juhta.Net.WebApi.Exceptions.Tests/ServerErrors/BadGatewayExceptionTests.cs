
using Juhta.Net.WebApi.Exceptions.ServerErrors;
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
            ServerError serverError1, serverError2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrors.BadGatewayException' was thrown.",
                    HttpStatusCode.BadGateway
                );

                serverError1 = ex.ToServerError();

                exception1 = ex;
            }

            serverError2 = JsonConvert.DeserializeObject<ServerError>(JsonConvert.SerializeObject(serverError1));

            try
            {
                serverError2.Throw();
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
            ServerError serverError1, serverError2;
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

                serverError1 = ex.ToServerError();

                exception1 = ex;
            }

            serverError2 = JsonConvert.DeserializeObject<ServerError>(JsonConvert.SerializeObject(serverError1));

            try
            {
                serverError2.Throw();
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
            ServerError serverError1, serverError2;
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

                serverError1 = ex.ToServerError();

                exception1 = ex;
            }

            serverError2 = JsonConvert.DeserializeObject<ServerError>(JsonConvert.SerializeObject(serverError1));

            try
            {
                serverError2.Throw();
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
