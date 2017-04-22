
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a static class containing certain regular expression patterns for validating string data.
    /// </summary>
    public static class RegularExpressions
    {
        #region Public Constants

        /// <summary>
        /// Specifies a regular expression for validating assembly class file URIs.
        /// </summary>
        public const string AssemblyClassFileUri = @"^((file://)?(([a-zA-Z]:\\)|(\\\\(([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})|(([a-zA-Z][a-zA-Z0-9_]*\.)*[a-zA-Z][a-zA-Z0-9_]*))\\[^\\/:\*\?\|<>]+\\)))?([^\\/:\*\?\|<>]+\\)*([^\\/:\*\?\|<>]+)\.dll#[a-zA-Z0-9_]*(\.[a-zA-Z0-9_]+)+$";

        /// <summary>
        /// Specifies a regular expression for validating absolute or relative directory paths.
        /// </summary>
        public const string DirectoryPath = @"^(?!^ *$)(([a-zA-Z]:\\)|(\\\\(([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})|(([a-zA-Z][a-zA-Z0-9_]*\.)*[a-zA-Z][a-zA-Z0-9_]*))\\[^\\/:\*\?\|<>]+\\))?([^\\/:\*\?\|<>]+\\)*([^\\/:\*\?\|<>]+(\\)?)?$";

        /// <summary>
        /// Specifies a regular expression for validating email addresses.
        /// </summary>
        public const string EmailAddress = @"^[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,4}$";

        /// <summary>
        /// Specifies a regular expression for validating email address lists.
        /// </summary>
        public const string EmailAddressList = @"^[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,4}(; ?[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([_\.-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,4})*;?$";

        /// <summary>
        /// Specifies a regular expression for validating absolute or relative file paths.
        /// </summary>
        public const string FilePath = @"^(([a-zA-Z]:\\)|(\\\\(([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})|(([a-zA-Z][a-zA-Z0-9_]*\.)*[a-zA-Z][a-zA-Z0-9_]*))\\[^\\/:\*\?\|<>]+\\))?([^\\/:\*\?\|<>]+\\)*([^\\/:\*\?\|<>]+)$";

        #endregion
    }
}
