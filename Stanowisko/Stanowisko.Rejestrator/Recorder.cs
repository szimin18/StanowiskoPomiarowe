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
        public void startRecording();
        public void stopRecording();
        public Measurement getRecording();
        public void setPeriod(uint period);
    }
}
