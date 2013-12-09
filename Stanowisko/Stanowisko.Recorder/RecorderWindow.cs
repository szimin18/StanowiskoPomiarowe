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
        private Series serie;
        private const String SerieName = "seria";

        public RecorderWindow()
        {
            InitializeComponent();
            InitializeChart();
            ClearChart();
        }

        private void InitializeChart()
        {
            serie = chart.Series.Add(SerieName);
            serie.ChartType = SeriesChartType.FastLine;
        }

        public void AddSampleToChart(Sample sample)
        {
            serie.Points.AddXY(sample.Time, sample.Value);
        }

        public void ClearChart()
        {
            chart.Series.Clear();
        }
    }
}
