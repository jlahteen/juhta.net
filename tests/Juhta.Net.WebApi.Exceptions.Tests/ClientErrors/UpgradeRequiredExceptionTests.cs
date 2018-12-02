
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class UpgradeRequiredExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException();
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException1_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.UpgradeRequired.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UpgradeRequiredException' was thrown.",
                    HttpStatusCode.UpgradeRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(MyApiError.InvalidOrderNumber);
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException2_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.UpgradeRequired.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UpgradeRequiredException' was thrown.",
                    HttpStatusCode.UpgradeRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException("UpgradeRequiredException Specified order number is invalid.");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException3_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.UpgradeRequired.ToString(),
                    "UpgradeRequiredException Specified order number is invalid.",
                    HttpStatusCode.UpgradeRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(MyApiError.InvalidOrderNumber, "UpgradeRequiredException Specified order number is invalid.");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException4_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.UpgradeRequired.ToString(),
                    "UpgradeRequiredException Specified order number is invalid.",
                    HttpStatusCode.UpgradeRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
