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
    public partial class Form_TCalibration : DockContent
    {
        #region 定义变量
        CommandsToSend CD = new CommandsToSend();//串口发送命令类
        Form_Communicate frm = new Form_Communicate();
        public static System.Timers.Timer updataDataT = new System.Timers.Timer(10);//更新显示数据定时器
        #endregion
        public Form_TCalibration()
        {
            InitializeComponent();
            updataDataT.Elapsed += T_UpdateData_Elapsed;
        }
        //-->函数：更新显示数据
        private void T_UpdateData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    textBox_Data.Text = Form_Communicate.tCalibrationData;
                    textBox_State.Text = Form_Communicate.tCalibrationState;
                }));
            }
            catch { }
        }

        private void Form_TCalibration_Load(object sender, EventArgs e)
        {

        }
        //-->温度零位标定
        private void button_TZero_Click(object sender, EventArgs e)
        {//EC 40 00 FF 0D 0A
            frm.SendCommData(CD.TZeroC, 6);
        }
        //-->温度增益参数标定
        private void button_TGainC_Click(object sender, EventArgs e)
        {//EC 42 03 FF 0D 0A
            frm.SendCommData(CD.TGainC, 6);
        }
        //-->温度二次插补参数标定
        private void button_TC1_Click(object sender, EventArgs e)
        {//EC 41 xx FF 0D 0A
            byte[] cm = new byte[6];
            cm[0] = 0xEC;
            cm[1] = 0x41;
            switch(comboBox_TEnum1.Text.Trim())
            {
                case "60":
                    cm[2] = 0x03;
                    break;
                case "40":
                    cm[2] = 0x02;
                    break;
                case "20":
                    cm[2] = 0x01;
                    break;
                case "0":
                    cm[2] = 0x0;
                    break;
                case "-20":
                    cm[2] = 0x81;
                    break;
                case "-40":
                    cm[2] = 0x82;
                    break;
            }
            cm[3] = 0xFF;
            cm[4] = 0x0D;
            cm[5] = 0x0A;
            frm.SendCommData(cm, 6);
        }
        //-->X、Y角度传感器温度零位二次插补参数标定
        private void button_TC2_Click(object sender, EventArgs e)
        {//EC 48 xx FF 0D 0A
            byte[] cm = new byte[6];
            cm[0] = 0xEC;
            cm[1] = 0x48;
            switch (comboBox_TEnum2.Text.Trim())
            {
                case "60":
                    cm[2] = 0x03;
                    break;
                case "40":
                    cm[2] = 0x02;
                    break;
                case "20":
                    cm[2] = 0x01;
                    break;
                case "0":
                    cm[2] = 0x0;
                    break;
                case "-20":
                    cm[2] = 0x81;
                    break;
                case "-40":
                    cm[2] = 0x82;
                    break;
            }
            cm[3] = 0xFF;
            cm[4] = 0x0D;
            cm[5] = 0x0A;
            frm.SendCommData(cm, 6);
        }
    }
}
