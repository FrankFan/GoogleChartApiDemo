using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleChartApi
{
    /// <summary>
    /// 柱状图
    /// </summary>
    public class BarChart : BaseChart
    {
        BarChartOrientation orientation;
        BarChartStyle style;
        int barWidth;

        /// <summary>
        /// Create a bar chart
        /// </summary>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        /// <param name="orientation">The orientation of the bars.</param>
        /// <param name="style">Bar chart style when using multiple data sets</param>
        public BarChart(int width, int height, BarChartOrientation orientation, BarChartStyle style)
            : base(width, height)
        {
            this.orientation = orientation;
            this.style = style;
        }

        /// <summary>
        /// Set the width of the individual bars
        /// </summary>
        /// <param name="width">Width in pixels</param>
        public void SetBarWidth(int width)
        {
            this.barWidth = width;
        }


        /// <summary>
        /// Return the chart identifier used in the chart url.
        /// </summary>
        /// <returns></returns>
        protected override string UrlChartType()
        {
            char orientationChar = this.orientation == BarChartOrientation.Horizontal ? 'h' : 'v';
            char styleChar = this.style == BarChartStyle.Stacked ? 's' : 'g';

            return string.Format("b{0}{1}", orientationChar, styleChar);
        }

        /// <summary>
        /// Return the chart type for this chart
        /// </summary>
        /// <returns></returns>
        protected override ChartType GetCharType()
        {
            return ChartType.BarChart;
        }

        /// <summary>
        /// Collect all the elements that will make up the chart url.
        /// </summary>
        protected override void CollectUrlElements()
        {
            base.CollectUrlElements();
            if (this.barWidth != 0)
            {
                base.urlElements.Enqueue(string.Format("chbh={0}", this.barWidth));
            }
        }

    }

    /// <summary>
    /// 柱状图的方向
    /// </summary>
    public enum BarChartOrientation
    {
        /// <summary>
        /// 垂直方向的柱状图
        /// </summary>
        Vertical,

        /// <summary>
        /// 水平方向的柱状图
        /// </summary>
        Horizontal
    }

    /// <summary>
    /// Bar chart style when using multiple data sets
    /// </summary>
    public enum BarChartStyle
    {
        /// <summary>
        /// Multiple data sets will be stacked.
        /// </summary>
        Stacked,

        /// <summary>
        /// Multiple data sets will be grouped.
        /// </summary>
        Grouped
    }
}
