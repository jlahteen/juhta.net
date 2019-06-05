
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class BadRequestExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException1_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException();
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.BadRequestException' was thrown.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException2_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber);
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.BadRequestException' was thrown.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException3_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException("BadRequestException Specified order number is invalid.");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException3_ShouldReturn",
                    null,
                    "BadRequestException Specified order number is invalid.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException4_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber, "BadRequestException Specified order number is invalid.");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "BadRequestException Specified order number is invalid.",
                    HttpStatusCode.BadRequest
                );

                clientErrorResponse1 = ex.ToClientErrorResponse();

                exception1 = ex;
            }

            clientErrorResponse2 = JsonConvert.DeserializeObject<ClientErrorResponse>(JsonConvert.SerializeObject(clientErrorResponse1));

            try
            {
                clientErrorResponse2.Throw();
            }

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException5_ShouldReturn()
        {
            ClientError clientError;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                clientError = new ClientError{Code = "MyApiErrorCode.XYZ", Message = "Error XYZ occurred.", Field = "FormX.FieldY", HelpUrl = "http://juhta.net"};

                throw new BadRequestException(clientError);
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException5_ShouldReturn",
                    HttpStatusCode.BadRequest,
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException6_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                clientError1 = new ClientError{Code = "MyApiErrorCode.XYZ1", Message = "Error XYZ1 occurred.", Field = "FormX.FieldY1", HelpUrl = "http://juhta.net/errorxyz1"};

                clientError2 = new ClientError{Code = "MyApiErrorCode.XYZ2", Message = "Error XYZ2 occurred.", Field = "FormX.FieldY2", HelpUrl = "http://juhta.net/errorxyz2"};

                throw new BadRequestException(new ClientError[]{clientError1, clientError2});
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException6_ShouldReturn",
                    HttpStatusCode.BadRequest,
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException7_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber, Field.CustomerName);
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException7_ShouldReturn",
                    HttpStatusCode.BadRequest,
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException8_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException("This is an error, please consult the help URL.", "http://juhta.net/helpurls/353353");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException8_ShouldReturn",
                    HttpStatusCode.BadRequest,
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException9_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not allowed at all. Please do better.");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException9_ShouldReturn",
                    HttpStatusCode.BadRequest,
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException10_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber, "BadRequestExceptionField", "The field content is not allowed at all. Please do better. At least check out the help URL!");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException10_ShouldReturn",
                    HttpStatusCode.BadRequest,
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "The field content is not allowed at all. Please do better. At least check out the help URL!",
                    "BadRequestExceptionField",
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException11_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber, Field.CustomerName, "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException11_ShouldReturn",
                    HttpStatusCode.BadRequest,
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_BadRequestException12_ShouldReturn()
        {
            ClientErrorResponse clientErrorResponse1, clientErrorResponse2;
            BadRequestException exception1 = null, exception2 = null;

            try
            {
                throw new BadRequestException(ErrorCode.InvalidOrderNumber, "Field.CustomerName", "The field content is not valid. Please consult the help URL!", "http://juhta.net/helpurls/125533");
            }

            catch (BadRequestException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_BadRequestException11_ShouldReturn",
                    HttpStatusCode.BadRequest,
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

            catch (BadRequestException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ToString_ClientErrorArrayConstructor_ShouldReturn()
        {
            ClientError[] clientErrors;
            string[] lines;

            try
            {
                clientErrors = new ClientError[]{new ClientError(), new ClientError {Code = "CodeValue", Field = "FieldValue", HelpUrl = "HelpUrlValue", Message = "MessageValue"}};

                throw new BadRequestException(clientErrors);
            }

            catch (BadRequestException ex)
            {
                lines = ex.ToString().Split(Environment.NewLine);

                Assert.AreEqual<string>("   --- ClientErrorException properties ---", lines[lines.Length - 15]);

                Assert.AreEqual<string>("     \"Errors\": [", lines[lines.Length - 14]);

                Assert.AreEqual<string>("       {", lines[lines.Length - 13]);

                Assert.AreEqual<string>("         \"Code\": null,", lines[lines.Length - 12]);

                Assert.AreEqual<string>("         \"Field\": null,", lines[lines.Length - 11]);

                Assert.AreEqual<string>("         \"Message\": null,", lines[lines.Length - 10]);

                Assert.AreEqual<string>("         \"HelpUrl\": null", lines[lines.Length - 9]);

                Assert.AreEqual<string>("       },", lines[lines.Length - 8]);

                Assert.AreEqual<string>("       {", lines[lines.Length - 7]);

                Assert.AreEqual<string>("         \"Code\": \"CodeValue\",", lines[lines.Length - 6]);

                Assert.AreEqual<string>("         \"Field\": \"FieldValue\",", lines[lines.Length - 5]);

                Assert.AreEqual<string>("         \"Message\": \"MessageValue\",", lines[lines.Length - 4]);

                Assert.AreEqual<string>("         \"HelpUrl\": \"HelpUrlValue\"", lines[lines.Length - 3]);

                Assert.AreEqual<string>("       }", lines[lines.Length - 2]);

                Assert.AreEqual<string>("     ]", lines[lines.Length - 1]);
            }
        }

        [TestMethod]
        public void ToString_DefaultConstructor_ShouldReturn()
        {
            string[] lines;

            try
            {
                throw new BadRequestException();
            }

            catch (BadRequestException ex)
            {
                lines = ex.ToString().Split(Environment.NewLine);

                Assert.AreEqual<string>("   --- ClientErrorException properties ---", lines[lines.Length - 2]);

                Assert.AreEqual<string>("     \"Errors\": null", lines[lines.Length - 1]);
            }
        }

        [TestMethod]
        public void ToString_NullClientErrorConstructor_ShouldReturn()
        {
            ClientError clientError = null;
            string[] lines;

            try
            {
                throw new BadRequestException(clientError);
            }

            catch (BadRequestException ex)
            {
                lines = ex.ToString().Split(Environment.NewLine);

                Assert.AreEqual<string>("   --- ClientErrorException properties ---", lines[lines.Length - 4]);

                Assert.AreEqual<string>("     \"Errors\": [", lines[lines.Length - 3]);

                Assert.AreEqual<string>("       null", lines[lines.Length - 2]);

                Assert.AreEqual<string>("     ]", lines[lines.Length - 1]);
            }
        }

        [TestMethod]
        public void ToString_SimpleClientErrorConstructor_ShouldReturn()
        {
            ClientError clientError = new ClientError();
            string[] lines;

            try
            {
                throw new BadRequestException(clientError);
            }

            catch (BadRequestException ex)
            {
                lines = ex.ToString().Split(Environment.NewLine);

                Assert.AreEqual<string>("   --- ClientErrorException properties ---", lines[lines.Length - 9]);

                Assert.AreEqual<string>("     \"Errors\": [", lines[lines.Length - 8]);

                Assert.AreEqual<string>("       {", lines[lines.Length - 7]);

                Assert.AreEqual<string>("         \"Code\": null,", lines[lines.Length - 6]);

                Assert.AreEqual<string>("         \"Field\": null,", lines[lines.Length - 5]);

                Assert.AreEqual<string>("         \"Message\": null,", lines[lines.Length - 4]);

                Assert.AreEqual<string>("         \"HelpUrl\": null", lines[lines.Length - 3]);

                Assert.AreEqual<string>("       }", lines[lines.Length - 2]);

                Assert.AreEqual<string>("     ]", lines[lines.Length - 1]);
            }
        }

        #endregion
    }
}
