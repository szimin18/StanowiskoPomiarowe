﻿using Stanowisko.SharedClasses;
using System;

namespace Stanowisko.Symulator
{
    public class Simulator : IMeasuringDevice
    {
        #region Private Member Variables
        private DateTime _startingTime;
        private bool _isConnected = false;
        #endregion

        #region Private Properties
        private DateTime StartingTime
        {
            set { _startingTime = value; }
            get { return _startingTime; }
        }

        private bool IsConnected
        {
            set { _isConnected = value; }
            get { return _isConnected; }
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
            if (IsConnected)
            {
                return "Połączenie już istnieje";
            }
            else
            {
                IsConnected = true;
                StartingTime = DateTime.Now;
                return null;
            }
        }

        public void StopConnection()
        {
            IsConnected = false;
        }

        public Sample GetSample()
        {
            if (IsConnected)
            {
                DateTime currentTime = DateTime.Now;
                return new Sample(SimulatingFunction((currentTime - StartingTime).TotalMilliseconds), currentTime);
            }
            else
            {
                throw new Exception();
            }
        }

        public void ShowSettingsWindow()
        {

        }
        #endregion
    }
}