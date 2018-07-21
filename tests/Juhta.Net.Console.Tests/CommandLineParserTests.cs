
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Juhta.Net.Console.Tests
{
    [TestClass]
    public class CommandLineParserTests
    {
        #region Test Methods

        [TestMethod]
        public void GetNamedArgument_ExistingNamedArgument_ShouldReturnNamedArgument()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            NamedArgument namedArgument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/inputFile:MyInputFile.txt",
                    "/targetFile:MyTargetFile.txt",
                    "-myLoggerClass",
                    "AppX.Logger"
                }
            );

            namedArgument = commandLineParser.GetNamedArgument("myLoggerClass");

            Assert.AreEqual<string>("myLoggerClass", namedArgument.Name);

            Assert.AreEqual<string>("AppX.Logger", namedArgument.Value);
        }

        [TestMethod]
        public void GetNamedArgument_NonExistingNamedArgument_DefaultValueGiven_ShouldReturnNamedArgument()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            NamedArgument namedArgument;

            commandLineParser.ParseArguments(null);

            namedArgument = commandLineParser.GetNamedArgument("inputFile", "MyInputFile.txt");

            Assert.AreEqual<string>("inputFile", namedArgument.Name);

            Assert.AreEqual<string>("MyInputFile.txt", namedArgument.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void GetNamedArgument_NonExistingNamedArgument_NoDefaultValueGiven_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            NamedArgument namedArgument;

            commandLineParser.ParseArguments(null);

            try
            {
                namedArgument = commandLineParser.GetNamedArgument("inputFile");
            }

            catch (CommandLineArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Console.Error103012]"));

                throw;
            }
        }

        [TestMethod]
        public void GetOptionArgument_ExistingOption_ShouldReturnOptionArgument()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            OptionArgument optionArgument; 

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/inputFile:MyInputFile.txt",
                    "/targetFile:MyTargetFile.txt",
                }
            );

            optionArgument = commandLineParser.GetOptionArgument("inputFile");

            Assert.AreEqual<string>("inputFile", optionArgument.Name);

            Assert.AreEqual<string>("MyInputFile.txt", optionArgument.Value);
        }

        [TestMethod]
        public void GetOptionArgument_NonExistingOption_DefaultValueGiven_ShouldReturnOptionArgument()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            OptionArgument optionArgument;

            commandLineParser.ParseArguments(null);

            optionArgument = commandLineParser.GetOptionArgument("inputFile", "MyInputFile.txt");

            Assert.AreEqual<string>("inputFile", optionArgument.Name);

            Assert.AreEqual<string>("MyInputFile.txt", optionArgument.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void GetOptionArgument_NonExistingOption_NoDefaultValueGiven_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            OptionArgument optionArgument;

            commandLineParser.ParseArguments(null);

            try
            {
                optionArgument = commandLineParser.GetOptionArgument("inputFile");
            }

            catch (CommandLineArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Console.Error103010]"));

                throw;
            }
        }

        [TestMethod]
        public void GetPlainArgument_ExistingPlainArgument_ShouldReturnPlainArgument()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            PlainArgument plainArgument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/inputFile:MyInputFile.txt",
                    "/targetFile:MyTargetFile.txt",
                    "ThisIsPlainArgument0",
                    "ThisIsPlainArgument1",
                    "-myLoggerClass",
                    "AppX.Logger"
                }
            );

            plainArgument = commandLineParser.GetPlainArgument(0);

            Assert.AreEqual<string>("ThisIsPlainArgument0", plainArgument.Value);

            plainArgument = commandLineParser.GetPlainArgument(1);

            Assert.AreEqual<string>("ThisIsPlainArgument1", plainArgument.Value);
        }

        [TestMethod]
        public void GetPlainArgument_NonExistingPlainArgument_DefaultValueGiven_ShouldReturnPlainArgument()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            PlainArgument plainArgument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/inputFile:MyInputFile.txt",
                    "/targetFile:MyTargetFile.txt",
                    "ThisIsPlainArgument0",
                    "ThisIsPlainArgument1",
                    "-myLoggerClass",
                    "AppX.Logger"
                }
            );

            plainArgument = commandLineParser.GetPlainArgument(2, "ThisIsPlainArgument2");

            Assert.AreEqual<string>("ThisIsPlainArgument2", plainArgument.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void GetPlainArgument_NonExistingPlainArgument_NoDefaultValueGiven_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            PlainArgument plainArgument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/inputFile:MyInputFile.txt",
                    "/targetFile:MyTargetFile.txt",
                    "ThisIsPlainArgument0",
                    "ThisIsPlainArgument1",
                    "-myLoggerClass",
                    "AppX.Logger"
                }
            );

            try
            {
                plainArgument = commandLineParser.GetPlainArgument(2);
            }

            catch (CommandLineArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Console.Error103014]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineParserException))]
        public void ParseArguments_ArgumentNamePrefixAndOptionPrefixSame_ShouldThrowCommandLineParserException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            try
            {
                commandLineParser.ParseArguments(null, "--", "--", "=");
            }

            catch (CommandLineParserException ex)
            {
                Assert.AreEqual<string>("[Juhta.Net.Console.Error103005] Argument name prefix and option prefix cannot be the same in the command line parser.", ex.Message);

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void ParseArguments_ArgumentValueMissing_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            try
            {
                commandLineParser.ParseArguments(
                    new string[]
                    {
                        "/inputFile:MyInputFile.txt",
                        "/targetFile:MyTargetFile.txt",
                        "-myLoggerClass",
                        "AppX.Logger",
                        "-myService"
                    }
                );
            }

            catch (CommandLineArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Console.Error103013]"));

                throw;
            }
        }

        [TestMethod]
        public void ParseArguments_ComplexArguments_ShouldReturn()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            NamedArgument namedArgument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "-sourceDirectory",
                    "C:\\Users\\Joe",
                    "-destinationDirectory",
                    "C:\\Temp",
                    "/S",
                    "/E",
                    "/ignoreFileTypes:*.dll;*.pdb;*.exe",
                    "MyFile1.json",
                    "MyFile2.json",
                    "MyFile3.json",
                    "/serviceName:AppService",
                    "/setting1:123",
                    "/setting2:2001-10-23",
                    "/generateReport:true",
                    "/reportType:html",
                    "-reportOutputFile",
                    "AppServiceReport.html"
                }
            );

            namedArgument = commandLineParser.GetNamedArgument("sourceDirectory");

            Assert.AreEqual<string>("C:\\Users\\Joe", namedArgument.Value);

            namedArgument = commandLineParser.GetNamedArgument("destinationDirectory");

            Assert.AreEqual<string>("C:\\Temp", namedArgument.Value);

            Assert.AreEqual<string>("true", commandLineParser.GetOptionArgument("S").Value);

            Assert.AreEqual<string>("true", commandLineParser.GetOptionArgument("E").Value);

            Assert.AreEqual<string>("*.dll;*.pdb;*.exe", commandLineParser.GetOptionArgument("ignoreFileTypes").Value);

            Assert.AreEqual<string>("AppService", commandLineParser.GetOptionArgument("serviceName").Value);

            Assert.AreEqual<string>("123", commandLineParser.GetOptionArgument("setting1").Value);

            Assert.AreEqual<string>("2001-10-23", commandLineParser.GetOptionArgument("setting2").Value);

            Assert.AreEqual<string>("true", commandLineParser.GetOptionArgument("generateReport").Value);

            Assert.AreEqual<string>("AppServiceReport.html", commandLineParser.GetNamedArgument("reportOutputFile").Value);

            Assert.AreEqual<bool>(true, commandLineParser.HasUnconsumedArguments);

            Assert.AreEqual<string>("MyFile1.json", commandLineParser.GetUnconsumedArguments()[0].Value);

            Assert.AreEqual<string>("MyFile2.json", commandLineParser.GetUnconsumedArguments()[1].Value);

            Assert.AreEqual<string>("MyFile3.json", commandLineParser.GetUnconsumedArguments()[2].Value);

            Assert.AreEqual<string>("MyFile1.json", commandLineParser.GetPlainArgument(0).Value);

            Assert.AreEqual<string>("MyFile2.json", commandLineParser.GetPlainArgument(1).Value);

            Assert.AreEqual<string>("MyFile3.json", commandLineParser.GetPlainArgument(2).Value);

            Assert.AreEqual<bool>(true, commandLineParser.HasUnconsumedArguments);

            Assert.AreEqual<string>("html", commandLineParser.GetOptionArgument("reportType").Value);

            Assert.AreEqual<bool>(false, commandLineParser.HasUnconsumedArguments);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void ParseArguments_EmptyArgumentGiven_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            try
            {
                commandLineParser.ParseArguments(
                    new string[]
                    {
                        "/inputFile:MyInputFile.txt",
                        "/targetFile:MyTargetFile.txt",
                        "   "
                    }
                );
            }

            catch (CommandLineArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Console.Error103011]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseArguments_InvalidArgumentNamePrefix_ShouldThrowArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            try
            {
                commandLineParser.ParseArguments(null, "%&¤", "/", ":");
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Common.Error100005] Invalid 'argumentNamePrefix' parameter value was passed to the method 'Juhta.Net.Console.CommandLineParser.ParseArguments'."));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseArguments_InvalidOptionNameValueSeparator_ShouldThrowArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            try
            {
                commandLineParser.ParseArguments(null, "--", "/", "==");
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Common.Error100005] Invalid 'optionNameValueSeparator' parameter value was passed to the method 'Juhta.Net.Console.CommandLineParser.ParseArguments'."));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseArguments_InvalidOptionPrefix_ShouldThrowArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            try
            {
                commandLineParser.ParseArguments(null, "--", "PRE", ":");
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Common.Error100005] Invalid 'optionPrefix' parameter value was passed to the method 'Juhta.Net.Console.CommandLineParser.ParseArguments'."));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void ParseArguments_NullArgumentGiven_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            try
            {
                commandLineParser.ParseArguments(
                    new string[]
                    {
                        "/inputFile:MyInputFile.txt",
                        "/targetFile:MyTargetFile.txt",
                        null
                    }
                );
            }

            catch (CommandLineArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Console.Error103011]"));

                throw;
            }
        }

        #endregion
    }
}
