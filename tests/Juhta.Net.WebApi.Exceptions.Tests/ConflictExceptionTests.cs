
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests
{
    [TestClass]
    public class ConflictExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ConflictException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.Conflict.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ConflictException' was thrown.",
                    HttpStatusCode.Conflict
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            ConflictException exception1 = null, exception2 = null;

            try
            {
                throw new ConflictException(MyApiError.InvalidOrderNumber);
            }

            catch (ConflictException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ConflictException2_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Conflict.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ConflictException' was thrown.",
                    HttpStatusCode.Conflict
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.Conflict.ToString(),
                    "ConflictException Specified order number is invalid.",
                    HttpStatusCode.Conflict
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            ConflictException exception1 = null, exception2 = null;

            try
            {
                throw new ConflictException(MyApiError.InvalidOrderNumber, "ConflictException Specified order number is invalid.");
            }

            catch (ConflictException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ConflictException4_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Conflict.ToString(),
                    "ConflictException Specified order number is invalid.",
                    HttpStatusCode.Conflict
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
