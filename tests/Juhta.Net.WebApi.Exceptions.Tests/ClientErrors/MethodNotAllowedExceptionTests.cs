
using Juhta.Net.WebApi.Exceptions.ClientErrors;
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
            ClientError clientError1, clientError2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.MethodNotAllowedException' was thrown.",
                    HttpStatusCode.MethodNotAllowed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.MethodNotAllowedException' was thrown.",
                    HttpStatusCode.MethodNotAllowed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
