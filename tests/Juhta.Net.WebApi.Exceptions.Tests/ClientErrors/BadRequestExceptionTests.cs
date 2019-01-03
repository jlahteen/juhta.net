
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.BadRequestException' was thrown.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.BadRequestException' was thrown.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "BadRequestException Specified order number is invalid.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "BadRequestException Specified order number is invalid.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
