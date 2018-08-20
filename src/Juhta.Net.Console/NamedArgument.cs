
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines a class that represents a named argument. At the command line level, named arguments consist of two
    /// separate raw arguments. The first argument specifies the name for the argument and the second argument the
    /// actual argument value. The first argument must begin with a named argument prefix.
    /// </summary>
    public class NamedArgument : NameValueArgument
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name">Specifies a value for the <see cref="NameValueArgument.Name"/> property.</param>
        /// <param name="value">Specifies a value for the <see cref="CommandLineArgument.Value"/> property.</param>
        public NamedArgument(string name, string value) : base(name, value)
        {}

        #endregion
    }
}
