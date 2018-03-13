
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Text;

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
            ParseArguments(arguments, c_defaultArgumentNamePrefix, c_defaultOptionNamePrefix, c_defaultOptionNameValueSeparator);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void ParseArguments(string[] arguments, string argumentNamePrefix, string optionNamePrefix, string optionNameValueSeparator)
        {
            m_arguments = new List<CommandLineArgument>();
            CommandLineArgument argument;
            NameValueArgument nameValueArgument;

            m_nameValueArguments = new Dictionary<string, NameValueArgument>();

            if (arguments == null || arguments.Length == 0)
                return;

            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i].StartsWith(optionNamePrefix))
                    argument = CreateOptionArgument(arguments, i);

                else if (arguments[i].StartsWith(argumentNamePrefix))
                    argument = CreateNamedArgument(arguments, i);

                else
                    argument = CreateRawArgument(arguments, i);

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
        /// <param name="arguments"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private CommandLineArgument CreateNamedArgument(string[] arguments, int i)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private CommandLineArgument CreateOptionArgument(string[] arguments, int i)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an instance of <see cref="PlainArgument"/>.
        /// </summary>
        /// <param name="arguments">Specifies an array of raw command line arguments.</param>
        /// <param name="i">An index in <paramref name="arguments"/> specifying the raw argument based on which to
        /// create an instance of <see cref="PlainArgument"/>.</param>
        /// <returns>Returns the created <see cref="PlainArgument"/> instance.</returns>
        private CommandLineArgument CreateRawArgument(string[] arguments, int i)
        {
            return(new PlainArgument(arguments[i]));
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the default argument name prefix.
        /// </summary>
        private const string c_defaultArgumentNamePrefix = "-";

        /// <summary>
        /// Specifies the default option name prefix.
        /// </summary>
        private const string c_defaultOptionNamePrefix = "/";

        /// <summary>
        /// Specifies the default name-value separator in option arguments.
        /// </summary>
        private const string c_defaultOptionNameValueSeparator = ":";

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies a list of the parsed command line arguments.
        /// </summary>
        private List<CommandLineArgument> m_arguments;

        /// <summary>
        /// Specifies a list of the parsed name-value command line arguments indexed by name.
        /// </summary>
        private Dictionary<string, NameValueArgument> m_nameValueArguments;

        #endregion
    }
}
