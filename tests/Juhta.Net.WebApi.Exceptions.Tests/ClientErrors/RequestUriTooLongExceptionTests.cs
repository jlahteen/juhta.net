
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestUriTooLongException' was thrown.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestUriTooLongException' was thrown.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "RequestUriTooLongException Specified order number is invalid.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "RequestUriTooLongException Specified order number is invalid.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
