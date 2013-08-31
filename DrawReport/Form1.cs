using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GoogleChartApiDemo;
using GoogleChartApi;

namespace DrawReport
{
    public partial class Form1 : Form
    {
        //图片url
        string url = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLineChart_Click(object sender, EventArgs e)
        {
            url = GenerateLineChart();

            SetUrl2PicBox(url);
        }

        private void btnPieChart_Click(object sender, EventArgs e)
        {
            url = GeneratePieChart();

            SetUrl2PicBox(url);
        }

        private void btnBarChart_Click(object sender, EventArgs e)
        {
            url = GenerateBarChart();

            SetUrl2PicBox(url);
        }

        private void SetUrl2PicBox(string url)
        {
            HttpHelper helper = new HttpHelper();
            byte[] bytes = helper.DownloadPng(url);

            //将byte数组转成Image对象
            Image img = FileOperateHelper.BytToImg(bytes);
            img.Save("chart.png", System.Drawing.Imaging.ImageFormat.Png);

            //把image对象设值到picturebox上
            pictureBox1.Image = img;
        }

        /// <summary>
        /// 折线图
        /// </summary>
        /// <returns></returns>
        public string GenerateLineChart()
        {
            int[] data = new int[] { 1027, 98, 36, 374, 473, 54, 126, 22, 147, 20, 188, 58 };
            string[] axisLabels = new string[] { "AA", "BB", "CC", "DD", "EE", "FF", "GG", "HH", "II", "JJ", "KK", "LL" };

            LineChart lineChart = new LineChart(450, 250);
            lineChart.SetTitle("三日内更新", "000000", 14);
            lineChart.SetData(lineChart.ConvertToPorcent(data));

            ChartAxis axisX = new ChartAxis(ChartAxisType.Bottom, axisLabels);
            ChartAxis axisY = new ChartAxis(ChartAxisType.Left);
            axisY.SetRange(0, lineChart.findMaxValue(data));

            lineChart.AddFillArea(new FillArea("EFEFEF", 0));
            lineChart.AddAxis(axisX);
            lineChart.AddAxis(axisY);

            return lineChart.GetUrl();
        }

        /// <summary>
        /// 柱状图
        /// </summary>
        /// <returns></returns>
        public string GenerateBarChart()
        {
            int[] data = new int[] { 1027, 98, 36, 374, 473, 54, 126, 22, 147, 20, 188, 58 };
            string[] axisLabels = new string[] { "AA", "BB", "BB", "DD", "EE", "FF", "GG", "HH", "II", "JJ", "KK", "LL" };

            BarChart barChart = new BarChart(500, 250, BarChartOrientation.Vertical, BarChartStyle.Grouped);
            barChart.SetTitle("三日内更新数据");

            ChartAxis axisX = new ChartAxis(ChartAxisType.Bottom, axisLabels);
            ChartAxis axisY = new ChartAxis(ChartAxisType.Left);
            axisY.SetRange(0, barChart.findMaxValue(data));

            barChart.AddAxis(axisX);
            barChart.AddAxis(axisY);
            barChart.SetData(barChart.ConvertToPorcent(data));

            return barChart.GetUrl();
        }

        /// <summary>
        /// 饼图
        /// </summary>
        /// <returns></returns>
        public static string GeneratePieChart()
        {

            float[] data = new float[] { 0.4836F, 0.0154F, 0.008F, 0.158F, 0.0987F, 0.0115F, 0.0164F, 0.0032F, 0.0393F, 0.0018F };
            //double[] data = new double[] { 0.4836, 0.0154, 0.008, 0.158, 0.0987, 0.0115, 0.0164, 0.0032, 0.0393, 0.0018, 0.0611, 0.0103, 0.0116, 0.0091, 0.072 };
            string[] axisLabels = new string[] { "测试 ", "测试2 ", "测试3 ", "测试4", "测试5 ", "测试6 ", "测试7 ", "测试8 ", "测试9", "T测试10 " };

            PieChart pieChart = new PieChart(450, 230, PieChartType.TwoD);
            pieChart.SetTitle("每日书库占比数据");
            axisLabels = pieChart.ConvertPieChartPercent(data, axisLabels);
            pieChart.SetPieChartLabels(axisLabels);
            pieChart.SetData(data);

            return pieChart.GetUrl();
        }
    }
}
