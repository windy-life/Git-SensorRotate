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
using RotateDll;
using System.IO.Ports;
using System.IO;

namespace Sensor_Rotate
{
    public partial class Form_Rotate303Settings : DockContent
    {
        System.Timers.Timer T_SendData = new System.Timers.Timer(100);//创建定时器T_SendData，并设置时间间隔为50ms，定时向转台发送命令
        //Form_Rotate303Control _formcontrol = (Form_Rotate303Control);
        public Form_Rotate303Settings()
        {
            InitializeComponent();
            PortNum = SerialPort.GetPortNames();//获取可用串口号
            foreach (string port in PortNum)
            {
                comboBox_SerialPort.Items.Add(port);//添加串口号
            }
            if (comboBox_SerialPort.Items.Count > 0)//有可用的串口号
            {
                comboBox_SerialPort.SelectedIndex = 0;//显示第一个号
            }

            T_SendData.Elapsed += new System.Timers.ElapsedEventHandler(CommDataSend);//指定到时执行函数
            T_SendData.Enabled = true;//启动
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        //定义变量
        #region
        public static RotateDll.Class1 Rotate = new Class1();
        public string [] PortNum;//可用串口号
        static public SerialPort SerialRotate = new SerialPort();//建立一个串口实例
        static public bool Control_State = false;//远控通讯状态
        //X轴变量
        public static bool X_ToZero = false;//寻零状态标志位
        public static bool X_IsZero = false;//是否已经寻零状标志位
        public static double Xposition = 0;//方位轴位置
        public static double Xspeed = 0;//方位轴速度
        public static string XState = "";//方位轴状态
        public static bool X_Power = false;//方位上电状态
        public static bool X_Enable = false;//方位闭合状态
        public static int X_RunWay = 0;//方位运行方式(0-无；1-位置；2-速率；3-相对位置)
        public static bool X_Run = false;//运行状态标志位
        //Y轴变量
        public static bool Y_ToZero = false;//寻零状态标志位
        public static bool Y_IsZero = false;//是否已经寻零状标志位
        public static double Yposition = 0;//俯仰轴位置
        public static double Yspeed = 0;//俯仰轴速度
        public static string YState = "";//俯仰轴状态
        public static bool Y_Power = false;//俯仰上电状态
        public static bool Y_Enable = false;//俯仰闭合状态
        public static int Y_RunWay = 0;//方位运行方式(0-无；1-位置；2-速率；3-相对位置)
        public static bool Y_Run = false;//运行状态标志位
        //暂时未用到变量
        
        
        
        double X_Position = 0;//方位位置
        double Y_Position = 0;//俯仰位置
        double X_Speed = 0;//方位速度
        double Y_Speed = 0;//俯仰速度
        public double X_PositionSet = 0;//方位轴已设置位置
        public double Y_PositionSet = 0;//俯仰轴已设置位置
        public double X_SpeedSet = 5;//方位轴已设置速度
        public double Y_SpeedSet = 5;//俯仰轴已设置速度
        public double X_AccSet = 10;//方位轴已设置加速度
        public double Y_AccSet = 10;//俯仰轴已设置加速度
        
        #endregion
        //------------------------------------------
        //刷新串口
        //------------------------------------------
        private void button_updatePorts_Click(object sender, EventArgs e)
        {
            comboBox_SerialPort.Items.Clear();//清空
            PortNum = SerialPort.GetPortNames();//获取当前可用串口
            foreach (string port in PortNum)
            {
                comboBox_SerialPort.Items.Add(port);//添加串口号
            }
            if (comboBox_SerialPort.Items.Count > 0)//有可用的串口号
            {
                comboBox_SerialPort.SelectedIndex = 0;//显示第一个号
            }
        }
        //------------------------------------------
        //打开/关闭串口
        //------------------------------------------
        private void button_OpenPorts_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == false)//串口未打开
            {
                SerialRotate.PortName = comboBox_SerialPort.Text;//赋值串口号
                SerialRotate.BaudRate = 9600;//波特率
                SerialRotate.DataBits = 8;//数据位
                SerialRotate.StopBits = (StopBits)1;//停止位
                SerialRotate.ReadTimeout = 500;//设置超时时间
                SerialRotate.Open();//打开串口
                if (SerialRotate.IsOpen == true)//成功打开
                {
                    SerialRotate.DataReceived += new SerialDataReceivedEventHandler(SerialDataProcess);
                    button_OpenPorts.Text = "关闭串口";
                    button_updatePorts.Enabled = false;
                    comboBox_SerialPort.Enabled = false;
                    T_SendData.Enabled = true;
                    Form_Rotate303Control.T_UpdateData.Enabled = true;//打开更新数据的定时器
                }
                else
                {
                    MessageBox.Show("打开串口失败", "error");
                }
            }
            else//已打开串口
            {
                if (DialogResult.No == MessageBox.Show("确定关闭串口？", "提示", MessageBoxButtons.YesNo))
                {
                    return;
                }
                T_SendData.Enabled = false;
                Delay(300);//延时300ms
                SerialRotate.Close();//关闭串口
                if (SerialRotate.IsOpen == false)//成功关闭
                {
                    button_updatePorts.Enabled = true;
                    comboBox_SerialPort.Enabled = true;
                    SerialRotate.DataReceived -= new SerialDataReceivedEventHandler(SerialDataProcess);
                    button_OpenPorts.Text = "打开串口";
                }

            }
        }
        //------------------------------------------
        //延时函数
        //------------------------------------------
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }
        //---------------------------------------------
        //字节转HEX
        //---------------------------------------------
        public string Change_TentoHex2(Byte[] bytTempData)
        {
            string Hex_String = "";
            foreach (byte byte_tem in bytTempData)
            {
                if (byte_tem < 16)
                {
                    Hex_String += "0" + byte_tem.ToString("X").Trim() + " ";//byte转HEX方法
                }
                else
                {
                    Hex_String += byte_tem.ToString("X").Trim() + " ";
                }
            }
            return Hex_String;
        }
        //------------------------------------------
        //串口接收数据处理
        //------------------------------------------
        public void SerialDataProcess(object sender, SerialDataReceivedEventArgs e)
        {
            int j = 0;
            int CheckSum = 0;//校验
            j = SerialRotate.BytesToRead;//获取缓存中的字节数
            string strRecieve3 = "";
            if (j >= 16)
            {
                byte[] RecieveBuf = new byte[16];
                byte[] Head = new byte[1];
                SerialRotate.Read(Head, 0, 1);//检查包头
                if (Head[0] == 221)//包头正确
                {
                    SerialRotate.Read(RecieveBuf, 0, 15);//接收后面的缓存
                    for (int i = 0; i < 15; i++)
                    {
                        CheckSum += RecieveBuf[i];
                    }
                    CheckSum += 0xDD;
                    CheckSum = CheckSum % 256;
                    if (CheckSum == 0)//校验正确
                    {
                        strRecieve3 = Change_TentoHex2(RecieveBuf);//转为16进制字符串
                        switch (RecieveBuf[1])
                        {
                            case 0x13://位置和速率
                                #region 
                                double position = 0;
                                double speed = 0;
                                for(int i = 2; i < 6; i++)
                                {
                                    //double a = Math.Pow(256, i - 2);
                                    position += RecieveBuf[i] * Math.Pow(256, i - 2);

                                    speed += RecieveBuf[i + 4] * Math.Pow(256, i - 2);
                                }
                                if (position >= 2147483648)//补码
                                {
                                    position = position - 4294967296;//获取原码 
                                }
                                position = position * 0.00001;//乘以标度
                                if (speed >= 2147483648)//补码
                                {
                                    speed = speed - 4294967296;//获取原码 
                                }
                                speed = speed * 0.00001;
                                if (RecieveBuf[0] == 0x01)//方位轴
                                {
                                    Xposition = position;
                                    Xspeed = speed;
                                    //Invoke(new MethodInvoker(delegate () { Form_Rotate303Control = position.ToString("0.0000"); }));
                                    //Invoke(new MethodInvoker(delegate () { textBox_XSpeed.Text = speed.ToString("0.0000"); }));
                                }
                                else//俯仰轴
                                {
                                    Yposition = position;
                                    Yspeed = speed;
                                    //Invoke(new MethodInvoker(delegate () { textBox_YPosition.Text = position.ToString("0.0000"); }));
                                    //Invoke(new MethodInvoker(delegate () { textBox_YSpeed.Text = speed.ToString("0.0000"); }));
                                }
                                #endregion
                                break;
                            case 0x55://通讯成功
                                #region
                                MessageBox.Show("建立通讯成功！", "提示");
                                Invoke(new MethodInvoker(delegate () { button_Communicate.Text = "断开"; }));
                                Control_State = true;//远控连接成功
                                pictureBox_Communicaate.Image = Resource1.connect;//更新图标
                                #endregion
                                break;
                            case 0x15://归零
                                #region
                                if (RecieveBuf[0] == 1)//方位轴
                                {
                                    if (RecieveBuf[2] == 2)//归零完成
                                    {
                                        X_ToZero = false;//寻零完成，置位标志位
                                        X_IsZero = true;//寻过零
                                        X_Run = false;//置位标志位
                                        XState = "方位轴寻零完成！";
                                        //Invoke(new MethodInvoker(delegate () { textBox_XMessageShow.Text = "方位轴寻零完成！"; }));
                                        //Invoke(new MethodInvoker(delegate () { pictureBox_XRunState.Image = global::Rotate_Base.Properties.Resources.黄圆; }));//更新图标
                                    }
                                    else if (RecieveBuf[2] == 1)//正在归零
                                    {
                                        X_ToZero = true;//寻零中，置位标志位
                                        XState = "方位轴寻零中…";
                                        //Invoke(new MethodInvoker(delegate () { textBox_XMessageShow.Text = "方位轴寻零中！"; }));
                                    }
                                }
                                else if (RecieveBuf[0] == 2)//俯仰轴
                                {
                                    if (RecieveBuf[2] == 2)//归零完成
                                    {
                                        Y_ToZero = false;//寻零完成，置位标志位
                                        Y_IsZero = true;//寻过零
                                        Y_Run = false;//置位标志位
                                        YState = "俯仰轴寻零完成！";
                                        //Invoke(new MethodInvoker(delegate () { textBox_YMessageShow.Text = "俯仰轴寻零完成！"; }));
                                        //Invoke(new MethodInvoker(delegate () { pictureBox_YRunState.Image = global::Rotate_Base.Properties.Resources.黄圆; }));//更新图标
                                    }
                                    else if (RecieveBuf[2] == 1)//正在归零
                                    {
                                        Y_ToZero = true;//寻零中，置位标志位
                                        YState = "俯仰轴寻零中…";
                                        //Invoke(new MethodInvoker(delegate () { textBox_YMessageShow.Text = "俯仰轴寻零中！"; }));
                                    }
                                }
                                #endregion
                                break;
                            case 0x16://转台运行状态
                                #region
                                int State = 0;
                                int Position_State = 0, Speed_State = 0, Stop_State = 0;
                                State = RecieveBuf[2];//取值状态字节
                                Position_State = State % 2;//结果为1则已到位
                                Speed_State = (State / 2) % 2;//结果为1则速率已到达
                                Stop_State = (State / 2 / 2) % 2;//结果为1则已停止

                                if (RecieveBuf[0] == 1)//方位轴
                                {
                                    if (Position_State == 1 && Form_Rotate303Control.X_SetRunWay == 1 && X_Run == true)
                                    {
                                        XState = "位置已到达！";
                                        X_Run = false;
                                        return;
                                    }
                                    else if (Position_State == 0 && Form_Rotate303Control.X_SetRunWay == 1 && X_Run == true)
                                    {
                                        XState = "位置运行中！";
                                        return;
                                    }

                                    if (Speed_State == 1 && Form_Rotate303Control.X_SetRunWay == 2 && X_Run == true)
                                    {
                                        XState = "速率已到达！";
                                        //X_Run = false;
                                        return;
                                    }
                                    else if (Speed_State == 0 && Form_Rotate303Control.X_SetRunWay == 2 && X_Run == true)
                                    {
                                        XState = "速率运行中！";
                                        return;
                                    }
                                    if (Stop_State == 1 && X_Run == true)
                                    {
                                        XState = "已停止！";
                                        X_Run = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    if (Position_State == 1 && Form_Rotate303Control.Y_SetRunWay == 1 && Y_Run == true)
                                    {
                                        YState = "位置已到达！";
                                        Y_Run = false;
                                        return;
                                    }
                                    else if (Position_State == 0 && Form_Rotate303Control.Y_SetRunWay == 1 && Y_Run == true)
                                    {
                                        YState = "位置运行中！";
                                        return;
                                    }

                                    if (Speed_State == 1 && Form_Rotate303Control.Y_SetRunWay == 2 && Y_Run == true)
                                    {
                                        YState = "速率已到达！";
                                        //X_Run = false;
                                        return;
                                    }
                                    else if (Speed_State == 0 && Form_Rotate303Control.Y_SetRunWay == 2 && Y_Run == true)
                                    {
                                        YState = "速率运行中！";
                                        return;
                                    }
                                    if (Stop_State == 1 && Y_Run == true)
                                    {
                                        YState = "已停止！";
                                        Y_Run = false;
                                        return;
                                    }
                                }
                                #endregion
                                break;
                            case 0x17://转台运行状态
                                #region
                                //int Mode = 0;
                                //Mode  = RecieveBuf[2];//取值状态字节

                                //if (RecieveBuf[0] == 1)//方位轴
                                //{
                                //    Form_Rotate303Control.X_SetRunWay = Mode;
                                //}
                                //else if (RecieveBuf[0] == 2)//俯仰轴
                                //{
                                //    Form_Rotate303Control.Y_SetRunWay = Mode;
                                //}
                                #endregion
                                break;
                        }
                    }

                }

            }


        }
        //------------------------------------------
        //与转台建立通讯
        //------------------------------------------
        private void button_Communicate_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (Control_State == true)//已经建立了通讯
                {
                    if (DialogResult.No == MessageBox.Show("确定断开转台连接？", "提示", MessageBoxButtons.YesNo))
                    {
                        return;
                    }
                    SerialRotate.Write(Rotate.DisConnect(), 0, 16);//断开通讯
                    pictureBox_Communicaate.Image = Resource1.disconnect;//更新图
                    button_Communicate.Text = "连接";
                    Control_State = false;//通讯状态标志位
                    X_Run = false;
                    X_Power = false;
                    X_Enable = false;
                    Y_Run = false;
                    Y_Power = false;
                    Y_Enable = false;
                    Delay(50);
                }
                else
                {
                    SerialRotate.Write(Rotate.Connect(), 0, 16);//建立通讯
                    Delay(50);
                }
            }
            else
            {
                MessageBox.Show("请先打开串口！","提示");
            }
        }
        //------------------------------------------
        //方位轴上电下电
        //------------------------------------------
        private void button_XPower_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (Control_State == false)
                {
                    MessageBox.Show("请先连接转台！","提示");
                    return;
                }
                if (X_Power == false)//未上电
                {
                    SerialRotate.Write(Rotate.XPowerOn(), 0, 16);//方位上电
                    button_XPower.Text = "下电";
                    pictureBox_XPower.Image = Resource1.PowerOn;//更改图标
                    X_Power = true;//已上电
                }
                else
                {
                    if (X_Enable == true)
                    {
                        MessageBox.Show("请先闲置方位轴！");
                        return;
                    }
                    SerialRotate.Write(Rotate.XPowerOff(), 0, 16);//方位上电
                    button_XPower.Text = "上电";
                    pictureBox_XPower.Image = Resource1.PowerOff;//更改图标
                    X_Power = false;//已下电
                }
                
            }
            else
            {
                MessageBox.Show("请先打开串口！","提示");
            }
        }
        //------------------------------------------
        //方位轴闭合断开
        //------------------------------------------
        private void button_XConnect_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (X_Power == false)
                {
                    MessageBox.Show("请先给方位轴上电！", "提示");
                    return;
                }
                if (X_Enable == false)//未闭合
                {
                    SerialRotate.Write(Rotate.XEnable(), 0, 16);//方位闭合
                    button_XConnect.Text = "闲置";
                    pictureBox_XEnable.Image = Resource1.闭合;//更改图标
                    X_Enable = true;//已闭合
                }
                else
                {
                    SerialRotate.Write(Rotate.XDisable(), 0, 16);//方位闲置
                    button_XConnect.Text = "闭合";
                    pictureBox_XEnable.Image = Resource1.闲置;//更改图标
                    X_Enable = false;//已下电
                }

            }
            else
            {
                MessageBox.Show("请先打开串口！", "提示");
            }
        }
        //------------------------------------------
        //方位轴寻零
        //------------------------------------------
        private void button_XToZero_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (X_Enable == false)
                {
                    MessageBox.Show("请先闭合方位轴！", "提示");
                    return;
                }
                if (X_ToZero == false)//未在寻零中
                {
                    SerialRotate.Write(Rotate.XToZero(), 0, 16);//方位寻零
                    //pictureBox_XZero.Image = Resource1.闭合;//更改图标
                    X_ToZero = true;//正在寻零中
                }
                //else
                //{
                //    SerialRotate.Write(Rotate.XDisable(), 0, 16);//方位闲置
                //    pictureBox_XEnable.Image = Resource1.闲置;//更改图标
                //    X_Enable = true;//已上电
                //}

            }
            else
            {
                MessageBox.Show("请先打开串口！", "提示");
            }
        }
        //------------------------------------------
        //俯仰轴上电下电
        //------------------------------------------
        private void button_YPower_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (Control_State == false)
                {
                    MessageBox.Show("请先连接转台！", "提示");
                    return;
                }
                if (Y_Power == false)//未上电
                {
                    SerialRotate.Write(Rotate.YPowerOn(), 0, 16);//俯仰上电
                    button_YPower.Text = "下电";
                    pictureBox_YPower.Image = Resource1.PowerOn;//更改图标
                    Y_Power = true;//已上电
                }
                else
                {
                    SerialRotate.Write(Rotate.YPowerOff(), 0, 16);//俯仰上电
                    button_YPower.Text = "上电";
                    pictureBox_YPower.Image = Resource1.PowerOff;//更改图标
                    Y_Power = false;//已下电
                }

            }
            else
            {
                MessageBox.Show("请先打开串口！", "提示");
            }
        }
        //------------------------------------------
        //俯仰轴闭合断开
        //------------------------------------------
        private void button_YConnect_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (Y_Power == false)
                {
                    MessageBox.Show("请先给俯仰轴上电！", "提示");
                    return;
                }
                if (Y_Enable == false)//未闭合
                {
                    SerialRotate.Write(Rotate.YEnable(), 0, 16);//俯仰闭合
                    button_YConnect.Text = "闲置";
                    pictureBox_YEnable.Image = Resource1.闭合;//更改图标
                    Y_Enable = true;//已闭合
                }
                else
                {
                    SerialRotate.Write(Rotate.YDisable(), 0, 16);//俯仰闲置
                    button_YConnect.Text = "闭合";
                    pictureBox_YEnable.Image = Resource1.闲置;//更改图标
                    Y_Enable = false;//已闲置
                }

            }
            else
            {
                MessageBox.Show("请先打开串口！", "提示");
            }
        }
        //------------------------------------------
        //俯仰轴寻零
        //------------------------------------------
        private void button_YToZero_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (Y_Enable == false)
                {
                    MessageBox.Show("请先闭合俯仰轴！", "提示");
                    return;
                }
                if (Y_ToZero == false)//未在寻零中
                {
                    SerialRotate.Write(Rotate.YToZero(), 0, 16);//俯仰寻零
                    //pictureBox_XZero.Image = Resource1.闭合;//更改图标
                    Y_ToZero = true;//正在寻零中
                }
                //else
                //{
                //    SerialRotate.Write(Rotate.XDisable(), 0, 16);//方位闲置
                //    pictureBox_XEnable.Image = Resource1.闲置;//更改图标
                //    X_Enable = true;//已上电
                //}

            }
            else
            {
                MessageBox.Show("请先打开串口！", "提示");
            }
        }

        //------------------------------------------
        //定时发送回读数据命令和查询寻零完成命令
        //------------------------------------------
        private void CommDataSend(object source, System.Timers.ElapsedEventArgs e)
        {
            if (SerialRotate.IsOpen == true && Control_State == true)
            {
               
                byte[] CommandBytes = new byte[16];
                //回读数据命令

                //方位轴回读
                SerialRotate.Write(Rotate.Read_XPS(),0,16);
                Delay(50);//延时30ms

                //俯仰轴回读
                SerialRotate.Write(Rotate.Read_YPS(), 0, 16);
                Delay(50);//延时30ms

                //查询方位轴归零完成
                if (X_ToZero == true)
                {
                    SerialRotate.Write(Rotate.Read_XZero(), 0, 16);
                    Delay(50);//延时30ms
                }

                ////查询俯仰轴归零完成
                if (Y_ToZero == true)
                {
                    SerialRotate.Write(Rotate.Read_YZero(), 0, 16);
                    Delay(50);//延时30ms
                }

                ////反馈方位轴运行状态
                if (X_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_XState(), 0, 16);
                    Delay(50);//延时30ms
                }
                ////反馈俯仰轴运行状态
                if (Y_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_YState(), 0, 16);
                    Delay(50);//延时30ms
                }
                ////反馈方位轴运行方式
                if (X_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_XMode(), 0, 16);
                    Delay(50);//延时30ms
                }
                ////反馈俯仰轴运行方式
                if (Y_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_YMode(), 0, 16);
                    Delay(50);//延时30ms
                }
            }
        }

        private void checkBox_DebugZero_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_XDebugZero.Checked == true)
            {
                X_IsZero = true;
            }
            else
            {
                X_IsZero = false;
            }
        }

        private void checkBox_YDebugZero_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_YDebugZero.Checked == true)
            {
                Y_IsZero = true;
            }
            else
            {
                Y_IsZero = false;
            }
        }
    }
}
