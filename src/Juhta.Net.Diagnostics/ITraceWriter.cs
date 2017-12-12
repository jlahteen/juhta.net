
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines an interface for trace writers. A trace writer writes trace data to any storage where it can be
    /// analyzed afterwards. Furthermore, it's totally up to a trace writer how trace data will be formatted when
    /// written to a storage.
    /// </summary>
    public interface ITraceWriter
    {
        #region Methods

        /// <summary>
        /// Closes this ITraceWriter instance. After calling this method, any trace data should not be written through
        /// this instance.
        /// </summary>
        void Close();

        /// <summary>
        /// Decreases the current relative indent for trace output by one.
        /// </summary>
        void DecreaseRelativeIndent();

        /// <summary>
        /// Flushes all cached trace data written by the current thread to the underlying trace data storage.
        /// </summary>
        void Flush();

        /// <summary>
        /// Increases the current relative indent for trace output by one.
        /// </summary>
        void IncreaseRelativeIndent();

        /// <summary>
        /// Opens this ITraceWriter instance for data tracing.
        /// </summary>
        void Open();

        /// <summary>
        /// Writes a string to the underlying trace data storage.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        void Write(string s);

        /// <summary>
        /// Writes a formatted string to the underlying trace data storage.
        /// </summary>
        /// <param name="format">Specifies a string containing zero or more format items.</param>
        /// <param name="args">Specifies an array of objects to format.</param>
        void Write(string format, params object[] args);

        /// <summary>
        /// Writes a block end to the underlying trace data storage.
        /// </summary>
        void WriteBlockEnd();

        /// <summary>
        /// Writes a block start to the underlying trace data storage.
        /// </summary>
        void WriteBlockStart();

        /// <summary>
        /// Writes a block start to the underlying trace data storage.
        /// </summary>
        /// <param name="description">Specifies a description for a block to start.</param>
        void WriteBlockStart(string description);

        /// <summary>
        /// Writes a constructor end to the underlying trace data storage. The constructor end will be written for the
        /// latest constructor start.
        /// </summary>
        void WriteConstructorEnd();

        /// <summary>
        /// Writes a constructor end to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        void WriteConstructorEnd(string className);

        /// <summary>
        /// Writes a constructor start to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        void WriteConstructorStart(string className);

        /// <summary>
        /// Writes an exception to the underlying trace data storage.
        /// </summary>
        /// <param name="exception">Specifies an exception.</param>
        void WriteException(Exception exception);

        /// <summary>
        /// Writes a get-property end to the underlying trace data storage. The get-property end will be written for
        /// the latest get-property start.
        /// </summary>
        void WriteGetPropertyEnd();

        /// <summary>
        /// Writes a get-property end to the underlying trace data storage. The get-property end will be written for
        /// the latest get-property start.
        /// </summary>
        /// <param name="returnValue">Specifies the return value of the get-property.</param>
        void WriteGetPropertyEnd(object returnValue);

        /// <summary>
        /// Writes a get-property end to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        void WriteGetPropertyEnd(string className, string propertyName);

        /// <summary>
        /// Writes a get-property end to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        /// <param name="returnValue">Specifies the return value of the get-property.</param>
        void WriteGetPropertyEnd(string className, string propertyName, object returnValue);

        /// <summary>
        /// Writes a get-property start to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        void WriteGetPropertyStart(string className, string propertyName);

        /// <summary>
        /// Writes a labeled message to the underlying trace data storage.
        /// </summary>
        /// <param name="label">Specifies a label.</param>
        /// <param name="message">Specifies a message.</param>
        void WriteLabeledMessage(string label, string message);

        /// <summary>
        /// Writes a labeled message to the underlying trace data storage.
        /// </summary>
        /// <param name="label">Specifies a label.</param>
        /// <param name="messageFormat">Specifies a message format.</param>
        /// <param name="args">Specifies an array of objects to format.</param>
        void WriteLabeledMessage(string label, string messageFormat, params object[] args);

        /// <summary>
        /// Writes a line terminator to the underlying trace data storage.
        /// </summary>
        void WriteLine();

        /// <summary>
        /// Writes a string with a line terminator to the underlying trace data storage.
        /// </summary>
        /// <param name="s">Specifies a string.</param>
        void WriteLine(string s);

        /// <summary>
        /// Writes a formatted string with a line terminator to the underlying trace data storage.
        /// </summary>
        /// <param name="format">Specifies a string containing zero or more format items.</param>
        /// <param name="args">Specifies an array of objects to format.</param>
        void WriteLine(string format, params object[] args);

        /// <summary>
        /// Writes a method end to the underlying trace data storage. The method end will be written for the latest
        /// method start.
        /// </summary>
        void WriteMethodEnd();

        /// <summary>
        /// Writes a method end to the underlying trace data storage. The method end will be written for the latest
        /// method start.
        /// </summary>
        /// <param name="returnValue">Specifies the return value of the method.</param>
        void WriteMethodEnd(object returnValue);

        /// <summary>
        /// Writes a method end to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="methodName">Specifies a method name.</param>
        void WriteMethodEnd(string className, string methodName);

        /// <summary>
        /// Writes a method end to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="methodName">Specifies a method name.</param>
        /// <param name="returnValue">Specifies the return value of the method.</param>
        void WriteMethodEnd(string className, string methodName, object returnValue);

        /// <summary>
        /// Writes a method start to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="methodName">Specifies a method name.</param>
        void WriteMethodStart(string className, string methodName);

        /// <summary>
        /// Writes an object graph to the underlying trace data storage.
        /// </summary>
        /// <param name="objectID">Specifies an object identifier, e.g. a variable or property name.</param>
        /// <param name="object">Specifies an object.</param>
        void WriteObjectGraph(string objectID, object @object);

        /// <summary>
        /// Writes an operation end to the underlying trace data storage. The operation end will be written for the
        /// latest operation start.
        /// </summary>
        void WriteOperationEnd();

        /// <summary>
        /// Writes an operation end to the underlying trace data storage.
        /// </summary>
        /// <param name="description">Specifies the description of an operation that ends.</param>
        void WriteOperationEnd(string description);

        /// <summary>
        /// Writes an operation start to the underlying trace data storage.
        /// </summary>
        /// <param name="description">Specifies a description for an operation that starts.</param>
        void WriteOperationStart(string description);

        /// <summary>
        /// Writes a set-property end to the underlying trace data storage. The set-property end will be written for
        /// the latest set-property start.
        /// </summary>
        void WriteSetPropertyEnd();

        /// <summary>
        /// Writes a set-property end to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        void WriteSetPropertyEnd(string className, string propertyName);

        /// <summary>
        /// Writes a set-property start to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        void WriteSetPropertyStart(string className, string propertyName);

        /// <summary>
        /// Writes a set-property start to the underlying trace data storage.
        /// </summary>
        /// <param name="className">Specifies a class name.</param>
        /// <param name="propertyName">Specifies a property name.</param>
        /// <param name="newValue">Specifies a new value for the property.</param>
        void WriteSetPropertyStart(string className, string propertyName, object newValue);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current absolute indent for trace output.
        /// </summary>
        int AbsoluteIndent {get;}

        /// <summary>
        /// Gets a boolean value determining whether this ITraceWriter instance is thread-safe. If it's not, access to
        /// its methods will be synchronized.
        /// </summary>
        /// <remarks>Thread-safety is not required on the methods <see cref="Open"/> and <see cref="Close"/>.</remarks>
        bool IsThreadSafe {get;}

        /// <summary>
        /// Gets or sets the current relative indent for trace output.
        /// </summary>
        int RelativeIndent {get; set;}

        #endregion
    }
}
