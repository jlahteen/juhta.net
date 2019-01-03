
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class ForbiddenExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ForbiddenException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ForbiddenException exception1 = null, exception2 = null;

            try
            {
                throw new ForbiddenException();
            }

            catch (ForbiddenException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ForbiddenException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ForbiddenException' was thrown.",
                    HttpStatusCode.Forbidden
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ForbiddenException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ForbiddenException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ForbiddenException exception1 = null, exception2 = null;

            try
            {
                throw new ForbiddenException(ErrorCode.InvalidOrderNumber);
            }

            catch (ForbiddenException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ForbiddenException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ForbiddenException' was thrown.",
                    HttpStatusCode.Forbidden
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ForbiddenException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ForbiddenException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ForbiddenException exception1 = null, exception2 = null;

            try
            {
                throw new ForbiddenException("ForbiddenException Specified order number is invalid.");
            }

            catch (ForbiddenException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ForbiddenException3_ShouldReturn",
                    null,
                    "ForbiddenException Specified order number is invalid.",
                    HttpStatusCode.Forbidden
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ForbiddenException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ForbiddenException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ForbiddenException exception1 = null, exception2 = null;

            try
            {
                throw new ForbiddenException(ErrorCode.InvalidOrderNumber, "ForbiddenException Specified order number is invalid.");
            }

            catch (ForbiddenException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ForbiddenException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ForbiddenException Specified order number is invalid.",
                    HttpStatusCode.Forbidden
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ForbiddenException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
