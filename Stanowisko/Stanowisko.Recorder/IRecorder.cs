using Stanowisko.SharedClasses;

namespace Rejestrator
{
    interface IRecorder
    {
        void startRecording();
        void stopRecording();
        Measurement getRecording();
        void setPeriod(uint period);
    }
}