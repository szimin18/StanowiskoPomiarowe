using Stanowisko.SharedClasses;
using Stanowisko.Symulator;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;

namespace Stanowisko.Recorder
{
    public class Recorder : IRecorder
    {
        #region Private Member Variables
        private const uint DefaultPeriod = 1000;
        private uint _period;
        private List<Sample> _samples;
        private bool _isRecording = false;
        private bool _isConnected = false;
        private IMeasuringDevice _measuringDevice;
        private System.Timers.Timer _timer;
        private RecorderWindow _window;
        #endregion

        #region Private Methods

        private delegate void AddSampleToChartDelegate(Sample sample);

        private void _getSample()
        {
            Sample sample = _measuringDevice.GetSample();
            _samples.Add(sample);

            _window.BeginInvoke(new AddSampleToChartDelegate(addSampleToChart), sample);
        }

        private void addSampleToChart(Sample sample)
        {
            _window.AddSampleToChart(sample);
        }
        
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _getSample();
        }
        #endregion

        #region Constructors
        public Recorder(IMeasuringDevice measuringDevice)
        {
            this._measuringDevice = measuringDevice;
            _period = DefaultPeriod;
            _timer = new System.Timers.Timer(_period);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _samples = new List<Sample>();
            _window = new RecorderWindow(this);

            _window.Show();
        }
        #endregion

        #region Public Methods
        public void startRecording()
        {
            if (!_isConnected)
            {
                string errorMessage = _measuringDevice.StartConnection();
                if (errorMessage != null)
                {
                    MessageBox.Show(errorMessage);
                    return;
                }
            }
            _isRecording = true;
            _timer.Start();
        }

        public void stopRecording()
        {
            _timer.Stop();
            _measuringDevice.StopConnection();
            _isConnected = false;
            _isRecording = false;
        }

        public void setPeriod(uint period)
        {
            if (_isRecording)
                throw new Exception();
            this._period = period;
            _timer.Interval = period;
        }

        public Measurement getRecording()
        {
            Measurement measurement = new Measurement(_samples);
            return measurement;
        }
        #endregion
    }
}
