
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Diagnostics;
using Juhta.Net.Helpers;

namespace Juhta.Net.Validation
{
    /// <summary>
    /// Defines a validator class for validating directory paths.
    /// </summary>
    public class DirectoryPathValidator : PathValidator, IStringValidator
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DirectoryPathValidator() : base(LibraryMessages.Error003)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorMessage">Specifies an error message to associate with the instance.</param>
        public DirectoryPathValidator(ErrorMessage errorMessage) : base(errorMessage)
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IValidator{T}.Validate"/>.
        /// </summary>
        public void Validate(string value)
        {
            ArgumentHelper.CheckNull(nameof(value), value);

            if (!IsValidPath(value, PathType.DirectoryPath))
                throw new ValidationException(m_errorMessage.FormatMessage(value));
        }

        #endregion
    }
}
