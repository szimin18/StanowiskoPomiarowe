using Stanowisko.SharedClasses;
using Stanowisko.Symulator;

namespace Stanowisko.Recorder
{
    public interface IRecorder
    {
        void connectWithDevice(IMeasuringDevice device);
        void disconnectWithDevice();
        void startRecording();
        void stopRecording();
        Measurement getRecording();
    }
}