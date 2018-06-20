
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Text;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an event formatter class that can be used to format events to HTML-based event streams where each event
    /// will be rendered in a HTML table element.
    /// </summary>
    internal class HtmlTableFormatter : IEventFormatter
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="IEventFormatter.FormatEvent"/>.
        /// </summary>
        public string FormatEvent(Event @event)
        {
            List<string> fieldNames, fieldValues;

            Event.GetNonEmptyFields(@event, out fieldNames, out fieldValues);

            return(BuildTableElement(fieldNames, fieldValues));
        }

        /// <summary>
        /// Gets definitions for all possible cell styles.
        /// </summary>
        /// <returns>Returns a string containing definitions for all possible cell styles.</returns>
        public static string GetAllCellStyleDefinitions()
        {
            CellStyleFlags[] horizontalFlags, verticalFlags, backgroundFlags;
            StringBuilder definitions = new StringBuilder();

            horizontalFlags = new CellStyleFlags[]{CellStyleFlags.LeftColumnCell, CellStyleFlags.RightColumnCell};

            verticalFlags = new CellStyleFlags[]{CellStyleFlags.TopRowCell, CellStyleFlags.MiddleRowCell, CellStyleFlags.BottomRowCell};

            backgroundFlags = new CellStyleFlags[]{CellStyleFlags.LightGrayBackground, CellStyleFlags.DarkGrayBackground, CellStyleFlags.GreenBackground, CellStyleFlags.YellowBackground, CellStyleFlags.RedBackground, CellStyleFlags.DarkRedBackground};

            foreach (CellStyleFlags horizontalFlag in horizontalFlags)
                foreach (CellStyleFlags verticalFlag in verticalFlags)
                    foreach (CellStyleFlags backgroundFlag in backgroundFlags)
                        definitions.Append(GenerateCellStyleDefinition(horizontalFlag | verticalFlag | backgroundFlag));

            return(definitions.ToString());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds an HTML table element based on specified event field names and values.
        /// </summary>
        /// <param name="fieldNames">Specifies a list of event field names.</param>
        /// <param name="fieldValues">Specifies a list of event field values.</param>
        /// <returns>Returns the HTML code of the built table element.</returns>
        /// <remarks>Items in the arrays <paramref name="fieldNames"/> and <paramref name="fieldValues"/> must
        /// correspond to each other.</remarks>
        private static string BuildTableElement(List<string> fieldNames, List<string> fieldValues)
        {
            StringBuilder tableElement = new StringBuilder();
            int i;
            CellStyleFlags fieldNameStyleFlags, fieldValueStyleFlags;

            // Open the table element
            tableElement.AppendLine("<table border=\"0\" cellpadding=\"2\" cellspacing=\"0\" width=\"100%\">");

            // Set default values for the columns
            tableElement.AppendLine("<col align=\"left\" valign=\"top\" width=\"142\" height=\"200\"></col><col height=\"200\"></col>");

            // Add the table rows

            for (i = 0; i < fieldNames.Count; i++)
            {
                // Set the style flags regarding horizontal cell positions

                fieldNameStyleFlags = CellStyleFlags.LeftColumnCell;

                fieldValueStyleFlags = CellStyleFlags.RightColumnCell;

                // Set the style flags regarding vertical cell positions

                if (i == 0)
                {
                    fieldNameStyleFlags |= CellStyleFlags.TopRowCell;

                    fieldValueStyleFlags |= CellStyleFlags.TopRowCell;
                }
                else if (i < fieldNames.Count - 1)
                {
                    fieldNameStyleFlags |= CellStyleFlags.MiddleRowCell;

                    fieldValueStyleFlags |= CellStyleFlags.MiddleRowCell;
                }
                else
                {
                    fieldNameStyleFlags |= CellStyleFlags.BottomRowCell;

                    fieldValueStyleFlags |= CellStyleFlags.BottomRowCell;
                }

                // Set the style flags regarding cell backgrounds

                if (fieldNames[i] == EventField.Type.ToString())
                {
                    if (i % 2 == 0)
                        fieldNameStyleFlags |= CellStyleFlags.LightGrayBackground;
                    else
                        fieldNameStyleFlags |= CellStyleFlags.DarkGrayBackground;

                    if (fieldValues[i] == EventType.Information.ToString())
                        fieldValueStyleFlags |= CellStyleFlags.GreenBackground;

                    else if (fieldValues[i] == EventType.Warning.ToString())
                        fieldValueStyleFlags |= CellStyleFlags.YellowBackground;

                    else if (fieldValues[i] == EventType.Error.ToString())
                        fieldValueStyleFlags |= CellStyleFlags.RedBackground;

                    else if (fieldValues[i] == EventType.Alert.ToString())
                        fieldValueStyleFlags |= CellStyleFlags.DarkRedBackground;
                }
                else if (i % 2 == 0)
                {
                    fieldNameStyleFlags |= CellStyleFlags.LightGrayBackground;

                    fieldValueStyleFlags |= CellStyleFlags.LightGrayBackground;
                }
                else
                {
                    fieldNameStyleFlags |= CellStyleFlags.DarkGrayBackground;

                    fieldValueStyleFlags |= CellStyleFlags.DarkGrayBackground;
                }

                // Replace all HTML special characters in the field value
                fieldValues[i] = fieldValues[i].ReplaceHtmlSpecialCharacters();

                // Preserve line breaks in the Message field value
                if (fieldNames[i] == EventField.Message.ToString())
                    fieldValues[i] = fieldValues[i].Replace(Environment.NewLine, Environment.NewLine + "<br>");

                // Add the table row
                tableElement.AppendLine(String.Format("<tr><td class=\"{0}\">{1}</td><td class=\"{2}\">{3}</td></tr>", GenerateCellStyleName(fieldNameStyleFlags), fieldNames[i], GenerateCellStyleName(fieldValueStyleFlags), fieldValues[i]));
            }

            // Close the table element
            tableElement.AppendLine("</table>");

            // Return
            return(tableElement.ToString());
        }

        /// <summary>
        /// Generates a cell style definition corresponding to specified cell style flags.
        /// </summary>
        /// <param name="cellStyleFlags">Specifies cell style flags.</param>
        /// <returns>Returns the generated cell style definition.</returns>
        private static string GenerateCellStyleDefinition(CellStyleFlags cellStyleFlags)
        {
            StringBuilder definition = new StringBuilder();

            // Generate the cell style definition

            // Cell style name
            definition.AppendLine("." + GenerateCellStyleName(cellStyleFlags));

            // Open the definition block
            definition.AppendLine("{");

            // Common attributes

            definition.AppendLine("font-family: Courier New;");

            definition.AppendLine("font-size: 10pt;");

            definition.AppendLine("height: 15pt;");

            definition.AppendLine("vertical-align: top;");

            // Left column cell attributes

            if ((cellStyleFlags & CellStyleFlags.LeftColumnCell) != 0)
            {
                definition.AppendLine("border-left: 1px solid #000000;");

                definition.AppendLine("border-right: 1px solid #C0C0C0;");

                definition.AppendLine("font-weight: bold;");
            }

            // Right column cell attributes
            if ((cellStyleFlags & CellStyleFlags.RightColumnCell) != 0)
                definition.AppendLine("border-right: 1px solid #000000;");

            // Top row cell attributes
            if ((cellStyleFlags & CellStyleFlags.TopRowCell) != 0)
                definition.AppendLine("border-top: 1px solid #000000;");

            // Bottom row cell attributes
            if ((cellStyleFlags & CellStyleFlags.BottomRowCell) != 0)
                definition.AppendLine("border-bottom: 1px solid #000000;");

            // Background attributes

            if ((cellStyleFlags & CellStyleFlags.LightGrayBackground) != 0)
                definition.AppendLine("background: #F3F3F3;");

            if ((cellStyleFlags & CellStyleFlags.DarkGrayBackground) != 0)
                definition.AppendLine("background: #E6E6E6;");

            if ((cellStyleFlags & CellStyleFlags.GreenBackground) != 0)
                definition.AppendLine("background: #00FF00;");

            if ((cellStyleFlags & CellStyleFlags.YellowBackground) != 0)
            {
                definition.AppendLine("background: #FFFF00;");

                definition.AppendLine("font-weight: bold;");
            }

            if ((cellStyleFlags & CellStyleFlags.RedBackground) != 0)
            {
                definition.AppendLine("background: #FF0000;");

                definition.AppendLine("font-weight: bold;");

                definition.AppendLine("color: #FFFFFF;");
            }

            if ((cellStyleFlags & CellStyleFlags.DarkRedBackground) != 0)
            {
                definition.AppendLine("background: #8B0000;");

                definition.AppendLine("font-weight: bold;");

                definition.AppendLine("color: #FFFFFF;");
            }

            // Close the definition block
            definition.AppendLine("}");

            // Return
            return(definition.ToString());
        }

        /// <summary>
        /// Generates a name for a cell style that is defined by cell style flags.
        /// </summary>
        /// <param name="cellStyleFlags">Specifies cell style flags.</param>
        /// <returns>Returns the generated name.</returns>
        private static string GenerateCellStyleName(CellStyleFlags cellStyleFlags)
        {
            return("style_" + Convert.ToString((int)cellStyleFlags, 16));
        }

        #endregion

        #region Private Types

        /// <summary>
        /// Defines an enumeration for the cell style flags.
        /// </summary>
        private enum CellStyleFlags
        {
            /// <summary>
            /// The cell lies in the left column of the table.
            /// </summary>
            LeftColumnCell = 1 << 0,

            /// <summary>
            /// The cell lies in the right column of the table.
            /// </summary>
            RightColumnCell = 1 << 1,

            /// <summary>
            /// The cell lies in the top row of the table.
            /// </summary>
            TopRowCell = 1 << 2,

            /// <summary>
            /// The cell lies in a middle row of the table.
            /// </summary>
            MiddleRowCell = 1 << 3,

            /// <summary>
            /// The cell lies in the bottom row of the table.
            /// </summary>
            BottomRowCell = 1 << 4,

            /// <summary>
            /// The cell has a light gray background.
            /// </summary>
            LightGrayBackground = 1 << 5,

            /// <summary>
            /// The cell has a dark gray background.
            /// </summary>
            DarkGrayBackground = 1 << 6,

            /// <summary>
            /// The cell has a green background.
            /// </summary>
            GreenBackground = 1 << 7,

            /// <summary>
            /// The cell has a yellow background.
            /// </summary>
            YellowBackground = 1 << 8,

            /// <summary>
            /// The cell has a red background.
            /// </summary>
            RedBackground = 1 << 9,

            /// <summary>
            /// The cell has a dark red background.
            /// </summary>
            DarkRedBackground = 1 << 10
        }

        #endregion
    }
}
