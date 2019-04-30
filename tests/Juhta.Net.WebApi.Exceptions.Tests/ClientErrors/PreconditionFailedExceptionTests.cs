
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class PreconditionFailedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException();
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.PreconditionFailedException' was thrown.",
                    HttpStatusCode.PreconditionFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber);
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.PreconditionFailedException' was thrown.",
                    HttpStatusCode.PreconditionFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException("PreconditionFailedException Specified order number is invalid.");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException3_ShouldReturn",
                    null,
                    "PreconditionFailedException Specified order number is invalid.",
                    HttpStatusCode.PreconditionFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber, "PreconditionFailedException Specified order number is invalid.");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "PreconditionFailedException Specified order number is invalid.",
                    HttpStatusCode.PreconditionFailed
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new PreconditionFailedException(clientError);
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException5_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new PreconditionFailedException(new ClientError[]{clientError1, clientError2});
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException6_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException7_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException8_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException9_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber, "PreconditionFailedExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException10_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "PreconditionFailedExceptionField",
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException11_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_PreconditionFailedException12_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            PreconditionFailedException exception1 = null, exception2 = null;

            try
            {
                throw new PreconditionFailedException(ErrorCode.InvalidOrderNumber, "Field.CustomerName", "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (PreconditionFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_PreconditionFailedException11_ShouldReturn",
                    HttpStatusCode.PreconditionFailed,
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

            catch (PreconditionFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
