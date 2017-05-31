namespace Sensor_Rotate
{
    partial class Form_Rotate303Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Rotate303Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.button_updatePorts = new System.Windows.Forms.Button();
            this.button_OpenPorts = new System.Windows.Forms.Button();
            this.button_Communicate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_XDebugZero = new System.Windows.Forms.CheckBox();
            this.pictureBox_XZero = new System.Windows.Forms.PictureBox();
            this.pictureBox_XEnable = new System.Windows.Forms.PictureBox();
            this.pictureBox_XPower = new System.Windows.Forms.PictureBox();
            this.button_XToZero = new System.Windows.Forms.Button();
            this.button_XConnect = new System.Windows.Forms.Button();
            this.button_XPower = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_YDebugZero = new System.Windows.Forms.CheckBox();
            this.pictureBox_YZero = new System.Windows.Forms.PictureBox();
            this.pictureBox_YEnable = new System.Windows.Forms.PictureBox();
            this.pictureBox_YPower = new System.Windows.Forms.PictureBox();
            this.button_YToZero = new System.Windows.Forms.Button();
            this.button_YConnect = new System.Windows.Forms.Button();
            this.button_YPower = new System.Windows.Forms.Button();
            this.comboBox_SerialPort = new System.Windows.Forms.ComboBox();
            this.pictureBox_Communicaate = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_XZero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_XEnable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_XPower)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_YZero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_YEnable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_YPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Communicaate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择转台串口：";
            // 
            // button_updatePorts
            // 
            this.button_updatePorts.Location = new System.Drawing.Point(12, 39);
            this.button_updatePorts.Name = "button_updatePorts";
            this.button_updatePorts.Size = new System.Drawing.Size(78, 26);
            this.button_updatePorts.TabIndex = 2;
            this.button_updatePorts.Text = "刷新串口";
            this.button_updatePorts.UseVisualStyleBackColor = true;
            this.button_updatePorts.Click += new System.EventHandler(this.button_updatePorts_Click);
            // 
            // button_OpenPorts
            // 
            this.button_OpenPorts.Location = new System.Drawing.Point(100, 39);
            this.button_OpenPorts.Name = "button_OpenPorts";
            this.button_OpenPorts.Size = new System.Drawing.Size(78, 26);
            this.button_OpenPorts.TabIndex = 3;
            this.button_OpenPorts.Text = "打开串口";
            this.button_OpenPorts.UseVisualStyleBackColor = true;
            this.button_OpenPorts.Click += new System.EventHandler(this.button_OpenPorts_Click);
            // 
            // button_Communicate
            // 
            this.button_Communicate.Location = new System.Drawing.Point(100, 71);
            this.button_Communicate.Name = "button_Communicate";
            this.button_Communicate.Size = new System.Drawing.Size(78, 26);
            this.button_Communicate.TabIndex = 4;
            this.button_Communicate.Text = "连接";
            this.button_Communicate.UseVisualStyleBackColor = true;
            this.button_Communicate.Click += new System.EventHandler(this.button_Communicate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_XDebugZero);
            this.groupBox3.Controls.Add(this.pictureBox_XZero);
            this.groupBox3.Controls.Add(this.pictureBox_XEnable);
            this.groupBox3.Controls.Add(this.pictureBox_XPower);
            this.groupBox3.Controls.Add(this.button_XToZero);
            this.groupBox3.Controls.Add(this.button_XConnect);
            this.groupBox3.Controls.Add(this.button_XPower);
            this.groupBox3.Location = new System.Drawing.Point(14, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(181, 137);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "X轴设置";
            // 
            // checkBox_XDebugZero
            // 
            this.checkBox_XDebugZero.AutoSize = true;
            this.checkBox_XDebugZero.Location = new System.Drawing.Point(86, 113);
            this.checkBox_XDebugZero.Name = "checkBox_XDebugZero";
            this.checkBox_XDebugZero.Size = new System.Drawing.Size(90, 16);
            this.checkBox_XDebugZero.TabIndex = 17;
            this.checkBox_XDebugZero.Text = "调试-已寻零";
            this.checkBox_XDebugZero.UseVisualStyleBackColor = true;
            this.checkBox_XDebugZero.CheckedChanged += new System.EventHandler(this.checkBox_DebugZero_CheckedChanged);
            // 
            // pictureBox_XZero
            // 
            this.pictureBox_XZero.Image = global::Sensor_Rotate.Resource1.寻零;
            this.pictureBox_XZero.Location = new System.Drawing.Point(23, 84);
            this.pictureBox_XZero.Name = "pictureBox_XZero";
            this.pictureBox_XZero.Size = new System.Drawing.Size(33, 26);
            this.pictureBox_XZero.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_XZero.TabIndex = 19;
            this.pictureBox_XZero.TabStop = false;
            // 
            // pictureBox_XEnable
            // 
            this.pictureBox_XEnable.Image = global::Sensor_Rotate.Resource1.闲置;
            this.pictureBox_XEnable.Location = new System.Drawing.Point(23, 52);
            this.pictureBox_XEnable.Name = "pictureBox_XEnable";
            this.pictureBox_XEnable.Size = new System.Drawing.Size(33, 26);
            this.pictureBox_XEnable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_XEnable.TabIndex = 18;
            this.pictureBox_XEnable.TabStop = false;
            // 
            // pictureBox_XPower
            // 
            this.pictureBox_XPower.Image = global::Sensor_Rotate.Resource1.PowerOff;
            this.pictureBox_XPower.Location = new System.Drawing.Point(23, 20);
            this.pictureBox_XPower.Name = "pictureBox_XPower";
            this.pictureBox_XPower.Size = new System.Drawing.Size(33, 26);
            this.pictureBox_XPower.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_XPower.TabIndex = 17;
            this.pictureBox_XPower.TabStop = false;
            // 
            // button_XToZero
            // 
            this.button_XToZero.Location = new System.Drawing.Point(86, 84);
            this.button_XToZero.Name = "button_XToZero";
            this.button_XToZero.Size = new System.Drawing.Size(78, 26);
            this.button_XToZero.TabIndex = 7;
            this.button_XToZero.Text = "寻零";
            this.button_XToZero.UseVisualStyleBackColor = true;
            this.button_XToZero.Click += new System.EventHandler(this.button_XToZero_Click);
            // 
            // button_XConnect
            // 
            this.button_XConnect.Location = new System.Drawing.Point(86, 52);
            this.button_XConnect.Name = "button_XConnect";
            this.button_XConnect.Size = new System.Drawing.Size(78, 26);
            this.button_XConnect.TabIndex = 6;
            this.button_XConnect.Text = "闭合";
            this.button_XConnect.UseVisualStyleBackColor = true;
            this.button_XConnect.Click += new System.EventHandler(this.button_XConnect_Click);
            // 
            // button_XPower
            // 
            this.button_XPower.Location = new System.Drawing.Point(86, 20);
            this.button_XPower.Name = "button_XPower";
            this.button_XPower.Size = new System.Drawing.Size(78, 26);
            this.button_XPower.TabIndex = 5;
            this.button_XPower.Text = "上电";
            this.button_XPower.UseVisualStyleBackColor = true;
            this.button_XPower.Click += new System.EventHandler(this.button_XPower_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_YDebugZero);
            this.groupBox1.Controls.Add(this.pictureBox_YZero);
            this.groupBox1.Controls.Add(this.pictureBox_YEnable);
            this.groupBox1.Controls.Add(this.pictureBox_YPower);
            this.groupBox1.Controls.Add(this.button_YToZero);
            this.groupBox1.Controls.Add(this.button_YConnect);
            this.groupBox1.Controls.Add(this.button_YPower);
            this.groupBox1.Location = new System.Drawing.Point(14, 246);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 137);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Y轴设置";
            // 
            // checkBox_YDebugZero
            // 
            this.checkBox_YDebugZero.AutoSize = true;
            this.checkBox_YDebugZero.Location = new System.Drawing.Point(86, 113);
            this.checkBox_YDebugZero.Name = "checkBox_YDebugZero";
            this.checkBox_YDebugZero.Size = new System.Drawing.Size(90, 16);
            this.checkBox_YDebugZero.TabIndex = 20;
            this.checkBox_YDebugZero.Text = "调试-已寻零";
            this.checkBox_YDebugZero.UseVisualStyleBackColor = true;
            this.checkBox_YDebugZero.CheckedChanged += new System.EventHandler(this.checkBox_YDebugZero_CheckedChanged);
            // 
            // pictureBox_YZero
            // 
            this.pictureBox_YZero.Image = global::Sensor_Rotate.Resource1.寻零;
            this.pictureBox_YZero.Location = new System.Drawing.Point(23, 84);
            this.pictureBox_YZero.Name = "pictureBox_YZero";
            this.pictureBox_YZero.Size = new System.Drawing.Size(33, 26);
            this.pictureBox_YZero.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_YZero.TabIndex = 19;
            this.pictureBox_YZero.TabStop = false;
            // 
            // pictureBox_YEnable
            // 
            this.pictureBox_YEnable.Image = global::Sensor_Rotate.Resource1.闲置;
            this.pictureBox_YEnable.Location = new System.Drawing.Point(23, 52);
            this.pictureBox_YEnable.Name = "pictureBox_YEnable";
            this.pictureBox_YEnable.Size = new System.Drawing.Size(33, 26);
            this.pictureBox_YEnable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_YEnable.TabIndex = 18;
            this.pictureBox_YEnable.TabStop = false;
            // 
            // pictureBox_YPower
            // 
            this.pictureBox_YPower.Image = global::Sensor_Rotate.Resource1.PowerOff;
            this.pictureBox_YPower.Location = new System.Drawing.Point(23, 20);
            this.pictureBox_YPower.Name = "pictureBox_YPower";
            this.pictureBox_YPower.Size = new System.Drawing.Size(33, 26);
            this.pictureBox_YPower.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_YPower.TabIndex = 17;
            this.pictureBox_YPower.TabStop = false;
            // 
            // button_YToZero
            // 
            this.button_YToZero.Location = new System.Drawing.Point(86, 84);
            this.button_YToZero.Name = "button_YToZero";
            this.button_YToZero.Size = new System.Drawing.Size(78, 26);
            this.button_YToZero.TabIndex = 7;
            this.button_YToZero.Text = "寻零";
            this.button_YToZero.UseVisualStyleBackColor = true;
            this.button_YToZero.Click += new System.EventHandler(this.button_YToZero_Click);
            // 
            // button_YConnect
            // 
            this.button_YConnect.Location = new System.Drawing.Point(86, 52);
            this.button_YConnect.Name = "button_YConnect";
            this.button_YConnect.Size = new System.Drawing.Size(78, 26);
            this.button_YConnect.TabIndex = 6;
            this.button_YConnect.Text = "闭合";
            this.button_YConnect.UseVisualStyleBackColor = true;
            this.button_YConnect.Click += new System.EventHandler(this.button_YConnect_Click);
            // 
            // button_YPower
            // 
            this.button_YPower.Location = new System.Drawing.Point(86, 20);
            this.button_YPower.Name = "button_YPower";
            this.button_YPower.Size = new System.Drawing.Size(78, 26);
            this.button_YPower.TabIndex = 5;
            this.button_YPower.Text = "上电";
            this.button_YPower.UseVisualStyleBackColor = true;
            this.button_YPower.Click += new System.EventHandler(this.button_YPower_Click);
            // 
            // comboBox_SerialPort
            // 
            this.comboBox_SerialPort.FormattingEnabled = true;
            this.comboBox_SerialPort.Location = new System.Drawing.Point(104, 12);
            this.comboBox_SerialPort.Name = "comboBox_SerialPort";
            this.comboBox_SerialPort.Size = new System.Drawing.Size(74, 20);
            this.comboBox_SerialPort.TabIndex = 15;
            // 
            // pictureBox_Communicaate
            // 
            this.pictureBox_Communicaate.Image = global::Sensor_Rotate.Resource1.disconnect;
            this.pictureBox_Communicaate.Location = new System.Drawing.Point(37, 71);
            this.pictureBox_Communicaate.Name = "pictureBox_Communicaate";
            this.pictureBox_Communicaate.Size = new System.Drawing.Size(33, 26);
            this.pictureBox_Communicaate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Communicaate.TabIndex = 16;
            this.pictureBox_Communicaate.TabStop = false;
            // 
            // Form_Rotate303Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(254, 491);
            this.Controls.Add(this.pictureBox_Communicaate);
            this.Controls.Add(this.comboBox_SerialPort);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button_Communicate);
            this.Controls.Add(this.button_OpenPorts);
            this.Controls.Add(this.button_updatePorts);
            this.Controls.Add(this.label1);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Rotate303Settings";
            this.TabText = "转台初始化";
            this.Text = "转台初始化";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_XZero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_XEnable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_XPower)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_YZero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_YEnable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_YPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Communicaate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_updatePorts;
        private System.Windows.Forms.Button button_OpenPorts;
        private System.Windows.Forms.Button button_Communicate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_XConnect;
        private System.Windows.Forms.Button button_XPower;
        private System.Windows.Forms.Button button_XToZero;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_YToZero;
        private System.Windows.Forms.Button button_YConnect;
        private System.Windows.Forms.Button button_YPower;
        private System.Windows.Forms.ComboBox comboBox_SerialPort;
        private System.Windows.Forms.PictureBox pictureBox_Communicaate;
        private System.Windows.Forms.PictureBox pictureBox_XZero;
        private System.Windows.Forms.PictureBox pictureBox_XEnable;
        private System.Windows.Forms.PictureBox pictureBox_XPower;
        private System.Windows.Forms.PictureBox pictureBox_YZero;
        private System.Windows.Forms.PictureBox pictureBox_YEnable;
        private System.Windows.Forms.PictureBox pictureBox_YPower;
        private System.Windows.Forms.CheckBox checkBox_XDebugZero;
        private System.Windows.Forms.CheckBox checkBox_YDebugZero;
    }
}