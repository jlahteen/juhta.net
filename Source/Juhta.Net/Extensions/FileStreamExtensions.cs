
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Diagnostics;
using System.IO;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// A static class that contains extension methods for the <see cref="FileStream"/> class.
    /// </summary>
    public static class FileStreamExtensions
    {
        #region Public Methods

        /// <summary>
        /// Locks the file specified by this FileStream instance.
        /// </summary>
        /// <param name="fileStream">Specifies the current FileStream instance.</param>
        /// <param name="timeout">Specifies a timeout in milliseconds for the operation.</param>
        public static void Lock(this FileStream fileStream, int timeout)
        {
            Lock(fileStream, 0, fileStream.Length, timeout);
        }

        /// <summary>
        /// Locks a range of the file specified by this FileStream instance.
        /// </summary>
        /// <param name="fileStream">Specifies the current FileStream instance.</param>
        /// <param name="position">Specifies the position of a range to lock.</param>
        /// <param name="length">Specifies the length in bytes of a range to lock.</param>
        /// <param name="timeout">Specifies a timeout in milliseconds for the operation.</param>
        public static void Lock(this FileStream fileStream, long position, long length, int timeout)
        {
            FileSystemWatcher fileSystemWatcher;
            Stopwatch stopwatch = new Stopwatch();
            bool rangeLocked = false;
            int timeoutLeft;

            // Create a file system watcher
            fileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(fileStream.Name), Path.GetFileName(fileStream.Name));

            // Start a stopwatch
            stopwatch.Start();

            // Try to lock the specified range of the file

            while (!rangeLocked)

                try
                {
                    fileStream.Lock(position, length);

                    rangeLocked = true;
                }

                catch (IOException)
                {
                    // The range or a part of it is currently locked by another process

                    // Wait for something to happen on the file unless the operation has timed out

                    if ((timeoutLeft = timeout - (int)stopwatch.ElapsedMilliseconds) <= 0)
                        break;

                    else if (fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All, timeoutLeft).TimedOut)
                        break;
                }

            if (!rangeLocked)
            {
                if (position == 0 && length == fileStream.Length)
                    throw new TimeoutException(LibraryMessages.Error054.FormatMessage(fileStream.Name, timeout));
                else
                    throw new TimeoutException(LibraryMessages.Error055.FormatMessage(position, position + length - 1, fileStream.Name, timeout));
            }
        }

        /// <summary>
        /// Tries to lock the file specified by this FileStream instance.
        /// </summary>
        /// <param name="fileStream">Specifies the current FileStream instance.</param>
        /// <param name="timeout">Specifies a timeout in milliseconds for the operation.</param>
        /// <returns>Returns true if a lock to the file was aqcuired within the specified timeout, otherwise false.</returns>
        public static bool TryLock(this FileStream fileStream, int timeout)
        {
            try
            {
                Lock(fileStream, 0, fileStream.Length, timeout);

                return(true);
            }

            catch (TimeoutException)
            {
                return(false);
            }
        }

        /// <summary>
        /// Tries to lock a range of the file specified by this FileStream instance.
        /// </summary>
        /// <param name="fileStream">Specifies the current FileStream instance.</param>
        /// <param name="position">Specifies the position of a range to lock.</param>
        /// <param name="length">Specifies the length in bytes of a range to lock.</param>
        /// <param name="timeout">Specifies a timeout in milliseconds for the operation.</param>
        /// <returns>Returns true if a lock to the range of the file was aqcuired within the specified timeout,
        /// otherwise false.</returns>
        public static bool TryLock(this FileStream fileStream, long position, long length, int timeout)
        {
            try
            {
                Lock(fileStream, position, length, timeout);

                return(true);
            }

            catch (TimeoutException)
            {
                return(false);
            }
        }

        #endregion
    }
}
