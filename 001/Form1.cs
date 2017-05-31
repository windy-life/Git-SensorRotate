using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tt.Elapsed += Tt_Elapsed;
        }

        private void Tt_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {


            Invoke(new MethodInvoker(delegate () { progressBar1.PerformStep(); }));
            if (progressBar1.Value == 100)
            {
                //button1.Text = "开始";
                Invoke(new MethodInvoker(delegate () { button1.Text = "开始"; }));
                tt.Enabled = false;
                //MessageBox.Show("a");
                return;
            }
            /*Invoke(new MethodInvoker(delegate () { progressBar1.PerformStep(); }));*///progressBar1.Value = i;
            //progressBar1.PerformStep();
        }

        System.Timers.Timer tt = new System.Timers.Timer(10);
        int i = 0;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开始")
            {
                tt.Enabled = true;
                button1.Text = "停止";
                progressBar1.Value = 0;
            }
            else
            {
                tt.Enabled = false;
                button1.Text = "开始";
            }
            
        }
    }
}
