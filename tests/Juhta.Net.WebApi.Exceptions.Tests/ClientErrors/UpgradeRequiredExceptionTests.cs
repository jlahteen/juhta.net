
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.UpgradeRequiredException' was thrown.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber);
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.UpgradeRequiredException' was thrown.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "UpgradeRequiredException Specified order number is invalid.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber, "UpgradeRequiredException Specified order number is invalid.");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "UpgradeRequiredException Specified order number is invalid.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
