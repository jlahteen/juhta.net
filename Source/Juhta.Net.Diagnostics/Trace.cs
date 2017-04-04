
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using Juhta.Net.Common;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a static class whose purpose is to simplify software tracing. If tracing is not switched on in the
    /// configuration, the methods of this class have no effect but return immediately. In case of tracing is switched
    /// on, the methods of the class encapsulate tracing through the current trace writer. The current trace writer can
    /// be an instance of any class that implements the <see cref="ITraceWriter"/> interface.
    /// </summary>
    public static class Trace
    {
        #region Public Methods

        /// <summary>
        /// See <see cref="ITraceWriter.DecreaseRelativeIndent"/>.
        /// </summary>
        public static void DecreaseRelativeIndent()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("DecreaseRelativeIndent");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.Flush"/>.
        /// </summary>
        public static void Flush()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("Flush");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.IncreaseRelativeIndent"/>.
        /// </summary>
        public static void IncreaseRelativeIndent()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("IncreaseRelativeIndent");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.Write(string)"/>.
        /// </summary>
        public static void Write(string s)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("Write", s);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.Write(string, object[])"/>.
        /// </summary>
        public static void Write(string format, params object[] args)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("Write", format, args);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteBlockEnd"/>.
        /// </summary>
        public static void WriteBlockEnd()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteBlockEnd");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteBlockStart()"/>.
        /// </summary>
        public static void WriteBlockStart()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteBlockStart");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteBlockStart(string)"/>.
        /// </summary>
        public static void WriteBlockStart(string description)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteBlockStart", description);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteConstructorEnd()"/>.
        /// </summary>
        public static void WriteConstructorEnd()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteConstructorEnd");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteConstructorEnd(string)"/>.
        /// </summary>
        public static void WriteConstructorEnd(string className)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteConstructorEnd", className);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteConstructorStart"/>.
        /// </summary>
        public static void WriteConstructorStart(string className)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteConstructorStart", className);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteException"/>.
        /// </summary>
        public static void WriteException(Exception exception)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteException", exception);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd()"/>.
        /// </summary>
        public static void WriteGetPropertyEnd()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteGetPropertyEnd");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd(object)"/>.
        /// </summary>
        public static void WriteGetPropertyEnd(object returnValue)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteGetPropertyEnd", returnValue);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd(string, string)"/>.
        /// </summary>
        public static void WriteGetPropertyEnd(string className, string propertyName)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteGetPropertyEnd", className, propertyName);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd(string, string, object)"/>.
        /// </summary>
        public static void WriteGetPropertyEnd(string className, string propertyName, object returnValue)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteGetPropertyEnd", className, propertyName, returnValue);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyStart"/>.
        /// </summary>
        public static void WriteGetPropertyStart(string className, string propertyName)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteGetPropertyStart", className, propertyName);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLabeledMessage(string, string)"/>.
        /// </summary>
        public static void WriteLabeledMessage(string label, string message)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteLabeledMessage", label, message);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLabeledMessage(string, string, object[])"/>.
        /// </summary>
        public static void WriteLabeledMessage(string label, string messageFormat, params object[] args)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteLabeledMessage", label, messageFormat, args);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLine()"/>.
        /// </summary>
        public static void WriteLine()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteLine");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLine(string)"/>.
        /// </summary>
        public static void WriteLine(string s)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteLine", s);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLine(string, object[])"/>.
        /// </summary>
        public static void WriteLine(string format, params object[] args)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteLine", format, args);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd()"/>.
        /// </summary>
        public static void WriteMethodEnd()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteMethodEnd");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd(object)"/>.
        /// </summary>
        public static void WriteMethodEnd(object returnValue)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteMethodEnd", returnValue);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd(string, string)"/>.
        /// </summary>
        public static void WriteMethodEnd(string className, string methodName)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteMethodEnd", className, methodName);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd(string, string, object)"/>.
        /// </summary>
        public static void WriteMethodEnd(string className, string methodName, object returnValue)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteMethodEnd", className, methodName, returnValue);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodStart"/>.
        /// </summary>
        public static void WriteMethodStart(string className, string methodName)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteMethodStart", className, methodName);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteObjectGraph"/>.
        /// </summary>
        public static void WriteObjectGraph(string objectID, object @object)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteObjectGraph", objectID, @object);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteOperationEnd()"/>.
        /// </summary>
        public static void WriteOperationEnd()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteOperationEnd");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteOperationEnd(string)"/>.
        /// </summary>
        public static void WriteOperationEnd(string description)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteOperationEnd", description);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteOperationStart"/>.
        /// </summary>
        public static void WriteOperationStart(string description)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteOperationStart", description);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyEnd()"/>.
        /// </summary>
        public static void WriteSetPropertyEnd()
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteSetPropertyEnd");
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyEnd(string, string)"/>.
        /// </summary>
        public static void WriteSetPropertyEnd(string className, string propertyName)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteSetPropertyEnd", className, propertyName);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyStart(string, string)"/>.
        /// </summary>
        public static void WriteSetPropertyStart(string className, string propertyName)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteSetPropertyStart", className, propertyName);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyStart(string, string, object)"/>.
        /// </summary>
        public static void WriteSetPropertyStart(string className, string propertyName, object newValue)
        {
            if (s_tracingOff)
                return;

            InvokeTraceWriterMethod("WriteSetPropertyStart", className, propertyName, newValue);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// See <see cref="ITraceWriter.AbsoluteIndent"/>.
        /// </summary>
        public static int AbsoluteIndent
        {
            get
            {
                object value;

                if (s_tracingOff)
                    return(0);

                if ((value = InvokeTraceWriterGetProperty("AbsoluteIndent")) == null)
                    return(0);
                else
                    return((int)value);
            }
        }

        /// <summary>
        /// Returns true if tracing is currently switched on.
        /// </summary>
        public static bool IsTracingOn
        {
            get {return(s_tracingOn);}
        }

        /// <summary>
        /// See <see cref="ITraceWriter.RelativeIndent"/>.
        /// </summary>
        public static int RelativeIndent
        {
            get
            {
                object value;

                if (s_tracingOff)
                    return(0);

                if ((value = InvokeTraceWriterGetProperty("RelativeIndent")) == null)
                    return(0);
                else
                    return((int)value);
            }

            set
            {
                if (s_tracingOff)
                    return;

                InvokeTraceWriterSetProperty("RelativeIndent", value);
            }
        }

        /// <summary>
        /// Gets the directory where files containing trace data will be stored.
        /// </summary>
        public static string TraceDirectory
        {
            get {return(s_traceDirectory);}
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Closes the trace services.
        /// </summary>
        /// <returns>Returns the number of errors occurred in the closing operation.</returns>
        internal static int Close()
        {
            int errors = 0;

            try
            {
                if (s_traceWriter != null)
                    s_traceWriter.Close();
            }

            catch (Exception ex)
            {
                EventLogger.LogError(CommonDiagnosticMessages.Error013_2x, DiagnosticsLibrary.Instance.LibraryName, ex);

                errors += 1;
            }

            return(errors);
        }

        /// <summary>
        /// Stores the configuration state of the class to a specified ConfigState object.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object.</param>
        internal static void GetConfigState(ConfigState configState)
        {
            configState.TraceDirectory = s_traceDirectory;

            configState.TraceWriter = s_traceWriter;

            configState.TracingOn = s_tracingOn;
        }

        /// <summary>
        /// Initializes the trace services.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object containing a configuration state.</param>
        internal static void Initialize(ConfigState configState)
        {
            s_configStateLock = DiagnosticsLibrary.Instance.GetConfigStateLock();

            SetConfigState(configState);
        }

        /// <summary>
        /// Sets the configuration state of the class based on a specified ConfigState object.
        /// </summary>
        /// <param name="configState">Specifies a ConfigState object.</param>
        internal static void SetConfigState(ConfigState configState)
        {
            s_traceDirectory = configState.TraceDirectory;

            s_traceWriter = configState.TraceWriter;

            s_tracingOn = configState.TracingOn;

            s_tracingOff = !s_tracingOn;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Invokes a specified get-property on the current trace writer.
        /// </summary>
        /// <param name="propertyName">Specifies a property name.</param>
        /// <returns>Returns the value of the specified property, or null, if there is no current trace writer.</returns>
        private static object InvokeTraceWriterGetProperty(string propertyName)
        {
            bool readLockEntered = false;
            Type type;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;

            try
            {
                s_configStateLock.EnterReadLock();

                readLockEntered = true;

                if (s_traceWriter == null)
                    return(null);

                type = s_traceWriter.GetType();

                if (s_traceWriter.IsThreadSafe)
                    return(type.InvokeMember(propertyName, bindingFlags, null, s_traceWriter, null));
                else
                    lock(s_traceWriter)
                    {
                        return(type.InvokeMember(propertyName, bindingFlags, null, s_traceWriter, null));
                    }
            }

            catch (Exception ex)
            {
                EventLogger.LogError(DiagnosticMessages.Error002_2x, propertyName, ex);

                return(null);
            }

            finally
            {
                if (readLockEntered)
                    s_configStateLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Invokes a specified method on the current trace writer.
        /// </summary>
        /// <param name="methodName">Specifies a method name.</param>
        /// <param name="args">Specifies an array of arguments that will be passed to the method.</param>
        private static void InvokeTraceWriterMethod(string methodName, params object[] args)
        {
            object[] concatArgs, initialArgs;
            bool readLockEntered = false;
            Type type;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod;

            try
            {
                if (args == null)
                    concatArgs = null;

                else if (args.Length <= 1)
                    concatArgs = args;

                else if ((initialArgs = args[args.Length - 1] as object[]) != null)
                {
                    concatArgs = new object[args.Length - 1 + initialArgs.Length];

                    Array.Copy(args, concatArgs, args.Length - 1);

                    Array.Copy(initialArgs, 0, concatArgs, args.Length - 1, initialArgs.Length);
                }
                else
                    concatArgs = args;

                s_configStateLock.EnterReadLock();

                readLockEntered = true;

                if (s_traceWriter == null)
                    return;

                type = s_traceWriter.GetType();

                if (s_traceWriter.IsThreadSafe)
                    type.InvokeMember(methodName, bindingFlags, null, s_traceWriter, concatArgs);
                else
                    lock(s_traceWriter)
                    {
                        type.InvokeMember(methodName, bindingFlags, null, s_traceWriter, concatArgs);
                    }
            }

            catch (Exception ex)
            {
                EventLogger.LogError(DiagnosticMessages.Error001_2x, methodName, ex);
            }

            finally
            {
                if (readLockEntered)
                    s_configStateLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Invokes a specified set-property on the current trace writer.
        /// </summary>
        /// <param name="propertyName">Specifies a property name.</param>
        /// <param name="value">Specifies a value that will be set for the property.</param>
        private static void InvokeTraceWriterSetProperty(string propertyName, object value)
        {
            bool readLockEntered = false;
            Type type;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty;

            try
            {
                s_configStateLock.EnterReadLock();

                readLockEntered = true;

                if (s_traceWriter == null)
                    return;

                type = s_traceWriter.GetType();

                if (s_traceWriter.IsThreadSafe)
                    type.InvokeMember(propertyName, bindingFlags, null, s_traceWriter, new object[]{value});
                else
                    lock(s_traceWriter)
                    {
                        type.InvokeMember(propertyName, bindingFlags, null, s_traceWriter, new object[]{value});
                    }
            }

            catch (Exception ex)
            {
                EventLogger.LogError(DiagnosticMessages.Error003_2x, propertyName, ex);
            }

            finally
            {
                if (readLockEntered)
                    s_configStateLock.ExitReadLock();
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies a lock object that manages concurrent access to the configuration state of the library.
        /// </summary>
        private static ReaderWriterLockSlim s_configStateLock;

        /// <summary>
        /// Stores the <see cref="TraceDirectory"/> property.
        /// </summary>
        private static volatile string s_traceDirectory;

        /// <summary>
        /// Specifies the current trace writer.
        /// </summary>
        private static ITraceWriter s_traceWriter;

        /// <summary>
        /// Specifies whether tracing is currently switched off.
        /// </summary>
        private static volatile bool s_tracingOff;

        /// <summary>
        /// Specifies whether tracing is currently switched on.
        /// </summary>
        private static volatile bool s_tracingOn;

        #endregion
    }
}
