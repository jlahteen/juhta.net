
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.GoneException' was thrown.",
                    HttpStatusCode.Gone
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.GoneException' was thrown.",
                    HttpStatusCode.Gone
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "GoneException Specified order number is invalid.",
                    HttpStatusCode.Gone
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
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
                    "GoneException Specified order number is invalid.",
                    HttpStatusCode.Gone
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
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
