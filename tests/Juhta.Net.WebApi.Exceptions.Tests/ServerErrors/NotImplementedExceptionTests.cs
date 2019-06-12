
using Juhta.Net.WebApi.Exceptions.ServerErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
//using System;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ServerErrors
{
    [TestClass]
    public class NotImplementedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_NotImplementedException1_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            NotImplementedException exception1 = null, exception2 = null;

            try
            {
                throw new NotImplementedException();
            }

            catch (NotImplementedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotImplementedException1_ShouldReturn",
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ServerErrors.NotImplementedException' was thrown.",
                    HttpStatusCode.NotImplemented
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (NotImplementedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotImplementedException2_ShouldReturn()
        {
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            NotImplementedException exception1 = null, exception2 = null;

            try
            {
                throw new NotImplementedException("This is a server exception of the type NotImplementedException!");
            }

            catch (NotImplementedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotImplementedException2_ShouldReturn",
                    "This is a server exception of the type NotImplementedException!",
                    HttpStatusCode.NotImplemented
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (NotImplementedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_NotImplementedException3_ShouldReturn()
        {
            System.Exception innerException = null;
            ServerErrorResponse serverErrorResponse1, serverErrorResponse2;
            NotImplementedException exception1 = null, exception2 = null;

            try
            {
                ThrowException();
            }

            catch (System.Exception ex)
            {
                innerException = ex;
            }

            try
            {
                throw new NotImplementedException("This is a server exception of the type NotImplementedException!", innerException);
            }

            catch (NotImplementedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_NotImplementedException3_ShouldReturn",
                    "This is a server exception of the type NotImplementedException!",
                    HttpStatusCode.NotImplemented
                );

                serverErrorResponse1 = ex.ToServerErrorResponse();

                exception1 = ex;
            }

            serverErrorResponse2 = JsonConvert.DeserializeObject<ServerErrorResponse>(JsonConvert.SerializeObject(serverErrorResponse1));

            try
            {
                serverErrorResponse2.Throw();
            }

            catch (NotImplementedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
