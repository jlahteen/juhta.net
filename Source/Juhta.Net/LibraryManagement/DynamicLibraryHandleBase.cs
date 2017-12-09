
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System.Threading;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines an abstract base class for dynamic library handles.
    /// </summary>
    public abstract class DynamicLibraryHandleBase : LibraryHandleBase, IDynamicLibrary
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="IDynamicLibrary.GoLive(ILibraryState)"/>.
        /// </summary>
        public virtual void GoLive(ILibraryState libraryState)
        {}

        #endregion

        #region Public Properties

        /// <summary>
        /// See <see cref="IDynamicLibrary.LibraryState"/>.
        /// </summary>
        public ILibraryState LibraryState
        {
            get {return(m_libraryState);}

            set {m_libraryState = value;}
        }

        /// <summary>
        /// See <see cref="IDynamicLibrary.LibraryStateLock"/>.
        /// </summary>
        public ReaderWriterLockSlim LibraryStateLock
        {
            get {return(m_libraryStateLock);}

            set {m_libraryStateLock = value;}
        }

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="libraryFileName">Specifies a value for the <see cref="LibraryHandleBase.LibraryFileName"/>
        /// property.</param>
        protected DynamicLibraryHandleBase(string libraryFileName) : base(libraryFileName)
        {
            m_libraryStateLock = new ReaderWriterLockSlim();
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="LibraryState"/> property.
        /// </summary>
        private volatile ILibraryState m_libraryState;

        /// <summary>
        /// Stores the <see cref="LibraryStateLock"/> property.
        /// </summary>
        private ReaderWriterLockSlim m_libraryStateLock;

        #endregion
    }
}
