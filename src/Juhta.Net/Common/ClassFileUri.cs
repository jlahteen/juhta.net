
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Helpers;
using Juhta.Net.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Juhta.Net.Common
{
    /// <summary>
    /// todo
    /// </summary>
    public class ClassFileUri
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="fileUri"></param>
        public ClassFileUri(string fileUri)
        {
            int fragmentPosition;
            string fragment, filePath;
            FilePathValidator filePathValidator = new FilePathValidator();

            ArgumentHelper.CheckNotNull(nameof(fileUri), fileUri);

            if (fileUri.StartsWith("file://") && !fileUri.StartsWith("file:///"))
                throw new ArgumentException();

            else if (fileUri.StartsWith("file:///"))
                fileUri = fileUri.Substring("file:///".Length);

            fragmentPosition = fileUri.IndexOf('#');

            if (fragmentPosition <= 0 || fragmentPosition == fileUri.Length - 1)
                throw new ArgumentException();

            try
            {

                fragment = fileUri.Substring(fragmentPosition + 1);

                filePath = fileUri.Substring(0, fragmentPosition);

                filePathValidator.Validate(filePath);

                filePath = Path.GetFullPath(fileUri);

                m_libaryDirectory = Path.GetDirectoryName(filePath);

                m_libaryFileName = Path.GetFileName(filePath);

                if (fragment.StartsWith("."))
                    m_fullClassName = Path.GetFileNameWithoutExtension(filePath) + fragment;
                else
                    m_fullClassName = fragment;

                m_classNamespace = m_fullClassName.Substring(0, m_fullClassName.LastIndexOf('.'));

                m_className = m_fullClassName.Substring(m_fullClassName.LastIndexOf('.'));

                //RegexValidator.ValidateFullClassName(m_fullClassName);
            }

            catch (Validators.ValidationException ex)
            {
                throw new ArgumentException("", ex);
            }
        }

        #endregion

        #region Private Fields

        private string m_className;

        private string m_classNamespace;

        private string m_fullClassName;

        private string m_libaryDirectory;

        private string m_libaryFileName;

        #endregion
    }
}
