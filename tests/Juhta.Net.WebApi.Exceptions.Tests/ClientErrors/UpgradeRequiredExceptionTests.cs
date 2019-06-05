
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class UpgradeRequiredExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException();
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UpgradeRequiredException' was thrown.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber);
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.UpgradeRequiredException' was thrown.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException("UpgradeRequiredException Specified order number is invalid.");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException3_ShouldReturn",
                    null,
                    "UpgradeRequiredException Specified order number is invalid.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber, "UpgradeRequiredException Specified order number is invalid.");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "UpgradeRequiredException Specified order number is invalid.",
                    HttpStatusCode.UpgradeRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new UpgradeRequiredException(clientError);
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException5_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new UpgradeRequiredException(new ClientError[]{clientError1, clientError2});
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException6_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException7_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException8_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException9_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber, "UpgradeRequiredExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException10_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "UpgradeRequiredExceptionField",
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException11_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_UpgradeRequiredException12_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            UpgradeRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new UpgradeRequiredException(ErrorCode.InvalidOrderNumber, "Field.CustomerName", "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (UpgradeRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_UpgradeRequiredException11_ShouldReturn",
                    HttpStatusCode.UpgradeRequired,
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

            catch (UpgradeRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
