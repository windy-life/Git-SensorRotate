using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sensor_Rotate;
using MSExcel = Microsoft.Office.Interop.Excel;
using Sensor_Rotate.Properties;
using System.IO;

namespace excel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 变量

        string path = @"C:\Users\afei\Desktop\1.xls"; //文件路径
        DateTime beforeTime; //Excel启动之前时间
        DateTime afterTime; //Excel启动之后时间 
        private string[] a1 = new string[] {"时间", "1", "2", "3", "4"};
        private string[] a2 = new string[] {"方位角", "1", "2", "3", "4"};
        private string[] a3 = new string[] {"精度值", "1", "2", "3", "4"};

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //CreateExcel(path);
            OpenExcel(path);
        }

        private void CreateExcel(string filePath)
        {
            MSExcel.Application excel; //创建一个excel对象

            beforeTime = DateTime.Now; //获取excel开始启动时间
            excel = new MSExcel.ApplicationClass(); //创建实例,这时在系统进程中会多出一个excel进程
            afterTime = DateTime.Now; //获取excel启动结束的时间
            try
            {
                object missing = Missing.Value; //Missing 用于调用带默认参数的方法
                excel.Visible = false; //不显示excel文档
                excel.Application.Workbooks.Add(true); //Open Original Excel File
                MSExcel.Workbook myBook;
                MSExcel.Worksheet mySheet;
                myBook = excel.Workbooks[1]; //获取excel程序的工作簿
                mySheet = (MSExcel.Worksheet) myBook.ActiveSheet; //获取Workbook的活动工作表（最上层的工作表）
                mySheet.Cells[1, 1] = "aaaaa";

                mySheet.SaveAs(filePath); //另存为

                #region 释放资源

                //ReleaseComObject 方法递减运行库可调用包装的引用计数。详细信息见MSDN
                Marshal.ReleaseComObject(myBook);
                Marshal.ReleaseComObject(mySheet);
                Marshal.ReleaseComObject(excel);

                myBook.Close();
                excel.Workbooks.Close();
                mySheet = null;
                myBook = null;
                missing = null;
                excel.Quit();
                excel = null;

                #endregion
            }
            catch (Exception)
            {
                KillExcelProcess(); //杀掉进程
            }
            finally
            {
                //可以把KillExcelProcess();放在该处从而杀掉Excel的进程
                KillExcelProcess();
            }
        }

        private void OpenExcel(string filePath)
        {
            MSExcel.Application excel; //创建一个excel对象
            beforeTime = DateTime.Now; //获取excel开始启动时间
            excel = new MSExcel.ApplicationClass(); //创建实例,这时在系统进程中会多出一个excel进程
            afterTime = DateTime.Now; //获取excel启动结束的时间
            try
            {
                object missing = Missing.Value; //Missing 用于调用带默认参数的方法
                excel.Visible = false; //不显示excel文档
                excel.Application.Workbooks.Open(filePath); //Open Original Excel File
                MSExcel.Workbook myBook;
                MSExcel.Worksheet mySheet;
                myBook = excel.Workbooks[1]; //获取excel程序的工作簿
                mySheet = (MSExcel.Worksheet) myBook.Worksheets[1]; //获取Workbook的第一张作表

                #region 设置某一区域的格式

                //MSExcel.Range r = mySheet.get_Range(mySheet.Cells[1, 17], mySheet.Cells[65231, 17]);//获取矩形选择框
                //r.NumberFormatLocal = XlColumnDataType.xlTextFormat;

                #endregion

                #region 写入数据

                mySheet.Cells[10, 10] = "bbbbb";

                #endregion

                excel.DisplayAlerts = false; //进制弹出提示框
                mySheet.SaveAs(filePath, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //另存为

                #region 释放资源

                //ReleaseComObject 方法递减运行库可调用包装的引用计数。详细信息见MSDN
                Marshal.ReleaseComObject(myBook);
                Marshal.ReleaseComObject(mySheet);
                Marshal.ReleaseComObject(excel);
                //Marshal.ReleaseComObject(r);

                myBook.Close();
                excel.Workbooks.Close();
                //r = null;
                mySheet = null;
                myBook = null;
                missing = null;
                excel.Quit();
                excel = null;

                #endregion
            }
            catch (Exception e)
            {
                KillExcelProcess(); //杀掉进程
            }
            finally
            {
                //可以把KillExcelProcess();放在该处从而杀掉Excel的进程
                KillExcelProcess();
            }
        }

        private void KillExcelProcess()
        {
            DateTime startTime;
            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName("Excel");
            foreach (Process myProcess in myProcesses)
            {
                startTime = myProcess.StartTime;

                if (startTime > beforeTime && startTime < afterTime)
                {
                    myProcess.Kill();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(path)) //文件不存在则创建一个
            {
                Class_Comm.NPOICreateExcel(path);
            }
            else //读取并修改数据
            {
                Class_Comm.NPOIReadExcel(path, a1, a2, a3);
            }
        }
    }
}