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
    public partial class Form_DataShow : DockContent
    {
        //定义变量
        public static System.Timers.Timer updataDataXC = new System.Timers.Timer(10);//更新数据显示
        //public static System.Timers.Timer updataDataRotate = new System.Timers.Timer(10);//更新转台数据显示
        public Form_DataShow()
        {
            InitializeComponent();
            updataDataXC.Elapsed += new System.Timers.ElapsedEventHandler(UpdateShow);
            //updataDataRotate.Elapsed += new System.Timers.ElapsedEventHandler(UpdateRotate);
        }
        //更新数据显示
        private void UpdateShow(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    //更新原始数
                    textBox_RawData.Text = Form_Communicate.rawData;
                    //更新X轴
                    if (Form_Communicate.XAngle < 0)
                        XAngle.Text = Form_Communicate.XAngle.ToString("0.000");
                    else
                        XAngle.Text = Form_Communicate.XAngle.ToString(" 0.000");
                    //更新Y轴
                    if (Form_Communicate.YAngle < 0)
                        YAngle.Text = Form_Communicate.YAngle.ToString("0.000");
                    else
                        YAngle.Text = Form_Communicate.YAngle.ToString(" 0.000");
                    //更新温度
                    if (Form_Communicate.YAngle < 0)
                        Tempreature.Text = Form_Communicate.Tempreature.ToString("0.00");
                    else
                        Tempreature.Text = Form_Communicate.Tempreature.ToString(" 0.00");
                    //更新方位
                    if (Form_TCM.azimuth < 0)
                        Azimuth.Text = Form_TCM.azimuth.ToString("0.00");
                    else
                        Azimuth.Text = Form_TCM.azimuth.ToString(" 0.00");
                    //更新横滚
                    if (Form_TCM.roll < 0)
                        Roll.Text = Form_TCM.roll.ToString("0.00");
                    else
                        Roll.Text = Form_TCM.roll.ToString(" 0.00");
                    //更新俯仰
                    if (Form_TCM.pitch < 0)
                        Pitch.Text = Form_TCM.pitch.ToString("0.00");
                    else
                        Pitch.Text = Form_TCM.pitch.ToString(" 0.00");
                }));
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
