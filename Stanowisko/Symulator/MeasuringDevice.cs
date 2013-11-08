using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedClasses;
///////////////////////////////////////////////////////////////////////////////////////////////ssadassdgdfgververvrdvrdxfdfvrdvrrareegsadfsdf
namespace Symulator
{
    interface MeasuringDevice
    {
        string StartConnection();
        void StopConnection();
        Sample Sampling();
        void ChangeSettings();
    }
}