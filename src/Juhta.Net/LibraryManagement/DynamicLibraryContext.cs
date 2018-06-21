
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using System;

namespace Juhta.Net.LibraryManagement
{
    /// <summary>
    /// Defines a class that simplifies the implementation of dynamic library services. The class acquires a read-lock
    /// to the state of the dynamic library and provides access both to the handle and state of the library. In other
    /// words, the class saves a developer of a dynamic library from storing the instances of the current handle and
    /// state within the library.
    /// </summary>
    /// <typeparam name="TDynamicLibrary">Specifies a dynamic library type.</typeparam>
    /// <typeparam name="TLibraryState">Specifies a library state type.</typeparam>
    public class DynamicLibraryContext<TDynamicLibrary, TLibraryState> : IDisposable
        where TDynamicLibrary : IDynamicLibrary
        where TLibraryState : ILibraryState
    {
        #region Public Methods

        /// <summary>
        /// Disposes the instance, that is, releases the acquired read-lock to the state of the library.
        /// </summary>
        public void Dispose()
        {
            if (!m_disposed)
                m_dynamicLibrary.LibraryStateLock.ExitReadLock();

            m_disposed = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the dynamic library instance.
        /// </summary>
        public TDynamicLibrary DynamicLibrary
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(CommonMessages.Error015.FormatMessage("DynamicLibrary", this.GetType()));

                return(m_dynamicLibrary);
            }
        }

        /// <summary>
        /// Gets the library state instance.
        /// </summary>
        public TLibraryState LibraryState
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(CommonMessages.Error015.FormatMessage("LibraryState", this.GetType()));

                return(m_libraryState);
            }
        }

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dynamicLibrary">Specifies a dynamic library instance to be associated with the context.</param>
        internal DynamicLibraryContext(TDynamicLibrary dynamicLibrary)
        {
            m_dynamicLibrary = dynamicLibrary;

            m_dynamicLibrary.LibraryStateLock.EnterReadLock();

            m_libraryState = (TLibraryState)m_dynamicLibrary.LibraryState;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies whether this instance has been disposed.
        /// </summary>
        private bool m_disposed;

        /// <summary>
        /// Stores the <see cref="DynamicLibrary"/> property.
        /// </summary>
        private TDynamicLibrary m_dynamicLibrary;

        /// <summary>
        /// Stores the <see cref="LibraryState"/> property.
        /// </summary>
        private TLibraryState m_libraryState;

        #endregion
    }
}
