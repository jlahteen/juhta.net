
using Juhta.Net.WebApi.Exceptions.ServerErrorExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ServerErrors
{
    [TestClass]
    public class InternalServerErrorExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_InternalServerErrorException1_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            InternalServerErrorException exception1 = null, exception2 = null;

            try
            {
                throw new InternalServerErrorException();
            }

            catch (InternalServerErrorException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_InternalServerErrorException1_ShouldReturn",
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrorExceptions.InternalServerErrorException' was thrown.",
                    HttpStatusCode.InternalServerError
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (InternalServerErrorException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_InternalServerErrorException2_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            InternalServerErrorException exception1 = null, exception2 = null;

            try
            {
                throw new InternalServerErrorException("This is a server exception of the type InternalServerErrorException!");
            }

            catch (InternalServerErrorException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_InternalServerErrorException2_ShouldReturn",
                    "This is a server exception of the type InternalServerErrorException!",
                    HttpStatusCode.InternalServerError
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (InternalServerErrorException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_InternalServerErrorException3_ShouldReturn()
        {
            Exception innerException = null;
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            InternalServerErrorException exception1 = null, exception2 = null;

            try
            {
                ThrowException();
            }

            catch (Exception ex)
            {
                innerException = ex;
            }

            try
            {
                throw new InternalServerErrorException("This is a server exception of the type InternalServerErrorException!", innerException);
            }

            catch (InternalServerErrorException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_InternalServerErrorException3_ShouldReturn",
                    "This is a server exception of the type InternalServerErrorException!",
                    HttpStatusCode.InternalServerError
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (InternalServerErrorException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
