
using AppXLibrary.DynamicCustomXmlConfigurableAndStartable;
using AppXLibrary.Services;
using Juhta.Net.Common;
using Juhta.Net.Startup;
using Juhta.Net.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Juhta.Net.Services.Tests
{
    [TestClass]
    public class ServiceFactoryTests : TestClassBase
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
        public void CreateService_AggregateSumService_ServiceGroups_ShouldReturn()
        {
            AggregateSumService aggregateSumService;

            SetConfigFiles("Services", "AggregateSumService_");

            Application.StartInstance(null, s_configDirectory);

            aggregateSumService = ServiceFactory.Instance.CreateService<AggregateSumService>();

            aggregateSumService.Add(15);

            Assert.AreEqual<int>(11 + 12 + 13 + 3 * 15, aggregateSumService.GetSum());
        }

        [TestMethod]
        public void CreateService_AllParamTypeService_ShouldReturn()
        {
            IAllParamTypeService allParamTypeService;

            SetConfigFiles("Services", "AllParamTypeService_");

            Application.StartInstance(null, s_configDirectory);

            allParamTypeService = ServiceFactory.Instance.CreateService<IAllParamTypeService>("AllParamTypeService");

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
        [ExpectedException(typeof(ArgumentException))]
        public void CreateService_InvalidServiceIdScheme_ShouldThrowArgumentException()
        {
            SetConfigFiles("Services", "AllParamTypeService_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                ServiceFactory.Instance.CreateService<SumService>("scheme%", "SumService");
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
        public void CreateService_InvalidServiceIdSpecifier_ShouldThrowArgumentException()
        {
            SetConfigFiles("Services", "AllParamTypeService_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                ServiceFactory.Instance.CreateService<SumService>("scheme", "SumService%");
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid 'specifier' parameter value was passed to the method 'Juhta.Net.Services.ServiceId..ctor'."));

                Assert.IsTrue(ex.Message.Contains("The value 'SumService%' does not conform to the regex pattern '^([a-zA-Z0-9\\._/-])+$'."));

                throw;
            }
        }

        [TestMethod]
        public void CreateService_NoConstructorParams_DefaultConstructor1_ShouldReturn()
        {
            SumService2 sumService2;

            SetConfigFiles("Services", "NoConstructorParams_DefaultConstructor_");

            Application.StartInstance(null, s_configDirectory);

            sumService2 = ServiceFactory.Instance.CreateService<SumService2>("SumService20");

            sumService2.Add(10);

            Assert.AreEqual<int>(100 + 10, sumService2.GetSum());
        }

        [TestMethod]
        public void CreateService_NoConstructorParams_DefaultConstructor2_ShouldReturn()
        {
            SumService2 sumService2;

            SetConfigFiles("Services", "NoConstructorParams_DefaultConstructor_");

            Application.StartInstance(null, s_configDirectory);

            sumService2 = ServiceFactory.Instance.CreateService<SumService2>("SumService21");

            sumService2.Add(11);

            Assert.AreEqual<int>(100 + 11, sumService2.GetSum());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceCreationException))]
        public void CreateService_NoConstructorParams_NoDefaultConstructor_ShouldThrowServiceCreationException()
        {
            SumService sumService;

            SetConfigFiles("Services", "NoConstructorParams_NoDefaultConstructor_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                sumService = ServiceFactory.Instance.CreateService<SumService>("SumService");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Services.Error105004]",
                    "Juhta.Net.Services.ServiceCreationException: [Juhta.Net.Services.Error105004] An instance of the dependency injection service 'name:SumService' could not be created.",
                    "System.MissingMethodException: Constructor on type 'AppXLibrary.Services.SumService' not found."
                );

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceCreationException))]
        public void CreateService_NonExistingLibraryClass_ShouldThrowServiceCreationException()
        {
            object testService;

            SetConfigFiles("Services", "NonExistingLibraryClass_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                testService = ServiceFactory.Instance.CreateService<object>("TestService");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Common.Error100017]",
                    "Juhta.Net.Services.ServiceCreationException: [Juhta.Net.Services.Error105004] An instance of the dependency injection service 'name:TestService' could not be created.",
                    "System.ArgumentException",
                    "[Juhta.Net.Common.Error100017] An instance of the class 'AppXLibrary.Services.TestService' could not be created because the type was not found in the assembly"
                );

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceCreationException))]
        public void CreateService_NonExistingLibraryFile_ShouldThrowServiceCreationException()
        {
            object testService;

            SetConfigFiles("Services", "NonExistingLibraryFile_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                testService = ServiceFactory.Instance.CreateService<object>("TestService");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Services.Error105004]",
                    "Juhta.Net.Services.ServiceCreationException: [Juhta.Net.Services.Error105004] An instance of the dependency injection service 'name:TestService' could not be created.",
                    "System.IO.FileNotFoundException",
                    "AppXLibrary1234.dll' or one of its dependencies. The system cannot find the file specified."
                );

                throw;
            }
        }

        [TestMethod]
        public void CreateService_NoServiceName_ServiceTypeImplemented_ShouldReturn()
        {
            IAllParamTypeService allParamTypeService;

            SetConfigFiles("Services", "NoServiceName_ServiceTypeImplemented_");

            Application.StartInstance(null, s_configDirectory);

            allParamTypeService = ServiceFactory.Instance.CreateService<IAllParamTypeService>();

            Assert.AreEqual<string>("This service has no name!", allParamTypeService.StringValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void CreateService_NoServiceName_ServiceTypeNotImplemented_ShouldThrowKeyNotFoundException()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                sumService = ServiceFactory.Instance.CreateService<SumService>();
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Services.Error105001]",
                    "System.Collections.Generic.KeyNotFoundException: [Juhta.Net.Services.Error105001] No dependency injection service was found with the identifier 'type:AppXLibrary.Services.SumService'."
                );

                throw;
            }
        }

        [TestMethod]
        public void CreateService_SumService10_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = ServiceFactory.Instance.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        public void CreateService_SumService10_ServiceGroups_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_ServiceGroups_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = ServiceFactory.Instance.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        public void CreateService_SumService10_ServiceGroups2_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_ServiceGroups2_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = ServiceFactory.Instance.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        public void CreateService_SumService10_ServiceGroups3_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_ServiceGroups3_");

            Application.StartInstance(null, s_configDirectory);

            for (int i = 0; i < 9; i++)
            {
                sumService = ServiceFactory.Instance.CreateService<SumService>($"SumService{i}");

                sumService.Add(10 + i);

                Assert.AreEqual<int>(10 + i + 10 + i, sumService.GetSum());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void CreateService_UndefinedServiceName_ShouldThrowKeyNotFoundException()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_");

            Application.StartInstance(null, s_configDirectory);

            try
            {
                sumService = ServiceFactory.Instance.CreateService<SumService>("SumService10");
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                AssertDefaultLogFileContent(
                    "[Juhta.Net.Services.Error105001]",
                    "System.Collections.Generic.KeyNotFoundException: [Juhta.Net.Services.Error105001] No dependency injection service was found with the identifier 'name:SumService10'."
                );

                throw;
            }
        }

        [TestMethod]
        public void CreateService_ValidServiceId_AllCharacters_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "ValidServiceId_AllCharacters_");

            Application.StartInstance(null, s_configDirectory);

            sumService = ServiceFactory.Instance.CreateService<SumService>(new ServiceId("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789:abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._/-"));

            sumService.Add(19);

            Assert.AreEqual<int>(72 + 19, sumService.GetSum());
        }

        [TestMethod]
        public void CreateService_ValidServiceIdSchemeAndSpecifier_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_");

            Application.StartInstance(null, s_configDirectory);

            sumService = ServiceFactory.Instance.CreateService<SumService>("name", "SumService0");

            sumService.Add(9);

            Assert.AreEqual<int>(10 + 9, sumService.GetSum());
        }

        [TestMethod]
        public void CreateService_ValidServiceIdSchemeAndSpecifier2_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_");

            Application.StartInstance(null, s_configDirectory);

            sumService = ServiceFactory.Instance.CreateService<SumService>(new ServiceId("name", "SumService0"));

            sumService.Add(9);

            Assert.AreEqual<int>(10 + 9, sumService.GetSum());
        }

        [TestMethod]
        public void CreateService_ValidServiceIdSchemeAndSpecifier3_ShouldReturn()
        {
            SumService sumService;

            SetConfigFiles("Services", "SumService10_");

            Application.StartInstance(null, s_configDirectory);

            sumService = ServiceFactory.Instance.CreateService<SumService>(new ServiceId("name:SumService0"));

            sumService.Add(9);

            Assert.AreEqual<int>(10 + 9, sumService.GetSum());
        }

        [TestMethod]
        public void Services_ShouldReturn()
        {
            Service[] services;

            SetConfigFiles("Services", "SumService10_");

            Application.StartInstance(null, s_configDirectory);

            services = ServiceFactory.Instance.Services;

            Assert.AreEqual<int>(10, services.Length);

            var sumServices = services.Where(service => service.Id.Specifier.StartsWith("SumService"));

            Assert.AreEqual<int>(10, sumServices.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(LibraryInitializationException))]
        public void Startup_FractionSecondInDateTimeConstructorParam_ShouldThrowLibraryInitializationException()
        {
            SetConfigFiles("Services", "FractionSecondInDateTimeConstructorParam_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch (Exception ex)
            {
                AssertDefaultLogFileContent(
                    "[Juhta.Net.Services.Error105003]",
                    "Juhta.Net.Services.ServiceInitializationException: [Juhta.Net.Services.Error105005] Dependency injection service 'name:AnyService' could not be initialized.",
                    "Juhta.Net.Services.ConstructorParamException: [Juhta.Net.Services.Error105002] Constructor parameter 'dateTimeValue' could not be initialized.",
                    "Juhta.Net.Common.InvalidConfigValueException: [Juhta.Net.Services.Error105003] Value '2018-02-04T16:55:00.7774' of the constructor parameter 'dateTimeValue' is not a valid 'DateTime' parameter value."
                );

                Assert.IsTrue(ex.InnerException is ServiceInitializationException);

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LibraryInitializationException))]
        public void Startup_IdNotGiven_ShouldThrowLibraryInitializationException()
        {
            SetConfigFiles("Services", "IdNotGiven_");

            try
            {
                Application.StartInstance(null, s_configDirectory);
            }

            catch (Exception ex)
            {
                AssertDefaultLogFileContent(
                    "[Juhta.Net.Startup.Error101002]",
                    "Juhta.Net.Common.InvalidConfigFileException: [Juhta.Net.Startup.Error101002] XML configuration file",
                    "[Juhta.Net.Startup.Error101002] XML configuration file",
                    "Juhta.Net.Services.config' does not conform to the configuration schema(s) of the custom XML configurable library 'Juhta.Net.Services.dll'.",
                    "Juhta.Net.Validation.ValidationException: [Juhta.Net.Validation.Error102004] XML document is not valid according to the given schema(s).",
                    "System.Xml.Schema.XmlSchemaValidationException: The required attribute 'id' is missing."
                );

                Assert.IsTrue(ex.InnerException is InvalidConfigFileException);

                throw;
            }
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
