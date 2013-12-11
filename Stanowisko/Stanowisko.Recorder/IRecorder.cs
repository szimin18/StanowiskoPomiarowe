using Stanowisko.SharedClasses;
using Stanowisko.Symulator;

namespace Stanowisko.Recorder
{
    public interface IRecorder
    {
        void startRecording();
        void stopRecording();
        Measurement getRecording();
        bool recording();
        void disconnectDevice();
    }
}