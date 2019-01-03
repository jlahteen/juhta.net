
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class UnsupportedMediaTypeExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_UnsupportedMediaTypeException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException();
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.UnsupportedMediaTypeException' was thrown.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnsupportedMediaTypeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnsupportedMediaTypeException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(ErrorCode.InvalidOrderNumber);
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.UnsupportedMediaTypeException' was thrown.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnsupportedMediaTypeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnsupportedMediaTypeException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException("UnsupportedMediaTypeException Specified order number is invalid.");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException3_ShouldReturn",
                    null,
                    "UnsupportedMediaTypeException Specified order number is invalid.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnsupportedMediaTypeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnsupportedMediaTypeException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(ErrorCode.InvalidOrderNumber, "UnsupportedMediaTypeException Specified order number is invalid.");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "UnsupportedMediaTypeException Specified order number is invalid.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnsupportedMediaTypeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
