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
using System.IO;

namespace Sensor_Rotate
{
    public partial class Form_ReadWritePara : DockContent
    {
        #region 定义变量
        static public System.Timers.Timer updataDataRW = new System.Timers.Timer(10);
        Form_Communicate frm = new Form_Communicate();
        CommandsToSend CD = new CommandsToSend();//串口发送命令类
        #endregion
        public Form_ReadWritePara()
        {
            InitializeComponent();
            updataDataRW.Elapsed += new System.Timers.ElapsedEventHandler(UpdateShow);
        }

        private void Form_ReadWritePara_Load(object sender, EventArgs e)
        {

        }
        #region 函数
        //-->函数:更新数据显示
        private void UpdateShow(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate () { textBox_State.Text = Form_Communicate.readWriteState; textBox_Data.Text = Form_Communicate.readWriteData; }));
            }
            catch { }
        }
        #endregion
        //-->帮助按钮
        private void button_ShowHelp_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(System.IO.File.ReadAllText(Application.StartupPath + @"\..\..\HelpIndex\读写参数.txt"),"帮助");
            System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + @"\..\..\HelpIndex\读写参数说明");
            //System.Diagnostics.Process.Start("www.baidu.com");
        }
        //-->读取对应区号的信息
        private void button_ReadPara_Click(object sender, EventArgs e)
        {//ED 00 FF 0D 0A
            byte[] cm = new byte[5];
            cm[0] = 0xED;
            cm[1] = Convert.ToByte(comboBox_ZoneNum.Text);
            cm[2] = 0xFF;
            cm[3] = 0x0D;
            cm[4] = 0x0A;
            frm.SendCommData(cm, 5);
            frm.SendCommData(cm, 5);
        }
        //-->读取系统工作模式
        private void button_ReadSys_Click(object sender, EventArgs e)
        {//ED e0 FF 0D 0A
            frm.SendCommData(CD.readSysMode, 5);
        }
        //-->读取CAN口配置1
        private void button_ReadCAN1_Click(object sender, EventArgs e)
        {//0xED,0xE1,0xFF,0x0D,0x0A
            frm.SendCommData(CD.readCAN1, 5);
        }
        //-->读取CAN口配置2
        private void button_ReadCAN2_Click(object sender, EventArgs e)
        {//0xED,0xE2,0xFF,0x0D,0x0A
            frm.SendCommData(CD.readCAN2, 5);
        }
        //-->读取串口A
        private void button_ReadCommA_Click(object sender, EventArgs e)
        {//0xED,0xE3,0xFF,0x0D,0x0A
            frm.SendCommData(CD.readCommA, 5);
        }
        //-->读取串口B
        private void button_ReadCommB_Click(object sender, EventArgs e)
        {//xED,0xE4,0xFF,0x0D,0x0A
            frm.SendCommData(CD.readCommB, 5);
        }
        //-->加载存储在当前路径下的数据文件
        private void button_LoadDataFile_Click(object sender, EventArgs e)
        {
            string _path = Application.StartupPath + "\\para" + System.DateTime.Today.Date.ToString("yyyy_MM_dd") + "\\para" + comboBox_ParaAbs.Text.Trim() + ".txt";//文件路径
            if (!File.Exists(_path))
            {
                MessageBox.Show("文件不存在！");
            }
            else
            {
                string[] _data = File.ReadAllLines(_path);//获取所有行
                if (_data.Length >= 21)
                {
                    Abs1.Text = (Convert.ToInt32(_data[2])).ToString("X").PadLeft(2, '0');
                    Abs2.Text = (Convert.ToInt32(_data[3])).ToString("X").PadLeft(2, '0');
                    Abs3.Text = (Convert.ToInt32(_data[4])).ToString("X").PadLeft(2, '0');
                    Abs4.Text = (Convert.ToInt32(_data[5])).ToString("X").PadLeft(2, '0');
                    Abs5.Text = (Convert.ToInt32(_data[6])).ToString("X").PadLeft(2, '0');
                    Abs6.Text = (Convert.ToInt32(_data[7])).ToString("X").PadLeft(2, '0');
                    Abs7.Text = (Convert.ToInt32(_data[8])).ToString("X").PadLeft(2, '0');
                    Abs8.Text = (Convert.ToInt32(_data[9])).ToString("X").PadLeft(2, '0');
                    Abs9.Text = (Convert.ToInt32(_data[10])).ToString("X").PadLeft(2, '0');
                    Abs10.Text = (Convert.ToInt32(_data[11])).ToString("X").PadLeft(2, '0');
                    Abs11.Text = (Convert.ToInt32(_data[12])).ToString("X").PadLeft(2, '0');
                    Abs12.Text = (Convert.ToInt32(_data[13])).ToString("X").PadLeft(2, '0');
                    Abs13.Text = (Convert.ToInt32(_data[14])).ToString("X").PadLeft(2, '0');
                    Abs14.Text = (Convert.ToInt32(_data[15])).ToString("X").PadLeft(2, '0');
                    Abs15.Text = (Convert.ToInt32(_data[16])).ToString("X").PadLeft(2, '0');
                    Abs16.Text = (Convert.ToInt32(_data[17])).ToString("X").PadLeft(2, '0');
                }
                else
                {
                    MessageBox.Show("文件格式不正确！");  
                }
            }
        }
        //-->写参数（绝对值）
        private void button_WriteAbs_Click(object sender, EventArgs e)
        {//E8 0x d0 d1 d2 d3 d4 d5 d6 d7 d8 d9 da db dc dd dd de df FF 0D 0A
            byte[] cm = new byte[21];
            cm[0] = 0xE8;
            cm[1] = Convert.ToByte(comboBox_ParaAbs.Text.Trim());
            cm[2] = Convert.ToByte(Abs1.Text.Trim());
            cm[3] = Convert.ToByte(Abs2.Text.Trim());
            cm[4] = Convert.ToByte(Abs3.Text.Trim());
            cm[5] = Convert.ToByte(Abs4.Text.Trim());
            cm[6] = Convert.ToByte(Abs5.Text.Trim());
            cm[7] = Convert.ToByte(Abs6.Text.Trim());
            cm[8] = Convert.ToByte(Abs7.Text.Trim());
            cm[9] = Convert.ToByte(Abs8.Text.Trim());
            cm[10] = Convert.ToByte(Abs9.Text.Trim());
            cm[11] = Convert.ToByte(Abs10.Text.Trim());
            cm[12] = Convert.ToByte(Abs11.Text.Trim());
            cm[13] = Convert.ToByte(Abs12.Text.Trim());
            cm[14] = Convert.ToByte(Abs13.Text.Trim());
            cm[15] = Convert.ToByte(Abs14.Text.Trim());
            cm[16] = Convert.ToByte(Abs15.Text.Trim());
            cm[17] = Convert.ToByte(Abs16.Text.Trim());
            cm[18] = 0xFF;
            cm[19] = 0x0D;
            cm[20] = 0x0A;
            frm.SendCommData(cm, 21);
        }
        //-->写参数（相对值）
        private void button_WriteRel_Click(object sender, EventArgs e)
        {//E9 0x d0 d1 d2 d3 d4 d5 d6 d7 d8 d9 da db dc dd dd de df FF 0D 0A
            byte[] cm = new byte[21];
            cm[0] = 0xE9;
            cm[1] = Convert.ToByte(comboBox_ParaRel.Text.Trim());
            cm[2] = Convert.ToByte(Rel1.Text.Trim());
            cm[3] = Convert.ToByte(Rel2.Text.Trim());
            cm[4] = Convert.ToByte(Rel3.Text.Trim());
            cm[5] = Convert.ToByte(Rel4.Text.Trim());
            cm[6] = Convert.ToByte(Rel5.Text.Trim());
            cm[7] = Convert.ToByte(Rel6.Text.Trim());
            cm[8] = Convert.ToByte(Rel7.Text.Trim());
            cm[9] = Convert.ToByte(Rel8.Text.Trim());
            cm[10] = Convert.ToByte(Rel9.Text.Trim());
            cm[11] = Convert.ToByte(Rel10.Text.Trim());
            cm[12] = Convert.ToByte(Rel11.Text.Trim());
            cm[13] = Convert.ToByte(Rel12.Text.Trim());
            cm[14] = Convert.ToByte(Rel13.Text.Trim());
            cm[15] = Convert.ToByte(Rel14.Text.Trim());
            cm[16] = Convert.ToByte(Rel15.Text.Trim());
            cm[17] = Convert.ToByte(Rel16.Text.Trim());
            cm[18] = 0xFF;
            cm[19] = 0x0D;
            cm[20] = 0x0A;
            frm.SendCommData(cm, 21);
        }
    }
}
