using Stanowisko.SharedClasses;
using Stanowisko.Symulator;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Rejestrator
{
    class Recorder : IRecorder
    {
        #region Private Member Variables
        private const uint _defaultPeriod = 1000;
        private uint _period;
        private List<Sample> _samples;
        private bool _isRecording;
        private IMeasuringDevice _measuringDevice;
        private Timer _timer;
        #endregion

        #region Private Methods
        private void _getSample()
        {
            _samples.Add(_measuringDevice.GetSample());
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
            _period = _defaultPeriod;
            _timer = new Timer(_period);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _samples = new List<Sample>();
        }
        #endregion

        #region Public Methods
        public void startRecording()
        {
            _isRecording = true;
            _timer.Start();
        }

        public void stopRecording()
        {
            _timer.Stop();
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
