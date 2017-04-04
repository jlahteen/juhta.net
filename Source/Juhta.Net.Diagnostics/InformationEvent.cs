
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
    /// Defines a class for information events.
    /// </summary>
    internal class InformationEvent : Event
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a value for the <see cref="Event.Message"/> property.</param>
        public InformationEvent(string message) : base(EventType.Information, message)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Specifies a value for the <see cref="Event.Message"/> property.</param>
        /// <param name="id">Specifies a value for the <see cref="Event.ID"/> property.</param>
        public InformationEvent(string message, string id) : base(EventType.Information, message, id)
        {}

        #endregion
    }
}
