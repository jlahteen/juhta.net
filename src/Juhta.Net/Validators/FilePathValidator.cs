﻿
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;

namespace Juhta.Net.Validators
{
    /// <summary>
    /// Defines a validator class for validating file paths.
    /// </summary>
    public class FilePathValidator : PathValidator, IStringValidator
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public FilePathValidator() : base(LibraryMessages.Error038)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorMessage">Specifies an error message to associate with the instance.</param>
        public FilePathValidator(ErrorMessage errorMessage) : base(errorMessage)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IValidator{T}.Validate"/>.
        /// </summary>
        public void Validate(string value)
        {
            if (!IsValidPath(value, PathType.FilePath))
                throw new ValidationException(m_errorMessage.FormatMessage(value));
        }

        #endregion
    }
}