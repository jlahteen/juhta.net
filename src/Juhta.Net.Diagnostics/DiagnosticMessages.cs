
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using Juhta.Net.Common;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// A static class that defines diagnostic messages for Diagnostics Library.
    /// </summary>
    internal static class DiagnosticMessages
    {
        #region Internal Constants

        /// <summary>
        /// Gets the DiagnosticMessageFactory instance for creating diagnostic messages.
        /// </summary>
        internal static readonly DiagnosticMessageFactory DiagnosticMessageFactory = new DiagnosticMessageFactory(ProductLibraryType.Diagnostics);

        /// <summary>
        /// An error occurred when the method '{0}' was invoked on the current trace writer: {1}
        /// </summary>
        internal static readonly ErrorMessage Error001_2x = DiagnosticMessageFactory.CreateErrorMessage("An error occurred when the method '{0}' was invoked on the current trace writer: {1}");

        /// <summary>
        /// An error occurred when the property '{0}' (get) was invoked on the current trace writer: {1}
        /// </summary>
        internal static readonly ErrorMessage Error002_2x = DiagnosticMessageFactory.CreateErrorMessage("An error occurred when the property '{0}' (get) was invoked on the current trace writer: {1}");

        /// <summary>
        /// An error occurred when the property '{0}' (set) was invoked on the current trace writer: {1}
        /// </summary>
        internal static readonly ErrorMessage Error003_2x = DiagnosticMessageFactory.CreateErrorMessage("An error occurred when the property '{0}' (set) was invoked on the current trace writer: {1}");

        /// <summary>
        /// Event logger thread had to terminate due to an unexpected error: {0}
        /// </summary>
        internal static readonly ErrorMessage Error004_1x = DiagnosticMessageFactory.CreateErrorMessage("Event logger thread had to terminate due to an unexpected error: {0}");

        /// <summary>
        /// Trace directory '{0}' does not exist.
        /// </summary>
        internal static readonly ErrorMessage Error005_1x = DiagnosticMessageFactory.CreateErrorMessage("Trace directory '{0}' does not exist.");

        /// <summary>
        /// An error occurred when a diagnostic event was logged: {0}
        /// </summary>
        internal static readonly ErrorMessage Error006_1x = DiagnosticMessageFactory.CreateErrorMessage("An error occurred when a diagnostic event was logged: {0}");

        /// <summary>
        /// An error occurred when a diagnostic event was written to the event stream '{0}': {1}
        /// </summary>
        internal static readonly ErrorMessage Error007_2x = DiagnosticMessageFactory.CreateErrorMessage("An error occurred when a diagnostic event was written to the event stream '{0}': {1}");

        /// <summary>
        /// An unexpected error occurred when a diagnostic event was written to the event streams: {0}
        /// </summary>
        internal static readonly ErrorMessage Error008_1x = DiagnosticMessageFactory.CreateErrorMessage("An unexpected error occurred when a diagnostic event was written to the event streams: {0}");

        #endregion
    }
}
