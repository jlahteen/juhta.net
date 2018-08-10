
using AppXLibrary.CustomXmlConfig;
using AppXLibrary.DynamicCustomXmlConfigurableAndStartable;
using Juhta.Net.Common;
using Juhta.Net.Diagnostics;
using Juhta.Net.Extensions;
using Juhta.Net.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;

namespace Juhta.Net.Startup.Tests
{
    [TestClass]
    public class ApplicationTests : TestClassBase
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
            SetConfigFiles("Startup", "StartableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.Startable.StartableLibrary.IsStarted);

            AppXLibrary.Startable.StartableLibrary.StopProcessesReturnValue = false;

            Application.Instance.Close();

            Assert.AreEqual<bool>(false, AppXLibrary.Startable.StartableLibrary.IsStarted);

            AssertDefaultLogFileContent("Juhta.Net.Startup.Warning106072", "At least one error occurred when the processes of the library 'AppXLibrary.dll' were being stopped.");
        }

        [TestMethod]
        public void Close_StartableLibrary_StopProcessesThrowsException_ShouldReturn()
        {
            SetConfigFiles("Startup", "StartableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.Startable.StartableLibrary.IsStarted);

            AppXLibrary.Startable.StartableLibrary.StopProcessesException = new Exception("This is an injected exception.");

            Application.Instance.Close();

            Assert.AreEqual<bool>(true, AppXLibrary.Startable.StartableLibrary.IsStarted);

            AssertDefaultLogFileContent("Juhta.Net.Startup.Error106071", "An unexpected error occurred when the processes of the library 'AppXLibrary.dll' were being stopped.", "This is an injected exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void Start_BrokenConfig_ShouldThrowXmlException()
        {
            SetConfigFiles("Startup", "BrokenConfig_", ".");

            Application.StartInstance();
        }

        [TestMethod]
        public void Start_ClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Startup", "ClosableLibrary_");

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
            SetConfigFiles("Startup", "SimpleConfig_");

            Application.StartInstance(null, s_configDirectory);
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Ini_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Startup", "ConfigurableLibrary_Ini_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Startup", "ConfigurableLibrary_Json_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_ConfigFileNameOverriddenInConfig_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Startup", "ConfigurableLibrary_Json_ConfigFileNameOverriddenInConfig_");

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
            SetConfigFiles("Startup", "ConfigurableLibrary_Json_NullConfigFileName_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch (LibraryInitializationException ex)
            {
                Assert.IsTrue(ex.InnerException.Message.StartsWith("[Juhta.Net.Startup.Error106014] Configuration file name cannot be null for the configurable library 'AppXLibrary.dll'."));

                throw;
            }
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_NullConfigFileName_ConfigFileNameSpecifiedInConfig_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Startup", "ConfigurableLibrary_Json_NullConfigFileName_ConfigFileNameSpecifiedInConfig_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Json_NullConfigFileName_DefaultConfigFileNameSpecifiedInConfig_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Startup", "ConfigurableLibrary_Json_NullConfigFileName_DefaultConfigFileNameSpecifiedInConfig_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_ConfigurableLibrary_Xml_ShouldReturn()
        {
            AppXLibrary.Configurable.StringCache stringCache;

            SetConfigFiles("Startup", "ConfigurableLibrary_Xml_");

            Application.StartInstance(null, s_configDirectory);

            stringCache = AppXLibrary.Configurable.StringCache.Instance;

            for (int i = 0; i < 10; i++)
                Assert.AreEqual<string>($"This is String{i}", stringCache.Get($"String{i}"));
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableAndClosableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Startup", "CustomXmlConfigurableAndClosableLibrary_");

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

            SetConfigFiles("Startup", "CustomXmlConfigurableAndClosableLibrary_CloseLibraryReturnsFalse_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("WARNING event", "[Juhta.Net.Startup.Warning106017] At least one error occurred when the library 'AppXLibrary.dll' was closed.");
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableAndClosableLibrary_CloseLibraryThrowsException_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Startup", "CustomXmlConfigurableAndClosableLibrary_CloseLibraryThrowsException_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(457473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is another configured StringSetting value.", libraryConfig.GetStringSetting());

            Application.Instance.Close();

            Assert.AreEqual<int>(Int32.MaxValue, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("<closed>", libraryConfig.GetStringSetting());

            AssertDefaultLogFileContent("ERROR event", "[Juhta.Net.Startup.Error106004] An unexpected error occurred when the library 'AppXLibrary.dll' was being closed.", "Something went wrong in the closing of AppXLibrary.");
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Startup", "CustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(473473383, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the configured StringSetting value.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        public void Start_CustomXmlConfigurableLibrary100_ShouldReturn()
        {
            ILibraryConfig libraryConfig;

            SetConfigFiles("Startup", "CustomXmlConfigurableLibrary100_");

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
            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Ini_");

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

            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Ini_");

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
            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Json_");

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

            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Json_");

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
            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Json_");

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
                "[Juhta.Net.Startup.Warning106063] Library Manager detected that the configuration file",
                "RenamedAppXLibrary.json' was created or changed, but no actions were performed because there were no dynamic libraries associated with this configuration file.",
                "INFORMATION event",
                "[Juhta.Net.Startup.Info106012] Library Manager detected that the configuration file",
                "AppXLibrary.json' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Json_ConfigFileRenamingIntoConfigDirectory_ShouldReturn()
        {
            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Json_");

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
                "[Juhta.Net.Startup.Info106065] Library Manager detected that the configuration file",
                "AppXLibrary.json' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Json_ConfigFileRenamingOutOfConfigDirectory_ShouldReturn()
        {
            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Json_");

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
                "[Juhta.Net.Startup.Info106012] Library Manager detected that the configuration file",
                "AppXLibrary.json' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicConfigurableLibrary_Xml_ShouldReturn()
        {
            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Xml_");

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

            SetConfigFiles("Startup", "DynamicConfigurableLibrary_Xml_");

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

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_ConfigFileDeletion_ShouldReturn()
        {
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            File.Delete(appXConfigFilePath);

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("Hello there, what's up?", currentGreeting);

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Startup.Info106012] Library Manager detected that the configuration file",
                "AppXLibrary.config' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_ConfigFileDeletionAndCreation_ShouldReturn()
        {
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            File.Delete(appXConfigFilePath);

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("Hello there, what's up?", currentGreeting);

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableLibrary_");

            Thread.Sleep(2000);

            currentGreeting = AppXLibrary.DynamicCustomXmlConfigurable.GreetingService.GetGreeting();

            Assert.AreEqual<string>("This is the current greeting.#%!", currentGreeting);

            AssertDefaultLogFileContent(
                "INFORMATION event",
                "[Juhta.Net.Startup.Info106012] Library Manager detected that the configuration file",
                "AppXLibrary.config' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully.",
                "INFORMATION event",
                "[Juhta.Net.Startup.Info106065] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully.",
                "WARNING event",
                "[Juhta.Net.Startup.Warning106063] Library Manager detected that the configuration file",
                "Juhta.Net.Startup.config' was created or changed, but no actions were performed because there were no dynamic libraries associated with this configuration file."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_InvalidConfigChange_ShouldReturn()
        {
            XmlDocument appXConfig = new XmlDocument();
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableLibrary_");

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
                "[Juhta.Net.Startup.Error106064] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. The state of the library was left unmodified.",
                "Juhta.Net.Common.InvalidConfigFileException: [Juhta.Net.Startup.Error106002] XML configuration file",
                "AppXLibrary.config' does not conform to the configuration schema(s) of the custom XML configurable library 'AppXLibrary.dll'.",
                "Juhta.Net.Validation.ValidationException: [Juhta.Net.Validation.Error102004] XML document is not valid according to the given schema(s).",
                "ALERT event",
                "[Juhta.Net.Startup.Alert106005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_ValidConfigChange_ShouldReturn()
        {
            XmlDocument appXConfig = new XmlDocument();
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableLibrary_");

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
                "[Juhta.Net.Startup.Info106065] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableLibrary_UnknownConfigFileDeletion_ShouldReturn()
        {
            string currentGreeting;
            string appXConfigFilePath = s_configDirectory + "\\AppXLibrary.config";
            string unknownConfigFilePath = s_configDirectory + "\\Unknown.config";

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableLibrary_");

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
                "[Juhta.Net.Startup.Warning106011] Library Manager detected that the configuration file",
                "Unknown.config' was deleted, but no actions were performed because there were no dynamic libraries associated with this configuration file."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_ShouldReturn()
        {
            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_");

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

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_");

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
                "[Juhta.Net.Startup.Info106065] Library Manager detected that the configuration file",
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

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_");

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

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_StartProcessesThrowsException_");

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
                "Juhta.Net.Startup.LibraryStateException: [Juhta.Net.Startup.Error106061] Processes in the new state of the library 'AppXLibrary.dll' could not be started.",
                "System.InvalidOperationException: Cannot replace with 'XYZ' strings. Please use any other token but not that. Sorry.",
                "[Juhta.Net.Startup.Warning106078] Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. NOTE: The library continues running with the current state.",
                "ALERT event",
                "[Juhta.Net.Startup.Alert106005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
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

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_StopProcessesReturnsFalse_");

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
                    "[Juhta.Net.Startup.Error106066] Library Manager detected that the configuration file",
                    "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated.",
                    "NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.",
                    "[Juhta.Net.Startup.Error106075] An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped.",
                    "Juhta.Net.Startup.LibraryStateException: [Juhta.Net.Startup.Error106059] Processes in the current state of the library 'AppXLibrary.dll' could not be completely stopped.",
                    "ALERT event",
                    "[Juhta.Net.Startup.Alert106005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
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

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_ConfigChange_StopProcessesThrowsException_");

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
                "Juhta.Net.Startup.LibraryStateException: [Juhta.Net.Startup.Error106075] An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped. ---> System.Exception: Processes could not be stopped.",
                "ALERT event",
                "[Juhta.Net.Startup.Alert106005] Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_StopProcessesReturnsFalse_ShouldReturn()
        {
            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_StopProcessesReturnsFalse_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.DynamicCustomXmlConfigurableAndStartable.ReplaceProcessInfo.IsStarted);

            Application.CloseInstance();

            AssertDefaultLogFileContent(
                "WARNING event",
                "[Juhta.Net.Startup.Warning106073] At least one error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable_StopProcessesThrowsException_ShouldReturn()
        {
            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable_StopProcessesThrowsException_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<bool>(true, AppXLibrary.DynamicCustomXmlConfigurableAndStartable.ReplaceProcessInfo.IsStarted);

            Application.CloseInstance();

            AssertDefaultLogFileContent(
                "ERROR event",
                "[Juhta.Net.Startup.Error106075] An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped.",
                "System.Exception: Processes could not be stopped."
            );
        }

        [TestMethod]
        public void Start_DynamicCustomXmlConfigurableAndStartable100_ShouldReturn()
        {
            IReplaceService replaceService;
            string s;

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable100_");

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

            SetConfigFiles("Startup", "DynamicCustomXmlConfigurableAndStartable100_ConfigChange_");

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
                    "[Juhta.Net.Startup.Info106065] Library Manager detected that the configuration file",
                    $"AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibraryTempC{i}.dll' was updated successfully."
                );
        }

        [TestMethod]
        [ExpectedException(typeof(LibraryInitializationException))]
        public void Start_CustomXmlConfigurableLibrary_InvalidConfigValue_ShouldThrowLibraryInitializationException()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Startup", "CustomXmlConfigurableLibrary_InvalidConfigValue_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch
            {
                AssertDefaultLogFileContent("ERROR event", "[Juhta.Net.Startup.Error106003] Initialization of the library 'AppXLibrary.dll' failed.", "IntSetting 1234567 is invalid. Please use any other integer value but not this one!");

                throw;
            }
        }

        [TestMethod]
        public void Start_InitializableLibrary_ShouldReturn()
        {
            LibraryConfig libraryConfig = new LibraryConfig();

            SetConfigFiles("Startup", "InitializableLibrary_");

            Application.StartInstance(null, s_configDirectory);

            Assert.AreEqual<int>(89473537, libraryConfig.GetIntSetting());

            Assert.AreEqual<string>("This is the default value for the StringSetting.", libraryConfig.GetStringSetting());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidConfigFileException))]
        public void Start_InvalidConfig_InvalidHandleClass_ShouldThrowInvalidConfigFileException()
        {
            SetConfigFiles("Startup", "InvalidConfig_InvalidHandleClass_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch
            {
                AssertDefaultLogFileContent(
                    "ERROR event",
                    "[Juhta.Net.Startup.Error106006] An error occurred when the application",
                    "Juhta.Net.Common.InvalidConfigFileException: [Juhta.Net.Startup.Error106002] XML configuration file",
                    "does not conform to the configuration schema(s) of the custom XML configurable library 'Juhta.Net.Startup.dll'.",
                    "Juhta.Net.Validation.ValidationException: [Juhta.Net.Validation.Error102004] XML document is not valid according to the given schema(s).",
                    "System.Xml.Schema.XmlSchemaValidationException: The 'handleClass' attribute is invalid - The value '.Initializable.InitializableLibrary' is invalid according to its datatype 'http://schemas.juhta.net/common-v1.xsd:shortClassIdType' - The Pattern constraint failed."
                );

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidConfigFileException))]
        public void Start_InvalidConfig_UndefinedElement_ShouldThrowInvalidConfigFileException()
        {
            SetConfigFiles("Startup", "InvalidConfig_UndefinedElement_", ".");

            try
            {
                Application.StartInstance();
            }

            catch
            {
                AssertDefaultLogFileContent("[Juhta.Net.Startup.Error106002]");

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
            SetConfigFiles("Startup", "SimpleConfig_", ".");

            Application.StartInstance();

            Assert.AreEqual<string>(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName), Application.Instance.Name);

            Assert.AreEqual<string>(null, Application.Instance.DefaultConfigFileName);
        }

        [TestMethod]
        public void Start_NoParameters_SimpleConfig2_ShouldReturn()
        {
            SetConfigFiles("Startup", "SimpleConfig2_", ".");

            Application.StartInstance();

            Assert.AreEqual<string>(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName), Application.Instance.Name);

            Assert.AreEqual<string>(null, Application.Instance.DefaultConfigFileName);
        }

        [TestMethod]
        public void Start_NoParameters_SimpleConfig_NameAndDefaultConfigFileNameSpecified_ShouldReturn()
        {
            SetConfigFiles("Startup", "SimpleConfig_NameAndDefaultConfigFileNameSpecified_", ".");

            Application.StartInstance();

            Assert.AreEqual<string>("Application X", Application.Instance.Name);

            Assert.AreEqual<string>("SomeConfig.ini", Application.Instance.DefaultConfigFileName);
        }

        [TestMethod]
        public void Start_StartableLibrary_ShouldReturn()
        {
            SetConfigFiles("Startup", "StartableLibrary_");

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
