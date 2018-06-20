
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
    /// Defines an event stream whose destination is an XML file.
    /// </summary>
    internal class XmlFileStream : FileStream
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="xmlFileStreamNode">Specifies an xmlFileStream XML node based on which the instance will be
        /// initialized.</param>
        public XmlFileStream(XmlNode xmlFileStreamNode) : base(xmlFileStreamNode, "xml")
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="FileStream.WriteEvent"/>.
        /// </summary>
        public override void WriteEvent(Event @event)
        {
            IEventFormatter eventFormatter = new XmlEventNodeFormatter();

            if (this.IsNewFile)
            {
                WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                WriteLine("<events>");
            }
            else
                SeekBackFromEnd("</events>".Length + Environment.NewLine.Length);

            Write(eventFormatter.FormatEvent(@event));

            WriteLine("</events>");
        }

        #endregion
    }
}
