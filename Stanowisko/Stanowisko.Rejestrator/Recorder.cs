using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Rejestrator
{
    interface Recorder
    {
        void startRecording();
        void stopRecording();
        Measurement getRecording();
        void setPeriod(uint period);
    }
}