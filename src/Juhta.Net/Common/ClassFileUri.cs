
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

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
            Juhta.Net.Helpers.ArgumentHelper.CheckNotNull(nameof(fileUri), fileUri);

            if (fileUri.StartsWith("file:///"))
                fileUri = fileUri.Substring("file:///".Length);

            if (String.IsNullOrWhiteSpace(fileUri))
                throw new ArgumentException();

            fileUri = Path.GetFullPath(fileUri);

            if (!System.IO.File.Exists(fileUri))
                throw new ArgumentException("", new FileNotFoundException());

            string libraryDirectory = Path.GetDirectoryName(fileUri);

            string libraryFileName = Path.GetFileName(fileUri);



        }

        #endregion
    }
}
