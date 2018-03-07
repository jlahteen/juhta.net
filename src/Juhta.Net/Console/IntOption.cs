
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines a class that represents a command line integer option. At the command line level, integer options
    /// consist of a prefix, name and an integer value. The name and value parts are separated by a name-value
    /// separator.
    /// </summary>
    public class IntOption : CommandLineArg<int>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name">Specifies a value for the <see cref="CommandLineArg{TValue}.Name"/> property.</param>
        /// <param name="value">Specifies a value for the <see cref="CommandLineArg{TValue}.Value"/> property.</param>
        public IntOption(string name, int value) : base(name, value)
        {}

        #endregion
    }
}
