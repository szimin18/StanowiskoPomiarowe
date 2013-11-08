using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedClasses;

namespace Stanowisko.Symulator
{
    interface IMeasuringDevice
    {
        string StartConnection();
        void StopConnection();
        Sample Sampling();
        void ChangeSettings();
    }
}