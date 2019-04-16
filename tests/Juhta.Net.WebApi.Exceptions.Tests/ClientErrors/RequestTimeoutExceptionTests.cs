
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestTimeoutExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException();
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestTimeoutException' was thrown.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestTimeoutException' was thrown.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException("RequestTimeoutException Specified order number is invalid.");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException3_ShouldReturn",
                    null,
                    "RequestTimeoutException Specified order number is invalid.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber, "RequestTimeoutException Specified order number is invalid.");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "RequestTimeoutException Specified order number is invalid.",
                    HttpStatusCode.RequestTimeout
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new RequestTimeoutException(clientError);
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException5_ShouldReturn",
                    HttpStatusCode.RequestTimeout,
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

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new RequestTimeoutException(new ClientError[]{clientError1, clientError2});
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException6_ShouldReturn",
                    HttpStatusCode.RequestTimeout,
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

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException7_ShouldReturn",
                    HttpStatusCode.RequestTimeout,
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

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException8_ShouldReturn",
                    HttpStatusCode.RequestTimeout,
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

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException9_ShouldReturn",
                    HttpStatusCode.RequestTimeout,
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

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber, "RequestTimeoutExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException10_ShouldReturn",
                    HttpStatusCode.RequestTimeout,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "RequestTimeoutExceptionField",
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

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestTimeoutException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestTimeoutException exception1 = null, exception2 = null;

            try
            {
                throw new RequestTimeoutException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (RequestTimeoutException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestTimeoutException11_ShouldReturn",
                    HttpStatusCode.RequestTimeout,
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

            catch (RequestTimeoutException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
