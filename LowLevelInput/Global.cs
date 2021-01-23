using System;

namespace LowLevelInput
{
    internal static class Global
    {
        /// <summary>
        /// </summary>
        public delegate void ProcessExitCallback();

        /// <summary>
        /// </summary>
        public delegate void UnhandledExceptionCallback();

        static Global()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        ///     Occurs when [on process exit].
        /// </summary>
        public static event ProcessExitCallback OnProcessExit;

        /// <summary>
        ///     Occurs when [on unhandled exception].
        /// </summary>
        public static event UnhandledExceptionCallback OnUnhandledException;

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            OnProcessExit?.Invoke();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnUnhandledException?.Invoke();
        }
    }
}