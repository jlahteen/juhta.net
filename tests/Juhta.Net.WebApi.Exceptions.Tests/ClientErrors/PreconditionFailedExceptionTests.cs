
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class PreconditionFailedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException();
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.PreconditionFailedException' was thrown.",
                    HttpStatusCode.PreconditionFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber);
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.PreconditionFailedException' was thrown.",
                    HttpStatusCode.PreconditionFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException("PreconditionFailedException Specified order number is invalid.");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException3_ShouldReturn",
                    null,
                    "PreconditionFailedException Specified order number is invalid.",
                    HttpStatusCode.PreconditionFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber, "PreconditionFailedException Specified order number is invalid.");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "PreconditionFailedException Specified order number is invalid.",
                    HttpStatusCode.PreconditionFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
