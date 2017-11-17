
using Juhta.Net.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Juhta.Net.Tests
{
    public abstract class TestClassBase
    {
        #region Static Constructor

        static TestClassBase()
        {
            s_configFilesRootDirectory = "..\\..\\..\\..\\Config".Replace('\\', Path.DirectorySeparatorChar);

            s_configDirectory = "..\\..\\..\\..\\Config\\Current".Replace('\\', Path.DirectorySeparatorChar);

            s_logDirectory = "..\\..\\..\\..\\Logs".Replace('\\', Path.DirectorySeparatorChar);
        }

        #endregion

        #region Public Properties

        public TestContext TestContext
        {
            get {return m_testContextInstance;}

            set {m_testContextInstance = value;}
        }

        #endregion

        #region Protected Methods

        protected void AssertDefaultLogFileContent(params string[] messageExcerpts)
        {
            string logFileContent;

            logFileContent = File.ReadAllText(GetDefaultLogFile());

            foreach (string messageExcerpt in messageExcerpts)
                Assert.IsTrue(logFileContent.Contains(messageExcerpt));
        }

        protected static void AssertLines(string[] expectedLines, string[] actualLines)
        {
            Assert.AreEqual<int>(expectedLines.Length, actualLines.Length);

            for (int i = 0; i < expectedLines.Length; i++)
                if (expectedLines[i].StartsWith("^") && expectedLines[i].EndsWith("$"))
                    Assert.IsTrue(Regex.IsMatch(actualLines[i], expectedLines[i]));
                else
                    Assert.AreEqual<string>(expectedLines[i], actualLines[i]);
        }

        protected static void DeleteConfigFiles(string configDirectory)
        {
            if (!Directory.Exists(configDirectory))
                return;

            foreach (string configFile in Directory.GetFiles(configDirectory, "*.config"))
                File.Delete(configFile);
        }

        protected static void DeleteConfigFilesAll()
        {
            DeleteConfigFiles(".");

            DeleteConfigFiles(s_configDirectory);
        }

        protected static void DeleteLogFiles(string logDirectory)
        {
            foreach (string configFile in Directory.GetFiles(logDirectory, "*.log"))
                File.Delete(configFile);
        }

        protected static string GetDefaultLogDirectory()
        {
            string defaultLogDirectory;

            defaultLogDirectory = Path.GetTempPath();

            if (!Directory.Exists(defaultLogDirectory))
                Directory.CreateDirectory(defaultLogDirectory);

            return(defaultLogDirectory);
        }

        protected static string GetDefaultLogFile()
        {
            string logFilePath;

            logFilePath = GetDefaultLogDirectory() + Process.GetCurrentProcess().ProcessName + ".log";

            return(logFilePath);
        }

        protected static string GetTestLogDirectory()
        {
            string testLogDirectory;

            testLogDirectory = Path.GetTempPath() + FrameworkInfo.FrameworkName + " Test Logs" + Path.DirectorySeparatorChar;

            if (!Directory.Exists(testLogDirectory))
                Directory.CreateDirectory(testLogDirectory);

            return(testLogDirectory);
        }

        protected bool HasTimeoutExceeded()
        {
            return(m_stopwatch.Elapsed.TotalSeconds > m_timeout);
        }

        protected static XmlDocument ReadXmlLog(string logFileName)
        {
            XmlDocument xmlLog = new XmlDocument();

            xmlLog.Load(s_logDirectory + Path.DirectorySeparatorChar  + logFileName);

            return(xmlLog);
        }

        protected static string SetConfigFiles(string configLoadDirectory, string configFilePrefix, string configDirectory = null)
        {
            FileInfo fileInfo;

            if (configDirectory == null)
            {
                configDirectory = s_configDirectory;

                if (!Directory.Exists(configDirectory))
                    Directory.CreateDirectory(configDirectory);
            }

            DeleteConfigFiles(configDirectory);

            if (configLoadDirectory != null)
            {
                foreach (string configFile in Directory.GetFiles(s_configFilesRootDirectory + Path.DirectorySeparatorChar + configLoadDirectory, configFilePrefix + "*.config"))
                    File.Copy(configFile, String.Format("{0}{1}{2}", configDirectory, Path.DirectorySeparatorChar, Path.GetFileName(configFile).RemoveStart(configFilePrefix)));

                foreach (string configFile in Directory.GetFiles(configDirectory, "*.config"))
                {
                    fileInfo = new FileInfo(configFile);

                    fileInfo.IsReadOnly = false;
                }
            }

            return(s_configDirectory);
        }

        protected void SetTimeout(int timeout)
        {
            m_stopwatch = new  System.Diagnostics.Stopwatch();

            m_stopwatch.Start();

            m_timeout = timeout;
        }

        #endregion

        #region Protected Fields

        protected static string s_configFilesRootDirectory;

        protected static string s_configDirectory;

        protected static string s_logDirectory;

        #endregion

        #region Private Fields

        private Stopwatch m_stopwatch;

        private TestContext m_testContextInstance;

        private int m_timeout;

        #endregion
    }
}
