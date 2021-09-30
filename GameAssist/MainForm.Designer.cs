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
            this.radio_gun4 = new System.Windows.Forms.RadioButton();
            this.radio_gun3 = new System.Windows.Forms.RadioButton();
            this.radio_gun2 = new System.Windows.Forms.RadioButton();
            this.radio_gun1 = new System.Windows.Forms.RadioButton();
            this.checkBox_checkImg = new System.Windows.Forms.CheckBox();
            this.checkBox_autoTrace = new System.Windows.Forms.CheckBox();
            this.checkBox_autoFire = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radio_rButtonTrace = new System.Windows.Forms.RadioButton();
            this.radio_persistTrace = new System.Windows.Forms.RadioButton();
            this.checkBox_autoPush = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_bag1GunType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_bag1ScopeType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_bag2ScopeType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_bag2GunType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_detectionRectW = new System.Windows.Forms.TextBox();
            this.textBox_detectionRectH = new System.Windows.Forms.TextBox();
            this.textBox_autoFireTime = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(534, 363);
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
            this.pictureBox1.Location = new System.Drawing.Point(219, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(404, 259);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 8);
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
            this.button7.Location = new System.Drawing.Point(121, 27);
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
            this.comboBox_process.Size = new System.Drawing.Size(105, 20);
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
            this.button9.Location = new System.Drawing.Point(534, 307);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(86, 33);
            this.button9.TabIndex = 14;
            this.button9.Text = "键鼠设备检测";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // textBox_msg
            // 
            this.textBox_msg.Location = new System.Drawing.Point(219, 300);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ReadOnly = true;
            this.textBox_msg.Size = new System.Drawing.Size(293, 103);
            this.textBox_msg.TabIndex = 15;
            this.textBox_msg.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 286);
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
            this.groupBox1.Controls.Add(this.radio_gun4);
            this.groupBox1.Controls.Add(this.radio_gun3);
            this.groupBox1.Controls.Add(this.radio_gun2);
            this.groupBox1.Controls.Add(this.radio_gun1);
            this.groupBox1.Location = new System.Drawing.Point(49, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 90);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // radio_gun4
            // 
            this.radio_gun4.AutoSize = true;
            this.radio_gun4.Location = new System.Drawing.Point(5, 68);
            this.radio_gun4.Name = "radio_gun4";
            this.radio_gun4.Size = new System.Drawing.Size(95, 16);
            this.radio_gun4.TabIndex = 3;
            this.radio_gun4.Text = "持续射击(ms)";
            this.radio_gun4.UseVisualStyleBackColor = true;
            this.radio_gun4.CheckedChanged += new System.EventHandler(this.radio_gun4_CheckedChanged);
            // 
            // radio_gun3
            // 
            this.radio_gun3.AutoSize = true;
            this.radio_gun3.Location = new System.Drawing.Point(4, 49);
            this.radio_gun3.Name = "radio_gun3";
            this.radio_gun3.Size = new System.Drawing.Size(77, 16);
            this.radio_gun3.TabIndex = 2;
            this.radio_gun3.Text = "6连点射击";
            this.radio_gun3.UseVisualStyleBackColor = true;
            this.radio_gun3.CheckedChanged += new System.EventHandler(this.radio_gun3_CheckedChanged);
            // 
            // radio_gun2
            // 
            this.radio_gun2.AutoSize = true;
            this.radio_gun2.Location = new System.Drawing.Point(4, 32);
            this.radio_gun2.Name = "radio_gun2";
            this.radio_gun2.Size = new System.Drawing.Size(77, 16);
            this.radio_gun2.TabIndex = 1;
            this.radio_gun2.Text = "3连点射击";
            this.radio_gun2.UseVisualStyleBackColor = true;
            this.radio_gun2.CheckedChanged += new System.EventHandler(this.radio_gun2_CheckedChanged);
            // 
            // radio_gun1
            // 
            this.radio_gun1.AutoSize = true;
            this.radio_gun1.Checked = true;
            this.radio_gun1.Location = new System.Drawing.Point(4, 15);
            this.radio_gun1.Name = "radio_gun1";
            this.radio_gun1.Size = new System.Drawing.Size(71, 16);
            this.radio_gun1.TabIndex = 0;
            this.radio_gun1.TabStop = true;
            this.radio_gun1.Text = "单点射击";
            this.radio_gun1.UseVisualStyleBackColor = true;
            this.radio_gun1.CheckedChanged += new System.EventHandler(this.radio_gun1_CheckedChanged);
            // 
            // checkBox_checkImg
            // 
            this.checkBox_checkImg.AutoSize = true;
            this.checkBox_checkImg.Location = new System.Drawing.Point(10, 67);
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
            this.checkBox_autoTrace.Location = new System.Drawing.Point(10, 115);
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
            this.checkBox_autoFire.Location = new System.Drawing.Point(10, 188);
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
            this.groupBox2.Location = new System.Drawing.Point(51, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 57);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // radio_rButtonTrace
            // 
            this.radio_rButtonTrace.AutoSize = true;
            this.radio_rButtonTrace.Location = new System.Drawing.Point(4, 30);
            this.radio_rButtonTrace.Name = "radio_rButtonTrace";
            this.radio_rButtonTrace.Size = new System.Drawing.Size(143, 16);
            this.radio_rButtonTrace.TabIndex = 1;
            this.radio_rButtonTrace.Text = "点击鼠标右键触发追踪";
            this.radio_rButtonTrace.UseVisualStyleBackColor = true;
            this.radio_rButtonTrace.CheckedChanged += new System.EventHandler(this.radio_rButtonTrace_CheckedChanged);
            // 
            // radio_persistTrace
            // 
            this.radio_persistTrace.AutoSize = true;
            this.radio_persistTrace.Checked = true;
            this.radio_persistTrace.Location = new System.Drawing.Point(4, 12);
            this.radio_persistTrace.Name = "radio_persistTrace";
            this.radio_persistTrace.Size = new System.Drawing.Size(71, 16);
            this.radio_persistTrace.TabIndex = 0;
            this.radio_persistTrace.TabStop = true;
            this.radio_persistTrace.Text = "持续追踪";
            this.radio_persistTrace.UseVisualStyleBackColor = true;
            this.radio_persistTrace.CheckedChanged += new System.EventHandler(this.radio_persistTrace_CheckedChanged);
            // 
            // checkBox_autoPush
            // 
            this.checkBox_autoPush.AutoSize = true;
            this.checkBox_autoPush.Location = new System.Drawing.Point(10, 294);
            this.checkBox_autoPush.Name = "checkBox_autoPush";
            this.checkBox_autoPush.Size = new System.Drawing.Size(204, 16);
            this.checkBox_autoPush.TabIndex = 28;
            this.checkBox_autoPush.Text = "开启自动压枪(目前只适用于pubg)";
            this.checkBox_autoPush.UseVisualStyleBackColor = true;
            this.checkBox_autoPush.CheckedChanged += new System.EventHandler(this.checkBox_autoPush_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "枪械类型：";
            // 
            // comboBox_bag1GunType
            // 
            this.comboBox_bag1GunType.AllowDrop = true;
            this.comboBox_bag1GunType.FormattingEnabled = true;
            this.comboBox_bag1GunType.Items.AddRange(new object[] {
            "uzi",
            "vector",
            "scar",
            "m4",
            "ak",
            "m762",
            "mini",
            "sks",
            "98k",
            "m24"});
            this.comboBox_bag1GunType.Location = new System.Drawing.Point(51, 332);
            this.comboBox_bag1GunType.Name = "comboBox_bag1GunType";
            this.comboBox_bag1GunType.Size = new System.Drawing.Size(75, 20);
            this.comboBox_bag1GunType.TabIndex = 29;
            this.comboBox_bag1GunType.Text = "ak";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "背包1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "倍镜类型：";
            // 
            // comboBox_bag1ScopeType
            // 
            this.comboBox_bag1ScopeType.AllowDrop = true;
            this.comboBox_bag1ScopeType.FormattingEnabled = true;
            this.comboBox_bag1ScopeType.Items.AddRange(new object[] {
            "基础镜",
            "2倍镜",
            "4倍镜",
            "6倍镜",
            "8倍镜"});
            this.comboBox_bag1ScopeType.Location = new System.Drawing.Point(134, 333);
            this.comboBox_bag1ScopeType.Name = "comboBox_bag1ScopeType";
            this.comboBox_bag1ScopeType.Size = new System.Drawing.Size(75, 20);
            this.comboBox_bag1ScopeType.TabIndex = 32;
            this.comboBox_bag1ScopeType.Text = "基础镜";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(133, 359);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "倍镜类型：";
            // 
            // comboBox_bag2ScopeType
            // 
            this.comboBox_bag2ScopeType.AllowDrop = true;
            this.comboBox_bag2ScopeType.FormattingEnabled = true;
            this.comboBox_bag2ScopeType.Items.AddRange(new object[] {
            "基础镜",
            "2倍镜",
            "4倍镜",
            "6倍镜",
            "8倍镜"});
            this.comboBox_bag2ScopeType.Location = new System.Drawing.Point(135, 376);
            this.comboBox_bag2ScopeType.Name = "comboBox_bag2ScopeType";
            this.comboBox_bag2ScopeType.Size = new System.Drawing.Size(75, 20);
            this.comboBox_bag2ScopeType.TabIndex = 37;
            this.comboBox_bag2ScopeType.Text = "4倍镜";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 379);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "背包2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(50, 358);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "枪械类型：";
            // 
            // comboBox_bag2GunType
            // 
            this.comboBox_bag2GunType.AllowDrop = true;
            this.comboBox_bag2GunType.FormattingEnabled = true;
            this.comboBox_bag2GunType.Items.AddRange(new object[] {
            "uzi",
            "vector",
            "scar",
            "m4",
            "ak",
            "m762",
            "mini",
            "sks",
            "98k",
            "m24"});
            this.comboBox_bag2GunType.Location = new System.Drawing.Point(52, 375);
            this.comboBox_bag2GunType.Name = "comboBox_bag2GunType";
            this.comboBox_bag2GunType.Size = new System.Drawing.Size(75, 20);
            this.comboBox_bag2GunType.TabIndex = 34;
            this.comboBox_bag2GunType.Text = "98k";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 12);
            this.label10.TabIndex = 39;
            this.label10.Text = "检测区域：宽/高";
            // 
            // textBox_detectionRectW
            // 
            this.textBox_detectionRectW.Location = new System.Drawing.Point(112, 83);
            this.textBox_detectionRectW.Name = "textBox_detectionRectW";
            this.textBox_detectionRectW.Size = new System.Drawing.Size(41, 21);
            this.textBox_detectionRectW.TabIndex = 40;
            this.textBox_detectionRectW.Text = "290";
            this.textBox_detectionRectW.TextChanged += new System.EventHandler(this.textBox_detectionRectW_TextChanged);
            // 
            // textBox_detectionRectH
            // 
            this.textBox_detectionRectH.Location = new System.Drawing.Point(159, 83);
            this.textBox_detectionRectH.Name = "textBox_detectionRectH";
            this.textBox_detectionRectH.Size = new System.Drawing.Size(41, 21);
            this.textBox_detectionRectH.TabIndex = 41;
            this.textBox_detectionRectH.Text = "230";
            this.textBox_detectionRectH.TextChanged += new System.EventHandler(this.textBox_detectionRectH_TextChanged);
            // 
            // textBox_autoFireTime
            // 
            this.textBox_autoFireTime.Location = new System.Drawing.Point(154, 257);
            this.textBox_autoFireTime.Name = "textBox_autoFireTime";
            this.textBox_autoFireTime.Size = new System.Drawing.Size(41, 21);
            this.textBox_autoFireTime.TabIndex = 42;
            this.textBox_autoFireTime.Text = "600";
            this.textBox_autoFireTime.TextChanged += new System.EventHandler(this.textBox_autoFireTime_TextChanged);
            // 
            // AIAssistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 408);
            this.Controls.Add(this.textBox_autoFireTime);
            this.Controls.Add(this.textBox_detectionRectH);
            this.Controls.Add(this.textBox_detectionRectW);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox_bag2ScopeType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox_bag2GunType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox_bag1ScopeType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_bag1GunType);
            this.Controls.Add(this.checkBox_autoPush);
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
        private System.Windows.Forms.RadioButton radio_gun4;
        private System.Windows.Forms.CheckBox checkBox_autoPush;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_bag1GunType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_bag1ScopeType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_bag2ScopeType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_bag2GunType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_detectionRectW;
        private System.Windows.Forms.TextBox textBox_detectionRectH;
        private System.Windows.Forms.TextBox textBox_autoFireTime;
    }
}

