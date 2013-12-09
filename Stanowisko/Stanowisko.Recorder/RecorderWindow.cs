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
using Stanowisko.SharedClasses;

namespace Stanowisko.Recorder
{
    public partial class RecorderWindow : Form
    {
        private const String SerieName = "seria";

        public RecorderWindow()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            ClearChart();
            chart.Series[SerieName].ChartType = SeriesChartType.FastLine;
            chart.Series[SerieName].Points.AddXY(1, 3);
        }

        public void AddSampleToChart(Sample sample)
        {
            chart.Series[SerieName].Points.AddXY(sample.Time, sample.Value);
        }

        public void ClearChart()
        {
            chart.Series.Clear();
            chart.Series.Add(SerieName);
        }
    }
}
