
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
        /// Gets a named argument.
        /// </summary>
        /// <param name="argumentName">Specifies an argument name.</param>
        /// <returns>Returns an instance of <see cref="NamedArgument"/> holding the specified named argument.</returns>
        /// <remarks>If the specified named argument is not found, an exception will be thrown.</remarks>
        public NamedArgument GetNamedArgument(string argumentName)
        {
            return(GetNamedArgument(argumentName, null));
        }

        /// <summary>
        /// Gets a named argument.
        /// </summary>
        /// <param name="argumentName">Specifies an argument name.</param>
        /// <param name="defaultValue">Specifies a default value for the named argument. Can be null.</param>
        /// <returns>Returns an instance of <see cref="NamedArgument"/> holding the specified named argument.</returns>
        /// <remarks>If the specified named argument is not found and <paramref name="defaultValue"/> is null, an
        /// exception will be thrown.</remarks>
        public NamedArgument GetNamedArgument(string argumentName, string defaultValue)
        {
            NamedArgument namedArgument;

            if (m_namedArguments.TryGetValue(argumentName, out namedArgument))
                return(namedArgument);

            else if (defaultValue != null)
                return(CreateNamedArgument(m_argumentNamePrefix + argumentName, defaultValue));

            else
                throw new CommandLineArgumentException(LibraryMessages.Error093.FormatMessage(argumentName));
        }

        /// <summary>
        /// Gets an option argument.
        /// </summary>
        /// <param name="optionName">Specifies an option name.</param>
        /// <returns>Returns an instance of <see cref="OptionArgument"/> holding the specified option argument.</returns>
        /// <remarks>If the specified option is not found, an exception will be thrown.</remarks>
        public OptionArgument GetOptionArgument(string optionName)
        {
            return(GetOptionArgument(optionName, null));
        }

        /// <summary>
        /// Gets an option argument.
        /// </summary>
        /// <param name="optionName">Specifies an option name.</param>
        /// <param name="defaultValue">Specifies a default value for the option argument. Can be null.</param>
        /// <returns>Returns an instance of <see cref="OptionArgument"/> holding the specified option argument.</returns>
        /// <remarks>If the specified option is not found and <paramref name="defaultValue"/> is null, an exception
        /// will be thrown.</remarks>
        public OptionArgument GetOptionArgument(string optionName, string defaultValue)
        {
            OptionArgument optionArgument;

            if (m_optionArguments.TryGetValue(optionName, out optionArgument))
                return(optionArgument);

            else if (defaultValue != null)
                return(CreateOptionArgument(m_optionPrefix + optionName + m_optionNameValueSeparator + defaultValue));

            else
                throw new CommandLineArgumentException(LibraryMessages.Error091.FormatMessage(optionName));
        }

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
            CommandLineArgument argument;

            // Initialize the argument lists

            m_allArguments = new List<CommandLineArgument>();

            m_optionArguments = new Dictionary<string, OptionArgument>();

            m_namedArguments = new Dictionary<string, NamedArgument>();

            m_plainArguments = new List<PlainArgument>();

            // Initialize the parsing settings

            m_argumentNamePrefix = argumentNamePrefix;

            m_optionPrefix = optionPrefix;

            m_optionNameValueSeparator = optionNameValueSeparator;

            // Return if there is nothing to parse
            if (arguments == null || arguments.Length == 0)
                return;

            // Check null and empty arguments

            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] != null)
                    arguments[i] = arguments[i].Trim();

                if (String.IsNullOrEmpty(arguments[i]))
                    throw new CommandLineArgumentException(LibraryMessages.Error092.GetMessage());
            }

            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i].StartsWith(optionPrefix))
                {
                    // Parse an option argument

                    argument = CreateOptionArgument(arguments[i]);

                    m_optionArguments.Add(((OptionArgument)argument).Name, ((OptionArgument)argument));
                }
                else if (arguments[i].StartsWith(argumentNamePrefix))
                {
                    // Parse a named argument

                    if (i == arguments.Length - 1)
                        throw new CommandLineArgumentException(LibraryMessages.Error094.GetMessage());

                    argument = CreateNamedArgument(arguments[i], arguments[i + 1]);

                    i++;

                    m_namedArguments.Add(((NamedArgument)argument).Name, ((NamedArgument)argument));
                }
                else
                {
                    // Parse a plain argument

                    argument = CreatePlainArgument(arguments[i]);

                    m_plainArguments.Add((PlainArgument)argument);
                }

                m_allArguments.Add(argument);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates an instance of <see cref="NamedArgument"/> based on a specified argument name and value.
        /// </summary>
        /// <param name="argumentName">Specifies an argument name. The name must begin with an argument name prefix.</param>
        /// <param name="argumentValue">Specifies an argument value.</param>
        /// <returns>Returns the created <see cref="NamedArgument"/> instance.</returns>
        private NamedArgument CreateNamedArgument(string argumentName, string argumentValue)
        {
            if (!argumentName.StartsWith(m_argumentNamePrefix))
                throw new CommandLineArgumentException(LibraryMessages.Error088.FormatMessage(argumentName, m_argumentNamePrefix));

            argumentName = argumentName.Substring(m_argumentNamePrefix.Length);

            if (String.IsNullOrWhiteSpace(argumentName))
                throw new CommandLineArgumentException(LibraryMessages.Error089.FormatMessage(m_argumentNamePrefix));

            if (!Regex.IsMatch(argumentName, c_regexArgumentName))
                throw new CommandLineArgumentException(LibraryMessages.Error090.FormatMessage(argumentName, c_regexArgumentName));

            return(new NamedArgument(argumentName, argumentValue));
        }

        /// <summary>
        /// Creates an instance of <see cref="OptionArgument"/> based on a specified string value.
        /// </summary>
        /// <param name="optionArgument">Specifies an option argument as a string value.</param>
        /// <returns>Returns the created <see cref="OptionArgument"/> instance.</returns>
        private OptionArgument CreateOptionArgument(string optionArgument)
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
        private PlainArgument CreatePlainArgument(string argument)
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
        /// Specifies a list of all parsed command line arguments.
        /// </summary>
        private List<CommandLineArgument> m_allArguments;

        /// <summary>
        /// Specifies the argument name prefix.
        /// </summary>
        private string m_argumentNamePrefix;

        /// <summary>
        /// Specifies a list of the parsed named arguments indexed by name.
        /// </summary>
        private Dictionary<string, NamedArgument> m_namedArguments;

        /// <summary>
        /// Specifies a list of the parsed option arguments indexed by name.
        /// </summary>
        private Dictionary<string, OptionArgument> m_optionArguments;

        /// <summary>
        /// Specifies the option name-value separator.
        /// </summary>
        private string m_optionNameValueSeparator;

        /// <summary>
        /// Specifies the option prefix.
        /// </summary>
        private string m_optionPrefix;

        /// <summary>
        /// Specifies a list of the parsed plain arguments.
        /// </summary>
        private List<PlainArgument> m_plainArguments;

        #endregion
    }
}
