using Stanowisko.SharedClasses;

namespace Stanowisko.Recorder
{
    public interface IRecorder
    {
        void startRecording();
        void stopRecording();
        Measurement getRecording();
        void disconnect();
    }
}