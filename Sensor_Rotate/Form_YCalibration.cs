using System;
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
    public partial class Form_YCalibration : DockContent
    {
        #region 定义变量
        Form_Communicate frm = new Form_Communicate();
        CommandsToSend CD = new CommandsToSend();
        public static System.Timers.Timer updateDataYC = new System.Timers.Timer(10);
        #endregion
        public Form_YCalibration()
        {
            InitializeComponent();
            updateDataYC.Elapsed += new System.Timers.ElapsedEventHandler(UpdateDataYC_Elapsed);
        }
        //-->函数：更新显示数据
        private void UpdateDataYC_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate () {
                    textBox1.Text = Form_Communicate.yCalibrationData;
                    textBox2.Text = Form_Communicate.yCalibrationState; }));
            }
            catch { }
        }

        private void Form_YCalibration_Load(object sender, EventArgs e)
        {

        }
        //-->1、台体转到绝对位置180度
        private void button1_Click(object sender, EventArgs e)
        {
            Class_Comm.SetXPosition(-90, 20, 10);//X轴转到-90度           
            Class_Comm.WaitXAxis();//等待停止
            Class_Comm.SetYPosition(180, 20, 10);//Y轴转到0度
        }
        //-->1、电压零位（负零位）标定
        private void button2_Click(object sender, EventArgs e)
        {//EC 20 02 FF 0D 0A
            frm.SendCommData(CD.YNegativeZeroC, 6);
        }
        //-->2、台体转到绝对位置0度
        private void button4_Click(object sender, EventArgs e)
        {
            Class_Comm.SetYPosition(0, 20, 10);//Y轴转到0度
        }
        //-->2、电压零位（正零位）标定
        private void button3_Click(object sender, EventArgs e)
        {
            frm.SendCommData(CD.YPositiveZeroC, 6);
        }
        //-->3、倾角显示归零
        private void button5_Click(object sender, EventArgs e)
        {//Y轴正数转台Y轴反转，负数正转
            double _yAngle = Form_Communicate.YAngle;//当前X轴角度
            Class_Comm.SetYPosition(-_yAngle, 20, 10);//Y轴转动
        }
        //-->4、台体转到相对位置12度(顺时针)
        private void button7_Click(object sender, EventArgs e)
        {//当前Y轴位置加上12
            double _yAngleToMove = Form_Rotate303.Yposition - 12;
            Class_Comm.SetYPosition(_yAngleToMove, 20, 10);
        }
        //-->4、倾角增益参数标定
        private void button6_Click(object sender, EventArgs e)
        {
            //EC 22 XX FF 0D 0A
            byte[] cm = new byte[6];
            cm[0] = 0xEC;
            cm[1] = 0x22;
            if (Form_Communicate.YAngle >= 0)
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
        //-->5、Y角度二次插补循环
        private void button9_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text.Trim())
            {
                case "-15":
                    Class_Comm.SetYPosition(-15, 20, 10);
                    break;
                case "-12":
                    Class_Comm.SetYPosition(-12, 20, 10);
                    break;
                case "-9":
                    Class_Comm.SetYPosition(-9, 20, 10);
                    break;
                case "-6":
                    Class_Comm.SetYPosition(-6, 20, 10);
                    break;
                case "-3":
                    Class_Comm.SetYPosition(-3, 20, 10);
                    break;
                case "0":
                    Class_Comm.SetYPosition(0, 20, 10);
                    break;
                case "3":
                    Class_Comm.SetYPosition(3, 20, 10);
                    break;
                case "6":
                    Class_Comm.SetYPosition(6, 20, 10);
                    break;
                case "9":
                    Class_Comm.SetYPosition(9, 20, 10);
                    break;
                case "12":
                    Class_Comm.SetYPosition(12, 20, 10);
                    break;
                case "15":
                    Class_Comm.SetYPosition(15, 20, 10);
                    break;
            }
        }
        //-->5、二次插补参数标定
        private void button8_Click(object sender, EventArgs e)
        {//EC 21 xx FF 0D 0A
            byte[] cm = new byte[6];
            cm[0] = 0xEC;
            cm[1] = 0x21;
            switch (comboBox1.Text.Trim())
            {
                case "-15":
                    cm[2] = 0x85;
                    break;
                case "-12":
                    cm[2] = 0x84;
                    break;
                case "-9":
                    cm[2] = 0x83;
                    break;
                case "-6":
                    cm[2] = 0x82;
                    break;
                case "-3":
                    cm[2] = 0x81;
                    break;
                case "0":
                    cm[2] = 0x0;
                    break;
                case "3":
                    cm[2] = 0x1;
                    break;
                case "6":
                    cm[2] = 0x2;
                    break;
                case "9":
                    cm[2] = 0x3;
                    break;
                case "12":
                    cm[2] = 0x4;
                    break;
                case "15":
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
        {
            frm.SendCommData(CD.YZeroC, 6);
        }
    }
}
