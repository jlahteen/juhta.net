
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestedRangeNotSatisfiableExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestedRangeNotSatisfiableException' was thrown.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestedRangeNotSatisfiableException' was thrown.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException("RequestedRangeNotSatisfiableException Specified order number is invalid.");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException3_ShouldReturn",
                    null,
                    "RequestedRangeNotSatisfiableException Specified order number is invalid.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber, "RequestedRangeNotSatisfiableException Specified order number is invalid.");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "RequestedRangeNotSatisfiableException Specified order number is invalid.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
