﻿using Stanowisko.SharedClasses;
using System;
using System.Collections.Generic;

namespace Stanowisko.Symulator
{
    public class Simulator : IMeasuringDevice
    {
        #region Private Variables
        private IEstimatingFunction _selectedEstimatingFunction;
        #endregion

        #region Private Properties
        private DateTime StartingTime { set; get; }
        private bool IsConnected { set; get; }

        private long SampleInsertionDelay { set; get; }
        private long InitialValue { set; get; }
        private long ExperimentDuration { set; get; }
        private long Amplitude { set; get; }
        private IEnumerable<IEstimatingFunction> EstimatingFunctionList
        {
            get
            {
                return (IEnumerable<IEstimatingFunction>)EstimatingFunctionFactory.EstimatingFunctionsList;
            }
        }
        private IEstimatingFunction SelectedEstimatingFunction
        {
            get { return _selectedEstimatingFunction; }
            set
            {
                if (_selectedEstimatingFunction != value)
                {
                    _selectedEstimatingFunction = value;
                    //RaisePropertyChanged("SelectedRank");
                }
            }
        }
        #endregion

        #region Consructors
        public Simulator()
        {
            SelectedEstimatingFunction = EstimatingFunctionFactory.Simple;
            SampleInsertionDelay = 1000;
            ExperimentDuration = 2000;
            Amplitude = 1;
            InitialValue = 1;
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
                return new Sample(SelectedEstimatingFunction.GetValue(totalMiliseconds, SampleInsertionDelay, InitialValue, ExperimentDuration, Amplitude), totalMiliseconds);
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