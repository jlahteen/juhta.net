
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Juhta.Net.Console
{
    /// <summary>
    /// todo
    /// </summary>
    public class CommandLineParser
    {
        #region Public Methods

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="arguments"></param>
        public void ParseArguments(string[] arguments)
        {
            ParseArguments(arguments, c_defaultArgumentNamePrefix, c_defaultOptionPrefix, c_defaultOptionNameValueSeparator);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void ParseArguments(string[] arguments, string argumentNamePrefix, string optionPrefix, string optionNameValueSeparator)
        {
            m_arguments = new List<CommandLineArgument>();
            CommandLineArgument argument;
            NameValueArgument nameValueArgument;

            m_nameValueArguments = new Dictionary<string, NameValueArgument>();

            m_argumentNamePrefix = argumentNamePrefix;

            m_optionPrefix = optionPrefix;

            m_optionNameValueSeparator = optionNameValueSeparator;

            if (arguments == null || arguments.Length == 0)
                return;

            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i].StartsWith(optionPrefix))
                    argument = CreateOptionArgument(arguments[i]);

                else if (arguments[i].StartsWith(argumentNamePrefix))
                {
                    if (i == arguments.Length - 1)
                        throw new CommandLineArgumentException("");

                    argument = CreateNamedArgument(arguments[i], arguments[i + 1]);

                    i++;
                }
                else
                    argument = CreatePlainArgument(arguments[i]);

                m_arguments.Add(argument);

                nameValueArgument = argument as NameValueArgument;

                if (nameValueArgument != null)
                    m_nameValueArguments.Add(nameValueArgument.Name, nameValueArgument);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="nameArgument"></param>
        /// <param name="valueArgument"></param>
        /// <returns></returns>
        private CommandLineArgument CreateNamedArgument(string nameArgument, string valueArgument)
        {
            string nameArgumentOriginal = nameArgument;

            if (!nameArgument.StartsWith(m_argumentNamePrefix))
                throw new CommandLineArgumentException(LibraryMessages.Error088.FormatMessage(nameArgument, m_argumentNamePrefix));

            nameArgument = nameArgument.Substring(m_argumentNamePrefix.Length);

            if (String.IsNullOrWhiteSpace(nameArgument))
                throw new CommandLineArgumentException(LibraryMessages.Error089.FormatMessage(nameArgumentOriginal, m_argumentNamePrefix));

            if (!Regex.IsMatch(nameArgument, c_regexArgumentName))
                throw new CommandLineArgumentException(LibraryMessages.Error090.FormatMessage(nameArgument, c_regexArgumentName));

            return(new NamedArgument(nameArgument, valueArgument));
        }

        /// <summary>
        /// Creates an instance of <see cref="OptionArgument"/> based on a specified string value.
        /// </summary>
        /// <param name="optionArgument">Specifies an option argument as a string value.</param>
        /// <returns>Returns the created <see cref="OptionArgument"/> instance.</returns>
        private CommandLineArgument CreateOptionArgument(string optionArgument)
        {
            string optionArgumentOriginal, optionName, optionValue;
            int nameValueSeparatorPosition;

            optionArgumentOriginal = optionArgument;

            if (!optionArgument.StartsWith(m_optionPrefix))
                throw new CommandLineArgumentException(LibraryMessages.Error083.FormatMessage(optionArgument, m_optionPrefix));

            optionArgument = optionArgument.Substring(m_optionPrefix.Length);

            if (String.IsNullOrWhiteSpace(optionArgument))
                throw new CommandLineArgumentException(LibraryMessages.Error084.FormatMessage(m_optionPrefix));

            nameValueSeparatorPosition = optionArgument.IndexOf(m_optionNameValueSeparator);

            if (nameValueSeparatorPosition < 0)
            {
                optionName = optionArgument;

                optionValue = "true";
            }
            else
            {
                optionName = optionArgument.Substring(0, nameValueSeparatorPosition);

                if (String.IsNullOrWhiteSpace(optionName))
                    throw new CommandLineArgumentException(LibraryMessages.Error085.FormatMessage(optionArgumentOriginal));

                optionValue = optionArgument.Substring(nameValueSeparatorPosition + m_optionNameValueSeparator.Length);

                if (String.IsNullOrWhiteSpace(optionValue))
                    throw new CommandLineArgumentException(LibraryMessages.Error086.FormatMessage(optionArgumentOriginal));
            }

            if (!Regex.IsMatch(optionName, c_regexOptionName))
                throw new CommandLineArgumentException(LibraryMessages.Error087.FormatMessage(optionArgumentOriginal, c_regexOptionName));

            return(new OptionArgument(optionName, optionValue));
        }

        /// <summary>
        /// Creates an instance of <see cref="PlainArgument"/>.
        /// </summary>
        /// <param name="argument">Specifies a raw command line argument based on which to create an instance of
        /// <see cref="PlainArgument"/>.</param>
        /// <returns>Returns the created <see cref="PlainArgument"/> instance.</returns>
        private CommandLineArgument CreatePlainArgument(string argument)
        {
            return(new PlainArgument(argument));
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the default argument name prefix.
        /// </summary>
        private const string c_defaultArgumentNamePrefix = "-";

        /// <summary>
        /// Specifies the default option name-value separator.
        /// </summary>
        private const string c_defaultOptionNameValueSeparator = ":";

        /// <summary>
        /// Specifies the default option prefix.
        /// </summary>
        private const string c_defaultOptionPrefix = "/";

        /// <summary>
        /// Specifies the regex for validating argument names.
        /// </summary>
        private const string c_regexArgumentName = "^[a-zA-Z0-9]+([_-][a-zA-Z0-9]+)*$";

        /// <summary>
        /// Specifies the regex for validating option names.
        /// </summary>
        private const string c_regexOptionName = "^[a-zA-Z0-9]+([_-][a-zA-Z0-9]+)*$";

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the argument name prefix.
        /// </summary>
        private string m_argumentNamePrefix;

        /// <summary>
        /// Specifies a list of the parsed command line arguments.
        /// </summary>
        private List<CommandLineArgument> m_arguments;

        /// <summary>
        /// Specifies a list of the parsed name-value command line arguments indexed by name.
        /// </summary>
        private Dictionary<string, NameValueArgument> m_nameValueArguments;

        /// <summary>
        /// Specifies the option name-value separator.
        /// </summary>
        private string m_optionNameValueSeparator;

        /// <summary>
        /// Specifies the option prefix.
        /// </summary>
        private string m_optionPrefix;

        #endregion
    }
}
