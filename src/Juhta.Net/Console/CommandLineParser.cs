
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines a class for parsing command line arguments. The following types of command line arguments are
    /// supported:
    /// <list type="table">
    /// <listheader>
    /// <term>Argument type</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term>Named arguments</term>
    /// <description>Represents a named command line argument</description>
    /// </item>
    /// <item>
    /// <term>Option arguments</term>
    /// <description>Represents a command line option</description>
    /// </item>
    /// <item>
    /// <term>Plain arguments</term>
    /// <description>Represents any raw command line argument</description>
    /// </item>
    /// </list>
    /// <para>A named argument consists of two raw arguments. The first argument determines an argument name and the
    /// second argument an actual argument value. Argument names must be prefixed by an argument name prefix. For
    /// example, <c>-workingFolder C:\Temp</c> is a valid named argument. Character '-' is the default named argument
    /// prefix.</para>
    /// <para>An option argument consists of an option prefix, option name, option name-value separator and an actual
    /// option value. For example, <c>/BufferSize:12345</c> is a valid option argument. Characters '/' and ':' are the
    /// default option prefix and option name-value separator, respectively. If a value part is missing from an option
    /// argument, it’s assumed to be a boolean option with the default value of true. Thus, the option arguments
    /// <c>/SaveLog</c> and <c>/SaveLog:true</c> are equivalent.</para>
    /// <para>A plain argument is any raw argument that doesn’t fall into the two categories above. In other words, all
    /// raw arguments that are not prefixed either by an argument name prefix or an option prefix, are treated as plain
    /// arguments.</para>
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

            if (m_namedArguments == null)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(nameof(GetNamedArgument), this.GetType()));

            else if (m_namedArguments.TryGetValue(argumentName, out namedArgument))
                return((NamedArgument)SetConsumed(namedArgument));

            else if (defaultValue != null)
                return(CreateNamedArgument(argumentName, defaultValue));

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

            if (m_optionArguments == null)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(nameof(GetOptionArgument), this.GetType()));

            else if (m_optionArguments.TryGetValue(optionName, out optionArgument))
                return((OptionArgument)SetConsumed(optionArgument));

            else if (defaultValue != null)
                return(CreateOptionArgument(optionName + m_optionNameValueSeparator + defaultValue));

            else
                throw new CommandLineArgumentException(LibraryMessages.Error091.FormatMessage(optionName));
        }

        /// <summary>
        /// Gets a plain argument.
        /// </summary>
        /// <param name="index">Specifies a plain argument index. The index of the first plain argument is 0.</param>
        /// <returns>Returns an instance of <see cref="PlainArgument"/> holding the specified plain argument.</returns>
        /// <remarks>If the specified plain argument is not found, an exception will be thrown.</remarks>
        public PlainArgument GetPlainArgument(int index)
        {
            return(GetPlainArgument(index, null));
        }

        /// <summary>
        /// Gets a plain argument.
        /// </summary>
        /// <param name="index">Specifies a plain argument index. The index of the first plain argument is 0.</param>
        /// <param name="defaultValue">Specifies a default value for the plain argument. Can be null.</param>
        /// <returns>Returns an instance of <see cref="PlainArgument"/> holding the specified plain argument.</returns>
        /// <remarks>If the specified plain argument is not found and <paramref name="defaultValue"/> is null, an
        /// exception will be thrown.</remarks>
        public PlainArgument GetPlainArgument(int index, string defaultValue)
        {
            if (m_plainArguments == null)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(nameof(GetPlainArgument), this.GetType()));

            else if (0 <= index && index < m_plainArguments.Count)
                return((PlainArgument)SetConsumed(m_plainArguments[index]));

            else if (defaultValue != null)
                return(CreatePlainArgument(defaultValue));

            else
                throw new CommandLineArgumentException(LibraryMessages.Error095.FormatMessage(index));
        }

        /// <summary>
        /// Gets all unconsumed command line arguments.
        /// </summary>
        /// <returns>Returns an array of <see cref="CommandLineArgument"/> objects not yet consumed.</returns>
        public CommandLineArgument[] GetUnconsumedArguments()
        {
            if (m_allArguments == null)
                throw new InvalidOperationException(CommonMessages.Error006.FormatMessage(nameof(GetUnconsumedArguments), this.GetType()));

            return(m_allArguments.Where(x => x.Consumed == false).ToArray());
        }

        /// <summary>
        /// Parses an array of raw command line arguments.
        /// </summary>
        /// <param name="arguments">Specifies an array of raw command line arguments.</param>
        public void ParseArguments(string[] arguments)
        {
            ParseArguments(arguments, c_defaultArgumentNamePrefix, c_defaultOptionPrefix, c_defaultOptionNameValueSeparator);
        }

        /// <summary>
        /// Parses an array of raw command line arguments.
        /// </summary>
        /// <param name="arguments">Specifies an array of raw command line arguments.</param>
        /// <param name="argumentNamePrefix">Specifies an argument name prefix.</param>
        /// <param name="optionPrefix">Specifies an option prefix.</param>
        /// <param name="optionNameValueSeparator">Specifies an option name-value separator.</param>
        public void ParseArguments(string[] arguments, string argumentNamePrefix, string optionPrefix, string optionNameValueSeparator)
        {
            string prefix;
            CommandLineArgument argument;

            // Initialize the argument lists and collections

            m_allArguments = new List<CommandLineArgument>();

            m_namedArguments = new Dictionary<string, NamedArgument>();

            m_optionArguments = new Dictionary<string, OptionArgument>();

            m_plainArguments = new List<PlainArgument>();

            // Initialize the parsing settings

            ArgumentHelper.CheckNotNull(nameof(argumentNamePrefix), argumentNamePrefix);

            m_argumentNamePrefix = argumentNamePrefix;

            ArgumentHelper.CheckNotNull(nameof(optionPrefix), optionPrefix);

            m_optionPrefix = optionPrefix;

            ArgumentHelper.CheckNotNull(nameof(optionNameValueSeparator), optionNameValueSeparator);

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
                prefix = RemovePossiblePrefix(ref arguments[i]);

                if (prefix == m_optionPrefix)
                {
                    // Create an option argument

                    argument = CreateOptionArgument(arguments[i]);

                    m_optionArguments.Add(((OptionArgument)argument).Name, ((OptionArgument)argument));
                }
                else if (prefix == m_argumentNamePrefix)
                {
                    // Create a named argument

                    if (i == arguments.Length - 1)
                        throw new CommandLineArgumentException(LibraryMessages.Error094.GetMessage());

                    argument = CreateNamedArgument(arguments[i], arguments[i + 1]);

                    i++;

                    m_namedArguments.Add(((NamedArgument)argument).Name, ((NamedArgument)argument));
                }
                else
                {
                    // Create a plain argument

                    argument = CreatePlainArgument(arguments[i]);

                    m_plainArguments.Add((PlainArgument)argument);
                }

                m_allArguments.Add(argument);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a boolean value determining whether this <see cref="CommandLineParser"/> instance has unconsumed
        /// arguments.
        /// </summary>
        public bool HasUnconsumedArguments
        {
            get
            {
                if (m_allArguments == null)
                    throw new InvalidOperationException(CommonMessages.Error018.FormatMessage(nameof(HasUnconsumedArguments), this.GetType()));

                return(m_allArguments.Count(x => x.Consumed == false) > 0);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates an instance of <see cref="NamedArgument"/> based on a specified argument name and value.
        /// </summary>
        /// <param name="argumentName">Specifies an argument name.</param>
        /// <param name="argumentValue">Specifies an argument value.</param>
        /// <returns>Returns the created <see cref="NamedArgument"/> instance.</returns>
        private NamedArgument CreateNamedArgument(string argumentName, string argumentValue)
        {
            if (!Regex.IsMatch(argumentName, c_regexArgumentName))
                throw new CommandLineArgumentException(LibraryMessages.Error090.FormatMessage(argumentName, c_regexArgumentName));

            return(new NamedArgument(argumentName, argumentValue));
        }

        /// <summary>
        /// Creates an instance of <see cref="OptionArgument"/> based on a specified string value.
        /// </summary>
        /// <param name="optionArgument">Specifies an option argument as a string value. The value is not assumed to
        /// start with an option prefix.</param>
        /// <returns>Returns the created <see cref="OptionArgument"/> instance.</returns>
        private OptionArgument CreateOptionArgument(string optionArgument)
        {
            string fullOptionArgument, optionName, optionValue;
            int nameValueSeparatorPosition;

            fullOptionArgument = m_optionPrefix + optionArgument;

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
                    throw new CommandLineArgumentException(LibraryMessages.Error085.FormatMessage(fullOptionArgument));

                optionValue = optionArgument.Substring(nameValueSeparatorPosition + m_optionNameValueSeparator.Length);

                if (String.IsNullOrWhiteSpace(optionValue))
                    throw new CommandLineArgumentException(LibraryMessages.Error086.FormatMessage(fullOptionArgument));
            }

            if (!Regex.IsMatch(optionName, c_regexOptionName))
                throw new CommandLineArgumentException(LibraryMessages.Error087.FormatMessage(fullOptionArgument, c_regexOptionName));

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

        /// <summary>
        /// Removes a possible reserved prefix from the beginning of an argument.
        /// </summary>
        /// <param name="argument">Specifies an argument. If the argument starts with a reserved prefix, returns the
        /// argument without the prefix.</param>
        /// <returns>Returns the removed prefix or null, if no prefix was removed.</returns>
        private string RemovePossiblePrefix(ref string argument)
        {
            if (argument.StartsWith(m_argumentNamePrefix))
            {
                argument = argument.Substring(m_argumentNamePrefix.Length);

                if (String.IsNullOrWhiteSpace(argument))
                    throw new CommandLineArgumentException(LibraryMessages.Error089.FormatMessage(m_argumentNamePrefix));

                return(m_argumentNamePrefix);
            }
            else if (argument.StartsWith(m_optionPrefix))
            {
                argument = argument.Substring(m_optionPrefix.Length);

                if (String.IsNullOrWhiteSpace(argument))
                    throw new CommandLineArgumentException(LibraryMessages.Error084.FormatMessage(m_optionPrefix));

                return(m_optionPrefix);
            }
            else
                return(null);
        }

        /// <summary>
        /// Sets a specified <see cref="CommandLineArgument"/> as consumed.
        /// </summary>
        /// <param name="argument">Specifies an instance of <see cref="CommandLineArgument"/>.</param>
        /// <returns>Returns the specified <see cref="CommandLineArgument"/> instance.</returns>
        private static CommandLineArgument SetConsumed(CommandLineArgument argument)
        {
            argument.Consumed = true;

            return(argument);
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
