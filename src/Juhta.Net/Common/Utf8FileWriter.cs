
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using System.Text;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a writer class for UTF8-encoded files.
    /// </summary>
    public class Utf8FileWriter
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="filePath">Specifies the ablosute or a relative path of a UTF8-encoded file where data will be
        /// written. If the file already exists, it will be overwritten.</param>
        public Utf8FileWriter(string filePath) : this(filePath, FileMode.Create)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="filePath">Specifies the ablosute or a relative path of a UTF8-encoded file where data will be
        /// written.</param>
        /// <param name="openMode">Specifies how the UTF8-encoded file should be opened.</param>
        public Utf8FileWriter(string filePath, FileMode openMode)
        {
            m_filePath = Path.GetFullPath(filePath);

            m_openMode = openMode;

            m_atBeginningOfLine = true;

            m_tabSize = c_defaultTabSize;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Flushes all unwritten data to the underlying UTF8-encoded file, and after that, closes the file.
        /// </summary>
        /// <remarks>This method does nothing if the underlying UTF8-encoded file is not open.</remarks>
        /// <seealso cref="Open"/>
        public void Close()
        {
            if (m_fileWriter == null)
                return;

            m_fileWriter.Flush();

            m_fileWriter.Close();

            m_fileWriter.Dispose();

            m_fileWriter = null;
        }

        /// <summary>
        /// Decreases the current indent level by one.
        /// </summary>
        public void DecreaseIndentLevel()
        {
            if (m_indentLevel > 0)
                m_indentLevel--;
        }

        /// <summary>
        /// Flushes all unwritten data to the underlying UTF8-encoded file.
        /// </summary>
        /// <remarks>This method does nothing if the underlying UTF8-encoded file is not open.</remarks>
        public void Flush()
        {
            if (m_fileWriter != null)
                m_fileWriter.Flush();
        }

        /// <summary>
        /// Increases the current indent level by one.
        /// </summary>
        public void IncreaseIndentLevel()
        {
            if (m_indentLevel < byte.MaxValue)
                m_indentLevel++;
        }

        /// <summary>
        /// Opens the underlying UTF8-encoded file.
        /// </summary>
        /// <remarks>
        /// <para>This method does nothing if the underlying UTF8-encoded file is already open.</para>
        /// <para>This method does not necessarily have to be called when writing data to the underlying UTF8-encoded
        /// file. If the file is not open, it will be automatically opened and closed in context of each write
        /// operation. However, recurrent openings and closings of the file might cause some performance loss. To gain
        /// the maximum performance, the file should be opened with an explicit call on this method prior to write
        /// operations. In this case, the file must also be explicitly closed with the <see cref="Close"/> method.</para>
        /// </remarks>
        /// <seealso cref="Close"/>
        public void Open()
        {
            if (m_fileWriter != null)
                return;

            m_fileWriter = new StreamWriter(new FileStream(m_filePath, m_openMode, FileAccess.Write), UTF8Encoding.UTF8);

            m_openMode = FileMode.Append;
        }

        /// <summary>
        /// Writes a specified string to the underlying UTF8-encoded file.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        public void Write(string s)
        {
            Write(false, s, null);
        }

        /// <summary>
        /// Writes a formatted string to the underlying UTF8-encoded file.
        /// </summary>
        /// <param name="format">Specifies a string containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="format"/>.</param>
        public void Write(string format, params object[] args)
        {
            Write(false, format, args);
        }

        /// <summary>
        /// Writes a line terminator to the underlying UTF8-encoded file.
        /// </summary>
        public void WriteLine()
        {
            WriteLineTerminators(1);
        }

        /// <summary>
        /// Writes a specified number of line terminators to the underlying UTF8-encoded file.
        /// </summary>
        /// <param name="count">Specifies a number of line terminators to write.</param>
        public void WriteLine(int count)
        {
            WriteLineTerminators(count);
        }

        /// <summary>
        /// Writes a specified string followed by a line terminator to the underlying UTF8-encoded file.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        public void WriteLine(string s)
        {
            Write(true, s, null);
        }

        /// <summary>
        /// Writes a formatted string followed by a line terminator to the underlying UTF8-encoded file.
        /// </summary>
        /// <param name="format">Specifies a string containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="format"/>.</param>
        public void WriteLine(string format, params object[] args)
        {
            Write(true, format, args);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the absolute path of the underlying UTF8-encoded file.
        /// </summary>
        public string FilePath
        {
            get {return(m_filePath);}
        }

        /// <summary>
        /// Gets or sets the current indent level.
        /// </summary>
        /// <remarks>The result of the multiplication of <see cref="IndentLevel"/> and <see cref="TabSize"/> determines
        /// the number of space characters to write at the beginning of each new line.</remarks>
        /// <seealso cref="TabSize"/>
        public byte IndentLevel
        {
            get {return(m_indentLevel);}

            set {m_indentLevel = value;}
        }

        /// <summary>
        /// Gets or sets the current tab size.
        /// </summary>
        /// <seealso cref="IndentLevel"/>
        public byte TabSize
        {
            get {return(m_tabSize);}

            set {m_tabSize = value;}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Writes data to the underlying UTF8-encoded file.
        /// </summary>
        /// <param name="endWithLineTerminator">If true, a line terminator will be finally written to the file.</param>
        /// <param name="format">Specifies a string containing zero or more format items.</param>
        /// <param name="args">Specifies an object array containing zero or more objects to format. These objects must
        /// correspond to the format items in <paramref name="format"/>. Can be null.</param>
        private void Write(bool endWithLineTerminator, string format, params object[] args)
        {
            bool close = m_fileWriter == null;

            Open();

            if (m_atBeginningOfLine)
                m_fileWriter.Write(String.Empty.PadRight(m_indentLevel * m_tabSize, ' '));

            m_atBeginningOfLine = false;

            if (args != null)
                m_fileWriter.Write(format, args);
            else
                m_fileWriter.Write(format);

            if (endWithLineTerminator)
            {
                m_fileWriter.WriteLine();

                m_atBeginningOfLine = true;
            }

            if (close)
                Close();
        }

        /// <summary>
        /// Writes a specified number of line terminators to the underlying UTF8-encoded file.
        /// </summary>
        /// <param name="count">Specifies a number of line terminators to write.</param>
        private void WriteLineTerminators(int count)
        {
            bool close = m_fileWriter == null;

            Open();

            for (int i = 0; i < count; i++)
                m_fileWriter.WriteLine();

            m_atBeginningOfLine = true;

            if (close)
                Close();
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the default tab size.
        /// </summary>
        private const int c_defaultTabSize = 4;

        #endregion

        #region Private Fields

        /// <summary>
        /// Determines whether the file pointer of the underlying UTF8-encoded file is currently at the beginning of a
        /// line.
        /// </summary>
        private bool m_atBeginningOfLine;

        /// <summary>
        /// Stores the <see cref="FilePath"/> property.
        /// </summary>
        private string m_filePath;

        /// <summary>
        /// Specifies a StreamWriter object for writing data to the underlying UTF8-encoded file.
        /// </summary>
        private StreamWriter m_fileWriter;

        /// <summary>
        /// Stores the <see cref="IndentLevel"/> property.
        /// </summary>
        private byte m_indentLevel;

        /// <summary>
        /// Specifies the mode with which the underlying UTF8-encoded file must be opened for the first time. After the
        /// first opening, this field will be updated to <see cref="FileMode.Append"/>.
        /// </summary>
        private FileMode m_openMode;

        /// <summary>
        /// Stores the <see cref="TabSize"/> property.
        /// </summary>
        private byte m_tabSize;

        #endregion
    }
}
