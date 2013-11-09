﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Symulator
{
    class Simulator : IMeasuringDevice
    {
        private long _time;

        public string StartConnection()
        {
            _time = System.DateTime.Now.Millisecond;
            return null;
        }

        public void StopConnection()
        {

        }

        public Sample GetSample()
        {
            return null;
        }

        public void ShowSettingsWindow()
        {

        }
    }
}