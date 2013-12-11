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
        private uint period = 1000;
        private List<Sample> samples;
        private bool isRecording = false;
        private bool isConnected = false;
        private IMeasuringDevice measuringDevice;
        private System.Timers.Timer timer;
        private RecorderWindow window;
        #endregion

        #region Private Methods

        private delegate void AddSampleToChartDelegate(Sample sample);

        private void addSampleToChart(Sample sample)
        {
            window.AddSampleToChart(sample);
        }
        
        private void timerElapsed(object sender, ElapsedEventArgs e)
        {
            Sample sample = measuringDevice.GetSample();

            if (isRecording)
            {
                samples.Add(sample);
            }

            window.BeginInvoke(new AddSampleToChartDelegate(addSampleToChart), sample);
        }
        #endregion

        #region Constructors
        public Recorder()
        {
        }

        public Recorder(IMeasuringDevice measuringDevice)
        {
            this.measuringDevice = measuringDevice;
            timer = new System.Timers.Timer(period);
            timer.Elapsed += new ElapsedEventHandler(timerElapsed);
            samples = new List<Sample>();
            window = new RecorderWindow(this);

            if (ConnectWithDevice())
            {
                throw new Exception();
            }

            window.Show();
            timer.Start();
        }

        private bool ConnectWithDevice()
        {
            string errorMessage = measuringDevice.StartConnection();
            if (errorMessage != null)
            {
                MessageBox.Show(errorMessage);
                return true;
            }
            isConnected = true;
            return false;
        }

        #endregion

        #region Public Methods
        public void startRecording()
        {
            if (!isConnected)
            {
                MessageBox.Show("Polaczenie z urzadzeniem zostalo zakonczone");
                return;
            }
            samples.Clear();
            isRecording = true;

        }

        public void stopRecording()
        {
            isRecording = false;
        }

        public Measurement getRecording()
        {
            return new Measurement(samples);
        }

        public void connectWithDevice(IMeasuringDevice device)
        {
            throw new NotImplementedException();
        }

        public void disconnectWithDevice()
        {
            if (isRecording)
            {
                stopRecording();
            }
            timer.Stop();
            measuringDevice.StopConnection();
            isConnected = false;
        }
        #endregion
    }
}
