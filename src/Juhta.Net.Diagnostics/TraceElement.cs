
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an abstract base class for trace elements. A trace element can represent any logical tracing entity. In
    /// addition, a trace element may also have a parent and child element.
    /// </summary>
    internal abstract class TraceElement
    {
        #region Public Methods

        /// <summary>
        /// Closes this TraceElement instance. After calling this method, any trace data should not be written to this
        /// TraceElement instance.
        /// </summary>
        public virtual void Close()
        {
            if (m_currentLine != null)
                WriteLine();

            this.RelativeIndent = 0;

            WriteLine(m_endLine);

            if (m_parentElement != null)
            {
                m_parentElement.m_childElement = null;

                m_parentElement = null;
            }

            if (m_childElement != null)
            {
                m_childElement.m_parentElement = null;

                m_childElement = null;
            }
        }

        /// <summary>
        /// Decreases the current relative indent for trace output by one.
        /// </summary>
        public virtual void DecreaseRelativeIndent()
        {
            if (m_relativeIndent > 1)
                m_relativeIndent--;
        }

        /// <summary>
        /// Climbs up in the trace element tree specified by this TraceElement instance, and finds a TraceElement
        /// object by a type.
        /// </summary>
        /// <typeparam name="T">Specifies a type that derives from TraceElement.</typeparam>
        /// <returns>Returns the first TraceElement object whose type matches the specified type, or null, if such
        /// TraceElement object was not found.</returns>
        public T FindElement<T>() where T : TraceElement
        {
            TraceElement traceElement = this;

            while (traceElement != null)
                if (traceElement is T)
                    return((T)traceElement);
                else
                    traceElement = traceElement.m_parentElement;

            return(null);
        }

        /// <summary>
        /// Climbs up in the trace element tree specified by this TraceElement instance, and finds a TraceElement
        /// object by a type and description.
        /// </summary>
        /// <typeparam name="T">Specifies a type that derives from TraceElement.</typeparam>
        /// <param name="description">Specifies a description.</param>
        /// <returns>Returns the first TraceElement object that meets the specified type and description conditions, or
        /// null, if such TraceElement object was not found.</returns>
        public T FindElement<T>(string description) where T : TraceElement
        {
            TraceElement traceElement = this;

            while (traceElement != null)
                if (traceElement is T && traceElement.m_description == description)
                    return((T)traceElement);
                else
                    traceElement = traceElement.m_parentElement;

            return(null);
        }

        /// <summary>
        /// Gets the leaf element of the trace element tree specified by this TraceElement instance.
        /// </summary>
        /// <returns>Returns the leaf element of the trace element tree specified by this TraceElement instance.</returns>
        public TraceElement GetLeafElement()
        {
            TraceElement traceElement = this;

            while (traceElement.m_childElement != null)
                traceElement = traceElement.m_childElement;

            return(traceElement);
        }

        /// <summary>
        /// Increases the current relative indent for trace output by one.
        /// </summary>
        public virtual void IncreaseRelativeIndent()
        {
            m_relativeIndent++;
        }

        /// <summary>
        /// Opens this TraceElement instance for tracing data.
        /// </summary>
        public void Open()
        {
            m_stopwatch.Start();

            WriteLine(m_startLine);

            this.RelativeIndent++;
        }

        /// <summary>
        /// Writes a string to this TraceElement instance.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        public virtual void Write(string s)
        {
            if (s == null)
                s = "";

            s = s.Replace(Environment.NewLine, "\\n");

            if (m_currentLine == null)
            {
                m_currentLine += m_traceBlock.TraceWriter.CurrentTimestamp.ToTimestamp('T', true, true) + "  ";

                m_currentLine += "+" + FormatElapsedTime(m_traceBlock.TraceWriter.Stopwatch) + "  ";

                m_currentLine += "+" + FormatElapsedTime(m_traceBlock.m_stopwatch) + "  ";

                m_currentLine += "+" + FormatElapsedTime(m_stopwatch) + "  ";

                for (int i = 0; i < this.AbsoluteIndent; i++)
                    m_currentLine += "| ";
            }

            m_currentLine += s;
        }

        /// <summary>
        /// Writes a formatted string to this TraceElement instance.
        /// </summary>
        /// <param name="format">Specifies a string containing zero or more format items.</param>
        /// <param name="args">Specifies an array of objects to format.</param>
        public virtual void Write(string format, params object[] args)
        {
            Write(String.Format(format, args));
        }

        /// <summary>
        /// Writes an exception to this TraceElement instance.
        /// </summary>
        /// <param name="exception">Specifies an exception.</param>
        public virtual void WriteException(Exception exception)
        {
            if (exception != null)
                WriteLabeledMessage("Exception", exception.ToString());
            else
                WriteLabeledMessage("Exception", "<null>");
        }

        /// <summary>
        /// Writes a labeled message to this TraceElement instance.
        /// </summary>
        /// <param name="label">Specifies a label.</param>
        /// <param name="message">Specifies a message.</param>
        public virtual void WriteLabeledMessage(string label, string message)
        {
            string[] lines;

            if (label == null)
                label = "";

            if (message == null)
                message = "";

            if (message.Contains(Environment.NewLine))
            {
                lines = message.Split(new string[]{Environment.NewLine}, StringSplitOptions.None);

                WriteLine(label + ":");

                IncreaseRelativeIndent();

                foreach (string line in lines)
                    WriteLine(line);

                DecreaseRelativeIndent();

                WriteLine("End of " + label);
            }
            else
                WriteLine(label + ": " + message);
        }

        /// <summary>
        /// Writes a labeled message to this TraceElement instance.
        /// </summary>
        /// <param name="label">Specifies a label.</param>
        /// <param name="messageFormat">Specifies a message format.</param>
        /// <param name="args">Specifies an array of objects to format.</param>
        public virtual void WriteLabeledMessage(string label, string messageFormat, params object[] args)
        {
            WriteLabeledMessage(label, String.Format(messageFormat, args));
        }

        /// <summary>
        /// Writes a line terminator to this TraceElement instance.
        /// </summary>
        public virtual void WriteLine()
        {
            WriteLine(String.Empty);
        }

        /// <summary>
        /// Writes a string with a line terminator to this TraceElement instance.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        public virtual void WriteLine(string s)
        {
            Write(s);

            m_traceBlock.AppendLine(m_currentLine);

            m_currentLine = null;
        }

        /// <summary>
        /// Writes a formatted string with a line terminator to this TraceElement instance.
        /// </summary>
        /// <param name="format">Specifies a string containing zero or more format items.</param>
        /// <param name="args">Specifies an array of objects to format.</param>
        public virtual void WriteLine(string format, params object[] args)
        {
            WriteLine(String.Format(format, args));
        }

        /// <summary>
        /// Writes an object graph to this TraceElement instance.
        /// </summary>
        /// <param name="objectID">Specifies an object identifier, e.g. a variable or property name.</param>
        /// <param name="object">Specifies an object.</param>
        public virtual void WriteObjectGraph(string objectID, object @object)
        {
            ObjectGraphWriter objectGraphWriter = new ObjectGraphWriter(this);

            objectGraphWriter.WriteGraph(objectID, @object);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current absolute indent for trace output.
        /// </summary>
        public virtual int AbsoluteIndent
        {
            get {return(m_baseIndent + m_relativeIndent);}
        }

        /// <summary>
        /// Gets or sets the current relative indent for trace output.
        /// </summary>
        public virtual int RelativeIndent
        {
            get {return(m_relativeIndent);}

            set
            {
                if (value >= 0)
                    m_relativeIndent = value;
            }
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parentElement">Specifies a TraceElement object that will be set as the parent element for the
        /// instance.</param>
        protected TraceElement(TraceElement parentElement)
        {
            if (parentElement != null)
            {
                m_parentElement = parentElement;

                m_parentElement.m_childElement = this;

                m_baseIndent = m_parentElement.AbsoluteIndent;
            }

            m_stopwatch = new Stopwatch();

            m_traceBlock = FindElement<TraceBlock>();
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Specifies a description for the trace data written to this TraceElement instance.
        /// </summary>
        protected string m_description;

        /// <summary>
        /// Specifies the end line for this TraceElement instance. In other words, the trace data specified by the
        /// instance ends with this line.
        /// </summary>
        protected string m_endLine;

        /// <summary>
        /// Specifies the start line for this TraceElement instance. In other words, the trace data specified by the
        /// instance starts with this line.
        /// </summary>
        protected string m_startLine;

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats the elapsed time measured by a specified Stopwatch object.
        /// </summary>
        /// <param name="stopwatch">Specifies a Stopwatch object.</param>
        /// <returns>Returns the elapsed time measured by the specified Stopwatch object formatted to a string.</returns>
        private string FormatElapsedTime(Stopwatch stopwatch)
        {
            TimeSpan elapsed;

            elapsed = stopwatch.Elapsed;

            return(String.Format("{0:0000}:{1:00}:{2:00},{3:0000000}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds, elapsed.Ticks % 10000000));
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the base indent for trace output for this TraceElement instance.
        /// </summary>
        private int m_baseIndent;

        /// <summary>
        /// Specifies the child of this TraceElement instance. A TraceElement object can have only one child at a time.
        /// </summary>
        private TraceElement m_childElement;

        /// <summary>
        /// Specifies the current incomplete line of trace data.
        /// </summary>
        private string m_currentLine;

        /// <summary>
        /// Specifies the parent of this TraceElement instance.
        /// </summary>
        private TraceElement m_parentElement;

        /// <summary>
        /// Stores the <see cref="RelativeIndent"/> property.
        /// </summary>
        private int m_relativeIndent;

        /// <summary>
        /// Specifies a Stopwatch object that measures elapsed time from the moment when this TraceElement instance has
        /// been opened.
        /// </summary>
        private Stopwatch m_stopwatch;

        /// <summary>
        /// Specifies the TraceBlock object of the trace element tree to which this TraceElement instance belongs.
        /// </summary>
        private TraceBlock m_traceBlock;

        #endregion
    }
}
