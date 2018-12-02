
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class BadRequestExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException();
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException1_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.BadRequest.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.BadRequestException' was thrown.",
                    HttpStatusCode.BadRequest
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber);
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.BadRequest.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.BadRequestException' was thrown.",
                    HttpStatusCode.BadRequest
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException("BadRequestException Specified order number is invalid.");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException3_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.BadRequest.ToString(),
                    "BadRequestException Specified order number is invalid.",
                    HttpStatusCode.BadRequest
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber, "BadRequestException Specified order number is invalid.");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.BadRequest.ToString(),
                    "BadRequestException Specified order number is invalid.",
                    HttpStatusCode.BadRequest
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
