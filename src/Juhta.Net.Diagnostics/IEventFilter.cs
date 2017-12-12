
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
    /// Defines a base interface for event filters. An event filter is useful when only events having certain
    /// characteristics are wanted to write to an event stream.
    /// </summary>
    internal interface IEventFilter
    {
        #region Methods

        /// <summary>
        /// Checks whether the current IEventFilter instance stops a specified Event object.
        /// </summary>
        /// <param name="event">Specifies an Event object.</param>
        /// <returns>Returns true if the current IEventFilter instance stops the specified Event object, that is, the
        /// specified Event object doesn't pass the filter. Otherwise the return value is false.</returns>
        bool StopsEvent(Event @event);

        #endregion
    }
}
