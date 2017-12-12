
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Juhta.Net.Extensions;

namespace Juhta.Net.Diagnostics
{
    /// <summary>
    /// Defines a class that implements a built-in trace writer.
    /// </summary>
    internal class TraceWriter : ITraceWriter
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="traceDirectory">Specifies a directory where trace files will be stored.</param>
        public TraceWriter(string traceDirectory)
        {
            m_traceDirectory = traceDirectory;

            m_stopwatch = new Stopwatch();

            m_traceBlocks = new Dictionary<int, TraceBlock>();

            m_traceFiles = new Dictionary<int, TraceFile>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// See <see cref="ITraceWriter.Close"/>.
        /// </summary>
        public void Close()
        {
            foreach (TraceBlock traceBlock in m_traceBlocks.Values)
            {
                traceBlock.Close();

                SaveTraceData(traceBlock);
            }
        }

        /// <summary>
        /// See <see cref="ITraceWriter.DecreaseRelativeIndent"/>.
        /// </summary>
        public void DecreaseRelativeIndent()
        {
            GetTraceBlock().GetLeafElement().DecreaseRelativeIndent();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.Flush"/>.
        /// </summary>
        public void Flush()
        {
            TraceBlock traceBlock = TryGetTraceBlock();

            if (traceBlock == null)
                return;

            SaveTraceData(traceBlock);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.IncreaseRelativeIndent"/>.
        /// </summary>
        public void IncreaseRelativeIndent()
        {
            GetTraceBlock().GetLeafElement().IncreaseRelativeIndent();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.Open"/>.
        /// </summary>
        public void Open()
        {
            m_traceStarted = DateTime.Now;

            m_stopwatch.Start();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.Write(string)"/>.
        /// </summary>
        public void Write(string s)
        {
            GetTraceBlock().GetLeafElement().Write(s);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.Write(string, object[])"/>.
        /// </summary>
        public void Write(string format, params object[] args)
        {
            GetTraceBlock().GetLeafElement().Write(format, args);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteBlockEnd"/>.
        /// </summary>
        public void WriteBlockEnd()
        {
            TraceBlock traceBlock = TryGetTraceBlock();

            if (traceBlock == null)
                return;

            traceBlock.Close();

            SaveTraceData(traceBlock);

            RemoveTraceBlock();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteBlockStart()"/>.
        /// </summary>
        public void WriteBlockStart()
        {
            WriteBlockStart(null);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteBlockStart(string)"/>.
        /// </summary>
        public void WriteBlockStart(string description)
        {
            TraceBlock traceBlock;

            WriteBlockEnd();

            if (description == null)
                traceBlock = new TraceBlock(this);
            else
                traceBlock = new TraceBlock(this, description);

            traceBlock.Open();

            SetTraceBlock(traceBlock);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteConstructorEnd()"/>.
        /// </summary>
        public void WriteConstructorEnd()
        {
            WriteConstructorEnd(null);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteConstructorEnd(string)"/>.
        /// </summary>
        public void WriteConstructorEnd(string className)
        {
            TraceBlock traceBlock = TryGetTraceBlock();
            ConstructorTrace constructorTrace;

            if (traceBlock == null)
                return;

            if (className == null)
                constructorTrace = traceBlock.GetLeafElement().FindElement<ConstructorTrace>();
            else
                constructorTrace = traceBlock.GetLeafElement().FindElement<ConstructorTrace>(ConstructorTrace.BuildDescription(className));

            if (constructorTrace == null)
                return;

            constructorTrace.Close();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteConstructorStart"/>.
        /// </summary>
        public void WriteConstructorStart(string className)
        {
            ConstructorTrace constructorTrace = new ConstructorTrace(GetTraceBlock().GetLeafElement(), className);

            constructorTrace.Open();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteException"/>.
        /// </summary>
        public void WriteException(Exception exception)
        {
            GetTraceBlock().GetLeafElement().WriteException(exception);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd()"/>.
        /// </summary>
        public void WriteGetPropertyEnd()
        {
            WriteGetPropertyEnd(null, null);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd(object)"/>.
        /// </summary>
        public void WriteGetPropertyEnd(object returnValue)
        {
            WriteGetPropertyEnd(null, null, returnValue);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd(string, string)"/>.
        /// </summary>
        public void WriteGetPropertyEnd(string className, string propertyName)
        {
            TraceBlock traceBlock = TryGetTraceBlock();
            GetPropertyTrace getPropertyTrace;

            if (traceBlock == null)
                return;

            if (className == null && propertyName == null)
                getPropertyTrace = traceBlock.GetLeafElement().FindElement<GetPropertyTrace>();
            else
                getPropertyTrace = traceBlock.GetLeafElement().FindElement<GetPropertyTrace>(GetPropertyTrace.BuildDescription(className, propertyName));

            if (getPropertyTrace == null)
                return;

            getPropertyTrace.Close();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyEnd(string, string, object)"/>.
        /// </summary>
        public void WriteGetPropertyEnd(string className, string propertyName, object returnValue)
        {
            TraceBlock traceBlock = TryGetTraceBlock();
            GetPropertyTrace getPropertyTrace;

            if (traceBlock == null)
                return;

            if (className == null && propertyName == null)
                getPropertyTrace = traceBlock.GetLeafElement().FindElement<GetPropertyTrace>();
            else
                getPropertyTrace = traceBlock.GetLeafElement().FindElement<GetPropertyTrace>(GetPropertyTrace.BuildDescription(className, propertyName));

            if (getPropertyTrace == null)
                return;

            getPropertyTrace.RelativeIndent = 1;

            getPropertyTrace.WriteLine("Return Value:");

            getPropertyTrace.RelativeIndent++;

            getPropertyTrace.WriteObjectGraph(getPropertyTrace.ClassName + "." + getPropertyTrace.MemberName, returnValue);

            getPropertyTrace.RelativeIndent--;

            getPropertyTrace.WriteLine("End of Return Value");

            getPropertyTrace.Close();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteGetPropertyStart"/>.
        /// </summary>
        public void WriteGetPropertyStart(string className, string propertyName)
        {
            GetPropertyTrace getPropertyTrace = new GetPropertyTrace(GetTraceBlock().GetLeafElement(), className, propertyName);

            getPropertyTrace.Open();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLabeledMessage(string, string)"/>.
        /// </summary>
        public void WriteLabeledMessage(string label, string message)
        {
            GetTraceBlock().GetLeafElement().WriteLabeledMessage(label, message);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLabeledMessage(string, string, object[])"/>.
        /// </summary>
        public void WriteLabeledMessage(string label, string messageFormat, params object[] args)
        {
            GetTraceBlock().GetLeafElement().WriteLabeledMessage(label, messageFormat, args);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLine()"/>.
        /// </summary>
        public void WriteLine()
        {
            GetTraceBlock().GetLeafElement().WriteLine();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLine(string)"/>.
        /// </summary>
        public void WriteLine(string s)
        {
            GetTraceBlock().GetLeafElement().WriteLine(s);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteLine(string, object[])"/>.
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            GetTraceBlock().GetLeafElement().WriteLine(format, args);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd()"/>.
        /// </summary>
        public void WriteMethodEnd()
        {
            WriteMethodEnd(null, null);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd(object)"/>.
        /// </summary>
        public void WriteMethodEnd(object returnValue)
        {
            WriteMethodEnd(null, null, returnValue);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd(string, string)"/>.
        /// </summary>
        public void WriteMethodEnd(string className, string methodName)
        {
            TraceBlock traceBlock = TryGetTraceBlock();
            MethodTrace methodTrace;

            if (traceBlock == null)
                return;

            if (className == null && methodName == null)
                methodTrace = traceBlock.GetLeafElement().FindElement<MethodTrace>();
            else
                methodTrace = traceBlock.GetLeafElement().FindElement<MethodTrace>(MethodTrace.BuildDescription(className, methodName));

            if (methodTrace == null)
                return;

            methodTrace.Close();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodEnd(string, string, object)"/>.
        /// </summary>
        public void WriteMethodEnd(string className, string methodName, object returnValue)
        {
            TraceBlock traceBlock = TryGetTraceBlock();
            MethodTrace methodTrace;

            if (traceBlock == null)
                return;

            if (className == null && methodName == null)
                methodTrace = traceBlock.GetLeafElement().FindElement<MethodTrace>();
            else
                methodTrace = traceBlock.GetLeafElement().FindElement<MethodTrace>(MethodTrace.BuildDescription(className, methodName));

            if (methodTrace == null)
                return;

            methodTrace.RelativeIndent = 1;

            methodTrace.WriteLine("Return Value:");

            methodTrace.RelativeIndent++;

            methodTrace.WriteObjectGraph("returnValue", returnValue);

            methodTrace.RelativeIndent--;

            methodTrace.WriteLine("End of Return Value");

            methodTrace.Close();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteMethodStart"/>.
        /// </summary>
        public void WriteMethodStart(string className, string methodName)
        {
            MethodTrace methodTrace = new MethodTrace(GetTraceBlock().GetLeafElement(), className, methodName);

            methodTrace.Open();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteObjectGraph"/>.
        /// </summary>
        public void WriteObjectGraph(string objectID, object @object)
        {
            GetTraceBlock().GetLeafElement().WriteObjectGraph(objectID, @object);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteOperationEnd()"/>.
        /// </summary>
        public void WriteOperationEnd()
        {
            WriteOperationEnd(null);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteOperationEnd(string)"/>.
        /// </summary>
        public void WriteOperationEnd(string description)
        {
            TraceBlock traceBlock = TryGetTraceBlock();
            OperationTrace operationTrace;

            if (traceBlock == null)
                return;

            if (description == null)
                operationTrace = traceBlock.GetLeafElement().FindElement<OperationTrace>();
            else
                operationTrace = traceBlock.GetLeafElement().FindElement<OperationTrace>(description);

            if (operationTrace == null)
                return;

            operationTrace.Close();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteOperationStart"/>.
        /// </summary>
        public void WriteOperationStart(string description)
        {
            OperationTrace operationTrace = new OperationTrace(GetTraceBlock().GetLeafElement(), description);

            operationTrace.Open();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyEnd()"/>.
        /// </summary>
        public void WriteSetPropertyEnd()
        {
            WriteSetPropertyEnd(null, null);
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyEnd(string, string)"/>.
        /// </summary>
        public void WriteSetPropertyEnd(string className, string propertyName)
        {
            TraceBlock traceBlock = TryGetTraceBlock();
            SetPropertyTrace setPropertyTrace;

            if (traceBlock == null)
                return;

            if (className == null && propertyName == null)
                setPropertyTrace = traceBlock.GetLeafElement().FindElement<SetPropertyTrace>();
            else
                setPropertyTrace = traceBlock.GetLeafElement().FindElement<SetPropertyTrace>(SetPropertyTrace.BuildDescription(className, propertyName));

            if (setPropertyTrace == null)
                return;

            setPropertyTrace.Close();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyStart(string, string)"/>.
        /// </summary>
        public void WriteSetPropertyStart(string className, string propertyName)
        {
            SetPropertyTrace setPropertyTrace = new SetPropertyTrace(GetTraceBlock().GetLeafElement(), className, propertyName);

            setPropertyTrace.Open();
        }

        /// <summary>
        /// See <see cref="ITraceWriter.WriteSetPropertyStart(string, string, object)"/>.
        /// </summary>
        public void WriteSetPropertyStart(string className, string propertyName, object newValue)
        {
            SetPropertyTrace setPropertyTrace = new SetPropertyTrace(GetTraceBlock().GetLeafElement(), className, propertyName);

            setPropertyTrace.Open();

            setPropertyTrace.WriteLine("New Value:");

            setPropertyTrace.RelativeIndent++;

            setPropertyTrace.WriteObjectGraph("newValue", newValue);

            setPropertyTrace.RelativeIndent--;

            setPropertyTrace.WriteLine("End of New Value");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// See <see cref="ITraceWriter.AbsoluteIndent"/>.
        /// </summary>
        public int AbsoluteIndent
        {
            get {return(GetTraceBlock().GetLeafElement().AbsoluteIndent);}
        }

        /// <summary>
        /// Gets a DateTime structure containing the current timestamp.
        /// </summary>
        public DateTime CurrentTimestamp
        {
            get {return(m_traceStarted.Add(m_stopwatch.Elapsed));}
        }

        /// <summary>
        /// See <see cref="ITraceWriter.RelativeIndent"/>.
        /// </summary>
        public int RelativeIndent
        {
            get {return(GetTraceBlock().GetLeafElement().RelativeIndent);}

            set {GetTraceBlock().GetLeafElement().RelativeIndent = value;}
        }

        /// <summary>
        /// See <see cref="ITraceWriter.IsThreadSafe"/>.
        /// </summary>
        public bool IsThreadSafe
        {
            get {return(true);}
        }

        /// <summary>
        /// Gets a Stopwatch object that measures elapsed time from the moment when this TraceWriter instance has been
        /// opened.
        /// </summary>
        public Stopwatch Stopwatch
        {
            get {return(m_stopwatch);}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the TraceBlock instance associated with the current thread.
        /// </summary>
        /// <returns>Returns the TraceBlock instance associated with the current thread.</returns>
        /// <remarks>This method creates a TraceBlock instance for the current thread if necessary.</remarks>
        private TraceBlock GetTraceBlock()
        {
            TraceBlock traceBlock;

            lock(m_traceBlocks)
            {
                if (!m_traceBlocks.TryGetValue(Thread.CurrentThread.ManagedThreadId, out traceBlock))
                {
                    traceBlock = new TraceBlock(this);

                    m_traceBlocks.Add(Thread.CurrentThread.ManagedThreadId, traceBlock);

                    traceBlock.Open();
                }
            }

            return(traceBlock);
        }

        /// <summary>
        /// Gets the TraceFile instance corresponding to a specified managed thread ID.
        /// </summary>
        /// <param name="threadID">Specifies a managed thread ID.</param>
        /// <returns>Returns the TraceFile instance corresponding to the specified managed thread ID.</returns>
        /// <remarks>This method creates a TraceFile instance for the specified managed thread ID if necessary.</remarks>
        private TraceFile GetTraceFile(int threadID)
        {
            TraceFile traceFile;

            lock(m_traceFiles)
            {
                if (!m_traceFiles.TryGetValue(threadID, out traceFile))
                {
                    traceFile = new TraceFile(m_traceDirectory);

                    m_traceFiles.Add(threadID, traceFile);
                }
            }

            return(traceFile);
        }

        /// <summary>
        /// Removes the TraceBlock instance associated with the current thread if such exists.
        /// </summary>
        private void RemoveTraceBlock()
        {
            lock(m_traceBlocks)
            {
                m_traceBlocks.Remove(Thread.CurrentThread.ManagedThreadId);
            }
        }

        /// <summary>
        /// Saves the trace data stored in a specified TraceBlock object to a thread-specific trace file.
        /// </summary>
        /// <param name="traceBlock">Specifies a TraceBlock object.</param>
        private void SaveTraceData(TraceBlock traceBlock)
        {
            TraceFile traceFile = GetTraceFile(traceBlock.ThreadID);
            StringBuilder fileHeader = null;

            if (!File.Exists(traceFile.FilePath))
            {
                fileHeader = new StringBuilder();

                fileHeader.AppendLine();

                fileHeader.AppendFormat("** Trace Data Written by {0} **", this.GetType().FullName);
                fileHeader.AppendLine();
                fileHeader.AppendLine();

                fileHeader.AppendFormat("{0, -15}: {1}", "Executable Path", Process.GetCurrentProcess().MainModule.FileName);
                fileHeader.AppendLine();

                fileHeader.AppendFormat("{0, -15}: {1}", "Process ID", Process.GetCurrentProcess().Id);
                fileHeader.AppendLine();

                fileHeader.AppendFormat("{0, -15}: {1}", ".NET Thread ID", traceBlock.ThreadID);
                fileHeader.AppendLine();
                fileHeader.AppendLine();

                fileHeader.AppendFormat("{0, -35}", "TIME STAMP");
                fileHeader.AppendFormat("{0, -21}", "ELAPSED FROM");
                fileHeader.AppendFormat("{0, -21}", "ELAPSED FROM");
                fileHeader.AppendFormat("{0, -21}", "ELAPSED FROM");
                fileHeader.AppendLine("TRACE INFORMATION");

                fileHeader.AppendFormat("{0, -35}", "");
                fileHeader.AppendFormat("{0, -21}", "TRACE START");
                fileHeader.AppendFormat("{0, -21}", "BLOCK START");
                fileHeader.AppendFormat("{0, -21}", "LATEST START");
                fileHeader.AppendLine();
                fileHeader.AppendLine();

                traceFile.Write(fileHeader.ToString());
            }

            traceFile.Write(traceBlock.GetTraceData());

            traceBlock.ClearTraceData();
        }

        /// <summary>
        /// Associates a specified TraceBlock object with the current thread.
        /// </summary>
        /// <param name="traceBlock">Specifies a TraceBlock object.</param>
        private void SetTraceBlock(TraceBlock traceBlock)
        {
            lock(m_traceBlocks)
            {
                m_traceBlocks.Add(Thread.CurrentThread.ManagedThreadId, traceBlock);
            }
        }

        /// <summary>
        /// Tries to get the TraceBlock instance associated with the current thread.
        /// </summary>
        /// <returns>Returns the TraceBlock instance associated with the current thread, or null, if such doesn't
        /// exist.</returns>
        private TraceBlock TryGetTraceBlock()
        {
            TraceBlock traceBlock;

            lock(m_traceBlocks)
            {
                m_traceBlocks.TryGetValue(Thread.CurrentThread.ManagedThreadId, out traceBlock);
            }

            return(traceBlock);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Stopwatch"/> property.
        /// </summary>
        private Stopwatch m_stopwatch;

        /// <summary>
        /// Specifies the collection of thread-specific TraceBlock instances.
        /// </summary>
        private Dictionary<int, TraceBlock> m_traceBlocks;

        /// <summary>
        /// Specifies the directory where trace files will be stored.
        /// </summary>
        private string m_traceDirectory;

        /// <summary>
        /// Specifies the collection of thread-specific TraceFile objects where trace data will be written.
        /// </summary>
        private Dictionary<int, TraceFile> m_traceFiles;

        /// <summary>
        /// Specifies the timestamp when tracing has been started.
        /// </summary>
        private DateTime m_traceStarted;

        #endregion
    }
}
