using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using LowLevelInput.Converters;
using LowLevelInput.Hooks;


namespace GameAssist
{
    public partial class AIAssistForm : Form
    {

        private const int WM_HOTKEY = 0x312; //窗口消息：热键
        private const int WM_CREATE = 0x1; //窗口消息：创建
        private const int WM_DESTROY = 0x2; //窗口消息：销毁

        private const int HotKeyID1 = 1; //热键ID（自定义）
        private const int HotKeyID2 = 2; //热键ID（自定义）
        private const int HotKeyID3 = 3; //热键ID（自定义）

        private void ShowToolTip(string message)
        {
            this.systemTrayNotifyIcon.ShowBalloonTip(5000,"变更", message,ToolTipIcon.Info);
            //new ToolTip().Show(message, this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y, 5000);
        }

        //重载系统方法，用于处理热键
        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            switch (msg.Msg)
            {
                case WM_HOTKEY: //窗口消息：热键
                    int tmpWParam = msg.WParam.ToInt32();
                    //MessageBox.Show("hot key:" + tmpWParam);
                    //ctrl+NumPad0开启自动跟踪
                    //ctrl+enter关闭自动跟踪
                    if (usbDevice != null && systemStatus != null)
                    {
                        if (tmpWParam == HotKeyID1)
                        {                      
                            usbDevice.IfAutoTrace(true);
                            systemStatus.autoTrace = usbDevice.autoTrace;
                            this.textBox_msg.Text = systemStatus.GetSytemStatus();
                            ShowToolTip("已开启自动跟踪");
                        }
                        else if (tmpWParam == HotKeyID2)
                        {
                            usbDevice.IfAutoTrace(false);
                            systemStatus.autoTrace = usbDevice.autoTrace;
                            this.textBox_msg.Text = systemStatus.GetSytemStatus();
                            ShowToolTip("已关闭自动跟踪");
                        }
                        else if (tmpWParam == HotKeyID3)
                        {
                            ShowToolTip("测试热键");
                        }
                    }
                    break;
                case WM_CREATE: //窗口消息：创建
                    SystemHotKey.RegHotKey(this.Handle, HotKeyID1, SystemHotKey.KeyModifiers.Ctrl, Keys.NumPad0);
                    SystemHotKey.RegHotKey(this.Handle, HotKeyID2, SystemHotKey.KeyModifiers.WindowsKey, Keys.NumPad0);
                    SystemHotKey.RegHotKey(this.Handle, HotKeyID3, SystemHotKey.KeyModifiers.Alt, Keys.Add);
                    break;
                case WM_DESTROY: //窗口消息：销毁
                    SystemHotKey.UnRegHotKey(this.Handle, HotKeyID1); //销毁热键
                    SystemHotKey.UnRegHotKey(this.Handle, HotKeyID2); //销毁热键
                    SystemHotKey.UnRegHotKey(this.Handle, HotKeyID3); //销毁热键
                    break;
                default:
                    break;
            }
        }

        //各种控制对象
        KeyboardMouseHook keyboardMouseHook = null;
        ScreenDetection objectDetection = null;
        UsbDevice usbDevice = null;
        SytemStatus systemStatus = null;

        //线程安全的队列
        BlockingCollection<DetectionResult> imgQueue = new BlockingCollection<DetectionResult>(10);
        BlockingCollection<DetectionResult4Rect> posQueue = new BlockingCollection<DetectionResult4Rect>(10);
        
        public AIAssistForm()
        {

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void GameMasterForm_SizeChanged(object sender, EventArgs e)
        {
            // 判断只有最小化时，隐藏窗体
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
            }
        }

        private void systemTrayNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 正常显示窗体
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void GameMasterForm_Load(object sender, EventArgs e)
        {
            objectDetection = new ScreenDetection(this, this.pictureBox1);
            usbDevice = new UsbDevice();
            systemStatus = new SytemStatus();

            keyboardMouseHook = new KeyboardMouseHook(usbDevice, this, this.comboBox_bag1GunType, this.comboBox_bag1ScopeType, this.comboBox_bag2GunType, this.comboBox_bag2ScopeType);

            //缺省启动鼠标右键hook
            systemStatus.keyMouseHook = true;
            keyboardMouseHook.StartHook();

            this.textBox_msg.Text = systemStatus.GetSytemStatus();

            //设置一个缺省的检测位置
            DetectionRect srcRect = new DetectionRect();
            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.h = 900;
            srcRect.w = 900;
            if (srcRect.h > 0 && srcRect.w > 0)
            {
                objectDetection.SetSrcScreenRect(srcRect);
                usbDevice.SetProcessName(comboBox_process.Text);
                systemStatus.processScreen = "" + comboBox_process.Text + ":" + srcRect.x + "," + srcRect.y + "," + srcRect.w + "," + srcRect.h;
                this.textBox_msg.Text = systemStatus.GetSytemStatus();
            }
        }


        private void GameMasterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            keyboardMouseHook.IfShowMsg(false);
            keyboardMouseHook.StopHook();
            backgroundWorker_detection.CancelAsync();
            backgroundWorker_showimg.CancelAsync();
            backgroundWorker_usbdev.CancelAsync();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            objectDetection.IfShowPicture(true);
            systemStatus.imgShow = true;
            this.textBox_msg.Text = systemStatus.GetSytemStatus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(comboBox_process.Text == null || comboBox_process.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请指定游戏进程名称。");
            }
            else
            {
                SystemUtil util = new SystemUtil();
                DetectionRect srcRect = util.findProcessWindowRect(comboBox_process.Text);
                if (srcRect.h > 0 && srcRect.w > 0)
                {
                    objectDetection.SetSrcScreenRect(srcRect);
                    usbDevice.SetProcessName(comboBox_process.Text);
                    systemStatus.processScreen = "" + comboBox_process.Text + ":" + srcRect.x + "," + srcRect.y + "," + srcRect.w + "," + srcRect.h;
                    this.textBox_msg.Text = systemStatus.GetSytemStatus();
                }
                else
                {
                    MessageBox.Show("没找到运行的进程：" + comboBox_process.Text);
                    systemStatus.processScreen = "没找到运行的进程：" + comboBox_process.Text;
                    this.textBox_msg.Text = systemStatus.GetSytemStatus();
                }
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {
            int ret = usbDevice.OpenUsbDevice();
            if(ret == 1)
            {
                string msg = usbDevice.GetUsbDeviceInfo();
                //this.textBox_msg.Text = msg;
                systemStatus.usbDevic = msg;
            }
            else
            {
                systemStatus.usbDevic  = "不能打开设备，请检查设备是否安装。";
            }
            this.textBox_msg.Text = systemStatus.GetSytemStatus();
        }


        //屏幕检测后台线程
        private void backgroundWorker_detection_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;

            //在后台线程中循环检测屏幕对象
            while (!bgWorker.CancellationPending)
            {
                try
                {
                    //objectDetection.CopyScreenTest();
                    DetectionResult detectionResult = objectDetection.DetectionScreen();
                    //检测结果放到异步队列中，在其他线程中执行
                    if (detectionResult.detectionResult4Rect.maxConfidencePos.confidence > 0)
                    {
                        if (posQueue.Count < 10)
                        {
                            posQueue.Add(detectionResult.detectionResult4Rect);
                        }
                    }
                    if (detectionResult.frameMat != null && !detectionResult.frameMat.Empty())
                    {
                        if (imgQueue.Count < 10)
                        {
                            imgQueue.Add(detectionResult);
                        }
                        else
                        {
                            //没有加入处理队列的对象，手工释放资源
                            if (detectionResult.frameMat != null && !detectionResult.frameMat.IsDisposed)
                                detectionResult.frameMat.Dispose();
                        }
                    }
                    //bgWorker.ReportProgress(0, null);  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //显示检测结果后台线程
        private void backgroundWorker_showimg_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;

            //在后台线程中循环显示检测结果
            DetectionResult result = new DetectionResult();           
            while (!bgWorker.CancellationPending)
            {
                try
                {

                    if (imgQueue.TryTake(out result, 100))
                    {                    
                        //持续获取并处理检测结果
                        if (result.frameMat != null && !result.frameMat.Empty())
                        {
                            this.objectDetection.ShowPicture(result.frameMat, result.objectPosRects,
                                result.detectionResult4Rect.maxConfidencePos, result.toltalMillis);

                            //对于检测对象的图像对象，需要手工释放资源
                            if (!result.frameMat.IsDisposed)
                                result.frameMat.Dispose();
                        }                  
                    }
                    //bgWorker.ReportProgress(0, null);  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //鼠标操作后台线程
        private void backgroundWorker_usbdev_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;

            //在后台线程中循环处理鼠标键盘操作
            DetectionResult4Rect result = new DetectionResult4Rect();
            while (!bgWorker.CancellationPending)
            {
                try
                {
                    //如果队列中有多个需要处理的鼠标移动对象，则全部丢弃，只剩最后一个
                    //这样做的目的是在鼠标操作积压的时候，直接使用最新的图片位置检测结果，来移动鼠标，保障更实时的鼠标操作
                    while (posQueue.Count > 1)
                    {
                        posQueue.TryTake(out result);
                    }

                    //处理最后一个鼠标移动对象
                    if (posQueue.TryTake(out result, 100))
                    {
                        //持续获取并处理检测结果
                        if (result.maxConfidencePos.confidence > 0)
                        {
                            this.usbDevice.DoAutoAction(result);
                            systemStatus.currentX = this.usbDevice.currentX;
                            systemStatus.currentY = this.usbDevice.currentY;
                            this.textBox_msg.Text = systemStatus.GetSytemStatus();
                        }
                    }
                    //bgWorker.ReportProgress(0, null);  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void GameMasterForm_Shown(object sender, EventArgs e)
        {

        }

        private void radio_gun1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radio_gun1.Checked)
                this.usbDevice.gunType = 1;
        }

        private void radio_gun2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radio_gun2.Checked)
                this.usbDevice.gunType = 2;
        }

        private void radio_gun3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radio_gun3.Checked)
                this.usbDevice.gunType = 3;
        }

        private void radio_gun4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radio_gun4.Checked)
                this.usbDevice.gunType = 4;
        }

        private void checkBox_checkImg_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_checkImg.Checked)
            {
                //启动屏幕检测
                if (!backgroundWorker_detection.IsBusy)
                {
                    backgroundWorker_detection.RunWorkerAsync();
                    backgroundWorker_showimg.RunWorkerAsync();
                    backgroundWorker_usbdev.RunWorkerAsync();
                    systemStatus.imgCheck = true;
                    objectDetection.IfShowPicture(true);
                    systemStatus.imgShow = true;
                    this.textBox_msg.Text = systemStatus.GetSytemStatus();
                }
            }
            else
            {
                //关闭屏幕检测
                objectDetection.IfShowPicture(false);
                backgroundWorker_detection.CancelAsync();
                backgroundWorker_showimg.CancelAsync();
                backgroundWorker_usbdev.CancelAsync();

                systemStatus.imgCheck = false;
                systemStatus.imgShow = false;
                this.textBox_msg.Text = systemStatus.GetSytemStatus();
            }
        }

        //开启自动开火
        private void checkBox_autoFire_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_autoFire.Checked)
            {
                usbDevice.IfAutoFire(true);
                systemStatus.autoFire = true;
                this.textBox_msg.Text = systemStatus.GetSytemStatus();
            }
            else
            {
                usbDevice.IfAutoFire(false);
                systemStatus.autoFire = false;
                this.textBox_msg.Text = systemStatus.GetSytemStatus();
            }
        }

        //开启自动追踪
        private void checkBox_autoTrace_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_autoTrace.Checked)
            {
                usbDevice.IfAutoTrace(true);
                systemStatus.autoTrace = true;
                this.textBox_msg.Text = systemStatus.GetSytemStatus();
            }
            else
            {
                usbDevice.IfAutoTrace(false);
                systemStatus.autoTrace = false;
                this.textBox_msg.Text = systemStatus.GetSytemStatus();
            }
        }

        private void radio_persistTrace_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radio_persistTrace.Checked)
                usbDevice.autoTraceType = 1;
        }

        private void radio_rButtonTrace_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radio_rButtonTrace.Checked)
                usbDevice.autoTraceType = 2;
        }

        private void timer_rButtonTrace_Tick(object sender, EventArgs e)
        {
            if(usbDevice.autoTraceTimeOut > 0)
            usbDevice.autoTraceTimeOut = usbDevice.autoTraceTimeOut - 1;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox_autoPush_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_autoPush.Checked)
            {
                this.keyboardMouseHook.autoPush = true;
            }
            else
            {
                this.keyboardMouseHook.autoPush = false;
            }
        }
    }
}
