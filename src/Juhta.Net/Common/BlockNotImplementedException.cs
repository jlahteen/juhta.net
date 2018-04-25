
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Common
{
    /// <summary>
    /// This exception will be thrown when the execution flow enters a block that is not implemented.
    /// </summary>
    /// <remarks>Generally it's a better idea to throw this kind of exception than to do nothing which can easily lead
    /// to weird or most probably error behaviour in the subsequent execution. This is especially true regarding code
    /// blocks that 'should' never be reached.</remarks>
    public class BlockNotImplementedException : InternalException
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public BlockNotImplementedException() : base(LibraryMessages.Error031.FormatMessage("N/A"))
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="hint">Specifies a hint object.</param>
        public BlockNotImplementedException(object hint) : base(LibraryMessages.Error031.FormatMessage(hint))
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="hint">Specifies a hint string.</param>
        public BlockNotImplementedException(string hint) : base(LibraryMessages.Error031.FormatMessage(hint))
        {}

        #endregion
    }
}
