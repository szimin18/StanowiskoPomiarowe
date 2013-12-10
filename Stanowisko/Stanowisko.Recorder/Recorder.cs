using System.IO;
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
        private uint _period = 1000;
        private List<Sample> _samples;
        private bool _isRecording = false;
        private bool _isConnected = false;
        private IMeasuringDevice _measuringDevice;
        private System.Timers.Timer _timer;
        private RecorderWindow _window;
        #endregion

        #region Private Methods

        private delegate void AddSampleToChartDelegate(Sample sample);

        private void addSampleToChart(Sample sample)
        {
            _window.AddSampleToChart(sample);
        }
        
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Sample sample = _measuringDevice.GetSample();

            if (_isRecording)
            {
                _samples.Add(sample);
            }

            _window.BeginInvoke(new AddSampleToChartDelegate(addSampleToChart), sample);
        }
        #endregion

        #region Constructors
        public Recorder(IMeasuringDevice measuringDevice)
        {
            this._measuringDevice = measuringDevice;
            _timer = new System.Timers.Timer(_period);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _samples = new List<Sample>();
            _window = new RecorderWindow(this);

            if (ConnectWithDevice())
            {
                return;
            }

            _window.Show();
            _timer.Start();
        }

        public Recorder(IMeasuringDevice measuringDevice, uint period)
            : this(measuringDevice)
        {
            if (period <= 0)
            {
                throw new InvalidDataException();
            }
            this._period = period;
        }

        private bool ConnectWithDevice()
        {
            string errorMessage = _measuringDevice.StartConnection();
            if (errorMessage != null)
            {
                MessageBox.Show(errorMessage);
                return true;
            }
            _isConnected = true;
            return false;
        }

        #endregion

        #region Public Methods
        public void startRecording()
        {
            if (!_isConnected)
            {
                MessageBox.Show("Polaczenie z urzadzeniem zostalo zakonczone");
                return;
            }
            _samples.Clear();
            _isRecording = true;

        }

        public void stopRecording()
        {
            _isRecording = false;
        }

        public Measurement getRecording()
        {
            Measurement measurement = new Measurement(_samples);
            return measurement;
        }

        public void disconnect()
        {
            if (_isRecording)
            {
                stopRecording();
            }
            _timer.Stop();
            _measuringDevice.StopConnection();
            _isConnected = false;
        }
        #endregion
    }
}
