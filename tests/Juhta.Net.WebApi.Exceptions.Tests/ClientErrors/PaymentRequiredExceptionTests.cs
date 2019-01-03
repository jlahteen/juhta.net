
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class PaymentRequiredExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_PaymentRequiredException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PaymentRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new PaymentRequiredException();
            }

            catch (PaymentRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PaymentRequiredException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.PaymentRequiredException' was thrown.",
                    HttpStatusCode.PaymentRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PaymentRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PaymentRequiredException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PaymentRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new PaymentRequiredException(ErrorCode.InvalidOrderNumber);
            }

            catch (PaymentRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PaymentRequiredException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.PaymentRequiredException' was thrown.",
                    HttpStatusCode.PaymentRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PaymentRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PaymentRequiredException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PaymentRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new PaymentRequiredException("PaymentRequiredException Specified order number is invalid.");
            }

            catch (PaymentRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PaymentRequiredException3_ShouldReturn",
                    null,
                    "PaymentRequiredException Specified order number is invalid.",
                    HttpStatusCode.PaymentRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PaymentRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PaymentRequiredException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PaymentRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new PaymentRequiredException(ErrorCode.InvalidOrderNumber, "PaymentRequiredException Specified order number is invalid.");
            }

            catch (PaymentRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PaymentRequiredException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "PaymentRequiredException Specified order number is invalid.",
                    HttpStatusCode.PaymentRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PaymentRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
