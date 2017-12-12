
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Management;
using System.Threading;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an abstract base class for diagnostic events.
    /// </summary>
    internal abstract class Event
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type">Specifies a value for the <see cref="Type"/> property.</param>
        /// <param name="message">Specifies a value for the <see cref="Message"/> property.</param>
        public Event(EventType type, string message) : this(type, message, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type">Specifies a value for the <see cref="Type"/> property.</param>
        /// <param name="message">Specifies a value for the <see cref="Message"/> property.</param>
        /// <param name="id">Specifies a value for the <see cref="ID"/> property.</param>
        public Event(EventType type, string message, string id)
        {
            this.Type = type;

            this.Message = message;

            this.ID = id;

            this.ExecutablePath = s_executablePath;

            this.MachineName = Environment.MachineName;

            this.ManagedThreadID = Thread.CurrentThread.ManagedThreadId;

            this.ProcessID = s_processID;

            this.ProcessOwner = s_processOwner;

            this.Timestamp = DateTime.Now.ToTimestamp('T', true, true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the display name of a specified event field.
        /// </summary>
        /// <param name="field">Specifies an event field.</param>
        /// <returns>Returns the display name of the specified event field.</returns>
        public static string GetFieldName(EventField field)
        {
            switch (field)
            {
                case EventField.ExecutablePath:
                    return("Executable Path");

                case EventField.ID:
                    return("Event ID");

                case EventField.MachineName:
                    return("Machine Name");

                case EventField.ManagedThreadID:
                    return("Managed Thread ID");

                case EventField.ProcessID:
                    return("Process ID");

                case EventField.ProcessOwner:
                    return("Process Owner");

                default:
                    return(field.ToString());
            }
        }

        /// <summary>
        /// Gets the value of a specified event field.
        /// </summary>
        /// <param name="field">Specifies an event field.</param>
        /// <returns>Returns the value of the specified event field.</returns>
        public string GetFieldValue(EventField field)
        {
            switch (field)
            {
                case EventField.ExecutablePath:
                    return(this.ExecutablePath);

                case EventField.ID:
                    return(this.ID);

                case EventField.MachineName:
                    return(this.MachineName);

                case EventField.ManagedThreadID:
                    return(this.ManagedThreadID.ToString());

                case EventField.Message:
                    return(this.Message);

                case EventField.ProcessID:
                    return(this.ProcessID.ToString());

                case EventField.ProcessOwner:
                    return(this.ProcessOwner);

                case EventField.Timestamp:
                    return(this.Timestamp);

                case EventField.Type:
                    return(this.Type.ToString());

                default:
                    return(null);
            }
        }

        /// <summary>
        /// Gets the non-empty fields of a specified Event object.
        /// </summary>
        /// <param name="event">Specifies an Event object.</param>
        /// <param name="fieldNames">Returns the names of the non-empty fields of the specified Event object.</param>
        /// <param name="fieldValues">Returns the values of the non-empty fields of the specified Event object.</param>
        /// <remarks>Field names and values in <paramref name="fieldNames"/> and <paramref name="fieldValues"/>
        /// correspond to each other.</remarks>
        public static void GetNonEmptyFields(Event @event, out List<string> fieldNames, out List<string> fieldValues)
        {
            string fieldName, fieldValue;

            fieldNames = new List<string>();
            fieldValues = new List<string>();

            foreach (EventField field in Enum.GetValues(typeof(EventField)))
            {
                fieldName = Event.GetFieldName(field);
                fieldValue = @event.GetFieldValue(field);

                if (!String.IsNullOrEmpty(fieldValue))
                {
                    fieldNames.Add(fieldName);
                    fieldValues.Add(fieldValue);
                }
            }
        }

        /// <summary>
        /// Initializes the class.
        /// </summary>
        public static void Initialize()
        {
            ManagementObjectSearcher processSearcher = null;
            string[] args = new string[2];

            // Initialize the static fields

            // s_executablePath
            s_executablePath = Process.GetCurrentProcess().MainModule.FileName;

            // s_processID
            s_processID = Process.GetCurrentProcess().Id;

            // s_processOwner

            try
            {
                processSearcher = new ManagementObjectSearcher();

                processSearcher.Query = new SelectQuery(String.Format("SELECT Handle FROM Win32_Process where ProcessId = {0}", s_processID));

                foreach (ManagementObject process in processSearcher.Get())
                {
                    // Note: We expect only one process but there is no convenient way to reference it

                    process.InvokeMethod("GetOwner", args);

                    s_processOwner = args[1] + "\\" + args[0];

                    break;
                }
            }

            finally
            {
                // Dispose unmanaged resources
                if (processSearcher != null)
                    processSearcher.Dispose();
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the full path of the executable to which the event relates.
        /// </summary>
        public string ExecutablePath
        {
            get; private set;
        }

        /// <summary>
        /// Returns true if the event has an ID.
        /// </summary>
        public bool HasID
        {
            get {return(!String.IsNullOrEmpty(this.ID));}
        }

        /// <summary>
        /// Gets the ID of the event.
        /// </summary>
        public string ID
        {
            get; private set;
        }

        /// <summary>
        /// Gets the name of the machine where the event has been created, that is, where the event has occurred.
        /// </summary>
        public string MachineName
        {
            get; private set;
        }

        /// <summary>
        /// Gets the ID of the managed thread to which the event relates.
        /// </summary>
        public int ManagedThreadID
        {
           get; private set;
        }

        /// <summary>
        /// Gets the message of the event.
        /// </summary>
        public string Message
        {
            get; private set;
        }

        /// <summary>
        /// Gets the ID of the process to which the event relates.
        /// </summary>
        public int ProcessID
        {
            get; private set;
        }

        /// <summary>
        /// Gets the owner (i.e. username) of the process to which the event relates.
        /// </summary>
        public string ProcessOwner
        {
            get; private set;
        }

        /// <summary>
        /// Gets the timestamp of the event.
        /// </summary>
        public string Timestamp
        {
           get; private set;
        }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        public EventType Type
        {
            get; private set;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the full path of the executable that the current process is running.
        /// </summary>
        private static string s_executablePath;

        /// <summary>
        /// Specifies the ID of the current process.
        /// </summary>
        private static int s_processID;

        /// <summary>
        /// Specifies the owner (i.e. username) of the current process.
        /// </summary>
        private static string s_processOwner;

        #endregion
    }
}
