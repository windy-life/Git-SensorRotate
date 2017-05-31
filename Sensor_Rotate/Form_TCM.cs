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
using MSExcel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace Sensor_Rotate
{
    public partial class Form_TCM : DockContent
    {
        public Form_TCM()
        {
            InitializeComponent();
            timerProcessBar.Elapsed += TimerProcessBar_Elapsed; //更新进度条
        }

        //-->更新进度条
        private void TimerProcessBar_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate() { progressBar1.Value = Num; }));
        }

        #region 定义变量

        System.Timers.Timer timerSend; //发送命令定时器
        System.Timers.Timer timerProcessBar = new System.Timers.Timer(100); //发送命令定时器
        SerialPort sp = new SerialPort(); //建立串口实例
        string _filePath = ""; //文件夹路径
        public static string _path = ""; //文件路径
        public static string _exclePath = ""; //文件路径
        public static bool _collectFlag; //采集标志位
        public static int _countNum; //转动次数
        bool _stopFlag; //停止标志位
        StringBuilder sensorData = new StringBuilder(); //拼接字符串
        public static float azimuth, pitch, roll = 0; //罗盘的方位、俯仰和横滚
        int Num; //记录当前采集到第几次
        byte[] saveBuffer = new byte[30]; //存储上次未处理完的数据
        int bufferNum; //存储上次未处理完的数据的个数
        ArrayList azimuthSave = new ArrayList(); //存储连续相对定位时，每次停止时的方位角
        bool relSaveFlag = false; //相对定位时，间隔停止采集标志位

        #endregion

        //-->转台转动相对位置
        private void button_RPosition_Click(object sender, EventArgs e)
        {
            _stopFlag = false; //置位标志位
            if (Form_Rotate303.Control_State == false)
            {
                MessageBox.Show("请先连接转台！");
                return;
            }
            if (RPosition.Text.Trim() == "")
            {
                MessageBox.Show("请输入正确的角度!");
                return;
            }
            //try
            //{
            double _rPosition = Convert.ToDouble(RPosition.Text.Trim()); //要转动的相对位置
            if (radSingleGo.Checked == true) //单次定位
            {
                try
                {
                    button_RPosition.Enabled = false;
                    Class_Comm.RelXRun(_rPosition, 20, 10);
                    button_RPosition.Enabled = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }
            }
            else //连续定位
            {
                if (Convert.ToDouble(RPosition.Text.Trim()) == 0)
                {
                    MessageBox.Show("请输入正确的角度!");
                    return;
                }
                if (360%_rPosition != 0)
                {
                    MessageBox.Show("请输入一个能被360整除的角度！");
                    return;
                }
                if (txtTime.Text.Trim() == "")
                {
                    MessageBox.Show("请输入时间!");
                    return;
                }
                if (_path == string.Empty)
                {
                    MessageBox.Show("请选择文件存储路径!");
                    return;
                }

                #region 禁止按键

                radSingleGo.Enabled = false;
                radContinuousGo.Enabled = false;
                RPosition.Enabled = false;
                txtTime.Enabled = false;
                txtFileName.Enabled = false;

                #endregion

                _countNum = Convert.ToInt32(Math.Truncate(360/_rPosition)); //计算中间停止次数
                double[] maxAzimuth = new double[_countNum + 1]; //存储每次停止数据中出现频率最大的值
                double[] azimuthPrecision = new double[_countNum + 1]; //存储方位角的精确值

                #region 设置进度条

                progressBar1.Visible = true;
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = _countNum + 1;
                progressBar1.Step = 1;
                Num = 0;
                timerProcessBar.Enabled = true; //开启定时器

                #endregion

                _collectFlag = true; //采集
                button_RPosition.Enabled = false;
                relSaveFlag = true; //开始存储静态方位角
                Class_Comm.Delay(Convert.ToInt32(Convert.ToDouble(txtTime.Text.Trim())*1000)); //暂停指定的时间
                //Thread.Sleep(Convert.ToInt32(txtTime.Text.Trim()));
                relSaveFlag = false;
                ; //停止存储静态方位角
                _collectFlag = false; //停止采集

                #region 方位角处理

                #region backup

                //Dictionary<double, Int32> azimuthMax = new Dictionary<double, int>();//存放已经遍历过的方位角和此方位角出现的次数
                //for (int i = 0; i < azimuthSave.Count; i++)//遍历所有方位角数据
                //{
                //    if (azimuthMax.Count == 0)
                //    {
                //        azimuthMax.Add((double)azimuthSave[i], 1);//增加这一数据并计次数为1
                //        return;
                //    }
                //    foreach (KeyValuePair<double, Int32> kvp in azimuthMax)
                //    {
                //        if (kvp.Key == (double)azimuthSave[i])//键值对中已经有此数据
                //        {
                //            azimuthMax[(double)azimuthSave[i]]++;//数据出现次数+1
                //        }
                //        else//键值对中没有此数据
                //        {
                //            azimuthMax.Add((double)azimuthSave[i],1);//增加这一数据并计次数为1
                //        }
                //    }
                //}
                //KeyValuePair<double, Int32> max = azimuthMax.Max(); 

                #endregion

                maxAzimuth[Num] = GetMaxAzimuth(azimuthSave); //赋值当前转动次数的最大值
                if (maxAzimuth[Num] == -1)
                {
                    MessageBox.Show("产品没有数据，请检查！");
                    btnStop_Click(null, null);
                    return;
                }
                azimuthSave.Clear(); //清空保存的数据

                #endregion

                #region 连续转动的处理

                for (int i = 0; i < _countNum; i++)
                {
                    Num = i + 1; //当前转动次数增加
                    if (_stopFlag)
                    {
                        return;
                    }
                    Class_Comm.RelXRun(_rPosition, 20, 10); //转动
                    //int start = Environment.TickCount;
                    _collectFlag = true; //采集
                    relSaveFlag = true; //开始存储静态方位角
                    //MessageBox.Show("开始采集");
                    Class_Comm.Delay(Convert.ToInt32(Convert.ToDouble(txtTime.Text.Trim())*1000)); //暂停指定的时间
                    //Thread.Sleep(Convert.ToInt32(txtTime.Text.Trim()));
                    relSaveFlag = false;
                    ; //停止存储静态方位角
                    _collectFlag = false; //停止采集
                    //int end = Math.Abs(Environment.TickCount - start);
                    //MessageBox.Show(end.ToString());
                    maxAzimuth[Num] = GetMaxAzimuth(azimuthSave); //赋值当前转动次数的最大值
                    azimuthSave.Clear(); //清空保存的数据
                }

                #endregion

                #region 解算精度

                azimuthPrecision[0] = 0; //第一次精度为0（精度计算方法：当前角度 - 上一角度 - 相对转动角度）
                for (int i = 1; i < _countNum + 1; i++)
                {
                    azimuthPrecision[i] = maxAzimuth[i] - maxAzimuth[i - 1] - Math.Abs(Convert.ToDouble(RPosition.Text));
                    if (Math.Abs(maxAzimuth[i] - maxAzimuth[i - 1]) > 180) //判断过渡0°和360°方法
                    {
                        if (maxAzimuth[i] < maxAzimuth[i - 1]) //后一个值小于前一个值  例如 5° 355°
                        {
                            azimuthPrecision[i] = maxAzimuth[i] + 360 - maxAzimuth[i - 1] -
                                                  Math.Abs(Convert.ToDouble(RPosition.Text));
                        }
                        else //后一个值大于前一个值  例如 355° 5°
                        {
                            azimuthPrecision[i] = 360 - maxAzimuth[i] + maxAzimuth[i - 1] -
                                                  Math.Abs(Convert.ToDouble(RPosition.Text));
                        }
                    }
                    azimuthPrecision[i] = Math.Round(azimuthPrecision[i], 2);
                }

                #endregion

                #region 合成要写入EXCEL的信息

                string[] infoHead = new string[_countNum + 2]; //时间，0，1，...
                string[] infoAzimuth = new string[_countNum + 2]; //方位角，a1,a2,...
                string[] infoPrecision = new string[_countNum + 2]; //精度值，p1,p2....
                infoHead[0] = DateTime.Now.ToString("MM-dd HH:mm:ss");
                infoAzimuth[0] = "方位角";
                infoPrecision[0] = "精度值";
                for (int i = 1; i < _countNum + 2; i++)
                {
                    infoHead[i] = (i - 1).ToString();
                    infoAzimuth[i] = maxAzimuth[i - 1].ToString("0.0");
                    infoPrecision[i] = azimuthPrecision[i - 1].ToString("0.000");
                }

                #endregion

                #region 将精度数据写入excel

                #region 微软方法写数据（不兼容不同的office 版本）

                ////查询是否有excel文件，如果没有则建立一个
                //_exclePath = _filePath + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".xlsx";
                //if (!File.Exists(_exclePath))//不存在
                //{
                //    Class_Comm.CreateExcel(_exclePath); //创建
                //}
                //Class_Comm.OpenExcel(_exclePath, infoHead, infoAzimuth, infoPrecision);//打开并修改文件 

                #endregion

                #region NPOI方法

                _exclePath = _filePath + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".xls";
                if (!File.Exists(_exclePath)) //文件不存在则创建一个
                {
                    Class_Comm.NPOICreateExcel(_exclePath);
                }
                Class_Comm.NPOIReadExcel(_exclePath, infoHead, infoAzimuth, infoPrecision); //打开并修改文件

                #endregion

                #endregion

                #region 生成图表

                Class_Comm.MakeChart(chart1, azimuthPrecision, txtFileName.Text.Trim());

                #endregion

                #region 使能按键

                radSingleGo.Enabled = true;
                radContinuousGo.Enabled = true;
                RPosition.Enabled = true;
                txtTime.Enabled = true;
                txtFileName.Enabled = true;
                button_RPosition.Enabled = true;

                #endregion

                timerProcessBar.Enabled = false; //关闭定时器
                progressBar1.Visible = false;
            }
            //}
            //catch (Exception a)
            //{
            //    MessageBox.Show(a.Message);
            //    #region 使能按键
            //    radSingleGo.Enabled = true;
            //    radContinuousGo.Enabled = true;
            //    RPosition.Enabled = true;
            //    txtTime.Enabled = true;
            //    txtFileName.Enabled = true;
            //    #endregion
            //}
        }

        //-->获取当前一组数据中出现频率最高的数据
        private double GetMaxAzimuth(ArrayList DataAzimuth)
        {
            Dictionary<double, Int32> azimuthMax = new Dictionary<double, int>(); //存放已经遍历过的方位角和此方位角出现的次数
            for (int i = 0; i < DataAzimuth.Count; i++) //遍历所有方位角数据
            {
                if (azimuthMax.Count == 0)
                {
                    azimuthMax.Add(Convert.ToDouble(DataAzimuth[i]), 1); //增加这一数据并计次数为1
                    break;
                }
                foreach (KeyValuePair<double, Int32> kvp in azimuthMax)
                {
                    if (kvp.Key == Convert.ToDouble(DataAzimuth[i])) //键值对中已经有此数据
                    {
                        azimuthMax[Convert.ToDouble(DataAzimuth[i])]++; //数据出现次数+1
                    }
                    else //键值对中没有此数据
                    {
                        azimuthMax.Add(Convert.ToDouble(DataAzimuth[i]), 1); //增加这一数据并计次数为1
                    }
                }
            }
            if (azimuthMax.Count < 1) //没有数据
            {
                return -1;
            }
            KeyValuePair<double, Int32> max = azimuthMax.Max();
            return max.Key; //赋值当前转动次数的最大值
        }

        //-->选择存储路径
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (_filePath != string.Empty) //不是第一次选择路径，赋值之前的路径
            {
                folderBrowserDialog1.SelectedPath = _filePath;
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) //选择了一个路径
            {
                _filePath = folderBrowserDialog1.SelectedPath; //获取路径
                txtFilePath.Text = _filePath; //显示路径
                _path = _filePath + "\\" + txtFileName.Text.Trim() + ".txt"; //全路径
            }
        }

        //-->文件名更改跟随变化
        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            _path = _filePath + "\\" + txtFileName.Text.Trim() + ".txt"; //全路径
        }

        //-->手动采集
        private void btnCollect_Click(object sender, EventArgs e)
        {
            if (btnCollect.Text == "手动采集")
            {
                if (MessageBox.Show("确定手动采集？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                if (_path == string.Empty)
                {
                    MessageBox.Show("请选择文件存储路径!");
                    return;
                }
                txtFileName.Enabled = false;
                _countNum = 0;
                _collectFlag = true;
                btnCollect.Text = "停止采集";
                txtFileName.Enabled = false; //禁止使能
            }
            else
            {
                txtFileName.Enabled = true;
                _collectFlag = false;
                btnCollect.Text = "手动采集";
                txtFileName.Enabled = true; //使能
            }
        }

        //-->停止转动
        private void btnStop_Click(object sender, EventArgs e)
        {
            _stopFlag = true; //置为停止
            try
            {
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XStop(), 0, 16); //停止
            }
            catch (Exception)
            {
            }

            Form_Rotate303.X_SetRunWay = 0;

            #region 使能按键

            radSingleGo.Enabled = true;
            radContinuousGo.Enabled = true;
            RPosition.Enabled = true;
            txtTime.Enabled = true;
            txtFileName.Enabled = true;
            button_RPosition.Enabled = true;

            #endregion
        }

        //-->刷新串口
        private void button_RefreshPortsNum_Click(object sender, EventArgs e)
        {
            comboBox_CommPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string p in ports)
            {
                SerialPort tempSp = new SerialPort(p);
                try
                {
                    tempSp.Open();
                    comboBox_CommPort.Items.Add(p);
                    tempSp.Close();
                }
                catch
                {
                }
            }
            if (comboBox_CommPort.Items.Count > 0)
                comboBox_CommPort.SelectedIndex = 0; //显示第一个串口号
        }

        //-->打开串口
        private void button_OpenCommPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp.IsOpen == false)
                {
                    #region 打开

                    sp.PortName = comboBox_CommPort.Text;
                    sp.BaudRate = 38400;
                    sp.DataBits = 8;
                    sp.StopBits = (StopBits) (1);
                    sp.Parity = Parity.None;
                    sp.RtsEnable = true;
                    sp.DtrEnable = true;
                    sp.ReadTimeout = -1;
                    sp.ReceivedBytesThreshold = 35;
                    sp.WriteTimeout = -1;
                    sp.DataReceived += new SerialDataReceivedEventHandler(SerialDataProc);
                    sp.Open(); //打开
                    if (sp.IsOpen == true) //成功打开
                    {
                        button_OpenCommPort.Text = "关闭串口";
                        Form_DataShow.updataDataXC.Enabled = true; //开启数据显示界面的数据更新定时器
                        comboBox_CommPort.Enabled = false;
                        button_RefreshPortsNum.Enabled = false;
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
                        comboBox_CommPort.Enabled = true;
                        button_RefreshPortsNum.Enabled = true;
                    }

                    #endregion
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        //-->自动发送数据
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (!sp.IsOpen)
                {
                    MessageBox.Show("请先打开产品串口！");
                    checkBox1.Checked = false;
                    return;
                }
                string[] bytesToSend = comboBox1.Text.Trim().Split(' ');
                byte[] CMByte = new byte[bytesToSend.Length];
                for (int i = 0; i < bytesToSend.Length; i++)
                {
                    CMByte[i] = Convert.ToByte((Convert.ToInt32(bytesToSend[i], 16)).ToString());
                }
                numericUpDown1.Enabled = false;
                timerSend = new System.Timers.Timer(Convert.ToDouble(numericUpDown1.Value));
                //T_SendCommand.Interval = Convert.ToDouble( numericUpDown1.Value);
                timerSend.Elapsed += new System.Timers.ElapsedEventHandler((s, a) => timerSend_Elapsed(s, a, CMByte));
                timerSend.Enabled = true;
            }
            else
            {
                timerSend.Enabled = false;
                numericUpDown1.Enabled = true;
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
                byte[] newBuffer = new byte[bytes + bufferNum];
                for (int i = 0; i < bufferNum; i++)
                {
                    newBuffer[i] = saveBuffer[i];
                }
                for (int i = bufferNum; i < bytes + bufferNum; i++)
                {
                    newBuffer[i] = buffer[i - bufferNum];
                }
                DataProc(newBuffer);
            }
            catch (Exception a)
            {
                //MessageBox.Show(a.Message);
            }
        }

        //-->函数:数据处理
        private void DataProc(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (i + 1 == data.Length)
                {
                    for (int k = i; k < data.Length; k++)
                    {
                        saveBuffer[i - k] = data[k];
                    }
                    bufferNum = 1;
                    break; //跳出
                }

                #region 进行校验

                if (data[i] == 0x00 && data[i + 1] == 0x15)
                {
                    if (i + 20 == data.Length)
                    {
                        for (int k = i; k < data.Length; k++)
                        {
                            saveBuffer[i - k] = data[k];
                        }
                        bufferNum = data.Length - i;
                        break; //跳出
                    }
                    sensorData.Clear();
                    for (int j = i; j < i + 21; j++)
                    {
                        sensorData.Append(data[j].ToString("X").PadLeft(2, '0').PadRight(3, ' '));
                    }
                    Form_Communicate.rawData = sensorData.ToString(); //更新数据

                    if (data[i + 4] == 0x05) //方位
                    {
                        byte[] az = new byte[] {data[i + 8], data[i + 7], data[i + 6], data[i + 5]};
                        azimuth = BitConverter.ToSingle(az, 0);

                        #region 存储计算精度用的数据

                        if (relSaveFlag == true)
                        {
                            azimuthSave.Add(azimuth); //存储
                        }

                        #endregion
                    }
                    if (data[i + 9] == 0x18) //俯仰
                    {
                        byte[] pi = new byte[] {data[i + 13], data[i + 12], data[i + 11], data[i + 10]};
                        pitch = BitConverter.ToSingle(pi, 0);
                    }
                    if (data[i + 14] == 0x19) //横滚
                    {
                        byte[] ro = new byte[] {data[i + 18], data[i + 17], data[i + 16], data[i + 15]};
                        roll = BitConverter.ToSingle(ro, 0);
                    }

                    #region 记录数据

                    if (Form_TCM._collectFlag == true)
                    {
                        if (!File.Exists(Form_TCM._path)) //文件不存在则创建一个
                        {
                            FileStream file_WR = new FileStream(Form_TCM._path, FileMode.Create, FileAccess.ReadWrite);
                            //创建文件 
                            file_WR.Close();
                        }
                        using (FileStream fileW = new FileStream(Form_TCM._path, FileMode.Append, FileAccess.Write))
                            //写入数据
                        {
                            StreamWriter fileWrite = new StreamWriter(fileW);
                            fileWrite.WriteLine(string.Format(@"{0}   {1}    {2}     {3}    {4}", DateTime.Now, Num,
                                azimuth.ToString("0.0"), roll.ToString("0.000"), pitch.ToString("0.000")));
                            fileWrite.Close();
                        }
                    }

                    #endregion
                }

                #endregion
            }
        }

        private void Form_TCM_Load(object sender, EventArgs e)
        {
            button_RefreshPortsNum_Click(null, null);
            progressBar1.Visible = false;
        }

        private void txtTime_TextChanged(object sender, EventArgs e)
        {
        }

        //-->函数:定时器发送
        private void timerSend_Elapsed(object sender, System.Timers.ElapsedEventArgs e, byte[] data)
        {
            SendCommData(data, data.Length);
        }

        //-->函数:向串口发送数据函数
        public void SendCommData(byte[] data, int dataLength)
        {
            if (sp.IsOpen == true)
                sp.Write(data, 0, dataLength); //向串口发送数据
            else
                MessageBox.Show("请先打开串口！");
        }
    }
}