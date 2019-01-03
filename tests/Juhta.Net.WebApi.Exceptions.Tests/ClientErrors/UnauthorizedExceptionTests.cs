
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.UnauthorizedException' was thrown.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber);
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.UnauthorizedException' was thrown.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "UnauthorizedException Specified order number is invalid.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber, "UnauthorizedException Specified order number is invalid.");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "UnauthorizedException Specified order number is invalid.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
