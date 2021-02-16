using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LowLevelInput.Converters;
using LowLevelInput.Hooks;

namespace GameAssist
{
    class KeyboardMouseHook
    {
        //注意有些游戏如逆战禁用了鼠标键盘hook，绝地求生可以使用。

        // creates a new instance to capture inputs
        // also provides IsPressed, WasPressed and GetState methods
        // hook框架对象
        private InputManager inputManager = null;

        //使用队列保存最近20个按键记录
        private static int msgLimit = 20;
        private Queue<string> msgQueue = new Queue<string>(msgLimit);

        //主窗体对象、消息显示文本对象，及是否显示消息标志
        private Form mainForm = null;
        private ComboBox comboBox_bag1GunType;
        private ComboBox comboBox_bag1ScopeType;
        private ComboBox comboBox_bag2ScopeType;
        private ComboBox comboBox_bag2GunType;
        private TextBox msgTextBox = null;
        private bool showMsg = false;

        //是否自动压枪
        public Boolean autoPush = false;
        //当前的武器键
        private VirtualKeyCode curBagKey = VirtualKeyCode.Three;
        //当前配置设置步骤，顺序为Numpad0、背包位置(Numpad1,Numpad2)、枪械类型、倍镜类型
        //枪械类型对应按键：uzi/vector:Numpad5,m4:Numpad4,scar:Numpad6,ak:Numpad7,m762:Numpad8,mini/sks/98k/m24:Numpad9
        private int confSetStep = 0;
        private int confSetBag = 1;
        private String bag1GunType = "ak";
        private String bag1ScopeType = "基础镜";
        private String bag2GunType = "98k";
        private String bag2ScopeType = "4倍镜";

        //枪械数组
        string[] gunTypeArr = { "uzi", "m4", "scar", "ak", "m762", "98k"};
        //各种枪械压枪数据数组(tbs,comp)
        int[,] gunPushArr = { { 3, 12 }, { 4, 40 }, { 5, 32 }, { 6, 34 }, { 4, 60 }, { 6, 34 } };
        //倍镜数组
        string[] scopeTypeArr = { "基础镜", "2倍镜", "4倍镜", "6倍镜", "8倍镜" };
        //倍镜加成数组
        double[] scopePushArr = { 1.0, 1.5, 2.0, 2.5, 3.0 };

        //压枪的comp调整值
        int compTune = 0;

        //usb设备
        private UsbDevice usbDevice = null;

        public KeyboardMouseHook(UsbDevice usbDevice, Form mainForm, ComboBox comboBox_bag1GunType, ComboBox comboBox_bag1ScopeType, ComboBox comboBox_bag2GunType, ComboBox comboBox_bag2ScopeType)
        {
            this.usbDevice = usbDevice;
            this.mainForm = mainForm;
            this.comboBox_bag1GunType = comboBox_bag1GunType;
            this.comboBox_bag1ScopeType = comboBox_bag1ScopeType;
            this.comboBox_bag2GunType = comboBox_bag2GunType;
            this.comboBox_bag2ScopeType = comboBox_bag2ScopeType;
            
        }

        //启动hook
        public void StartHook()
        {
            if (inputManager != null) 
            {
                inputManager.Dispose();
                inputManager = null;
            }

            // creates a new instance to capture inputs
            // also provides IsPressed, WasPressed and GetState methods
            inputManager = new InputManager();

            // subscribe to the events offered by InputManager
            //inputManager.OnKeyboardEvent += InputManager_OnKeyboardEvent;
            //inputManager.OnMouseEvent += InputManager_OnMouseEvent;

            // we need to initialize our classes before they fire events and are completely usable
            inputManager.Initialize(false,true,true);

            // registers an event (callback) which gets fired whenever the key changes it's state
            // be sure to use this method after the InputManager is initialized
            //inputManager.RegisterEvent(VirtualKeyCode.Lbutton, InputManager_KeyStateChanged);
            //对于可以设置鼠键钩子的游戏，如绝地求生等，设置鼠键钩子监视鼠标右键按下。有些游戏如逆战，在游戏启用后会禁用鼠键钩子，这种方式不起作用
            //对于支持鼠键钩子的游戏，监控鼠标操作/武器切换操作/数据键盘设置操作。
            inputManager.RegisterEvent(VirtualKeyCode.Rbutton, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Lbutton, InputManager_KeyStateChanged);

            inputManager.RegisterEvent(VirtualKeyCode.One, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Two, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Three, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Four, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Five, InputManager_KeyStateChanged);

            inputManager.RegisterEvent(VirtualKeyCode.Numpad0, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad1, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad2, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad3, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad4, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad5, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad6, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad7, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad8, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Numpad9, InputManager_KeyStateChanged);

            inputManager.RegisterEvent(VirtualKeyCode.Add, InputManager_KeyStateChanged);
            inputManager.RegisterEvent(VirtualKeyCode.Subtract, InputManager_KeyStateChanged);
        }

        //停止Hook
        public void StopHook()
        {
            // be sure to dispose instances you dont use anymore
            // not doing so may block windows input and let inputs appear delayed or lagging
            // these classes try dispose itself when an unhandled exception occurs or the process exits
            showMsg = false;

            if (inputManager != null)
            {
                inputManager.Dispose();
                inputManager = null;
            }
        }

        private void InputManager_OnMouseEvent(VirtualKeyCode key, KeyState state, int x, int y)
        {
            // x and y may be 0 if there is no data
            // string msg = "OnMouseEvent: " + KeyCodeConverter.ToString(key) + " - " + KeyStateConverter.ToString(state) + " - X: " + x + ", Y: " + y;
            // this.addMsg(msg);
            // this.showMsg();
        }

        private void InputManager_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
        {
            string msg = "OnKeyboardEvent: " + KeyCodeConverter.ToString(key) + " - " + KeyStateConverter.ToString(state);
            this.AddMsg(msg);
            this.ShowMsg();
        }

        private void InputManager_KeyStateChanged(VirtualKeyCode key, KeyState state)
        {
            // you may use the same callback for every key or define a new one for each
            /**
            string msg = "The key state of " + KeyCodeConverter.ToString(key) + " changed to " + KeyStateConverter.ToString(state);
            this.AddMsg(msg);
            this.ShowMsg();
            **/

            switch (key)
            {
                case VirtualKeyCode.Rbutton:
                    //鼠标右键按下时，设置自动追踪运行的时间长度，单位是秒
                    if (state == KeyState.Down)
                    {
                        this.usbDevice.autoTraceTimeOut = 10;
                    }
                    break;

                case VirtualKeyCode.Lbutton:
                    //鼠标左键按下时，如果是背包1或者背包2的武器，则执行压枪操作
                    if (autoPush)
                    {
                        if (state == KeyState.Down)
                        {
                            this.startAutoPush();
                        }
                        else if (state == KeyState.Up)
                        {
                            this.stopAutoPush();
                        }
                    }
                    break;

                case VirtualKeyCode.One:
                case VirtualKeyCode.Two:
                case VirtualKeyCode.Three:
                case VirtualKeyCode.Four:
                case VirtualKeyCode.Five:
                    //记录玩家当前使用的背包
                    if (state == KeyState.Down)
                    {
                        this.curBagKey = key;
                    }
                    break;

                case VirtualKeyCode.Numpad0:
                    //开始配置
                    if (state == KeyState.Down)
                    {
                        //压枪的comp调整值重置为0
                        compTune = 0;
                        //开始配置
                        this.confSetStep = 0;
                    }
                    break;
                case VirtualKeyCode.Numpad1:               
                case VirtualKeyCode.Numpad2:
                case VirtualKeyCode.Numpad3:
                case VirtualKeyCode.Numpad4:
                case VirtualKeyCode.Numpad5:
                case VirtualKeyCode.Numpad6:
                case VirtualKeyCode.Numpad7:
                case VirtualKeyCode.Numpad8:
                case VirtualKeyCode.Numpad9:
                    //当前配置设置步骤，顺序为Numpad0、背包位置(Numpad1,Numpad2)、枪械类型、倍镜类型
                    //枪械类型对应按键：uzi/vector:Numpad5,m4:Numpad4,scar:Numpad6,ak:Numpad7,m762:Numpad8,mini/sks/98k/m24:Numpad9
                    if (state == KeyState.Down)
                    {
                        //设置要配置的背包
                        if (this.confSetStep == 0)
                        {
                            this.confSetBag = convertBag(key);
                            this.confSetStep++;
                        }
                        //设置背包里的武器
                        else if (this.confSetStep == 1)
                        {
                            if (this.confSetBag == 1)
                            {
                                this.bag1GunType = convertGunType(key);
                                this.comboBox_bag1GunType.Text = this.bag1GunType;
                            }
                            else if (this.confSetBag == 2)
                            {
                                this.bag2GunType = convertGunType(key);
                                this.comboBox_bag2GunType.Text = this.bag2GunType;
                            }
                            this.confSetStep++;
                        }
                        //设置倍镜
                        else if (this.confSetStep == 2)
                        {
                            if (this.confSetBag == 1)
                            {
                                this.bag1ScopeType = convertScopeType(key);
                                this.comboBox_bag1ScopeType.Text = this.bag1ScopeType;
                            }
                            else if (this.confSetBag == 2)
                            {
                                this.bag2ScopeType = convertScopeType(key);
                                this.comboBox_bag2ScopeType.Text = this.bag2ScopeType;
                            }
                            this.confSetStep++;
                        }
                    }
                    break;

                case VirtualKeyCode.Add:
                    if (state == KeyState.Down)
                    {
                        //压枪的comp调整值增加
                        compTune++;
                    }
                    break;
                case VirtualKeyCode.Subtract:
                    if (state == KeyState.Down)
                    {
                        //压枪的comp调整值减少
                        compTune --;
                    }
                    break;
            }
            
        }

        //开始自动压枪
        private void startAutoPush()
        {
            //鼠标左键按下时，如果是背包1或者背包2的武器，则执行压枪操作，其他背包不需要压枪
            if (this.curBagKey == VirtualKeyCode.One || this.curBagKey == VirtualKeyCode.Two)
            {
                string gunType = (this.curBagKey == VirtualKeyCode.One) ? this.bag1GunType: this.bag2GunType;
                string scopeType = (this.curBagKey == VirtualKeyCode.One) ? this.bag1ScopeType : this.bag2ScopeType;
                int gunTypeIdx = gunTypeArr.ToList().IndexOf(gunType);
                int scopeTypeIdx = scopeTypeArr.ToList().IndexOf(scopeType);

                if (gunTypeIdx >= 0 && scopeTypeIdx >= 0)
                {
                    int tbs = gunPushArr[gunTypeIdx, 0];
                    int comp = gunPushArr[gunTypeIdx, 1];
                    double scope = scopePushArr[scopeTypeIdx];

                    comp = (int)(comp * scope);

                    comp = comp + compTune;

                    this.usbDevice.StartAutoPush(tbs, comp);
                }
            }
        }

        //停止自动压枪
        private void stopAutoPush()
        {
            this.usbDevice.StopAutoPush();     
        }


        //转换为背包数字
        private int convertBag(VirtualKeyCode key)
        {
            //倍镜类型
            int ret = 1;
            switch (key)
            {
                case VirtualKeyCode.Numpad1:
                    ret = 1;
                    break;
                case VirtualKeyCode.Numpad2:
                    ret = 2;
                    break;          
            }
            return ret;
        }

        //转换为枪械类型
        private string convertGunType(VirtualKeyCode key)
        {
            //枪械类型对应按键：uzi/vector:Numpad5,m4:Numpad4,scar:Numpad6,ak:Numpad7,m762:Numpad8,mini/sks/98k/m24:Numpad9
            string ret = gunTypeArr[3];
            switch (key)
            {
                case VirtualKeyCode.Numpad4:
                    ret = gunTypeArr[1];
                    break;
                case VirtualKeyCode.Numpad5:
                    ret = gunTypeArr[0];
                    break;
                case VirtualKeyCode.Numpad6:
                    ret = gunTypeArr[2];
                    break;
                case VirtualKeyCode.Numpad7:
                    ret = gunTypeArr[3];
                    break;
                case VirtualKeyCode.Numpad8:
                    ret = gunTypeArr[4];
                    break;
                case VirtualKeyCode.Numpad9:
                    ret = gunTypeArr[5];
                    break;
            }
            return ret;
        }

        //转换为倍镜类型
        private string convertScopeType(VirtualKeyCode key)
        {
            //倍镜类型
            string ret = scopeTypeArr[0];
            switch (key)
            {
                case VirtualKeyCode.Numpad1:
                    ret = scopeTypeArr[0];
                    break;
                case VirtualKeyCode.Numpad2:
                    ret = scopeTypeArr[1];
                    break;
                case VirtualKeyCode.Numpad4:
                    ret = scopeTypeArr[2];
                    break;
                case VirtualKeyCode.Numpad6:
                    ret = scopeTypeArr[3];
                    break;
                case VirtualKeyCode.Numpad8:
                    ret = scopeTypeArr[4];
                    break;
            }
            return ret;
        }

        //在队列中保存定长的最新消息
        private void AddMsg(string msg)
        {
            while (msgQueue.Count >= msgLimit)
            {
                msgQueue.Dequeue();
            }
            msgQueue.Enqueue(msg);
        }

        public void IfShowMsg(bool showMsg)
        {
            this.showMsg = showMsg;
        }

        private void ShowMsg()
        {
            //窗体隐藏的时候也不用显示消息
            if (this.showMsg && mainForm != null && msgTextBox != null && mainForm.Visible)
            {
                
                //注意程序是事件却动，在遍历queque的时候，可能会同时修改msgQueue的值，导致遍历失败
                //使用ToArray方法拷贝一份副本，可以避免出现上述错误。
                string msg = "";
                foreach (string str in msgQueue.ToArray<string>())
                    msg += str + "\r\n";
                msgTextBox.Text = msg;
              
            }
        }
    }
}
