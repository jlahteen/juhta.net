
using Juhta.Net.Console;
using Juhta.Net.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Juhta.Net.Tests.Console
{
    [TestClass]
    public class CommandLineArgumentTests
    {
        #region Test Methods

        [TestMethod]
        public void GetValueAs_Boolean_ShouldReturnBoolean()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:true"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<bool>(true, argument.GetValueAs<bool>());
        }

        [TestMethod]
        public void GetValueAs_Byte_ShouldReturnByte()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:45"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<byte>(45, argument.GetValueAs<byte>());
        }

        [TestMethod]
        public void GetValueAs_Char_ShouldReturnChar()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:&"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<char>('&', argument.GetValueAs<char>());
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void GetValueAs_DateTime_InvalidValue_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            DateTime expected = new DateTime(2016, 12, 6, 19, 0, 6);

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:2017-76hsh"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<DateTime>(expected, argument.GetValueAs<DateTime>());
        }

        [TestMethod]
        public void GetValueAs_DateTime1_ShouldReturnDateTime()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            DateTime expected = new DateTime(2016, 12, 6, 19, 0, 6);

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:2016-12-06T19:00:06"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<DateTime>(expected, argument.GetValueAs<DateTime>());
        }

        [TestMethod]
        public void GetValueAs_DateTime2_ShouldReturnDateTime()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            DateTime expected = new DateTime(2016, 12, 6);

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:2016-12-06"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<DateTime>(expected, argument.GetValueAs<DateTime>());
        }

        [TestMethod]
        public void GetValueAs_Decimal_ShouldReturnDecimal()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            decimal expected = 4453.8M;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:4453.8"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<decimal>(expected, argument.GetValueAs<decimal>());
        }

        [TestMethod]
        public void GetValueAs_Double_ShouldReturnDouble()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            double expected = 44464453.8;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:44464453.8"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<double>(expected, argument.GetValueAs<double>());
        }

        [TestMethod]
        public void GetValueAs_Int16_ShouldReturnInt16()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            Int16 expected = -444;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:-444"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<short>(expected, argument.GetValueAs<short>());
        }

        [TestMethod]
        public void GetValueAs_Int32_ShouldReturnInt32()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            Int32 expected = -11444223;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:-11444223"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<int>(expected, argument.GetValueAs<int>());
        }

        [TestMethod]
        public void GetValueAs_Int64_ShouldReturnInt64()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            Int64 expected = -11444223998877;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:-11444223998877"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<long>(expected, argument.GetValueAs<long>());
        }

        [TestMethod]
        public void GetValueAs_SByte_ShouldReturnSByte()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            sbyte expected = -118;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:-118"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<sbyte>(expected, argument.GetValueAs<sbyte>());
        }

        [TestMethod]
        public void GetValueAs_Single_ShouldReturnSingle()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            Single expected = -78118.7f;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:-78118.7"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<float>(expected, argument.GetValueAs<Single>());
        }

        [TestMethod]
        [ExpectedException(typeof(CommandLineArgumentException))]
        public void GetValueAs_String_InvalidValue_ShouldThrowCommandLineArgumentException()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:XXXHello"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            try
            {
                Assert.AreEqual<string>("XXXHello", argument.GetValueAs<string>(new TestValidator()));
            }

            catch (CommandLineArgumentException ex)
            {
                Assert.AreEqual<string>("[Juhta.Net.Error10047] Command line argument value 'XXXHello' is invalid according to a validator of the type 'Juhta.Net.Tests.Console.TestValidator'.", ex.Message);

                throw;
            }
        }

        [TestMethod]
        public void GetValueAs_UInt16_ShouldReturnUInt16()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            UInt16 expected = 3444;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:3444"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<ushort>(expected, argument.GetValueAs<ushort>());
        }

        [TestMethod]
        public void GetValueAs_UInt32_ShouldReturnuInt32()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            UInt32 expected = 11444223;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:11444223"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<uint>(expected, argument.GetValueAs<uint>());
        }

        [TestMethod]
        public void GetValueAs_UInt64_ShouldReturnUInt64()
        {
            CommandLineParser commandLineParser = new CommandLineParser();
            CommandLineArgument argument;
            UInt64 expected = 11444223998877;

            commandLineParser.ParseArguments(
                new string[]
                {
                    "/MyOption:11444223998877"
                }
            );

            argument = commandLineParser.GetOptionArgument("MyOption");

            Assert.AreEqual<ulong>(expected, argument.GetValueAs<ulong>());
        }

        #endregion
    }

    internal class TestValidator : IValidator<string>
    {
        #region Public Methods

        public void Validate(string value)
        {
            if (value.StartsWith("XXX"))
                throw new ValidationException("Value cannot start with 'XXX' because otherwise the system will crash. Try to be patient, we are still learning to code.");
        }

        #endregion
    }
}
