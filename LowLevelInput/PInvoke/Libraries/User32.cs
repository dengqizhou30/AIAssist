using System;
using System.Security;

using LowLevelInput.PInvoke.Types;

namespace LowLevelInput.PInvoke.Libraries
{
    [SuppressUnmanagedCodeSecurity]
    internal static class User32
    {
        public delegate IntPtr CallNextHookExDelegate(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

        public delegate int GetMessageDelegate(ref Message lpMessage, IntPtr hwnd, uint msgFilterMin,
            uint msgFilterMax);

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public delegate int PostThreadMessageDelegate(uint threadId, uint msg, IntPtr wParam, IntPtr lParam);

        public delegate IntPtr SetWindowsHookExDelegate(int type, IntPtr hookProcedure, IntPtr hModule, uint threadId);

        public delegate int UnhookWindowsHookExDelegate(IntPtr hHook);

        public static CallNextHookExDelegate CallNextHookEx =
            WinApi.GetMethod<CallNextHookExDelegate>("user32.dll", "CallNextHookEx");

        public static GetMessageDelegate GetMessage = WinApi.GetMethod<GetMessageDelegate>("user32.dll", "GetMessageW");

        public static PostThreadMessageDelegate PostThreadMessage =
            WinApi.GetMethod<PostThreadMessageDelegate>("user32.dll", "PostThreadMessageW");

        public static SetWindowsHookExDelegate SetWindowsHookEx =
            WinApi.GetMethod<SetWindowsHookExDelegate>("user32.dll", "SetWindowsHookExW");

        public static UnhookWindowsHookExDelegate UnhookWindowsHookEx =
            WinApi.GetMethod<UnhookWindowsHookExDelegate>("user32.dll", "UnhookWindowsHookEx");
    }
}