
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using Juhta.Net.Extensions;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a class that can be used for locating assembly classes with file URIs.
    /// </summary>
    public class AssemblyClassFileUri : AssemblyClassUri
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="fileUri">Specifies a file URI whose fragment part determines a class name in a referenced
        /// assembly file.</param>
        /// <remarks>This constructor also accepts an incomplete file URI consisting only of a file name or a relative
        /// file path and the fragment part. For example, MyAssembly.dll#MyNamespace.MyClass is a valid file URI for
        /// this constructor. Furthermore, the fragment part can start with a dot '.' character in which case the root
        /// namespace will be taken from the assembly file name.</remarks>
        public AssemblyClassFileUri(string fileUri) : base(CompleteAndCheckFileUri(fileUri))
        {
            if (IsFixedDiskDrivePath(this.LocalPath))
            {
                if (!File.Exists(this.LocalPath))
                    throw new FileNotFoundException(CommonMessages.Error007.FormatMessage(this.LocalPath));

                m_localAssemblyPath = this.LocalPath;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Completes and checks a specified file URI value.
        /// </summary>
        /// <param name="fileUri">Specifies a file URI value.</param>
        /// <returns>Returns the specified file URI value as a completed URI value.</returns>
        private static string CompleteAndCheckFileUri(string fileUri)
        {
            string fileUriOrig = fileUri;

            if (fileUri == null)
                throw new ArgumentNullException(CommonMessages.Error001.FormatMessage("fileUri"));

            else if (!fileUri.Contains("://"))
            {
                if (!fileUri.Contains(":\\") && !fileUri.StartsWith("\\\\"))
                    fileUri = Environment.CurrentDirectory.EnsureEnd("\\") + fileUri;

                fileUri = Uri.UriSchemeFile + "://" + fileUri;
            }

            if (!fileUri.IsMatch(RegularExpressions.AssemblyClassFileUri))
                throw new ArgumentException(LibraryMessages.Error025.FormatMessage(fileUriOrig));

            return(fileUri);
        }

        #endregion
    }
}
