using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;
using Stanowisko.Symulator;

namespace Rejestrator
{
    class Recorder : IRecorder
    {
        #region Private Member Variables
        private const uint defaultPeriod = 1000;
        private uint period;
        private IMeasuringDevice measuringDevice;
        private bool isRecording;
        private Measurement currentMeasurement;
        #endregion

        #region Constructors
        public Recorder(IMeasuringDevice measuringDevice)
        {
            this.measuringDevice = measuringDevice;
            period = defaultPeriod;
        }
        #endregion

        #region Public Methods
        public void startRecording()
        {
        }

        public void stopRecording()
        {
        }

        public void setPeriod(uint period)
        {
            this.period = period;
        }
        #endregion
    }
}
