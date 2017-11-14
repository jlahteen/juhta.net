
using Juhta.Net.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Juhta.Net.Tests
{
    [TestClass]
    public class LoggerTests : TestClassBase
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
            DeleteConfigFilesAll();

            Application.InitializeInstance();
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void LogAlert_AlertMessage_ShouldReturn()
        {
            Logger.LogAlert(new AlertMessage("This is an alert message 'Alert #1'.", "Alert #1"));

            AssertDefaultLogFileContent("ALERT 'Alert #1'", "This is an alert message 'Alert #1'.");
        }

        [TestMethod]
        public void LogAlert_AlertMessageWithArgs_ShouldReturn()
        {
            Logger.LogAlert(new AlertMessage("This is an alert message '{0}'.", "Alert #3"), "Alert #3");

            AssertDefaultLogFileContent("ALERT 'Alert #3'", "This is an alert message 'Alert #3'.");
        }

        [TestMethod]
        public void LogAlert_StringMessage_ShouldReturn()
        {
            Logger.LogAlert("This is an alert message 'Alert #2'.");

            AssertDefaultLogFileContent("This is an alert message 'Alert #2'.");
        }

        [TestMethod]
        public void LogAlert_StringMessageWithArgs_ShouldReturn()
        {
            Logger.LogAlert("This is an alert message '{0}'.", "Alert #4");

            AssertDefaultLogFileContent("This is an alert message 'Alert #4'.");
        }

        [TestMethod]
        public void LogError_ErrorMessage_ShouldReturn()
        {
            Logger.LogError(new ErrorMessage("This is an error message 'Error #1'.", "Error #1"));

            AssertDefaultLogFileContent("ERROR 'Error #1'", "This is an error message 'Error #1'.");
        }

        [TestMethod]
        public void LogError_ErrorMessageWithArgs_ShouldReturn()
        {
            Logger.LogError(new ErrorMessage("This is an error message '{0}'.", "Error #3"), "Error #3");

            AssertDefaultLogFileContent("ERROR 'Error #3'", "This is an error message 'Error #3'.");
        }

        [TestMethod]
        public void LogError_Exception_ShouldReturn()
        {
            Logger.LogError(new Exception("This is an error message 'Error #5'.", new ArgumentException("This is an inner exception.")));

            AssertDefaultLogFileContent("This is an error message 'Error #5'.", "This is an inner exception.");
        }

        [TestMethod]
        public void LogError_StringMessage_ShouldReturn()
        {
            Logger.LogError("This is an error message 'Error #2'.");

            AssertDefaultLogFileContent("This is an error message 'Error #2'.");
        }

        [TestMethod]
        public void LogError_StringMessageWithArgs_ShouldReturn()
        {
            Logger.LogError("This is an error message '{0}'.", "Error #4");

            AssertDefaultLogFileContent("This is an error message 'Error #4'.");
        }

        [TestMethod]
        public void LogEvent_DiagnosticMessage_ShouldReturn()
        {
            Logger.LogEvent(new ErrorMessage("This is an error message 'Error #10'.", "Error #10"));

            AssertDefaultLogFileContent("ERROR 'Error #10'", "This is an error message 'Error #10'.");
        }

        [TestMethod]
        public void LogEvent_DiagnosticMessageWithArgs_ShouldReturn()
        {
            Logger.LogEvent(new ErrorMessage("This is an error message '{0}'.", "Error #11"), "Error #11");

            AssertDefaultLogFileContent("ERROR 'Error #11'", "This is an error message 'Error #11'.");
        }

        [TestMethod]
        public void LogInformation_InformationMessage_ShouldReturn()
        {
            Logger.LogInformation(new InformationMessage("This is an information message 'Information #1'.", "Information #1"));

            AssertDefaultLogFileContent("INFORMATION 'Information #1'", "This is an information message 'Information #1'.");
        }

        [TestMethod]
        public void LogInformation_InformationMessageWithArgs_ShouldReturn()
        {
            Logger.LogInformation(new InformationMessage("This is an information message '{0}'.", "Information #3"), "Information #3");

            AssertDefaultLogFileContent("INFORMATION 'Information #3'", "This is an information message 'Information #3'.");
        }

        [TestMethod]
        public void LogInformation_StringMessage_ShouldReturn()
        {
            Logger.LogInformation("This is an information message 'Information #2'.");

            AssertDefaultLogFileContent("This is an information message 'Information #2'.");
        }

        [TestMethod]
        public void LogInformation_StringMessageWithArgs_ShouldReturn()
        {
            Logger.LogInformation("This is an information message '{0}'.", "Information #4");

            AssertDefaultLogFileContent("This is an information message 'Information #4'.");
        }

        [TestMethod]
        public void LogWarning_WarningMessage_ShouldReturn()
        {
            Logger.LogWarning(new WarningMessage("This is a warning message 'Warning #1'.", "Warning #1"));

            AssertDefaultLogFileContent("WARNING 'Warning #1'", "This is a warning message 'Warning #1'.");
        }

        [TestMethod]
        public void LogWarning_WarningMessageWithArgs_ShouldReturn()
        {
            Logger.LogWarning(new WarningMessage("This is a warning message '{0}'.", "Warning #3"), "Warning #3");

            AssertDefaultLogFileContent("WARNING 'Warning #3'", "This is a warning message 'Warning #3'.");
        }

        [TestMethod]
        public void LogWarning_StringMessage_ShouldReturn()
        {
            Logger.LogWarning("This is a warning message 'Warning #2'.");

            AssertDefaultLogFileContent("This is a warning message 'Warning #2'.");
        }

        [TestMethod]
        public void LogWarning_StringMessageWithArgs_ShouldReturn()
        {
            Logger.LogWarning("This is a warning message '{0}'.", "Warning #4");

            AssertDefaultLogFileContent("This is a warning message 'Warning #4'.");
        }

        #endregion
    }
}
