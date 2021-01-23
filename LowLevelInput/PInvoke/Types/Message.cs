using System;
using System.Runtime.InteropServices;

namespace LowLevelInput.PInvoke.Types
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Message
    {
        public IntPtr Hwnd;
        public IntPtr lParam;
        public uint Msg;
        public uint Time;
        public IntPtr wParam;
        public int X;
        public int Y;
    }
}