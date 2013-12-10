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
        private const String SerieName = "Pomiar z urzadzenia";

        private IRecorder recorder;

        public RecorderWindow(IRecorder recorder)
        {
            this.recorder = recorder;
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

        private void button1_Click(object sender, EventArgs e)
        {
            recorder.startRecording();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recorder.stopRecording();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            recorder.disconnect();
        }

        private void RecorderWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            recorder.disconnect();
        }
    }
}
