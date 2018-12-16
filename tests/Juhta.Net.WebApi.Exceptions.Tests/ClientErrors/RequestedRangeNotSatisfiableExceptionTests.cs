
using Juhta.Net.WebApi.Exceptions.ClientErrors;
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
            ClientError clientError1, clientError2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestedRangeNotSatisfiableException' was thrown.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.RequestedRangeNotSatisfiableException' was thrown.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
