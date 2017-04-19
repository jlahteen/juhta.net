
using Juhta.Net;
using System;
using System.IO;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class StartupTests : TestClassBase
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloseFramework_FrameworkNotInitialized_ShouldThrowInvalidOperationException()
        {
            DeleteConfigFiles(".");

            Startup.CloseFramework();
        }

        [TestMethod]
        public void CloseFramework_FrameworkInitialized_ShouldReturn()
        {
            DeleteConfigFiles(".");

            Startup.InitializeFramework();

            Startup.CloseFramework();
        }

        [TestMethod]
        public void InitializeFramework_NoParameters_NoConfigFiles_ShouldReturn()
        {
            DeleteConfigFiles(".");

            Startup.InitializeFramework();

            Startup.CloseFramework();
        }

        [TestMethod]
        public void InitializeFramework_NoParameters_ConfigFileExists_ShouldReturn()
        {
            SetCurrentConfig("Root", "EmptyLibrariesNode_", ".");

            Startup.InitializeFramework();

            Startup.CloseFramework();
        }

        [TestMethod]
        public void InitializeFramework_ConfigDirectoryGiven_ConfigFileExists_ShouldReturn()
        {
            SetCurrentConfig("Root", "EmptyLibrariesNode_");

            Startup.InitializeFramework(null, c_currentConfigDirectory);

            Startup.CloseFramework();
        }

        [TestMethod]
        public void InitializeFramework_ConfigDirectoryGiven_NoConfigFiles_ShouldReturn()
        {
            DeleteConfigFiles(c_currentConfigDirectory);

            Startup.InitializeFramework(null, c_currentConfigDirectory);

            Startup.CloseFramework();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitializeFramework_TwoSubsequentCalls_ShouldThrowInvalidOperationException()
        {
            DeleteConfigFiles(".");

            try
            {
                Startup.InitializeFramework();

                Startup.InitializeFramework();
            }

            finally
            {
                Startup.CloseFramework();
            }
        }

        #endregion
    }
}
