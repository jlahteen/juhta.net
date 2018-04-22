
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Juhta.Net.Validators
{
    /// <summary>
    /// Defines an abstract base class for classes validating directory or file paths.
    /// </summary>
    public abstract class PathValidator
    {
        #region Protected Methods

        /// <summary>
        /// Checks whether a specified value is a valid directory name.
        /// </summary>
        /// <param name="value">Specifies a value to be checked.</param>
        /// <returns>Returns true if the specified value is a valid directory name, otherwise false.</returns>
        protected bool IsValidDirectoryName(string value)
        {
            if (value == "." || value == "..")
                return(true);
            else
                return(IsValidFileName(value));
        }

        /// <summary>
        /// Checks whether a specified value is a valid file name.
        /// </summary>
        /// <param name="value">Specifies a value to be checked.</param>
        /// <returns>Returns true if the specified value is a valid file name, otherwise false.</returns>
        protected bool IsValidFileName(string value)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

            if (Char.IsWhiteSpace(value[value.Length - 1]) || value[value.Length - 1] == '.')
                return(false);

            for (int i = 0; i < value.Length; i++)
                if (Array.Exists<char>(invalidFileNameChars, x => x == value[i]))
                    return(false);

            return(true);
        }

        protected bool IsValidPath(string value, PathType pathType)
        {
            int index = 0;
            string[] parts = value.Split(new char[]{Path.DirectorySeparatorChar}, StringSplitOptions.None);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (Regex.IsMatch(parts[0], "^[A-Za-z]:$"))
                    index++;
            }

            for (; index < parts.Length; index++)
                if (index < parts.Length - 1)
                {
                    if (!IsValidDirectoryName(parts[index]))
                        return(false);
                }
                else if (pathType == PathType.DirectoryPath)
                {
                    if (!IsValidDirectoryName(parts[index]))
                        return (false);
                }
                else if (pathType == PathType.FilePath)
                {
                    if (!IsValidFileName(parts[index]))
                        return(false);
                }
                else
                    throw new UnimplementedCodeBranchException(pathType);

            return(true);
        }

        #endregion

        #region Protected Types

        /// <summary>
        /// Defines an enumeration for the path types.
        /// </summary>
        protected enum PathType
        {
            /// <summary>
            /// The path is a directory path.
            /// </summary>
            DirectoryPath,

            /// <summary>
            /// The path is a file path.
            /// </summary>
            FilePath
        }

        #endregion
    }
}
