
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class GoneExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_GoneException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            GoneException exception1 = null, exception2 = null;

            try
            {
                throw new GoneException();
            }

            catch (GoneException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_GoneException1_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.Gone.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.GoneException' was thrown.",
                    HttpStatusCode.Gone
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (GoneException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_GoneException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            GoneException exception1 = null, exception2 = null;

            try
            {
                throw new GoneException(ErrorCode.InvalidOrderNumber);
            }

            catch (GoneException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_GoneException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Gone.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.GoneException' was thrown.",
                    HttpStatusCode.Gone
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (GoneException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_GoneException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            GoneException exception1 = null, exception2 = null;

            try
            {
                throw new GoneException("GoneException Specified order number is invalid.");
            }

            catch (GoneException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_GoneException3_ShouldReturn",
                    null,
                    "ClientError." + HttpStatusCode.Gone.ToString(),
                    "GoneException Specified order number is invalid.",
                    HttpStatusCode.Gone
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (GoneException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_GoneException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            GoneException exception1 = null, exception2 = null;

            try
            {
                throw new GoneException(ErrorCode.InvalidOrderNumber, "GoneException Specified order number is invalid.");
            }

            catch (GoneException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_GoneException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.Gone.ToString(),
                    "GoneException Specified order number is invalid.",
                    HttpStatusCode.Gone
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (GoneException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
