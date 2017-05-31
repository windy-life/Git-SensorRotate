using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;

namespace Sensor_Rotate
{
    public partial class Form_Rotate303Control: DockContent
    {
        static public System.Timers.Timer T_UpdateData = new System.Timers.Timer(10);//更新数据显示
        public Form_Rotate303Control()
        {
            InitializeComponent();
            T_UpdateData.Elapsed += new System.Timers.ElapsedEventHandler(UpdateData);//指定到时执行函数
            //T_UpdateData.Enabled = true;//启动
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            groupBox_XPosition.Enabled = false;
            textBox_X1.Text = "10";
            textBox_X2.Text = "10";
            groupBox_YPosition.Enabled = false;
            textBox_Y1.Text = "10";
            textBox_Y2.Text = "10";
        }
        //定义变量
        private double Acc = 10;
        private double Set_XAcc = 0;//方位轴已经在运行（设置）的加速度
        private double Set_YAcc = 0;//俯仰轴已经在运行（设置）的加速度
        private int XRun_Option = 0;//0-停止；1-绝对位置；2-速率；3-相对位置
        private int YRun_Option = 0;//0-停止；1-绝对位置；2-速率；3-相对位置
        public static int X_SetRunWay = 0;//方位轴设置的运行方式（0-停止；1-绝对位置；2-速率；3-相对位置）
        public static int Y_SetRunWay = 0;//俯仰轴设置的运行方式（0-停止；1-绝对位置；2-速率；3-相对位置）
        
        
        //------------------------------------------
        //更新数据显示
        //------------------------------------------
        private void UpdateData(object source,System.Timers.ElapsedEventArgs e)        
        {
            Invoke(new MethodInvoker(delegate () { textBox_XPosition.Text = Form_Rotate303Settings.Xposition.ToString("0.0000"); }));
            Invoke(new MethodInvoker(delegate () { textBox_XSpeed.Text = Form_Rotate303Settings.Xspeed.ToString("0.0000"); }));
            Invoke(new MethodInvoker(delegate () { textBox_XState.Text = Form_Rotate303Settings.XState; }));
            Invoke(new MethodInvoker(delegate () { textBox_YPosition.Text = Form_Rotate303Settings.Yposition.ToString("0.0000"); }));
            Invoke(new MethodInvoker(delegate () { textBox_YSpeed.Text = Form_Rotate303Settings.Yspeed.ToString("0.0000"); }));
            Invoke(new MethodInvoker(delegate () { textBox_YState.Text = Form_Rotate303Settings.YState; }));

        }
        //------------------------------------------
        //方位轴运行
        //------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            if (Form_Rotate303Settings.SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            if (Form_Rotate303Settings.Control_State == false)
            {
                MessageBox.Show("请先连接转台！");
                return;
            }
            if (Form_Rotate303Settings.X_IsZero == false)
            {
                MessageBox.Show("请先给X轴寻零 ！");
                return;
            }
            if (textBox_X1.Text.Equals("") | textBox_X2.Text.Equals(""))
            {
                MessageBox.Show("参数不能为空！");
                return;
            }
            if (DialogResult.No == MessageBox.Show("运行方位轴？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            switch (XRun_Option)
            {
                case 0://停止
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XStop(), 0, 16);//停止
                    X_SetRunWay = 0;
                    break;
                case 1://绝对位置
                    #region
                    if (Convert.ToDouble(textBox_X1.Text)<-360 | Convert.ToDouble(textBox_X1.Text) > 360)
                    {
                        MessageBox.Show("方位轴角度范围为±360°！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_X2.Text) < 0.01 | Convert.ToDouble(textBox_X2.Text) > 400)
                    {
                        MessageBox.Show("方位轴速率范围为0.01 ～ 400°/s！");
                        return;
                    }
                    if ((X_SetRunWay != 1 && X_SetRunWay != 0) && Form_Rotate303Settings.X_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XSet(0, Convert.ToDouble(textBox_X1.Text), Convert.ToDouble(textBox_X2.Text), Acc), 0, 16);//设置绝对位置运行和方式
                    Form_Rotate303Settings.Delay(50);
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XRun_Position(), 0, 16);//设置绝对位置运行
                    X_SetRunWay = 1;
                    Form_Rotate303Settings.X_Run = true;
                    #endregion
                    break;
                case 2://速率
                    #region
                    //加速度
                    if (Convert.ToDouble(textBox_X2.Text) < 0.1 | Convert.ToDouble(textBox_X2.Text) > 100)
                    {
                        MessageBox.Show("方位轴加速度范围为0.1 ～ 100°/s²！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_X1.Text) < -400 | Convert.ToDouble(textBox_X1.Text) > 400)
                    {
                        MessageBox.Show("方位轴速率范围为±400°/s！");
                        return;
                    }
                    if ((X_SetRunWay != 2 && X_SetRunWay != 0) && Form_Rotate303Settings.X_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    if (X_SetRunWay == 0)
                    {
                        Set_XAcc = Convert.ToDouble(textBox_X2.Text);
                    }
                    else if (X_SetRunWay == 2)
                    {
                        if (Set_XAcc != Convert.ToDouble(textBox_X2.Text))
                        {
                            MessageBox.Show("转台运行中不能改变加速度！", "提示");
                            textBox_X2.Text = Set_XAcc.ToString("0.00");
                            return;
                        }
                    }
                    
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XSet(1, 0,Convert.ToDouble(textBox_X1.Text), Convert.ToDouble(textBox_X2.Text)), 0, 16);//设置速率运行和方式
                    Form_Rotate303Settings.Delay(50);
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XRun_Speed(), 0, 16);//设置速率运行
                    X_SetRunWay = 2;
                    Form_Rotate303Settings.X_Run = true;
                    #endregion
                    break;
                case 3://相对位置
                    #region
                    
                    if (Convert.ToDouble(textBox_X1.Text) < -360 | Convert.ToDouble(textBox_X1.Text) > 360)
                    {
                        MessageBox.Show("方位轴角度范围为±360°！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_X2.Text) < 0.01 | Convert.ToDouble(textBox_X2.Text) > 400)
                    {
                        MessageBox.Show("方位轴速率范围为0.01 ～ 400°/s！");
                        return;
                    }
                    if ((X_SetRunWay != 3 && X_SetRunWay != 0) && Form_Rotate303Settings.X_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XSet(2, Convert.ToDouble(textBox_X1.Text), Convert.ToDouble(textBox_X2.Text), Acc), 0, 16);//设置相对位置运行和方式
                    Form_Rotate303Settings.Delay(50);
                    //Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XRun_RPosition(), 0, 16);//设置相对位置运行
                    X_SetRunWay = 3;
                    Form_Rotate303Settings.X_Run = true;
                    #endregion
                    break;
            }
            
        }
        //------------------------------------------
        //选中停止
        //------------------------------------------
        private void radioButton_XStop_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_XStop.Checked == true)
            {
                label_X1.Text = "";
                label_X2.Text = "";
                groupBox_XPosition.Enabled = false;
                XRun_Option = 0;
            }
        }
        //------------------------------------------
        //选中位置
        //------------------------------------------
        private void radioButton_XModeP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_XModeP.Checked == true)
            {
                label_X1.Text = "位置±360°";
                label_X2.Text = "速度0.01 ～ 400°/s";
                groupBox_XPosition.Enabled = true;
                if (radioButton_XAP.Checked == true)
                {
                    XRun_Option = 1;
                }
                else
                {
                    XRun_Option = 3;
                }
            }
        }
        //------------------------------------------
        //选中绝对位置
        //------------------------------------------
        private void radioButton_XAP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_XAP.Checked == true)
            {
                XRun_Option = 1;
            }
            
        }
        //------------------------------------------
        //选中相对位置
        //------------------------------------------
        private void radioButton_XRP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_XRP.Checked == true)
            {
                XRun_Option = 3;
            }
        }
        //------------------------------------------
        //选中速率
        //------------------------------------------
        private void radioButton_XModeS_CheckedChanged(object sender, EventArgs e)
        {
            label_X1.Text = "速度0.01 ～ 400°/s";
            label_X2.Text = "加速度0.1 ～ 100°/s²";
            groupBox_XPosition.Enabled = false;
            XRun_Option = 2;
        }
        //------------------------------------------
        //急停
        //------------------------------------------
        private void button_EmergencyStop_Click(object sender, EventArgs e)
        {
            if (Form_Rotate303Settings.SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            //DialogResult button1 = new DialogResult();
            
            if (DialogResult.Yes == MessageBox.Show("确定急停？","提示",MessageBoxButtons.YesNo))
            {
                Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.EmergencyStop(), 0, 16);
            }
            
        }
        //------------------------------------------
        //框体使能后操作
        //------------------------------------------
        private void groupBox_XPosition_EnabledChanged(object sender, EventArgs e)
        {
            //if (groupBox_XPosition.Enabled == true)
            //{
            //    radioButton_XAP_CheckedChanged(new object(), new EventArgs());
            //}
            
        }
        //------------------------------------------
        //俯仰轴运行
        //------------------------------------------
        private void button_YRun_Click(object sender, EventArgs e)
        {
            if (Form_Rotate303Settings.SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            if (Form_Rotate303Settings.Control_State == false)
            {
                MessageBox.Show("请先连接转台！");
                return;
            }
            if (Form_Rotate303Settings.Y_IsZero == false)
            {
                MessageBox.Show("请先给Y轴寻零 ！");
                return;
            }
            if (textBox_Y1.Text.Equals("") | textBox_Y2.Text.Equals(""))
            {
                MessageBox.Show("参数不能为空！");
                return;
            }
            if (DialogResult.No == MessageBox.Show("运行俯仰轴？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            switch (YRun_Option)
            {
                case 0://停止
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.YStop(), 0, 16);//停止
                    Y_SetRunWay = 0;
                    break;
                case 1://绝对位置
                    #region
                    if (Convert.ToDouble(textBox_Y1.Text) < -360 | Convert.ToDouble(textBox_Y1.Text) > 360)
                    {
                        MessageBox.Show("俯仰轴角度范围为±360°！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_Y2.Text) < 0.01 | Convert.ToDouble(textBox_Y2.Text) > 100)
                    {
                        MessageBox.Show("俯仰轴速率范围为0.01 ～ 100°/s！");
                        return;
                    }
                    if ((Y_SetRunWay != 1 && Y_SetRunWay != 0) && Form_Rotate303Settings.Y_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.YSet(0, Convert.ToDouble(textBox_Y1.Text), Convert.ToDouble(textBox_Y2.Text), Acc), 0, 16);//设置绝对位置运行和方式
                    Form_Rotate303Settings.Delay(50);
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.YRun_Position(), 0, 16);//设置绝对位置运行
                    Y_SetRunWay = 1;
                    Form_Rotate303Settings.Y_Run = true;
                    #endregion
                    break;
                case 2://速率
                    #region
                    if (Convert.ToDouble(textBox_Y2.Text) < 0.1 | Convert.ToDouble(textBox_Y2.Text) > 50)
                    {
                        MessageBox.Show("方位轴加速度范围为0.1 ～ 50°/s²！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_Y1.Text) < -100 | Convert.ToDouble(textBox_Y1.Text) > 100)
                    {
                        MessageBox.Show("方位轴速率范围为±100°/s！");
                        return;
                    }
                    if ((Y_SetRunWay != 2 && Y_SetRunWay != 0) && Form_Rotate303Settings.Y_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    if (Y_SetRunWay == 0)
                    {
                        Set_YAcc = Convert.ToDouble(textBox_Y2.Text);
                    }
                    else if (Y_SetRunWay == 2)
                    {
                        if (Set_YAcc != Convert.ToDouble(textBox_Y2.Text))
                        {
                            MessageBox.Show("转台运行中不能改变加速度！", "提示");
                            textBox_Y2.Text = Set_YAcc.ToString("0.00");
                            return;
                        }
                    }

                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.YSet(1, 0, Convert.ToDouble(textBox_Y1.Text), Convert.ToDouble(textBox_Y2.Text)), 0, 16);//设置速率运行和方式
                    Form_Rotate303Settings.Delay(50);
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.YRun_Speed(), 0, 16);//设置速率运行
                    Y_SetRunWay = 2;
                    Form_Rotate303Settings.Y_Run = true;
                    #endregion
                    break;
                case 3://相对位置
                    #region

                    if (Convert.ToDouble(textBox_Y1.Text) < -360 | Convert.ToDouble(textBox_Y1.Text) > 360)
                    {
                        MessageBox.Show("俯仰轴角度范围为±360°！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_Y2.Text) < 0.01 | Convert.ToDouble(textBox_Y2.Text) > 100)
                    {
                        MessageBox.Show("俯仰轴速率范围为0.01 ～ 100°/s！");
                        return;
                    }
                    if ((Y_SetRunWay != 3 && Y_SetRunWay != 0) && Form_Rotate303Settings.Y_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.YSet(2, Convert.ToDouble(textBox_Y1.Text), Convert.ToDouble(textBox_Y2.Text), Acc), 0, 16);//设置相对位置运行和方式
                    Form_Rotate303Settings.Delay(50);
                    //Form_Rotate303Settings.SerialRotate.Write(Form_Rotate303Settings.Rotate.XRun_RPosition(), 0, 16);//设置相对位置运行
                    Y_SetRunWay = 3;
                    Form_Rotate303Settings.Y_Run = true;
                    #endregion
                    break;
            }
        }
        //------------------------------------------
        //选中停止
        //------------------------------------------
        private void radioButton_YStop_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_YStop.Checked == true)
            {
                label_Y1.Text = "";
                label_Y2.Text = "";
                groupBox_YPosition.Enabled = false;
                YRun_Option = 0;
            }
        }
        //------------------------------------------
        //选中位置
        //------------------------------------------
        private void radioButton_YModeP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_YModeP.Checked == true)
            {
                label_Y1.Text = "位置±360°";
                label_Y2.Text = "速度0.01 ～ 100°/s";
                groupBox_YPosition.Enabled = true;
                if (radioButton_YAP.Checked == true)
                {
                    YRun_Option = 1;
                }
                else
                {
                    YRun_Option = 3;
                }
            }
        }
        //------------------------------------------
        //选中相对位置
        //------------------------------------------
        private void radioButton_YRP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_YRP.Checked == true)
            {
                YRun_Option = 3;
            }
        }
        //------------------------------------------
        //选中绝对位置
        //------------------------------------------
        private void radioButton_YAP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_YAP.Checked == true)
            {
                YRun_Option = 1;
            }
        }
        //------------------------------------------
        //选中速率
        //------------------------------------------
        private void radioButton_YModeS_CheckedChanged(object sender, EventArgs e)
        {
            label_Y1.Text = "速度0.01 ～ 100°/s";
            label_Y2.Text = "加速度0.1 ～ 50°/s²";
            groupBox_YPosition.Enabled = false;
            YRun_Option = 2;
        }
    }
}
