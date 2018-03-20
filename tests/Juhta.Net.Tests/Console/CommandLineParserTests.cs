
using Juhta.Net.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Juhta.Net.Tests.Console
{
    [TestClass]
    public class CommandLineParserTests
    {
        #region Test Methods

        //[TestMethod]
        //public void GetOption_OneOptionWithValue_ShoulReturnCommandLineOption()
        //{
        //    string[] args;
        //    CommandLineArgsParser commandLineArgsParser;
        //    CommandLineOption option;

        //    args = new string[]
        //    {
        //        "/SaveOption=Force"
        //    };

        //    commandLineArgsParser = new CommandLineArgsParser(args, '/', '=');

        //    Assert.AreEqual<int>(1, commandLineArgsParser.OriginalArgCount);

        //    Assert.AreEqual<int>(1, commandLineArgsParser.CurrentArgCount);

        //    option = commandLineArgsParser.GetOption("SaveOption");

        //    Assert.AreEqual<string>("SaveOption", option.Name);

        //    Assert.AreEqual<string>("Force", option.Value);

        //    Assert.AreEqual<int>(0, commandLineArgsParser.CurrentArgCount);

        //    commandLineArgsParser.VerifyArgsConsumed();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(CommandLineArgException))]
        //public void GetOption_UnknownOption_ShouldThrowCommandLineArgException()
        //{
        //    string[] args;
        //    CommandLineArgsParser commandLineArgsParser;
        //    CommandLineOption option;

        //    args = new string[]
        //    {
        //        "/SkipValidation=true"
        //    };

        //    commandLineArgsParser = new CommandLineArgsParser(args, '/', '=');

        //    Assert.AreEqual<int>(1, commandLineArgsParser.OriginalArgCount);

        //    Assert.AreEqual<int>(1, commandLineArgsParser.CurrentArgCount);

        //    try
        //    {
        //        option = commandLineArgsParser.GetOption("SaveOption");
        //    }

        //    catch (CommandLineArgException ex)
        //    {
        //        Assert.AreEqual<string>("[Juhta.Net.Error10043] Option 'SaveOption' was not found in the command line arguments.", ex.Message);

        //        throw;
        //    }
        //}

        #endregion
    }
}
