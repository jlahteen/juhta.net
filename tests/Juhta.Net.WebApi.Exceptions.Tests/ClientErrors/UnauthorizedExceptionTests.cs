
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class UnauthorizedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException();
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnauthorizedException' was thrown.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber);
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UnauthorizedException' was thrown.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException("UnauthorizedException Specified order number is invalid.");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException3_ShouldReturn",
                    null,
                    "UnauthorizedException Specified order number is invalid.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber, "UnauthorizedException Specified order number is invalid.");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "UnauthorizedException Specified order number is invalid.",
                    HttpStatusCode.Unauthorized
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new UnauthorizedException(clientError);
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException5_ShouldReturn",
                    HttpStatusCode.Unauthorized,
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new UnauthorizedException(new ClientError[]{clientError1, clientError2});
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException6_ShouldReturn",
                    HttpStatusCode.Unauthorized,
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException7_ShouldReturn",
                    HttpStatusCode.Unauthorized,
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException8_ShouldReturn",
                    HttpStatusCode.Unauthorized,
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException9_ShouldReturn",
                    HttpStatusCode.Unauthorized,
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber, "UnauthorizedExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException10_ShouldReturn",
                    HttpStatusCode.Unauthorized,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "UnauthorizedExceptionField",
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException11_ShouldReturn",
                    HttpStatusCode.Unauthorized,
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UnauthorizedException12_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UnauthorizedException exception1 = null, exception2 = null;

            try
            {
                throw new UnauthorizedException(ErrorCode.InvalidOrderNumber, "Field.CustomerName", "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (UnauthorizedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UnauthorizedException11_ShouldReturn",
                    HttpStatusCode.Unauthorized,
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

            catch (UnauthorizedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
