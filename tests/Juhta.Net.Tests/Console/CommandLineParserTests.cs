
using Juhta.Net.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Juhta.Net.Tests.Console
{
    [TestClass]
    public class CommandLineParserTests
    {
        #region Test Methods

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
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10091]"));

                throw;
            }
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
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10092]"));

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
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10092]"));

                throw;
            }
        }

        #endregion
    }
}
