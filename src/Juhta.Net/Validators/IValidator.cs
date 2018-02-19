
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Validators
{
    /// <summary>
    /// Defines a generic interface for validators.
    /// </summary>
    /// <typeparam name="T">Specifies the type of values to validate.</typeparam>
    public interface IValidator<T>
    {
        #region Methods

        /// <summary>
        /// Validates a specified value. If the specified value doesn't pass validation, the method must throw an
        /// instance of <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="value">Specifies a value to validate.</param>
        void Validate(T value);

        #endregion
    }
}
