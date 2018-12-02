
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestTimeoutExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException();
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException1_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.RequestTimeout.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestTimeoutException' was thrown.",
                    HttpStatusCode.RequestTimeout
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.RequestTimeout.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestTimeoutException' was thrown.",
                    HttpStatusCode.RequestTimeout
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException("RequestTimeoutException Specified order number is invalid.");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException3_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.RequestTimeout.ToString(),
                    "RequestTimeoutException Specified order number is invalid.",
                    HttpStatusCode.RequestTimeout
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber, "RequestTimeoutException Specified order number is invalid.");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.RequestTimeout.ToString(),
                    "RequestTimeoutException Specified order number is invalid.",
                    HttpStatusCode.RequestTimeout
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
