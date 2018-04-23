
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Diagnostics;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Juhta.Net.Validators
{
    /// <summary>
    /// Defines an abstract base class for validator classes validating directory or file paths.
    /// </summary>
    public abstract class PathValidator
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="errorMessage">Specifies an error message to associate with the instance.</param>
        protected PathValidator(ErrorMessage errorMessage)
        {
            m_errorMessage = errorMessage;
        }

        #endregion

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

        /// <summary>
        /// Checks whether a specified value is a valid path.
        /// </summary>
        /// <param name="value">Specifies a value to be checked.</param>
        /// <param name="pathType">Specifies a path type.</param>
        /// <returns>Returns true if the specified value is a valid path, otherwise false.</returns>
        protected bool IsValidPath(string value, PathType pathType)
        {
            int i = 0;
            string[] parts = value.Split(new char[]{Path.DirectorySeparatorChar}, StringSplitOptions.None);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                if (Regex.IsMatch(parts[0], "^[A-Za-z]:$"))
                    i++;

            for (; i < parts.Length; i++)
                if (i < parts.Length - 1)
                {
                    if (!IsValidDirectoryName(parts[i]))
                        return(false);
                }
                else if (pathType == PathType.DirectoryPath)
                {
                    if (!IsValidDirectoryName(parts[i]))
                        return(false);
                }
                else if (pathType == PathType.FilePath)
                {
                    if (!IsValidFileName(parts[i]))
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

        #region Protected Fields

        /// <summary>
        /// Specifies the error message associated with this validator instance.
        /// </summary>
        protected readonly ErrorMessage m_errorMessage;

        #endregion
    }
}
