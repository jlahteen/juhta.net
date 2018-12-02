
using Juhta.Net.WebApi.Exceptions.ClientErrors;
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.NotAcceptable.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.NotAcceptableException' was thrown.",
                    HttpStatusCode.NotAcceptable
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.NotAcceptable.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.NotAcceptableException' was thrown.",
                    HttpStatusCode.NotAcceptable
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.NotAcceptable.ToString(),
                    "NotAcceptableException Specified order number is invalid.",
                    HttpStatusCode.NotAcceptable
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.NotAcceptable.ToString(),
                    "NotAcceptableException Specified order number is invalid.",
                    HttpStatusCode.NotAcceptable
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
