using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Sensor_Rotate.Properties;
using word;

namespace Sensor_Rotate
{
    public partial class Form_Communicate : DockContent
    {
        #region 定义变量

        System.Timers.Timer T_SendCommand; // = new System.Timers.Timer();
        public static SerialPort sp = new SerialPort(); //建立串口实例
        string[] baudeRate = {"9600", "38400", "115200", "460800"};
        string[] parity = {"None", "Odd", "Even"};
        string[] dataBits = {"7", "8", "9"};
        string[] stopBits = {"1", "1.5", "2"};
        string[] dataLength = {"8", "9", "10", "12"};
        private int dL = 0; //数据包长度
        CommandsToSend CD = new CommandsToSend(); //串口发送命令类
        //显示数据变量
        public static string rawData = string.Empty; //原始数据字符串
        StringBuilder sensorData = new StringBuilder(); //获取字符串
        //string sensorState = string.Empty;//记录状态信息
        public static double XAngle, YAngle, Tempreature, Gyro = 0; //X轴和Y轴的角度
        //public static float azimuth, pitch, roll = 0;//罗盘的方位、俯仰和横滚
        public static string readWriteData = string.Empty; //读写参数界面返回的指令
        public static string readWriteState = string.Empty; //读写参数界面状态
        public static string yCalibrationData = string.Empty; //Y轴标定界面的返回数据
        public static string yCalibrationState = string.Empty; //Y轴标定界面的返回状态 
        public static string xCalibrationData = string.Empty; //X轴标定界面的返回数据
        public static string xCalibrationState = string.Empty; //X轴标定界面的返回状态 
        public static string tCalibrationData = string.Empty; //温度标定界面的返回数据
        public static string tCalibrationState = string.Empty; //温度标定界面的返回状态 
        public static string gCalibrationData = string.Empty; //陀螺标定界面的返回数据
        public static string gCalibrationState = string.Empty; //陀螺标定界面的返回状态
        public static string initiateData = string.Empty; //初始化界面的返回数据

        #region 测试记录中的变量

        private Form_WordConfig frmWord = new Form_WordConfig();//新建一个word模板窗体设置 实例

        private int[] testPoints = new[] {11, 13, 13}; //±15,±30,±60  三类产品需要测试的点数
        Dictionary<int, double> _dicTestAuto = new Dictionary<int, double>(); //存储自动测试时的数据
        private const int RangeNum = 3; //有几种测量范围
        int[][] testPosition = new int[RangeNum][]; //测试点
        private int[] position15 = new[] {-15, -12, -9, -6, -3, 0, 3, 6, 9, 12, 15}; //15度测试点
        private int[] position30 = new[] {-30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30}; //30度测试点
        private int[] position60 = new[] {-60, -50, -40, -30, -20, -10, 0, 10, 20, 30, 40, 50, 60}; //60度测试点
        private bool collectXFlag, collectYFlag; //测试时采集精度标志位
        List<double> listXData = new List<double>(); //存储X轴测试值
        List<double> listYData = new List<double>(); //存储Y轴测试值
        List<double> listXAccuracy = new List<double>(); //存储X轴测试的精度
        List<double> listYAccuracy = new List<double>(); //存储Y轴测试的精度
        //private string projectPath = Environment.CurrentDirectory;
        private string[] wordModelPath = new[]
        {
            Environment.CurrentDirectory + @"\模板15度倾斜传感器测试记录.doc",
            Environment.CurrentDirectory + @"\模板30度倾斜传感器测试记录.doc",
            Environment.CurrentDirectory + @"\模板60度倾斜传感器测试记录.doc"
        };

        /// <summary>
        /// 模板文件中产品信息的书签
        /// </summary>
        private string[] _bookmarks = new[] //word中的书签
            {"产品名称", "产品型号", "产品编号", "测量范围", "输入电压", "输入电流", "要求精度", "实测精度", "测试人员", "测试日期"};

        string[][] _dataBookMarks = new string[RangeNum][]; //定义多维数组存储不同模板文件中的书签

        /// <summary>
        /// 15度倾角模板中表格的书签
        /// </summary>
        private string[] _bMarks15 = new[]
        {
            "DataN15", "DataN12", "DataN9", "DataN6", "DataN3", "Data0", "DataP3", "DataP6", "DataP9", "DataP12",
            "DataP15",
            "AccuracyN15", "AccuracyN12", "AccuracyN9", "AccuracyN6", "AccuracyN3", "Accuracy0", "AccuracyP3",
            "AccuracyP6", "AccuracyP9", "AccuracyP12", "AccuracyP15",
            "ResultN15", "ResultN12", "ResultN9", "ResultN6", "ResultN3", "Result0", "ResultP3", "ResultP6", "ResultP9",
            "ResultP12", "ResultP15"
        };

        /// <summary>
        /// 30度倾角模板中表格的书签
        /// </summary>
        private string[] _bMarks30 = new[]
        {
            "DataN30", "DataN25", "DataN20", "DataN15", "DataN10", "DataN5", "Data0", "DataP5", "DataP10", "DataP15",
            "DataP20", "DataP25", "DataP30",
            "AccuracyN30", "AccuracyN25", "AccuracyN20", "AccuracyN15", "AccuracyN10", "AccuracyN5", "Accuracy0",
            "AccuracyP5", "AccuracyP10", "AccuracyP15", "AccuracyP20", "AccuracyP25", "AccuracyP30",
            "ResultN30", "ResultN25", "ResultN20", "ResultN15", "ResultN10", "ResultN5", "Result0", "ResultP5",
            "ResultP10", "ResultP15", "ResultP20", "ResultP25", "ResultP30"
        };

        /// <summary>
        /// 60度倾角模板中表格的书签
        /// </summary>
        private string[] _bMarks60 = new[]
        {
            "DataN60", "DataN50", "DataN40", "DataN30", "DataN20", "DataN10", "Data0", "DataP10", "DataP20", "DataP30",
            "DataP40", "DataP50", "DataP60",
            "AccuracyN60", "AccuracyN50", "AccuracyN40", "AccuracyN30", "AccuracyN20", "AccuracyN10", "Accuracy0",
            "AccuracyP10", "AccuracyP20", "AccuracyP30", "AccuracyP40", "AccuracyP50", "AccuracyP60",
            "ResultN60", "ResultN50", "ResultN40", "ResultN30", "ResultN20", "ResultN10", "Result0", "ResultP10",
            "ResultP20", "ResultP30", "ResultP40", "ResultP50", "ResultP60"
        };

        #endregion

        #endregion

        public Form_Communicate()
        {
            
            #region 赋值二维数组
            //赋值测试点
            testPosition[0] = position15;
            testPosition[1] = position30;
            testPosition[2] = position60;
            //赋值书签
            _dataBookMarks[0] = _bMarks15;
            _dataBookMarks[1] = _bMarks30;
            _dataBookMarks[2] = _bMarks60; 
            #endregion

            InitializeComponent();
            RefreshPorts(); //刷新串口
            FillCombox(comboBox_CommBaudeRate, baudeRate);
            FillCombox(comboBox_CommParity, parity);
            FillCombox(comboBox_CommDatabits, dataBits);
            FillCombox(comboBox_CommStopBits, stopBits);
            FillCombox(comboBox_DataLength, dataLength);
            comboBox_CommDatabits.SelectedIndex = 1;
            //sp.DataReceived += new SerialDataReceivedEventHandler(SerialDataProc);
        }

        private void Form_Communicate_Load(object sender, EventArgs e)
        {
            rdbTestAuto_CheckedChanged(null, null);
        }

        #region 函数

        //-->函数:刷新串口号
        public void RefreshPorts()
        {
            comboBox_CommPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string p in ports)
            {
                comboBox_CommPort.Items.Add(p);
            }
            if (comboBox_CommPort.Items.Count > 0)
                comboBox_CommPort.SelectedIndex = 0; //显示第一个串口号
        }

        //-->函数:填充串口相关参数的combox
        private void FillCombox(ComboBox box, string[] data)
        {
            box.Items.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                box.Items.Add(data[i]);
            }
            if (data.Length > 0)
            {
                box.SelectedIndex = 0;
            }
        }

        //-->函数:设置串口校验位
        private void SetParity(SerialPort sp1, ComboBox box)
        {
            if (box.Text == null || box.Text == "")
            {
                MessageBox.Show("请输入正确的校验位");
                return;
            }
            char[] pa = box.Text.ToCharArray();
            switch (pa[0])
            {
                case 'N':
                    sp1.Parity = Parity.None;
                    break;
                case 'O':
                    sp1.Parity = Parity.Odd;
                    break;
                case 'E':
                    sp1.Parity = Parity.Even;
                    break;
            }
        }

        //-->函数:串口接收函数
        private void SerialDataProc(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytes = sp.BytesToRead;
                byte[] buffer = new byte[bytes];
                sp.Read(buffer, 0, bytes);
                DataProc(buffer);
            }
            catch (Exception)
            {
                //MessageBox.Show(a.Message);
            }
        }

        //-->函数:数据处理函数
        private void DataProc(byte[] data)
        {
            int sysPart; //系统区
            int checkSum = 0; //和校验
            string[] bytesData = new string[21]; //写入文件的命令字符串
            for (int i = 0; i < data.Length; i++)
            {
                switch (data[i])
                {
                        #region TCM罗盘数据格式

                        //case 0x00://TCM罗盘数据格式
                        //    if (data[i + 1] == 0x15)
                        //    {
                        //        sensorData.Clear();
                        //        for (int j = i; j < i + 21; j++)
                        //        {
                        //            sensorData.Append(data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' '));
                        //        }
                        //        rawData = sensorData.ToString();//更新数据

                        //        if (data[i + 4] == 0x05)//方位
                        //        {
                        //            byte[] az = new byte[] { data[i + 8], data[i + 7], data[i + 6], data[i + 5] };
                        //            azimuth = BitConverter.ToSingle(az, 0);
                        //        }
                        //        if (data[i + 9] == 0x18)//俯仰
                        //        {
                        //            byte[] pi = new byte[] { data[i + 13], data[i + 12], data[i + 11], data[i + 10] };
                        //            pitch = BitConverter.ToSingle(pi, 0);
                        //        }
                        //        if (data[i + 14] == 0x19)//横滚
                        //        {
                        //            byte[] ro = new byte[] { data[i + 18], data[i + 17], data[i + 16], data[i + 15] };
                        //            roll = BitConverter.ToSingle(ro, 0);
                        //        }
                        //        #region 记录数据
                        //        if (Form_TCM._collectFlag == true)
                        //        {
                        //            if (!File.Exists(Form_TCM._path))//文件不存在则创建一个
                        //            {
                        //                FileStream file_WR = new FileStream(Form_TCM._path, FileMode.Create, FileAccess.ReadWrite);//创建文件 
                        //                file_WR.Close();
                        //            }
                        //            using (FileStream fileW = new FileStream(Form_TCM._path, FileMode.Append, FileAccess.Write))//写入数据
                        //            {
                        //                StreamWriter fileWrite = new StreamWriter(fileW);
                        //                fileWrite.WriteLine(string.Format(@"{0}   {1}    {2}     {3}    {4}", DateTime.Now, Form_TCM._countNum, azimuth.ToString("0.0"), roll.ToString("0.000"), pitch.ToString("0.000")));
                        //                fileWrite.Close();
                        //            }

                        //        }
                        //        #endregion
                        //}
                        //break; 

                        #endregion

                    case 0xAA: //传感器数据

                        #region 传感器数据


                        #region 赋值标度因数
                        double scaleFactor = 0;
                        if (Form_ProductTree._s2000Flag == 0)
                        {
                            scaleFactor = 0.0005;
                        }
                        else
                        {
                            scaleFactor = 0.001;
                        } 
                        #endregion
                        if (data[i + 10] == 0x0D && data[i + 11] == 0x0A) //判断是否是配置模式下的数据
                        {
                            //配置模式下数据
                            sensorData.Clear();
                            for (int j = i; j < i + 12; j++)
                            {
                                sensorData.Append(data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' '));
                            }
                            rawData = sensorData.ToString(); //更新数据
                            //解算数据

                            #region X轴

                            int _xA = data[i + 1]*256 + data[i + 2];
                            if (_xA >= 32768)
                            {
//取负值
                                _xA = _xA - 65536;
                            }
                            XAngle = _xA* scaleFactor;

                            #endregion

                            #region Y轴

                            int _yA = data[i + 3]*256 + data[i + 4];
                            if (_yA >= 32768)
                            {
//取负值
                                _yA = _yA - 65536;
                            }
                            YAngle = _yA* scaleFactor;

                            #endregion

                            #region 陀螺

                            int _gyro = data[i + 5]*256 + data[i + 6];
                            if (_gyro >= 32768)
                            {
//取负值
                                _gyro = _gyro - 65536;
                            }
                            Gyro = _gyro*0.01;

                            #endregion

                            #region 温度

                            int _tempreature = data[i + 7]*256 + data[i + 8];
                            if (_tempreature >= 32768)
                            {
//取负值
                                _tempreature = _tempreature - 65536;
                            }
                            Tempreature = _tempreature*0.01;

                            #endregion

                            i = i + 11;
                        }
                        else
                        {
                            //正常模式下数据
                            sensorData.Clear();
                            checkSum = 0;
                            for (int j = i; j < i + dL; j++)
                            {
                                sensorData.Append(data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' '));
                                if (j > i && j < dL + i - 1)
                                {
                                    checkSum += data[j];
                                }
                            }
                            if (checkSum%256 == data[i + dL - 1]) //和校验
                            {
                                rawData = sensorData.ToString(); //更新数据
                                //解算数据

                                #region X轴

                                int _xA = data[i + 1]*256 + data[i + 2];
                                if (_xA >= 32768)
                                {
//取负值
                                    _xA = _xA - 65536;
                                }
                                XAngle = _xA* scaleFactor;
                                if (collectXFlag)
                                {
                                    listXData.Add(XAngle); //添加记录数据
                                }

                                #endregion

                                #region Y轴

                                int _yA = data[i + 3]*256 + data[i + 4];
                                if (_yA >= 32768)
                                {
//取负值
                                    _yA = _yA - 65536;
                                }
                                YAngle = _yA* scaleFactor;
                                if (collectYFlag)
                                {
                                    listYData.Add(YAngle); //添加记录数据
                                }

                                #endregion

                                #region 陀螺

                                int _gyro = data[i + 5]*256 + data[i + 6];
                                if (_gyro >= 32768)
                                {
//取负值
                                    _gyro = _gyro - 65536;
                                }
                                Gyro = _gyro*0.01;

                                #endregion

                                #region 温度

                                int _tempreature = data[i + 7]*256 + data[i + 8];
                                if (_tempreature >= 32768)
                                {
//取负值
                                    _tempreature = _tempreature - 65536;
                                }
                                Tempreature = _tempreature*0.01;

                                #endregion

                                i = i + dL - 1;
                            }
                        }

                        #endregion

                        break;
                    case 0xED: //读设置及参数的指令

                        #region 读设置及参数的指令

                        //获取指令
                        //readWriteData = "";
                        //for (int j = i; j <= i + 7; j++)
                        //{
                        //    readWriteData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                        //}
                        switch (data[i + 1])
                        {
                            case 0xE0: //读系统工作模式指令 返回指令格式：ED E0 d0 d1 d2 FF 0D 0A

                                #region 读工作模式

                                if (data[i + 6] == 0x0D && data[i + 7] == 0x0A) //数据正确
                                {
                                    readWriteState = "";
                                    readWriteState = "读【系统工作模式】成功   ";

                                    readWriteState += "系统版本号" + data[i + 2] + "   ";

                                    if (data[i + 3] == 0)

                                        readWriteState += "倾角目前处于不工作状态，需命令才能启动倾角进入工作状态   ";
                                    else
                                        readWriteState += "倾角为自启动模式   ";


                                    readWriteState += "数据输出定时器T0：" + data[i + 4] + "ms   ";

                                    readWriteState += " 数据初始化调用参数次数：" + data[i + 5] + "次   ";

                                    //获取指令
                                    readWriteData = "";
                                    for (int j = i; j <= i + 7; j++)
                                    {
                                        readWriteData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                    }
                                    i = i + 7;
                                }

                                #endregion

                                break;
                            case 0xE1:

                                #region 读CAN口配置1

                                if (data[i + 9] == 0x0D && data[i + 10] == 0x0A)
                                {
                                    readWriteState = "读【CAN口配置1】成功   ";

                                    if (data[i + 2] == 0)
                                        readWriteState += "CAN总线没有启动   ";
                                    else
                                        readWriteState += "CAN总线已经启动   ";

                                    if (data[i + 3] == 0)
                                        readWriteState += "数据输出定时器T1：" + data[i + 3] + "ms，应答输出   ";
                                    else
                                        readWriteState += "数据输出定时器T1：" + data[i + 3] + "ms，不是应答输出   ";

                                    readWriteState += "滤波级数为" + data[i + 4] + "级   ";

                                    if (data[i + 5] == 0)
                                        readWriteState += "输出格式为默认16进制格式   ";
                                    else
                                        readWriteState += "输出格式为8字节16进制格式   ";

                                    switch (data[i + 6])
                                    {
                                        case 0x0:
                                            readWriteState += "波特率50kHz   ";
                                            break;
                                        case 0x1:
                                            readWriteState += "波特率100kHz   ";
                                            break;
                                        case 0x2:
                                            readWriteState += "波特率125kHz   ";
                                            break;
                                        case 0x3:
                                            readWriteState += "波特率250kHz   ";
                                            break;
                                        case 0x4:
                                            readWriteState += "波特率500kHz   ";
                                            break;
                                        case 0x5:
                                            readWriteState += "波特率1MHz   ";
                                            break;
                                    }

                                    switch (data[i + 7])
                                    {
                                        case 0x0:
                                            readWriteState += "帧模式：标准帧   ";
                                            break;
                                        case 0x1:
                                            readWriteState += "帧模式：扩展帧   ";
                                            break;
                                    }
                                    //获取指令
                                    readWriteData = "";
                                    for (int j = i; j <= i + 11; j++)
                                    {
                                        readWriteData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                    }
                                    i = i + 10;
                                }

                                #endregion

                                break;
                            case 0xE2:

                                #region 读CAN口配置2

                                if (data[i + 11] == 0x0D && data[i + 12] == 0x0A)
                                {
                                    readWriteState = "读【CAN口配置2】成功   ";
                                    readWriteState += "发送ID为：" +
                                                      data[i + 2].ToString("X").PadLeft(2, '0').PadRight(3, ' ') +
                                                      data[i + 3].ToString("X").PadLeft(2, '0').PadRight(3, ' ') +
                                                      data[i + 4].ToString("X").PadLeft(2, '0').PadRight(3, ' ') +
                                                      data[i + 5].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                    readWriteState += "接收ID为：" +
                                                      data[i + 6].ToString("X").PadLeft(2, '0').PadRight(3, ' ') +
                                                      data[i + 7].ToString("X").PadLeft(2, '0').PadRight(3, ' ') +
                                                      data[i + 8].ToString("X").PadLeft(2, '0').PadRight(3, ' ') +
                                                      data[i + 9].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                    //获取指令
                                    readWriteData = "";
                                    for (int j = i; j <= i + 7; j++)
                                    {
                                        readWriteData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                    }
                                    i = i + 12;
                                }

                                #endregion

                                break;
                            case 0xE3:

                                #region 串行口A配置

                                if (data[i + 9] == 0x0D && data[i + 10] == 0x0A)
                                {
                                    readWriteState = "读【串行口A配置】成功   ";
                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            readWriteState += "串行口A未使能   ";
                                            break;
                                        default:
                                            readWriteState += "串行口A已使能   ";
                                            break;
                                    }

                                    if (data[i + 3] == 0)
                                        readWriteState += "数据输出定时器T2：" + data[i + 3] + "ms，应答输出   ";
                                    else
                                        readWriteState += "数据输出定时器T2：" + data[i + 3] + "ms，不是应答输出   ";

                                    readWriteState += "滤波级数为：" + data[i + 4] + "级   ";

                                    if (data[i + 5] == 0)
                                        readWriteState += "输出格式为默认16进制格式   ";
                                    else
                                        readWriteState += "输出格式为8字节16进制格式   ";

                                    switch (data[i + 6])
                                    {
                                        case 0x0:
                                            readWriteState += "波特率1200KHz   ";
                                            break;
                                        case 0x1:
                                            readWriteState += "波特率2400KHz   ";
                                            break;
                                        case 0x2:
                                            readWriteState += "波特率4800KHz   ";
                                            break;
                                        case 0x3:
                                            readWriteState += "波特率9600KHz   ";
                                            break;
                                        case 0x4:
                                            readWriteState += "波特率19200KHz   ";
                                            break;
                                        case 0x5:
                                            readWriteState += "波特率38400KHz   ";
                                            break;
                                        case 0x6:
                                            readWriteState += "波特率57600KHz   ";
                                            break;
                                        case 0x7:
                                            readWriteState += "波特率76800KHz   ";
                                            break;
                                        case 0x8:
                                            readWriteState += "波特率115200KHz   ";
                                            break;
                                    }
                                    switch (data[i + 7])
                                    {
                                        case 0x0:
                                            readWriteState += "串行口A接口类型:RS232   ";
                                            break;
                                        case 0x1:
                                            readWriteState += "串行口A接口类型:RS422   ";
                                            break;
                                        case 0x2:
                                            readWriteState += "串行口A接口类型:RS485   ";
                                            break;
                                    }
                                    //获取指令
                                    readWriteData = "";
                                    for (int j = i; j <= i + 7; j++)
                                    {
                                        readWriteData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                    }
                                    i = i + 10;
                                }

                                #endregion

                                break;
                            case 0xE4:

                                #region 串行口B配置

                                if (data[i + 9] == 0x0D && data[i + 10] == 0x0A)
                                {
                                    readWriteState = "读【串行口B配置】成功   ";
                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            readWriteState += "串行口B未使能   ";
                                            break;
                                        default:
                                            readWriteState += "串行口B已使能   ";
                                            break;
                                    }

                                    if (data[i + 3] == 0)
                                        readWriteState += "数据输出定时器T3：" + data[i + 3] + "ms，应答输出   ";
                                    else
                                        readWriteState += "数据输出定时器T3：" + data[i + 3] + "ms，不是应答输出   ";

                                    readWriteState += "滤波级数为：" + data[i + 4] + "级   ";

                                    if (data[i + 5] == 0)
                                        readWriteState += "输出格式为默认16进制格式   ";
                                    else
                                        readWriteState += "输出格式为8字节16进制格式   ";

                                    switch (data[i + 6])
                                    {
                                        case 0x0:
                                            readWriteState += "波特率1200KHz   ";
                                            break;
                                        case 0x1:
                                            readWriteState += "波特率2400KHz   ";
                                            break;
                                        case 0x2:
                                            readWriteState += "波特率4800KHz   ";
                                            break;
                                        case 0x3:
                                            readWriteState += "波特率9600KHz   ";
                                            break;
                                        case 0x4:
                                            readWriteState += "波特率19200KHz   ";
                                            break;
                                        case 0x5:
                                            readWriteState += "波特率38400KHz   ";
                                            break;
                                        case 0x6:
                                            readWriteState += "波特率57600KHz   ";
                                            break;
                                        case 0x7:
                                            readWriteState += "波特率76800KHz   ";
                                            break;
                                        case 0x8:
                                            readWriteState += "波特率115200KHz   ";
                                            break;
                                    }

                                    switch (data[i + 7])
                                    {
                                        case 0x0:
                                            readWriteState += "串行口B接口类型:RS232   ";
                                            break;
                                        case 0x1:
                                            readWriteState += "串行口B接口类型:RS422   ";
                                            break;
                                        case 0x2:
                                            readWriteState += "串行口B接口类型:RS485   ";
                                            break;
                                    }
                                    //获取指令
                                    readWriteData = "";
                                    for (int j = i; j <= i + 7; j++)
                                    {
                                        readWriteData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                    }
                                    i = i + 10;
                                }

                                #endregion

                                break;
                            default: //读系统参数
                                if (data[i + 19] == 0x0D && data[i + 20] == 0x0A) //数据正确
                                {
                                    readWriteState = "";

                                    #region 系统0~1区

                                    for (sysPart = 0; sysPart <= 1; sysPart++)
                                    {
                                        if (data[i + 1] == sysPart)
                                        {
                                            readWriteState = "读【配置】参数" + sysPart + "区成功   ";

                                            #region 判断0区

                                            if (sysPart == 0)
                                            {
                                                readWriteState += "软件版本号" + data[i + 2] + "   ";

                                                if (data[i + 3] == 0)

                                                    readWriteState += "倾角目前处于不工作状态，需命令才能启动倾角进入工作状态   ";
                                                else
                                                    readWriteState += "倾角为自启动模式   ";

                                                if (data[i + 4] == 0)
                                                    readWriteState += "CAN总线没有启动   ";
                                                else
                                                    readWriteState += "CAN总线已经启动   ";


                                                readWriteState += "数据输出时间间隔时间：" + data[i + 5] + "ms   ";
                                                readWriteState += "滤波级数为：" + data[i + 6] + "级   ";
                                                if (data[i + 7] == 0)
                                                    readWriteState += "输出格式为默认16进制格式   ";
                                                else
                                                    readWriteState += "输出格式为8字节16进制格式   ";

                                                switch (data[i + 8])
                                                {
                                                    case 0x0:
                                                        readWriteState += "波特率50kHz   ";
                                                        break;
                                                    case 0x1:
                                                        readWriteState += "波特率100kHz   ";
                                                        break;
                                                    case 0x2:
                                                        readWriteState += "波特率125kHz   ";
                                                        break;
                                                    case 0x3:
                                                        readWriteState += "波特率250kHz   ";
                                                        break;
                                                    case 0x4:
                                                        readWriteState += "波特率500kHz   ";
                                                        break;
                                                    case 0x5:
                                                        readWriteState += "波特率1MHz   ";
                                                        break;
                                                }

                                                switch (data[i + 9])
                                                {
                                                    case 0x0:
                                                        readWriteState += "帧模式:标准帧   ";
                                                        break;
                                                    case 0x1:
                                                        readWriteState += "帧模式:扩展帧   ";
                                                        break;
                                                }

                                                readWriteState += "发送ID为：" +
                                                                  data[i + 10].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 11].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 12].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 13].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "接收ID为：" +
                                                                  data[i + 14].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 15].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 16].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 17].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断1区

                                            if (sysPart == 1)
                                            {
                                                switch (data[i + 2])
                                                {
                                                    case 0x0:
                                                        readWriteState += "倾角串行口A没有启动   ";
                                                        break;
                                                    default:
                                                        readWriteState += "倾角串行口A已经启动   ";
                                                        break;
                                                }

                                                readWriteState += "数据输出时间间隔时间：" +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') + "ms   ";
                                                readWriteState += "滤波级数为：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') + "级   ";
                                                if (data[i + 5] == 0)
                                                    readWriteState += "输出格式为默认16进制格式   ";
                                                else
                                                    readWriteState += "输出格式为8字节16进制格式   ";

                                                switch (data[i + 6])
                                                {
                                                    case 0x0:
                                                        readWriteState += "波特率1200KHz   ";
                                                        break;
                                                    case 0x1:
                                                        readWriteState += "波特率2400KHz   ";
                                                        break;
                                                    case 0x2:
                                                        readWriteState += "波特率4800KHz   ";
                                                        break;
                                                    case 0x3:
                                                        readWriteState += "波特率9600KHz   ";
                                                        break;
                                                    case 0x4:
                                                        readWriteState += "波特率19200KHz   ";
                                                        break;
                                                    case 0x5:
                                                        readWriteState += "波特率38400KHz   ";
                                                        break;
                                                    case 0x6:
                                                        readWriteState += "波特率57600KHz   ";
                                                        break;
                                                    case 0x7:
                                                        readWriteState += "波特率76800KHz   ";
                                                        break;
                                                    case 0x8:
                                                        readWriteState += "波特率115200KHz   ";
                                                        break;
                                                }

                                                switch (data[i + 7])
                                                {
                                                    case 0x0:
                                                        readWriteState += "串行口A接口类型:RS232   ";
                                                        break;
                                                    case 0x1:
                                                        readWriteState += "串行口A接口类型:RS422   ";
                                                        break;
                                                    case 0x2:
                                                        readWriteState += "串行口A接口类型:RS485   ";
                                                        break;
                                                }

                                                switch (data[i + 8])
                                                {
                                                    case 0x0:
                                                        readWriteState += "倾角串行口B没有启动   ";
                                                        break;
                                                    default:
                                                        readWriteState += "倾角串行口B已经启动   ";
                                                        break;
                                                }
                                                readWriteState += "数据输出时间间隔时间：" + data[i + 9] + "ms   ";
                                                readWriteState += "滤波级数为：" + data[i + 10] + "级   ";
                                                if (data[i + 11] == 0)
                                                    readWriteState += "输出格式为默认16进制格式   ";
                                                else
                                                    readWriteState += "输出格式为8字节16进制格式   ";

                                                switch (data[i + 12])
                                                {
                                                    case 0x0:
                                                        readWriteState += "波特率1200KHz   ";
                                                        break;
                                                    case 0x1:
                                                        readWriteState += "波特率2400KHz   ";
                                                        break;
                                                    case 0x2:
                                                        readWriteState += "波特率4800KHz   ";
                                                        break;
                                                    case 0x3:
                                                        readWriteState += "波特率9600KHz   ";
                                                        break;
                                                    case 0x4:
                                                        readWriteState += "波特率19200KHz   ";
                                                        break;
                                                    case 0x5:
                                                        readWriteState += "波特率38400KHz   ";
                                                        break;
                                                    case 0x6:
                                                        readWriteState += "波特率57600KHz   ";
                                                        break;
                                                    case 0x7:
                                                        readWriteState += "波特率76800KHz   ";
                                                        break;
                                                    case 0x8:
                                                        readWriteState += "波特率115200KHz   ";
                                                        break;
                                                }

                                                switch (data[i + 13])
                                                {
                                                    case 0x0:
                                                        readWriteState += "串行口B接口类型:RS232   ";
                                                        break;
                                                    case 0x1:
                                                        readWriteState += "串行口B接口类型:RS422   ";
                                                        break;
                                                    case 0x2:
                                                        readWriteState += "串行口B接口类型:RS485   ";
                                                        break;
                                                }
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion
                                        }
                                    }

                                    #endregion

                                    #region 系统2~13区

                                    for (sysPart = 2; sysPart <= 14; sysPart++)
                                    {
                                        if (sysPart == data[i + 1])
                                        {
                                            readWriteState = "读【系统】参数" + sysPart + "区成功   ";

                                            #region 判断2区

                                            if (sysPart == 2)
                                            {
                                                readWriteState += "X轴电压零位：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴角度标度：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴角度零位：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断3区

                                            if (sysPart == 3)
                                            {
                                                readWriteState += "X轴+15度二补值：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴+12度二补值：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴+09度二补值：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴+06度二补值：" +
                                                                  data[i + 8].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 9].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴+03度二补值：" +
                                                                  data[i + 10].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 11].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴+00度二补值：" +
                                                                  data[i + 12].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 13].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴-03度二补值：" +
                                                                  data[i + 14].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 15].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴-06度二补值：" +
                                                                  data[i + 16].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 17].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断4区

                                            if (sysPart == 4)
                                            {
                                                readWriteState += "X轴-09度二补值：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴-12度二补值：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "X轴-15度二补值：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断5区

                                            if (sysPart == 5)
                                            {
                                                readWriteState += "Y轴电压零位：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴角度标度：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴角度零位：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断6区

                                            if (sysPart == 6)
                                            {
                                                readWriteState += "Y轴+15度二补值：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴+12度二补值：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴+09度二补值：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴+06度二补值：" +
                                                                  data[i + 8].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 9].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴+03度二补值：" +
                                                                  data[i + 10].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 11].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴+00度二补值：" +
                                                                  data[i + 12].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 13].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴-03度二补值：" +
                                                                  data[i + 14].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 15].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴-06度二补值：" +
                                                                  data[i + 16].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 17].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断7区

                                            if (sysPart == 7)
                                            {
                                                readWriteState += "Y轴-09度二补值：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴-12度二补值：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "Y轴-15度二补值：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断8区

                                            if (sysPart == 8)
                                            {
                                                readWriteState += "角速率零位：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速率标度：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断9区

                                            if (sysPart == 9)
                                            {
                                                readWriteState += "角速度100度二补值：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度80度二补值：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度60度二补值：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度40度二补值：" +
                                                                  data[i + 8].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 9].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度20度二补值：" +
                                                                  data[i + 10].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 11].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度0度二补值：" +
                                                                  data[i + 12].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 13].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度-20度二补值：" +
                                                                  data[i + 14].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 15].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度-40度二补值：" +
                                                                  data[i + 16].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 17].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断10区

                                            if (sysPart == 10)
                                            {
                                                readWriteState += "角速度-60度二补值：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度-80度二补值：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "角速度-100度二补值：" +
                                                                  data[i + 6].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 7].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion

                                            #region 判断11区

                                            if (sysPart == 11)
                                            {
                                                readWriteState += "温度零位：" +
                                                                  data[i + 2].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 3].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                readWriteState += "温度标度：" +
                                                                  data[i + 4].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ') +
                                                                  data[i + 5].ToString("X")
                                                                      .PadLeft(2, '0')
                                                                      .PadRight(3, ' ');
                                                SaveData(sysPart, data, i);
                                            }

                                            #endregion
                                        }
                                    }

                                    #endregion

                                    readWriteData = "";
                                    for (int j = 0; j < 21; j++)
                                    {
                                        readWriteData += data[i].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                                        i++;
                                    }
                                    //File.WriteAllLines(filePath, bytesData);
                                    i = i + 20;
                                }
                                break;
                        }

                        #endregion

                        break;
                    case 0xEF: //启动配置与调试指令 返回指令：EF 00 00 00 00 FF 0D 0A

                        #region 启动配置与调试指令

                        //获取指令
                        //Invoke(new MethodInvoker(delegate () { textBox_DataBack.Text = ""; }));// textBox_DataBack.Text = "";
                        string str = string.Empty;
                        for (int j = i; j <= i + 7; j++)
                        {
                            str += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                        }

                        if (data[i + 5] == 0xFF && data[i + 6] == 0x0D && data[i + 7] == 0x0A)
                        {
                            Invoke(new MethodInvoker(delegate() { textBox_DataBack.Text = str; }));
                            if (data[i + 1] != 0x0 && data[i + 2] != 0x0 && data[i + 3] != 0x0 && data[i + 4] != 0x0)
                            {
                                if (data[i + 1] == 0x11 && data[i + 2] == 0x11 && data[i + 3] == 0x11 &&
                                    data[i + 4] == 0x11)
                                    Invoke(new MethodInvoker(delegate() { textBox_ConfigureState.Text = "进入倾角配置状态!"; }));
                                //
                                else
                                    Invoke(new MethodInvoker(delegate() { textBox_ConfigureState.Text = "写入配置错误!"; }));
                                //
                            }
                            if (data[i + 1] == 0x0 && data[i + 2] == 0x0 && data[i + 3] == 0x0 && data[i + 4] == 0x0)
                                Invoke(new MethodInvoker(delegate() { textBox_ConfigureState.Text = "写入配置完成!"; }));
                            //textBox_ConfigureState.Text = "写入配置完成!";
                        }

                        #endregion

                        break;
                    case 0xE0:

                        #region 配置系统工作模式 返回指令：E0 d0 d1 FF 0D 0A

                        if (data[i + 3] == 0xFF && data[i + 4] == 0x0D && data[i + 5] == 0x0A)
                        {
                            string str1 = string.Empty;
                            //获取指令

                            for (int j = i; j <= i + 5; j++)
                            {
                                str1 += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }
                            Invoke(new MethodInvoker(delegate() { textBox_DataBack2.Text = str1; }));
                        }

                        #endregion

                        break;
                    case 0xE1:

                        #region CAN口配置1指令,配置CAN口工作模式 返回指令：E1 d0 d1 d2 d4 d5 d6 FF 0D 0A

                        if (data[i + 7] == 0xFF && data[i + 8] == 0x0D && data[i + 9] == 0x0A)
                        {
                            string str2 = string.Empty;
                            //获取指令

                            for (int j = i; j <= i + 6; j++)
                            {
                                str2 += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }
                            Invoke(new MethodInvoker(delegate() { textBox_DataBack2.Text = str2; }));
                        }

                        #endregion

                        break;
                    case 0xE2:

                        #region CAN口配置2指令,配置CAN口发送与接收地址号  返回指令：E2 d0 d1 d2 d3 d4 d5 d6 d7 FF 0D 0A

                        if (data[i + 9] == 0xFF && data[i + 10] == 0x0D && data[i + 11] == 0x0A)
                        {
                            //获取指令
                            string str3 = string.Empty;
                            for (int j = i; j <= i + 11; j++)
                            {
                                str3 += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }
                            Invoke(new MethodInvoker(delegate() { textBox_DataBack2.Text = str3; }));
                        }

                        #endregion

                        break;
                    case 0xE3:

                        #region 配置串行口1工作模式 返回指令：E3 d0 d1 d2 d4 d5 d6 FF 0D 0A  

                        if (data[i + 7] == 0xFF && data[i + 8] == 0x0D && data[i + 9] == 0x0A)
                        {
                            //获取指令
                            string str4 = string.Empty;
                            for (int j = i; j <= i + 9; j++)
                            {
                                str4 += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }
                            Invoke(new MethodInvoker(delegate() { textBox_DataBack2.Text = str4; }));
                        }

                        #endregion

                        break;
                    case 0xE4:

                        #region 配置串行口2工作模式 返回指令：E4 d0 d1 d2 d4 d5 d6 FF 0D 0A

                        if (data[i + 7] == 0xFF && data[i + 8] == 0x0D && data[i + 9] == 0x0A)
                        {
                            //获取指令
                            string str5 = string.Empty;
                            for (int j = i; j <= i + 9; j++)
                            {
                                str5 += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }
                            Invoke(new MethodInvoker(delegate() { textBox_DataBack2.Text = str5; }));
                        }

                        #endregion

                        break;
                    case 0xEE:

                        #region 配置初始化 返回指令：EE 00 00 00 00 00 FF 0D 0A

                        if (data[i + 7] == 0x0D && data[i + 8] == 0x0A)
                        {
                            //获取指令
                            string str6 = string.Empty;
                            for (int j = i; j <= i + 8; j++)
                            {
                                str6 += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }
                            Invoke(new MethodInvoker(delegate() { textBox1.Text = str6; }));
                        }

                        #endregion

                        break;
                    case 0xEC:

                        #region 倾角零位标定 返回指令：EC XX XX FF 0D 0A

                        if (data[i + 3] == 0xFF && data[i + 4] == 0x0D && data[i + 5] == 0x0A)
                        {
                            #region 判断是标定的什么

                            if (data[i + 1] == 0x20 || data[i + 1] == 0x21 || data[i + 1] == 0x22)
                            {
                                //Y标定
                                yCalibrationData = "";
                                yCalibrationState = "";
                            }
                            if (data[i + 1] == 0x10 || data[i + 1] == 0x11 || data[i + 1] == 0x12)
                            {
                                //X标定
                                xCalibrationData = "";
                                xCalibrationState = "";
                            }
                            if (data[i + 1] == 0x40 || data[i + 1] == 0x41 || data[i + 1] == 0x42 || data[i + 1] == 0x48)
                            {
                                //温度标定
                                tCalibrationData = "";
                                tCalibrationState = "";
                            }
                            if (data[i + 1] == 0x30 || data[i + 1] == 0x31 || data[i + 1] == 0x32)
                            {
                                //陀螺标定
                                gCalibrationData = "";
                                gCalibrationState = "";
                            }

                            #endregion

                            #region Y轴

                            switch (data[i + 1])
                            {
                                case 0x20:

                                    #region 判断3个状态

                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            yCalibrationState = "倾角Y轴角度零位 标定完成!";
                                            break;
                                        case 0x1:
                                            yCalibrationState = "倾角Y轴电压零位正位置取数完成! 电压零位标定完成!";
                                            break;
                                        case 0x2:
                                            yCalibrationState = "倾角Y轴电压零位负位置取数完成!";
                                            break;
                                    }

                                    #endregion

                                    break;
                                case 0x21:
                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            yCalibrationState = "Y_0度角二次插补参数标定完成";
                                            break;
                                        case 0x1:
                                            yCalibrationState = "Y_3度角二次插补参数标定完成";
                                            break;
                                        case 0x2:
                                            yCalibrationState = "Y_6度角二次插补参数标定完成!";
                                            break;
                                        case 0x3:
                                            yCalibrationState = "Y_9度角二次插补参数标定完成";
                                            break;
                                        case 0x4:
                                            yCalibrationState = "Y_12度角二次插补参数标定完成";
                                            break;
                                        case 0x5:
                                            yCalibrationState = "Y_15度角二次插补参数标定完成";
                                            break;
                                        case 0x81:
                                            yCalibrationState = "Y_-3度角二次插补参数标定完成!";
                                            break;
                                        case 0x82:
                                            yCalibrationState = "Y_-6度角二次插补参数标定完成!";
                                            break;
                                        case 0x83:
                                            yCalibrationState = "Y_-9度角二次插补参数标定完成!";
                                            break;
                                        case 0x84:
                                            yCalibrationState = "Y_-12度角二次插补参数标定完成!";
                                            break;
                                        case 0x85:
                                            yCalibrationState = "Y_-15度角二次插补参数标定完成!";
                                            break;
                                    }
                                    break;
                                case 0x22:
                                    yCalibrationState = "倾角Y轴角度零位 标定完成!";
                                    break;
                            }
                            for (int j = i; j <= i + 5; j++)
                            {
                                yCalibrationData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }

                            #endregion

                            #region X轴

                            switch (data[i + 1])
                            {
                                case 0x10:

                                    #region 判断3个状态

                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            xCalibrationState = "倾角X轴角度零位 标定完成!";
                                            break;
                                        case 0x1:
                                            xCalibrationState = "倾角X轴电压零位正位置取数完成! 电压零位标定完成!";
                                            break;
                                        case 0x2:
                                            xCalibrationState = "倾角X轴电压零位负位置取数完成!";
                                            break;
                                    }

                                    #endregion

                                    break;
                                case 0x11:
                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            xCalibrationState = "X_0度角二次插补参数标定完成";
                                            break;
                                        case 0x1:
                                            xCalibrationState = "X_3度角二次插补参数标定完成";
                                            break;
                                        case 0x2:
                                            xCalibrationState = "X_6度角二次插补参数标定完成!";
                                            break;
                                        case 0x3:
                                            xCalibrationState = "X_9度角二次插补参数标定完成";
                                            break;
                                        case 0x4:
                                            xCalibrationState = "X_12度角二次插补参数标定完成";
                                            break;
                                        case 0x5:
                                            xCalibrationState = "X_15度角二次插补参数标定完成";
                                            break;
                                        case 0x81:
                                            xCalibrationState = "X_-3度角二次插补参数标定完成!";
                                            break;
                                        case 0x82:
                                            xCalibrationState = "X_-6度角二次插补参数标定完成!";
                                            break;
                                        case 0x83:
                                            xCalibrationState = "X_-9度角二次插补参数标定完成!";
                                            break;
                                        case 0x84:
                                            xCalibrationState = "X_-12度角二次插补参数标定完成!";
                                            break;
                                        case 0x85:
                                            xCalibrationState = "X_-15度角二次插补参数标定完成!";
                                            break;
                                    }
                                    break;
                                case 0x12:
                                    xCalibrationState = "倾角X轴角度零位 标定完成!";
                                    break;
                            }
                            for (int j = i; j <= i + 5; j++)
                            {
                                xCalibrationData += data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' ');
                            }

                            #endregion

                            #region 温度

                            switch (data[i + 1])
                            {
                                case 0x40:
                                    tCalibrationState = "温度零位标定完成!";
                                    break;
                                case 0x41:
                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            tCalibrationState = "当前00度温度二次插补参数标定完成!";
                                            break;
                                        case 0x1:
                                            tCalibrationState = "当前20度温度二次插补参数标定完成!";
                                            break;
                                        case 0x2:
                                            tCalibrationState = "当前40度温度二次插补参数标定完成!";
                                            break;
                                        case 0x3:
                                            tCalibrationState = "当前60度温度二次插补参数标定完成!";
                                            break;
                                        case 0x81:
                                            tCalibrationState = "当前-20度温度二次插补参数标定完成!";
                                            break;
                                        case 0x82:
                                            tCalibrationState = "当前-40度温度二次插补参数标定完成!";
                                            break;
                                    }
                                    break;
                                case 0x42:
                                    tCalibrationState = "温度增益标定完成!";
                                    break;
                                case 0x48:
                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            tCalibrationState = "0度温度X、Y角度传感器温度零位二次插补参数标定完成!";
                                            break;
                                        case 0x1:
                                            tCalibrationState = "20度温度X、Y角度传感器温度零位二次插补参数标定完成!";
                                            break;
                                        case 0x2:
                                            tCalibrationState = "40度温度X、Y角度传感器温度零位二次插补参数标定完成!";
                                            break;
                                        case 0x3:
                                            tCalibrationState = "60度温度X、Y角度传感器温度零位二次插补参数标定完成!";
                                            break;
                                        case 0x81:
                                            tCalibrationState = "-20度温度X、Y角度传感器温度零位二次插补参数标定完成!";
                                            break;
                                        case 0x82:
                                            tCalibrationState = "-40度温度X、Y角度传感器温度零位二次插补参数标定完成!";
                                            break;
                                    }
                                    break;
                            }

                            #endregion

                            #region 陀螺

                            switch (data[i + 1])
                            {
                                case 0x30:
                                    gCalibrationState = "陀螺零位标定完成!";
                                    break;
                                case 0x31:
                                    switch (data[i + 2])
                                    {
                                        case 0x0:
                                            gCalibrationState = "0度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x1:
                                            gCalibrationState = "20度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x2:
                                            gCalibrationState = "40度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x3:
                                            gCalibrationState = "60度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x4:
                                            gCalibrationState = "80度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x5:
                                            gCalibrationState = "100度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x81:
                                            gCalibrationState = "-20度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x82:
                                            gCalibrationState = "-40度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x83:
                                            gCalibrationState = "-60度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x84:
                                            gCalibrationState = "-80度/秒角速率二次插补参数标定完成!";
                                            break;
                                        case 0x85:
                                            gCalibrationState = "-100度/秒角速率二次插补参数标定完成!";
                                            break;
                                    }
                                    break;
                                case 0x32:
                                    gCalibrationState = "角速率增益参数标定完成!";
                                    break;
                            }

                            #endregion
                        }

                        #endregion

                        break;
                }
            }
        }

        //-->函数:延时函数
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        //-->函数:向串口发送数据函数
        public void SendCommData(byte[] data, int dataLength)
        {
            if (sp.IsOpen == true)
                sp.Write(data, 0, dataLength); //向串口发送数据
            else
                MessageBox.Show("请先打开串口！");
        }

        #endregion

        //-->刷新串口
        private void button_RefreshPortsNum_Click(object sender, EventArgs e)
        {
            RefreshPorts();
        }

        //-->打开、关闭串口
        private void button_OpenCommPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp.IsOpen == false)
                {
                    #region 打开

                    sp.PortName = comboBox_CommPort.Text;
                    sp.BaudRate = int.Parse(comboBox_CommBaudeRate.Text);
                    sp.DataBits = int.Parse(comboBox_CommDatabits.Text);
                    sp.StopBits = (StopBits) int.Parse(comboBox_CommStopBits.Text);
                    SetParity(sp, comboBox_CommParity);
                    sp.RtsEnable = true;
                    sp.DtrEnable = true;
                    sp.ReadTimeout = -1;
                    sp.ReceivedBytesThreshold = 35;
                    sp.WriteTimeout = -1;
                    sp.DataReceived += new SerialDataReceivedEventHandler(SerialDataProc);
                    dL = int.Parse(comboBox_DataLength.Text); //获取数据包长度
                    sp.Open(); //打开
                    if (sp.IsOpen == true) //成功打开
                    {
                        //sp.DataReceived += new SerialDataReceivedEventHandler(SerialDataProc);
                        button_OpenCommPort.Text = "关闭串口";
                        Form_DataShow.updataDataXC.Enabled = true; //开启数据显示界面的数据更新定时器
                        Form_ReadWritePara.updataDataRW.Enabled = true;
                        Form_TCalibration.updataDataT.Enabled = true;
                        Form_XCalibration.updataDataXC.Enabled = true;
                        Form_YCalibration.updateDataYC.Enabled = true;
                        Form_YCalibration_D30.updateDataYC_D30.Enabled = true;
                        Form_XCalibration_D30.updataDataXC_D30.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("打开串口失败！");
                    }

                    #endregion
                }
                else
                {
                    #region 关闭

                    checkBox1.Checked = false;
                    sp.Close(); //关闭串口
                    if (sp.IsOpen == false)
                    {
                        button_OpenCommPort.Text = "打开串口";
                        Form_DataShow.updataDataXC.Enabled = false; //关闭数据显示界面的数据更新定时器
                        Form_ReadWritePara.updataDataRW.Enabled = false;
                        Form_TCalibration.updataDataT.Enabled = false;
                        Form_XCalibration.updataDataXC.Enabled = false;
                        Form_YCalibration.updateDataYC.Enabled = false;
                        Form_YCalibration_D30.updateDataYC_D30.Enabled = false;
                        Form_XCalibration_D30.updataDataXC_D30.Enabled = false;
                    }

                    #endregion
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        //-->启动配置
        private void button_StartConfigure_Click(object sender, EventArgs e)
        {
            SendCommData(CD.startDebug, 8);
        }

        //-->停止配置
        private void button_EndConfigure_Click(object sender, EventArgs e)
        {
            SendCommData(CD.endDebug, 8);
        }

        //-->设置系统工作模式
        private void button_SetSys_Click(object sender, EventArgs e)
        {
//E0 d0 d1 FF 0D 0A
            byte[] cm = new byte[6];
            cm[0] = 0xE0;
            cm[1] = Convert.ToByte(comboBox_sysMode.SelectedIndex);
            cm[2] = Convert.ToByte(comboBox_T0.Text);
            cm[3] = 0xFF;
            cm[4] = 0x0D;
            cm[5] = 0x0A;
            SendCommData(cm, 6);
        }

        //-->CAN口配置1
        private void button_SetCan1_Click(object sender, EventArgs e)
        {
//E1 d0 d1 d2 d3 d4 d5 d6 d7 d8 d9 FF 0D 0A
            byte[] cm = new byte[10];
            cm[0] = 0xE1;
            cm[1] = Convert.ToByte(comboBox_Can1Enable.SelectedIndex);
            cm[2] = Convert.ToByte(textBox_T1.Text.Trim());
            cm[3] = Convert.ToByte(comboBox_Can1Filter.Text.Trim());
            cm[4] = Convert.ToByte(comboBox_Can1OutputFormat.Text.Trim());
            cm[5] = Convert.ToByte(comboBox_Can1Baud.SelectedIndex);
            cm[6] = Convert.ToByte(comboBox_Can1Frame.SelectedIndex);
            cm[7] = 0xFF;
            cm[8] = 0x0D;
            cm[9] = 0x0A;
            SendCommData(cm, 10);
        }

        //-->配置初始化指令
        private void button1_Click(object sender, EventArgs e)
        {
//EE 00 00 00 00 00 FF 0D 0A
            SendCommData(CD.Initiate, 9);
        }

        //-->清零参数
        private void button2_Click(object sender, EventArgs e)
        {
//EE x0 00 00 00 00 FF 0D 0A
            byte[] cm = new byte[9];
            cm[0] = 0xEE;
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    cm[1] = 0x10;
                    break;
                case 1:
                    cm[1] = 0x20;
                    break;
                case 2:
                    cm[1] = 0x30;
                    break;
                case 3:
                    cm[1] = 0x40;
                    break;
            }
            cm[2] = 0x00;
            cm[3] = 0x00;
            cm[4] = 0x00;
            cm[5] = 0x00;
            cm[6] = 0xFF;
            cm[7] = 0x0D;
            cm[8] = 0x0A;
            SendCommData(cm, 9);
        }

        //-->CAN口配置2
        private void button_SetCan2_Click(object sender, EventArgs e)
        {
//E2 d0 d1 d2 d3 d4 d5 d6 d7 FF 0D 0A
            byte[] cm = new byte[12];
            cm[0] = 0xE2;
            cm[1] = Convert.ToByte(textBox_CanAdS1.Text.Trim() == "" ? "0" : textBox_CanAdS1.Text.Trim());
            cm[2] = Convert.ToByte(textBox_CanAdS2.Text.Trim() == "" ? "0" : textBox_CanAdS2.Text.Trim());
            cm[3] = Convert.ToByte(textBox_CanAdS3.Text.Trim() == "" ? "0" : textBox_CanAdS3.Text.Trim());
            cm[4] = Convert.ToByte(textBox_CanAdS4.Text.Trim() == "" ? "0" : textBox_CanAdS4.Text.Trim());
            cm[5] = Convert.ToByte(textBox_CanAdR1.Text.Trim() == "" ? "0" : textBox_CanAdS1.Text.Trim());
            cm[6] = Convert.ToByte(textBox_CanAdR2.Text.Trim() == "" ? "0" : textBox_CanAdS2.Text.Trim());
            cm[7] = Convert.ToByte(textBox_CanAdR3.Text.Trim() == "" ? "0" : textBox_CanAdS3.Text.Trim());
            cm[8] = Convert.ToByte(textBox_CanAdR4.Text.Trim() == "" ? "0" : textBox_CanAdS4.Text.Trim());
            cm[9] = 0xFF;
            cm[10] = 0x0D;
            cm[11] = 0x0A;
            SendCommData(cm, 12);
        }

        //-->自动发送应答命令
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
//T_SendCommand
            if (checkBox1.Checked == true)
            {
                string[] bytesToSend = comboBox1.Text.Trim().Split(' ');
                byte[] CMByte = new byte[bytesToSend.Length];
                for (int i = 0; i < bytesToSend.Length; i++)
                {
                    CMByte[i] = Convert.ToByte((Convert.ToInt32(bytesToSend[i], 16)).ToString());
                }
                numericUpDown1.Enabled = false;
                T_SendCommand = new System.Timers.Timer(Convert.ToDouble(numericUpDown1.Value));
                //T_SendCommand.Interval = Convert.ToDouble( numericUpDown1.Value);
                T_SendCommand.Elapsed +=
                    new System.Timers.ElapsedEventHandler((s, a) => T_SendCommand_Elapsed(s, a, CMByte));
                T_SendCommand.Enabled = true;
            }
            else
            {
                T_SendCommand.Enabled = false;
                numericUpDown1.Enabled = true;
            }
        }

        private void T_SendCommand_Elapsed(object sender, System.Timers.ElapsedEventArgs e, byte[] data)
        {
            SendCommData(data, data.Length);
        }

        private void btnConfigureWord_Click(object sender, EventArgs e)
        {
            //要加载新的数据
            if (frmWord.Equals(null) || frmWord.IsDisposed)
            {
                frmWord = new Form_WordConfig();
                frmWord.ShowDialog(this);
            }
            else
            {
                frmWord.ShowDialog(this);
            }
        }

        private void rdbTestAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTestAuto.Checked)
            {
                grbHand.Visible = false;
                grbAuto.Visible = true;
            }
        }

        private void rdbTestByHand_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTestByHand.Checked)
            {
                grbAuto.Visible = false;
                grbHand.Visible = true;
            }
        }

        /// <summary>
        /// 自动测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestAuto_Click(object sender, EventArgs e)
        {
            Properties.Settings mysSettings = new Settings();
            _dicTestAuto.Clear();

            #region step1 将转台双轴归零

            Class_Comm.SetXPosition(0, 20, 20);
            Class_Comm.SetYPosition(0, 20, 20);

            #endregion

            #region step2 测试X轴精度

            for (int i = 0; i < testPoints[mysSettings.wordPRange]; i++) //0:±15,1:±30,2:±60
            {
                Class_Comm.SetXPosition(testPosition[mysSettings.wordPRange][i], 20, 20); //转动到位
                Class_Comm.WaitXAxis(); //等待到位
                Delay(1000); //延时1秒，等待产品数据稳定

                #region 测量精度

                listXAccuracy.Clear(); //清空精度数据
                listXData.Clear(); //清空采集数据
                collectXFlag = true; //开始采集
                Delay(2000); //数据采集1秒
                collectXFlag = false; //停止采集 
                //listXData.Average();//计算平均值
                listXAccuracy.Add(listXData.Average() - testPosition[mysSettings.wordPRange][i]); //精度值

                #endregion
            }

            #endregion

            #region step3 将转台归零位并转到Y轴测试位置

            Class_Comm.SetXPosition(0, 20, 20);
            Class_Comm.WaitXAxis();
            Class_Comm.SetYPosition(90, 20, 20);
            Class_Comm.WaitYAxis();

            #endregion

            #region step4 测试Y轴精度

            for (int i = 0; i < testPoints[mysSettings.wordPRange]; i++) //0:±15,±30,±60
            {
                Class_Comm.SetYPosition(testPosition[mysSettings.wordPRange][i], 20, 20); //转动到位
                Class_Comm.WaitYAxis(); //等待到位
                Delay(1000); //延时1秒，等待产品数据稳定

                #region 测量精度

                listYAccuracy.Clear(); //清空精度数据
                listYData.Clear(); //清空采集数据
                collectYFlag = true; //开始采集
                Delay(2000); //数据采集1秒
                collectYFlag = false; //停止采集 
                //listXData.Average();//计算平均值
                listYAccuracy.Add(listYData.Average() - testPosition[mysSettings.wordPRange][i]); //精度值

                #endregion
            }

            #endregion

            #region step5 测试完成并生成word文件

            string savePath = mysSettings.wordFilePath + "\\" + mysSettings.wordFileName + ".doc"; //要保存的文件位置
            WordDocument wd = new WordDocument(savePath, wordModelPath[mysSettings.wordPRange]); //读取模板文件
            //foreach (KeyValuePair<string, string> kV in dic)
            //{
            //    wd.InsertValue(kV.Key, kV.Value);
            //}
            #endregion
        }


        //-->设置串口A
        private void button_SetComA_Click(object sender, EventArgs e)
        {
//E3 d0 d1 d2 d3 d4 d5 FF 0D 0A
            byte[] cm = new byte[10];
            cm[0] = 0xE3;
            cm[1] = Convert.ToByte(comboBox_ComAEnable.SelectedIndex);
            cm[2] = Convert.ToByte(textBox_T2.Text.Trim());
            cm[3] = Convert.ToByte(comboBox_ComFilterA.Text.Trim());
            cm[4] = Convert.ToByte(comboBox_InterfaceA.Text.Trim());
            cm[5] = Convert.ToByte(comboBox_BaudA.SelectedIndex);
            cm[6] = Convert.ToByte(comboBox_ComFormatA.SelectedIndex);
            cm[7] = 0xFF;
            cm[8] = 0x0D;
            cm[9] = 0x0A;
            SendCommData(cm, 10);
        }

        //-->设置串口B
        private void button_SetComB_Click(object sender, EventArgs e)
        {
//E4 d0 d1 d2 d3 d4 d5 FF 0D 0A
            byte[] cm = new byte[10];
            cm[0] = 0xE4;
            cm[1] = Convert.ToByte(comboBox_ComBEnable.SelectedIndex);
            cm[2] = Convert.ToByte(textBox_T3.Text.Trim());
            cm[3] = Convert.ToByte(comboBox_ComFilterB.Text.Trim());
            cm[4] = Convert.ToByte(comboBox_InterfaceB.Text.Trim());
            cm[5] = Convert.ToByte(comboBox_BaudB.SelectedIndex);
            cm[6] = Convert.ToByte(comboBox_ComFormatB.SelectedIndex);
            cm[7] = 0xFF;
            cm[8] = 0x0D;
            cm[9] = 0x0A;
            SendCommData(cm, 10);
        }

        private void SaveData(int Part, byte[] Data, int x)
        {
            string directoryPath = Application.StartupPath + "\\para" +
                                   System.DateTime.Today.Date.ToString("yyyy_MM_dd"); //文件夹路径
            string filePath = directoryPath + "\\para" + Part.ToString() + ".txt"; //文件路径
            //查找文件夹
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath); //不存在此文件夹则创建一个新的
            //查找文件
            if (!File.Exists(filePath))
                File.Create(filePath); //不存在此文件则创建一个
            //获取字符串
            string[] bytesData = new string[21];
            for (int j = 0; j < 21; j++)
            {
                bytesData[j] = Data[x].ToString();
                x++;
            }
            Delay(30);
            //写入文件
            File.WriteAllLines(filePath, bytesData);
            Delay(20);
            File.WriteAllLines(filePath, bytesData);
        }
    }
}