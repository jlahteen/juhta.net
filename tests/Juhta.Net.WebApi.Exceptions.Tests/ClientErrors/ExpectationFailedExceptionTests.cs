
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class ExpectationFailedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException();
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ExpectationFailedException' was thrown.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber);
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ExpectationFailedException' was thrown.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException("ExpectationFailedException Specified order number is invalid.");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException3_ShouldReturn",
                    null,
                    "ExpectationFailedException Specified order number is invalid.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, "ExpectationFailedException Specified order number is invalid.");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ExpectationFailedException Specified order number is invalid.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
