namespace Sensor_Rotate
{
    partial class Form_ProductTree
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("配置通讯", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("读写参数", 3, 3);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Y标定", 5, 5);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("X标定", 5, 5);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("温度标定", 4, 4);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("SANG2000S-D15DTR", 6, 6, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("配置通讯", 2, 2);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("读写参数", 3, 3);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Y标定", 5, 5);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("X标定", 5, 5);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("温度标定", 4, 4);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("SANG2000S-D30DTR", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("SANG2000S", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("动态水平仪");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("倾角传感器", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("TCM", 1, 1);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("罗盘", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode16});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ProductTree));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "SANG2000S-D15DTR_Settings";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "配置通讯";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "SANG2000S-D15DTR_ReadP";
            treeNode2.SelectedImageIndex = 3;
            treeNode2.Text = "读写参数";
            treeNode3.ImageIndex = 5;
            treeNode3.Name = "SANG2000S-D15DTR_Y";
            treeNode3.SelectedImageIndex = 5;
            treeNode3.Text = "Y标定";
            treeNode4.ImageIndex = 5;
            treeNode4.Name = "SANG2000S-D15DTR_X";
            treeNode4.SelectedImageIndex = 5;
            treeNode4.Text = "X标定";
            treeNode5.ImageIndex = 4;
            treeNode5.Name = "SANG2000S-D15DTR_T";
            treeNode5.SelectedImageIndex = 4;
            treeNode5.Text = "温度标定";
            treeNode6.ImageIndex = 6;
            treeNode6.Name = "SANG2000S-D15DTR";
            treeNode6.SelectedImageIndex = 6;
            treeNode6.Text = "SANG2000S-D15DTR";
            treeNode7.ImageIndex = 2;
            treeNode7.Name = "SANG2000S-D30DTR_Settings";
            treeNode7.SelectedImageIndex = 2;
            treeNode7.Text = "配置通讯";
            treeNode8.ImageIndex = 3;
            treeNode8.Name = "SANG2000S-D30DTR_ReadP";
            treeNode8.SelectedImageIndex = 3;
            treeNode8.Text = "读写参数";
            treeNode9.ImageIndex = 5;
            treeNode9.Name = "SANG2000S-D30DTR_Y";
            treeNode9.SelectedImageIndex = 5;
            treeNode9.Text = "Y标定";
            treeNode10.ImageIndex = 5;
            treeNode10.Name = "SANG2000S-D30DTR_X";
            treeNode10.SelectedImageIndex = 5;
            treeNode10.Text = "X标定";
            treeNode11.ImageIndex = 4;
            treeNode11.Name = "SANG2000S-D30DTR_T";
            treeNode11.SelectedImageIndex = 4;
            treeNode11.Text = "温度标定";
            treeNode12.Name = "SANG2000S-D30DTR";
            treeNode12.Text = "SANG2000S-D30DTR";
            treeNode13.Name = "SANG2000S";
            treeNode13.Text = "SANG2000S";
            treeNode14.Name = "DANG120-CC2";
            treeNode14.Text = "动态水平仪";
            treeNode15.ImageIndex = 0;
            treeNode15.Name = "倾角传感器";
            treeNode15.SelectedImageIndex = 0;
            treeNode15.Text = "倾角传感器";
            treeNode16.ImageIndex = 1;
            treeNode16.Name = "TCM";
            treeNode16.SelectedImageIndex = 1;
            treeNode16.Text = "TCM";
            treeNode17.ImageIndex = 1;
            treeNode17.Name = "罗盘";
            treeNode17.SelectedImageIndex = 1;
            treeNode17.Text = "罗盘";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode17});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(277, 657);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Angle.ico");
            this.imageList1.Images.SetKeyName(1, "Compass.ico");
            this.imageList1.Images.SetKeyName(2, "配置通讯.jpg");
            this.imageList1.Images.SetKeyName(3, "读写.png");
            this.imageList1.Images.SetKeyName(4, "温度.png");
            this.imageList1.Images.SetKeyName(5, "轴.png");
            this.imageList1.Images.SetKeyName(6, "SANG2000S-D15.JPG");
            // 
            // Form_ProductTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(333, 513);
            this.Controls.Add(this.treeView1);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ProductTree";
            this.TabText = "产品";
            this.Text = "产品";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}