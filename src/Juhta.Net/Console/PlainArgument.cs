
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Console
{
    /// <summary>
    /// Defines a class that represents a plain argument. At the command line level, plain arguments are handled as
    /// individual arguments without any parsing. An argument is assumed to be a plain argument if it doesn’t begin
    /// with a reserved prefix and it’s not a successor of an argument name argument.
    /// </summary>
    public class PlainArgument : CommandLineArgument
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="value">Specifies a value for the <see cref="CommandLineArgument.Value"/> property.</param>
        public PlainArgument(string value) : base(value)
        {}

        #endregion
    }
}
