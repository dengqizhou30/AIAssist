using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameAssist
{
    //系统工具类
    class SystemUtil
    {
        // 引入系统dll，根据窗口标题查找窗体
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        // 根据进程名查找窗体区域
        public DetectionRect findProcessWindowRect(string processName)
        {
            DetectionRect srcRect = new DetectionRect();
            //缺省使用使用全屏作为工作屏幕，找不到进程窗体区域就使用缺省值
            srcRect.setValues(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
          
            Process[] allProcess = Process.GetProcesses();
            if (allProcess != null && allProcess.Length > 0)
            {
                foreach (Process process in allProcess) 
                {
                    if (process != null && process.ProcessName != null)
                    {
                        string tmpName = process.ProcessName.ToLower();
                        if (tmpName.StartsWith(processName.ToLower()))
                        {
                            //如果MainWindowHandle不为0，则获取找到的进程的窗口                   
                            IntPtr hWnd = process.MainWindowHandle;
                            if (hWnd != IntPtr.Zero)
                            {
                                RECT winRect = new RECT();
                                GetWindowRect(hWnd, ref winRect);//h为窗口句柄
                                RECT clientRect = new RECT();
                                GetClientRect(hWnd, ref clientRect);//h为窗口句柄
                                srcRect.setValues(winRect.Left + (winRect.Right - winRect.Left - clientRect.Right) / 2,
                                    winRect.Top + (winRect.Bottom - winRect.Top - clientRect.Bottom), clientRect.Right, clientRect.Bottom);
                                
                                break;
                            }
                            
                        }
                    }
                }
            }

            return srcRect;
        }
    }
}
