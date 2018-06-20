
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an interface for filter rules. For any diagnostic event, validity of a filter rule must always be
    /// resolvable either to true or false.
    /// </summary>
    internal interface IFilterRule
    {
        #region Methods

        /// <summary>
        /// Checks whether this IFilterRule instance is valid for a specified diagnostic event.
        /// </summary>
        /// <param name="event">Specifies a diagnostic event.</param>
        /// <returns>Returns true if this IFilterRule instance is valid for the specified diagnostic event, otherwise
        /// false.</returns>
        bool IsValidFor(Event @event);

        #endregion
    }
}
