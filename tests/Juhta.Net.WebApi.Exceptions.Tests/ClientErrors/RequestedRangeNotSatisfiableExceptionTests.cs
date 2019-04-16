
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class RequestedRangeNotSatisfiableExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestedRangeNotSatisfiableException' was thrown.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber);
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.RequestedRangeNotSatisfiableException' was thrown.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException("RequestedRangeNotSatisfiableException Specified order number is invalid.");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException3_ShouldReturn",
                    null,
                    "RequestedRangeNotSatisfiableException Specified order number is invalid.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber, "RequestedRangeNotSatisfiableException Specified order number is invalid.");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "RequestedRangeNotSatisfiableException Specified order number is invalid.",
                    HttpStatusCode.RequestedRangeNotSatisfiable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new RequestedRangeNotSatisfiableException(clientError);
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException5_ShouldReturn",
                    HttpStatusCode.RequestedRangeNotSatisfiable,
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

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new RequestedRangeNotSatisfiableException(new ClientError[]{clientError1, clientError2});
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException6_ShouldReturn",
                    HttpStatusCode.RequestedRangeNotSatisfiable,
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

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException7_ShouldReturn",
                    HttpStatusCode.RequestedRangeNotSatisfiable,
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

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException8_ShouldReturn",
                    HttpStatusCode.RequestedRangeNotSatisfiable,
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

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException9_ShouldReturn",
                    HttpStatusCode.RequestedRangeNotSatisfiable,
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

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber, "RequestedRangeNotSatisfiableExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException10_ShouldReturn",
                    HttpStatusCode.RequestedRangeNotSatisfiable,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "RequestedRangeNotSatisfiableExceptionField",
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

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_RequestedRangeNotSatisfiableException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            RequestedRangeNotSatisfiableException exception1 = null, exception2 = null;

            try
            {
                throw new RequestedRangeNotSatisfiableException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (RequestedRangeNotSatisfiableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_RequestedRangeNotSatisfiableException11_ShouldReturn",
                    HttpStatusCode.RequestedRangeNotSatisfiable,
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

            catch (RequestedRangeNotSatisfiableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
