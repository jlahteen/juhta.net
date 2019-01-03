
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class ConflictExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ConflictException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ConflictException exception1 = null, exception2 = null;

            try
            {
                throw new ConflictException();
            }

            catch (ConflictException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ConflictException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ConflictException' was thrown.",
                    HttpStatusCode.Conflict
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ConflictException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ConflictException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ConflictException exception1 = null, exception2 = null;

            try
            {
                throw new ConflictException(ErrorCode.InvalidOrderNumber);
            }

            catch (ConflictException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ConflictException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ConflictException' was thrown.",
                    HttpStatusCode.Conflict
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ConflictException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ConflictException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ConflictException exception1 = null, exception2 = null;

            try
            {
                throw new ConflictException("ConflictException Specified order number is invalid.");
            }

            catch (ConflictException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ConflictException3_ShouldReturn",
                    null,
                    "ConflictException Specified order number is invalid.",
                    HttpStatusCode.Conflict
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ConflictException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ConflictException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ConflictException exception1 = null, exception2 = null;

            try
            {
                throw new ConflictException(ErrorCode.InvalidOrderNumber, "ConflictException Specified order number is invalid.");
            }

            catch (ConflictException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ConflictException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ConflictException Specified order number is invalid.",
                    HttpStatusCode.Conflict
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ConflictException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
