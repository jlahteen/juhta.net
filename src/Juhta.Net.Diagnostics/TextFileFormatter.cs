
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a default event formatter for text file streams.
    /// </summary>
    internal class TextFileFormatter : IEventFormatter
    {
        #region Static Constructor

        /// <summary>
        /// Initializes the class.
        /// </summary>
        static TextFileFormatter()
        {
            foreach (EventField field in Enum.GetValues(typeof(EventField)))
                if (Event.GetFieldName(field).Length > s_maxFieldNameLength)
                    s_maxFieldNameLength = Event.GetFieldName(field).Length;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="IEventFormatter.FormatEvent"/>.
        /// </summary>
        public string FormatEvent(Event @event)
        {
            List<string> fieldNames, fieldValues;
            int i, j;
            string[] fieldValueLines;
            StringBuilder builder = new StringBuilder();

            // Get the non-empty fields
            Event.GetNonEmptyFields(@event, out fieldNames, out fieldValues);

            // Format the non-empty fields

            for (i = 0; i < fieldNames.Count; i++)
            {
                fieldValueLines = fieldValues[i].Split(new string[]{Environment.NewLine}, StringSplitOptions.None);

                for (j = 0; j < fieldValueLines.Length; j++)
                {
                    if (j == 0)
                        builder.AppendFormat("{0}: {1}", fieldNames[i].PadRight(s_maxFieldNameLength, '.'), fieldValueLines[j]);
                    else
                        builder.AppendFormat("{0}: {1}", " ".PadRight(s_maxFieldNameLength), fieldValueLines[j]);

                    builder.AppendLine();
                }
            }

            // Return
            return(builder.ToString());
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the length of the longest field name.
        /// </summary>
        private static int s_maxFieldNameLength;

        #endregion
    }
}
