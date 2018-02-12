
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines a class for parsing command line arguments.
    /// </summary>
    public class CommandLineArgsParser
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="args">Specifies an array of command line arguments. Can be null.</param>
        /// <param name="optionPrefix">Specifies a prefix for command line options. Can be null in which case all
        /// arguments are expected to be parameters.</param>
        /// <param name="optionNameValueSeparator">Specifies a separator for name and value parts in command line
        /// options. Can be null in which case options are not expected to contain any value parts.</param>
        public CommandLineArgsParser(string[] args, char? optionPrefix, char? optionNameValueSeparator)
        {
            m_commandLineArgs = new LinkedList<CommandLineArg>();

            if (optionPrefix != null)
            {
                // Validate and initialize the option prefix

                if (!optionPrefix.Value.ToString().IsMatch(c_optionPrefixPattern))
                    throw new ArgumentException(LibraryMessages.Error037.FormatMessage(optionPrefix, c_optionPrefixPattern));

                m_optionPrefix = optionPrefix;
            }

            if (optionNameValueSeparator != null)
            {
                // Validate and initialize the option name-value separator

                if (!optionNameValueSeparator.Value.ToString().IsMatch(c_optionNameValueSeparatorPattern))
                    throw new ArgumentException(LibraryMessages.Error038.FormatMessage(optionNameValueSeparator, c_optionNameValueSeparatorPattern));

                m_optionNameValueSeparator = optionNameValueSeparator;
            }

            if (args == null)
                // There are no command line arguments given
                return;

            else
                // There are command line arguments given

                foreach (string arg in args)
                    if (m_optionPrefix == null)
                        // No command line options are expected
                        m_commandLineArgs.AddLast(new CommandLineParam(arg));

                    else if (arg[0] == m_optionPrefix.Value)
                        // The command line argument is an option
                        m_commandLineArgs.AddLast(CreateCommandLineOption(arg));

                    else
                        // The command line argument is a parameter
                        m_commandLineArgs.AddLast(new CommandLineParam(arg));

            // Store the number of the command line arguments
            m_originalArgCount = m_commandLineArgs.Count;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets and removes a mutually exclusive option from the command line arguments.
        /// </summary>
        /// <param name="optionNames">Specifies an array of option names that are treated as mutually exclusive.</param>
        /// <returns>Returns the option from the specified mutually exclusive options that is found among the command
        /// line arguments. If no option or more than one options are found, an exception will be thrown.</returns>
        public CommandLineOption GetExclusiveOption(string[] optionNames)
        {
            return(GetExclusiveOption(optionNames, null));
        }

        /// <summary>
        /// Gets and removes a mutually exclusive option from the command line arguments.
        /// </summary>
        /// <param name="optionNames">Specifies an array of option names that are treated as mutually exclusive.</param>
        /// <param name="defaultOption">Specifies a default option. Can be null.</param>
        /// <returns>Returns the option from the specified mutually exclusive options that is found among the command
        /// line arguments. If more than one options are found, an exception will be thrown; in case of no option is
        /// found, returns the default option if such is given, or throws an exception.</returns>
        public CommandLineOption GetExclusiveOption(string[] optionNames, string defaultOption)
        {
            CommandLineOption option;

            var optionQuery = m_commandLineArgs.Where(arg =>
                arg is CommandLineOption &&
                optionNames.Contains(((CommandLineOption)arg).Name, StringComparer.OrdinalIgnoreCase));

            if (optionQuery.Count() > 1)
                throw new CommandLineArgException(LibraryMessages.Error035.FormatMessage(String.Join(", ", optionNames)));

            else if (optionQuery.Count() == 1)
            {
                option = (CommandLineOption)optionQuery.First();

                m_commandLineArgs.Remove(option);

                return(option);
            }
            else if (defaultOption != null)
            {
                option = CreateCommandLineOption(defaultOption);

                if (!optionNames.Contains(option.Name, StringComparer.OrdinalIgnoreCase))
                    throw new CommandLineArgException(LibraryMessages.Error040.FormatMessage(String.Join(", ", optionNames)));

                return(option);
            }
            else
                throw new CommandLineArgException(LibraryMessages.Error036.FormatMessage(String.Join(", ", optionNames)));
        }

        /// <summary>
        /// Gets and removes the next parameter from the command line arguments.
        /// </summary>
        /// <returns>Returns the next parameter from the command line arguments. If there are no parameters to be
        /// consumed, an exception will be thrown.</returns>
        public CommandLineParam GetNextParam()
        {
            var param = m_commandLineArgs.FirstOrDefault(arg => arg is CommandLineParam);

            if (param == null)
                throw new CommandLineArgException(LibraryMessages.Error050.GetMessage());

            m_commandLineArgs.Remove(param);

            return((CommandLineParam)param);
        }

        /// <summary>
        /// Gets and removes an option from the command line arguments.
        /// </summary>
        /// <param name="optionName">Specifies an option name.</param>
        /// <returns>Returns the specified option from the command line arguments. If the option is specified more than
        /// once, an exception will be thrown as well as when the option is not found.</returns>
        public CommandLineOption GetOption(string optionName)
        {
            return(GetOption(optionName, null, true));
        }

        /// <summary>
        /// Gets and removes an option from the command line arguments.
        /// </summary>
        /// <param name="optionName">Specifies an option name.</param>
        /// <param name="defaultOption">Specifies a default option. Can be null.</param>
        /// <returns>Returns the specified option from the command line arguments. If the option is specified more than
        /// once, an exception will be thrown as well as when the option is not found and no default option is given.</returns>
        public CommandLineOption GetOption(string optionName, string defaultOption)
        {
            return(GetOption(optionName, defaultOption, true));
        }

        /// <summary>
        /// Tries to get and remove an option from the command line arguments.
        /// </summary>
        /// <param name="optionName">Specifies an option name.</param>
        /// <param name="option">If the function returns true, this parameter returns the specified option from the
        /// command line arguments, otherwise the return value is null.</param>
        /// <returns>The function returns true if the specified option was found in the command line arguments,
        /// otherwise the return value is false.</returns>
        public bool TryGetOption(string optionName, out CommandLineOption option)
        {
            option = GetOption(optionName, null, false);

            return(option != null);
        }

        /// <summary>
        /// Verifies that all command line arguments have been consumed. If not, an exception will be thrown.
        /// </summary>
        public void VerifyArgsConsumed()
        {
            if (m_commandLineArgs.Count > 0)
                throw new CommandLineArgException(LibraryMessages.Error049.FormatMessage(m_commandLineArgs.First.Value.RawArg));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of command line arguments left to be consumed.
        /// </summary>
        public int CurrentArgCount
        {
            get {return(m_commandLineArgs.Count);}
        }

        /// <summary>
        /// Gets the original number of the command line arguments.
        /// </summary>
        public int OriginalArgCount
        {
            get {return(m_originalArgCount);}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a CommandLineOption object based on a command line option string.
        /// </summary>
        /// <param name="option">Specifies a command line option string.</param>
        /// <returns>Returns the CommandLineOption object created based on the specified command line option string.</returns>
        private CommandLineOption CreateCommandLineOption(string option)
        {
            string optionWithoutPrefix, optionName, optionValue;

            // Remove the prefix from the option
            optionWithoutPrefix = option.Substring(1);

            if (m_optionNameValueSeparator == null)
                // The option is not expected to contain a value part
                return(new CommandLineOption(option, optionWithoutPrefix, null));

            else if (optionWithoutPrefix.IndexOf(m_optionNameValueSeparator.Value) < 0)
                // The option has no value part
                return(new CommandLineOption(option, optionWithoutPrefix, null));

            else
            {
                // The option has a value part

                // Parse the option name
                optionName = optionWithoutPrefix.SubstringBefore(m_optionNameValueSeparator.Value, StringComparison.Ordinal);

                // Check that the option name is not empty
                if (String.IsNullOrEmpty(optionName))
                    throw new CommandLineArgException(LibraryMessages.Error033.FormatMessage(option));

                // Validate the option name
                if (!optionName.IsMatch(c_optionNamePattern))
                    throw new CommandLineArgException(LibraryMessages.Error039.FormatMessage(optionName, c_optionNamePattern));

                // Parse the option value
                optionValue = optionWithoutPrefix.SubstringAfter(m_optionNameValueSeparator.Value, StringComparison.Ordinal);

                // Check that the option value is not empty
                if (String.IsNullOrEmpty(optionValue))
                    throw new CommandLineArgException(LibraryMessages.Error034.FormatMessage(option));

                return(new CommandLineOption(option, optionName, optionValue));
            }
        }

        /// <summary>
        /// Gets and removes an option from the command line arguments.
        /// </summary>
        /// <param name="optionName">Specifies an option name.</param>
        /// <param name="defaultOption">Specifies a default option. Can be null.</param>
        /// <param name="throwExceptionIfNotFound">Specifies whether an exception will be thrown if the option is not
        /// found and no default option is given.</param>
        /// <returns>Returns the specified option from the command line arguments. If the option is specified more than
        /// once, an exception will be thrown. If the option is not found and no default option is given, the function
        /// either throws an exception or returns null depending on the boolean value of
        /// <paramref name="throwExceptionIfNotFound"/>.</returns>
        private CommandLineOption GetOption(string optionName, string defaultOption, bool throwExceptionIfNotFound)
        {
            CommandLineOption option;

            var optionQuery = m_commandLineArgs.Where(arg =>
                arg is CommandLineOption &&
                String.Compare(((CommandLineOption)arg).Name, optionName, true) == 0);

            if (optionQuery.Count() > 1)
                throw new CommandLineArgException(LibraryMessages.Error042.FormatMessage(optionName));

            else if (optionQuery.Count() == 1)
            {
                option = (CommandLineOption)optionQuery.First();

                m_commandLineArgs.Remove(option);

                return(option);
            }
            else if (defaultOption != null)
            {
                option = CreateCommandLineOption(defaultOption);

                if (String.Compare(option.Name, optionName, true) != 0)
                    throw new CommandLineArgException(LibraryMessages.Error041.FormatMessage(optionName));

                return(option);
            }
            else if (throwExceptionIfNotFound)
                throw new CommandLineArgException(LibraryMessages.Error043.FormatMessage(optionName));

            else
                return(null);
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the regex pattern for validating option names.
        /// </summary>
        private const string c_optionNamePattern = "[A-Za-z0-9_]+";

        /// <summary>
        /// Specifies the regex pattern for validating name and value part separators in command line options.
        /// </summary>
        private const string c_optionNameValueSeparatorPattern = "[:?=]";

        /// <summary>
        /// Specifies the regex pattern for validating option prefixes.
        /// </summary>
        private const string c_optionPrefixPattern = "[!#&*./~^-]";

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies a linked list of the command line arguments.
        /// </summary>
        private LinkedList<CommandLineArg> m_commandLineArgs;

        /// <summary>
        /// Specifies the separator for name and value parts in command line options.
        /// </summary>
        private char? m_optionNameValueSeparator;

        /// <summary>
        /// Specifies the prefix for command line options.
        /// </summary>
        private char? m_optionPrefix;

        /// <summary>
        /// Stores the <see cref="OriginalArgCount"/> property.
        /// </summary>
        private int m_originalArgCount;

        #endregion
    }
}
