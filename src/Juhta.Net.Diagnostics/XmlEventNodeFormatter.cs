
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Text;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an event formatter for XML file streams.
    /// </summary>
    internal class XmlEventNodeFormatter : IEventFormatter
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="IEventFormatter.FormatEvent"/>.
        /// </summary>
        public string FormatEvent(Event @event)
        {
            StringBuilder builder = new StringBuilder();
            string fieldValue, nodeName;

            // Open the event node
            builder.AppendLine("<event>");

            // Append the non-empty event fields as child nodes

            foreach (EventField field in Enum.GetValues(typeof(EventField)))
                if (!String.IsNullOrEmpty(fieldValue = @event.GetFieldValue(field)))
                {
                    // Replace all XML special characters in the field value
                    fieldValue = fieldValue.ReplaceXmlSpecialCharacters();

                    // Build the node name
                    if (field.ToString().Length <= "IO".Length)
                        nodeName = field.ToString().ToLower();
                    else
                        nodeName = Char.ToLower(field.ToString()[0]) + field.ToString().Substring(1);

                    // Append the field as a child node
                    builder.AppendFormat("<{0}>{1}</{0}>", nodeName, fieldValue);
                    builder.AppendLine();
                }

            // Close the event node
            builder.AppendLine("</event>");

            // Return
            return(builder.ToString());
        }

        #endregion
    }
}
