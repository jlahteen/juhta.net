
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestEntityTooLargeExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException();
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestEntityTooLargeException' was thrown.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestEntityTooLargeException' was thrown.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException("RequestEntityTooLargeException Specified order number is invalid.");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException3_ShouldReturn",
                    null,
                    "RequestEntityTooLargeException Specified order number is invalid.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber, "RequestEntityTooLargeException Specified order number is invalid.");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "RequestEntityTooLargeException Specified order number is invalid.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
