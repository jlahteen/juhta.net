
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ProxyAuthenticationRequiredException' was thrown.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ProxyAuthenticationRequiredException' was thrown.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "ProxyAuthenticationRequiredException Specified order number is invalid.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "ProxyAuthenticationRequiredException Specified order number is invalid.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
