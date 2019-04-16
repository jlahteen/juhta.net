
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class LengthRequiredExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException();
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.LengthRequiredException' was thrown.",
                    HttpStatusCode.LengthRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber);
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.LengthRequiredException' was thrown.",
                    HttpStatusCode.LengthRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException("LengthRequiredException Specified order number is invalid.");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException3_ShouldReturn",
                    null,
                    "LengthRequiredException Specified order number is invalid.",
                    HttpStatusCode.LengthRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber, "LengthRequiredException Specified order number is invalid.");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "LengthRequiredException Specified order number is invalid.",
                    HttpStatusCode.LengthRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new LengthRequiredException(clientError);
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException5_ShouldReturn",
                    HttpStatusCode.LengthRequired,
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

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new LengthRequiredException(new ClientError[]{clientError1, clientError2});
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException6_ShouldReturn",
                    HttpStatusCode.LengthRequired,
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

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException7_ShouldReturn",
                    HttpStatusCode.LengthRequired,
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

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException8_ShouldReturn",
                    HttpStatusCode.LengthRequired,
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

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException9_ShouldReturn",
                    HttpStatusCode.LengthRequired,
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

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber, "LengthRequiredExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException10_ShouldReturn",
                    HttpStatusCode.LengthRequired,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "LengthRequiredExceptionField",
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

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException11_ShouldReturn",
                    HttpStatusCode.LengthRequired,
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

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
