
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Juhta.Net.Common;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an abstract base class for event streams whose destination is a file.
    /// </summary>
    internal abstract class FileStream : EventStream, IEventStream
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="IEventStream.Close"/>.
        /// </summary>
        public virtual void Close()
        {
            if (m_fileStream != null)
            {
                m_fileStream.Flush();

                m_fileStream.Close();

                m_fileStream.Dispose();
            }
        }

        /// <summary>
        /// See <see cref="IEventStream.Open"/>.
        /// </summary>
        public virtual void Open()
        {
            string filePath;
            FileSystemWatcher fileSystemWatcher;
            Stopwatch stopwatch = new Stopwatch();
            bool fileLocked = false;
            byte[] utf8Bom;

            // Get the file path that is effective at the moment
            filePath = GetEffectiveFilePath();

            // Create or open the file
            m_fileStream = new System.IO.FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            // Lock the file

            fileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), Path.GetFileName(filePath));

            stopwatch.Start();

            while (!fileLocked)
                try
                {
                    // Try to get a lock on the file
                    m_fileStream.Lock(0, m_fileStream.Length);

                    // We got a lock on the file
                    fileLocked = true;
                }

                catch (IOException)
                {
                    // The file is currently locked by another process

                    // Wait for something to happen with the file

                    if (fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All, 1000).TimedOut)
                        if (stopwatch.ElapsedMilliseconds > c_lockingTimeout)
                            throw;
                }

            // If the file is empty, write a UTF-8 byte order mark to the beginning of the file

            if (m_fileStream.Length == 0)
            {
                utf8Bom = new byte[]{0xEF, 0xBB, 0xBF};

                m_fileStream.Write(utf8Bom, 0, utf8Bom.Length);

                m_isNewFile = true;
            }
            else
                m_isNewFile = false;
        }

        /// <summary>
        /// See <see cref="IEventStream.WriteEvent"/>.
        /// </summary>
        public abstract void WriteEvent(Event @event);

        #endregion

        #region Public Properties

        /// <summary>
        /// See <see cref="IEventStream.Uri"/>.
        /// </summary>
        public string Uri
        {
            get {return(String.Format("file://{0}", System.Uri.EscapeUriString(m_filePath.Replace(c_fileSplitPlaceholder, String.Empty))));}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="fileStreamNode">Specifies a fileStream XML node based on which the instance will be
        /// initialized.</param>
        /// <param name="fileExtension">Specifies a file extension to use.</param>
        /// <remarks>The value of <paramref name="fileExtension"/> overrides the possible file extension in the
        /// <i>filePath</i> attribute of <paramref name="fileStreamNode"/>.</remarks>
        protected FileStream(XmlNode fileStreamNode, string fileExtension) : base(fileStreamNode)
        {
            string filePath, directoryPath, fileNameWithoutExtension, fileSplitInterval;
            XmlSchema configSchema = DiagnosticsLibrary.Instance.GetConfigSchema();

            filePath = Path.GetFullPath(fileStreamNode.GetAttribute("filePath"));

            directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException(CommonDiagnosticMessages.Error010_1x.FormatMessage(directoryPath));

            fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

            m_filePath = String.Format("{0}\\{1}{2}.{3}", directoryPath, fileNameWithoutExtension, c_fileSplitPlaceholder, fileExtension);

            if (fileStreamNode.HasAttribute("fileSplitInterval"))
                fileSplitInterval = fileStreamNode.GetAttribute("fileSplitInterval");
            else
                fileSplitInterval = configSchema.GetAttributeDefaultValue("fileStreamType", "fileSplitInterval");

            m_fileSplitInterval = (FileSplitInterval)Enum.Parse(typeof(FileSplitInterval), fileSplitInterval);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the current file position by seeking backward from the end of the file stream.
        /// </summary>
        /// <param name="offset">Specifies an offset to seek backward from the end of the file stream.</param>
        protected void SeekBackFromEnd(int offset)
        {
            m_fileStream.Seek(-offset, SeekOrigin.End);
        }

        /// <summary>
        /// Sets the current file position to the end of the file stream.
        /// </summary>
        protected void SeekEnd()
        {
            SeekBackFromEnd(0);
        }

        /// <summary>
        /// Writes a string to the file associated with this <see cref="FileStream"/> instance.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        protected void Write(string s)
        {
            byte[] bytes;

            bytes = UTF8Encoding.UTF8.GetBytes(s);

            m_fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Writes a line terminator to the file associated with this <see cref="FileStream"/> instance.
        /// </summary>
        protected void WriteLine()
        {
            Write("\r\n");
        }

        /// <summary>
        /// Writes a string plus a line terminator to the file associated with this <see cref="FileStream"/> instance.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        protected void WriteLine(string s)
        {
            Write(s);

            WriteLine();
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Returns true if the file associated with this FileStream instance was created as a new file.
        /// </summary>
        protected bool IsNewFile
        {
            get {return(m_isNewFile);}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the effective file path where file splitting has been resolved corresponding to the current date and
        /// time.
        /// </summary>
        /// <returns>Returns the effective file path corresponding to the current date and time.</returns>
        private string GetEffectiveFilePath()
        {
            string fileSplit;
            DateTime now = DateTime.Now;
            int weekNumber;

            switch (m_fileSplitInterval)
            {
                case FileSplitInterval.Day:
                    fileSplit = String.Format("-{0}-{1:00}-{2:00}", now.Year, now.Month, now.Day);

                    break;

                case FileSplitInterval.Hour:
                    fileSplit = String.Format("-{0}-{1:00}-{2:00}-{3:00}", now.Year, now.Month, now.Day, now.Hour);

                    break;

                case FileSplitInterval.Minute:
                    fileSplit = String.Format("-{0}-{1:00}-{2:00}-{3:00}-{4:00}", now.Year, now.Month, now.Day, now.Hour, now.Minute);

                    break;

                case FileSplitInterval.Month:
                    fileSplit = String.Format("-{0}-{1:00}", now.Year, now.Month);

                    break;

                case FileSplitInterval.None:
                    fileSplit = String.Empty;

                    break;

                case FileSplitInterval.Week:
                    weekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                    fileSplit = String.Format("-{0}-Week{1:00}", now.Year, weekNumber);

                    break;

                case FileSplitInterval.Year:
                    fileSplit = String.Format("-{0}", now.Year);

                    break;

                default:
                    throw new UnimplementedCodeBranchException(m_fileSplitInterval);
            }

            return(m_filePath.Replace(c_fileSplitPlaceholder, fileSplit));
        }

        #endregion

        #region Private Types

        /// <summary>
        /// Defines an enumeration for file splitting intervals.
        /// </summary>
        private enum FileSplitInterval
        {
            /// <summary>
            /// File splitting will be done at daily intervals.
            /// </summary>
            Day,

            /// <summary>
            /// File splitting will be done at hourly intervals.
            /// </summary>
            Hour,

            /// <summary>
            /// File splitting will be done at minute intervals.
            /// </summary>
            Minute,

            /// <summary>
            /// File splitting will be done at monthly intervals.
            /// </summary>
            Month,

            /// <summary>
            /// No file splitting, everything goes to the same file.
            /// </summary>
            None,

            /// <summary>
            /// File splitting will be done at weekly intervals.
            /// </summary>
            Week,

            /// <summary>
            /// File splitting will be done at yearly intervals.
            /// </summary>
            Year
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// Specifies the file split placeholder in the value of the <see cref="m_filePath"/> field.
        /// </summary>
        private const string c_fileSplitPlaceholder = "{fileSplit}";

        /// <summary>
        /// Specifies the timeout in milliseconds for file locking operations.
        /// </summary>
        private const int c_lockingTimeout = 30000;

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the absolute path of the file associated with this FileStream instance. The value contains a file
        /// split placeholder (<see cref="c_fileSplitPlaceholder"/>).
        /// </summary>
        private string m_filePath;

        /// <summary>
        /// Specifies the file split interval associated with this FileStream instance.
        /// </summary>
        private FileSplitInterval m_fileSplitInterval;

        /// <summary>
        /// Specifies the underlying <see cref="System.IO.FileStream"/> instance.
        /// </summary>
        private System.IO.FileStream m_fileStream;

        /// <summary>
        /// Stores the <see cref="IsNewFile"/> property.
        /// </summary>
        private bool m_isNewFile;

        #endregion
    }
}
