﻿using Stanowisko.SharedClasses;
using System;

namespace Stanowisko.Symulator
{
    public class Simulator : IMeasuringDevice
    {
        #region Private Member Variables
        private DateTime _startingTime;
        #endregion

        #region Private Properties
        private DateTime StartingTime
        {
            set { _startingTime = value; }
            get { return _startingTime; }
        }
        #endregion

        #region Private Methods
        private double SimulatingFunction(double miliseconds)
        {
            return 0;
        }
        #endregion

        #region Consructors
        public Simulator()
        {

        }
        #endregion

        #region Public Methods
        public string StartConnection()
        {
            StartingTime = DateTime.Now;
            return null;
        }

        public void StopConnection()
        {

        }

        public Sample GetSample()
        {
            DateTime currentTime = DateTime.Now;
            return new Sample(SimulatingFunction((currentTime - StartingTime).TotalMilliseconds), currentTime);
        }

        public void ShowSettingsWindow()
        {

        }
        #endregion
    }
}