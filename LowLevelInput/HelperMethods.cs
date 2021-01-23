using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using LowLevelInput.Hooks;
using LowLevelInput.PInvoke.Types;

namespace LowLevelInput
{
    internal static class HelperMethods
    {
        public static bool TryGetMouseData(IntPtr wParam, IntPtr lParam, out VirtualKeyCode key, out KeyState state)
        {
            var msg = (WindowsMessage)(uint)wParam.ToInt32();

            int mouseData = 0;

            switch (msg)
            {
                case WindowsMessage.Lbuttondblclk:
                case WindowsMessage.Nclbuttondblclk:
                case WindowsMessage.Lbuttondown:
                case WindowsMessage.Nclbuttondown:
                    key = VirtualKeyCode.Lbutton;
                    state = KeyState.Down;
                    return true;
                case WindowsMessage.Lbuttonup:
                case WindowsMessage.Nclbuttonup:
                    key = VirtualKeyCode.Lbutton;
                    state = KeyState.Up;
                    return true;
                case WindowsMessage.Mbuttondown:
                case WindowsMessage.Ncmbuttondown:
                case WindowsMessage.Mbuttondblclk:
                case WindowsMessage.Ncmbuttondblclk:
                    key = VirtualKeyCode.Mbutton;
                    state = KeyState.Down;
                    return true;
                case WindowsMessage.Mbuttonup:
                case WindowsMessage.Ncmbuttonup:
                    key = VirtualKeyCode.Mbutton;
                    state = KeyState.Up;
                    return true;
                case WindowsMessage.Rbuttondblclk:
                case WindowsMessage.Ncrbuttondblclk:
                case WindowsMessage.Rbuttondown:
                case WindowsMessage.Ncrbuttondown:
                    key = VirtualKeyCode.Rbutton;
                    state = KeyState.Down;
                    return true;
                case WindowsMessage.Rbuttonup:
                case WindowsMessage.Ncrbuttonup:
                    key = VirtualKeyCode.Rbutton;
                    state = KeyState.Up;
                    return true;
                case WindowsMessage.Xbuttondblclk:
                case WindowsMessage.Ncxbuttondblclk:
                case WindowsMessage.Xbuttondown:
                case WindowsMessage.Ncxbuttondown:
                    mouseData = Marshal.ReadInt32(lParam, 8);

                    if (HIWORD(mouseData) == 0x1)
                    {
                        key = VirtualKeyCode.Xbutton1;
                        state = KeyState.Down;
                    }
                    else
                    {
                        key = VirtualKeyCode.Xbutton2;
                        state = KeyState.Down;
                    }
                    return true;
                case WindowsMessage.Xbuttonup:
                case WindowsMessage.Ncxbuttonup:
                    mouseData = Marshal.ReadInt32(lParam, 8);

                    if (HIWORD(mouseData) == 0x1)
                    {
                        key = VirtualKeyCode.Xbutton1;
                        state = KeyState.Up;
                    }
                    else
                    {
                        key = VirtualKeyCode.Xbutton2;
                        state = KeyState.Up;
                    }
                    return true;
                case WindowsMessage.Mousewheel:
                case WindowsMessage.Mousehwheel:
                    key = VirtualKeyCode.Scroll;
                    state = KeyState.Pressed;
                    return true;
                case WindowsMessage.Mousemove:
                case WindowsMessage.Ncmousemove:
                    key = VirtualKeyCode.Invalid;
                    state = KeyState.Pressed;
                    return true;
                default:
                    key = VirtualKeyCode.Invalid;
                    state = KeyState.None;
                    return false;
            }
        }

        //public static bool TryGetMouseData(IntPtr wParam, IntPtr lParam, out VirtualKeyCode key, out KeyState state, out int x, out int y)
        //{
        //    var msg = (WindowsMessage)(uint)wParam.ToInt32();

        //    int mouseData = 0;

        //    switch (msg)
        //    {
        //        case WindowsMessage.Lbuttondblclk:
        //        case WindowsMessage.Nclbuttondblclk:
        //        case WindowsMessage.Lbuttondown:
        //        case WindowsMessage.Nclbuttondown:

        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            key = VirtualKeyCode.Lbutton;
        //            state = KeyState.Down;
        //            return true;
        //        case WindowsMessage.Lbuttonup:
        //        case WindowsMessage.Nclbuttonup:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            key = VirtualKeyCode.Lbutton;
        //            state = KeyState.Up;
        //            return true;
        //        case WindowsMessage.Mbuttondown:
        //        case WindowsMessage.Ncmbuttondown:
        //        case WindowsMessage.Mbuttondblclk:
        //        case WindowsMessage.Ncmbuttondblclk:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            key = VirtualKeyCode.Mbutton;
        //            state = KeyState.Down;
        //            return true;
        //        case WindowsMessage.Mbuttonup:
        //        case WindowsMessage.Ncmbuttonup:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            key = VirtualKeyCode.Mbutton;
        //            state = KeyState.Up;
        //            return true;
        //        case WindowsMessage.Rbuttondblclk:
        //        case WindowsMessage.Ncrbuttondblclk:
        //        case WindowsMessage.Rbuttondown:
        //        case WindowsMessage.Ncrbuttondown:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            key = VirtualKeyCode.Rbutton;
        //            state = KeyState.Down;
        //            return true;
        //        case WindowsMessage.Rbuttonup:
        //        case WindowsMessage.Ncrbuttonup:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            key = VirtualKeyCode.Rbutton;
        //            state = KeyState.Up;
        //            return true;
        //        case WindowsMessage.Xbuttondblclk:
        //        case WindowsMessage.Ncxbuttondblclk:
        //        case WindowsMessage.Xbuttondown:
        //        case WindowsMessage.Ncxbuttondown:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            mouseData = Marshal.ReadInt32(lParam, 8);

        //            if (HIWORD(mouseData) == 0x1)
        //            {
        //                key = VirtualKeyCode.Xbutton1;
        //                state = KeyState.Down;
        //            }
        //            else
        //            {
        //                key = VirtualKeyCode.Xbutton2;
        //                state = KeyState.Down;
        //            }
        //            return true;
        //        case WindowsMessage.Xbuttonup:
        //        case WindowsMessage.Ncxbuttonup:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            mouseData = Marshal.ReadInt32(lParam, 8);

        //            if (HIWORD(mouseData) == 0x1)
        //            {
        //                key = VirtualKeyCode.Xbutton1;
        //                state = KeyState.Up;
        //            }
        //            else
        //            {
        //                key = VirtualKeyCode.Xbutton2;
        //                state = KeyState.Up;
        //            }
        //            return true;
        //        case WindowsMessage.Mousewheel:
        //        case WindowsMessage.Mousehwheel:
        //            mouseData = Marshal.ReadInt32(lParam, 8);

        //            x = HIWORD(mouseData);
        //            y = x;

        //            key = VirtualKeyCode.Scroll;
        //            state = KeyState.Pressed;
        //            return true;
        //        case WindowsMessage.Mousemove:
        //        case WindowsMessage.Ncmousemove:
        //            x = Marshal.ReadInt32(lParam);
        //            y = Marshal.ReadInt32(lParam, 4);

        //            key = VirtualKeyCode.Invalid;
        //            state = KeyState.Pressed;
        //            return true;
        //        default:
        //            key = VirtualKeyCode.Invalid;
        //            state = KeyState.None;
        //            x = 0;
        //            y = 0;
        //            return false;
        //    }
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void KbdClearInjectedFlag(IntPtr lParam)
        {
            int flags = Marshal.ReadInt32(lParam + 8);

            flags = SetBit(flags, 1, false);
            flags = SetBit(flags, 4, false);

            Marshal.WriteInt32(lParam + 8, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SetBit(int num, int index, bool value)
        {
            if(value)
            {
                return num | (1 << index);
            }
            else
            {
                return num & ~(1 << index);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort HIWORD(int number)
        {
            return (ushort)(((uint)number >> 16) & 0xFFFF);
        }
    }
}
