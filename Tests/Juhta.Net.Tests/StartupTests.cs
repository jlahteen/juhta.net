
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
            if (Application.IsInitialized)
                Application.Close();
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
        public void CloseFramework_FrameworkInitialized_ShouldReturn()
        {
            Application.Initialize();

            Application.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloseFramework_FrameworkNotInitialized_ShouldThrowInvalidOperationException()
        {
            Application.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void InitializeFramework_BrokenConfig_ShouldThrowXmlException()
        {
            SetConfigFiles("Root", "BrokenConfig_", ".");

            Application.Initialize();
        }

        [TestMethod]
        public void InitializeFramework_ClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ClosableLibrary_");

            Application.Initialize(null, s_configDirectory);

            Application.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<null>", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void InitializeFramework_ConfigDirectoryGiven_NoConfigFiles_ShouldReturn()
        {
            Application.Initialize(null, s_configDirectory);
        }

        [TestMethod]
        public void InitializeFramework_ConfigDirectoryGiven_SimpleConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_");

            Application.Initialize(null, s_configDirectory);
        }

        [TestMethod]
        public void InitializeFramework_ConfigurableAndClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ConfigurableAndClosableLibrary_");

            Application.Initialize(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void InitializeFramework_ConfigurableAndClosableLibrary_CloseLibraryReturnsFalse_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ConfigurableAndClosableLibrary_CloseLibraryReturnsFalse_");

            Application.Initialize(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("WARNING 'Juhta.Net.Warning10017'", "At least one error occurred when the library 'AppXLibrary.dll' was closed.");
        }

        [TestMethod]
        public void InitializeFramework_ConfigurableAndClosableLibrary_CloseLibraryThrowsException_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ConfigurableAndClosableLibrary_CloseLibraryThrowsException_");

            Application.Initialize(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10004'", "LibraryClosingException", "Closing of the library 'AppXLibrary.dll' failed.", "Something went wrong in the closing of AppXLibrary.");
        }

        [TestMethod]
        public void InitializeFramework_ConfigurableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ConfigurableLibrary_");

            Application.Initialize(null, s_configDirectory);

            Assert.AreEqual<int>(473473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the configured StringSetting value.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        [ExpectedException(typeof(LibraryInitializationException))]
        public void InitializeFramework_ConfigurableLibrary_InvalidConfigValue_ShouldThrowLibraryInitializationException()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ConfigurableLibrary_InvalidConfigValue_");

            try
            {
                Application.Initialize(null, s_configDirectory);
            }

            catch
            {
                AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10003'", "Initialization of the library 'AppXLibrary.dll' failed.", "IntSetting 1234567 is invalid. Please use any other integer value but not this one!");

                throw;
            }
        }

        [TestMethod]
        public void InitializeFramework_InitializableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_");

            Application.Initialize(null, s_configDirectory);

            Assert.AreEqual<int>(89473537, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void InitializeFramework_InitializableLibrary_LibraryHandleClassNamespaceMissing_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_LibraryHandleClassNamespaceMissing_");

            Application.Initialize(null, s_configDirectory);

            Assert.AreEqual<int>(89473537, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void InitializeFramework_InitializableLibrary_NoLibraryHandleClassSpecified_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_NoLibraryHandleClassSpecified_");

            Application.Initialize(null, s_configDirectory);

            Assert.AreEqual<int>(121213, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting set by the default LibraryHandle class.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidConfigFileException))]
        public void InitializeFramework_InvalidConfig_ShouldThrowException()
        {
            SetConfigFiles("Root", "InvalidConfig_", ".");

            try
            {
                Application.Initialize();
            }

            catch
            {
                AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10002'");

                throw;
            }
        }

        [TestMethod]
        public void InitializeFramework_LogEventOutputToDefaultLogFile_ShouldReturn()
        {
            Application.Initialize();

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(GetDefaultLogFile()));
        }

        [TestMethod]
        public void InitializeFramework_LogEventOutputToSpecifiedLogFile_ShouldReturn()
        {
            string logFilePath;

            logFilePath = GetTestLogDirectory() + "AppX.log";

            Application.Initialize(logFilePath);

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(logFilePath));
        }

        [TestMethod]
        public void InitializeFramework_NoParameters_NoConfigFiles_ShouldReturn()
        {
            Application.Initialize();
        }

        [TestMethod]
        public void InitializeFramework_NoParameters_SimplyConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_", ".");

            Application.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitializeFramework_TwoSubsequentCalls_ShouldThrowInvalidOperationException()
        {
            Application.Initialize();

            Application.Initialize();
        }

        #endregion
    }
}
