﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sensor_Rotate
{
    public partial class Form_XCalibration_D30 : DockContent
    {
        public Form_XCalibration_D30()
        {
            InitializeComponent();
            updataDataXC_D30.Elapsed += UpdataDataXC_D30_Elapsed;
        }

        private void UpdataDataXC_D30_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate() {
                textBox1.Text = Form_Communicate.xCalibrationData;
                textBox2.Text = Form_Communicate.xCalibrationState;
            }));
        }
        #region 定义变量
        Form_Communicate frm = new Form_Communicate();
        CommandsToSend CD = new CommandsToSend();
        double[] AngleRange = new double[] { -30, -24, -18, -12, -6, 0, 6, 12, 18, 24, 30 };
        public static System.Timers.Timer updataDataXC_D30 = new System.Timers.Timer(10);
        #endregion
        //-->1、台体转到绝对位置180度
        private void button1_Click(object sender, EventArgs e)
        {//X轴转到0度-->Y轴转到180度
            Class_Comm.SetXPosition(0, 20, 10);//X轴转到0度
            Class_Comm.WaitXAxis();//等待停止
            Class_Comm.SetYPosition(180, 20, 10);//Y轴转到0度
        }
        //-->1、电压零位（负零位）标定
        private void button2_Click(object sender, EventArgs e)
        {//EC 10 02 FF 0D 0A
            frm.SendCommData(CD.XNegativeZeroC, 6);
        }
        //-->2、台体转到绝对位置0度
        private void button4_Click(object sender, EventArgs e)
        {//Y轴转到0度
            Class_Comm.SetYPosition(0, 20, 10);//Y轴转到0度
        }
        //-->2、电压零位（正零位）标定
        private void button3_Click(object sender, EventArgs e)
        {//EC 10 01 FF 0D 0A
            frm.SendCommData(CD.XPositiveZeroC, 6);
        }
        //-->3、倾角显示归零
        private void button5_Click(object sender, EventArgs e)
        {//X轴正数转台Y轴反转，负数正转
            double _xAngle = Form_Communicate.XAngle;//当前X轴角度
            Class_Comm.SetYPosition(-_xAngle, 20, 10);//Y轴转动
        }
        //-->4、台体转到相对位置24度(顺时针)
        private void button7_Click(object sender, EventArgs e)
        {//当前Y轴位置加上24
            double _yAngleToMove = Form_Rotate303.Yposition - 24;
            Class_Comm.SetYPosition(_yAngleToMove, 20, 10);
        }
        //-->4、倾角增益参数标定
        private void button6_Click(object sender, EventArgs e)
        {
            //EC 12 XX FF 0D 0A
            byte[] cm = new byte[6];
            cm[0] = 0xEC;
            cm[1] = 0x12;
            if (Form_Communicate.XAngle >= 0)
            {
                cm[2] = 0x04;
            }
            else
            {
                cm[2] = 0x84;
            }
            cm[3] = 0xFF;
            cm[4] = 0x0D;
            cm[5] = 0x0A;
            frm.SendCommData(cm, 6);
        }
        //-->5、X角度二次插补循环
        private void button9_Click(object sender, EventArgs e)
        {
            double _position;
            bool _flag = false;
            _position = Convert.ToDouble(comboBox1.Text.Trim());
            foreach (double a in AngleRange)
            {
                if (a == _position)
                {
                    _flag = true;
                }
            }
            if (_flag == true)
            {
                Class_Comm.SetYPosition(_position, 20, 10);
            }
            else
            {
                MessageBox.Show("请输入正确的角度！");
            }
        }
        //-->5、二次插补参数标定
        private void button8_Click(object sender, EventArgs e)
        {
            //EC 11 xx FF 0D 0A
            byte[] cm = new byte[6];
            cm[0] = 0xEC;
            cm[1] = 0x11;
            switch (comboBox1.Text.Trim())
            {
                case "-30":
                    cm[2] = 0x85;
                    break;
                case "-24":
                    cm[2] = 0x84;
                    break;
                case "-18":
                    cm[2] = 0x83;
                    break;
                case "-12":
                    cm[2] = 0x82;
                    break;
                case "-6":
                    cm[2] = 0x81;
                    break;
                case "0":
                    cm[2] = 0x0;
                    break;
                case "6":
                    cm[2] = 0x1;
                    break;
                case "12":
                    cm[2] = 0x2;
                    break;
                case "18":
                    cm[2] = 0x3;
                    break;
                case "24":
                    cm[2] = 0x4;
                    break;
                case "30":
                    cm[2] = 0x5;
                    break;
            }
            cm[3] = 0xFF;
            cm[4] = 0x0D;
            cm[5] = 0x0A;
            frm.SendCommData(cm, 6);
        }
        //-->6、台体转到绝对位置0度
        private void button10_Click(object sender, EventArgs e)
        {
            Class_Comm.SetYPosition(0, 20, 10);//Y轴转到0度
        }
        //-->6、倾角角度零位标定
        private void button11_Click(object sender, EventArgs e)
        {//EC 10 00 FF 0D 0A
            frm.SendCommData(CD.XZeroC, 6);
        }
    }
}