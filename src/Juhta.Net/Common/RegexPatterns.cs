
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Common
{
    /// <summary>
    /// A static class that defines regular expression patterns.
    /// </summary>
    public static class RegexPatterns
    {
        #region Public Constants

        /// <summary>
        /// Specifies a regular expression pattern for email addresses.
        /// </summary>
        public const string EmailAddress = @"^[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,4}$";

        /// <summary>
        /// Specifies a regular expression pattern for email address lists.
        /// </summary>
        public const string EmailAddressList = @"^[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,4}(; ?[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,4})*;?$";

        /// <summary>
        /// Specifies a regular expression pattern for full class names.
        /// </summary>
        public const string FullClassName = @"^[a-zA-Z0-9_]+\.[a-zA-Z0-9_]+(\.[a-zA-Z0-9_]+)*$";

        #endregion
    }
}
