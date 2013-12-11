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
        private uint period = 1000;
        private List<Sample> samples;
        private bool isRecording = false;
        private bool isConnected = false;
        private IMeasuringDevice measuringDevice;
        private System.Timers.Timer timer;
        private RecorderWindow window;

        public Recorder(IMeasuringDevice device)
        {
            measuringDevice = device;
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

        public void startRecording()
        {
            if (!isConnected)
            {
                MessageBox.Show("Brak polaczenia z urzadzeniem pomairowym.");
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

        public bool recording()
        {
            return isRecording;
        }

        public void disconnectDevice()
        {
            if (isRecording)
            {
                stopRecording();
            }
            timer.Stop();
            measuringDevice.StopConnection();
            isConnected = false;
        }

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

    }
}
