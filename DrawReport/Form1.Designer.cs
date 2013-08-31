namespace DrawReport
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLineChart = new System.Windows.Forms.Button();
            this.btnPieChart = new System.Windows.Forms.Button();
            this.btnBarChart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(31, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(459, 275);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnLineChart
            // 
            this.btnLineChart.Location = new System.Drawing.Point(81, 33);
            this.btnLineChart.Name = "btnLineChart";
            this.btnLineChart.Size = new System.Drawing.Size(75, 23);
            this.btnLineChart.TabIndex = 1;
            this.btnLineChart.Text = "折线图";
            this.btnLineChart.UseVisualStyleBackColor = true;
            this.btnLineChart.Click += new System.EventHandler(this.btnLineChart_Click);
            // 
            // btnPieChart
            // 
            this.btnPieChart.Location = new System.Drawing.Point(223, 33);
            this.btnPieChart.Name = "btnPieChart";
            this.btnPieChart.Size = new System.Drawing.Size(75, 23);
            this.btnPieChart.TabIndex = 2;
            this.btnPieChart.Text = "饼图";
            this.btnPieChart.UseVisualStyleBackColor = true;
            this.btnPieChart.Click += new System.EventHandler(this.btnPieChart_Click);
            // 
            // btnBarChart
            // 
            this.btnBarChart.Location = new System.Drawing.Point(365, 33);
            this.btnBarChart.Name = "btnBarChart";
            this.btnBarChart.Size = new System.Drawing.Size(75, 23);
            this.btnBarChart.TabIndex = 3;
            this.btnBarChart.Text = "柱状图";
            this.btnBarChart.UseVisualStyleBackColor = true;
            this.btnBarChart.Click += new System.EventHandler(this.btnBarChart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 375);
            this.Controls.Add(this.btnBarChart);
            this.Controls.Add(this.btnPieChart);
            this.Controls.Add(this.btnLineChart);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "自动生成报表图片Demo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLineChart;
        private System.Windows.Forms.Button btnPieChart;
        private System.Windows.Forms.Button btnBarChart;
    }
}

