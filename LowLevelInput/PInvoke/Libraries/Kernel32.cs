using System.Security;

namespace LowLevelInput.PInvoke.Libraries
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Kernel32
    {
        public delegate uint GetCurrentThreadIdDelegate();

        public static GetCurrentThreadIdDelegate GetCurrentThreadId =
            WinApi.GetMethod<GetCurrentThreadIdDelegate>("kernel32.dll", "GetCurrentThreadId");
    }
}