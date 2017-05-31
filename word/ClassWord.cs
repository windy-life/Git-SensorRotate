using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace word
{
    class ClassWord
    {
        #region 赋值模板文件到一个新的路径（新文件名称在路径里）

        static public void CopyWordModel(string sourcePath, string destPath)
        {
            if (!File.Exists(sourcePath))
            {
                MessageBox.Show("出错了！模板文件不存在！");
                return;
            }
            try
            {
                File.Copy(sourcePath, destPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion
    }
}