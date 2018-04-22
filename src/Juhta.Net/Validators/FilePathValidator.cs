
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Juhta.Net.Validators
{
    public class FilePathValidator : PathValidator, IStringValidator
    {
        #region Public Methods

        public void Validate(string value)
        {
            if (!IsValidPath(value, PathType.FilePath))
                throw new ValidationException("");
        }

        #endregion
    }
}
