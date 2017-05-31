using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double[] x = new double[] { 0,1,2,3,4,5};
        double[] y = new double[] { 0, 1, 2, 3, 4, 5 };

        private void button1_Click(object sender, EventArgs e)
        {
            //Series ser = new Series();
            //ser.Points.Add(1);
            //chart1.Series.Add(ser);
            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Titles.Clear();
            chart1.Titles.Add("111");
            chart1.Series["Series1"].LegendText = "222";
            chart1.Series["Series1"].AxisLabel = "3333";
            chart1.Series["Series1"].Label = "4444";
            chart1.AxisScrollBarClicked += Chart1_AxisScrollBarClicked; ;
            chart1.ChartAreas[0].AxisX.Title = "横向";
            chart1.ChartAreas[0].AxisY.Title = "纵向";
            chart1.Series["Series1"].Points.DataBindXY(x, y);
        }

        private void Chart1_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
