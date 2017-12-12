
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
    /// Defines a class for warning events.
    /// </summary>
    internal class WarningEvent : Event
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a value for the <see cref="Event.Message"/> property.</param>
        public WarningEvent(string message) : base(EventType.Warning, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a value for the <see cref="Event.Message"/> property.</param>
        /// <param name="id">Specifies a value for the <see cref="Event.ID"/> property.</param>
        public WarningEvent(string message, string id) : base(EventType.Warning, message, id)
        {}

        #endregion
    }
}
