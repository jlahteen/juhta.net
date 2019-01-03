
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestTimeoutException' was thrown.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestTimeoutException' was thrown.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "RequestTimeoutException Specified order number is invalid.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "RequestTimeoutException Specified order number is invalid.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
