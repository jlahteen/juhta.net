
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class NotAcceptableExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException();
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.NotAcceptableException' was thrown.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber);
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.NotAcceptableException' was thrown.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException("NotAcceptableException Specified order number is invalid.");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException3_ShouldReturn",
                    null,
                    "NotAcceptableException Specified order number is invalid.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber, "NotAcceptableException Specified order number is invalid.");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "NotAcceptableException Specified order number is invalid.",
                    HttpStatusCode.NotAcceptable
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new NotAcceptableException(clientError);
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException5_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new NotAcceptableException(new ClientError[]{clientError1, clientError2});
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException6_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException7_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException8_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException9_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber, "NotAcceptableExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException10_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "NotAcceptableExceptionField",
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException11_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotAcceptableException12_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            NotAcceptableException exception1 = null, exception2 = null;

            try
            {
                throw new NotAcceptableException(ErrorCode.InvalidOrderNumber, "Field.CustomerName", "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (NotAcceptableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotAcceptableException11_ShouldReturn",
                    HttpStatusCode.NotAcceptable,
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

            catch (NotAcceptableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
