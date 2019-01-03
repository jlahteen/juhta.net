
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestEntityTooLargeException' was thrown.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestEntityTooLargeException' was thrown.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
