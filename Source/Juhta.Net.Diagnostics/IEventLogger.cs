
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
    /// Defines an interface for event loggers. Typically an event logger writes events to one or more underlying event
    /// streams.
    /// </summary>
    /// <remarks>Instances of this interface implement the actual logging services provided by the static
    /// <see cref="EventLogger"/> class.</remarks>
    /// <seealso cref="IEventStream"/>
    internal interface IEventLogger
    {
        #region Methods

        /// <summary>
        /// Logs the data of a specified Event object.
        /// </summary>
        /// <param name="event">Specifies an Event object.</param>
        void LogEvent(Event @event);

        #endregion
    }
}
