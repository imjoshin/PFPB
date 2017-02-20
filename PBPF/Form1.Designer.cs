namespace PBPF {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txt_APIKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_pfLocation = new System.Windows.Forms.TextBox();
            this.btn_pfSelector = new System.Windows.Forms.Button();
            this.grp_General = new System.Windows.Forms.GroupBox();
            this.grp_Notifications = new System.Windows.Forms.GroupBox();
            this.chk_Rare = new System.Windows.Forms.CheckBox();
            this.chk_CPIV = new System.Windows.Forms.CheckBox();
            this.chk_Bot = new System.Windows.Forms.CheckBox();
            this.chk_Captcha = new System.Windows.Forms.CheckBox();
            this.chk_Egg = new System.Windows.Forms.CheckBox();
            this.chk_Level = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.num_IV = new System.Windows.Forms.NumericUpDown();
            this.num_CP = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_10K = new System.Windows.Forms.CheckBox();
            this.chk_2K = new System.Windows.Forms.CheckBox();
            this.chk_5K = new System.Windows.Forms.CheckBox();
            this.btn_StartStop = new System.Windows.Forms.Button();
            this.tim_Ticker = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.grp_General.SuspendLayout();
            this.grp_Notifications.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_IV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_CP)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_APIKey
            // 
            this.txt_APIKey.Location = new System.Drawing.Point(125, 19);
            this.txt_APIKey.Name = "txt_APIKey";
            this.txt_APIKey.Size = new System.Drawing.Size(131, 20);
            this.txt_APIKey.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PushBullet API Key: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "PokeFarmer Location:";
            // 
            // txt_pfLocation
            // 
            this.txt_pfLocation.Location = new System.Drawing.Point(125, 45);
            this.txt_pfLocation.Name = "txt_pfLocation";
            this.txt_pfLocation.ReadOnly = true;
            this.txt_pfLocation.Size = new System.Drawing.Size(107, 20);
            this.txt_pfLocation.TabIndex = 2;
            // 
            // btn_pfSelector
            // 
            this.btn_pfSelector.Location = new System.Drawing.Point(231, 44);
            this.btn_pfSelector.Name = "btn_pfSelector";
            this.btn_pfSelector.Size = new System.Drawing.Size(26, 22);
            this.btn_pfSelector.TabIndex = 4;
            this.btn_pfSelector.Text = "...";
            this.btn_pfSelector.UseVisualStyleBackColor = true;
            this.btn_pfSelector.Click += new System.EventHandler(this.btn_pfSelector_Click);
            // 
            // grp_General
            // 
            this.grp_General.Controls.Add(this.txt_pfLocation);
            this.grp_General.Controls.Add(this.btn_pfSelector);
            this.grp_General.Controls.Add(this.txt_APIKey);
            this.grp_General.Controls.Add(this.label2);
            this.grp_General.Controls.Add(this.label1);
            this.grp_General.Location = new System.Drawing.Point(13, 12);
            this.grp_General.Name = "grp_General";
            this.grp_General.Size = new System.Drawing.Size(263, 75);
            this.grp_General.TabIndex = 5;
            this.grp_General.TabStop = false;
            this.grp_General.Text = "General";
            // 
            // grp_Notifications
            // 
            this.grp_Notifications.Controls.Add(this.chk_Rare);
            this.grp_Notifications.Controls.Add(this.chk_CPIV);
            this.grp_Notifications.Controls.Add(this.chk_Bot);
            this.grp_Notifications.Controls.Add(this.chk_Captcha);
            this.grp_Notifications.Controls.Add(this.chk_Egg);
            this.grp_Notifications.Controls.Add(this.chk_Level);
            this.grp_Notifications.Location = new System.Drawing.Point(13, 93);
            this.grp_Notifications.Name = "grp_Notifications";
            this.grp_Notifications.Size = new System.Drawing.Size(263, 94);
            this.grp_Notifications.TabIndex = 6;
            this.grp_Notifications.TabStop = false;
            this.grp_Notifications.Text = "Notifications";
            // 
            // chk_Rare
            // 
            this.chk_Rare.AutoSize = true;
            this.chk_Rare.Checked = true;
            this.chk_Rare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Rare.Location = new System.Drawing.Point(108, 69);
            this.chk_Rare.Name = "chk_Rare";
            this.chk_Rare.Size = new System.Drawing.Size(86, 17);
            this.chk_Rare.TabIndex = 6;
            this.chk_Rare.Text = "Caught Rare";
            this.chk_Rare.UseVisualStyleBackColor = true;
            // 
            // chk_CPIV
            // 
            this.chk_CPIV.AutoSize = true;
            this.chk_CPIV.Checked = true;
            this.chk_CPIV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CPIV.Location = new System.Drawing.Point(108, 46);
            this.chk_CPIV.Name = "chk_CPIV";
            this.chk_CPIV.Size = new System.Drawing.Size(126, 17);
            this.chk_CPIV.TabIndex = 5;
            this.chk_CPIV.Text = "Caught Above CP/IV";
            this.chk_CPIV.UseVisualStyleBackColor = true;
            // 
            // chk_Bot
            // 
            this.chk_Bot.AutoSize = true;
            this.chk_Bot.Checked = true;
            this.chk_Bot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Bot.Location = new System.Drawing.Point(108, 23);
            this.chk_Bot.Name = "chk_Bot";
            this.chk_Bot.Size = new System.Drawing.Size(94, 17);
            this.chk_Bot.TabIndex = 4;
            this.chk_Bot.Text = "Bot Start/Stop";
            this.chk_Bot.UseVisualStyleBackColor = true;
            // 
            // chk_Captcha
            // 
            this.chk_Captcha.AutoSize = true;
            this.chk_Captcha.Checked = true;
            this.chk_Captcha.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Captcha.Location = new System.Drawing.Point(14, 69);
            this.chk_Captcha.Name = "chk_Captcha";
            this.chk_Captcha.Size = new System.Drawing.Size(66, 17);
            this.chk_Captcha.TabIndex = 3;
            this.chk_Captcha.Text = "Captcha";
            this.chk_Captcha.UseVisualStyleBackColor = true;
            // 
            // chk_Egg
            // 
            this.chk_Egg.AutoSize = true;
            this.chk_Egg.Checked = true;
            this.chk_Egg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Egg.Location = new System.Drawing.Point(14, 46);
            this.chk_Egg.Name = "chk_Egg";
            this.chk_Egg.Size = new System.Drawing.Size(77, 17);
            this.chk_Egg.TabIndex = 2;
            this.chk_Egg.Text = "Egg Hatch";
            this.chk_Egg.UseVisualStyleBackColor = true;
            // 
            // chk_Level
            // 
            this.chk_Level.AutoSize = true;
            this.chk_Level.Checked = true;
            this.chk_Level.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Level.Location = new System.Drawing.Point(14, 23);
            this.chk_Level.Name = "chk_Level";
            this.chk_Level.Size = new System.Drawing.Size(69, 17);
            this.chk_Level.TabIndex = 1;
            this.chk_Level.Text = "Level Up";
            this.chk_Level.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.num_IV);
            this.groupBox1.Controls.Add(this.num_CP);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chk_10K);
            this.groupBox1.Controls.Add(this.chk_2K);
            this.groupBox1.Controls.Add(this.chk_5K);
            this.groupBox1.Location = new System.Drawing.Point(13, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 110);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // num_IV
            // 
            this.num_IV.Location = new System.Drawing.Point(145, 61);
            this.num_IV.Name = "num_IV";
            this.num_IV.Size = new System.Drawing.Size(49, 20);
            this.num_IV.TabIndex = 15;
            this.num_IV.Value = new decimal(new int[] {
            92,
            0,
            0,
            0});
            // 
            // num_CP
            // 
            this.num_CP.Location = new System.Drawing.Point(145, 39);
            this.num_CP.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.num_CP.Name = "num_CP";
            this.num_CP.Size = new System.Drawing.Size(49, 20);
            this.num_CP.TabIndex = 14;
            this.num_CP.Value = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(115, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "IV:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "CP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "CP/IV Limits";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Egg Notifications";
            // 
            // chk_10K
            // 
            this.chk_10K.AutoSize = true;
            this.chk_10K.Checked = true;
            this.chk_10K.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_10K.Location = new System.Drawing.Point(14, 86);
            this.chk_10K.Name = "chk_10K";
            this.chk_10K.Size = new System.Drawing.Size(55, 17);
            this.chk_10K.TabIndex = 9;
            this.chk_10K.Text = "10 km";
            this.chk_10K.UseVisualStyleBackColor = true;
            // 
            // chk_2K
            // 
            this.chk_2K.AutoSize = true;
            this.chk_2K.Checked = true;
            this.chk_2K.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_2K.Location = new System.Drawing.Point(14, 40);
            this.chk_2K.Name = "chk_2K";
            this.chk_2K.Size = new System.Drawing.Size(49, 17);
            this.chk_2K.TabIndex = 7;
            this.chk_2K.Text = "2 km";
            this.chk_2K.UseVisualStyleBackColor = true;
            // 
            // chk_5K
            // 
            this.chk_5K.AutoSize = true;
            this.chk_5K.Checked = true;
            this.chk_5K.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_5K.Location = new System.Drawing.Point(14, 63);
            this.chk_5K.Name = "chk_5K";
            this.chk_5K.Size = new System.Drawing.Size(49, 17);
            this.chk_5K.TabIndex = 8;
            this.chk_5K.Text = "5 km";
            this.chk_5K.UseVisualStyleBackColor = true;
            // 
            // btn_StartStop
            // 
            this.btn_StartStop.Location = new System.Drawing.Point(12, 309);
            this.btn_StartStop.Name = "btn_StartStop";
            this.btn_StartStop.Size = new System.Drawing.Size(264, 23);
            this.btn_StartStop.TabIndex = 8;
            this.btn_StartStop.Text = "Start";
            this.btn_StartStop.UseVisualStyleBackColor = true;
            this.btn_StartStop.Click += new System.EventHandler(this.btn_StartStop_Click);
            // 
            // tim_Ticker
            // 
            this.tim_Ticker.Interval = 1000;
            this.tim_Ticker.Tick += new System.EventHandler(this.tim_Ticker_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "The PF PushBullet Companion was minimized! Double click this to open it again.";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "PF PushBullet Companion";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 341);
            this.Controls.Add(this.btn_StartStop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grp_Notifications);
            this.Controls.Add(this.grp_General);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(304, 380);
            this.MinimumSize = new System.Drawing.Size(304, 380);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "PF PB Companion";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grp_General.ResumeLayout(false);
            this.grp_General.PerformLayout();
            this.grp_Notifications.ResumeLayout(false);
            this.grp_Notifications.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_IV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_CP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_APIKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_pfLocation;
        private System.Windows.Forms.Button btn_pfSelector;
        private System.Windows.Forms.GroupBox grp_General;
        private System.Windows.Forms.GroupBox grp_Notifications;
        private System.Windows.Forms.CheckBox chk_Rare;
        private System.Windows.Forms.CheckBox chk_CPIV;
        private System.Windows.Forms.CheckBox chk_Bot;
        private System.Windows.Forms.CheckBox chk_Captcha;
        private System.Windows.Forms.CheckBox chk_Egg;
        private System.Windows.Forms.CheckBox chk_Level;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown num_IV;
        private System.Windows.Forms.NumericUpDown num_CP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_10K;
        private System.Windows.Forms.CheckBox chk_2K;
        private System.Windows.Forms.CheckBox chk_5K;
        private System.Windows.Forms.Button btn_StartStop;
        private System.Windows.Forms.Timer tim_Ticker;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

