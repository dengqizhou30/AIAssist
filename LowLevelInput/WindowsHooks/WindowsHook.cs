using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

using LowLevelInput.PInvoke;
using LowLevelInput.PInvoke.Libraries;
using LowLevelInput.PInvoke.Types;

namespace LowLevelInput.WindowsHooks
{
    /// <inheritdoc />
    /// <summary>
    ///     An generic class to install WindowsHooks
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    public class WindowsHook : IDisposable
    {
        /// <summary>
        /// </summary>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        public delegate void HookCalledEventHandler(IntPtr wParam, IntPtr lParam);

        private static readonly IntPtr MainModuleHandle = Process.GetCurrentProcess().MainModule.BaseAddress;
        private readonly object _lockObject;

        private IntPtr _hookHandler;
        private User32.HookProc _hookProc;
        private Thread _hookThread;
        private uint _hookThreadId;

        // ReSharper disable once UnusedMember.Local
        private WindowsHook()
        {
            _lockObject = new object();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WindowsHook" /> class.
        /// </summary>
        /// <param name="windowsHookType">Type of the windows hook.</param>
        public WindowsHook(WindowsHookType windowsHookType)
        {
            _lockObject = new object();
            WindowsHookType = windowsHookType;
        }

        /// <summary>
        ///     Gets the type of the windows hook.
        /// </summary>
        /// <value>The type of the windows hook.</value>
        public WindowsHookType WindowsHookType { get; }

        /// <summary>
        ///     Finalizes an instance of the <see cref="WindowsHook" /> class.
        /// </summary>
        ~WindowsHook()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Occurs when [on hook called].
        /// </summary>
        public event HookCalledEventHandler OnHookCalled;

        private IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == 0)
            {
                if (WindowsHookFilter.InternalFilterEventsHelper(wParam, lParam))
                {
                    return (IntPtr)(-1);
                }
                else
                {
                    OnHookCalled?.Invoke(wParam, lParam);
                }
            }

            return User32.CallNextHookEx(_hookHandler, nCode, wParam, lParam);
        }

        /// <summary>
        ///     Installs the hook.
        /// </summary>
        /// <returns></returns>
        public bool InstallHook()
        {
            lock (_lockObject)
            {
                if (_hookHandler != IntPtr.Zero) return false;
                if (_hookThreadId != 0) return false;

                _hookThread = new Thread(InitializeHookThread)
                {
                    IsBackground = true
                };

                _hookThread.Start();
            }

            while (_hookThreadId == 0) Thread.Sleep(10);

            if (_hookHandler == IntPtr.Zero)
                WinApi.ThrowWin32Exception("Failed to \"SetWindowsHookEx\"");

            return true;
        }

        /// <summary>
        ///     Uninstalls the hook.
        /// </summary>
        /// <returns></returns>
        public bool UninstallHook()
        {
            lock (_lockObject)
            {
                if (_hookHandler == IntPtr.Zero) return false;
                if (_hookThreadId == 0) return false;

                if (User32.PostThreadMessage(_hookThreadId, (uint) WindowsMessage.Quit, IntPtr.Zero, IntPtr.Zero) != 0)
                    try
                    {
                        _hookThread.Join();
                    }
                    catch
                    {
                        // ignored
                    }

                _hookHandler = IntPtr.Zero;
                _hookThreadId = 0;
                _hookThread = null;

                return true;
            }
        }

        private void InitializeHookThread()
        {
            lock (_lockObject)
            {
                _hookProc = HookProcedure;

                var methodPtr = Marshal.GetFunctionPointerForDelegate(_hookProc);

                _hookHandler = User32.SetWindowsHookEx((int) WindowsHookType, methodPtr, MainModuleHandle, 0);

                _hookThreadId = Kernel32.GetCurrentThreadId();
            }

            if (_hookHandler == IntPtr.Zero) return;

            var msg = new Message();

            while (User32.GetMessage(ref msg, IntPtr.Zero, 0, 0) != 0)
                if (msg.Msg == (uint) WindowsMessage.Quit) break;

            User32.UnhookWindowsHookEx(_hookHandler);
        }

        #region IDisposable Support

        private bool _disposedValue;

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
                OnHookCalled = null;

            UninstallHook();

            _disposedValue = true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}