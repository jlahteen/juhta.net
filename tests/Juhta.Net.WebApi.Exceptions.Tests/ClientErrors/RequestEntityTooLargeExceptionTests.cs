
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestEntityTooLargeExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException();
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestEntityTooLargeException' was thrown.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestEntityTooLargeException' was thrown.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException("RequestEntityTooLargeException Specified order number is invalid.");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException3_ShouldReturn",
                    null,
                    "RequestEntityTooLargeException Specified order number is invalid.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber, "RequestEntityTooLargeException Specified order number is invalid.");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "RequestEntityTooLargeException Specified order number is invalid.",
                    HttpStatusCode.RequestEntityTooLarge
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new RequestEntityTooLargeException(clientError);
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException5_ShouldReturn",
                    HttpStatusCode.RequestEntityTooLarge,
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

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new RequestEntityTooLargeException(new ClientError[]{clientError1, clientError2});
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException6_ShouldReturn",
                    HttpStatusCode.RequestEntityTooLarge,
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

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException7_ShouldReturn",
                    HttpStatusCode.RequestEntityTooLarge,
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

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException8_ShouldReturn",
                    HttpStatusCode.RequestEntityTooLarge,
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

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException9_ShouldReturn",
                    HttpStatusCode.RequestEntityTooLarge,
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

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber, "The field content is not allowed at all. Please do better. At least check out the help URL!", "http://juhta.net/helpurls/1233333");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException10_ShouldReturn",
                    HttpStatusCode.RequestEntityTooLarge,
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

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestEntityTooLargeException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestEntityTooLargeException exception1 = null, exception2 = null;

            try
            {
                throw new RequestEntityTooLargeException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (RequestEntityTooLargeException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestEntityTooLargeException11_ShouldReturn",
                    HttpStatusCode.RequestEntityTooLarge,
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

            catch (RequestEntityTooLargeException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
