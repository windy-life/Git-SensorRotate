using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sensor_Rotate
{
    public partial class Form_Main : Form
    {
        Form_Rotate303 frm2 = new Form_Rotate303();
        //Form_Rotate303Control frm2 = new Form_Rotate303Control();
        Form_ProductTree frm3 = new Form_ProductTree();
        //Form_Rotate303Settings frm4 = new Form_Rotate303Settings();
        private Form_DataShow frm5;//= new Form_DataShow();
        //定义变量
        Properties.Settings mySettings = new Properties.Settings();

        public Form_Main()
        {
            InitializeComponent();

            frm5= new Form_DataShow();
            //赋值四个方向停靠比例
            dockPanel1.DockBottomPortion = mySettings.dockPanelBottomPortion;
            dockPanel1.DockTopPortion = mySettings.dockPanelTopPortion;
            dockPanel1.DockLeftPortion = mySettings.dockPanelLeftPortion;
            dockPanel1.DockRightPortion = mySettings.dockPanelRightPortion;

            //-----------
            //frm5.Show(dockPanel1);
            //frm5.DockTo(dockPanel1, DockStyle.Top);

            frm5.AutoHidePortion = mySettings.frm5AutoDockPortion;//赋值自动隐藏比例
            if (mySettings.frm5DockState != DockState.Hidden)
            {
                frm5.Show(dockPanel1, mySettings.frm5DockState == DockState.Unknown ? DockState.DockTop : mySettings.frm5DockState); //显示窗体  
            }

            //-----------

            frm2.AutoHidePortion = mySettings.frm2AutoDockPortion;//赋值自动隐藏比例
            if (mySettings.frm2DockState != DockState.Hidden)
            {
                frm2.Show(dockPanel1, mySettings.frm2DockState == DockState.Unknown ? DockState.DockRightAutoHide : mySettings.frm2DockState); //显示窗体   
            }
            //-----------

            frm3.AutoHidePortion = mySettings.frm3AutoDockPortion;//赋值自动隐藏比例
            if (mySettings.frm3DockState != DockState.Hidden)
            {
                frm3.Show(dockPanel1, mySettings.frm3DockState == DockState.Unknown ? DockState.DockLeftAutoHide : mySettings.frm3DockState); //显示窗体   
            }

            //-----------

            //frm5.AutoHidePortion = mySettings.frm5AutoDockPortion;//赋值自动隐藏比例

            //if (mySettings.frm5DockState != DockState.Hidden)
            //{
            //    frm5.Show(dockPanel1, mySettings.frm5DockState == DockState.Unknown ? DockState.DockBottom : mySettings.frm5DockState); //显示窗体  
            //    frm5.Show(dockPanel1, DockState.Float);
            //}
            //#region 显示实验
            //frm2.Show(dockPanel1,DockState.DockRight);
            ////frm5.;
            //frm5.Show(dockPanel1,DockState.DockTop);
            //frm3.Show(dockPanel1);
            //frm3.DockTo(dockPanel1, DockStyle.Left);

            ////frm5.DockTo(dockPanel1, DockStyle.Top);
            ////frm2.Show(dockPanel1);
            ////frm2.DockTo(dockPanel1, DockStyle.Right);
            //#endregion
        }
        //定义变量
        //public static  SerialPort SerialProduct = new SerialPort();//建立一个串口实例
        
        //-----------------------------------------------
        //显示产品树
        //-----------------------------------------------
        private void toolStripButton_ShowProductTree_Click(object sender, EventArgs e)
        {
            mySettings.frm5DockState = frm5.DockState;//获取frm5的停靠状态
            mySettings.frm2DockState = frm2.DockState;
            mySettings.frm3DockState = frm3.DockState;
            if (frm3 == null || frm3.IsDisposed)
            {
                frm3 = new Form_ProductTree();
                frm3.Show(dockPanel1, DockState.DockLeftAutoHide);
            }
            else
            {
                frm3.Activate();
            }

        }

        
        //窗体关闭时保存布局信息
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Properties.Settings mySettings = new Properties.Settings();
            //---------------
            GetPortion(frm2);
            mySettings.frm2AutoDockPortion = frm2.AutoHidePortion;//获取frm2的自动隐藏比例
            mySettings.frm2DockState = frm2.DockState;//获取frm2的停靠状态
            //---------------
            GetPortion(frm3);
            mySettings.frm3AutoDockPortion = frm3.AutoHidePortion;//获取frm3的自动隐藏比例
            mySettings.frm3DockState = frm3.DockState;//获取frm3的停靠状态
            //---------------
            //GetPortion(frm4);
            //mySettings.frm4AutoDockPortion = frm4.AutoHidePortion;//获取frm4的自动隐藏比例
            //mySettings.frm4DockState = frm4.DockState;//获取frm4的停靠状态
            //---------------
            GetPortion(frm5);
            mySettings.frm5AutoDockPortion = frm5.AutoHidePortion;//获取frm5的自动隐藏比例
            mySettings.frm5DockState = frm5.DockState;//获取frm5的停靠状态
            mySettings.Save();//保存

            try
            {
                if (frm2.sendTh.IsAlive)
                {
                    frm2.sendTh.Abort();//结束线程
                }
            }
            catch (Exception a)
            {
                //MessageBox.Show(a.Message);
            }
            
            
        }
        //获取dockPanel1的上下左右四个方向的停靠比例
        private void GetPortion(DockContent frm)
        {
            switch (frm.DockState)
            {
                case DockState.DockBottom:
                    mySettings.dockPanelBottomPortion = frm.DockPanel.DockBottomPortion;
                    break;
                case DockState.DockBottomAutoHide:
                    mySettings.dockPanelBottomPortion = frm.DockPanel.DockBottomPortion;
                    break;
                case DockState.DockTop:
                    mySettings.dockPanelTopPortion = frm.DockPanel.DockTopPortion;
                    break;
                case DockState.DockTopAutoHide:
                    mySettings.dockPanelTopPortion = frm.DockPanel.DockTopPortion;
                    break;
                case DockState.DockLeft:
                    mySettings.dockPanelLeftPortion = frm.DockPanel.DockLeftPortion;
                    break;
                case DockState.DockLeftAutoHide:
                    mySettings.dockPanelLeftPortion = frm.DockPanel.DockLeftPortion;
                    break;
                case DockState.DockRight:
                    mySettings.dockPanelRightPortion = frm.DockPanel.DockRightPortion;
                    break;
                case DockState.DockRightAutoHide:
                    mySettings.dockPanelRightPortion = frm.DockPanel.DockRightPortion;
                    break;
            }
        }


        //-----------------------------------------------
        //显示双轴转台
        //-----------------------------------------------
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (frm2== null || frm2.IsDisposed)
            {
                frm2 = new Form_Rotate303();
                frm2.Show(dockPanel1, DockState.DockLeftAutoHide);
            }
            else
            {
                frm2.Activate();
            }
        }
        //-----------------------------------------------
        //显示数据显示界面
        //-----------------------------------------------
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (frm5 == null || frm5.IsDisposed)
            {
                frm5 = new Form_DataShow();
                frm5.Show(dockPanel1, DockState.DockTop);
            }
            else
            {
                frm5.Activate();
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

        private void Form_Main_Load(object sender, EventArgs e)
        {
            
            //frm5.AutoHidePortion = mySettings.frm5AutoDockPortion;//赋值自动隐藏比例
            //if (mySettings.frm5DockState != DockState.Hidden)
            //{
            //    frm5.Show(dockPanel1, mySettings.frm5DockState == DockState.Unknown ?DockState.DockTop : mySettings.frm5DockState); //显示窗体    
            //}
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
