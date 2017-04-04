
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using Juhta.Net.Extensions;
using Juhta.Net.Validation;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines an abstract base class for command line arguments.
    /// </summary>
    public abstract class CommandLineArg
    {
        #region Public Methods

        /// <summary>
        /// Validates the value of this CommandLineArg object through a specified IStringValidator instance.
        /// </summary>
        /// <param name="validator">Specifies an IStringValidator instance.</param>
        public void ValidateValue(IStringValidator validator)
        {
            if (m_value == null)
                throw new CommandLineArgException(LibraryMessages.Error044.FormatMessage(m_rawArg));

            try
            {
                validator.Validate(m_value);
            }

            catch (ValidationException ex)
            {
                if (m_value == m_rawArg)
                    throw new CommandLineArgException(LibraryMessages.Error045.FormatMessage(m_value), ex);
                else
                    throw new CommandLineArgException(LibraryMessages.Error047.FormatMessage(m_value), ex);
            }
        }

        /// <summary>
        /// Validates the value of this CommandLineArg object with a specified regular expression.
        /// </summary>
        /// <param name="regex">Specifies a regular expression.</param>
        public void ValidateValue(string regex)
        {
            if (m_value == null)
                throw new CommandLineArgException(LibraryMessages.Error044.FormatMessage(m_rawArg));

            if (!m_value.IsMatch(regex))
                if (m_value == m_rawArg)
                    throw new CommandLineArgException(LibraryMessages.Error046.FormatMessage(m_value, regex));
                else
                    throw new CommandLineArgException(LibraryMessages.Error048.FormatMessage(m_value, regex));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the raw command line argument specified by this CommandLineArg object.
        /// </summary>
        public string RawArg
        {
            get {return(m_rawArg);}
        }

        /// <summary>
        /// Gets the (possibly parsed) value of the command line argument specified by this CommandLineArg object.
        /// </summary>
        public string Value
        {
            get {return(m_value);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="rawArg">Specifies a value for the <see cref="RawArg"/> property.</param>
        /// <param name="value">Specifies a value for the <see cref="Value"/> property.</param>
        protected CommandLineArg(string rawArg, string value)
        {
            m_rawArg = rawArg;

            m_value = value;
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Stores the <see cref="RawArg"/> property.
        /// </summary>
        protected string m_rawArg;

        /// <summary>
        /// Stores the <see cref="Value"/> property.
        /// </summary>
        protected string m_value;

        #endregion
    }
}
