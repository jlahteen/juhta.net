
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestUriTooLongExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException();
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException1_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.RequestUriTooLong.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestUriTooLongException' was thrown.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.RequestUriTooLong.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestUriTooLongException' was thrown.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException("RequestUriTooLongException Specified order number is invalid.");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException3_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.RequestUriTooLong.ToString(),
                    "RequestUriTooLongException Specified order number is invalid.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber, "RequestUriTooLongException Specified order number is invalid.");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.RequestUriTooLong.ToString(),
                    "RequestUriTooLongException Specified order number is invalid.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
