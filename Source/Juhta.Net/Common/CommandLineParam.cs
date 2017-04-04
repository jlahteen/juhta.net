
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a class that represents a command line parameter. The values of the properties
    /// <see cref="CommandLineArg.Value"/> and <see cref="CommandLineArg.RawArg"/> are always the same with command
    /// line parameters.
    /// </summary>
    public class CommandLineParam : CommandLineArg
    {
        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="value">Specifies a value for the <see cref="CommandLineArg.Value"/> property as well as to the
        /// <see cref="CommandLineArg.RawArg"/> property.</param>
        internal CommandLineParam(string value) : base(value, value)
        {}

        #endregion
    }
}
