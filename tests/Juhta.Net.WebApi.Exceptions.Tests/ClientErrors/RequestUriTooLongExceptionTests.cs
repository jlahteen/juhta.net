
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestUriTooLongExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException();
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestUriTooLongException' was thrown.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestUriTooLongException' was thrown.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException("RequestUriTooLongException Specified order number is invalid.");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException3_ShouldReturn",
                    null,
                    "RequestUriTooLongException Specified order number is invalid.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber, "RequestUriTooLongException Specified order number is invalid.");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "RequestUriTooLongException Specified order number is invalid.",
                    HttpStatusCode.RequestUriTooLong
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new RequestUriTooLongException(clientError);
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException5_ShouldReturn",
                    HttpStatusCode.RequestUriTooLong,
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

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new RequestUriTooLongException(new ClientError[]{clientError1, clientError2});
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException6_ShouldReturn",
                    HttpStatusCode.RequestUriTooLong,
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

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException7_ShouldReturn",
                    HttpStatusCode.RequestUriTooLong,
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

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException8_ShouldReturn",
                    HttpStatusCode.RequestUriTooLong,
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

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException9_ShouldReturn",
                    HttpStatusCode.RequestUriTooLong,
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

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber, "The field content is not allowed at all. Please do better. At least check out the help URL!", "http://juhta.net/helpurls/1233333");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException10_ShouldReturn",
                    HttpStatusCode.RequestUriTooLong,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    null,
                    "http://juhta.net/helpurls/1233333"
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestUriTooLongException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestUriTooLongException exception1 = null, exception2 = null;

            try
            {
                throw new RequestUriTooLongException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (RequestUriTooLongException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestUriTooLongException11_ShouldReturn",
                    HttpStatusCode.RequestUriTooLong,
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

            catch (RequestUriTooLongException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
