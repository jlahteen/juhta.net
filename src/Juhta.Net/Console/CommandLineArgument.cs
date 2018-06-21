
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Validators;
using System;
using System.Globalization;

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines an abstract base class for command line arguments.
    /// </summary>
    public abstract class CommandLineArgument
    {
        #region Public Methods

        /// <summary>
        /// Converts the value of this command line argument to a specified type.
        /// </summary>
        /// <typeparam name="T">Specifies a type to which to convert the value.</typeparam>
        /// <returns>Returns the value of this command line argument converted to the specified type.</returns>
        public T GetValueAs<T>()
        {
            try
            {
                return((T)Convert.ChangeType(m_value, typeof(T), CultureInfo.InvariantCulture));
            }

            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                throw new CommandLineArgumentException(LibraryMessages.Error045.FormatMessage(m_value, typeof(T)), ex);
            }
        }

        /// <summary>
        /// Converts the value of this command line argument to a specified type and validates the converted value with
        /// a specified validator.
        /// </summary>
        /// <typeparam name="T">Specifies a type to which to convert the value.</typeparam>
        /// <param name="validator">Specifies a validator.</param>
        /// <returns>Returns the value of this command line argument converted to the specified type.</returns>
        public T GetValueAs<T>(IValidator<T> validator)
        {
            T value;

            value = GetValueAs<T>();

            try
            {
                validator.Validate(value);
            }

            catch (ValidationException ex)
            {
                throw new CommandLineArgumentException(LibraryMessages.Error047.FormatMessage(m_value, validator.GetType()), ex);
            }

            return(value);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets (internal) a boolean value determining whether this <see cref="CommandLineArgument"/> has been
        /// consumed.
        /// </summary>
        /// <remarks>A <see cref="CommandLineArgument"/> becomes consumed when it is fetched from a
        /// <see cref="CommandLineParser"/>.</remarks>
        public bool Consumed
        {
            get {return(m_consumed);}

            internal set {m_consumed = value;}
        }

        /// <summary>
        /// Gets the value of the command line argument.
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
        /// <param name="value">Specifies a value for the <see cref="Value"/> property.</param>
        protected CommandLineArgument(string value)
        {
            m_value = value;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Consumed"/> property.
        /// </summary>
        private bool m_consumed;

        /// <summary>
        /// Stores the <see cref="Value"/> property.
        /// </summary>
        private string m_value;

        #endregion
    }
}
