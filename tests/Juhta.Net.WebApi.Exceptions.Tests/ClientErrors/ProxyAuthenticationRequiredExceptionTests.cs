
using Juhta.Net.WebApi.Exceptions.ClientErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class ProxyAuthenticationRequiredExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ProxyAuthenticationRequiredException' was thrown.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber);
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrorExceptions.ProxyAuthenticationRequiredException' was thrown.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException("ProxyAuthenticationRequiredException Specified order number is invalid.");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException3_ShouldReturn",
                    null,
                    "ProxyAuthenticationRequiredException Specified order number is invalid.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber, "ProxyAuthenticationRequiredException Specified order number is invalid.");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ProxyAuthenticationRequiredException Specified order number is invalid.",
                    HttpStatusCode.ProxyAuthenticationRequired
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new ProxyAuthenticationRequiredException(clientError);
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException5_ShouldReturn",
                    HttpStatusCode.ProxyAuthenticationRequired,
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

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new ProxyAuthenticationRequiredException(new ClientError[]{clientError1, clientError2});
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException6_ShouldReturn",
                    HttpStatusCode.ProxyAuthenticationRequired,
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

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException7_ShouldReturn",
                    HttpStatusCode.ProxyAuthenticationRequired,
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

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException8_ShouldReturn",
                    HttpStatusCode.ProxyAuthenticationRequired,
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

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException9_ShouldReturn",
                    HttpStatusCode.ProxyAuthenticationRequired,
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

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber, "The field content is not allowed at all. Please do better. At least check out the help URL!", "http://juhta.net/helpurls/1233333");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException10_ShouldReturn",
                    HttpStatusCode.ProxyAuthenticationRequired,
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

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ProxyAuthenticationRequiredException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            ProxyAuthenticationRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new ProxyAuthenticationRequiredException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (ProxyAuthenticationRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ProxyAuthenticationRequiredException11_ShouldReturn",
                    HttpStatusCode.ProxyAuthenticationRequired,
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

            catch (ProxyAuthenticationRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
