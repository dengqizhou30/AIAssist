using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using LowLevelInput.PInvoke;
using LowLevelInput.PInvoke.Types;
using LowLevelInput.WindowsHooks;

namespace LowLevelInput.Hooks
{
    /// <inheritdoc />
    /// <summary>
    ///     Manage a LowLevelMouseHook
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    public class LowLevelMouseHook : IDisposable
    {
        /// <summary>
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="key">The key.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public delegate void MouseEventHandler(VirtualKeyCode key, KeyState state, int x, int y);

        private readonly object _lockObject;
        private WindowsHook _hook;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LowLevelMouseHook" /> class.
        /// </summary>
        public LowLevelMouseHook()
        {
            _lockObject = new object();
            CaptureMouseMove = false;
            ClearInjectedFlag = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LowLevelMouseHook" /> class.
        /// </summary>
        /// <param name="captureMouseMove">if set to <c>true</c> [capture mouse move].</param>
        public LowLevelMouseHook(bool captureMouseMove)
        {
            _lockObject = new object();
            CaptureMouseMove = captureMouseMove;
            ClearInjectedFlag = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LowLevelMouseHook" /> class.
        /// </summary>
        /// <param name="captureMouseMove">if set to <c>true</c> [capture mouse move].</param>
        /// <param name="clearInjectedFlag">if set to <c>true</c> [clear injected flag].</param>
        public LowLevelMouseHook(bool captureMouseMove, bool clearInjectedFlag)
        {
            _lockObject = new object();
            CaptureMouseMove = captureMouseMove;
            ClearInjectedFlag = clearInjectedFlag;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [capture mouse move].
        /// </summary>
        /// <value><c>true</c> if [capture mouse move]; otherwise, <c>false</c>.</value>
        public bool CaptureMouseMove { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [clear injected flag].
        /// </summary>
        /// <value><c>true</c> if [clear injected flag]; otherwise, <c>false</c>.</value>
        public bool ClearInjectedFlag { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is left mouse button pressed.
        /// </summary>
        /// <value><c>true</c> if this instance is left mouse button pressed; otherwise, <c>false</c>.</value>
        public bool IsLeftMouseButtonPressed { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is middle mouse button pressed.
        /// </summary>
        /// <value><c>true</c> if this instance is middle mouse button pressed; otherwise, <c>false</c>.</value>
        public bool IsMiddleMouseButtonPressed { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is right mouse button pressed.
        /// </summary>
        /// <value><c>true</c> if this instance is right mouse button pressed; otherwise, <c>false</c>.</value>
        public bool IsRightMouseButtonPressed { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is x button1 pressed.
        /// </summary>
        /// <value><c>true</c> if this instance is x button1 pressed; otherwise, <c>false</c>.</value>
        public bool IsXButton1Pressed { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is x button2 pressed.
        /// </summary>
        /// <value><c>true</c> if this instance is x button2 pressed; otherwise, <c>false</c>.</value>
        public bool IsXButton2Pressed { get; private set; }

        /// <summary>
        ///     Occurs when [on mouse event].
        /// </summary>
        public event MouseEventHandler OnMouseEvent;

        /// <summary>
        ///     Finalizes an instance of the <see cref="LowLevelMouseHook" /> class.
        /// </summary>
        ~LowLevelMouseHook()
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

            IsMiddleMouseButtonPressed = false; // important to reset here

            var msg = (WindowsMessage) (uint) wParam.ToInt32();

            int x = Marshal.ReadInt32(lParam);
            int y = Marshal.ReadInt32(lParam, 4);

            int mouseData = Marshal.ReadInt32(lParam, 8);

            if (ClearInjectedFlag)
                Marshal.WriteInt32(lParam, 12, 0);

            switch (msg)
            {
                case WindowsMessage.Lbuttondblclk:
                case WindowsMessage.Nclbuttondblclk:
                    IsLeftMouseButtonPressed = true;

                    InvokeEventListeners(KeyState.Down, VirtualKeyCode.Lbutton, x, y);

                    IsLeftMouseButtonPressed = false;

                    InvokeEventListeners(KeyState.Up, VirtualKeyCode.Lbutton, x, y);
                    break;

                case WindowsMessage.Lbuttondown:
                case WindowsMessage.Nclbuttondown:
                    IsLeftMouseButtonPressed = true;
                    InvokeEventListeners(KeyState.Down, VirtualKeyCode.Lbutton, x, y);
                    break;

                case WindowsMessage.Lbuttonup:
                case WindowsMessage.Nclbuttonup:
                    IsLeftMouseButtonPressed = false;
                    InvokeEventListeners(KeyState.Up, VirtualKeyCode.Lbutton, x, y);
                    break;

                case WindowsMessage.Mbuttondown:
                case WindowsMessage.Ncmbuttondown:
                    IsMiddleMouseButtonPressed = true;
                    InvokeEventListeners(KeyState.Down, VirtualKeyCode.Mbutton, x, y);
                    break;

                case WindowsMessage.Mbuttonup:
                case WindowsMessage.Ncmbuttonup:
                    IsMiddleMouseButtonPressed = false;
                    InvokeEventListeners(KeyState.Up, VirtualKeyCode.Mbutton, x, y);
                    break;

                case WindowsMessage.Mbuttondblclk:
                case WindowsMessage.Ncmbuttondblclk:
                    IsMiddleMouseButtonPressed = true;

                    InvokeEventListeners(KeyState.Down, VirtualKeyCode.Mbutton, x, y);

                    IsMiddleMouseButtonPressed = false;

                    InvokeEventListeners(KeyState.Up, VirtualKeyCode.Mbutton, x, y);
                    break;

                case WindowsMessage.Rbuttondblclk:
                case WindowsMessage.Ncrbuttondblclk:
                    IsRightMouseButtonPressed = true;

                    InvokeEventListeners(KeyState.Down, VirtualKeyCode.Rbutton, x, y);

                    IsRightMouseButtonPressed = false;

                    InvokeEventListeners(KeyState.Up, VirtualKeyCode.Rbutton, x, y);
                    break;

                case WindowsMessage.Rbuttondown:
                case WindowsMessage.Ncrbuttondown:
                    IsRightMouseButtonPressed = true;

                    InvokeEventListeners(KeyState.Down, VirtualKeyCode.Rbutton, x, y);
                    break;

                case WindowsMessage.Rbuttonup:
                case WindowsMessage.Ncrbuttonup:
                    IsRightMouseButtonPressed = false;

                    InvokeEventListeners(KeyState.Up, VirtualKeyCode.Rbutton, x, y);
                    break;

                case WindowsMessage.Xbuttondblclk:
                case WindowsMessage.Ncxbuttondblclk:
                    if (HelperMethods.HIWORD(mouseData) == 0x1)
                    {
                        IsXButton1Pressed = true;

                        InvokeEventListeners(KeyState.Down, VirtualKeyCode.Xbutton1, x, y);

                        IsXButton1Pressed = false;

                        InvokeEventListeners(KeyState.Up, VirtualKeyCode.Xbutton1, x, y);
                    }
                    else
                    {
                        IsXButton2Pressed = true;

                        InvokeEventListeners(KeyState.Down, VirtualKeyCode.Xbutton2, x, y);

                        IsXButton2Pressed = false;

                        InvokeEventListeners(KeyState.Up, VirtualKeyCode.Xbutton2, x, y);
                    }
                    break;

                case WindowsMessage.Xbuttondown:
                case WindowsMessage.Ncxbuttondown:
                    if (HelperMethods.HIWORD(mouseData) == 0x1)
                    {
                        IsXButton1Pressed = true;

                        InvokeEventListeners(KeyState.Down, VirtualKeyCode.Xbutton1, x, y);
                    }
                    else
                    {
                        IsXButton2Pressed = true;

                        InvokeEventListeners(KeyState.Down, VirtualKeyCode.Xbutton2, x, y);
                    }
                    break;

                case WindowsMessage.Xbuttonup:
                case WindowsMessage.Ncxbuttonup:
                    if (HelperMethods.HIWORD(mouseData) == 0x1)
                    {
                        IsXButton1Pressed = false;

                        InvokeEventListeners(KeyState.Up, VirtualKeyCode.Xbutton1, x, y);
                    }
                    else
                    {
                        IsXButton2Pressed = false;

                        InvokeEventListeners(KeyState.Up, VirtualKeyCode.Xbutton2, x, y);
                    }
                    break;

                case WindowsMessage.Mousewheel:
                case WindowsMessage.Mousehwheel:
                    InvokeEventListeners(KeyState.None, VirtualKeyCode.Scroll, HelperMethods.HIWORD(mouseData), HelperMethods.HIWORD(mouseData));

                    break;

                case WindowsMessage.Mousemove:
                case WindowsMessage.Ncmousemove:
                    if (CaptureMouseMove)
                        InvokeEventListeners(KeyState.None, VirtualKeyCode.Invalid, x, y);
                    break;
            }
        }

        private void InvokeEventListeners(KeyState state, VirtualKeyCode key, int x = 0, int y = 0)
        {
            Task.Factory.StartNew(() => OnMouseEvent?.Invoke(key, state, x, y));
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

                _hook = new WindowsHook(WindowsHookType.LowLevelMouse);
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
            if (disposing)
            {
            }

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