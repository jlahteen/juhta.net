
using AppXLibrary;
using Juhta.Net.Common;
using Juhta.Net.LibraryManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;

namespace Juhta.Net.Tests
{
    [TestClass]
    public class StartupTests : TestClassBase
    {
        #region Test Setup Methods

        [TestCleanup]
        public void TestCleanup()
        {
            Application.CloseInstance();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            string defaultLogFile;

            DeleteConfigFiles(".");

            DeleteConfigFiles(s_configDirectory);

            DeleteLogFiles(GetTestLogDirectory());

            defaultLogFile = GetDefaultLogFile();

            if (File.Exists(defaultLogFile))
                File.Delete(defaultLogFile);
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void Close_ApplicationInitialized_ShouldReturn()
        {
            Application.InitializeInstance();

            Application.Instance.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Close_ApplicationNotInitialized_ShouldThrowInvalidOperationException()
        {
            Application application = new Application();

            application.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void Initialize_BrokenConfig_ShouldThrowXmlException()
        {
            SetConfigFiles("Root", "BrokenConfig_", ".");

            Application.InitializeInstance();
        }

        [TestMethod]
        public void Initialize_ClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ClosableLibrary_");

            Application.InitializeInstance(null, s_configDirectory);

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<null>", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Initialize_ConfigDirectoryGiven_NoConfigFiles_ShouldReturn()
        {
            Application.InitializeInstance(null, s_configDirectory);
        }

        [TestMethod]
        public void Initialize_ConfigDirectoryGiven_SimpleConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_");

            Application.InitializeInstance(null, s_configDirectory);
        }

        [TestMethod]
        public void Initialize_CustomXmlConfigurableAndClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableAndClosableLibrary_");

            Application.InitializeInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Initialize_CustomXmlConfigurableAndClosableLibrary_CloseLibraryReturnsFalse_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableAndClosableLibrary_CloseLibraryReturnsFalse_");

            Application.InitializeInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("WARNING 'Juhta.Net.Warning10017'", "At least one error occurred when the library 'AppXLibrary.dll' was closed.");
        }

        [TestMethod]
        public void Initialize_CustomXmlConfigurableAndClosableLibrary_CloseLibraryThrowsException_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableAndClosableLibrary_CloseLibraryThrowsException_");

            Application.InitializeInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10004'", "An unexpected error occurred when the library 'AppXLibrary.dll' was being closed.", "Something went wrong in the closing of AppXLibrary.");
        }

        [TestMethod]
        public void Initialize_CustomXmlConfigurableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableLibrary_");

            Application.InitializeInstance(null, s_configDirectory);

            Assert.AreEqual<int>(473473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the configured StringSetting value.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        [ExpectedException(typeof(LibraryInitializationException))]
        public void Initialize_CustomXmlConfigurableLibrary_InvalidConfigValue_ShouldThrowLibraryInitializationException()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableLibrary_InvalidConfigValue_");

            try
            {
                Application.InitializeInstance(null, s_configDirectory);
            }

            catch
            {
                AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10003'", "Initialization of the library 'AppXLibrary.dll' failed.", "IntSetting 1234567 is invalid. Please use any other integer value but not this one!");

                throw;
            }
        }

        [TestMethod]
        public void Initialize_InitializableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_");

            Application.InitializeInstance(null, s_configDirectory);

            Assert.AreEqual<int>(89473537, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Initialize_InitializableLibrary_LibraryHandleClassNamespaceMissing_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_LibraryHandleClassNamespaceMissing_");

            Application.InitializeInstance(null, s_configDirectory);

            Assert.AreEqual<int>(89473537, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Initialize_InitializableLibrary_NoLibraryHandleClassSpecified_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_NoLibraryHandleClassSpecified_");

            Application.InitializeInstance(null, s_configDirectory);

            Assert.AreEqual<int>(121213, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting set by the default LibraryHandle class.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidConfigFileException))]
        public void Initialize_InvalidConfig_ShouldThrowException()
        {
            SetConfigFiles("Root", "InvalidConfig_", ".");

            try
            {
                Application.InitializeInstance();
            }

            catch
            {
                AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10002'");

                throw;
            }
        }

        [TestMethod]
        public void Initialize_LogEventOutputToDefaultLogFile_ShouldReturn()
        {
            Application.InitializeInstance();

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(GetDefaultLogFile()));
        }

        [TestMethod]
        public void Initialize_LogEventOutputToSpecifiedLogFile_ShouldReturn()
        {
            string logFilePath;

            logFilePath = GetTestLogDirectory() + "AppX.log";

            Application.InitializeInstance(logFilePath, null);

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(logFilePath));
        }

        [TestMethod]
        public void Initialize_NoParameters_NoConfigFiles_ShouldReturn()
        {
            Application.InitializeInstance();
        }

        [TestMethod]
        public void Initialize_NoParameters_SimplyConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_", ".");

            Application.InitializeInstance();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Initialize_TwoSubsequentCalls_ShouldThrowInvalidOperationException()
        {
            Application application = new Application();

            application.Initialize();

            application.Initialize();
        }

        #endregion
    }
}
