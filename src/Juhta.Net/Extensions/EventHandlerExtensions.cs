
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
    /// Defines a static class containing extension methods for the <see cref="EventHandler"/> delegate.
    /// </summary>
    public static class EventHandlerExtensions
    {
        #region Public Methods

        /// <summary>
        /// Raises the event specified by this EventHandler delegate.
        /// </summary>
        /// <param name="eventHandler">Specifies the current EventHandler delegate.</param>
        /// <param name="sender">Specifies an event source.</param>
        public static void RaiseEvent(this EventHandler eventHandler, object sender)
        {
            if (eventHandler != null)
                eventHandler(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the event specified by this EventHandler delegate.
        /// </summary>
        /// <typeparam name="TEventArgs">Specifies the type of the event data.</typeparam>
        /// <param name="eventHandler">Specifies the current EventHandler delegate.</param>
        /// <param name="sender">Specifies an event source.</param>
        /// <param name="eventArgs">Specifies an object containing the event data.</param>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs) where TEventArgs : EventArgs
        {
            if (eventHandler != null)
                eventHandler(sender, eventArgs);
        }

        #endregion
    }
}
