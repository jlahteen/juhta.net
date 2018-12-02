
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class UnauthorizedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException();
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException1_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.Unauthorized.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnauthorizedException' was thrown.",
                    HttpStatusCode.Unauthorized
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(MyApiError.InvalidOrderNumber);
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException2_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Unauthorized.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnauthorizedException' was thrown.",
                    HttpStatusCode.Unauthorized
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException("UnauthorizedException Specified order number is invalid.");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException3_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.Unauthorized.ToString(),
                    "UnauthorizedException Specified order number is invalid.",
                    HttpStatusCode.Unauthorized
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(MyApiError.InvalidOrderNumber, "UnauthorizedException Specified order number is invalid.");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException4_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Unauthorized.ToString(),
                    "UnauthorizedException Specified order number is invalid.",
                    HttpStatusCode.Unauthorized
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
