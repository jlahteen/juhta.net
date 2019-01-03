
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.PreconditionFailedException' was thrown.",
                    HttpStatusCode.PreconditionFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.PreconditionFailedException' was thrown.",
                    HttpStatusCode.PreconditionFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
