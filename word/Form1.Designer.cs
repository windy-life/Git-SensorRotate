namespace word
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxAccuracy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxVoltage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxContoler = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxMeasuredAccuracy = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxCurrent = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxRange = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxModel = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品名称";
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(82, 28);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(123, 21);
            this.tbxName.TabIndex = 1;
            this.tbxName.Text = "倾角传感器";
            this.tbxName.TextChanged += new System.EventHandler(this.tbxName_TextChanged);
            // 
            // tbxCode
            // 
            this.tbxCode.Location = new System.Drawing.Point(82, 55);
            this.tbxCode.Name = "tbxCode";
            this.tbxCode.Size = new System.Drawing.Size(123, 21);
            this.tbxCode.TabIndex = 3;
            this.tbxCode.Text = "1111111";
            this.tbxCode.TextChanged += new System.EventHandler(this.tbxCode_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "产品编号";
            // 
            // tbxAccuracy
            // 
            this.tbxAccuracy.Location = new System.Drawing.Point(82, 109);
            this.tbxAccuracy.Name = "tbxAccuracy";
            this.tbxAccuracy.Size = new System.Drawing.Size(123, 21);
            this.tbxAccuracy.TabIndex = 7;
            this.tbxAccuracy.Text = "0.01";
            this.tbxAccuracy.TextChanged += new System.EventHandler(this.tbxAccuracy_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "要求精度";
            // 
            // tbxVoltage
            // 
            this.tbxVoltage.Location = new System.Drawing.Point(82, 82);
            this.tbxVoltage.Name = "tbxVoltage";
            this.tbxVoltage.Size = new System.Drawing.Size(123, 21);
            this.tbxVoltage.TabIndex = 5;
            this.tbxVoltage.Text = "24";
            this.tbxVoltage.TextChanged += new System.EventHandler(this.tbxVoltage_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "输入电压";
            // 
            // tbxContoler
            // 
            this.tbxContoler.Location = new System.Drawing.Point(82, 136);
            this.tbxContoler.Name = "tbxContoler";
            this.tbxContoler.Size = new System.Drawing.Size(123, 21);
            this.tbxContoler.TabIndex = 9;
            this.tbxContoler.Text = "sjf";
            this.tbxContoler.TextChanged += new System.EventHandler(this.tbxContoler_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "测试人员";
            // 
            // tbxDate
            // 
            this.tbxDate.Location = new System.Drawing.Point(322, 136);
            this.tbxDate.Name = "tbxDate";
            this.tbxDate.Size = new System.Drawing.Size(123, 21);
            this.tbxDate.TabIndex = 19;
            this.tbxDate.TextChanged += new System.EventHandler(this.tbxDate_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(263, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "测试日期";
            // 
            // tbxMeasuredAccuracy
            // 
            this.tbxMeasuredAccuracy.Location = new System.Drawing.Point(322, 109);
            this.tbxMeasuredAccuracy.Name = "tbxMeasuredAccuracy";
            this.tbxMeasuredAccuracy.Size = new System.Drawing.Size(123, 21);
            this.tbxMeasuredAccuracy.TabIndex = 17;
            this.tbxMeasuredAccuracy.Text = "0.008";
            this.tbxMeasuredAccuracy.TextChanged += new System.EventHandler(this.tbxMeasuredAccuracy_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "实测精度";
            // 
            // tbxCurrent
            // 
            this.tbxCurrent.Location = new System.Drawing.Point(322, 82);
            this.tbxCurrent.Name = "tbxCurrent";
            this.tbxCurrent.Size = new System.Drawing.Size(123, 21);
            this.tbxCurrent.TabIndex = 15;
            this.tbxCurrent.Text = "1";
            this.tbxCurrent.TextChanged += new System.EventHandler(this.tbxCurrent_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(263, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "输入电流";
            // 
            // tbxRange
            // 
            this.tbxRange.Location = new System.Drawing.Point(322, 55);
            this.tbxRange.Name = "tbxRange";
            this.tbxRange.Size = new System.Drawing.Size(123, 21);
            this.tbxRange.TabIndex = 13;
            this.tbxRange.Text = "60";
            this.tbxRange.TextChanged += new System.EventHandler(this.tbxRange_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(263, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "测量范围";
            // 
            // tbxModel
            // 
            this.tbxModel.Location = new System.Drawing.Point(322, 28);
            this.tbxModel.Name = "tbxModel";
            this.tbxModel.Size = new System.Drawing.Size(123, 21);
            this.tbxModel.TabIndex = 11;
            this.tbxModel.Text = "SNAG2000S-D15DTR13";
            this.tbxModel.TextChanged += new System.EventHandler(this.tbxModel_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(263, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "产品型号";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(82, 295);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(96, 30);
            this.btnGenerate.TabIndex = 20;
            this.btnGenerate.Text = "生成文档";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(265, 295);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(96, 30);
            this.btnOpen.TabIndex = 21;
            this.btnOpen.Text = "打开文档";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 407);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbxDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxMeasuredAccuracy);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxCurrent);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbxRange);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbxModel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbxContoler);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxAccuracy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxVoltage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxAccuracy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxVoltage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxContoler;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxMeasuredAccuracy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxCurrent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxRange;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxModel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnOpen;
    }
}

