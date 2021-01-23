using System;
using System.Runtime.InteropServices;

using LowLevelInput.Hooks;
using LowLevelInput.PInvoke.Types;

namespace LowLevelInput.WindowsHooks
{
    /// <summary>
    /// 
    /// </summary>
    public static class WindowsHookFilter
    {
        /// <summary>
        /// return true if a filter should take action
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public delegate bool WindowsHookFilterEventHandler(VirtualKeyCode key, KeyState state);

        /// <summary>
        /// Occurs when [filter]. Returns true if a filter should take action
        /// </summary>
        public static WindowsHookFilterEventHandler Filter;

        /// <summary>
        /// returns true if an event needs to be filtered
        /// </summary>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        internal static bool InternalFilterEventsHelper(IntPtr wParam, IntPtr lParam)
        {
            if (wParam == IntPtr.Zero || lParam == IntPtr.Zero) return false;

            var events = Filter;

            if (events == null) return false;

            var msg = (WindowsMessage)(uint)wParam.ToInt32();

            switch (msg)
            {

                case WindowsMessage.Keydown:
                case WindowsMessage.Syskeydown:
                    return events.Invoke((VirtualKeyCode)Marshal.ReadInt32(lParam), KeyState.Down);

                case WindowsMessage.Keyup:
                case WindowsMessage.Syskeyup:
                    return events.Invoke((VirtualKeyCode)Marshal.ReadInt32(lParam), KeyState.Up);

                default:
                    if(HelperMethods.TryGetMouseData(wParam, lParam, out VirtualKeyCode key, out KeyState state))
                    {
                        return events.Invoke(key, state);
                    }
                    else
                    {
                        return false;
                    }
            }
        }
    }
}
