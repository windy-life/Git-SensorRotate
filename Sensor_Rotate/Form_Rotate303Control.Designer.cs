namespace Sensor_Rotate
{
    partial class Form_Rotate303Control
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Rotate303Control));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton_XModeS = new System.Windows.Forms.RadioButton();
            this.radioButton_XModeP = new System.Windows.Forms.RadioButton();
            this.radioButton_XStop = new System.Windows.Forms.RadioButton();
            this.groupBox_XPosition = new System.Windows.Forms.GroupBox();
            this.radioButton_XAP = new System.Windows.Forms.RadioButton();
            this.radioButton_XRP = new System.Windows.Forms.RadioButton();
            this.label_X1 = new System.Windows.Forms.Label();
            this.textBox_X1 = new System.Windows.Forms.TextBox();
            this.textBox_X2 = new System.Windows.Forms.TextBox();
            this.label_X2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_XState = new System.Windows.Forms.TextBox();
            this.textBox_XSpeed = new System.Windows.Forms.TextBox();
            this.textBox_XPosition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_YState = new System.Windows.Forms.TextBox();
            this.textBox_YSpeed = new System.Windows.Forms.TextBox();
            this.textBox_YPosition = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_Y2 = new System.Windows.Forms.TextBox();
            this.label_Y2 = new System.Windows.Forms.Label();
            this.textBox_Y1 = new System.Windows.Forms.TextBox();
            this.label_Y1 = new System.Windows.Forms.Label();
            this.groupBox_YPosition = new System.Windows.Forms.GroupBox();
            this.radioButton_YAP = new System.Windows.Forms.RadioButton();
            this.radioButton_YRP = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButton_YModeS = new System.Windows.Forms.RadioButton();
            this.radioButton_YModeP = new System.Windows.Forms.RadioButton();
            this.radioButton_YStop = new System.Windows.Forms.RadioButton();
            this.button_XRun = new System.Windows.Forms.Button();
            this.button_YRun = new System.Windows.Forms.Button();
            this.button_EmergencyStop = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox_XPosition.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox_YPosition.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton_XModeS);
            this.groupBox3.Controls.Add(this.radioButton_XModeP);
            this.groupBox3.Controls.Add(this.radioButton_XStop);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(88, 90);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "X轴运行模式";
            // 
            // radioButton_XModeS
            // 
            this.radioButton_XModeS.AutoSize = true;
            this.radioButton_XModeS.Location = new System.Drawing.Point(6, 64);
            this.radioButton_XModeS.Name = "radioButton_XModeS";
            this.radioButton_XModeS.Size = new System.Drawing.Size(71, 16);
            this.radioButton_XModeS.TabIndex = 2;
            this.radioButton_XModeS.Text = "速率模式";
            this.radioButton_XModeS.UseVisualStyleBackColor = true;
            this.radioButton_XModeS.CheckedChanged += new System.EventHandler(this.radioButton_XModeS_CheckedChanged);
            // 
            // radioButton_XModeP
            // 
            this.radioButton_XModeP.AutoSize = true;
            this.radioButton_XModeP.Location = new System.Drawing.Point(6, 42);
            this.radioButton_XModeP.Name = "radioButton_XModeP";
            this.radioButton_XModeP.Size = new System.Drawing.Size(71, 16);
            this.radioButton_XModeP.TabIndex = 1;
            this.radioButton_XModeP.Text = "位置模式";
            this.radioButton_XModeP.UseVisualStyleBackColor = true;
            this.radioButton_XModeP.CheckedChanged += new System.EventHandler(this.radioButton_XModeP_CheckedChanged);
            // 
            // radioButton_XStop
            // 
            this.radioButton_XStop.AutoSize = true;
            this.radioButton_XStop.Checked = true;
            this.radioButton_XStop.Location = new System.Drawing.Point(6, 20);
            this.radioButton_XStop.Name = "radioButton_XStop";
            this.radioButton_XStop.Size = new System.Drawing.Size(47, 16);
            this.radioButton_XStop.TabIndex = 0;
            this.radioButton_XStop.TabStop = true;
            this.radioButton_XStop.Text = "停止";
            this.radioButton_XStop.UseVisualStyleBackColor = true;
            this.radioButton_XStop.CheckedChanged += new System.EventHandler(this.radioButton_XStop_CheckedChanged);
            // 
            // groupBox_XPosition
            // 
            this.groupBox_XPosition.Controls.Add(this.radioButton_XAP);
            this.groupBox_XPosition.Controls.Add(this.radioButton_XRP);
            this.groupBox_XPosition.Location = new System.Drawing.Point(106, 12);
            this.groupBox_XPosition.Name = "groupBox_XPosition";
            this.groupBox_XPosition.Size = new System.Drawing.Size(88, 90);
            this.groupBox_XPosition.TabIndex = 13;
            this.groupBox_XPosition.TabStop = false;
            this.groupBox_XPosition.Text = "位置模式";
            this.groupBox_XPosition.EnabledChanged += new System.EventHandler(this.groupBox_XPosition_EnabledChanged);
            // 
            // radioButton_XAP
            // 
            this.radioButton_XAP.AutoSize = true;
            this.radioButton_XAP.Location = new System.Drawing.Point(6, 42);
            this.radioButton_XAP.Name = "radioButton_XAP";
            this.radioButton_XAP.Size = new System.Drawing.Size(71, 16);
            this.radioButton_XAP.TabIndex = 1;
            this.radioButton_XAP.Text = "绝对位置";
            this.radioButton_XAP.UseVisualStyleBackColor = true;
            this.radioButton_XAP.CheckedChanged += new System.EventHandler(this.radioButton_XAP_CheckedChanged);
            // 
            // radioButton_XRP
            // 
            this.radioButton_XRP.AutoSize = true;
            this.radioButton_XRP.Checked = true;
            this.radioButton_XRP.Location = new System.Drawing.Point(6, 20);
            this.radioButton_XRP.Name = "radioButton_XRP";
            this.radioButton_XRP.Size = new System.Drawing.Size(71, 16);
            this.radioButton_XRP.TabIndex = 0;
            this.radioButton_XRP.TabStop = true;
            this.radioButton_XRP.Text = "相对位置";
            this.radioButton_XRP.UseVisualStyleBackColor = true;
            this.radioButton_XRP.CheckedChanged += new System.EventHandler(this.radioButton_XRP_CheckedChanged);
            // 
            // label_X1
            // 
            this.label_X1.AutoSize = true;
            this.label_X1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_X1.Location = new System.Drawing.Point(10, 115);
            this.label_X1.Name = "label_X1";
            this.label_X1.Size = new System.Drawing.Size(0, 12);
            this.label_X1.TabIndex = 14;
            // 
            // textBox_X1
            // 
            this.textBox_X1.Location = new System.Drawing.Point(136, 112);
            this.textBox_X1.Name = "textBox_X1";
            this.textBox_X1.Size = new System.Drawing.Size(58, 21);
            this.textBox_X1.TabIndex = 15;
            // 
            // textBox_X2
            // 
            this.textBox_X2.Location = new System.Drawing.Point(136, 146);
            this.textBox_X2.Name = "textBox_X2";
            this.textBox_X2.Size = new System.Drawing.Size(58, 21);
            this.textBox_X2.TabIndex = 17;
            // 
            // label_X2
            // 
            this.label_X2.AutoSize = true;
            this.label_X2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_X2.Location = new System.Drawing.Point(10, 149);
            this.label_X2.Name = "label_X2";
            this.label_X2.Size = new System.Drawing.Size(0, 12);
            this.label_X2.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_XState);
            this.groupBox2.Controls.Add(this.textBox_XSpeed);
            this.groupBox2.Controls.Add(this.textBox_XPosition);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 98);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "X轴运行状态";
            // 
            // textBox_XState
            // 
            this.textBox_XState.Location = new System.Drawing.Point(53, 71);
            this.textBox_XState.Name = "textBox_XState";
            this.textBox_XState.Size = new System.Drawing.Size(123, 21);
            this.textBox_XState.TabIndex = 20;
            // 
            // textBox_XSpeed
            // 
            this.textBox_XSpeed.Location = new System.Drawing.Point(104, 44);
            this.textBox_XSpeed.Name = "textBox_XSpeed";
            this.textBox_XSpeed.Size = new System.Drawing.Size(72, 21);
            this.textBox_XSpeed.TabIndex = 19;
            // 
            // textBox_XPosition
            // 
            this.textBox_XPosition.Location = new System.Drawing.Point(104, 17);
            this.textBox_XPosition.Name = "textBox_XPosition";
            this.textBox_XPosition.Size = new System.Drawing.Size(72, 21);
            this.textBox_XPosition.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "速率：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "位置：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_YState);
            this.groupBox4.Controls.Add(this.textBox_YSpeed);
            this.groupBox4.Controls.Add(this.textBox_YPosition);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(12, 454);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 98);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Y轴运行状态";
            // 
            // textBox_YState
            // 
            this.textBox_YState.Location = new System.Drawing.Point(53, 71);
            this.textBox_YState.Name = "textBox_YState";
            this.textBox_YState.Size = new System.Drawing.Size(123, 21);
            this.textBox_YState.TabIndex = 20;
            // 
            // textBox_YSpeed
            // 
            this.textBox_YSpeed.Location = new System.Drawing.Point(104, 44);
            this.textBox_YSpeed.Name = "textBox_YSpeed";
            this.textBox_YSpeed.Size = new System.Drawing.Size(72, 21);
            this.textBox_YSpeed.TabIndex = 19;
            // 
            // textBox_YPosition
            // 
            this.textBox_YPosition.Location = new System.Drawing.Point(104, 17);
            this.textBox_YPosition.Name = "textBox_YPosition";
            this.textBox_YPosition.Size = new System.Drawing.Size(72, 21);
            this.textBox_YPosition.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "状态：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "速率：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "位置：";
            // 
            // textBox_Y2
            // 
            this.textBox_Y2.Location = new System.Drawing.Point(116, 427);
            this.textBox_Y2.Name = "textBox_Y2";
            this.textBox_Y2.Size = new System.Drawing.Size(72, 21);
            this.textBox_Y2.TabIndex = 24;
            // 
            // label_Y2
            // 
            this.label_Y2.AutoSize = true;
            this.label_Y2.Location = new System.Drawing.Point(18, 430);
            this.label_Y2.Name = "label_Y2";
            this.label_Y2.Size = new System.Drawing.Size(0, 12);
            this.label_Y2.TabIndex = 23;
            // 
            // textBox_Y1
            // 
            this.textBox_Y1.Location = new System.Drawing.Point(116, 393);
            this.textBox_Y1.Name = "textBox_Y1";
            this.textBox_Y1.Size = new System.Drawing.Size(72, 21);
            this.textBox_Y1.TabIndex = 22;
            // 
            // label_Y1
            // 
            this.label_Y1.AutoSize = true;
            this.label_Y1.Location = new System.Drawing.Point(18, 396);
            this.label_Y1.Name = "label_Y1";
            this.label_Y1.Size = new System.Drawing.Size(0, 12);
            this.label_Y1.TabIndex = 21;
            // 
            // groupBox_YPosition
            // 
            this.groupBox_YPosition.Controls.Add(this.radioButton_YAP);
            this.groupBox_YPosition.Controls.Add(this.radioButton_YRP);
            this.groupBox_YPosition.Location = new System.Drawing.Point(106, 291);
            this.groupBox_YPosition.Name = "groupBox_YPosition";
            this.groupBox_YPosition.Size = new System.Drawing.Size(88, 90);
            this.groupBox_YPosition.TabIndex = 20;
            this.groupBox_YPosition.TabStop = false;
            this.groupBox_YPosition.Text = "位置模式";
            // 
            // radioButton_YAP
            // 
            this.radioButton_YAP.AutoSize = true;
            this.radioButton_YAP.Location = new System.Drawing.Point(6, 42);
            this.radioButton_YAP.Name = "radioButton_YAP";
            this.radioButton_YAP.Size = new System.Drawing.Size(71, 16);
            this.radioButton_YAP.TabIndex = 1;
            this.radioButton_YAP.Text = "绝对位置";
            this.radioButton_YAP.UseVisualStyleBackColor = true;
            this.radioButton_YAP.CheckedChanged += new System.EventHandler(this.radioButton_YAP_CheckedChanged);
            // 
            // radioButton_YRP
            // 
            this.radioButton_YRP.AutoSize = true;
            this.radioButton_YRP.Checked = true;
            this.radioButton_YRP.Location = new System.Drawing.Point(6, 20);
            this.radioButton_YRP.Name = "radioButton_YRP";
            this.radioButton_YRP.Size = new System.Drawing.Size(71, 16);
            this.radioButton_YRP.TabIndex = 0;
            this.radioButton_YRP.TabStop = true;
            this.radioButton_YRP.Text = "相对位置";
            this.radioButton_YRP.UseVisualStyleBackColor = true;
            this.radioButton_YRP.CheckedChanged += new System.EventHandler(this.radioButton_YRP_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButton_YModeS);
            this.groupBox6.Controls.Add(this.radioButton_YModeP);
            this.groupBox6.Controls.Add(this.radioButton_YStop);
            this.groupBox6.Location = new System.Drawing.Point(12, 291);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(88, 90);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Y轴运行模式";
            // 
            // radioButton_YModeS
            // 
            this.radioButton_YModeS.AutoSize = true;
            this.radioButton_YModeS.Location = new System.Drawing.Point(6, 64);
            this.radioButton_YModeS.Name = "radioButton_YModeS";
            this.radioButton_YModeS.Size = new System.Drawing.Size(71, 16);
            this.radioButton_YModeS.TabIndex = 2;
            this.radioButton_YModeS.Text = "速率模式";
            this.radioButton_YModeS.UseVisualStyleBackColor = true;
            this.radioButton_YModeS.CheckedChanged += new System.EventHandler(this.radioButton_YModeS_CheckedChanged);
            // 
            // radioButton_YModeP
            // 
            this.radioButton_YModeP.AutoSize = true;
            this.radioButton_YModeP.Location = new System.Drawing.Point(6, 42);
            this.radioButton_YModeP.Name = "radioButton_YModeP";
            this.radioButton_YModeP.Size = new System.Drawing.Size(71, 16);
            this.radioButton_YModeP.TabIndex = 1;
            this.radioButton_YModeP.Text = "位置模式";
            this.radioButton_YModeP.UseVisualStyleBackColor = true;
            this.radioButton_YModeP.CheckedChanged += new System.EventHandler(this.radioButton_YModeP_CheckedChanged);
            // 
            // radioButton_YStop
            // 
            this.radioButton_YStop.AutoSize = true;
            this.radioButton_YStop.Checked = true;
            this.radioButton_YStop.Location = new System.Drawing.Point(6, 20);
            this.radioButton_YStop.Name = "radioButton_YStop";
            this.radioButton_YStop.Size = new System.Drawing.Size(47, 16);
            this.radioButton_YStop.TabIndex = 0;
            this.radioButton_YStop.TabStop = true;
            this.radioButton_YStop.Text = "停止";
            this.radioButton_YStop.UseVisualStyleBackColor = true;
            this.radioButton_YStop.CheckedChanged += new System.EventHandler(this.radioButton_YStop_CheckedChanged);
            // 
            // button_XRun
            // 
            this.button_XRun.BackColor = System.Drawing.Color.Yellow;
            this.button_XRun.Location = new System.Drawing.Point(200, 175);
            this.button_XRun.Name = "button_XRun";
            this.button_XRun.Size = new System.Drawing.Size(43, 47);
            this.button_XRun.TabIndex = 26;
            this.button_XRun.Text = "运行 X轴";
            this.button_XRun.UseVisualStyleBackColor = false;
            this.button_XRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_YRun
            // 
            this.button_YRun.BackColor = System.Drawing.Color.Lime;
            this.button_YRun.Location = new System.Drawing.Point(200, 311);
            this.button_YRun.Name = "button_YRun";
            this.button_YRun.Size = new System.Drawing.Size(43, 47);
            this.button_YRun.TabIndex = 27;
            this.button_YRun.Text = "运行 Y轴";
            this.button_YRun.UseVisualStyleBackColor = false;
            this.button_YRun.Click += new System.EventHandler(this.button_YRun_Click);
            // 
            // button_EmergencyStop
            // 
            this.button_EmergencyStop.BackColor = System.Drawing.Color.Red;
            this.button_EmergencyStop.Location = new System.Drawing.Point(200, 246);
            this.button_EmergencyStop.Name = "button_EmergencyStop";
            this.button_EmergencyStop.Size = new System.Drawing.Size(43, 47);
            this.button_EmergencyStop.TabIndex = 29;
            this.button_EmergencyStop.Text = "急停";
            this.button_EmergencyStop.UseVisualStyleBackColor = false;
            this.button_EmergencyStop.Click += new System.EventHandler(this.button_EmergencyStop_Click);
            // 
            // Form_Rotate303Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(261, 591);
            this.Controls.Add(this.button_EmergencyStop);
            this.Controls.Add(this.button_YRun);
            this.Controls.Add(this.button_XRun);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.textBox_Y2);
            this.Controls.Add(this.label_Y2);
            this.Controls.Add(this.textBox_Y1);
            this.Controls.Add(this.label_Y1);
            this.Controls.Add(this.groupBox_YPosition);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox_X2);
            this.Controls.Add(this.label_X2);
            this.Controls.Add(this.textBox_X1);
            this.Controls.Add(this.label_X1);
            this.Controls.Add(this.groupBox_XPosition);
            this.Controls.Add(this.groupBox3);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Rotate303Control";
            this.TabText = "转台控制";
            this.Text = "转台控制";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox_XPosition.ResumeLayout(false);
            this.groupBox_XPosition.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox_YPosition.ResumeLayout(false);
            this.groupBox_YPosition.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton_XModeS;
        private System.Windows.Forms.RadioButton radioButton_XModeP;
        private System.Windows.Forms.RadioButton radioButton_XStop;
        private System.Windows.Forms.GroupBox groupBox_XPosition;
        private System.Windows.Forms.RadioButton radioButton_XAP;
        private System.Windows.Forms.RadioButton radioButton_XRP;
        private System.Windows.Forms.Label label_X1;
        private System.Windows.Forms.TextBox textBox_X1;
        private System.Windows.Forms.TextBox textBox_X2;
        private System.Windows.Forms.Label label_X2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public  System.Windows.Forms.TextBox textBox_XState;
        private System.Windows.Forms.TextBox textBox_XSpeed;
        private System.Windows.Forms.GroupBox groupBox4;
        public  System.Windows.Forms.TextBox textBox_YState;
        private System.Windows.Forms.TextBox textBox_YSpeed;
        private System.Windows.Forms.TextBox textBox_YPosition;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_Y2;
        private System.Windows.Forms.Label label_Y2;
        private System.Windows.Forms.TextBox textBox_Y1;
        private System.Windows.Forms.Label label_Y1;
        private System.Windows.Forms.GroupBox groupBox_YPosition;
        private System.Windows.Forms.RadioButton radioButton_YAP;
        private System.Windows.Forms.RadioButton radioButton_YRP;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton_YModeS;
        private System.Windows.Forms.RadioButton radioButton_YModeP;
        private System.Windows.Forms.RadioButton radioButton_YStop;
        private System.Windows.Forms.Button button_XRun;
        private System.Windows.Forms.Button button_YRun;
        private System.Windows.Forms.Button button_EmergencyStop;
        private System.Windows.Forms.TextBox textBox_XPosition;
    }
}