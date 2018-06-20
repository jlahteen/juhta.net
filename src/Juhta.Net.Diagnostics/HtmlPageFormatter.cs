
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Text;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an event formatter class that formats events to complete HTML pages.
    /// </summary>
    internal class HtmlPageFormatter : IEventFormatter
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="IEventFormatter.FormatEvent"/>.
        /// </summary>
        public string FormatEvent(Event @event)
        {
            StringBuilder htmlPage = new StringBuilder();
            HtmlTableFormatter htmlTableFormatter = new HtmlTableFormatter();

            // Build the HTML page

            htmlPage.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");

            htmlPage.AppendLine("<head>");

            htmlPage.AppendLine("<title></title>");

            htmlPage.AppendLine("<style type=\"text/css\">");
            htmlPage.Append(HtmlTableFormatter.GetAllCellStyleDefinitions());
            htmlPage.AppendLine("</style>");

            htmlPage.AppendLine("</head>");

            htmlPage.AppendLine("<body style=\"background: #FAF0E6\">");

            htmlPage.Append(htmlTableFormatter.FormatEvent(@event));

            htmlPage.Append(HtmlPageFormatter.EndHtml);

            // Return
            return(htmlPage.ToString());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the HTML markup that is required to close an HTML page after the last table element.
        /// </summary>
        public static string EndHtml
        {
            get {return(String.Format("<h5 align=\"center\">Reported by {0}</h5>{1}</body>{1}</html>{1}", ProductInfo.Name, Environment.NewLine));}
        }

        #endregion
    }
}
