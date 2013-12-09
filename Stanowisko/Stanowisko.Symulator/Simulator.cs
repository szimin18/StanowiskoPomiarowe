﻿using Stanowisko.SharedClasses;
using System;

namespace Stanowisko.Symulator
{
    public class Simulator : IMeasuringDevice
    {
        #region Private Properties
        private IEstimatingFunction EstimatingFunction { get; }
        #endregion

        #region Private Properties
        private DateTime StartingTime { set; get; }
        private bool IsConnected { private set; get; }

        private long SapmleInsertionDelay { set; get; }
        private long InitialValue { set; get; }
        private long ExperimentDuration { set; get; }
        private long Amplitude { set; get; }
        private IEstimatingFunction EstimatingFunctionList
        {

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
                double totalMiliseconds = (currentTime - StartingTime).TotalMilliseconds;
                return new Sample(EstimatingFunction.GetValue(totalMiliseconds, SapmleInsertionDelay, InitialValue, ExperimentDuration, Amplitude), totalMiliseconds);
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