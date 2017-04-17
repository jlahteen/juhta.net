
using Juhta.Net;
using Juhta.Net.Extensions;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public abstract class TestClassBase
    {
        #region Public Properties

        public TestContext TestContext
        {
            get {return m_testContextInstance;}

            set {m_testContextInstance = value;}
        }

        #endregion

        #region Protected Methods

        protected static void AssertLines(string[] expectedLines, string[] actualLines)
        {
            Assert.AreEqual<int>(expectedLines.Length, actualLines.Length);

            for (int i = 0; i < expectedLines.Length; i++)
                if (expectedLines[i].StartsWith("^") && expectedLines[i].EndsWith("$"))
                    Assert.IsTrue(Regex.IsMatch(actualLines[i], expectedLines[i]));
                else
                    Assert.AreEqual<string>(expectedLines[i], actualLines[i]);
        }

        protected bool HasTimeoutExceeded()
        {
            return(m_stopwatch.Elapsed.TotalSeconds > m_timeout);
        }

        protected void InitializeFramework()
        {
            Startup.InitializeFramework(SetCurrentConfig(null, null));
        }

        protected void InitializeFramework(string configSubDirectory, string configFilePrefix)
        {
            Startup.InitializeFramework(SetCurrentConfig(configSubDirectory, configFilePrefix));
        }

        protected static XmlDocument ReadXmlLog(string logFileName)
        {
            XmlDocument xmlLog = new XmlDocument();

            xmlLog.Load(c_logDirectory + "\\"  + logFileName);

            return(xmlLog);
        }

        protected static string SetCurrentConfig(string configSubDirectory, string configFilePrefix)
        {
            FileInfo fileInfo;

            foreach (string configFile in Directory.GetFiles(c_currentConfigDirectory))
                File.Delete(configFile);

            if (configSubDirectory != null)
            {
                foreach (string configFile in Directory.GetFiles(c_configFilesDirectory + "\\" + configSubDirectory, configFilePrefix + "*.config"))
                    File.Copy(configFile, String.Format("{0}\\{1}", c_currentConfigDirectory, Path.GetFileName(configFile).RemoveStart(configFilePrefix)));

                foreach (string configFile in Directory.GetFiles(c_currentConfigDirectory))
                {
                    fileInfo = new FileInfo(configFile);

                    fileInfo.IsReadOnly = false;
                }
            }

            return(c_currentConfigDirectory);
        }

        protected void SetTimeout(int timeout)
        {
            m_stopwatch = new  System.Diagnostics.Stopwatch();

            m_stopwatch.Start();

            m_timeout = timeout;
        }

        #endregion

        #region Protected Constants

        protected const string c_configFilesDirectory = "..\\..\\..\\Tests\\Config\\ConfigFiles";

        protected const string c_currentConfigDirectory = "..\\..\\..\\Tests\\Config\\CurrentConfig";

        protected const string c_logDirectory = "..\\..\\..\\Tests\\LogDirectory";

        #endregion

        #region Private Fields

        private System.Diagnostics.Stopwatch m_stopwatch;

        private TestContext m_testContextInstance;

        private int m_timeout;

        #endregion
    }
}
