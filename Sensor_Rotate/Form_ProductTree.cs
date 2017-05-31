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
    public partial class Form_ProductTree : DockContent
    {
        public Form_ProductTree()
        {
            InitializeComponent();
        }
        //定义变量
        public static int _s2000Flag = 0;//SANG2000S打开产品类型标志位（0:15度;1:30度）
        Form_Communicate frm_Communicate;
        Form_ReadWritePara frm_ReadWritePara;
        Form_XCalibration frm_XCalibration;
        Form_YCalibration frm_YCalibration;
        Form_XCalibration_D30 frm_XCalibration_D30;
        Form_YCalibration_D30 frm_YCalibration_D30;
        Form_TCalibration frm_TCalibration;
        Form_TCM frm_TCM;
        

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        //-----------------------------------------
        //树状图中选中
        //-----------------------------------------
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }
        //-----------------------------------------
        //树状图双击节点
        //-----------------------------------------
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode == null)//点击空白处时为null，进行放错处理
                {
                    return;
                }
                
                #region 配置通讯
                if (treeView1.SelectedNode.Text == "配置通讯")
                {
                    if (frm_Communicate == null || frm_Communicate.IsDisposed)
                    {
                        frm_Communicate = new Form_Communicate();
                        frm_Communicate.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_Communicate.Activate();
                    }
                    CloseOtherProductForms(frm_Communicate);//隐藏其他窗体
                    if (treeView1.SelectedNode.Name == "SANG2000S-D15DTR_Settings")
                    {
                        _s2000Flag = 0;//15度
                    }
                    else
                    {
                        _s2000Flag = 1;//30度
                    }
                }
                #endregion
                
                #region SANG2000S读写参数
                if (treeView1.SelectedNode.Name == "SANG2000S-D15DTR_ReadP" || treeView1.SelectedNode.Name == "SANG2000S-D30DTR_ReadP")
                {
                    if (frm_ReadWritePara == null || frm_ReadWritePara.IsDisposed)
                    {
                        frm_ReadWritePara = new Form_ReadWritePara();
                        frm_ReadWritePara.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_ReadWritePara.Activate();
                    }
                    CloseOtherProductForms(frm_ReadWritePara);
                    
                }
                #endregion
                
                #region SANG2000S-D15 X轴标定
                if (treeView1.SelectedNode.Name == "SANG2000S-D15DTR_X")
                {
                    if (frm_XCalibration == null || frm_XCalibration.IsDisposed)
                    {
                        frm_XCalibration = new Form_XCalibration();
                        frm_XCalibration.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_XCalibration.Activate();
                    }
                    CloseOtherProductForms(frm_XCalibration);
                }
                #endregion

                #region SANG2000S-D15 Y轴标定
                if (treeView1.SelectedNode.Name == "SANG2000S-D15DTR_Y")
                {
                    if (frm_YCalibration == null || frm_YCalibration.IsDisposed)
                    {
                        frm_YCalibration = new Form_YCalibration();
                        frm_YCalibration.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_YCalibration.Activate();
                    }
                CloseOtherProductForms(frm_YCalibration);
            }
                #endregion

                #region SANG2000S-D30 X轴标定
                if (treeView1.SelectedNode.Name == "SANG2000S-D30DTR_X")
                {
                    if (frm_XCalibration_D30 == null || frm_XCalibration_D30.IsDisposed)
                    {
                        frm_XCalibration_D30 = new Form_XCalibration_D30();
                        frm_XCalibration_D30.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_XCalibration_D30.Activate();
                    }
                    CloseOtherProductForms(frm_XCalibration_D30);
                }
                #endregion

                #region SANG2000S-D30 Y轴标定
                if (treeView1.SelectedNode.Name == "SANG2000S-D30DTR_Y")
                {
                    if (frm_YCalibration_D30 == null || frm_YCalibration_D30.IsDisposed)
                    {
                        frm_YCalibration_D30 = new Form_YCalibration_D30();
                        frm_YCalibration_D30.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_YCalibration_D30.Activate();
                    }
                    CloseOtherProductForms(frm_YCalibration_D30);
                }
                #endregion

                #region SANG2000S温度标定
                if (treeView1.SelectedNode.Name == "SANG2000S-D15DTR_T" || treeView1.SelectedNode.Name == "SANG2000S-D30DTR_T")
                {
                    if (frm_TCalibration == null || frm_TCalibration.IsDisposed)
                    {
                        frm_TCalibration = new Form_TCalibration();
                        frm_TCalibration.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_TCalibration.Activate();
                    }
                    CloseOtherProductForms(frm_TCalibration);
                }
                #endregion
                
                #region TCM罗盘标定
                if (treeView1.SelectedNode.Text == "TCM")
                {
                    if (frm_TCM == null || frm_TCM.IsDisposed)
                    {
                        frm_TCM = new Form_TCM();
                        frm_TCM.Show(DockPanel, DockState.Document);
                    }
                    else
                    {
                        frm_TCM.Activate();
                    }
                    CloseOtherProductForms(frm_TCM);//隐藏其他窗体
                } 
                #endregion
            }
            catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        //关闭除新打开的此窗体外，其他调试窗体
        private void CloseOtherProductForms(DockContent frm)
        {
            try
            {
                DockContent[] frms = new DockContent[30];//存储所有已经打开窗体中的DockContent窗体
                int i = 0;
                foreach (Form form in Application.OpenForms)//遍历所有已经打开的窗体
                {
                    if (form is DockContent)
                    {
                        frms[i] = (DockContent)form;//将DockContent窗体存入数组
                        i++;
                    }
                }
            foreach (DockContent form in frms)//遍历所有DockContent窗体
                {
                if (form != null)
                {
                    if (form.DockState == DockState.Document && form != frm)
                    {
                        form.Hide();//隐藏该窗体
                    }
                }
            }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
