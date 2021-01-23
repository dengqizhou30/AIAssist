namespace GameAssist
{
    partial class AIAssistForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AIAssistForm));
            this.button1 = new System.Windows.Forms.Button();
            this.systemTrayNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer_rButtonTrace = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker_detection = new System.ComponentModel.BackgroundWorker();
            this.button7 = new System.Windows.Forms.Button();
            this.comboBox_process = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.textBox_msg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker_showimg = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_usbdev = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_gun3 = new System.Windows.Forms.RadioButton();
            this.radio_gun2 = new System.Windows.Forms.RadioButton();
            this.radio_gun1 = new System.Windows.Forms.RadioButton();
            this.checkBox_checkImg = new System.Windows.Forms.CheckBox();
            this.checkBox_autoTrace = new System.Windows.Forms.CheckBox();
            this.checkBox_autoFire = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radio_rButtonTrace = new System.Windows.Forms.RadioButton();
            this.radio_persistTrace = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(508, 321);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "隐藏程序窗口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // systemTrayNotifyIcon
            // 
            this.systemTrayNotifyIcon.BalloonTipText = "双击打开程序";
            this.systemTrayNotifyIcon.BalloonTipTitle = "提示";
            this.systemTrayNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTrayNotifyIcon.Icon")));
            this.systemTrayNotifyIcon.Text = "AI游戏助手";
            this.systemTrayNotifyIcon.Visible = true;
            this.systemTrayNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.systemTrayNotifyIcon_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(219, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(450, 274);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "检测结果：";
            // 
            // timer_rButtonTrace
            // 
            this.timer_rButtonTrace.Enabled = true;
            this.timer_rButtonTrace.Interval = 1000;
            this.timer_rButtonTrace.Tick += new System.EventHandler(this.timer_rButtonTrace_Tick);
            // 
            // backgroundWorker_detection
            // 
            this.backgroundWorker_detection.WorkerReportsProgress = true;
            this.backgroundWorker_detection.WorkerSupportsCancellation = true;
            this.backgroundWorker_detection.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_detection_DoWork);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(114, 27);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(89, 23);
            this.button7.TabIndex = 10;
            this.button7.Text = "获取屏幕区域";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // comboBox_process
            // 
            this.comboBox_process.AllowDrop = true;
            this.comboBox_process.FormattingEnabled = true;
            this.comboBox_process.Items.AddRange(new object[] {
            "TslGame",
            "TGame",
            "Borderlands",
            "test",
            "123"});
            this.comboBox_process.Location = new System.Drawing.Point(10, 29);
            this.comboBox_process.Name = "comboBox_process";
            this.comboBox_process.Size = new System.Drawing.Size(99, 20);
            this.comboBox_process.TabIndex = 11;
            this.comboBox_process.Text = "TslGame";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "游戏进程名：";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(298, 321);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(86, 33);
            this.button9.TabIndex = 14;
            this.button9.Text = "键鼠设备检测";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // textBox_msg
            // 
            this.textBox_msg.Location = new System.Drawing.Point(11, 259);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ReadOnly = true;
            this.textBox_msg.Size = new System.Drawing.Size(192, 103);
            this.textBox_msg.TabIndex = 15;
            this.textBox_msg.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "状态信息：";
            // 
            // backgroundWorker_showimg
            // 
            this.backgroundWorker_showimg.WorkerReportsProgress = true;
            this.backgroundWorker_showimg.WorkerSupportsCancellation = true;
            this.backgroundWorker_showimg.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_showimg_DoWork);
            // 
            // backgroundWorker_usbdev
            // 
            this.backgroundWorker_usbdev.WorkerReportsProgress = true;
            this.backgroundWorker_usbdev.WorkerSupportsCancellation = true;
            this.backgroundWorker_usbdev.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_usbdev_DoWork);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_gun3);
            this.groupBox1.Controls.Add(this.radio_gun2);
            this.groupBox1.Controls.Add(this.radio_gun1);
            this.groupBox1.Location = new System.Drawing.Point(80, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 68);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // radio_gun3
            // 
            this.radio_gun3.AutoSize = true;
            this.radio_gun3.Location = new System.Drawing.Point(4, 49);
            this.radio_gun3.Name = "radio_gun3";
            this.radio_gun3.Size = new System.Drawing.Size(47, 16);
            this.radio_gun3.TabIndex = 2;
            this.radio_gun3.Text = "单狙";
            this.radio_gun3.UseVisualStyleBackColor = true;
            this.radio_gun3.CheckedChanged += new System.EventHandler(this.radio_gun3_CheckedChanged);
            // 
            // radio_gun2
            // 
            this.radio_gun2.AutoSize = true;
            this.radio_gun2.Location = new System.Drawing.Point(4, 32);
            this.radio_gun2.Name = "radio_gun2";
            this.radio_gun2.Size = new System.Drawing.Size(47, 16);
            this.radio_gun2.TabIndex = 1;
            this.radio_gun2.Text = "连狙";
            this.radio_gun2.UseVisualStyleBackColor = true;
            this.radio_gun2.CheckedChanged += new System.EventHandler(this.radio_gun2_CheckedChanged);
            // 
            // radio_gun1
            // 
            this.radio_gun1.AutoSize = true;
            this.radio_gun1.Checked = true;
            this.radio_gun1.Location = new System.Drawing.Point(4, 18);
            this.radio_gun1.Name = "radio_gun1";
            this.radio_gun1.Size = new System.Drawing.Size(47, 16);
            this.radio_gun1.TabIndex = 0;
            this.radio_gun1.TabStop = true;
            this.radio_gun1.Text = "步枪";
            this.radio_gun1.UseVisualStyleBackColor = true;
            this.radio_gun1.CheckedChanged += new System.EventHandler(this.radio_gun1_CheckedChanged);
            // 
            // checkBox_checkImg
            // 
            this.checkBox_checkImg.AutoSize = true;
            this.checkBox_checkImg.Location = new System.Drawing.Point(13, 61);
            this.checkBox_checkImg.Name = "checkBox_checkImg";
            this.checkBox_checkImg.Size = new System.Drawing.Size(96, 16);
            this.checkBox_checkImg.TabIndex = 24;
            this.checkBox_checkImg.Text = "启动图像检测";
            this.checkBox_checkImg.UseVisualStyleBackColor = true;
            this.checkBox_checkImg.CheckedChanged += new System.EventHandler(this.checkBox_checkImg_CheckedChanged);
            // 
            // checkBox_autoTrace
            // 
            this.checkBox_autoTrace.AutoSize = true;
            this.checkBox_autoTrace.Location = new System.Drawing.Point(13, 88);
            this.checkBox_autoTrace.Name = "checkBox_autoTrace";
            this.checkBox_autoTrace.Size = new System.Drawing.Size(96, 16);
            this.checkBox_autoTrace.TabIndex = 25;
            this.checkBox_autoTrace.Text = "开启自动追踪";
            this.checkBox_autoTrace.UseVisualStyleBackColor = true;
            this.checkBox_autoTrace.CheckedChanged += new System.EventHandler(this.checkBox_autoTrace_CheckedChanged);
            // 
            // checkBox_autoFire
            // 
            this.checkBox_autoFire.AutoSize = true;
            this.checkBox_autoFire.Location = new System.Drawing.Point(13, 165);
            this.checkBox_autoFire.Name = "checkBox_autoFire";
            this.checkBox_autoFire.Size = new System.Drawing.Size(96, 16);
            this.checkBox_autoFire.TabIndex = 26;
            this.checkBox_autoFire.Text = "开启自动开火";
            this.checkBox_autoFire.UseVisualStyleBackColor = true;
            this.checkBox_autoFire.CheckedChanged += new System.EventHandler(this.checkBox_autoFire_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radio_rButtonTrace);
            this.groupBox2.Controls.Add(this.radio_persistTrace);
            this.groupBox2.Location = new System.Drawing.Point(48, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 57);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // radio_rButtonTrace
            // 
            this.radio_rButtonTrace.AutoSize = true;
            this.radio_rButtonTrace.Location = new System.Drawing.Point(4, 32);
            this.radio_rButtonTrace.Name = "radio_rButtonTrace";
            this.radio_rButtonTrace.Size = new System.Drawing.Size(143, 16);
            this.radio_rButtonTrace.TabIndex = 1;
            this.radio_rButtonTrace.Text = "鼠标右键瞄准触发追踪";
            this.radio_rButtonTrace.UseVisualStyleBackColor = true;
            this.radio_rButtonTrace.CheckedChanged += new System.EventHandler(this.radio_rButtonTrace_CheckedChanged);
            // 
            // radio_persistTrace
            // 
            this.radio_persistTrace.AutoSize = true;
            this.radio_persistTrace.Checked = true;
            this.radio_persistTrace.Location = new System.Drawing.Point(4, 16);
            this.radio_persistTrace.Name = "radio_persistTrace";
            this.radio_persistTrace.Size = new System.Drawing.Size(71, 16);
            this.radio_persistTrace.TabIndex = 0;
            this.radio_persistTrace.TabStop = true;
            this.radio_persistTrace.Text = "持续追踪";
            this.radio_persistTrace.UseVisualStyleBackColor = true;
            this.radio_persistTrace.CheckedChanged += new System.EventHandler(this.radio_persistTrace_CheckedChanged);
            // 
            // AIAssistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 364);
            this.Controls.Add(this.checkBox_autoFire);
            this.Controls.Add(this.checkBox_autoTrace);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox_checkImg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_msg);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_process);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AIAssistForm";
            this.Text = "AI游戏助手";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameMasterForm_FormClosed);
            this.Load += new System.EventHandler(this.GameMasterForm_Load);
            this.Shown += new System.EventHandler(this.GameMasterForm_Shown);
            this.SizeChanged += new System.EventHandler(this.GameMasterForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon systemTrayNotifyIcon;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer_rButtonTrace;
        private System.ComponentModel.BackgroundWorker backgroundWorker_detection;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ComboBox comboBox_process;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox textBox_msg;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker_showimg;
        private System.ComponentModel.BackgroundWorker backgroundWorker_usbdev;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio_gun3;
        private System.Windows.Forms.RadioButton radio_gun2;
        private System.Windows.Forms.RadioButton radio_gun1;
        private System.Windows.Forms.CheckBox checkBox_checkImg;
        private System.Windows.Forms.CheckBox checkBox_autoTrace;
        private System.Windows.Forms.CheckBox checkBox_autoFire;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radio_rButtonTrace;
        private System.Windows.Forms.RadioButton radio_persistTrace;
    }
}

