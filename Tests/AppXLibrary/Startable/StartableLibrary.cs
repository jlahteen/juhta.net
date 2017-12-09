
using Juhta.Net.LibraryManagement;
using System;

namespace AppXLibrary.Startable
{
    public class StartableLibrary : ILibraryHandle, IStartableLibrary
    {
        #region Public Methods

        public static void Reset()
        {
            s_started = false;

            StopProcessesException = null;

            StopProcessesReturnValue = null;
        }

        public void StartProcesses()
        {
            if (!s_started)
                s_started = true;
            else
                throw new InvalidOperationException();
        }

        public bool StopProcesses()
        {
            if (StopProcessesReturnValue.HasValue)
            {
                s_started = false;

                return(StopProcessesReturnValue.Value);
            }

            else if (StopProcessesException != null)
                throw StopProcessesException;

            if (s_started)
            {
                s_started = false;

                return(true);
            }
            else
                throw new InvalidOperationException();
        }

        #endregion

        #region Public Properties

        public static bool IsStarted => s_started;

        public string LibraryFileName => "AppXLibrary.dll";

        public static Exception StopProcessesException {get; set;}

        public static bool? StopProcessesReturnValue {get; set;}

        #endregion

        #region Private Fields

        private static bool s_started;

        #endregion
    }
}
