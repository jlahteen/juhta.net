
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    #region Internal Types

    /// <summary>
    /// Defines an enumeration for the fields (i.e. public properties) of diagnostic events.
    /// </summary>
    internal enum EventField
    {
        /// <summary>
        /// Specifies the Type field (<see cref="Event.Type"/>).
        /// </summary>
        Type,

        /// <summary>
        /// Specifies the Timestamp field (<see cref="Event.Timestamp"/>).
        /// </summary>
        Timestamp,

        /// <summary>
        /// Specifies the ID field (<see cref="Event.ID"/>).
        /// </summary>
        ID,

        /// <summary>
        /// Specifies the MachineName field (<see cref="Event.MachineName"/>).
        /// </summary>
        MachineName,

        /// <summary>
        /// Specifies the ExecutablePath field (<see cref="Event.ExecutablePath"/>).
        /// </summary>
        ExecutablePath,

        /// <summary>
        /// Specifies the ProcessOwner field (<see cref="Event.ProcessOwner"/>).
        /// </summary>
        ProcessOwner,

        /// <summary>
        /// Specifies the ProcessID field (<see cref="Event.ProcessID"/>).
        /// </summary>
        ProcessID,

        /// <summary>
        /// Specifies the ManagedThreadID field (<see cref="Event.ManagedThreadID"/>).
        /// </summary>
        ManagedThreadID,

        /// <summary>
        /// Specifies the Message field (<see cref="Event.Message"/>).
        /// </summary>
        Message
    }

    /// <summary>
    /// Defines an enumeration for the diagnostic event types.
    /// </summary>
    /// <remarks>The defined values conform to the values defined in the enumeration <see cref="DiagnosticMessageType"/>.</remarks>
    internal enum EventType
    {
        /// <summary>
        /// The diagnostic event is an alert event.
        /// </summary>
        Alert = DiagnosticMessageType.Alert,

        /// <summary>
        /// The diagnostic event is an error event.
        /// </summary>
        Error = DiagnosticMessageType.Error,

        /// <summary>
        /// The diagnostic event is an information event.
        /// </summary>
        Information = DiagnosticMessageType.Information,

        /// <summary>
        /// The diagnostic event is a warning event.
        /// </summary>
        Warning = DiagnosticMessageType.Warning
    }

    #endregion
}
