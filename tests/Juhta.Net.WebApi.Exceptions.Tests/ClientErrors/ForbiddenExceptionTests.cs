
using Juhta.Net.WebApi.Exceptions.ClientErrors;
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.Forbidden.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ForbiddenException' was thrown.",
                    HttpStatusCode.Forbidden
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            ForbiddenException exception1 = null, exception2 = null;

            try
            {
                throw new ForbiddenException(MyApiError.InvalidOrderNumber);
            }

            catch (ForbiddenException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ForbiddenException2_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Forbidden.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ForbiddenException' was thrown.",
                    HttpStatusCode.Forbidden
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.Forbidden.ToString(),
                    "ForbiddenException Specified order number is invalid.",
                    HttpStatusCode.Forbidden
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            ForbiddenException exception1 = null, exception2 = null;

            try
            {
                throw new ForbiddenException(MyApiError.InvalidOrderNumber, "ForbiddenException Specified order number is invalid.");
            }

            catch (ForbiddenException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ForbiddenException4_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Forbidden.ToString(),
                    "ForbiddenException Specified order number is invalid.",
                    HttpStatusCode.Forbidden
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
