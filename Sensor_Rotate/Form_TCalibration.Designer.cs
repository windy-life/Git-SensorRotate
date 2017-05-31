namespace Sensor_Rotate
{
    partial class Form_TCalibration
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
            this.label1 = new System.Windows.Forms.Label();
            this.button_TZero = new System.Windows.Forms.Button();
            this.button_TGainC = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_TEnum1 = new System.Windows.Forms.ComboBox();
            this.button_TC1 = new System.Windows.Forms.Button();
            this.button_TC2 = new System.Windows.Forms.Button();
            this.comboBox_TEnum2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_State = new System.Windows.Forms.TextBox();
            this.textBox_Data = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "传感器测量温度应为0度";
            // 
            // button_TZero
            // 
            this.button_TZero.Location = new System.Drawing.Point(167, 12);
            this.button_TZero.Name = "button_TZero";
            this.button_TZero.Size = new System.Drawing.Size(97, 27);
            this.button_TZero.TabIndex = 1;
            this.button_TZero.Text = "温度零位标定";
            this.button_TZero.UseVisualStyleBackColor = true;
            this.button_TZero.Click += new System.EventHandler(this.button_TZero_Click);
            // 
            // button_TGainC
            // 
            this.button_TGainC.Location = new System.Drawing.Point(167, 62);
            this.button_TGainC.Name = "button_TGainC";
            this.button_TGainC.Size = new System.Drawing.Size(115, 27);
            this.button_TGainC.TabIndex = 3;
            this.button_TGainC.Text = "温度增益参数标定";
            this.button_TGainC.UseVisualStyleBackColor = true;
            this.button_TGainC.Click += new System.EventHandler(this.button_TGainC_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "60度温度增益参数标定";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "输入当前温箱温度（整数）";
            // 
            // comboBox_TEnum1
            // 
            this.comboBox_TEnum1.FormattingEnabled = true;
            this.comboBox_TEnum1.Items.AddRange(new object[] {
            "60",
            "40",
            "20",
            "0",
            "-20",
            "-40"});
            this.comboBox_TEnum1.Location = new System.Drawing.Point(167, 123);
            this.comboBox_TEnum1.Name = "comboBox_TEnum1";
            this.comboBox_TEnum1.Size = new System.Drawing.Size(56, 20);
            this.comboBox_TEnum1.TabIndex = 5;
            this.comboBox_TEnum1.Text = "60";
            // 
            // button_TC1
            // 
            this.button_TC1.Location = new System.Drawing.Point(229, 119);
            this.button_TC1.Name = "button_TC1";
            this.button_TC1.Size = new System.Drawing.Size(140, 27);
            this.button_TC1.TabIndex = 6;
            this.button_TC1.Text = "温度二次插补参数标定";
            this.button_TC1.UseVisualStyleBackColor = true;
            this.button_TC1.Click += new System.EventHandler(this.button_TC1_Click);
            // 
            // button_TC2
            // 
            this.button_TC2.Location = new System.Drawing.Point(229, 166);
            this.button_TC2.Name = "button_TC2";
            this.button_TC2.Size = new System.Drawing.Size(131, 39);
            this.button_TC2.TabIndex = 9;
            this.button_TC2.Text = "X、Y角度传感器温度零位二次插补参数标定";
            this.button_TC2.UseVisualStyleBackColor = true;
            this.button_TC2.Click += new System.EventHandler(this.button_TC2_Click);
            // 
            // comboBox_TEnum2
            // 
            this.comboBox_TEnum2.FormattingEnabled = true;
            this.comboBox_TEnum2.Items.AddRange(new object[] {
            "-40",
            "-20",
            "0",
            "20",
            "40",
            "60"});
            this.comboBox_TEnum2.Location = new System.Drawing.Point(167, 176);
            this.comboBox_TEnum2.Name = "comboBox_TEnum2";
            this.comboBox_TEnum2.Size = new System.Drawing.Size(56, 20);
            this.comboBox_TEnum2.TabIndex = 8;
            this.comboBox_TEnum2.Text = "-40";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "输入当前温箱温度（整数）";
            // 
            // textBox_State
            // 
            this.textBox_State.Location = new System.Drawing.Point(261, 237);
            this.textBox_State.Name = "textBox_State";
            this.textBox_State.Size = new System.Drawing.Size(296, 21);
            this.textBox_State.TabIndex = 17;
            this.textBox_State.Text = "显示状态";
            // 
            // textBox_Data
            // 
            this.textBox_Data.Location = new System.Drawing.Point(14, 237);
            this.textBox_Data.Name = "textBox_Data";
            this.textBox_Data.Size = new System.Drawing.Size(197, 21);
            this.textBox_Data.TabIndex = 16;
            this.textBox_Data.Text = "显示数据指令";
            // 
            // Form_TCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 458);
            this.Controls.Add(this.textBox_State);
            this.Controls.Add(this.textBox_Data);
            this.Controls.Add(this.button_TC2);
            this.Controls.Add(this.comboBox_TEnum2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_TC1);
            this.Controls.Add(this.comboBox_TEnum1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_TGainC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_TZero);
            this.Controls.Add(this.label1);
            this.HideOnClose = true;
            this.Name = "Form_TCalibration";
            this.TabText = "温度标定";
            this.Text = "温度标定";
            this.Load += new System.EventHandler(this.Form_TCalibration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_TZero;
        private System.Windows.Forms.Button button_TGainC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_TEnum1;
        private System.Windows.Forms.Button button_TC1;
        private System.Windows.Forms.Button button_TC2;
        private System.Windows.Forms.ComboBox comboBox_TEnum2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_State;
        private System.Windows.Forms.TextBox textBox_Data;
    }
}