
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class NotFoundExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_NotFoundException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            NotFoundException exception1 = null, exception2 = null;

            try
            {
                throw new NotFoundException();
            }

            catch (NotFoundException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotFoundException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.NotFoundException' was thrown.",
                    HttpStatusCode.NotFound
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (NotFoundException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotFoundException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            NotFoundException exception1 = null, exception2 = null;

            try
            {
                throw new NotFoundException(ErrorCode.InvalidOrderNumber);
            }

            catch (NotFoundException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotFoundException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.NotFoundException' was thrown.",
                    HttpStatusCode.NotFound
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (NotFoundException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotFoundException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            NotFoundException exception1 = null, exception2 = null;

            try
            {
                throw new NotFoundException("NotFoundException Specified order number is invalid.");
            }

            catch (NotFoundException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotFoundException3_ShouldReturn",
                    null,
                    "NotFoundException Specified order number is invalid.",
                    HttpStatusCode.NotFound
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (NotFoundException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotFoundException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            NotFoundException exception1 = null, exception2 = null;

            try
            {
                throw new NotFoundException(ErrorCode.InvalidOrderNumber, "NotFoundException Specified order number is invalid.");
            }

            catch (NotFoundException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotFoundException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "NotFoundException Specified order number is invalid.",
                    HttpStatusCode.NotFound
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (NotFoundException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
