using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Symulator
{
    public interface IMeasuringDevice
    {
        string StartConnection();
        void StopConnection();
        Sample GetSample();
        void ShowSettingsWindow();
    }
}