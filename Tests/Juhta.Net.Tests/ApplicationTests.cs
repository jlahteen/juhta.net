
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

        [ClassCleanup]
        public static void ClassCleanup()
        {}

        [TestCleanup]
        public void TestCleanup()
        {
            Application.CloseInstance();

            DeleteConfigFiles(s_configDirectory);
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

            AppXLibrary.StartableLibrary.Reset();
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void Close_ApplicationInitialized_ShouldReturn()
        {
            Application.StartInstance();

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
        public void Close_StartableLibrary_StopProcessesReturnsFalse_ShouldReturn()
        {
            SetConfigFiles("Root", "StartableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.StartableLibrary.IsStarted);

            AppXLibrary.StartableLibrary.StopProcessesReturnValue = false;

            Application.Instance.Close();

            Assert.AreEqual<bool>(false, AppXLibrary.StartableLibrary.IsStarted);

            AssertDefaultLogFileContent("Juhta.Net.Warning10072", "At least one error occurred when the processes of the library 'AppXLibrary.dll' were being stopped.");
        }

        [TestMethod]
        public void Close_StartableLibrary_StopProcessesThrowsException_ShouldReturn()
        {
            SetConfigFiles("Root", "StartableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.StartableLibrary.IsStarted);

            AppXLibrary.StartableLibrary.StopProcessesException = new Exception("This is an injected exception.");

            Application.Instance.Close();

            Assert.AreEqual<bool>(true, AppXLibrary.StartableLibrary.IsStarted);

            AssertDefaultLogFileContent("Juhta.Net.Error10071", "An unexpected error occurred when the processes of the library 'AppXLibrary.dll' were being stopped.", "This is an injected exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void Start_BrokenConfig_ShouldThrowXmlException()
        {
            SetConfigFiles("Root", "BrokenConfig_", ".");

            Application.StartInstance();
        }

        [TestMethod]
        public void Start_ClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "ClosableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<null>", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Start_ConfigDirectoryGiven_NoConfigFiles_ShouldReturn()
        {
            Application.StartInstance(null, s_configDirectory);
        }

        [TestMethod]
        public void Start_ConfigDirectoryGiven_SimpleConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_");

            Application.StartInstance(null, s_configDirectory);
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableAndClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableAndClosableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableAndClosableLibrary_CloseLibraryReturnsFalse_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableAndClosableLibrary_CloseLibraryReturnsFalse_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("WARNING 'Juhta.Net.Warning10017'", "At least one error occurred when the library 'AppXLibrary.dll' was closed.");
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableAndClosableLibrary_CloseLibraryThrowsException_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableAndClosableLibrary_CloseLibraryThrowsException_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10004'", "An unexpected error occurred when the library 'AppXLibrary.dll' was being closed.", "Something went wrong in the closing of AppXLibrary.");
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(473473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the configured StringSetting value.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableLibrary100_ShouldReturn()
        {
            ILibraryConfig libraryConfig;

            SetConfigFiles("Root", "CustomXmlConfigurableLibrary100_");

            AppXLibrary.Cloning.Clone.BuildCopies(100);

            Application.StartInstance(null, s_configDirectory);

            for (int i = 1; i <= 100; i++)
            {
                libraryConfig = ObjectFactory.CreateInstance<ILibraryConfig>($"AppXLibrary{i}.dll", $"AppXLibrary{i}.LibraryConfig");

                Assert.AreEqual<string>($"This is the configured StringSetting value._{i:000}", libraryConfig.GetStringSetting());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LibraryInitializationException))]
        public void Start_CustomXmlConfigurableLibrary_InvalidConfigValue_ShouldThrowLibraryInitializationException()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "CustomXmlConfigurableLibrary_InvalidConfigValue_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch
            {
                AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10003'", "Initialization of the library 'AppXLibrary.dll' failed.", "IntSetting 1234567 is invalid. Please use any other integer value but not this one!");

                throw;
            }
        }

        [TestMethod]
        public void Start_InitializableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(89473537, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Start_InitializableLibrary_LibraryHandleClassNamespaceMissing_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_LibraryHandleClassNamespaceMissing_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(89473537, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Start_InitializableLibrary_NoLibraryHandleClassSpecified_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Root", "InitializableLibrary_NoLibraryHandleClassSpecified_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(121213, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting set by the default LibraryHandle class.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidConfigFileException))]
        public void Start_InvalidConfig_ShouldThrowException()
        {
            SetConfigFiles("Root", "InvalidConfig_", ".");

            try
            {
                Application.StartInstance();
            }

            catch
            {
                AssertDefaultLogFileContent("ERROR 'Juhta.Net.Error10002'");

                throw;
            }
        }

        [TestMethod]
        public void Start_LogEventOutputToDefaultLogFile_ShouldReturn()
        {
            Application.StartInstance();

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(GetDefaultLogFile()));
        }

        [TestMethod]
        public void Start_LogEventOutputToSpecifiedLogFile_ShouldReturn()
        {
            string logFilePath;

            logFilePath = GetTestLogDirectory() + "AppX.log";

            Application.StartInstance(logFilePath, null);

            Logger.LogInformation("This event should produce a log file.");

            Assert.IsTrue(File.Exists(logFilePath));
        }

        [TestMethod]
        public void Start_NoParameters_NoConfigFiles_ShouldReturn()
        {
            Application.StartInstance();
        }

        [TestMethod]
        public void Start_NoParameters_SimplyConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_", ".");

            Application.StartInstance();
        }

        [TestMethod]
        public void Start_StartableLibrary_ShouldReturn()
        {
            SetConfigFiles("Root", "StartableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.StartableLibrary.IsStarted);

            Application.Instance.Close();

            Assert.AreEqual<bool>(false, AppXLibrary.StartableLibrary.IsStarted);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Start_TwoSubsequentCalls_ShouldThrowInvalidOperationException()
        {
            Application application = new Application();

            application.Start();

            application.Start();
        }

        #endregion
    }
}
