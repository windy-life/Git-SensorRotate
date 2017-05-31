using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSWord = Microsoft.Office.Interop.Word;

namespace word
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 定义变量

        string productName = string.Empty; //产品名称
        string productModel = string.Empty; //产品型号
        string productCode = string.Empty; //产品编号
        string productMeasureRange = string.Empty; //产品测量范围
        string productVoltage = string.Empty; //产品电压
        string productCurrent = string.Empty; //产品电流
        string productAccuracy = string.Empty; //产品要求的精度
        string productAccuracyMea = string.Empty; //产品实测精度
        string productTestContoler = string.Empty; //测试人员
        string productTestDate = string.Empty; //测试日期 

        Dictionary<string, string> dic = new Dictionary<string, string>(); //保存书签名和书签值

        string sourcePath = @"C:\Users\afei\Desktop\模板-传感器测试记录.doc"; //模板路径
        string destPath = @"C:\Users\afei\Desktop\记录1.doc"; //目标路径

        private string[] bookmarks = new[]
            {"产品名称", "产品型号", "产品编号", "测量范围", "输入电压", "输入电流", "要求精度", "实测精度", "测试人员", "测试日期"};

        /// <summary>
        /// 定位书签在数组中的位置
        /// </summary>
        enum bMNum
        {
            /// <summary>
            /// 产品名称
            /// </summary>
            PName = 0,

            /// <summary>
            /// 产品型号
            /// </summary>
            PModel = 1,

            /// <summary>
            /// 产品编号
            /// </summary>
            PCode = 2,

            /// <summary>
            /// 测量范围
            /// </summary>
            PRange = 3,

            /// <summary>
            /// 输入电压
            /// </summary>
            PVoltage = 4,

            /// <summary>
            /// 输入电流
            /// </summary>
            PCurrent = 5,

            /// <summary>
            /// 要求精度
            /// </summary>
            PAccuracy = 6,

            /// <summary>
            /// 实测精度
            /// </summary>
            PAccuracyMes = 7,

            /// <summary>
            /// 测试人员
            /// </summary>
            PControler = 8,

            /// <summary>
            /// 测试日期
            /// </summary>
            PDate = 9
        }

        #endregion

        #region 编辑框变化赋值数据

        private void tbxName_TextChanged(object sender, EventArgs e)
        {
            productName = tbxName.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PName]))
            {
                dic[bookmarks[(int) bMNum.PName]] = productName;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PName], productName);
            }
        }

        private void tbxCode_TextChanged(object sender, EventArgs e)
        {
            productCode = tbxCode.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PCode]))
            {
                dic[bookmarks[(int) bMNum.PCode]] = productCode;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PCode], productCode);
            }
        }

        private void tbxVoltage_TextChanged(object sender, EventArgs e)
        {
            productVoltage = tbxVoltage.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PVoltage]))
            {
                dic[bookmarks[(int) bMNum.PVoltage]] = productVoltage;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PVoltage], productVoltage);
            }
        }

        private void tbxAccuracy_TextChanged(object sender, EventArgs e)
        {
            productAccuracy = tbxAccuracy.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PAccuracy]))
            {
                dic[bookmarks[(int) bMNum.PAccuracy]] = productAccuracy;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PAccuracy], productAccuracy);
            }
        }

        private void tbxContoler_TextChanged(object sender, EventArgs e)
        {
            productTestContoler = tbxContoler.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PControler]))
            {
                dic[bookmarks[(int) bMNum.PControler]] = productTestContoler;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PControler], productTestContoler);
            }
        }

        private void tbxModel_TextChanged(object sender, EventArgs e)
        {
            productModel = tbxModel.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PModel]))
            {
                dic[bookmarks[(int) bMNum.PModel]] = productModel;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PModel], productModel);
            }
        }

        private void tbxRange_TextChanged(object sender, EventArgs e)
        {
            productMeasureRange = tbxRange.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PRange]))
            {
                dic[bookmarks[(int) bMNum.PRange]] = productMeasureRange;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PRange], productMeasureRange);
            }
        }

        private void tbxCurrent_TextChanged(object sender, EventArgs e)
        {
            productCurrent = tbxCurrent.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PCurrent]))
            {
                dic[bookmarks[(int) bMNum.PCurrent]] = productCurrent;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PCurrent], productCurrent);
            }
        }

        private void tbxMeasuredAccuracy_TextChanged(object sender, EventArgs e)
        {
            productAccuracyMea = tbxMeasuredAccuracy.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PAccuracyMes]))
            {
                dic[bookmarks[(int) bMNum.PAccuracyMes]] = productAccuracyMea;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PAccuracyMes], productAccuracyMea);
            }
        }

        private void tbxDate_TextChanged(object sender, EventArgs e)
        {
            productTestDate = tbxDate.Text;
            if (dic.ContainsKey(bookmarks[(int) bMNum.PDate]))
            {
                dic[bookmarks[(int) bMNum.PDate]] = productTestDate;
            }
            else
            {
                dic.Add(bookmarks[(int) bMNum.PDate], productTestDate);
            }
        }

        #endregion

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            #region 新方法更改word

            WordDocument wd = new WordDocument(destPath, sourcePath); //读取模板文件
            foreach (KeyValuePair<string, string> kV in dic)
            {
                wd.InsertValue(kV.Key, kV.Value);
            }
            //wd.InsertValue("产品名称", "新产品");
            wd.Save();
            wd.Close();

            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbxDate.Text = DateTime.Today.ToString("d");
            tbxName_TextChanged(null, null);
            tbxCode_TextChanged(null, null);
            tbxModel_TextChanged(null, null);
            tbxDate_TextChanged(null, null);
            tbxContoler_TextChanged(null, null);
            tbxVoltage_TextChanged(null, null);
            tbxAccuracy_TextChanged(null, null);
            tbxCurrent_TextChanged(null, null);
            tbxMeasuredAccuracy_TextChanged(null, null);
            tbxRange_TextChanged(null, null);
        }
    }
}