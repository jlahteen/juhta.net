
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Common
{
    /// <summary>
    /// This exception will be thrown when the execution flow of software reaches a code branch that should in practise
    /// never be reached.
    /// </summary>
    /// <remarks>Generally it's better to throw this kind of exception than to do nothing which can easily lead to
    /// weird or even error behaviour in the later execution.</remarks>
    public class UnimplementedCodeBranchException : InternalException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public UnimplementedCodeBranchException() : base(LibraryMessages.Error031.FormatMessage("N/A"))
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="hint">Specifies a hint object.</param>
        public UnimplementedCodeBranchException(object hint) : base(LibraryMessages.Error031.FormatMessage(hint))
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="hint">Specifies a hint string.</param>
        public UnimplementedCodeBranchException(string hint) : base(LibraryMessages.Error031.FormatMessage(hint))
        {}

        #endregion
    }
}
