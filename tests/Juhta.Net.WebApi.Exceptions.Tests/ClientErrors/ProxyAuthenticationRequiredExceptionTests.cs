
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class ProxyAuthenticationRequiredExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException1_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.ProxyAuthenticationRequired.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ProxyAuthenticationRequiredException' was thrown.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber);
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.ProxyAuthenticationRequired.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ProxyAuthenticationRequiredException' was thrown.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException("ProxyAuthenticationRequiredException Specified order number is invalid.");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException3_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.ProxyAuthenticationRequired.ToString(),
                    "ProxyAuthenticationRequiredException Specified order number is invalid.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber, "ProxyAuthenticationRequiredException Specified order number is invalid.");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.ProxyAuthenticationRequired.ToString(),
                    "ProxyAuthenticationRequiredException Specified order number is invalid.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
