
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
    /// Defines an event stream whose destination is a text file.
    /// </summary>
    internal class TextFileStream : FileStream
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="textFileStreamNode">Specifies a textFileStream XML node based on which the instance will be
        /// initialized.</param>
        public TextFileStream(XmlNode textFileStreamNode) : base(textFileStreamNode, "log")
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="FileStream.WriteEvent"/>.
        /// </summary>
        public override void WriteEvent(Event @event)
        {
            IEventFormatter eventFormatter = new TextFileFormatter();

            SeekEnd();

            WriteLine();

            Write(eventFormatter.FormatEvent(@event));
        }

        #endregion
    }
}
