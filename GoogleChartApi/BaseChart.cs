using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleChartApi
{
    /// <summary>
    /// 所有报表的基类
    /// </summary>
    public abstract class BaseChart
    {
        private const string API_BASE = "http://chart.apis.google.com/chart?";
        internal Queue<string> urlElements = new Queue<string>();


        /// <summary>
        /// 创建一个报表
        /// </summary>
        /// <param name="width">宽度，像素</param>
        /// <param name="height">高度，像素</param>
        public BaseChart(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        // 宽、高
        private int width;
        private int height;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }


        //设置数据
        private string data;
        public void SetData(int[] data)
        {
            this.data = ChartData.Encode(data);
        }

        /// <summary>
        /// Set chart to use integer dataset collection
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ICollection<int[]> data)
        {
            this.data = ChartData.Encode(data);
        }

        /// <summary>
        /// Set chart to use single float dataset
        /// </summary>
        /// <param name="data"></param>
        public void SetData(float[] data)
        {
            this.data = ChartData.Encode(data);
        }

        /// <summary>
        /// Set chart to use float dataset collection
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ICollection<float[]> data)
        {
            this.data = ChartData.Encode(data);
        }


        //标题
        private string title;
        private string titleColor;

        public void SetTitle(string title)
        {
            string urlTitle = title.Replace(" ", "+");
            urlTitle = urlTitle.Replace(Environment.NewLine, "|");
            this.title = urlTitle;
        }

        public void SetTitle(string title, string color)
        {
            SetTitle(title);
            this.titleColor = color;
        }

        public void SetTitle(string title, string color, int fontSize)
        {
            SetTitle(title);
            this.titleColor = color + "," + fontSize;
        }

        private string[] datasetColors;
        public void SetDatasetColors(string[] datasetColors)
        {
            this.datasetColors = datasetColors;
        }

        // labels
        List<ChartAxis> axes = new List<ChartAxis>();
        List<string> legendStrings = new List<string>();

        /// <summary>
        /// Set chart legend
        /// </summary>
        /// <param name="strs">legend labels</param>
        public virtual void SetLegend(string[] strs)
        {
            foreach (string s in strs)
            {
                legendStrings.Add(s);
            }
        }

        public void AddAxis(ChartAxis axis)
        {
            axes.Add(axis);
        }


        //makers
        List<FillArea> fillAreas = new List<FillArea>();

        /// <summary>
        /// Add a fill area to the chart. Fill areas are fills between / under lines.
        /// </summary>
        /// <param name="fillArea"></param>
        public void AddFillArea(FillArea fillArea)
        {
            this.fillAreas.Add(fillArea);
        }

        private string getFillAreasUrlElement()
        {
            string s = string.Empty;
            foreach (FillArea fillArea in fillAreas)
            {
                s += fillArea.GetUrlString() + "|";
            }
            return s.TrimEnd("|".ToCharArray());
        }
        // end makers


        public string GetUrl()
        {
            CollectUrlElements();
            return GenerateUrlString();
        }


        protected abstract string UrlChartType();
        protected abstract ChartType GetCharType();

        protected virtual void CollectUrlElements()
        {
            urlElements.Clear();
            urlElements.Enqueue(string.Format("cht={0}", this.UrlChartType()));
            urlElements.Enqueue(string.Format("chs={0}x{1}", this.width, this.height));
            urlElements.Enqueue(this.data);

            //chart title
            if (title != null)
            {
                urlElements.Enqueue(string.Format("chtt={0}", this.title));
            }
            if (titleColor != null)
            {
                urlElements.Enqueue(string.Format("chts={0}", this.titleColor));
            }


            //dataset colors
            if (datasetColors != null)
            {
                string s = "chco=";
                foreach (string color in datasetColors)
                {
                    s += color + ",";
                }
                urlElements.Enqueue(s.TrimEnd(",".ToCharArray()));
            }


            // Axes
            if (axes.Count > 0)
            {
                string axisTypes = "chxt=";
                string axisLabels = "chxl=";
                string axisLabelPositions = "chxp=";
                string axisRange = "chxr=";
                string axisStyle = "chxs=";

                int axisIndex = 0;
                foreach (ChartAxis axis in axes)
                {
                    axisTypes += axis.urlAxisType() + ",";
                    axisLabels += axisIndex.ToString() + ":" + axis.urlLabels();
                    string labelPositions = axis.urlLabelPositions();
                    if (!String.IsNullOrEmpty(labelPositions))
                    {
                        axisLabelPositions += axisIndex.ToString() + "," + labelPositions + "|";
                    }
                    string axisRangeStr = axis.urlRange();
                    if (!String.IsNullOrEmpty(axisRangeStr))
                    {
                        axisRange += axisIndex.ToString() + "," + axisRangeStr + "|";
                    }
                    string axisStyleStr = axis.UrlAxisStyle();
                    if (!String.IsNullOrEmpty(axisStyleStr))
                    {
                        axisStyle += axisIndex.ToString() + "," + axisStyleStr + "|";
                    }
                    axisIndex++;
                }
                axisTypes = axisTypes.TrimEnd(",".ToCharArray());
                axisLabels = axisLabels.TrimEnd("|".ToCharArray());
                axisLabelPositions = axisLabelPositions.TrimEnd("|".ToCharArray());
                axisRange = axisRange.TrimEnd("|".ToCharArray());
                axisStyle = axisStyle.TrimEnd("|".ToCharArray());

                urlElements.Enqueue(axisTypes);
                urlElements.Enqueue(axisLabels);
                urlElements.Enqueue(axisLabelPositions);
                urlElements.Enqueue(axisRange);
                urlElements.Enqueue(axisStyle);
            }

            //makers
            string markersString = "chm=";
            if (fillAreas.Count > 0)
            {
                markersString += getFillAreasUrlElement() + "|";
            }
            if (fillAreas.Count > 0)
            {
                urlElements.Enqueue(markersString.TrimEnd("|".ToCharArray()));
            }

        }

        private string GenerateUrlString()
        {
            string url = string.Empty;
            url += BaseChart.API_BASE;
            url += urlElements.Dequeue();

            while (urlElements.Count > 0)
            {
                url += "&" + urlElements.Dequeue();
            }

            return url;
        }


        #region 设置数据源的公用方法
        public ICollection<float[]> ConvertToPorcent(int[] data)
        {
            ICollection<int[]> dataCollection = new List<int[]>();
            dataCollection.Add(data);
            return ConvertToPorcent(dataCollection);
        }

        public ICollection<float[]> ConvertToPorcent(ICollection<int[]> data)
        {
            int maxValue = findMaxValue(data);
            ICollection<float[]> dataPorcent = new List<float[]>();

            foreach (int[] dataLine in data)
            {
                float[] dataLinePorcent = new float[dataLine.Length];
                for (int j = 0; j < dataLine.Length; j++)
                    dataLinePorcent[j] = (dataLine[j] * 100 / maxValue); // Convert to porcent

                dataPorcent.Add(dataLinePorcent);
            }

            return dataPorcent;
        }

        public int findMaxValue(int[] data)
        {
            int maxValue = -1;
            foreach (int value in data)
            {
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }

            return maxValue;
        }

        protected int findMaxValue(ICollection<int[]> data)
        {
            List<int> maxValuesList = new List<int>();

            foreach (int[] objectArray in data)
            {
                maxValuesList.Add(findMaxValue(objectArray));
            }

            int[] maxValues = maxValuesList.ToArray();
            Array.Sort(maxValues);
            return maxValues[maxValues.Length - 1];
        }
        #endregion


    }

    /// <summary>
    /// 报表类型
    /// </summary>
    public enum ChartType
    {
        /// <summary>
        /// 饼图
        /// </summary>
        PieChart,

        /// <summary>
        /// 柱图
        /// </summary>
        BarChart,

        /// <summary>
        /// 折线图
        /// </summary>
        LineChart
    }
}
