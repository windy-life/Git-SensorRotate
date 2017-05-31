using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSExcel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.
using System.Windows.Forms.DataVisualization.Charting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Threading;
using NPOI.HPSF;

namespace Sensor_Rotate
{
    public static class Class_Comm
    {
        #region 变量

        static DateTime beforeTime; //Excel启动之前时间
        static DateTime afterTime; //Excel启动之后时间

        #endregion

        //-->函数：设置X轴绝对位置
        public static void SetXPosition(double position, double speed, double acc)
        {
            if (Form_Rotate303.SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            if (Form_Rotate303.Control_State == false)
            {
                MessageBox.Show("请先连接转台！");
                return;
            }
            if (Form_Rotate303.X_IsZero == false)
            {
                MessageBox.Show("请先给X轴寻零 ！");
                return;
            }
            if ((Form_Rotate303.X_SetRunWay != 1 && Form_Rotate303.X_SetRunWay != 0) && Form_Rotate303.X_Run == true)
            {
                MessageBox.Show("请先停止转台！");
                return;
            }
            Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XSet(0, position, speed, acc), 0, 16); //设置绝对位置运行和方式
            Form_Rotate303.Delay(50);
            Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XRun_Position(), 0, 16); //设置绝对位置运行
            Form_Rotate303.X_SetRunWay = 1;
            Form_Rotate303.X_Run = true;
        }

        //-->函数：设置Y轴绝对位置
        public static void SetYPosition(double position, double speed, double acc)
        {
            if (Form_Rotate303.SerialRotate.IsOpen == false)
            {
                MessageBox.Show("请先打开串口！");
                return;
            }
            if (Form_Rotate303.Control_State == false)
            {
                MessageBox.Show("请先连接转台！");
                return;
            }
            if (Form_Rotate303.Y_IsZero == false)
            {
                MessageBox.Show("请先给Y轴寻零 ！");
                return;
            }
            if ((Form_Rotate303.Y_SetRunWay != 1 && Form_Rotate303.Y_SetRunWay != 0) && Form_Rotate303.Y_Run == true)
            {
                MessageBox.Show("请先停止转台！");
                return;
            }
            Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.YSet(0, position, speed, acc), 0, 16); //设置绝对位置运行和方式
            Form_Rotate303.Delay(50);
            Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.YRun_Position(), 0, 16); //设置绝对位置运行
            Form_Rotate303.Y_SetRunWay = 1;
            Form_Rotate303.Y_Run = true;
        }

        //-->函数：延时等待X轴停止标志 
        public static void WaitXAxis()
        {
            while (true)
            {
                Application.DoEvents();
                if (Form_Rotate303.X_Run == false)
                {
                    Delay(500);
                    //Thread.Sleep(20);
                    if (Form_Rotate303.X_Run == false)
                    {
                        break;
                    }
                }
                Thread.Sleep(20);
            }
        }

        //-->函数：延时等待Y轴停止标志 
        public static void WaitYAxis()
        {
            while (true)
            {
                Application.DoEvents();
                if (Form_Rotate303.Y_Run == false)
                    break;
            }
        }

        //-->函数：X轴相对位置转动
        public static void RelXRun(double xPosToRun, double xSpeed, double XAcc)
        {
            double _rotatePosition = Form_Rotate303.Xposition; //转台位置
            double _position = xPosToRun + _rotatePosition; //
            int _circleNum = Convert.ToInt32(Math.Truncate(_position/360));
            if (Math.Abs(_position) < 360) //不过360度切换点
            {
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XSet(0, _position, xSpeed, XAcc), 0, 16);
                //设置绝对位置运行和方式
                //Form_Rotate303.Delay(50);
                Thread.Sleep(20);
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XRun_Position(), 0, 16); //设置绝对位置运行
                //Form_Rotate303.Delay(500);
                Thread.Sleep(200);
                Form_Rotate303.X_SetRunWay = 1;
                Form_Rotate303.X_Run = true;
                //Form_Rotate303.Delay(1000);
                Thread.Sleep(20);
                WaitXAxis(); //等待停止
                //Form_Rotate303.Delay(500);
                Thread.Sleep(20);
            }
            else //过360度切换点，需要转圈
            {
                for (int i = 1; i <= Math.Abs(_circleNum); i++) //转动几个360度
                {
                    Form_Rotate303.SerialRotate.Write(
                        Form_Rotate303.Rotate.XSet(0, Convert.ToDouble(_circleNum/Math.Abs(_circleNum)*359.9999), xSpeed,
                            XAcc), 0, 16); //设置绝对位置运行和方式
                    //Form_Rotate303.Delay(50);
                    Thread.Sleep(20);
                    Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XRun_Position(), 0, 16); //设置绝对位置运行
                    //Form_Rotate303.Delay(500);
                    Thread.Sleep(200);
                    Form_Rotate303.X_SetRunWay = 1;
                    Form_Rotate303.X_Run = true;
                    //Form_Rotate303.Delay(1000);
                    Thread.Sleep(50);
                    WaitXAxis(); //等待停止
                    //Form_Rotate303.Delay(500);
                    Thread.Sleep(20);
                }
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XSet(0, _position%360, xSpeed, XAcc), 0, 16);
                //设置绝对位置运行和方式
                //Form_Rotate303.Delay(50);
                Thread.Sleep(20);
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.XRun_Position(), 0, 16); //设置绝对位置运行
                //Form_Rotate303.Delay(500);
                Thread.Sleep(20);
                Form_Rotate303.X_SetRunWay = 1;
                Form_Rotate303.X_Run = true;
                //Form_Rotate303.Delay(1000);
                Thread.Sleep(50);
                WaitXAxis(); //等待停止
                //Form_Rotate303.Delay(500);
                Thread.Sleep(20);
            }
        }

        //-->函数：Y轴相对位置转动
        public static void RelYRun(double yPosToRun, double ySpeed, double yAcc)
        {
            double _rotatePosition = Form_Rotate303.Yposition; //转台位置
            double _position = yPosToRun + _rotatePosition; //
            int _circleNum = Convert.ToInt32(Math.Truncate(_position/360));
            if (Math.Abs(_position) < 360) //不过360度切换点
            {
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.YSet(0, _position, ySpeed, yAcc), 0, 16);
                //设置绝对位置运行和方式
                Form_Rotate303.Delay(50);
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.YRun_Position(), 0, 16); //设置绝对位置运行
                Form_Rotate303.Delay(500);
                Form_Rotate303.Y_SetRunWay = 1;
                Form_Rotate303.Y_Run = true;
                Form_Rotate303.Delay(1000);
                WaitYAxis(); //等待停止
                Form_Rotate303.Delay(500);
            }
            else //过360度切换点，需要转圈
            {
                for (int i = 1; i <= Math.Abs(_circleNum); i++) //转动几个360度
                {
                    Form_Rotate303.SerialRotate.Write(
                        Form_Rotate303.Rotate.YSet(0, Convert.ToDouble(_circleNum/Math.Abs(_circleNum)*359.9999), ySpeed,
                            yAcc), 0, 16); //设置绝对位置运行和方式
                    Form_Rotate303.Delay(50);
                    Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.YRun_Position(), 0, 16); //设置绝对位置运行
                    Form_Rotate303.Delay(500);
                    Form_Rotate303.Y_SetRunWay = 1;
                    Form_Rotate303.Y_Run = true;
                    Form_Rotate303.Delay(1000);
                    WaitYAxis(); //等待停止
                    Form_Rotate303.Delay(500);
                }
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.YSet(0, _position%360, ySpeed, yAcc), 0, 16);
                //设置绝对位置运行和方式
                Form_Rotate303.Delay(50);
                Form_Rotate303.SerialRotate.Write(Form_Rotate303.Rotate.YRun_Position(), 0, 16); //设置绝对位置运行
                Form_Rotate303.Delay(500);
                Form_Rotate303.Y_SetRunWay = 1;
                Form_Rotate303.Y_Run = true;
                Form_Rotate303.Delay(1000);
                WaitYAxis(); //等待停止
                Form_Rotate303.Delay(500);
            }
        }

        //-->函数：延时
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
                Thread.Sleep(10);
            }
            //string a = string.Format("开始时间：{0}\n结束时间：{1}\n进过时间为：{2}", start, Environment.TickCount,
            //    Math.Abs(Environment.TickCount - start));
            //MessageBox.Show(a);
        }

        #region 微软自带com组件修改excel

        //-->函数：创建excel
        public static void CreateExcel(string filePath)
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
                mySheet.Cells[1, 1] = "测试时间";
                mySheet.Cells[1, 2] = "转动次数...";
                MSExcel.Range r = mySheet.Columns; //获取矩形选择框
                r.AutoFit();
                excel.DisplayAlerts = false; //进制弹出提示框
                mySheet.SaveAs(filePath); //另存为

                #region 释放资源

                //ReleaseComObject 方法递减运行库可调用包装的引用计数。详细信息见MSDN
                Marshal.ReleaseComObject(myBook);
                Marshal.ReleaseComObject(mySheet);
                Marshal.ReleaseComObject(excel);
                Marshal.ReleaseComObject(r);

                myBook.Close();
                excel.Workbooks.Close();
                mySheet = null;
                myBook = null;
                r = null;
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

        //-->函数：打开并修改excel
        public static void OpenExcel(string filePath, string[] array1, string[] array2, string[] array3)
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

                #region 写入数据

                //mySheet.Cells[10, 10] = "";
                //查找最下面一行空行
                int rowsNum = mySheet.UsedRange.Rows.Count;
                //mySheet.UsedRange.RowHeight
                for (int i = 0; i < array1.Length; i++)
                {
                    mySheet.Cells[rowsNum + 1, i + 1] = array1[i];
                    mySheet.Cells[rowsNum + 2, i + 1] = array2[i];
                    mySheet.Cells[rowsNum + 3, i + 1] = array3[i];
                }

                #endregion

                #region 设置某一区域的格式

                MSExcel.Range r = mySheet.Columns; //获取矩形选择框
                r.AutoFit();

                #endregion

                excel.DisplayAlerts = false; //禁止弹出提示框
                mySheet.SaveAs(filePath, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //另存为

                #region 释放资源

                //ReleaseComObject 方法递减运行库可调用包装的引用计数。详细信息见MSDN
                Marshal.ReleaseComObject(myBook);
                Marshal.ReleaseComObject(mySheet);
                Marshal.ReleaseComObject(excel);
                Marshal.ReleaseComObject(r);

                myBook.Close();
                excel.Workbooks.Close();
                r = null;
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

        //-->函数：结束excel进程
        public static void KillExcelProcess()
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

        #endregion

        //-->函数：生成图表
        public static void MakeChart(Chart ch, double[] precision, string docName)
        {
            string chartTitle = docName + "精度合格";
            int[] x = new int[precision.Length]; //横坐标数组
            if (Math.Abs(precision.Max() - precision.Min()) > 0.7)
            {
                chartTitle = docName + "精度[不]合格";
            }

            //for (int i = 0; i < precision.Length; i++)
            //{
            //    x[i] = i;
            //    if (precision[i] > 0.7)
            //    {
            //        chartTitle = "精度[不]合格";
            //    }
            //}
            ch.Series[0].ChartType = SeriesChartType.Column;
            ch.Series[0].IsValueShownAsLabel = true;
            ch.Series[0].YValueType = ChartValueType.Double;
            ch.Titles.Clear();
            ch.Titles.Add(chartTitle); //标题
            ch.Series[0].LegendText = "精度值";

            ch.ChartAreas[0].AxisX.Title = "转动次数";
            ch.ChartAreas[0].AxisY.Title = "精度";
            ch.Series[0].Points.DataBindXY(x, precision);
        }

        #region 使用NPOI组件修改excel

        //-->函数：创建一个excel
        public static void NPOICreateExcel(string path)
        {
            #region 网上示例

            //HSSFWorkbook wk = new HSSFWorkbook(); //创建一个工作簿
            //ISheet tb = wk.CreateSheet("mySheet");//创建一个名称为mySheet的表
            ////创建一行，此行为第二行
            //IRow row = tb.CreateRow(1);
            //for (int i = 0; i < 20; i++)
            //{
            //    ICell cell = row.CreateCell(i); //在第二行中创建单元格
            //    cell.SetCellValue(i); //循环往第二行的单元格中添加数据
            //}
            //using (FileStream fs = File.OpenWrite(@"c:/myxls.xls")) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
            //{
            //    wk.Write(fs); //向打开的这个xls文件中写入mySheet表并保存。
            //    MessageBox.Show("提示：创建成功！");
            //} 

            #endregion

            HSSFWorkbook wBook = new HSSFWorkbook(); //新建一个工作簿

            #region 创建文件信息

            DocumentSummaryInformation dSI = PropertySetFactory.CreateDocumentSummaryInformation(); //创建文件摘要信息
            dSI.Company = "kthr"; //赋值一个属性
            SummaryInformation sI = PropertySetFactory.CreateSummaryInformation(); //创建摘要信息
            sI.Author = "SJF";
            wBook.DocumentSummaryInformation = dSI; //赋值到工作簿
            wBook.SummaryInformation = sI; //赋值到工作簿

            #endregion

            ISheet sheet = wBook.CreateSheet("Sheet1"); //新建一个表格
            NPOICreateCell(sheet, 0, 0, "测试时间", true);
            NPOICreateCell(sheet, 0, 1, "转动次数", true);
            using (FileStream file = new FileStream(path, FileMode.Create)) //保存工作簿
            {
                wBook.Write(file);
            }
        }

        //-->函数：在表格中创建一个单元格并赋值
        public static void NPOICreateCell(ISheet sheet, int rowNum, int columnNum, string value, bool isString)
        {
            //IRow roew = sheet.GetRow(rowNum);
            if (sheet.GetRow(rowNum) == (IRow) null) //不存在此行则创建一个新行
            {
                sheet.CreateRow(rowNum);
            }
            if (sheet.GetRow(rowNum).GetCell(columnNum) == (ICell) null) //如果不存在此单元格则创建一个新的
            {
                sheet.GetRow(rowNum).CreateCell(columnNum);
            }
            if (isString)
            {
                sheet.GetRow(rowNum).GetCell(columnNum).SetCellValue(value); //给单元格赋值（字符串）
            }
            else
            {
                sheet.GetRow(rowNum).GetCell(columnNum).SetCellValue(Convert.ToDouble(value)); //给单元格赋值（数字）
            }
        }

        //-->函数：在表格中获取一个单元格并赋值
        public static void NPOIGetCell(ISheet sheet, int rowNum, int columnNum, string value)
        {
            sheet.GetRow(rowNum).CreateCell(columnNum).SetCellValue(value);
        }

        //-->函数：读取并修改一个Excel
        public static void NPOIReadExcel(string path, string[] array1, string[] array2, string[] array3)
        {
            try
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook wBook = new HSSFWorkbook(file); //获取工作簿
                    ISheet sheet = wBook.GetSheetAt(0); //获取第一个表
                    //sheet.ForceFormulaRecalculation = true; //强制要求Excel在打开时重新计算
                    int rowNum = sheet.LastRowNum; //获取有数据的最后一行的行号
                    for (int i = 0; i < array1.Length; i++) //写入数据
                    {
                        NPOICreateCell(sheet, rowNum + 1, i, array1[i], i == 0 ? true : false);
                        NPOICreateCell(sheet, rowNum + 2, i, array2[i], i == 0 ? true : false);
                        NPOICreateCell(sheet, rowNum + 3, i, array3[i], i == 0 ? true : false);
                    }
                    using (FileStream saveFile = new FileStream(path, FileMode.Create, FileAccess.Write)) //保存修改
                    {
                        wBook.Write(saveFile);
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        #endregion
    }
}