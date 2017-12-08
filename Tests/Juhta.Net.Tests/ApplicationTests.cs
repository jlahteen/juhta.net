
using AppXLibrary;
using AppXLibrary.DynamicCustomXmlConfigurableAndStartable;
using Juhta.Net.Common;
using Juhta.Net.Extensions;
using Juhta.Net.LibraryManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
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

            AppXLibrary.Cloning.Clone.BuildCopies("AppXLibraryTempA", 100, "AppXLibrary.Cloning");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 1; i <= 100; i++)
            {
                libraryConfig = ObjectFactory.CreateInstance<ILibraryConfig>($"AppXLibraryTempA{i}.dll", $"AppXLibraryTempA{i}.LibraryConfig");

                Assert.AreEqual<string>($"This is the configured StringSetting value._{i:000}", libraryConfig.GetStringSetting());
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
                "INFORMATION 'Juhta.Net.Info10012'",
                "Library Manager detected that the configuration file",
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
                "INFORMATION 'Juhta.Net.Info10012'",
                "Library Manager detected that the configuration file",
                "AppXLibrary.config' was deleted, and the state of the associated dynamic library 'AppXLibrary.dll' was initialized successfully.",
                "INFORMATION 'Juhta.Net.Info10065'",
                "AppXLibrary.config' was created or changed, and the state of the associated dynamic library 'AppXLibrary.dll' was updated successfully.",
                "WARNING 'Juhta.Net.Warning10063'",
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
                "ERROR 'Juhta.Net.Error10002'",
                "Library Manager detected that the configuration file",
                "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. The state of the library was left unmodified.",
                "AppXLibrary.config' does not conform to the configuration schema(s) of the custom XML configurable library 'AppXLibrary.dll'.",
                "'Juhta.Net.Alert10005'",
                "Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
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
                "INFORMATION 'Juhta.Net.Info10065'",
                "Library Manager detected that the configuration file",
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
                "WARNING 'Juhta.Net.Warning10011'",
                "Library Manager detected that the configuration file",
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
                "INFORMATION 'Juhta.Net.Info10065'",
                "Library Manager detected that the configuration file",
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
                "ERROR 'Juhta.Net.Error10061'",
                "Juhta.Net.LibraryManagement.LibraryStateException: Processes in the new state of the library 'AppXLibrary.dll' could not be started.",
                "System.InvalidOperationException: Cannot replace with 'XYZ' strings. Please use any other token but not that. Sorry.",
                "WARNING 'Juhta.Net.Warning10078'",
                "AppXLibrary.config' was created or changed but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. NOTE: The library continues running with the current state.",
                "ALERT 'Juhta.Net.Alert10005'",
                "Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
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
                    "ERROR 'Juhta.Net.Error10059'",
                    "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated.",
                    "NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.",
                    "An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped.",
                    "Juhta.Net.LibraryManagement.LibraryStateException: Processes in the current state of the library 'AppXLibrary.dll' could not be completely stopped.",
                    "ALERT 'Juhta.Net.Alert10005'",
                    "Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
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
                "ERROR 'Juhta.Net.Error10075'",
                "AppXLibrary.config' was created or changed, but the state of the associated dynamic library 'AppXLibrary.dll' could not be updated. NOTE: The state of the library is currently unstable. You should restore the configuration file and possibly restart the process.",
                "Juhta.Net.LibraryManagement.LibraryStateException: An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped. ---> System.Exception: Processes could not be stopped.",
                "ALERT 'Juhta.Net.Alert10005'",
                "Library Manager detected changes in the configuration but failed to update the states of the associated dynamic libraries. The state of the process may be unstable. Please refer to the log events for more information."
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
                "WARNING 'Juhta.Net.Warning10073'",
                "At least one error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped."
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
                "ERROR 'Juhta.Net.Error10075'",
                "An unexpected error occurred when the processes in the current state of the library 'AppXLibrary.dll' were being stopped.",
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
                    "INFORMATION 'Juhta.Net.Info10065'",
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
            while (s == $"Yo-Yo-Yo" && stopwatch.ElapsedMilliseconds <= 15000);

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
