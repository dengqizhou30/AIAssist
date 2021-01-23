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
        //注意逆战清理禁用了鼠标键盘hook，绝地求生没有禁用。

        // creates a new instance to capture inputs
        // also provides IsPressed, WasPressed and GetState methods
        // hook框架对象
        private InputManager inputManager = null;

        //使用队列保存最近20个按键记录
        private static int msgLimit = 20;
        private Queue<string> msgQueue = new Queue<string>(msgLimit);

        //主窗体对象、消息显示文本对象，及是否显示消息标志
        private Form mainForm = null;
        private TextBox msgTextBox = null;
        private bool showMsg = false;

        private UsbDevice usbDevice = null;

        public KeyboardMouseHook(Form mainForm, UsbDevice usbDevice)
        {
            this.mainForm = mainForm;
            this.usbDevice = usbDevice;
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
            //只检测鼠标右键状态，也就是瞄准键的状态
            inputManager.RegisterEvent(VirtualKeyCode.Rbutton, InputManager_KeyStateChanged);
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

            //鼠标右键按下时，设置自动追踪运行的时间长度，单位是秒
            if(key == VirtualKeyCode.Rbutton && state == KeyState.Down)
            {
                this.usbDevice.autoTraceTimeOut = 10;
            }
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
