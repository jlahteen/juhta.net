
using Juhta.Net.WebApi.Exceptions.ClientErrors;
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.UnsupportedMediaType.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnsupportedMediaTypeException' was thrown.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(MyApiError.InvalidOrderNumber);
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException2_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.UnsupportedMediaType.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnsupportedMediaTypeException' was thrown.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
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
                    "ClientError." + HttpStatusCode.UnsupportedMediaType.ToString(),
                    "UnsupportedMediaTypeException Specified order number is invalid.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
            ClientError clientError1, clientError2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(MyApiError.InvalidOrderNumber, "UnsupportedMediaTypeException Specified order number is invalid.");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException4_ShouldReturn",
                    MyApiError.InvalidOrderNumber.ToString(),
                    "ClientError." + HttpStatusCode.UnsupportedMediaType.ToString(),
                    "UnsupportedMediaTypeException Specified order number is invalid.",
                    HttpStatusCode.UnsupportedMediaType
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
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
