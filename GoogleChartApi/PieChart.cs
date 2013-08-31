using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleChartApi
{
    public class PieChart : BaseChart
    {
        private PieChartType pieChartType;
        private string[] pieChartLabels;

        /// <summary>
        /// 默认创建2D饼图
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        public PieChart(int width, int height)
            : base(width, height)
        {

        }

        /// <summary>
        /// 创建一个指定类型的饼图
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        /// <param name="pieChartType"></param>
        public PieChart(int width, int heigth, PieChartType pieChartType)
            : base(width, heigth)
        {
            this.pieChartType = pieChartType;
        }

        public void SetPieChartLabels(string[] labels)
        {
            this.pieChartLabels = labels;
        }


        protected override void CollectUrlElements()
        {
            base.CollectUrlElements();
            if (pieChartLabels != null)
            {
                string s = "chl=";
                foreach (string label in pieChartLabels)
                {
                    s += label + "|";
                }
                this.urlElements.Enqueue(s.TrimEnd("|".ToCharArray()));
            }
        }

        public override void SetLegend(string[] strs)
        {
            throw new NotImplementedException();
        }

        protected override ChartType GetCharType()
        {
            return ChartType.PieChart;
        }

        protected override string UrlChartType()
        {
            if (this.pieChartType == PieChartType.ThreeD)
            {
                return "p3";
            }

            return "p";
        }

        #region 百分比转换方法

        public string[] ConvertPieChartPercent(int[] data, string[] labels)
        {
            int sum = GetSum(data);

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < labels.Length; j++)
                {
                    if (i == j)
                    {
                        string percent = string.Format("{0:P1}", data[i] * 1.0 / sum);
                        labels[j] = labels[j] + percent;
                    }
                }
            }

            return labels;
        }

        private static int GetSum(int[] data)
        {
            int sum = 0;
            foreach (int item in data)
            {
                sum += item;
            }
            return sum;
        }

        public string[] ConvertPieChartPercent(float[] data, string[] labels)
        {
            float sum = GetSum(data);

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < labels.Length; j++)
                {
                    if (i == j)
                    {
                        string percent = string.Format("{0:P1}", data[i] * 1.0 / sum);
                        labels[j] = labels[j] + percent;
                    }
                }
            }

            return labels;
        }

        private static float GetSum(float[] data)
        {
            float sum = 0;
            foreach (float item in data)
            {
                sum += item;
            }
            return sum;
        }

        #endregion

    }

    /// <summary>
    /// 饼图类型
    /// </summary>
    public enum PieChartType
    {
        /// <summary>
        /// 2D
        /// </summary>
        TwoD,

        /// <summary>
        /// 3D
        /// </summary>
        ThreeD
    }
}
