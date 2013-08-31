using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleChartApi
{
    public class LineChart : BaseChart
    {
        private LineChartType lineChartType = LineChartType.SingleDataSet;
        private List<LineStyle> lineStyles = new List<LineStyle>();


        /// <summary>
        /// Create a line chart with one line per dataset. Points are evenly spaced along the x-axis.
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        public LineChart(int width, int height)
            : base(width, height)
        {
            this.lineChartType = LineChartType.SingleDataSet;
        }

        public LineChart(int width, int heigth, LineChartType lineChartType)
            : base(width, heigth)
        {
            this.lineChartType = lineChartType;
        }

        /// <summary>
        /// Apply a style to a line. Line styles are applied to lines in order, the 
        /// first line will use the first line style.
        /// </summary>
        /// <param name="lineStyle"></param>
        public void AddLineStyle(LineStyle lineStyle)
        {
            lineStyles.Add(lineStyle);
        }

        protected override string UrlChartType()
        {
            if (this.lineChartType == LineChartType.MultiDataSet)
            {
                return "lxy";
            }
            return "lc";
        }

        protected override ChartType GetCharType()
        {
            return ChartType.LineChart;
        }

        protected override void CollectUrlElements()
        {
            base.CollectUrlElements();
            if (lineStyles.Count > 0)
            {
                string s = "chls=";
                foreach (LineStyle lineStyle in lineStyles)
                {
                    s += lineStyle.LineThickness.ToString() + ",";
                    s += lineStyle.LengthOfSegment.ToString() + ",";
                    s += lineStyle.LengthOfBlankSegment.ToString() + "|";
                }
                urlElements.Enqueue(s.TrimEnd(",".ToCharArray()));
            }
        }
    }


    /// <summary>
    /// Specifies how a line is drawn.
    /// </summary>
    public class LineStyle
    {
        private float lineThickness;
        private float lengthOfSegment;
        private float lengthOfBlankSegment;

        /// <summary>
        /// line thickness in pixels
        /// </summary>
        public float LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }

        /// <summary>
        /// length of each solid line segment in pixels
        /// </summary>
        public float LengthOfSegment
        {
            get { return lengthOfSegment; }
            set { lengthOfSegment = value; }
        }

        /// <summary>
        /// length of each blank line segment in pixels
        /// </summary>
        public float LengthOfBlankSegment
        {
            get { return lengthOfBlankSegment; }
            set { lengthOfBlankSegment = value; }
        }

        /// <summary>
        /// Create a line style
        /// </summary>
        /// <param name="lineThickness">line thickness in pixels</param>
        /// <param name="lengthOfSegment">length of each solid line segment in pixels</param>
        /// <param name="lengthOfBlankSegment">length of each blank line segment in pixels</param>
        public LineStyle(float lineThickness, float lengthOfSegment, float lengthOfBlankSegment)
        {
            this.lineThickness = lineThickness;
            this.lengthOfSegment = lengthOfSegment;
            this.lengthOfBlankSegment = lengthOfBlankSegment;
        }
    }



    /// <summary>
    /// Specifies how the line chart handles datasets
    /// </summary>
    public enum LineChartType
    {
        /// <summary>
        /// One line per dataset, points are evenly spaced along the x-axis
        /// </summary>
        SingleDataSet,

        /// <summary>
        /// Two datasets per line. The first dataset is the x coordinates 
        /// of the line. The second dataset is the Y coordinates of the line.
        /// </summary>
        MultiDataSet
    }
}
