
//
// Juhta.NET, Copyright (c) 2017-2018 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Extensions;
using Juhta.Net.Helpers;
using System;
using System.IO;

namespace Juhta.Net.Common
{
    /// <summary>
    /// <para>Defines a class identifier. A class identifier is a localhost file URI whose fragment part specifies a
    /// class name in a referenced library file.</para>
    /// <para>The fragment part can begin with a '~.' prefix indicating that the library file name specifies the root
    /// namespace of the class. If a library path is not absolute, the path will be filled according to the current
    /// directory.</para>
    /// <para>For example, the following values are valid class identifiers:</para>
    /// <list type="bullet">
    /// <item>
    /// <term><c>MyLibrary.dll#~.MyClass</c></term>
    /// </item>
    /// <item>
    /// <term><c>file:///MyLibrary.dll#~.MyClass</c></term>
    /// </item>
    /// <item>
    /// <term><c>file:///C:\MyDirectory\MyLibrary.dll#~.MyClass</c></term>
    /// </item>
    /// <item>
    /// <term><c>file:///C:\MyDirectory\MyLibrary.dll#MyNamespace.MyClass</c></term>
    /// </item>
    /// </list>
    /// </summary>
    public class ClassId
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="classId">Specifies a class identifier as a string.</param>
        public ClassId(string classId) : this(classId, null)
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="classId">Specifies a class identifier as a string.</param>
        /// <param name="libraryDirectory">Specifies a library directory. Can be null. If non-null,
        /// <paramref name="classId"/> must not contain a library directory part.</param>
        public ClassId(string classId, string libraryDirectory)
        {
            string originalValue, filePath, fragment;
            int indexOf;
            FileInfo fileInfo;

            originalValue = classId;

            ArgumentHelper.CheckNull(nameof(classId), classId);

            // Check that the class identifier is a localhost file URI
            if (classId.StartsWith("file://") && !classId.StartsWith("file:///"))
                throw new ArgumentException(LibraryMessages.Error048.GetMessage());

            // Remove the 'file' scheme if necessary
            else if (classId.StartsWith("file:///"))
                classId = classId.Substring("file:///".Length);

            // Check that no scheme is given
            else if (classId.IndexOf("://") >= 0)
                throw new ArgumentException(LibraryMessages.Error048.GetMessage());

            // Check that the fragment part is present

            indexOf = classId.IndexOf('#');

            if (indexOf < 0 || indexOf == classId.Length - 1)
                throw new ArgumentException(LibraryMessages.Error034.FormatMessage(originalValue));

            // Parse the file path
            filePath = classId.Substring(0, indexOf);

            // Add the library directory to the file path if necessary

            if (libraryDirectory != null)
            {
                if (!String.IsNullOrEmpty(Path.GetDirectoryName(filePath)))
                    throw new ArgumentException(LibraryMessages.Error024.FormatMessage(libraryDirectory, originalValue));

                filePath = libraryDirectory + Path.DirectorySeparatorChar + filePath;
            }

            // Validate the file path

            try
            {
                fileInfo = new FileInfo(filePath);
            }

            catch (Exception ex) when (ex is ArgumentException || ex is PathTooLongException || ex is NotSupportedException)
            {
                throw new ArgumentException(LibraryMessages.Error038.FormatMessage(originalValue), ex);
            }

            // Initialize the library file path
            m_libraryFilePath = Path.GetFullPath(filePath);

            // Initialize the library directory
            m_libraryDirectory = Path.GetDirectoryName(m_libraryFilePath);

            // Initialize the library file name
            m_libraryFileName = Path.GetFileName(m_libraryFilePath);

            // Check the library file extension
            if (Path.GetExtension(m_libraryFileName).ToLower() != ".dll")
                throw new ArgumentException(LibraryMessages.Error035.FormatMessage(originalValue));

            // Parse the fragment
            fragment = classId.Substring(indexOf + 1);

            // Initialize the full class name
            if (fragment.StartsWith("~."))
                m_fullClassName = Path.GetFileNameWithoutExtension(m_libraryFileName) + fragment.Substring(1);
            else
                m_fullClassName = fragment;

            // Validate the full class name
            if (!m_fullClassName.IsRegexMatch(RegexPatterns.FullClassName))
                throw new ArgumentException(LibraryMessages.Error036.FormatMessage(originalValue));

            // Initialize the class namespace and name

            indexOf = m_fullClassName.LastIndexOf('.');

            if (indexOf > 0)
            {
                m_classNamespace = m_fullClassName.Substring(0, indexOf);

                m_className = m_fullClassName.Substring(indexOf + 1);
            }
            else
            {
                m_classNamespace = null;

                m_className = m_fullClassName;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the class associated with this <see cref="ClassId"/> instance.
        /// </summary>
        public string ClassName
        {
            get {return(m_className);}
        }

        /// <summary>
        /// Gets the namespace of the class associated with this <see cref="ClassId"/> instance.
        /// </summary>
        public string ClassNamespace
        {
            get {return(m_classNamespace);}
        }

        /// <summary>
        /// Gets the full name of the class associated with this <see cref="ClassId"/> instance.
        /// </summary>
        public string FullClassName
        {
            get {return(m_fullClassName);}
        }

        /// <summary>
        /// Gets the library directory of the class associated with this <see cref="ClassId"/> instance.
        /// </summary>
        public string LibraryDirectory
        {
            get {return(m_libraryDirectory);}
        }

        /// <summary>
        /// Gets the library file name of the class associated with this <see cref="ClassId"/> instance.
        /// </summary>
        public string LibraryFileName
        {
            get {return(m_libraryFileName);}
        }

        /// <summary>
        /// Gets the library file path of the class associated with this <see cref="ClassId"/> instance.
        /// </summary>
        public string LibraryFilePath
        {
            get {return(m_libraryFilePath);}
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="ClassName"/> property.
        /// </summary>
        private string m_className;

        /// <summary>
        /// Stores the <see cref="ClassNamespace"/> property.
        /// </summary>
        private string m_classNamespace;

        /// <summary>
        /// Stores the <see cref="FullClassName"/> property.
        /// </summary>
        private string m_fullClassName;

        /// <summary>
        /// Stores the <see cref="LibraryDirectory"/> property.
        /// </summary>
        private string m_libraryDirectory;

        /// <summary>
        /// Stores the <see cref="LibraryFileName"/> property.
        /// </summary>
        private string m_libraryFileName;

        /// <summary>
        /// Stores the <see cref="LibraryFilePath"/> property.
        /// </summary>
        private string m_libraryFilePath;

        #endregion
    }
}
