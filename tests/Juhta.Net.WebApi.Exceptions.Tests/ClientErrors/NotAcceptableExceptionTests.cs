
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class NotAcceptableExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException();
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.NotAcceptableException' was thrown.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber);
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.NotAcceptableException' was thrown.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException("NotAcceptableException Specified order number is invalid.");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException3_ShouldReturn",
                    null,
                    "NotAcceptableException Specified order number is invalid.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber, "NotAcceptableException Specified order number is invalid.");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "NotAcceptableException Specified order number is invalid.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
