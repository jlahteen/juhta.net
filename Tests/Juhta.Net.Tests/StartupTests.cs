
using Juhta.Net.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;

namespace Juhta.Net.Tests
{
    [TestClass]
    public class StartupTests : TestClassBase
    {
        #region Test Initialization Methods

        [TestCleanup]
        public void TestCleanup()
        {
            if (Startup.IsFrameworkInitialized)
                Startup.CloseFramework();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            string defaultLogFile;

            DeleteConfigFiles(".");

            DeleteConfigFiles(c_configDirectory);

            DeleteLogFiles(GetTestLogDirectory());

            defaultLogFile = GetDefaultLogFile();

            if (File.Exists(defaultLogFile))
                File.Delete(defaultLogFile);
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void CloseFramework_FrameworkInitialized_ShouldReturn()
        {
            Startup.InitializeFramework();

            Startup.CloseFramework();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloseFramework_FrameworkNotInitialized_ShouldThrowInvalidOperationException()
        {
            Startup.CloseFramework();
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void InitializeFramework_BrokenConfig_ShouldThrowXmlException()
        {
            SetConfigFiles("Root", "BrokenConfig_", ".");

            Startup.InitializeFramework();
        }

        [TestMethod]
        public void InitializeFramework_ConfigDirectoryGiven_NoConfigFiles_ShouldReturn()
        {
            Startup.InitializeFramework(null, c_configDirectory);
        }

        [TestMethod]
        public void InitializeFramework_ConfigDirectoryGiven_SimpleConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_");

            Startup.InitializeFramework(null, c_configDirectory);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidConfigFileException))]
        public void InitializeFramework_InvalidConfig_ShouldThrowException()
        {
            string logFileContent;

            SetConfigFiles("Root", "InvalidConfig_", ".");

            try
            {
                Startup.InitializeFramework();
            }

            catch
            {
                logFileContent = File.ReadAllText(GetDefaultLogFile());

                Assert.IsTrue(logFileContent.Contains("ERROR 'Juhta.Net.Error10002'"));

                throw;
            }
        }

        [TestMethod]
        public void InitializeFramework_LogEventOutputToDefaultLogFile_ShouldReturn()
        {
            Startup.InitializeFramework();

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(GetDefaultLogFile()));
        }

        [TestMethod]
        public void InitializeFramework_LogEventOutputToSpecifiedLogFile_ShouldReturn()
        {
            string logFilePath;

            logFilePath = GetTestLogDirectory() + "AppX.log";

            Startup.InitializeFramework(logFilePath);

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(logFilePath));
        }

        [TestMethod]
        public void InitializeFramework_NoParameters_NoConfigFiles_ShouldReturn()
        {
            Startup.InitializeFramework();
        }

        [TestMethod]
        public void InitializeFramework_NoParameters_SimplyConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_", ".");

            Startup.InitializeFramework();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitializeFramework_TwoSubsequentCalls_ShouldThrowInvalidOperationException()
        {
            Startup.InitializeFramework();

            Startup.InitializeFramework();
        }

        #endregion
    }
}
