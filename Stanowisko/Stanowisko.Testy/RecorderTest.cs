using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.Recorder;
using Stanowisko.Symulator;
using Stanowisko.SharedClasses;
using System.Threading;

namespace Stanowisko.Testy
{
    [TestClass]
    public class RecorderTest
    {
        class MockMeasuringDevice : IMeasuringDevice
        {
            private bool IsConnected { set; get; }
            private double millis = 0.0; 
            public string StartConnection()
            {
                if (IsConnected)
                    return "Already connected";
                else
                {
                    IsConnected = true;
                    return null;
                }
            }
            void StopConnection()
            {
                IsConnected = false;
            }
            Sample GetSample()
            {
                millis += 1.0;
                return new Sample(0.0, millis);
            }
            void ShowSettingsWindow() { }
        }
        private IRecorder _recorder;

        [TestInitialize]
        public void SetUp()
        {
            _recorder = new Recorder.Recorder(new Simulator());
        }

        [TestMethod]
        public void RecordingTest()
        {
            _recorder.setPeriod(1);
            _recorder.startRecording();
            Thread.Sleep(10);
            _recorder.stopRecording();
            var result = _recorder.getRecording();
            Assert.IsNotNull(result);
        }
    }
}
