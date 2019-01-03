
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class MethodNotAllowedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_MethodNotAllowedException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            MethodNotAllowedException exception1 = null, exception2 = null;

            try
            {
                throw new MethodNotAllowedException();
            }

            catch (MethodNotAllowedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_MethodNotAllowedException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.MethodNotAllowedException' was thrown.",
                    HttpStatusCode.MethodNotAllowed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (MethodNotAllowedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_MethodNotAllowedException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            MethodNotAllowedException exception1 = null, exception2 = null;

            try
            {
                throw new MethodNotAllowedException(ErrorCode.InvalidOrderNumber);
            }

            catch (MethodNotAllowedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_MethodNotAllowedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.MethodNotAllowedException' was thrown.",
                    HttpStatusCode.MethodNotAllowed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (MethodNotAllowedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_MethodNotAllowedException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            MethodNotAllowedException exception1 = null, exception2 = null;

            try
            {
                throw new MethodNotAllowedException("MethodNotAllowedException Specified order number is invalid.");
            }

            catch (MethodNotAllowedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_MethodNotAllowedException3_ShouldReturn",
                    null,
                    "MethodNotAllowedException Specified order number is invalid.",
                    HttpStatusCode.MethodNotAllowed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (MethodNotAllowedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_MethodNotAllowedException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            MethodNotAllowedException exception1 = null, exception2 = null;

            try
            {
                throw new MethodNotAllowedException(ErrorCode.InvalidOrderNumber, "MethodNotAllowedException Specified order number is invalid.");
            }

            catch (MethodNotAllowedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_MethodNotAllowedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "MethodNotAllowedException Specified order number is invalid.",
                    HttpStatusCode.MethodNotAllowed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (MethodNotAllowedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
