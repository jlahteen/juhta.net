
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
    /// Defines an interface for event formatters. An event formatter converts Event objects to formatted strings. It
    /// totally depends on an event formatter how resulting formatted strings look like and what information they
    /// contain.
    /// </summary>
    internal interface IEventFormatter
    {
        #region Methods

        /// <summary>
        /// Formats a specified Event object to a string.
        /// </summary>
        /// <param name="event">Specifies an Event object.</param>
        /// <returns>Returns a formatted string that represents the specified Event object.</returns>
        string FormatEvent(Event @event);

        #endregion
    }
}
