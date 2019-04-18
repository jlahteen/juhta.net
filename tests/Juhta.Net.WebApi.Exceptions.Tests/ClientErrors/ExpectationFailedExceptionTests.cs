
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class ExpectationFailedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException();
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ExpectationFailedException' was thrown.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber);
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ExpectationFailedException' was thrown.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException("ExpectationFailedException Specified order number is invalid.");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException3_ShouldReturn",
                    null,
                    "ExpectationFailedException Specified order number is invalid.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, "ExpectationFailedException Specified order number is invalid.");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ExpectationFailedException Specified order number is invalid.",
                    HttpStatusCode.ExpectationFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new ExpectationFailedException(clientError);
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException5_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new ExpectationFailedException(new ClientError[]{clientError1, clientError2});
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException6_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException7_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException8_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException9_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, "ExpectationFailedExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException10_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "ExpectationFailedExceptionField",
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException11_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException12_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, "Field.CustomerName", "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException11_ShouldReturn",
                    HttpStatusCode.ExpectationFailed,
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

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
