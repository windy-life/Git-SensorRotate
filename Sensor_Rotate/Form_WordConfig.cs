using System;
using System.Windows.Forms;
using Sensor_Rotate.Properties;

namespace Sensor_Rotate
{
    /// <summary>
    /// 配置word记录文件的内容
    /// </summary>
    public partial class Form_WordConfig : Form
    {
        /// <summary>
        /// 加载初始化数据
        /// </summary>
        public Form_WordConfig()
        {
            InitializeComponent();
            //加载保存的数据
            tbxName.Text = mysSettings.wordPName;
            tbxModel.Text = mysSettings.wordPModel;
            tbxCode.Text = mysSettings.wordPCode;
            tbxContoler.Text = mysSettings.wordPControler;
            tbxAccuracy.Text = mysSettings.wordPAccuracy;
            tbxVoltage.Text = mysSettings.wordPVoltage;
            tbxCurrent.Text = mysSettings.wordPCurrent;
            tbxDate.Text = mysSettings.wordPDate;
            cobRange.SelectedIndex = mysSettings.wordPRange;
            tbxFileName.Text = mysSettings.wordFileName;
            tbxFilePath.Text = mysSettings.wordFilePath;
            fbdPath.SelectedPath = mysSettings.wordFilePath;
            //设置窗体位置
            StartPosition = FormStartPosition.CenterScreen;
        }

        #region 定义变量

        Settings mysSettings = new Settings();
        private bool _saveFlag;//是否保存写入的数据
        #endregion

        private void Form_WordConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_saveFlag) return;
            mysSettings.wordPName = tbxName.Text;
            mysSettings.wordPModel = tbxModel.Text;
            mysSettings.wordPCode = tbxCode.Text;
            mysSettings.wordPControler = tbxContoler.Text;
            mysSettings.wordPAccuracy = tbxAccuracy.Text;
            mysSettings.wordPVoltage = tbxVoltage.Text;
            mysSettings.wordPCurrent = tbxCurrent.Text;
            mysSettings.wordPDate = tbxDate.Text;
            mysSettings.wordPRange = cobRange.SelectedIndex;
            mysSettings.wordFileName = tbxFileName.Text;
            mysSettings.wordFilePath = tbxFilePath.Text;
            mysSettings.Save(); //保存数据
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            tbxDate.Text = DateTime.Today.ToString("d");
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.OK;
            if (result == fbdPath.ShowDialog())
            {
                tbxFilePath.Text = fbdPath.SelectedPath;
            }
            
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            _saveFlag = true;//保存数据
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}