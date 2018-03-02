
using AppXLibrary.CustomXmlConfig;
using AppXLibrary.DynamicCustomXmlConfigurableAndStartable;
using AppXLibrary.Services;
using Juhta.Net.Common;
using Juhta.Net.Extensions;
using Juhta.Net.LibraryManagement;
using Juhta.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            if (!Directory.Exists(s_tempDirectory))
                Directory.CreateDirectory(s_tempDirectory);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Application.CloseInstance();

            DeleteConfigFiles(".");

            DeleteConfigFiles(s_configDirectory);

            DeleteConfigFiles(s_tempDirectory);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            string defaultLogFile;

            DeleteLogFiles(GetTestLogDirectory());

            defaultLogFile = GetDefaultLogFile();

            if (File.Exists(defaultLogFile))
                File.Delete(defaultLogFile);

            AppXLibrary.Startable.StartableLibrary.Reset();
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

            Assert.AreEqual<bool>(true, AppXLibrary.Startable.StartableLibrary.IsStarted);

            AppXLibrary.Startable.StartableLibrary.StopProcessesReturnValue = false;

            Application.Instance.Close();

            Assert.AreEqual<bool>(false, AppXLibrary.Startable.StartableLibrary.IsStarted);

            AssertDefaultLogFileContent("Juhta.Net.Warning10072", "At least one error occurred when the processes of the library 'AppXLibrary.dll' were being stopped.");
        }

        [TestMethod]
        public void Close_StartableLibrary_StopProcessesThrowsException_ShouldReturn()
        {
            SetConfigFiles("Root", "StartableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.Startable.StartableLibrary.IsStarted);

            AppXLibrary.Startable.StartableLibrary.StopProcessesException = new Exception("This is an injected exception.");

            Application.Instance.Close();

            Assert.AreEqual<bool>(true, AppXLibrary.Startable.StartableLibrary.IsStarted);

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
        public void Start_ConfigurableLibrary_Ini_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Root", "ConfigurableLibrary_Ini_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Root", "ConfigurableLibrary_Json_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_ConfigFileNameOverriddenInConfig_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Root", "ConfigurableLibrary_Json_ConfigFileNameOverriddenInConfig_");

            File.Move(s_configDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json", s_configDirectory + Path.DirectorySeparatorChar + "UseThisConfig.json");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        [ExpectedException(typeof(LibraryInitializationException))]
        public void Start_ConfigurableLibrary_Json_NullConfigFileName_ShouldThrowLibraryInitializationException()
        {
            SetConfigFiles("Root", "ConfigurableLibrary_Json_NullConfigFileName_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch (LibraryInitializationException ex)
            {
                Assert.IsTrue(ex.InnerException.Message.StartsWith("[Juhta.Net.Error10014] Configuration file name cannot be null for the configurable library 'AppXLibrary.dll'."));

                throw;
            }
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_NullConfigFileName_ConfigFileNameSpecifiedInConfig_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Root", "ConfigurableLibrary_Json_NullConfigFileName_ConfigFileNameSpecifiedInConfig_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_NullConfigFileName_DefaultConfigFileNameSpecifiedInConfig_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Root", "ConfigurableLibrary_Json_NullConfigFileName_DefaultConfigFileNameSpecifiedInConfig_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Xml_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Root", "ConfigurableLibrary_Xml_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
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

            AssertDefaultLogFileContent("WARNING event", "[Juhta.Net.Warning10017] At least one error occurred when the library 'AppXLibrary.dll' was closed.");
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

            AssertDefaultLogFileContent("ERROR event", "[Juhta.Net.Error10004] An unexpected error occurred when the library 'AppXLibrary.dll' was being closed.", "Something went wrong in the closing of AppXLibrary.");
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

            AppXLibrary.Cloning.Clone.BuildCopies("AppXLibraryTempA", 100, "AppXLibrary.CustomXmlConfigurable");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 1; i <= 100; i++)
            {
                libraryConfig = ObjectFactory.CreateInstance<ILibraryConfig>($"AppXLibraryTempA{i}.dll", $"AppXLibraryTempA{i}.CustomXmlConfigurable.LibraryConfig");

                Assert.AreEqual<string>($"This is the configured StringSetting value._{i:000}", libraryConfig.GetStringSetting());
            }
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Ini_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicConfigurableLibrary_Ini_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableIni.LibraryHandle, AppXLibrary.DynamicConfigurableIni.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Ini_ConfigChange_ShouldReturn()
        {
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.ini";
            string configFileContent;

            SetConfigFiles("Root", "DynamicConfigurableLibrary_Ini_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableIni.LibraryHandle, AppXLibrary.DynamicConfigurableIni.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            configFileContent = File.ReadAllText(appXConfigFilePath);

            configFileContent = configFileContent.Replace("This is String", "This is the updated String");

            File.WriteAllText(appXConfigFilePath, configFileContent);

            Thread.Sleep(2000);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableIni.LibraryHandle, AppXLibrary.DynamicConfigurableIni.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is the updated String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Json_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicConfigurableLibrary_Json_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Json_ConfigChange_ShouldReturn()
        {
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.json";
            string configFileContent;

            SetConfigFiles("Root", "DynamicConfigurableLibrary_Json_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            configFileContent = File.ReadAllText(appXConfigFilePath);

            configFileContent = configFileContent.Replace("This is String", "This is the updated String");

            File.WriteAllText(appXConfigFilePath, configFileContent);

            Thread.Sleep(2000);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is the updated String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Json_ConfigFileRenamingInConfigDirectory_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicConfigurableLibrary_Json_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            File.Move(s_configDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json", s_configDirectory + Path.DirectorySeparatorChar + "RenamedAppXLibrary.json");

            Thread.Sleep(2000);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is Default String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            AssertDefaultLogFileContent(
                "WARNING event",
                "[Juhta.Net.Warning10063] Library Manager detected that the configuration file",
                "RenamedAppXLibrary.json' was created or changed, but no actions were performed because there were no dynamic libraries associated with this configuration file.",
                "INFORMATION event",
                "[Juhta.Net.Info10012] Library Manager detected that the configuration file",
                "AppXLibrary.json' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Json_ConfigFileRenamingIntoConfigDirectory_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicConfigurableLibrary_Json_");

            File.Move(s_configDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json", s_tempDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is Default String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            File.Move(s_tempDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json", s_configDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json");

            Thread.Sleep(2000);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Info10065] Library Manager detected that the configuration file",
                "AppXLibrary.json' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Json_ConfigFileRenamingOutOfConfigDirectory_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicConfigurableLibrary_Json_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            File.Move(s_configDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json", s_tempDirectory + Path.DirectorySeparatorChar + "AppXLibrary.json");

            Thread.Sleep(2000);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableJson.LibraryHandle, AppXLibrary.DynamicConfigurableJson.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is Default String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Info10012] Library Manager detected that the configuration file",
                "AppXLibrary.json' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Xml_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicConfigurableLibrary_Xml_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableXml.LibraryHandle, AppXLibrary.DynamicConfigurableXml.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Xml_ConfigChange_ShouldReturn()
        {
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.xml";
            string configFileContent;

            SetConfigFiles("Root", "DynamicConfigurableLibrary_Xml_");

            Application.StartInstance(null, s_configDirectory);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableXml.LibraryHandle, AppXLibrary.DynamicConfigurableXml.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }

            configFileContent = File.ReadAllText(appXConfigFilePath);

            configFileContent = configFileContent.Replace("This is String", "This is the updated String");

            File.WriteAllText(appXConfigFilePath, configFileContent);

            Thread.Sleep(2000);

            using (var context = Application.Instance.GetDynamicLibraryContext<AppXLibrary.DynamicConfigurableXml.LibraryHandle, AppXLibrary.DynamicConfigurableXml.LibraryState>())
            {
                for (int i = 0; i < 10; i++)
                    Assert.AreEqual<string>($"This is the updated String{i}", context.LibraryState.StringCache.Get($"String{i}"));
            }
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_ShouldReturn()
        {
            string currentGreeting;

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_ConfigFileDeletion_ShouldReturn()
        {
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            File.Delete(appXConfigFilePath);

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("Hello there, what's up?", currentGreeting);

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Info10012] Library Manager detected that the configuration file",
                "AppXLibrary.config' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_ConfigFileDeletionAndCreation_ShouldReturn()
        {
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            File.Delete(appXConfigFilePath);

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("Hello there, what's up?", currentGreeting);

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableLibrary_");

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Info10012] Library Manager detected that the configuration file",
                "AppXLibrary.config' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully.",
                "INFORMATION event",
                "[Juhta.Net.Info10065] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully.",
                "WARNING event",
                "[Juhta.Net.Warning10063] Library Manager detected that the configuration file",
                "Juhta.Net.config' was created or changed, but no actions were performed because there were no dynamic libraries associated with this configuration file."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_InvalidConfigChange_ShouldReturn()
        {
            XmlDocument appXConfig = new XmlDocument();
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.FirstChild.InnerText = "Too short greeting!";

            appXConfig.Save(appXConfigFilePath);

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            AssertDefaultLogFileContent(
                "ERROR event",
                "[Juhta.Net.Error10064] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. The state of the library was left unmodified.",
                "Juhta.Net.Common.InvalidConfigFileException: [Juhta.Net.Error10002] XML configuration file",
                "AppXLibrary.config' does not conform to the configuration schema(s) of the custom XML configurable library 'AppXLibrary.dll'.",
                "Juhta.Net.Validators.ValidationException: [Juhta.Net.Error10082] XML document is not valid according to the given schema(s).",
                "ALERT event",
                "[Juhta.Net.Alert10005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_ValidConfigChange_ShouldReturn()
        {
            XmlDocument appXConfig = new XmlDocument();
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.FirstChild.InnerText = "This is the new greeting dynamically changed by a unit test!";

            appXConfig.Save(appXConfigFilePath);

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the new greeting dynamically changed by a unit test!", currentGreeting);

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Info10065] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_UnknownConfigFileDeletion_ShouldReturn()
        {
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";
            string unknownConfigFilePath = s_configDirectory + "\\Unknown.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableLibrary_");

            File.Copy(appXConfigFilePath, unknownConfigFilePath);

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            File.Delete(unknownConfigFilePath);

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            AssertDefaultLogFileContent(
                "WARNING event",
                "[Juhta.Net.Warning10011] Library Manager detected that the configuration file",
                "Unknown.config' was deleted, but no actions were performed because there were no dynamic libraries associated with this configuration file."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.DynamicCustomXmlConfigurableAndStartable.ReplaceProcessInfo.IsStarted);
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_ConfigChange_ShouldReturn()
        {
            string s;
            ReplaceService replaceService = new ReplaceService();
            XmlDocument appXConfig = new XmlDocument();
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_");

            Application.StartInstance(null, s_configDirectory);

            s = replaceService.Replace("Ho-Ho-Ho");

            Assert.AreEqual<string>("Yo-Yo-Yo", s);

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.LastChild.SetAttribute("replace", "Zz");

            appXConfig.Save(appXConfigFilePath);

            Thread.Sleep(2000);

            s = replaceService.Replace("Ho-Ho-Ho");

            Assert.AreEqual<string>("Zz-Zz-Zz", s);

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Info10065] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_ConfigChangeUnderHeavyLoad_ShouldReturn()
        {
            Thread[] threads = new Thread[100];
            StressTestParam[] threadParams = new StressTestParam[100];
            XmlDocument appXConfig = new XmlDocument();
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < threads.Length; i++)
                threadParams[i] = new StressTestParam{Index = i, Value = "<null>"};

            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(new ParameterizedThreadStart(StressTestMain));

            for (int i = 0; i < threads.Length; i++)
                threads[i].Start(threadParams[i]);

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.LastChild.SetAttribute("replace", "Zz");

            appXConfig.Save(appXConfigFilePath);

            for (int i = 0; i < threads.Length; i++)
                threads[i].Join();

            for (int i = 0; i < threads.Length; i++)
                Assert.AreEqual<string>($"Zz-Zz-Zz-{i}", threadParams[i].Value);
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_ConfigChange_StartProcessesThrowsException_ShouldReturn()
        {
            string s;
            ReplaceService replaceService = new ReplaceService();
            XmlDocument appXConfig = new XmlDocument();
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_StartProcessesThrowsException_");

            Application.StartInstance(null, s_configDirectory);

            s = replaceService.Replace("Ho-Ho-Ho");

            Assert.AreEqual<string>("Yo-Yo-Yo", s);

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.LastChild.SetAttribute("replace", "XYZ");

            appXConfig.Save(appXConfigFilePath);

            Thread.Sleep(2000);

            s = replaceService.Replace("Ho-Ho-Ho");

            Assert.AreEqual<string>("Yo-Yo-Yo", s);

            AssertDefaultLogFileContent(
                "ERROR event",
                "Juhta.Net.LibraryManagement.LibraryStateException: [Juhta.Net.Error10061] Processes in the new state of the library 'AppXLibrary.dll' could not be started.",
                "System.InvalidOperationException: Cannot replace with 'XYZ' strings. Please use any other token but not that. Sorry.",
                "[Juhta.Net.Warning10078] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. NOTE: The library continues running with the current state.",
                "ALERT event",
                "[Juhta.Net.Alert10005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
            );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Start_DynamicCustomXmlConfigurableAndStartable_ConfigChange_StopProcessesReturnsFalse_ShouldReturn()
        {
            string s;
            ReplaceService replaceService = new ReplaceService();
            XmlDocument appXConfig = new XmlDocument();
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_StopProcessesReturnsFalse_");

            Application.StartInstance(null, s_configDirectory);

            s = replaceService.Replace("Ho-Ho-Ho");

            Assert.AreEqual<string>("Yo-Yo-Yo", s);

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.LastChild.SetAttribute("replace", "Zz");

            appXConfig.Save(appXConfigFilePath);

            Thread.Sleep(2000);

            try
            {
                s = replaceService.Replace("Ho-Ho-Ho");
            }

            catch
            {
                AssertDefaultLogFileContent(
                    "ERROR event",
                    "[Juhta.Net.Error10066] Library Manager detected that the configuration file",
                    "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated.",
                    "NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.",
                    "[Juhta.Net.Error10075] An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped.",
                    "Juhta.Net.LibraryManagement.LibraryStateException: [Juhta.Net.Error10059] Processes in the current state of the library 'AppXLibrary.dll' could not be completely stopped.",
                    "ALERT event",
                    "[Juhta.Net.Alert10005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
                );

                throw;
            }
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_ConfigChange_StopProcessesThrowsException_ShouldReturn()
        {
            string s;
            ReplaceService replaceService = new ReplaceService();
            XmlDocument appXConfig = new XmlDocument();
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_StopProcessesThrowsException_");

            Application.StartInstance(null, s_configDirectory);

            s = replaceService.Replace("Ho-Ho-Ho");

            Assert.AreEqual<string>("Yo-Yo-Yo", s);

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.LastChild.SetAttribute("replace", "Zz");

            appXConfig.Save(appXConfigFilePath);

            Thread.Sleep(2000);

            s = replaceService.Replace("Ho-Ho-Ho");

            Assert.AreEqual<string>("Yo-Yo-Yo", s);

            AssertDefaultLogFileContent(
                "ERROR event",
                "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.",
                "Juhta.Net.LibraryManagement.LibraryStateException: [Juhta.Net.Error10075] An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped. ---> System.Exception: Processes could not be stopped.",
                "ALERT event",
                "[Juhta.Net.Alert10005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_StopProcessesReturnsFalse_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_StopProcessesReturnsFalse_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.DynamicCustomXmlConfigurableAndStartable.ReplaceProcessInfo.IsStarted);

            Application.CloseInstance();

            AssertDefaultLogFileContent(
                "WARNING event",
                "[Juhta.Net.Warning10073] At least one error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_StopProcessesThrowsException_ShouldReturn()
        {
            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable_StopProcessesThrowsException_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.DynamicCustomXmlConfigurableAndStartable.ReplaceProcessInfo.IsStarted);

            Application.CloseInstance();

            AssertDefaultLogFileContent(
                "ERROR event",
                "[Juhta.Net.Error10075] An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped.",
                "System.Exception: Processes could not be stopped."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable100_ShouldReturn()
        {
            IReplaceService replaceService;
            string s;

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable100_");

            AppXLibrary.Cloning.Clone.BuildCopies("AppXLibraryTempB", 100, "AppXLibrary.DynamicCustomXmlConfigurableAndStartable");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 1; i <= 100; i++)
            {
                replaceService = ObjectFactory.CreateInstance<IReplaceService>($"AppXLibraryTempB{i}.dll", $"AppXLibraryTempB{i}.DynamicCustomXmlConfigurableAndStartable.ReplaceService");

                s = replaceService.Replace("Ho-Ho-Ho");

                Assert.AreEqual<string>($"Yo-Yo-Yo-{i}", s);
            }
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable100_ConfigChange_ShouldReturn()
        {
            IReplaceService replaceService;
            string s;
            XmlDocument appXConfig = new XmlDocument();
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";
            int secondsWaited = 0;

            SetConfigFiles("Root", "DynamicCustomXmlConfigurableAndStartable100_ConfigChange_");

            AppXLibrary.Cloning.Clone.BuildCopies("AppXLibraryTempC", 100, "AppXLibrary.DynamicCustomXmlConfigurableAndStartable");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 1; i <= 100; i++)
            {
                replaceService = ObjectFactory.CreateInstance<IReplaceService>($"AppXLibraryTempC{i}.dll", $"AppXLibraryTempC{i}.DynamicCustomXmlConfigurableAndStartable.ReplaceService");

                s = replaceService.Replace("Ho-Ho-Ho");

                Assert.AreEqual<string>($"Yo-Yo-Yo-{i}", s);
            }

            appXConfig.Load(appXConfigFilePath);

            appXConfig.DocumentElement.LastChild.SetAttribute("replace", "!#");

            appXConfig.Save(appXConfigFilePath);

            for (int i = 1; i <= 100; i++)
            {
                replaceService = ObjectFactory.CreateInstance<IReplaceService>($"AppXLibraryTempC{i}.dll", $"AppXLibraryTempC{i}.DynamicCustomXmlConfigurableAndStartable.ReplaceService");

                do
                {
                    s = replaceService.Replace("Ho-Ho-Ho");

                    if (s == $"Yo-Yo-Yo-{i}")
                    {
                        Thread.Sleep(1000);

                        secondsWaited++;

                        Assert.IsTrue(secondsWaited < 10, $"secondsWaited >= 10: i is {i}");
                    }
                }
                while (s == $"Yo-Yo-Yo-{i}");

                Assert.AreEqual<string>($"!#-!#-!#-{i}", s);
            }

            for (int i = 1; i <= 100; i++)
                AssertDefaultLogFileContent(
                    "INFORMATION event",
                    "[Juhta.Net.Info10065] Library Manager detected that the configuration file",
                    $"AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibraryTempC{i}.dll' was updated successfully."
                );
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
                AssertDefaultLogFileContent("ERROR event", "[Juhta.Net.Error10003] Initialization of the library 'AppXLibrary.dll' failed.", "IntSetting 1234567 is invalid. Please use any other integer value but not this one!");

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
                AssertDefaultLogFileContent("[Juhta.Net.Error10002]");

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
        public void Start_NoParameters_SimpleConfig_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_", ".");

            Application.StartInstance();

            Assert.AreEqual<string>(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName), Application.Instance.Name);

            Assert.AreEqual<string>(null, Application.Instance.DefaultConfigFileName);
        }

        [TestMethod]
        public void Start_NoParameters_SimpleConfig2_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig2_", ".");

            Application.StartInstance();

            Assert.AreEqual<string>(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName), Application.Instance.Name);

            Assert.AreEqual<string>(null, Application.Instance.DefaultConfigFileName);
        }

        [TestMethod]
        public void Start_NoParameters_SimpleConfig_NameAndDefaultConfigFileNameSpecified_ShouldReturn()
        {
            SetConfigFiles("Root", "SimpleConfig_NameAndDefaultConfigFileNameSpecified_", ".");

            Application.StartInstance();

            Assert.AreEqual<string>("Application X", Application.Instance.Name);

            Assert.AreEqual<string>("SomeConfig.ini", Application.Instance.DefaultConfigFileName);
        }

        [TestMethod]
        public void Start_Services_AggregateSumService_ServiceGroups_ShouldReturn()
        {
            AggregateSumService aggregateSumService;

            SetConfigFiles("Root", "Services_AggregateSumService_");

            Application.StartInstance(null, s_configDirectory);

            aggregateSumService = Application.Instance.ServiceFactory.CreateService<AggregateSumService>();

            aggregateSumService.Add(15);

            Assert.AreEqual<int>(11 + 12 + 13 + 3 * 15, aggregateSumService.GetSum());
        }

        [TestMethod]
        public void Start_Services_AllParamTypeService_ShouldReturn()
        {
            IAllParamTypeService allParamTypeService;

            SetConfigFiles("Root", "Services_AllParamTypeService_");

            Application.StartInstance(null, s_configDirectory);

            allParamTypeService = Application.Instance.ServiceFactory.CreateService<IAllParamTypeService>("AllParamTypeService");

            Assert.AreEqual<bool>(true, allParamTypeService.BoolValue);

            Assert.AreEqual<byte>(223, allParamTypeService.ByteValue);

            Assert.AreEqual<char>('d', allParamTypeService.CharValue);

            Assert.AreEqual<DateTime>(new DateTime(2018, 1, 18), allParamTypeService.DateValue);

            Assert.AreEqual<DateTime>(new DateTime(2018, 1, 16, 19, 8, 20), allParamTypeService.DateTimeValue);

            Assert.AreEqual<decimal>(54.7636m, allParamTypeService.DecimalValue);

            Assert.AreEqual<double>(9956.8763, allParamTypeService.DoubleValue);

            Assert.AreEqual<float>(6373.88f, allParamTypeService.FloatValue);

            Assert.AreEqual<int>(64644646, allParamTypeService.IntValue);

            Assert.AreEqual<Int16>(32767, allParamTypeService.Int16Value);

            Assert.AreEqual<Int32>(64655646, allParamTypeService.Int32Value);

            Assert.AreEqual<Int64>(64644646566, allParamTypeService.Int64Value);

            Assert.AreEqual<long>(327000, allParamTypeService.LongValue);

            Assert.AreEqual<sbyte>(-100, allParamTypeService.SByteValue);

            Assert.AreEqual<short>(-4533, allParamTypeService.ShortValue);

            Assert.AreEqual<Single>(-66334.775f, allParamTypeService.SingleValue);

            Assert.AreEqual<string>("Hello from the service!", allParamTypeService.StringValue);

            Assert.AreEqual<DateTime>(new DateTime(1, 1, 1, 3, 9, 21), allParamTypeService.TimeValue);

            Assert.AreEqual<TimeSpan>(new TimeSpan(12345, 23, 56, 59), allParamTypeService.TimeSpanValue);

            Assert.AreEqual<uint>(65000, allParamTypeService.UintValue);

            Assert.AreEqual<UInt16>(56433, allParamTypeService.Uint16Value);

            Assert.AreEqual<UInt32>(444444444, allParamTypeService.Uint32Value);

            Assert.AreEqual<UInt64>(555555555555555, allParamTypeService.Uint64Value);

            Assert.AreEqual<ulong>(46456464646464, allParamTypeService.UlongValue);

            Assert.AreEqual<ushort>(56733, allParamTypeService.UshortValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceInitializationException))]
        public void Start_Services_FractionSecondInDateTimeConstructorParam_ShouldThrowServiceInitializationException()
        {
            SetConfigFiles("Root", "Services_FractionSecondInDateTimeConstructorParam_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch
            {
                AssertDefaultLogFileContent(
                    "[Juhta.Net.Error10060]",
                    "Juhta.Net.Services.ServiceInitializationException: [Juhta.Net.Error10074] Dependency injection service 'name:AnyService' could not be initialized.",
                    "Juhta.Net.Services.ConstructorParamException: [Juhta.Net.Error10062] Constructor parameter 'dateTimeValue' could not be initialized.",
                    "Juhta.Net.Common.InvalidConfigValueException: [Juhta.Net.Error10060] Value '2018-02-04T16:55:00.7774' of the constructor parameter 'dateTimeValue' is not a valid 'DateTime' parameter value."
                );

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidConfigFileException))]
        public void Start_Services_IdNotGiven_ShouldThrowInvalidConfigFileException()
        {
            SetConfigFiles("Root", "Services_IdNotGiven_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch
            {
                AssertDefaultLogFileContent(
                    "[Juhta.Net.Error10002]",
                    "Juhta.Net.Common.InvalidConfigFileException: [Juhta.Net.Error10002] XML configuration file",
                    "Juhta.Net.Validators.ValidationException: [Juhta.Net.Error10082] XML document is not valid according to the given schema(s).",
                    "[Juhta.Net.Error10002] XML configuration file",
                    "Juhta.Net.config' does not conform to the configuration schema(s) of the custom XML configurable library 'Juhta.Net.dll'.",
                    "Juhta.Net.Validators.ValidationException: [Juhta.Net.Error10082] XML document is not valid according to the given schema(s).",
                    "System.Xml.Schema.XmlSchemaValidationException: The required attribute 'id' is missing."
                );

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Services_InvalidServiceIdScheme_ShouldThrowArgumentException()
        {
            SetConfigFiles("Root", "Services_AllParamTypeService_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                Application.Instance.ServiceFactory.CreateService<SumService>("scheme%", "SumService");
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid 'scheme' parameter value was passed to the method 'Juhta.Net.Services.ServiceId..ctor'."));

                Assert.IsTrue(ex.Message.Contains("The value 'scheme%' does not conform to the regex pattern '^([a-zA-Z0-9])+$'."));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_Services_InvalidServiceIdSpecifier_ShouldThrowArgumentException()
        {
            SetConfigFiles("Root", "Services_AllParamTypeService_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                Application.Instance.ServiceFactory.CreateService<SumService>("scheme", "SumService%");
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid 'specifier' parameter value was passed to the method 'Juhta.Net.Services.ServiceId..ctor'."));

                Assert.IsTrue(ex.Message.Contains("The value 'SumService%' does not conform to the regex pattern '^([a-zA-Z0-9\\._/-])+$'."));

                throw;
            }
        }

        [TestMethod]
        public void Start_Services_NoConstructorParams_DefaultConstructor1_ShouldReturn()
        {
            SumService2 sumService2;

            SetConfigFiles("Root", "Services_NoConstructorParams_DefaultConstructor_");

            Application.StartInstance(null, s_configDirectory);

            sumService2 = Application.Instance.ServiceFactory.CreateService<SumService2>("SumService20");

            sumService2.Add(10);

            Assert.AreEqual<int>(100 + 10, sumService2.GetSum());
        }

        [TestMethod]
        public void Start_Services_NoConstructorParams_DefaultConstructor2_ShouldReturn()
        {
            SumService2 sumService2;

            SetConfigFiles("Root", "Services_NoConstructorParams_DefaultConstructor_");

            Application.StartInstance(null, s_configDirectory);

            sumService2 = Application.Instance.ServiceFactory.CreateService<SumService2>("SumService21");

            sumService2.Add(11);

            Assert.AreEqual<int>(100 + 11, sumService2.GetSum());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceCreationException))]
        public void Start_Services_NoConstructorParams_NoDefaultConstructor_ShouldThrowServiceCreationException()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_NoConstructorParams_NoDefaultConstructor_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                sumService = Application.Instance.ServiceFactory.CreateService<SumService>("SumService");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Error10080]",
                    "Juhta.Net.Services.ServiceCreationException: [Juhta.Net.Error10080] An instance of the dependency injection service 'name:SumService' could not be created.",
                    "System.MissingMethodException: Constructor on type 'AppXLibrary.Services.SumService' not found."
                );

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceCreationException))]
        public void Start_Services_NonExistingLibraryClass_ShouldThrowServiceCreationException()
        {
            object testService;

            SetConfigFiles("Root", "Services_NonExistingLibraryClass_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                testService = Application.Instance.ServiceFactory.CreateService<object>("TestService");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Common.Error11017]",
                    "Juhta.Net.Services.ServiceCreationException: [Juhta.Net.Error10080] An instance of the dependency injection service 'name:TestService' could not be created.",
                    "System.ArgumentException",
                    "[Juhta.Net.Common.Error11017] An instance of the class 'AppXLibrary.Services.TestService' could not be created because the type was not found in the assembly"
                );

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceCreationException))]
        public void Start_Services_NonExistingLibraryFile_ShouldThrowServiceCreationException()
        {
            object testService;

            SetConfigFiles("Root", "Services_NonExistingLibraryFile_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                testService = Application.Instance.ServiceFactory.CreateService<object>("TestService");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Error10080]",
                    "Juhta.Net.Services.ServiceCreationException: [Juhta.Net.Error10080] An instance of the dependency injection service 'name:TestService' could not be created.",
                    "System.IO.FileNotFoundException",
                    "AppXLibrary1234.dll' or one of its dependencies. The system cannot find the file specified."
                );

                throw;
            }
        }

        [TestMethod]
        public void Start_Services_NoServiceName_ServiceTypeImplemented_ShouldReturn()
        {
            IAllParamTypeService allParamTypeService;

            SetConfigFiles("Root", "Services_NoServiceName_ServiceTypeImplemented_");

            Application.StartInstance(null, s_configDirectory);

            allParamTypeService = Application.Instance.ServiceFactory.CreateService<IAllParamTypeService>();

            Assert.AreEqual<string>("This service has no name!", allParamTypeService.StringValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Start_Services_NoServiceName_ServiceTypeNotImplemented_ShouldThrowKeyNotFoundException()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                sumService = Application.Instance.ServiceFactory.CreateService<SumService>();
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Error10016]",
                    "System.Collections.Generic.KeyNotFoundException: [Juhta.Net.Error10016] No dependency injection service was found with the identifier 'type:AppXLibrary.Services.SumService'."
                );

                throw;
            }
        }

        [TestMethod]
        public void Start_Services_Services_ShouldReturn()
        {
            Service[] services;

            SetConfigFiles("Root", "Services_SumService10_");

            Application.StartInstance(null, s_configDirectory);

            services = Application.Instance.ServiceFactory.Services;

            Assert.AreEqual<int>(10, services.Length);

            var sumServices = services.Where(service => service.Id.Specifier.StartsWith("SumService"));

            Assert.AreEqual<int>(10, sumServices.Count());
        }

        [TestMethod]
        public void Start_Services_SumService10_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = Application.Instance.ServiceFactory.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        public void Start_Services_SumService10_ServiceGroups_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_ServiceGroups_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = Application.Instance.ServiceFactory.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        public void Start_Services_SumService10_ServiceGroups2_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_ServiceGroups2_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = Application.Instance.ServiceFactory.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        public void Start_Services_SumService10_ServiceGroups3_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_ServiceGroups3_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = Application.Instance.ServiceFactory.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        public void Start_Services_ValidServiceId_AllCharacters_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_ValidServiceId_AllCharacters_");

            Application.StartInstance(null, s_configDirectory);

            sumService = Application.Instance.ServiceFactory.CreateService<SumService>(new ServiceId("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789:abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._/-"));

            sumService.Add(19);

            Assert.AreEqual<int>(72 + 19, sumService.GetSum());
        }

        [TestMethod]
        public void Start_Services_ValidServiceIdSchemeAndSpecifier_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_");

            Application.StartInstance(null, s_configDirectory);

            sumService = Application.Instance.ServiceFactory.CreateService<SumService>("name", "SumService0");

            sumService.Add(9);

            Assert.AreEqual<int>(10 + 9, sumService.GetSum());
        }

        [TestMethod]
        public void Start_Services_ValidServiceIdSchemeAndSpecifier2_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_");

            Application.StartInstance(null, s_configDirectory);

            sumService = Application.Instance.ServiceFactory.CreateService<SumService>(new ServiceId("name", "SumService0"));

            sumService.Add(9);

            Assert.AreEqual<int>(10 + 9, sumService.GetSum());
        }

        [TestMethod]
        public void Start_Services_ValidServiceIdSchemeAndSpecifier3_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_");

            Application.StartInstance(null, s_configDirectory);

            sumService = Application.Instance.ServiceFactory.CreateService<SumService>(new ServiceId("name:SumService0"));

            sumService.Add(9);

            Assert.AreEqual<int>(10 + 9, sumService.GetSum());
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Start_Services_UndefinedServiceName_ShouldThrowKeyNotFoundException()
        {
            SumService sumService;

            SetConfigFiles("Root", "Services_SumService10_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                sumService = Application.Instance.ServiceFactory.CreateService<SumService>("SumService10");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Error10016]",
                    "System.Collections.Generic.KeyNotFoundException: [Juhta.Net.Error10016] No dependency injection service was found with the identifier 'name:SumService10'."
                );

                throw;
            }
        }

        [TestMethod]
        public void Start_StartableLibrary_ShouldReturn()
        {
            SetConfigFiles("Root", "StartableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.Startable.StartableLibrary.IsStarted);

            Application.Instance.Close();

            Assert.AreEqual<bool>(false, AppXLibrary.Startable.StartableLibrary.IsStarted);
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

        #region Private Methods

        private void StressTestMain(object paramObj)
        {
            Stopwatch stopwatch = new Stopwatch();
            string s;
            ReplaceService replaceService = new ReplaceService();
            StressTestParam param = (StressTestParam)paramObj;

            stopwatch.Start();

            do
                s = replaceService.Replace("Ho-Ho-Ho");
            while (s == $"Yo-Yo-Yo" && stopwatch.ElapsedMilliseconds <= 30000);

            param.Value = s + "-" + param.Index.ToString();
        }

        #endregion

        #region Private Types

        private class StressTestParam
        {
            #region Public Properties

            public int Index {get; set;}

            public string Value {get; set;}

            #endregion
        }

        #endregion
    }
}
