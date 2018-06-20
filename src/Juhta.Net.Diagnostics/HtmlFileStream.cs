
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Xml;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an event stream whose destination is an HTML file.
    /// </summary>
    internal class HtmlFileStream : FileStream
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="htmlFileStreamNode">Specifies an htmlFileStream XML node based on which the instance will be
        /// initialized.</param>
        public HtmlFileStream(XmlNode htmlFileStreamNode) : base(htmlFileStreamNode, "html")
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="FileStream.WriteEvent"/>.
        /// </summary>
        public override void WriteEvent(Event @event)
        {
            IEventFormatter eventFormatter;

            if (this.IsNewFile)
            {
                eventFormatter = new HtmlPageFormatter();

                Write(eventFormatter.FormatEvent(@event));
            }
            else
            {
                SeekBackFromEnd(HtmlPageFormatter.EndHtml.Length);

                eventFormatter = new HtmlTableFormatter();

                WriteLine("<br>");

                Write(eventFormatter.FormatEvent(@event));

                Write(HtmlPageFormatter.EndHtml);
            }
        }

        #endregion
    }
}
