
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnsupportedMediaTypeException' was thrown.",
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
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnsupportedMediaTypeException' was thrown.",
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

        [TestMethod]
        public void ThrowAndSerialize_UnsupportedMediaTypeException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new UnsupportedMediaTypeException(clientError);
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException5_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    "MyApiErrorCode.XYZ",
                    "Error XYZ occurred.",
                    "FormX.FieldY",
                    "http://juhta.net"
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
        public void ThrowAndSerialize_UnsupportedMediaTypeException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new UnsupportedMediaTypeException(new ClientError[]{clientError1, clientError2});
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException6_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    "MyApiErrorCode.XYZ1",
                    "Error XYZ1 occurred.",
                    "FormX.FieldY1",
                    "http://juhta.net/errorxyz1",
                    "MyApiErrorCode.XYZ2",
                    "Error XYZ2 occurred.",
                    "FormX.FieldY2",
                    "http://juhta.net/errorxyz2"
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
        public void ThrowAndSerialize_UnsupportedMediaTypeException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException7_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    null,
                    Field.CustomerName.ToString(),
                    null
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
        public void ThrowAndSerialize_UnsupportedMediaTypeException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException8_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    null,
                    "This is an error, please consult the help URL.",
                    null,
                    "http://juhta.net/helpurls/353353"
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
        public void ThrowAndSerialize_UnsupportedMediaTypeException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException9_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better.",
                    Field.CustomerName.ToString(),
                    null
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
        public void ThrowAndSerialize_UnsupportedMediaTypeException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(ErrorCode.InvalidOrderNumber, "UnsupportedMediaTypeExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException10_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "UnsupportedMediaTypeExceptionField",
                    null
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
        public void ThrowAndSerialize_UnsupportedMediaTypeException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException11_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not valid. Please consult the help URL!",
                    Field.CustomerName.ToString(),
                    "http://juhta.net/helpurls/125533"
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
        public void ThrowAndSerialize_UnsupportedMediaTypeException12_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnsupportedMediaTypeException exception1 = null, exception2 = null;

            try
            {
                throw new UnsupportedMediaTypeException(ErrorCode.InvalidOrderNumber, "Field.CustomerName", "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (UnsupportedMediaTypeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnsupportedMediaTypeException11_ShouldReturn",
                    HttpStatusCode.UnsupportedMediaType,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not valid. Please consult the help URL!",
                    "Field.CustomerName",
                    "http://juhta.net/helpurls/125533"
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
