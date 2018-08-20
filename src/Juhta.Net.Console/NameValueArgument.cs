
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines an abstract base class for command line arguments that consist of name and value pairs.
    /// </summary>
    public abstract class NameValueArgument : CommandLineArgument
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the command line argument.
        /// </summary>
        public string Name
        {
            get {return(m_name);}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name">Specifies a value for the <see cref="Name"/> property.</param>
        /// <param name="value">Specifies a value for the <see cref="CommandLineArgument.Value"/> property.</param>
        protected NameValueArgument(string name, string value) : base(value)
        {
            m_name = name;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        #endregion
    }
}
