
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Juhta.Net.Validation
{
    /// <summary>
    /// Defines a validator class that can validate string values based on a regular expression.
    /// </summary>
    public class RegexValidator : IStringValidator
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pattern">Specifies a regular expression pattern.</param>
        public RegexValidator(string pattern) :
            this(new string[]{pattern}, RegexOptions.None, PatternMatchMode.And, LibraryMessages.Error053)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pattern">Specifies a regular expression pattern.</param>
        /// <param name="errorMessage">Specifies an error message to use in validation errors.</param>
        public RegexValidator(string pattern, ErrorMessage errorMessage) :
            this(new string[]{pattern}, RegexOptions.None, PatternMatchMode.And, errorMessage)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pattern">Specifies a regular expression pattern.</param>
        /// <param name="options">Specifies a bitwise combination of regular expression options.</param>
        public RegexValidator(string pattern, RegexOptions options) :
            this(new string[]{pattern}, options, PatternMatchMode.And, LibraryMessages.Error053)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pattern">Specifies a regular expression pattern.</param>
        /// <param name="options">Specifies a bitwise combination of regular expression options.</param>
        /// <param name="errorMessage">Specifies an error message to use in validation errors.</param>
        public RegexValidator(string pattern, RegexOptions options, ErrorMessage errorMessage) :
            this(new string[]{pattern}, options, PatternMatchMode.And, errorMessage)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="Validation.IValidator&lt;T&gt;.Validate"/>.
        /// </summary>
        public virtual void Validate(string value)
        {
            int matchCount = 0;
            bool success;

            foreach (string pattern in m_patterns)
                if (Regex.IsMatch(value, pattern, m_options))
                {
                    matchCount++;

                    if (m_patternMatchMode == PatternMatchMode.Or)
                        return;

                    else if (m_patternMatchMode == PatternMatchMode.Xor && matchCount > 1)
                        throw new ValidationException(m_errorMessage.FormatMessage(value));
                }
                else if (m_patternMatchMode == PatternMatchMode.And)
                    throw new ValidationException(m_errorMessage.FormatMessage(value));

            switch (m_patternMatchMode)
            {
                case PatternMatchMode.And:
                    success = matchCount == m_patterns.Count;

                    break;

                case PatternMatchMode.Or:
                    success = matchCount > 0;

                    break;

                case PatternMatchMode.Xor:
                    success = matchCount == 1;

                    break;

                default:
                    throw new UnimplementedCodeBranchException(m_patternMatchMode);
            }

            if (!success)
                throw new ValidationException(m_errorMessage.FormatMessage(value));
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="patterns">Specifies an array of regular expression patterns.</param>
        /// <param name="options">Specifies a bitwise combination of regular expression options.</param>
        /// <param name="patternMatchMode">Specifies a pattern match mode determining how many of the specified regular
        /// expression patterns must match for a successful validation.</param>
        /// <param name="errorMessage">Specifies an error message to use in validation errors. The message can contain
        /// a placeholder for a value to validate.</param>
        protected RegexValidator(string[] patterns, RegexOptions options, PatternMatchMode patternMatchMode, ErrorMessage errorMessage)
        {
            if (patterns == null)
                throw new ArgumentNullException(CommonMessages.Error001.FormatMessage("patterns"));

            m_patterns = new List<string>();

            foreach (string pattern in patterns)
            {
                if (pattern == null)
                    throw new ArgumentException(LibraryMessages.Error051.GetMessage());

                m_patterns.Add(pattern);
            }

            m_options = options;

            m_patternMatchMode = patternMatchMode;

            m_errorMessage = errorMessage;
        }

        #endregion

        #region Protected Types

        /// <summary>
        /// Defines an enumeration for the pattern match modes.
        /// </summary>
        protected enum PatternMatchMode
        {
            /// <summary>
            /// All associated regular expressions must match for a succesful validation.
            /// </summary>
            And,

            /// <summary>
            /// At least one associated regular expression must match for a succesful validation.
            /// </summary>
            Or,

            /// <summary>
            /// Exactly one associated regular expression must match for a succesful validation.
            /// </summary>
            Xor
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the error message to use in validation errors. The message can contain a placeholder for a value
        /// to validate. Can be null.
        /// </summary>
        private ErrorMessage m_errorMessage;

        /// <summary>
        /// Specifies a combination of the regular expression options to control validation operations.
        /// </summary>
        private RegexOptions m_options;

        /// <summary>
        /// Specifies the pattern match mode determining how many of the associated regular expression patterns must
        /// match for a successful validation.
        /// </summary>
        private PatternMatchMode m_patternMatchMode;

        /// <summary>
        /// Specifies a list of the regular expression patterns associated with this RegexValidator instance.
        /// </summary>
        private List<string> m_patterns;

        #endregion
    }
}
