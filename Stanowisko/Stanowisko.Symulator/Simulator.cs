﻿using Stanowisko.SharedClasses;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Stanowisko.Symulator
{
    public class Simulator : IMeasuringDevice
    {
        #region Private Variables
        private IEstimatingFunction _sEstimatingFunction;
        private IEstimatingFunction _selectedEstimatingFunction;
        private MainWindow _settingsWindow;
        private long _sampleInsertionDelay;
        private long _initialValue;
        private long _experimentDuration;
        private long _amplitude;
        #endregion

        #region private classes
        private class RelayCommand : ICommand
        {
            Action _action;

            public RelayCommand(Action execute)
            {
                _action = execute;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }
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
            get { return _sEstimatingFunction; }
            set
            {
                if (_sEstimatingFunction != value)
                {
                    _sEstimatingFunction = value;
                    //RaisePropertyChanged("SelectedRank");
                }
            }
        }
        private ICommand CancelCommand
        {
            get { return new RelayCommand(Cancel); }
        }
        private ICommand ApplyCommand
        {
            get { return new RelayCommand(Apply); }
        }
        #endregion

        #region Consructors
        public Simulator()
        {
            _selectedEstimatingFunction = SelectedEstimatingFunction = EstimatingFunctionFactory.Simple;
            _sampleInsertionDelay = SampleInsertionDelay = 1000;
            _experimentDuration = ExperimentDuration = 2000;
            _amplitude = Amplitude = 1;
            _initialValue = InitialValue = 1;
        }
        #endregion

        #region Private Methods
        private void Apply()
        {
            if (IsConnected)
            {
                MessageBox.Show("Cannot modify existing connection", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _selectedEstimatingFunction = SelectedEstimatingFunction;
                _sampleInsertionDelay = SampleInsertionDelay;
                _experimentDuration = ExperimentDuration;
                _amplitude = Amplitude;
                _initialValue = InitialValue;
            }
        }

        private void Cancel()
        {
            _settingsWindow.Close();
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
                return new Sample(SelectedEstimatingFunction.GetValue(totalMiliseconds, _sampleInsertionDelay, _initialValue, _experimentDuration, _amplitude), totalMiliseconds);
            }
            else
            {
                throw new Exception();
            }
        }

        public void ShowSettingsWindow()
        {
            _settingsWindow = new MainWindow(this);
            _settingsWindow.ShowDialog();
        }
        #endregion
    }
}