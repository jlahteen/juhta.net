
using Juhta.Net.Extensions;
using Juhta.Net.WebApi.Exceptions.ServerErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Reflection;

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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrors.BadGatewayException' was thrown.",
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
        public void ToString_InnerException_ShouldReturn()
        {
            Exception innerException = null;
            string[] lines;
            string serviceName = Assembly.GetEntryAssembly().GetFileNameWithoutExtension();

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
                lines = ex.ToString().Split(Environment.NewLine);

                Assert.AreEqual<string>("   --- WebApiException properties ---", lines[lines.Length - 3]);

                Assert.AreEqual<string>("     \"StatusCode\": \"BadGateway\"", lines[lines.Length - 2]);

                Assert.AreEqual<string>("     \"ServiceStack\": [\"" + serviceName + "\"]", lines[lines.Length - 1]);
            }
        }

        [TestMethod]
        public void ToString_NoInnerException_ShouldReturn()
        {
            string[] lines;
            string serviceName = Assembly.GetEntryAssembly().GetFileNameWithoutExtension();

            try
            {
                throw new BadGatewayException("This is a server exception of the type BadGatewayException!");
            }

            catch (BadGatewayException ex)
            {
                lines = ex.ToString().Split(Environment.NewLine);

                Assert.AreEqual<string>("   --- WebApiException properties ---", lines[lines.Length - 3]);

                Assert.AreEqual<string>("     \"StatusCode\": \"BadGateway\"", lines[lines.Length - 2]);

                Assert.AreEqual<string>("     \"ServiceStack\": [\"" + serviceName + "\"]", lines[lines.Length - 1]);
            }
        }

        #endregion
    }
}
