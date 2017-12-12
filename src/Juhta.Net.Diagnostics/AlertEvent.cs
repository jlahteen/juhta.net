
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
    /// Defines a class for alert events.
    /// </summary>
    internal class AlertEvent : Event
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a value for the <see cref="Event.Message"/> property.</param>
        public AlertEvent(string message) : base(EventType.Alert, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a value for the <see cref="Event.Message"/> property.</param>
        /// <param name="id">Specifies a value for the <see cref="Event.ID"/> property.</param>
        public AlertEvent(string message, string id) : base(EventType.Alert, message, id)
        {}

        #endregion
    }
}
