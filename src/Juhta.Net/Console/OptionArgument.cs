
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines a class that represents an option argument. At the command line level, option arguments consist of a
    /// prefix, name and an optional value, which is separated by a name-value separator from the name part. If the
    /// value part is missing, the option argument is assumed to be a boolean option with a 'true' value.
    /// </summary>
    public class OptionArgument : NameValueArgument
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name">Specifies a value for the <see cref="NameValueArgument.Name"/> property.</param>
        /// <param name="value">Specifies a value for the <see cref="CommandLineArgument.Value"/> property.</param>
        public OptionArgument(string name, string value) : base(name, value)
        {}

        #endregion
    }
}
