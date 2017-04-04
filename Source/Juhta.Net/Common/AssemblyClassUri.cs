
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using System.Linq;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines an abstract base class for classes that can be used for locating assembly classes with URIs. An
    /// assembly class URI is a standard URI whose fragment part specifies a class name in a referenced assembly file.
    /// For instance, file://C:\MyAssemblies\MyAssembly.dll#MyNamespace.MyClass is a valid assembly class URI.
    /// </summary>
    public abstract class AssemblyClassUri : Uri
    {
        #region Public Properties

        /// <summary>
        /// Gets the full name of the assembly class referenced by this AssemblyClassUri instance.
        /// </summary>
        public string FullClassName
        {
            get {return(m_fullClassName);}
        }

        /// <summary>
        /// Returns true if the assembly referenced by this AssemblyClassUri instance has been downloaded to the local
        /// machine or is available without a download on the local machine.
        /// </summary>
        public bool IsAssemblyDownloaded
        {
            get {return(m_localAssemblyPath != null);}
        }

        /// <summary>
        /// Gets the local path of the assembly referenced by this AssemblyClassUri instance.
        /// </summary>
        /// <remarks>An exception will be thrown if the assembly has not been downloaded to the local machine.</remarks>
        /// <seealso cref="IsAssemblyDownloaded"/>
        public string LocalAssemblyPath
        {
            get
            {
                if (m_localAssemblyPath == null)
                    throw new InvalidOperationException(LibraryMessages.Error026.FormatMessage(this.OriginalString));

                return(m_localAssemblyPath);
            }
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="uri">Specifies a URI whose fragment part determines a class name in a referenced assembly
        /// file.</param>
        /// <remarks>The fragment part can start with a dot '.' character in which case the root namespace will be
        /// taken from the assembly file name.</remarks>
        protected AssemblyClassUri(string uri) : base(uri)
        {
            if (this.Fragment.StartsWith("#."))
                m_fullClassName = Path.GetFileNameWithoutExtension(this.LocalPath) + this.Fragment.Substring(1);
            else
                m_fullClassName = this.Fragment.Substring(1);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks whether a specified file path refers to a file on a fixed disk drive.
        /// </summary>
        /// <param name="filePath">Specified a file path.</param>
        /// <returns>Returns true if the specified file path refers to a file on a fixed disk drive, otherwise false.</returns>
        protected static bool IsFixedDiskDrivePath(string filePath)
        {
            var drives =
                from drive in DriveInfo.GetDrives()
                where drive.DriveType == DriveType.Fixed && filePath.StartsWith(drive.Name, StringComparison.OrdinalIgnoreCase)
                select drive;

            return(drives.Count() == 1);
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Stores the <see cref="LocalAssemblyPath"/> property.
        /// </summary>
        protected string m_localAssemblyPath;

        #endregion

        #region Internal Methods

        /// <summary>
        /// Associates a local assembly with this AssemblyClassUri instance.
        /// </summary>
        /// <param name="localAssemblyPath">Specifies an absolute path that must refer to an assembly on a fixed disk
        /// drive.</param>
        internal void SetLocalAssemblyPath(string localAssemblyPath)
        {
            if (m_localAssemblyPath != null)
                throw new InvalidOperationException(LibraryMessages.Error024.GetMessage());

            else if (!IsFixedDiskDrivePath(localAssemblyPath))
                throw new ArgumentException(LibraryMessages.Error029.GetMessage());

            else if (!File.Exists(localAssemblyPath))
                throw new FileNotFoundException(CommonMessages.Error007.FormatMessage(localAssemblyPath));

            m_localAssemblyPath = localAssemblyPath;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="FullClassName"/> property.
        /// </summary>
        private string m_fullClassName;

        #endregion
    }
}
