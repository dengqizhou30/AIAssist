using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssist
{
    class SytemStatus
    {
        //系统状体信息汇总
        public string processScreen = "未检测";
        public string usbDevic = "未检测";
        public bool imgCheck = false;
        public bool imgShow = false;
        public bool autoTrace = false;
        public bool autoFire = false;
        public bool rButtonHook = false;

        public int currentX = 0;
        public int currentY = 0;

        public string GetSytemStatus() 
        {
            string ret = "";

            ret += "进程屏幕：" + processScreen + "\r\n";
            ret += "USB设备：" + usbDevic + "x:"+ currentX + ",y:" + currentY + "\r\n";
            ret += "图像检测：" + (imgCheck? "已开启":"未开启") + "\r\n";
            ret += "检测结果：" + (imgShow ? "已显示" : "不显示") + "\r\n";
            ret += "自动追踪：" + (autoTrace ? "已开启,numlock键控制" : "未开启") + "\r\n";
            ret += "自动开火：" + (autoFire ? "已开启,numlock键控制" : "未开启") + "\r\n";
            ret += "鼠标右键hook：" + (rButtonHook ? "已开启" : "未开启") + "\r\n";

            return ret;
        }
    }
}
