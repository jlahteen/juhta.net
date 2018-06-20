
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
    /// Defines an interface for event streams. Event streams are typically used by event loggers for storing events.
    /// </summary>
    /// <remarks>There are no restrictions on actual streams behind the instances of this interface.</remarks>
    /// <seealso cref="IEventLogger"/>
    internal interface IEventStream
    {
        #region Methods

        /// <summary>
        /// Closes the stream that lies behind this IEventStream instance.
        /// </summary>
        void Close();

        /// <summary>
        /// Opens the stream that lies behind this IEventStream instance.
        /// </summary>
        void Open();

        /// <summary>
        /// Writes the data of a specified Event object to the stream that lies behind this IEventStream instance.
        /// </summary>
        /// <param name="event">Specifies an Event object.</param>
        void WriteEvent(Event @event);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the IEventFilter instance that must be used to filter events before writing them to the event stream
        /// lying behind this IEventStream instance.
        /// </summary>
        IEventFilter EventFilter {get;}

        /// <summary>
        /// Gets a URI by means of which the stream that lies behind this IEventStream instance can be identified and
        /// possibly located.
        /// </summary>
        /// <remarks>The scheme of the returned URI doesn't have to be well-known.</remarks>
        string Uri {get;}

        #endregion
    }
}
