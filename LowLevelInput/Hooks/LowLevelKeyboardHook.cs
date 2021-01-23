using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using LowLevelInput.PInvoke;
using LowLevelInput.PInvoke.Types;
using LowLevelInput.WindowsHooks;

namespace LowLevelInput.Hooks
{
    /// <inheritdoc />
    /// <summary>
    ///     Manage a LowLevelKeyboardHook
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    public class LowLevelKeyboardHook : IDisposable
    {
        /// <summary>
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="key">The key.</param>
        public delegate void KeyboardEventHandler(VirtualKeyCode key, KeyState state);

        private readonly object _lockObject;
        private WindowsHook _hook;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LowLevelKeyboardHook" /> class.
        /// </summary>
        public LowLevelKeyboardHook()
        {
            _lockObject = new object();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LowLevelKeyboardHook" /> class.
        /// </summary>
        /// <param name="clearInjectedFlag">if set to <c>true</c> [clear injected flag].</param>
        public LowLevelKeyboardHook(bool clearInjectedFlag)
        {
            _lockObject = new object();
            ClearInjectedFlag = clearInjectedFlag;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [clear injected flag].
        /// </summary>
        /// <value><c>true</c> if [clear injected flag]; otherwise, <c>false</c>.</value>
        public bool ClearInjectedFlag { get; set; }

        /// <summary>
        ///     Occurs when [on keyboard event].
        /// </summary>
        public event KeyboardEventHandler OnKeyboardEvent;

        /// <summary>
        ///     Finalizes an instance of the <see cref="LowLevelKeyboardHook" /> class.
        /// </summary>
        ~LowLevelKeyboardHook()
        {
            Dispose(false);
        }

        private void Global_OnProcessExit()
        {
            Dispose();
        }

        private void Global_OnUnhandledException()
        {
            Dispose();
        }

        private void Hook_OnHookCalled(IntPtr wParam, IntPtr lParam)
        {
            if (lParam == IntPtr.Zero) return;

            if (ClearInjectedFlag)
                HelperMethods.KbdClearInjectedFlag(lParam);

            if (OnKeyboardEvent == null) return;

            var msg = (WindowsMessage) (uint) wParam.ToInt32();

            var key = (VirtualKeyCode) Marshal.ReadInt32(lParam);

            switch (msg)
            {
                case WindowsMessage.Keydown:
                case WindowsMessage.Syskeydown:
                    InvokeEventListeners(KeyState.Down, key);
                    break;

                case WindowsMessage.Keyup:
                case WindowsMessage.Syskeyup:
                    InvokeEventListeners(KeyState.Up, key);
                    break;
            }
        }

        private void InvokeEventListeners(KeyState state, VirtualKeyCode key)
        {
            Task.Factory.StartNew(() => OnKeyboardEvent?.Invoke(key, state));
        }

        /// <summary>
        ///     Installs the hook.
        /// </summary>
        /// <returns></returns>
        public bool InstallHook()
        {
            lock (_lockObject)
            {
                if (_hook != null) return false;

                _hook = new WindowsHook(WindowsHookType.LowLevelKeyboard);
            }

            _hook.OnHookCalled += Hook_OnHookCalled;

            if (!_hook.InstallHook())
                WinApi.ThrowWin32Exception("Unknown error while installing hook.");

            Global.OnProcessExit += Global_OnProcessExit;
            Global.OnUnhandledException += Global_OnUnhandledException;

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
                if (_hook == null) return false;

                Global.OnProcessExit -= Global_OnProcessExit;
                Global.OnUnhandledException -= Global_OnUnhandledException;

                _hook.OnHookCalled -= Hook_OnHookCalled;

                _hook.UninstallHook();

                _hook.Dispose();

                _hook = null;

                return true;
            }
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