
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines an abstract base class for command line arguments.
    /// </summary>
    public abstract class CommandLineArgument
    {
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
