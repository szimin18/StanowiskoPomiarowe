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
using Stanowisko.Persistance;
using Stanowisko.SharedClasses;

namespace Stanowisko.Recorder
{
    public partial class RecorderWindow : Form
    {
        private const String serieName = "Pomiar z urzadzenia";
        private const String startButtonText = "Rozpocznij rejestracje";
        private const String stopButtonText = "Zatrzymaj rejestracje";

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
            chart.Series[serieName].ChartType = SeriesChartType.FastLine;
            chart.Series[serieName].Points.AddXY(1, 3);
        }

        public void AddSampleToChart(Sample sample)
        {
            chart.Series[serieName].Points.AddXY(sample.Time, sample.Value);
        }

        public void ClearChart()
        {
            chart.Series.Clear();
            chart.Series.Add(serieName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (recorder.recording())
            {
                recorder.stopRecording();
                this.button1.Text = startButtonText;
            }
            else
            {
                recorder.startRecording();
                this.button1.Text = stopButtonText;
            }
        }

        private void RecorderWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            recorder.disconnectDevice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Experiment exp = new Experiment("experiment");
            List<Measurement> list = new List<Measurement>();
            list.Add(recorder.getRecording());
            exp.AddMeasurements(list);
            PersistenceFactory.GetPersistenceManager().AddExperiment(exp);
        }
    }
}
