
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// Defines a static class containing extension methods for the <see cref="Array"/> class.
    /// </summary>
    public static class ArrayExtensions
    {
        #region Public Methods

        /// <summary>
        /// Converts the current Array instance to a string array.
        /// </summary>
        /// <param name="array">Specifies the current Array instance.</param>
        /// <returns>Returns the current Array instance as a string array.</returns>
        public static string[] ToStringArray(this Array array)
        {
            string[] stringArray = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
                stringArray[i] = array.GetValue(i).ToString();

            return(stringArray);
        }

        #endregion
    }
}
