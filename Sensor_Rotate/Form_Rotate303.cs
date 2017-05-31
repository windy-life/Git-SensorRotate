using RotateDll;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sensor_Rotate
{
    public partial class Form_Rotate303 : DockContent
    {
        //定义变量

        #region

        public static RotateDll.Class1 Rotate = new Class1();
        public string[] PortNum; //可用串口号
        static public SerialPort SerialRotate = new SerialPort(); //建立一个串口实例
        static public bool Control_State = false; //远控通讯状态
        //X轴变量
        public static bool X_ToZero = false; //寻零状态标志位
        public static bool X_IsZero = false; //是否已经寻零状标志位
        public static double Xposition = 0; //方位轴位置
        public static double Xspeed = 0; //方位轴速度
        public static string XState = ""; //方位轴状态
        public static bool X_Power = false; //方位上电状态
        public static bool X_Enable = false; //方位闭合状态
        public static int X_RunWay = 0; //方位运行方式(0-无；1-位置；2-速率；3-相对位置)
        public static bool X_Run = false; //运行状态标志位
        //Y轴变量
        public static bool Y_ToZero = false; //寻零状态标志位
        public static bool Y_IsZero = false; //是否已经寻零状标志位
        public static double Yposition = 0; //俯仰轴位置
        public static double Yspeed = 0; //俯仰轴速度
        public static string YState = ""; //俯仰轴状态
        public static bool Y_Power = false; //俯仰上电状态
        public static bool Y_Enable = false; //俯仰闭合状态
        public static int Y_RunWay = 0; //方位运行方式(0-无；1-位置；2-速率；3-相对位置)
        public static bool Y_Run = false; //运行状态标志位

        System.Timers.Timer T_SendData = new System.Timers.Timer(100); //创建定时器T_SendData，并设置时间间隔为50ms，定时向转台发送命令
        public Thread sendTh;
        static public System.Timers.Timer T_UpdateData = new System.Timers.Timer(10); //更新数据显示
        private double Acc = 10;
        private double Set_XAcc = 0; //方位轴已经在运行（设置）的加速度
        private double Set_YAcc = 0; //俯仰轴已经在运行（设置）的加速度
        private int XRun_Option = 0; //0-停止；1-绝对位置；2-速率；3-相对位置
        private int YRun_Option = 0; //0-停止；1-绝对位置；2-速率；3-相对位置
        public static int X_SetRunWay = 0; //方位轴设置的运行方式（0-停止；1-绝对位置；2-速率；3-相对位置）
        public static int Y_SetRunWay = 0; //俯仰轴设置的运行方式（0-停止；1-绝对位置；2-速率；3-相对位置）
        #region 剩余未处理串口数据相关变量

        private int _commDataLeftNum;//剩余数据字节个数
        private byte[] _saveBytes = new byte[30]; //存储未解析完的字节 
        #endregion

        #endregion

        public Form_Rotate303()
        {
            InitializeComponent();
            PortNum = SerialPort.GetPortNames(); //获取可用串口号
            foreach (string port in PortNum)
            {
                comboBox_SerialPort.Items.Add(port); //添加串口号
            }
            if (comboBox_SerialPort.Items.Count > 0) //有可用的串口号
            {
                comboBox_SerialPort.SelectedIndex = 0; //显示第一个号
            }

            T_SendData.Elapsed += new System.Timers.ElapsedEventHandler(CommDataSend); //指定到时执行函数


            T_UpdateData.Elapsed += new System.Timers.ElapsedEventHandler(UpdateData); //指定到时执行函数
        }

        //------------------------------------------
        //刷新串口
        //------------------------------------------
        private void button_updatePorts_Click(object sender, EventArgs e)
        {
            comboBox_SerialPort.Items.Clear(); //清空
            PortNum = SerialPort.GetPortNames(); //获取当前可用串口
            foreach (string p in PortNum)
            {
                SerialPort tempSp = new SerialPort(p);
                try
                {
                    tempSp.Open();
                    comboBox_SerialPort.Items.Add(p); //添加串口号
                    tempSp.Close();
                }
                catch
                {
                }
            }
            if (comboBox_SerialPort.Items.Count > 0) //有可用的串口号
            {
                comboBox_SerialPort.SelectedIndex = 0; //显示第一个号
            }
        }

        //------------------------------------------
        //打开/关闭串口
        //------------------------------------------
        private void button_OpenPorts_Click(object sender, EventArgs e)
        {
            try
            {
                if (SerialRotate.IsOpen == false) //串口未打开
                {
                    #region 打开

                    SerialRotate.PortName = comboBox_SerialPort.Text; //赋值串口号
                    SerialRotate.BaudRate = 9600; //波特率
                    SerialRotate.DataBits = 8; //数据位
                    SerialRotate.StopBits = (StopBits) 1; //停止位
                    SerialRotate.ReadTimeout = -1; //设置超时时间
                    SerialRotate.ReceivedBytesThreshold = 16;
                    SerialRotate.Open(); //打开串口
                    if (SerialRotate.IsOpen == true) //成功打开
                    {
                        SerialRotate.DataReceived += new SerialDataReceivedEventHandler(SerialDataProcess);
                        button_OpenPorts.Text = "关闭串口";
                        button_updatePorts.Enabled = false;
                        comboBox_SerialPort.Enabled = false;
                        T_UpdateData.Enabled = true; //打开更新数据的定时器
                        //T_SendData.Enabled = true;//启动
                        sendTh = new Thread(SendCommand);
                        sendTh.Start(); //线程开始

                        //Form_DataShow.updataDataRotate.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("打开串口失败", "error");
                    }

                    #endregion
                }
                else //已打开串口
                {
                    #region 关闭

                    if (DialogResult.No == MessageBox.Show("确定关闭串口？", "提示", MessageBoxButtons.YesNo))
                    {
                        return;
                    }
                    T_SendData.Enabled = false;
                    Delay(300); //延时300ms
                    SerialRotate.Close(); //关闭串口
                    if (SerialRotate.IsOpen == false) //成功关闭
                    {
                        button_updatePorts.Enabled = true;
                        comboBox_SerialPort.Enabled = true;
                        SerialRotate.DataReceived -= new SerialDataReceivedEventHandler(SerialDataProcess);
                        button_OpenPorts.Text = "打开串口";
                        //Form_DataShow.updataDataRotate.Enabled = false;
                        T_UpdateData.Enabled = false;
                        //T_SendData.Enabled = false;
                        sendTh.Abort(); //线程开始
                    }

                    #endregion
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
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
                Thread.Sleep(10);
            }
        }

        //------------------------------------------
        //与转台建立通讯
        //------------------------------------------
        private void button_Communicate_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == true)
            {
                if (Control_State == true) //已经建立了通讯
                {
                    if (DialogResult.No == MessageBox.Show("确定断开转台连接？", "提示", MessageBoxButtons.YesNo))
                    {
                        return;
                    }
                    SerialRotate.Write(Rotate.DisConnect(), 0, 16); //断开通讯
                    pictureBox_Communicaate.Image = Resource1.disconnect; //更新图
                    button_Communicate.Text = "连接";
                    Control_State = false; //通讯状态标志位
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
                    SerialRotate.Write(Rotate.Connect(), 0, 16); //建立通讯
                    Delay(50);
                }
            }
            else
            {
                MessageBox.Show("请先打开串口！", "提示");
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
                    MessageBox.Show("请先连接转台！", "提示");
                    return;
                }
                if (X_Power == false) //未上电
                {
                    SerialRotate.Write(Rotate.XPowerOn(), 0, 16); //方位上电
                    button_XPower.Text = "下电";
                    pictureBox_XPower.Image = Resource1.PowerOn; //更改图标
                    X_Power = true; //已上电
                }
                else
                {
                    if (X_Enable == true)
                    {
                        MessageBox.Show("请先闲置方位轴！");
                        return;
                    }
                    SerialRotate.Write(Rotate.XPowerOff(), 0, 16); //方位上电
                    button_XPower.Text = "上电";
                    pictureBox_XPower.Image = Resource1.PowerOff; //更改图标
                    X_Power = false; //已下电
                }
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
                if (X_Enable == false) //未闭合
                {
                    SerialRotate.Write(Rotate.XEnable(), 0, 16); //方位闭合
                    button_XConnect.Text = "闲置";
                    pictureBox_XEnable.Image = Resource1.闭合; //更改图标
                    X_Enable = true; //已闭合
                }
                else
                {
                    SerialRotate.Write(Rotate.XDisable(), 0, 16); //方位闲置
                    button_XConnect.Text = "闭合";
                    pictureBox_XEnable.Image = Resource1.闲置; //更改图标
                    X_Enable = false; //已下电
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
                if (X_ToZero == false) //未在寻零中
                {
                    SerialRotate.Write(Rotate.XToZero(), 0, 16); //方位寻零
                    //pictureBox_XZero.Image = Resource1.闭合;//更改图标
                    X_ToZero = true; //正在寻零中
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
                if (Y_Power == false) //未上电
                {
                    SerialRotate.Write(Rotate.YPowerOn(), 0, 16); //俯仰上电
                    button_YPower.Text = "下电";
                    pictureBox_YPower.Image = Resource1.PowerOn; //更改图标
                    Y_Power = true; //已上电
                }
                else
                {
                    SerialRotate.Write(Rotate.YPowerOff(), 0, 16); //俯仰上电
                    button_YPower.Text = "上电";
                    pictureBox_YPower.Image = Resource1.PowerOff; //更改图标
                    Y_Power = false; //已下电
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
                if (Y_Enable == false) //未闭合
                {
                    SerialRotate.Write(Rotate.YEnable(), 0, 16); //俯仰闭合
                    button_YConnect.Text = "闲置";
                    pictureBox_YEnable.Image = Resource1.闭合; //更改图标
                    Y_Enable = true; //已闭合
                }
                else
                {
                    SerialRotate.Write(Rotate.YDisable(), 0, 16); //俯仰闲置
                    button_YConnect.Text = "闭合";
                    pictureBox_YEnable.Image = Resource1.闲置; //更改图标
                    Y_Enable = false; //已闲置
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
                if (Y_ToZero == false) //未在寻零中
                {
                    SerialRotate.Write(Rotate.YToZero(), 0, 16); //俯仰寻零
                    //pictureBox_XZero.Image = Resource1.闭合;//更改图标
                    Y_ToZero = true; //正在寻零中
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
                    Hex_String += "0" + byte_tem.ToString("X").Trim() + " "; //byte转HEX方法
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
            int bytes = SerialRotate.BytesToRead; //获取缓存中的字节数
            byte[] newBuffer = new byte[bytes];
            SerialRotate.Read(newBuffer, 0, bytes); //取回所有字节
            byte[] buffer = new byte[bytes + _commDataLeftNum];//新建一个数组存储上次未处理完的数据和这次来的新数据
            for (int i = 0; i < _commDataLeftNum; i++) //赋值上次的数据
            {
                buffer[i] = _saveBytes[i];
            }
            for (int i = _commDataLeftNum; i < bytes + _commDataLeftNum; i++) //赋值新的数据
            {
                buffer[i] = newBuffer[i - _commDataLeftNum];
            }
            //DataProc(buffer);

            #region 处理数据

            try
            {
                #region 处理数据

                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == 221) //包头正确
                    {
                        #region 判断剩余数据是否完全
                        if (i + 15 >= buffer.Length) //这组数据不完整
                        {
                            //存储剩下的数据和个数
                            _commDataLeftNum = buffer.Length - i; //个数
                            for (int j = 0; j < _commDataLeftNum; j++)
                            {
                                _saveBytes[j] = buffer[j + i];
                            }
                            return; //退出
                        } 

                        #endregion

                        int CheckSum = 0; //校验
                        for (int j = i; j <= i + 15; j++)
                        {
                            CheckSum += buffer[j];
                        }
                        CheckSum = CheckSum%256;
                        if (CheckSum == 0) //校验正确
                        {
                            #region MyRegion

                            switch (buffer[i + 2])
                            {
                                case 0x13: //位置和速率

                                    #region

                                    double position = 0;
                                    double speed = 0;
                                    for (int j = i + 3; j <= i + 6; j++)
                                    {
                                        //double a = Math.Pow(256, i - 2);
                                        position += buffer[j]*Math.Pow(256, j - i - 3);

                                        speed += buffer[j + 4]*Math.Pow(256, j - i - 3);
                                    }
                                    if (position >= 2147483648) //补码
                                    {
                                        position = position - 4294967296; //获取原码 
                                    }
                                    position = position*0.00001; //乘以标度
                                    if (speed >= 2147483648) //补码
                                    {
                                        speed = speed - 4294967296; //获取原码 
                                    }
                                    speed = speed*0.00001;
                                    if (buffer[i + 1] == 0x01) //方位轴
                                    {
                                        Xposition = position;
                                        Xspeed = speed;
                                    }
                                    else //俯仰轴
                                    {
                                        Yposition = position;
                                        Yspeed = speed;
                                    }

                                    #endregion

                                    break;
                                case 0x55: //通讯成功

                                    #region

                                    MessageBox.Show("建立通讯成功！", "提示");
                                    Invoke(new MethodInvoker(delegate() { button_Communicate.Text = "断开"; }));
                                    Control_State = true; //远控连接成功
                                    pictureBox_Communicaate.Image = Resource1.connect; //更新图标

                                    #endregion

                                    break;
                                case 0x15: //归零

                                    #region

                                    if (buffer[i + 1] == 1) //方位轴
                                    {
                                        if (buffer[i + 3] == 2) //归零完成
                                        {
                                            X_ToZero = false; //寻零完成，置位标志位
                                            X_IsZero = true; //寻过零
                                            X_Run = false; //置位标志位
                                            XState = "方位轴寻零完成！";
                                        }
                                        else if (buffer[i + 3] == 1) //正在归零
                                        {
                                            X_ToZero = true; //寻零中，置位标志位
                                            XState = "方位轴寻零中…";
                                            //Invoke(new MethodInvoker(delegate () { textBox_XMessageShow.Text = "方位轴寻零中！"; }));
                                        }
                                    }
                                    else if (buffer[i + 1] == 2) //俯仰轴
                                    {
                                        if (buffer[i + 3] == 2) //归零完成
                                        {
                                            Y_ToZero = false; //寻零完成，置位标志位
                                            Y_IsZero = true; //寻过零
                                            Y_Run = false; //置位标志位
                                            YState = "俯仰轴寻零完成！";
                                        }
                                        else if (buffer[i + 3] == 1) //正在归零
                                        {
                                            Y_ToZero = true; //寻零中，置位标志位
                                            YState = "俯仰轴寻零中…";
                                            //Invoke(new MethodInvoker(delegate () { textBox_YMessageShow.Text = "俯仰轴寻零中！"; }));
                                        }
                                    }

                                    #endregion

                                    break;
                                case 0x16: //转台运行状态

                                    #region

                                    int State = 0;
                                    int Position_State = 0, Speed_State = 0, Stop_State = 0;
                                    State = buffer[i + 3]; //取值状态字节
                                    Position_State = State%2; //结果为1则已到位
                                    Speed_State = (State/2)%2; //结果为1则速率已到达
                                    Stop_State = (State/2/2)%2; //结果为1则已停止


                                    if (buffer[i + 1] == 1) //方位轴
                                    {
                                        if (Position_State == 1 && X_SetRunWay == 1 && X_Run == true)
                                        {
                                            XState = "位置已到达！";
                                            X_Run = false;
                                            return;
                                        }
                                        else if (Position_State == 0 && X_SetRunWay == 1) //&& X_Run == true)
                                        {
                                            XState = "位置运行中！";
                                            X_Run = true;
                                            return;
                                        }

                                        if (Speed_State == 1 && X_SetRunWay == 2 && X_Run == true)
                                        {
                                            XState = "速率已到达！";
                                            //X_Run = false;
                                            return;
                                        }
                                        else if (Speed_State == 0 && X_SetRunWay == 2 && X_Run == true)
                                        {
                                            XState = "速率运行中！";
                                            return;
                                        }
                                        if (Stop_State == 1 && X_SetRunWay == 0 && X_Run == true)
                                        {
                                            XState = "已停止！";
                                            X_Run = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (Position_State == 1 && Y_SetRunWay == 1 && Y_Run == true)
                                        {
                                            YState = "位置已到达！";
                                            Y_Run = false;
                                            return;
                                        }
                                        else if (Position_State == 0 && Y_SetRunWay == 1) //&& Y_Run == true)
                                        {
                                            YState = "位置运行中！";
                                            Y_Run = true;
                                            return;
                                        }

                                        if (Speed_State == 1 && Y_SetRunWay == 2 && Y_Run == true)
                                        {
                                            YState = "速率已到达！";
                                            //X_Run = false;
                                            return;
                                        }
                                        else if (Speed_State == 0 && Y_SetRunWay == 2 && Y_Run == true)
                                        {
                                            YState = "速率运行中！";
                                            return;
                                        }
                                        if (Stop_State == 1 && Y_SetRunWay == 0 && Y_Run == true)
                                        {
                                            YState = "已停止！";
                                            Y_Run = false;
                                            return;
                                        }
                                    }

                                    #endregion

                                    break;
                                case 0x17: //转台运行状态

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

                            #endregion
                        }
                    }
                }

                #endregion
            }
            catch (Exception a)
            {
                //MessageBox.Show(a.Message);
            }

            #endregion
        }

        //private void DataProc(byte[] buffer)
        //{
        //    try
        //    {
        //        #region 处理数据
        //        for (int i = 0; i < buffer.Length; i++)
        //        {
        //            if (buffer[i] == 221)//包头正确
        //            {
        //                int CheckSum = 0;//校验
        //                for (int j = i; j <= i + 15; j++)
        //                {
        //                    CheckSum += buffer[j];
        //                }
        //                CheckSum = CheckSum % 256;
        //                if (CheckSum == 0)//校验正确
        //                {
        //                    #region MyRegion
        //                    switch (buffer[i + 2])
        //                    {
        //                        case 0x13://位置和速率
        //                            #region 
        //                            double position = 0;
        //                            double speed = 0;
        //                            for (int j = i + 3; j <= i + 6; j++)
        //                            {
        //                                //double a = Math.Pow(256, i - 2);
        //                                position += buffer[j] * Math.Pow(256, j - i - 3);

        //                                speed += buffer[j + 4] * Math.Pow(256, j - i - 3);
        //                            }
        //                            if (position >= 2147483648)//补码
        //                            {
        //                                position = position - 4294967296;//获取原码 
        //                            }
        //                            position = position * 0.00001;//乘以标度
        //                            if (speed >= 2147483648)//补码
        //                            {
        //                                speed = speed - 4294967296;//获取原码 
        //                            }
        //                            speed = speed * 0.00001;
        //                            if (buffer[i + 1] == 0x01)//方位轴
        //                            {
        //                                Xposition = position;
        //                                Xspeed = speed;

        //                            }
        //                            else//俯仰轴
        //                            {
        //                                Yposition = position;
        //                                Yspeed = speed;

        //                            }
        //                            #endregion
        //                            break;
        //                        case 0x55://通讯成功
        //                            #region
        //                            MessageBox.Show("建立通讯成功！", "提示");
        //                            Invoke(new MethodInvoker(delegate () { button_Communicate.Text = "断开"; }));
        //                            Control_State = true;//远控连接成功
        //                            pictureBox_Communicaate.Image = Resource1.connect;//更新图标
        //                            #endregion
        //                            break;
        //                        case 0x15://归零
        //                            #region
        //                            if (buffer[i + 1] == 1)//方位轴
        //                            {
        //                                if (buffer[i + 3] == 2)//归零完成
        //                                {
        //                                    X_ToZero = false;//寻零完成，置位标志位
        //                                    X_IsZero = true;//寻过零
        //                                    X_Run = false;//置位标志位
        //                                    XState = "方位轴寻零完成！";
        //                                }
        //                                else if (buffer[i + 3] == 1)//正在归零
        //                                {
        //                                    X_ToZero = true;//寻零中，置位标志位
        //                                    XState = "方位轴寻零中…";
        //                                    //Invoke(new MethodInvoker(delegate () { textBox_XMessageShow.Text = "方位轴寻零中！"; }));
        //                                }
        //                            }
        //                            else if (buffer[i + 1] == 2)//俯仰轴
        //                            {
        //                                if (buffer[i + 3] == 2)//归零完成
        //                                {
        //                                    Y_ToZero = false;//寻零完成，置位标志位
        //                                    Y_IsZero = true;//寻过零
        //                                    Y_Run = false;//置位标志位
        //                                    YState = "俯仰轴寻零完成！";
        //                                }
        //                                else if (buffer[i + 3] == 1)//正在归零
        //                                {
        //                                    Y_ToZero = true;//寻零中，置位标志位
        //                                    YState = "俯仰轴寻零中…";
        //                                    //Invoke(new MethodInvoker(delegate () { textBox_YMessageShow.Text = "俯仰轴寻零中！"; }));
        //                                }
        //                            }
        //                            #endregion
        //                            break;
        //                        case 0x16://转台运行状态
        //                            #region
        //                            int State = 0;
        //                            int Position_State = 0, Speed_State = 0, Stop_State = 0;
        //                            State = buffer[i + 3];//取值状态字节
        //                            Position_State = State % 2;//结果为1则已到位
        //                            Speed_State = (State / 2) % 2;//结果为1则速率已到达
        //                            Stop_State = (State / 2 / 2) % 2;//结果为1则已停止


        //                            if (buffer[i + 1] == 1)//方位轴
        //                            {
        //                                if (Position_State == 1 && X_SetRunWay == 1 && X_Run == true)
        //                                {
        //                                    XState = "位置已到达！";
        //                                    X_Run = false;
        //                                    return;
        //                                }
        //                                else if (Position_State == 0 && X_SetRunWay == 1) //&& X_Run == true)
        //                                {
        //                                    XState = "位置运行中！";
        //                                    X_Run = true;
        //                                    return;
        //                                }

        //                                if (Speed_State == 1 && X_SetRunWay == 2 && X_Run == true)
        //                                {
        //                                    XState = "速率已到达！";
        //                                    //X_Run = false;
        //                                    return;
        //                                }
        //                                else if (Speed_State == 0 && X_SetRunWay == 2 && X_Run == true)
        //                                {
        //                                    XState = "速率运行中！";
        //                                    return;
        //                                }
        //                                if (Stop_State == 1 && X_SetRunWay == 0 && X_Run == true)
        //                                {
        //                                    XState = "已停止！";
        //                                    X_Run = false;
        //                                    return;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (Position_State == 1 && Y_SetRunWay == 1 && Y_Run == true)
        //                                {
        //                                    YState = "位置已到达！";
        //                                    Y_Run = false;
        //                                    return;
        //                                }
        //                                else if (Position_State == 0 && Y_SetRunWay == 1)//&& Y_Run == true)
        //                                {
        //                                    YState = "位置运行中！";
        //                                    Y_Run = true;
        //                                    return;
        //                                }

        //                                if (Speed_State == 1 && Y_SetRunWay == 2 && Y_Run == true)
        //                                {
        //                                    YState = "速率已到达！";
        //                                    //X_Run = false;
        //                                    return;
        //                                }
        //                                else if (Speed_State == 0 && Y_SetRunWay == 2 && Y_Run == true)
        //                                {
        //                                    YState = "速率运行中！";
        //                                    return;
        //                                }
        //                                if (Stop_State == 1 && Y_SetRunWay == 0 && Y_Run == true)
        //                                {
        //                                    YState = "已停止！";
        //                                    Y_Run = false;
        //                                    return;
        //                                }
        //                            }
        //                            #endregion
        //                            break;
        //                        case 0x17://转台运行状态
        //                            #region
        //                            //int Mode = 0;
        //                            //Mode  = RecieveBuf[2];//取值状态字节

        //                            //if (RecieveBuf[0] == 1)//方位轴
        //                            //{
        //                            //    Form_Rotate303Control.X_SetRunWay = Mode;
        //                            //}
        //                            //else if (RecieveBuf[0] == 2)//俯仰轴
        //                            //{
        //                            //    Form_Rotate303Control.Y_SetRunWay = Mode;
        //                            //}
        //                            #endregion
        //                            break;
        //                    }
        //                    #endregion
        //                }

        //            }

        //        }
        //        #endregion
        //    }
        //    catch (Exception a)
        //    {
        //        MessageBox.Show(a.Message);
        //    }
        //}
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
                SerialRotate.Write(Rotate.Read_XPS(), 0, 16);
                Delay(50); //延时30ms

                //俯仰轴回读
                SerialRotate.Write(Rotate.Read_YPS(), 0, 16);
                Delay(50); //延时30ms

                //查询方位轴归零完成
                if (X_ToZero == true)
                {
                    SerialRotate.Write(Rotate.Read_XZero(), 0, 16);
                    Delay(50); //延时30ms
                }

                ////查询俯仰轴归零完成
                if (Y_ToZero == true)
                {
                    SerialRotate.Write(Rotate.Read_YZero(), 0, 16);
                    Delay(50); //延时30ms
                }

                ////反馈方位轴运行状态
                if (X_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_XState(), 0, 16);
                    Delay(50); //延时30ms
                }
                ////反馈俯仰轴运行状态
                if (Y_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_YState(), 0, 16);
                    Delay(50); //延时30ms
                }
                ////反馈方位轴运行方式
                if (X_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_XMode(), 0, 16);
                    Delay(50); //延时30ms
                }
                ////反馈俯仰轴运行方式
                if (Y_ToZero == false)
                {
                    SerialRotate.Write(Rotate.Read_YMode(), 0, 16);
                    Delay(50); //延时30ms
                }
            }
        }

        //调试归零
        //方位
        private void checkBox_XDebugZero_CheckedChanged_1(object sender, EventArgs e)
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

        //俯仰
        private void checkBox_YDebugZero_CheckedChanged_1(object sender, EventArgs e)
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
        //选中速率
        //------------------------------------------
        private void radioButton_YModeS_CheckedChanged(object sender, EventArgs e)
        {
            label_Y1.Text = "速度0.01 ～ 100°/s";
            label_Y2.Text = "加速度0.1 ～ 50°/s²";
            groupBox_YPosition.Enabled = false;
            YRun_Option = 2;
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
        //方位轴运行
        //------------------------------------------
        private void button_XRun_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            if (Control_State == false)
            {
                MessageBox.Show("请先连接转台！");
                return;
            }
            if (X_IsZero == false)
            {
                MessageBox.Show("请先给X轴寻零 ！");
                return;
            }
            if (X_ToZero == true)
            {
                MessageBox.Show("正在寻零，请寻零完成后再转动 ！");
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
                case 0: //停止
                    SerialRotate.Write(Rotate.XStop(), 0, 16); //停止
                    textBox_X2.Enabled = true;
                    X_SetRunWay = 0;
                    break;
                case 1: //绝对位置

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
                    if ((X_SetRunWay != 1 && X_SetRunWay != 0) && X_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    SerialRotate.Write(
                            Rotate.XSet(0, Convert.ToDouble(textBox_X1.Text), Convert.ToDouble(textBox_X2.Text), Acc), 0, 16);
                        //设置绝对位置运行和方式
                    Delay(50);
                    SerialRotate.Write(Rotate.XRun_Position(), 0, 16); //设置绝对位置运行
                    X_SetRunWay = 1;
                    X_Run = true;

                    #endregion

                    break;
                case 2: //速率

                    #region

                    //加速度
                    if (Convert.ToDouble(textBox_X2.Text) < 0.1 | Convert.ToDouble(textBox_X2.Text) >= 100)
                    {
                        MessageBox.Show("方位轴加速度范围为0.1 ～ 100°/s²！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_X1.Text) < -400 | Convert.ToDouble(textBox_X1.Text) >= 400)
                    {
                        MessageBox.Show("方位轴速率范围为±400°/s！");
                        return;
                    }
                    if ((X_SetRunWay != 2 && X_SetRunWay != 0) && X_Run == true)
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
                            //textBox_X2.Text = Set_XAcc.ToString("10.00");
                            return;
                        }
                    }

                    SerialRotate.Write(
                            Rotate.XSet(1, 0, Convert.ToDouble(textBox_X1.Text), Convert.ToDouble(textBox_X2.Text)), 0, 16);
                        //设置速率运行和方式
                    textBox_X2.Enabled = false;
                    Delay(50);
                    SerialRotate.Write(Rotate.XRun_Speed(), 0, 16); //设置速率运行
                    Delay(50);
                    X_SetRunWay = 2;
                    X_Run = true;

                    #endregion

                    break;
                case 3: //相对位置

                    #region

                    if (Convert.ToDouble(textBox_X2.Text) < 0.01 | Convert.ToDouble(textBox_X2.Text) > 400)
                    {
                        MessageBox.Show("方位轴速率范围为0.01 ～ 400°/s！");
                        return;
                    }
                    if ((X_SetRunWay != 3 && X_SetRunWay != 0) && X_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    //SerialRotate.Write(Rotate.XSet(2, Convert.ToDouble(textBox_X1.Text), Convert.ToDouble(textBox_X2.Text), Acc), 0, 16);//设置相对位置运行和方式
                    //Delay(50);
                    ////SerialRotate.Write(Rotate.XRun_RPosition(), 0, 16);//设置相对位置运行
                    //X_SetRunWay = 3;
                    //X_Run = true;
                    Class_Comm.RelXRun(Convert.ToDouble(textBox_X1.Text), Convert.ToDouble(textBox_X2.Text), Acc);

                    #endregion

                    break;
            }
        }

        //------------------------------------------
        //急停
        //------------------------------------------
        private void button_EmergencyStop_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            //DialogResult button1 = new DialogResult();

            if (DialogResult.Yes == MessageBox.Show("确定急停？", "提示", MessageBoxButtons.YesNo))
            {
                SerialRotate.Write(Rotate.EmergencyStop(), 0, 16);
            }
        }

        //------------------------------------------
        //俯仰轴运行
        //------------------------------------------
        private void button_YRun_Click(object sender, EventArgs e)
        {
            if (SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            if (Control_State == false)
            {
                MessageBox.Show("请先连接转台！");
                return;
            }
            if (Y_IsZero == false)
            {
                MessageBox.Show("请先给Y轴寻零 ！");
                return;
            }
            if (Y_ToZero == true)
            {
                MessageBox.Show("正在寻零，请寻零完成后再转动 ！");
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
                case 0: //停止
                    SerialRotate.Write(Rotate.YStop(), 0, 16); //停止
                    Y_SetRunWay = 0;
                    textBox_Y2.Enabled = true;
                    break;
                case 1: //绝对位置

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
                    if ((Y_SetRunWay != 1 && Y_SetRunWay != 0) && Y_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    SerialRotate.Write(
                            Rotate.YSet(0, Convert.ToDouble(textBox_Y1.Text), Convert.ToDouble(textBox_Y2.Text), Acc), 0, 16);
                        //设置绝对位置运行和方式
                    Delay(50);
                    SerialRotate.Write(Rotate.YRun_Position(), 0, 16); //设置绝对位置运行
                    Y_SetRunWay = 1;
                    Y_Run = true;

                    #endregion

                    break;
                case 2: //速率

                    #region

                    if (Convert.ToDouble(textBox_Y2.Text) < 0.1 | Convert.ToDouble(textBox_Y2.Text) >= 50)
                    {
                        MessageBox.Show("方位轴加速度范围为0.1 ～ 50°/s²！");
                        return;
                    }
                    if (Convert.ToDouble(textBox_Y1.Text) <= -100 | Convert.ToDouble(textBox_Y1.Text) >= 100)
                    {
                        MessageBox.Show("方位轴速率范围为±100°/s！");
                        return;
                    }
                    if ((Y_SetRunWay != 2 && Y_SetRunWay != 0) && Y_Run == true)
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
                            //textBox_Y2.Text = Set_YAcc.ToString("0.00");
                            return;
                        }
                    }

                    SerialRotate.Write(
                            Rotate.YSet(1, 0, Convert.ToDouble(textBox_Y1.Text), Convert.ToDouble(textBox_Y2.Text)), 0, 16);
                        //设置速率运行和方式
                    textBox_Y2.Enabled = false;
                    Delay(50);
                    SerialRotate.Write(Rotate.YRun_Speed(), 0, 16); //设置速率运行
                    Y_SetRunWay = 2;
                    Y_Run = true;

                    #endregion

                    break;
                case 3: //相对位置

                    #region

                    if (Convert.ToDouble(textBox_Y2.Text) < 0.01 | Convert.ToDouble(textBox_Y2.Text) > 100)
                    {
                        MessageBox.Show("俯仰轴速率范围为0.01 ～ 100°/s！");
                        return;
                    }
                    if ((Y_SetRunWay != 3 && Y_SetRunWay != 0) && Y_Run == true)
                    {
                        MessageBox.Show("请先停止转台！");
                        return;
                    }
                    //SerialRotate.Write(Rotate.YSet(2, Convert.ToDouble(textBox_Y1.Text), Convert.ToDouble(textBox_Y2.Text), Acc), 0, 16);//设置相对位置运行和方式
                    //Delay(50);
                    ////SerialRotate.Write(Rotate.XRun_RPosition(), 0, 16);//设置相对位置运行
                    //Y_SetRunWay = 3;
                    //Y_Run = true;
                    Class_Comm.RelYRun(Convert.ToDouble(textBox_Y1.Text), Convert.ToDouble(textBox_Y2.Text), Acc);

                    #endregion

                    break;
            }
        }

        private void Form_Rotate303_Load(object sender, EventArgs e)
        {
            groupBox_XPosition.Enabled = false;
            textBox_X1.Text = "10";
            textBox_X2.Text = "10";
            groupBox_YPosition.Enabled = false;
            textBox_Y1.Text = "10";
            textBox_Y2.Text = "10";
        }

        //------------------------------------------
        //更新数据显示
        //------------------------------------------
        private void UpdateData(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate()
                {
                    textBox_XPosition.Text = Xposition.ToString("0.0000");
                    textBox_XSpeed.Text = Xspeed.ToString("0.0000");
                    textBox_XState.Text = XState;
                    textBox_YPosition.Text = Yposition.ToString("0.0000");
                    textBox_YSpeed.Text = Yspeed.ToString("0.0000");
                    textBox_YState.Text = YState;
                }));
            }
            catch (Exception)
            {
            }
        }

        //------------------------------------------
        //调试连接，适用于转台已经启动了远控方式，而上位机还未连接情况
        //------------------------------------------
        private void button_DebugConnect_Click(object sender, EventArgs e)
        {
            MessageBox.Show("建立通讯成功！", "提示");
            button_Communicate.Text = "断开";
            Control_State = true; //远控连接成功
            pictureBox_Communicaate.Image = Resource1.connect; //更新图标
        }

        private static void SendCommand()
        {
            int sleepTime = 20;
            while (SerialRotate.IsOpen == true)
            {
                if (SerialRotate.IsOpen == true && Control_State == true)
                {
                    //回读数据命令

                    //方位轴回读
                    SerialRotate.Write(Rotate.Read_XPS(), 0, 16);
                    //Delay(50);//延时30ms
                    Thread.Sleep(sleepTime);

                    //俯仰轴回读
                    SerialRotate.Write(Rotate.Read_YPS(), 0, 16);
                    //Delay(50);//延时30ms
                    Thread.Sleep(sleepTime);

                    //查询方位轴归零完成
                    if (X_ToZero == true)
                    {
                        SerialRotate.Write(Rotate.Read_XZero(), 0, 16);
                        //Delay(50);//延时30ms
                        Thread.Sleep(sleepTime);
                    }

                    ////查询俯仰轴归零完成
                    if (Y_ToZero == true)
                    {
                        SerialRotate.Write(Rotate.Read_YZero(), 0, 16);
                        //Delay(50);//延时30ms
                        Thread.Sleep(sleepTime);
                    }

                    ////反馈方位轴运行状态
                    if (X_ToZero == false)
                    {
                        SerialRotate.Write(Rotate.Read_XState(), 0, 16);
                        //Delay(50);//延时30ms
                        Thread.Sleep(sleepTime);
                    }
                    ////反馈俯仰轴运行状态
                    if (Y_ToZero == false)
                    {
                        SerialRotate.Write(Rotate.Read_YState(), 0, 16);
                        //Delay(50);//延时30ms
                        Thread.Sleep(sleepTime);
                    }
                    ////反馈方位轴运行方式
                    if (X_ToZero == false)
                    {
                        SerialRotate.Write(Rotate.Read_XMode(), 0, 16);
                        //Delay(50);//延时30ms
                        Thread.Sleep(sleepTime);
                    }
                    ////反馈俯仰轴运行方式
                    if (Y_ToZero == false)
                    {
                        SerialRotate.Write(Rotate.Read_YMode(), 0, 16);
                        //Delay(50);//延时30ms
                        Thread.Sleep(sleepTime);
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}