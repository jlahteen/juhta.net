
using Juhta.Net.WebApi.Exceptions.ServerErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ServerErrors
{
    [TestClass]
    public class ServiceUnavailableExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ServiceUnavailableException1_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            ServiceUnavailableException exception1 = null, exception2 = null;

            try
            {
                throw new ServiceUnavailableException();
            }

            catch (ServiceUnavailableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ServiceUnavailableException1_ShouldReturn",
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrors.ServiceUnavailableException' was thrown.",
                    HttpStatusCode.ServiceUnavailable
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (ServiceUnavailableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ServiceUnavailableException2_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            ServiceUnavailableException exception1 = null, exception2 = null;

            try
            {
                throw new ServiceUnavailableException("This is a server exception of the type ServiceUnavailableException!");
            }

            catch (ServiceUnavailableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ServiceUnavailableException2_ShouldReturn",
                    "This is a server exception of the type ServiceUnavailableException!",
                    HttpStatusCode.ServiceUnavailable
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (ServiceUnavailableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ServiceUnavailableException3_ShouldReturn()
        {
            Exception innerException = null;
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            ServiceUnavailableException exception1 = null, exception2 = null;

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
                throw new ServiceUnavailableException("This is a server exception of the type ServiceUnavailableException!", innerException);
            }

            catch (ServiceUnavailableException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ServiceUnavailableException3_ShouldReturn",
                    "This is a server exception of the type ServiceUnavailableException!",
                    HttpStatusCode.ServiceUnavailable
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (ServiceUnavailableException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
