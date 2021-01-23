using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using LowLevelInput.Converters;

namespace LowLevelInput.Hooks
{
    /// <inheritdoc />
    /// <summary>
    ///     Provides methods to manage keyboard and mouse hooks
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    public class InputManager : IDisposable
    {
        /// <summary>
        ///     A callback for key state changed events
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The state.</param>
        public delegate void KeyStateChangedEventHandler(VirtualKeyCode key, KeyState state);

        private readonly object _lockObject;

        private LowLevelKeyboardHook _keyboardHook;
        private Dictionary<VirtualKeyCode, List<KeyStateChangedEventHandler>> _keyStateChangedCallbacks;

        private Dictionary<VirtualKeyCode, KeyState> _keyStates;
        private LowLevelMouseHook _mouseHook;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InputManager" /> class.
        /// </summary>
        public InputManager()
        {
            _lockObject = new object();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InputManager" /> class and it's hooks.
        /// </summary>
        /// <param name="captureMouseMove">if set to <c>true</c> [capture mouse move].</param>
        public InputManager(bool captureMouseMove, bool installMouseHook = true)
        {
            _lockObject = new object();

            Initialize(captureMouseMove, false, installMouseHook);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InputManager" /> class and it's hooks.
        /// </summary>
        /// <param name="captureMouseMove">if set to <c>true</c> [capture mouse move].</param>
        /// <param name="clearInjectedFlag">if set to <c>true</c> [clear injected flag].</param>
        public InputManager(bool captureMouseMove, bool clearInjectedFlag, bool installMouseHook = true)
        {
            _lockObject = new object();

            Initialize(captureMouseMove, clearInjectedFlag, installMouseHook);
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [capture mouse move].
        /// </summary>
        /// <value><c>true</c> if [capture mouse move]; otherwise, <c>false</c>.</value>
        public bool CaptureMouseMove
        {
            get
            {
                var tmp = _mouseHook;

                if (tmp == null)
                    throw new InvalidOperationException("The " + nameof(InputManager) + " is not initialized.");

                return tmp.CaptureMouseMove;
            }
            set
            {
                var tmp = _mouseHook;

                if (tmp == null)
                    throw new InvalidOperationException("The " + nameof(InputManager) + " is not initialized.");

                tmp.CaptureMouseMove = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [clear injected flag].
        /// </summary>
        /// <value><c>true</c> if [clear injected flag]; otherwise, <c>false</c>.</value>
        public bool ClearInjectedFlag
        {
            get
            {
                var tmpKeyboard = _keyboardHook;
                var tmpMouse = _mouseHook;

                if (tmpKeyboard == null || tmpMouse == null)
                    throw new InvalidOperationException("The " + nameof(InputManager) + " is not initialized.");

                return tmpKeyboard.ClearInjectedFlag;
            }
            set
            {
                var tmpKeyboard = _keyboardHook;
                var tmpMouse = _mouseHook;

                if (tmpKeyboard == null || tmpMouse == null)
                    throw new InvalidOperationException("The " + nameof(InputManager) + " is not initialized.");

                tmpKeyboard.ClearInjectedFlag = value;
                tmpMouse.ClearInjectedFlag = value;
            }
        }

        /// <summary>
        ///     Occurs when a key on the keyboard changed it's state.
        /// </summary>
        public event LowLevelKeyboardHook.KeyboardEventHandler OnKeyboardEvent;

        /// <summary>
        ///     Occurs when a key on the mouse changed it's state.
        /// </summary>
        public event LowLevelMouseHook.MouseEventHandler OnMouseEvent;

        /// <summary>
        ///     Finalizes an instance of the <see cref="InputManager" /> class.
        /// </summary>
        ~InputManager()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public void Initialize(bool installMouseHook = true)
        {
            Initialize(true, false, installMouseHook);
        }

        /// <summary>
        ///     Initializes the specified capture mouse move.
        /// </summary>
        /// <param name="captureMouseMove">if set to <c>true</c> [capture mouse move].</param>
        /// <param name="clearInjectedFlag">if set to <c>true</c> [clear injected flag].</param>
        /// <exception cref="InvalidOperationException">The " + nameof(InputManager) + " is already initialized.</exception>
        public void Initialize(bool captureMouseMove, bool clearInjectedFlag, bool installMouseHook)
        {
            lock (_lockObject)
            {
                if (IsInitialized)
                    throw new InvalidOperationException("The " + nameof(InputManager) + " is already initialized.");

                _keyStateChangedCallbacks = new Dictionary<VirtualKeyCode, List<KeyStateChangedEventHandler>>();
                _keyStates = new Dictionary<VirtualKeyCode, KeyState>();

                foreach (KeyValuePair<VirtualKeyCode, string> pair in KeyCodeConverter.EnumerateVirtualKeyCodes())
                {
                    _keyStateChangedCallbacks.Add(pair.Key, new List<KeyStateChangedEventHandler>());
                    _keyStates.Add(pair.Key, KeyState.None);
                }

                _keyboardHook = new LowLevelKeyboardHook(clearInjectedFlag);
                _keyboardHook.OnKeyboardEvent += _keyboardHook_OnKeyboardEvent;
                _keyboardHook.InstallHook();

                if (installMouseHook)
                {
                    _mouseHook = new LowLevelMouseHook(captureMouseMove, clearInjectedFlag);
                    _mouseHook.OnMouseEvent += _mouseHook_OnMouseEvent;
                    _mouseHook.InstallHook();
                }

                IsInitialized = true;
            }
        }

        private void _mouseHook_OnMouseEvent(VirtualKeyCode key, KeyState state, int x, int y)
        {
            if (key == VirtualKeyCode.Invalid && !CaptureMouseMove) return;

            state = state == KeyState.Down && _keyStates[key] == KeyState.Down
                ? KeyState.Pressed
                : state;

            _keyStates[key] = state;

            var mouseEvents = OnMouseEvent;

            if (mouseEvents != null)
                Task.Factory.StartNew(() => mouseEvents.Invoke(key, state, x, y));
            
            Task.Factory.StartNew(() =>
            {
                List<KeyStateChangedEventHandler> curCallbacks = _keyStateChangedCallbacks[key];

                if (curCallbacks == null) return;
                if (curCallbacks.Count == 0) return;

                foreach (var callback in curCallbacks)
                    callback(key, state);
            });
        }

        private void _keyboardHook_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
        {
            if (key == VirtualKeyCode.Invalid) return;

            state = state == KeyState.Down && _keyStates[key] == KeyState.Down
                ? KeyState.Pressed
                : state;

            _keyStates[key] = state;

            var keyboardEvents = OnKeyboardEvent;

            if (keyboardEvents != null)
                Task.Factory.StartNew(() => keyboardEvents.Invoke(key, state));

            Task.Factory.StartNew(() =>
            {
                List<KeyStateChangedEventHandler> curCallbacks = _keyStateChangedCallbacks[key];

                if (curCallbacks == null) return;
                if (curCallbacks.Count == 0) return;

                foreach (var callback in curCallbacks)
                    callback(key, state);
            });
        }

        /// <summary>
        ///     Terminates this instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can be
        ///     terminated.
        /// </exception>
        public void Terminate()
        {
            lock (_lockObject)
            {
                if (!IsInitialized)
                    throw new InvalidOperationException("The " + nameof(InputManager) +
                                                        " needs to be initialized before it can be terminated.");

                if (_keyboardHook != null)
                {
                    _keyboardHook.Dispose();
                    _keyboardHook = null;
                }

                if (_mouseHook != null)
                {
                    _mouseHook.Dispose();
                    _mouseHook = null;
                }

                OnKeyboardEvent = null;
                OnMouseEvent = null;

                _keyStateChangedCallbacks = null;
                _keyStates = null;

                IsInitialized = false;
            }
        }

        /// <summary>
        ///     Gets the state of this key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can
        ///     execute this method.
        /// </exception>
        public KeyState GetState(VirtualKeyCode key)
        {
            if (!IsInitialized)
                throw new InvalidOperationException("The " + nameof(InputManager) +
                                                    " needs to be initialized before it can execute this method.");

            if (key == VirtualKeyCode.Invalid) return KeyState.None;

            return _keyStates[key];
        }

        /// <summary>
        ///     Sets the internal state of this key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The state.</param>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can
        ///     execute this method.
        /// </exception>
        public void SetState(VirtualKeyCode key, KeyState state)
        {
            if (!IsInitialized)
                throw new InvalidOperationException("The " + nameof(InputManager) +
                                                    " needs to be initialized before it can execute this method.");

            if (key == VirtualKeyCode.Invalid) return;

            _keyStates[key] = state;
        }

        /// <summary>
        ///     Determines whether the specified key is pressed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///     <c>true</c> if the specified key is pressed; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can
        ///     execute this method.
        /// </exception>
        public bool IsPressed(VirtualKeyCode key)
        {
            if (!IsInitialized)
                throw new InvalidOperationException("The " + nameof(InputManager) +
                                                    " needs to be initialized before it can execute this method.");

            if (key == VirtualKeyCode.Invalid) return false;

            var state = _keyStates[key];

            return state == KeyState.Down || state == KeyState.Pressed;
        }

        /// <summary>
        ///     Determines whether the specified key was pressed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can
        ///     execute this method.
        /// </exception>
        public bool WasPressed(VirtualKeyCode key)
        {
            var state = GetState(key);

            if (state == KeyState.Down || state == KeyState.Pressed)
            {
                SetState(key, KeyState.Up);

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Registers an event (callback) for certain keys.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can
        ///     execute this method.
        /// </exception>
        /// <exception cref="ArgumentException">VirtualKeyCode.INVALID is not supported by this method. - key</exception>
        /// <exception cref="ArgumentNullException">handler</exception>
        public void RegisterEvent(VirtualKeyCode key, KeyStateChangedEventHandler handler)
        {
            if (!IsInitialized)
                throw new InvalidOperationException("The " + nameof(InputManager) +
                                                    " needs to be initialized before it can execute this method.");

            if (key == VirtualKeyCode.Invalid)
                throw new ArgumentException("VirtualKeyCode.INVALID is not supported by this method.", nameof(key));

            if (handler == null) throw new ArgumentNullException(nameof(handler));

            lock (_lockObject)
            {
                _keyStateChangedCallbacks[key].Add(handler);
            }
        }

        /// <summary>
        ///     Unregisters an event (callback) for certain keys.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can
        ///     execute this method.
        /// </exception>
        /// <exception cref="ArgumentException">VirtualKeyCode.INVALID is not supported by this method. - key</exception>
        /// <exception cref="ArgumentNullException">handler</exception>
        public bool UnregisterEvent(VirtualKeyCode key, KeyStateChangedEventHandler handler)
        {
            if (!IsInitialized)
                throw new InvalidOperationException("The " + nameof(InputManager) +
                                                    " needs to be initialized before it can execute this method.");

            if (key == VirtualKeyCode.Invalid)
                throw new ArgumentException("VirtualKeyCode.INVALID is not supported by this method.", nameof(key));

            if (handler == null) throw new ArgumentNullException(nameof(handler));

            lock (_lockObject)
            {
                return _keyStateChangedCallbacks[key].Remove(handler);
            }
        }

        /// <summary>
        ///     Waits until a given event on a key occurs.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The state. KeyState.None indicates any state</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     The " + nameof(InputManager) + " needs to be initialized before it can
        ///     execute this method.
        /// </exception>
        /// <exception cref="ArgumentException">VirtualKeyCode.INVALID is not supported by this method. - key</exception>
        public bool WaitForEvent(VirtualKeyCode key, KeyState state = KeyState.None, int timeout = -1)
        {
            if (!IsInitialized)
                throw new InvalidOperationException("The " + nameof(InputManager) +
                                                    " needs to be initialized before it can execute this method.");

            if (key == VirtualKeyCode.Invalid)
                throw new ArgumentException("VirtualKeyCode.INVALID is not supported by this method.", nameof(key));

            var threadLock = new object();

            KeyStateChangedEventHandler handler = (curKey, curState) =>
            {
                if (curKey != key) return;

                if (curState != state && state != KeyState.None) return;

                if (!Monitor.TryEnter(threadLock)) return;

                // someone else has the lock
                Monitor.PulseAll(threadLock);
                Monitor.Exit(threadLock);
            };

            bool result;

            RegisterEvent(key, handler);

            Monitor.Enter(threadLock);

            if (timeout < 0)
            {
                Monitor.Wait(threadLock);
                result = true;
            }
            else
            {
                result = Monitor.Wait(threadLock, timeout);
            }

            UnregisterEvent(key, handler);

            return result;
        }

        /// <summary>
        /// Gets the hotkey.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">The " + nameof(InputManager) +
        ///                                                     " needs to be initialized before it can execute this method.</exception>
        public VirtualKeyCode GetHotkey(int timeout = -1)
        {
            if (!IsInitialized)
                throw new InvalidOperationException("The " + nameof(InputManager) +
                                                    " needs to be initialized before it can execute this method.");

            var threadLock = new object();
            VirtualKeyCode hotkey = VirtualKeyCode.Invalid;

            LowLevelMouseHook.MouseEventHandler mouseEventHandler = (VirtualKeyCode key, KeyState state, int x, int y) =>
            {
                if (state != KeyState.Down) return;

                hotkey = key;

                if (!Monitor.TryEnter(threadLock)) return;

                // someone else has the lock
                Monitor.PulseAll(threadLock);
                Monitor.Exit(threadLock);
            };
            LowLevelKeyboardHook.KeyboardEventHandler keyboardEventHandler = (VirtualKeyCode key, KeyState state) =>
            {
                if (state != KeyState.Down) return;

                hotkey = key;

                if (!Monitor.TryEnter(threadLock)) return;

                // someone else has the lock
                Monitor.PulseAll(threadLock);
                Monitor.Exit(threadLock);
            };

            this.OnMouseEvent += mouseEventHandler;
            this.OnKeyboardEvent += keyboardEventHandler;

            bool result;
            
            Monitor.Enter(threadLock);

            if (timeout < 0)
            {
                Monitor.Wait(threadLock);
                result = true;
            }
            else
            {
                result = Monitor.Wait(threadLock, timeout);
            }

            this.OnMouseEvent -= mouseEventHandler;
            this.OnKeyboardEvent -= keyboardEventHandler;

            return hotkey;
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

            try
            {
                if (IsInitialized) Terminate();
            }
            catch
            {
                // NotInitialized
            }


            _disposedValue = true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}