
using Juhta.Net.WebApi.Exceptions.ClientErrors;
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.PaymentRequired.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.PaymentRequiredException' was thrown.",
                    HttpStatusCode.PaymentRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            PaymentRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new PaymentRequiredException(MyApiError.InvalidOrderNumber);
            }

            catch (PaymentRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PaymentRequiredException2_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.PaymentRequired.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.PaymentRequiredException' was thrown.",
                    HttpStatusCode.PaymentRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.PaymentRequired.ToString(),
                    "PaymentRequiredException Specified order number is invalid.",
                    HttpStatusCode.PaymentRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            PaymentRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new PaymentRequiredException(MyApiError.InvalidOrderNumber, "PaymentRequiredException Specified order number is invalid.");
            }

            catch (PaymentRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PaymentRequiredException4_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.PaymentRequired.ToString(),
                    "PaymentRequiredException Specified order number is invalid.",
                    HttpStatusCode.PaymentRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
