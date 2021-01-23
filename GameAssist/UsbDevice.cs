using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameAssist
{
    class UsbDevice
    {
        //注意幽灵键鼠的外部dll文件需要注册。
        //要使用管理员身份运行动态库目录下的注册脚本regkml.bat。
        //注意项目生成平台需要设置为x64，因为是64位的库，其他设置会导致程序启动找不到注册的类。

        //由于com方式调用有问题，直接使用dll方式调用,
        //测试只能使用32位库连接设备，64位库连不上
        //申明外部dll
        [DllImport("kmllib.dll")]
        static extern int OpenDevice();

        [DllImport("kmllib.dll")]
        static extern int CheckDevice();

        [DllImport("kmllib.dll")]
        static extern string GetModel();

        [DllImport("kmllib.dll")]
        static extern string GetSN();

        [DllImport("kmllib.dll")]
        static extern string GetVersion();

        [DllImport("kmllib.dll")]
        static extern string GetProductionDate();

        [DllImport("kmllib.dll")]
        static extern int KeyDown(string Key);

        [DllImport("kmllib.dll")]
        static extern int KeyUp(string Key);

        [DllImport("kmllib.dll")]
        static extern int KeyPress(string Key, int Count);

        [DllImport("kmllib.dll")]
        static extern int SimulationPressKey(string Key);

        [DllImport("kmllib.dll")]
        static extern int KeyUpAll();

        [DllImport("kmllib.dll")]
        static extern int GetCapsLock();

        [DllImport("kmllib.dll")]
        static extern int GetNumLock();

        [DllImport("kmllib.dll")]
        static extern int LeftDown();
        [DllImport("kmllib.dll")]
        static extern int LeftUp();

        [DllImport("kmllib.dll")]
        static extern int LeftClick(int Count);

        [DllImport("kmllib.dll")]
        static extern int LeftDoubleClick(int Count);

        [DllImport("kmllib.dll")]
        static extern int MoveTo(int x, int y);

        [DllImport("kmllib.dll")]
        static extern int MoveToR(int x, int y);

        [DllImport("kmllib.dll")]
        static extern int SimulationMove(int x, int y);

        [DllImport("kmllib.dll")]
        static extern int MouseUpAll();

        [DllImport("kmllib.dll")]
        static extern int WheelsMove(int Count);

        //[DllImport("kmllib.dll")]
        //static extern int GetCursorPos(ref int x, ref int y);


        [DllImport("user32")]
        private static extern bool GetCursorPos(out Point lpPoint);
        /// <summary>
        /// 获取光标相对于显示器的位置
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPosition()
        {
            Point showPoint = new Point();
            GetCursorPos(out showPoint);
            return showPoint;
        }


        //usb设备是否能正常工作
        private bool usbDeviceStatus = false;

        //不同的游戏，操作参数不一样
        private string[] processNames = { "TslGame", "TGame" };
        private string processName = "TslGame";

        //是否自动追踪
        public bool autoTrace = false;
        //自动追踪类型，1是持续追踪、2是鼠标右键瞄准触发追踪
        public int autoTraceType = 1;
        public int autoTraceTimeOut = 0; //鼠标右键瞄准触发追踪倒计时，缺省按30秒设置

        //是否自动开火
        public bool autoFire = false;
        //枪械类型,1是步枪、2连狙、3单狙
        public int gunType = 1; 
        //最后一次自动开枪的时间
        public long lastFireTicks = DateTime.Now.Ticks;

        public int currentX = 0;
        public int currentY = 0;

        //打开设备并检查设备是否连接，返回1成功
        public int OpenUsbDevice()
        {
            int ret = 0 ;

            ret = OpenDevice();

            if(ret > 0)
            {
                usbDeviceStatus = true;
            }

            return ret;
        }


        //获取设备信息
        public string GetUsbDeviceInfo()
        {
            string ret = "";

            if (CheckDevice() > 0)
            {
                ret += "设备连接成功";
            }

            //打开设备
            //ret += "设备序列号:" + GetSN() + "\r\n";
            //ret += "设备型号:" + GetModel() + "\r\n";
            //ret += "设备固件版本号:" + GetVersion() + "\r\n";
            //ret += "设备出厂日期:" + GetProductionDate() + "\r\n";

            return ret;
        }


        //移动光标到指定位置
        private void MoveMousePos(int x, int y)
        {
            SimulationMove(x, y);

        }

        //步枪开火
        private void Gun1Fire()
        {
            //LeftClick(1);
            LeftDown();
            Thread.Sleep(120);
            LeftUp();
        }

        //连狙开火
        private void Gun2Fire()
        {
            LeftClick(1);
        }

        //单阻开火
        private void Gun3Fire()
        {
            LeftClick(1);
            Thread.Sleep(120);
            //切枪
            KeyPress("3",2);
        }

        //自动开枪
        private bool AutoFire(DetectionResult4Rect detectionResult4Rect, int moveX, int moveY)
        {
            bool fired = false;

            //自动开枪逻辑
            if (usbDeviceStatus && autoFire)
            {

                //判断是否已瞄准对象
                if (moveX <= (detectionResult4Rect.maxConfidencePos.x2 - detectionResult4Rect.maxConfidencePos.x1) / 4 &&
                    moveY <= (detectionResult4Rect.maxConfidencePos.y2 - detectionResult4Rect.maxConfidencePos.y1) / 6)
                {

                    //绝地求生
                    if (this.processName.Equals(processNames[0]))
                    {
                        long curTicks = DateTime.Now.Ticks;
                        //自动开枪间隔3秒以上
                        //如果在600毫秒内都是瞄准的，可以多次开枪
                        /**
                        long intervalTime = (curTicks - lastFireTicks) / 10000;
                        if (intervalTime > 3000 || intervalTime < 600)
                        {
                            lastFireTicks = curTicks;                         
                        }
                        **/
                        switch (this.gunType)
                        {
                            case 1:
                                Gun1Fire();
                                break;
                            case 2:
                            case 3:
                                Gun2Fire();
                                break;
                        }
                    }
                    else if (this.processName.Equals(processNames[1]))
                    {
                        //逆战
                        long curTicks = DateTime.Now.Ticks;
                        //自动开枪间隔3秒以上
                        //如果在600毫秒内都是瞄准的，可以多次开枪
                        long intervalTime = (curTicks - lastFireTicks) / 10000;

                        switch (this.gunType)
                        {
                            case 1:
                                Gun1Fire();
                                break;
                            case 2:
                                Gun2Fire();
                                break;
                            case 3:
                                Gun3Fire();
                                break;
                        }
                       
                    }
                    fired = true;
                }
            }

            return fired;
        }

        //目标跟踪,参数位检测的屏幕区域位置、及检测到的对象位置
        private void TraceObject(DetectionResult4Rect detectionResult4Rect)
        {
            //计算对象的中心点，基于实际检测区域计算
            int objCenterX = detectionResult4Rect.maxConfidencePos.x1 + (detectionResult4Rect.maxConfidencePos.x2 - detectionResult4Rect.maxConfidencePos.x1) / 2;
            int objCenterY = detectionResult4Rect.maxConfidencePos.y1 + (detectionResult4Rect.maxConfidencePos.y2 - detectionResult4Rect.maxConfidencePos.y1) / 3;
            objCenterX = detectionResult4Rect.detectionRect.x + objCenterX;
            objCenterY = detectionResult4Rect.detectionRect.y + objCenterY;

            //计算屏幕中心点，要基于原始窗口区域计算
            int screenCenterX = detectionResult4Rect.rawDetectionRect.x + detectionResult4Rect.rawDetectionRect.w / 2;
            int screenCenterY = detectionResult4Rect.rawDetectionRect.y + detectionResult4Rect.rawDetectionRect.h / 2;

            //对于目前的3D游戏，瞄准就是把屏幕中心点，移动到对象中心点
            //计算出贯标移动的相对位置
            int moveX = objCenterX - screenCenterX;
            int moveY = objCenterY - screenCenterY;

            //如果位置很接近了，则直接退出
            if(Math.Abs(moveX) < 5 && Math.Abs(moveY) < 5)
            {
                return;
            }

            //自动开枪逻辑
            if (autoFire)
            {        
                bool fired = AutoFire(detectionResult4Rect, moveX, moveY);
                //如果正在开火，则后续的自动瞄准操作可以跳过
                if (fired)
                    return;
            }

            //由于是3D游戏，位置是3维坐标，物体越远，移动距离要乘的系数就越大。
            //暂时没有好的方法通过图片检测计算3维坐标，先使用对象的大小初略计算z坐标，但是开镜后的大小暂时无法处理。
            //为了处理太远图片的问题，在按对数log计算一个倍数，实现位置越远倍数不能太大的效果。
            double zParam = 0;
            if(detectionResult4Rect.maxPersonW > 0)
            {
                zParam = (detectionResult4Rect.maxPersonW) / (detectionResult4Rect.maxConfidencePos.x2 - detectionResult4Rect.maxConfidencePos.x1);
                if(zParam > 1)
                    zParam = Math.Log(2 * zParam, 2);
            }

            //再根据x/y的距离作为一个乘积参数，距离越小参数倍数越小，实现位置越近移动距离更小的效果。
            if (moveX != 0 && moveY != 0)
            {
                double zParam2 = Math.Log(Math.Abs(moveX)+ Math.Abs(moveY), 50);
                zParam = zParam * zParam2;
            }


            //另外由于游戏设置了鼠标灵敏度，所以鼠标移动，高根据游戏里面灵敏度设置，乘一个比例。
            //使用模拟移动函数移动鼠标
            Point point = GetCursorPosition();
            
            if (point != null && zParam > 0)
            {
                currentX = point.X;
                currentY = point.Y;

                //如果坐标超出原始检测区域所在范围，则不再移动
                //if (currentX > (detectionResult4Rect.rawDetectionRect.x + detectionResult4Rect.rawDetectionRect.w))
                //   return;
                //if (currentY < detectionResult4Rect.detectionRect.y || currentY > (detectionResult4Rect.detectionRect.y + detectionResult4Rect.detectionRect.h))
                //    return;

                if (this.processName.Equals(processNames[0]))
                {
                    //绝地求生追踪的步长,对应游戏鼠标灵敏度设置，大概为50，
                    //测试发现游戏控制了鼠标移动速度，移动距离和鼠标移动时间时间有关系，
                    //最好把图像检测和鼠标移动任务分开，在单独的线程中执行鼠标移动操作，保障移动鼠标的执行时间。

                    //初略计算3d坐标移动倍数，乘以10避免小数位丢失
                    int multiple = (int)(zParam * 10);
                    int targetX = currentX + multiple * moveX / 30;
                    int targetY = currentY + multiple * moveY / 30;
                    SimulationMove(targetX, targetY);
                }
                else if (this.processName.Equals(processNames[1]))
                {
                    //逆战追踪的步长,对应游戏鼠标灵敏度设置，大概为10，
                    //初略计算3d坐标移动倍数，乘以10避免小数位丢失
                    int multiple = (int)(zParam * 10);
                    int targetX = currentX + multiple * moveX / 50;
                    int targetY = currentY + multiple * moveY / 50;
                    SimulationMove(targetX, targetY);
                }
            }
            
        }

        //设置进程名称
        public void SetProcessName(string name)
        {
            this.processName = name;
        }

        //设置是否进行目标跟踪
        public void IfAutoTrace(bool val)
        {
            this.autoTrace = val;
        }

        //设置是否进行自动开火
        public void IfAutoFire(bool val)
        {
            this.autoFire = val;
        }

        //执行自动鼠键操作
        public void DoAutoAction(DetectionResult4Rect detectionResult4Rect)
        {
            //先检测是否打开了NumLock键，打开了就启动自动开枪
            //目前的设备，获取不到NumLock的键状态
            //int numLock = GetNumLock();
            //if (numLock != 2)
            //    return;

            //检查设备状态
            if (!usbDeviceStatus)
            {
                this.OpenUsbDevice();
            }

            //自动追踪
            if (usbDeviceStatus && autoTrace)               
                if (autoTraceType == 1)
                {
                    //是持续追踪
                    TraceObject(detectionResult4Rect);
                }
                else
                {
                    //鼠标右键瞄准触发追踪
                    if (autoTraceTimeOut > 0)
                        TraceObject(detectionResult4Rect);
                }
        }

    }
}
