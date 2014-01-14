using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Stanowisko.Persistance;
using Stanowisko.Calculator;
using Stanowisko.SharedClasses;
using Stanowisko.Exporter;
using Stanowisko.Exporter.Forms;

namespace CalculatorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IPersistenceManager persistence = PersistenceFactory.GetPersistenceManager();
        private List<Experiment> experiments;
        private Experiment experiment;
        private List<Measurement> measurements;
        private Measurement measurement;

        private IntegratingModuleType algoritm;
        ICalculatorFactory factory;
        IMeasurementCalculator calculator;
        private string CalibText;

        private double maxSlicer;
        private double minSlicer;

        public MainWindow()
        {
            InitializeComponent();
            experiments = persistence.GetAllExperiments();
            this.ExperimentComboBox.Items.Clear();
            foreach (Experiment exp in experiments)
            {
                this.ExperimentComboBox.Items.Add(exp.Name);
            }
            measurement = null;

            AlgoritmComboBox.Items.Add("Metoda Trapezow");
            AlgoritmComboBox.Items.Add("Metoda Simpsona");

            SaveMeasurementButton.IsEnabled = false;
            SaveExperimentButton.IsEnabled = false;

            CalibText = null;
        }

        private void ExperimentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ExperimentComboBox.SelectedItem.ToString();
            foreach (Experiment exp in experiments)
            {
                if (exp.Name.Equals(text))
                {
                    experiment = exp;
                    measurements = exp.GetMeasurements();
                    break;
                }
            }

            SaveExperimentButton.IsEnabled = true;

            this.MeasurementComboBox.Items.Clear();
            foreach (Measurement meas in measurements)
            {
                this.MeasurementComboBox.Items.Add(meas.Id.ToString());
            }

        }

        private void MeasurementComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = 0;
            if(MeasurementComboBox.SelectedItem != null)
                id = int.Parse(MeasurementComboBox.SelectedItem.ToString());
            foreach (Measurement meas in measurements)
            {
                if (id == meas.Id)
                {
                    measurement = meas;
                    break;
                }
            }

            SaveMeasurementButton.IsEnabled = true;
            if (algoritm != null)
            {
                factory = new CalculatorFactory();
                calculator = factory.CreateCalculator(measurement, algoritm);
            }
            this.calculator.InitializeBoundaries();
            minSlicer = this.calculator.CurveBeginning;
            maxSlicer = this.calculator.CurveEnd;
            Slicer1.Value = this.calculator.CurveBeginning;
            Slicer2.Value = this.calculator.CurveEnd;

            Console.WriteLine(maxSlicer);
            Console.WriteLine(minSlicer);
        }

        private void AlgoritmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = AlgoritmComboBox.SelectedItem.ToString();
            if (text.Equals("Metoda Trapezow"))
            {
                algoritm = IntegratingModuleType.Trapezoidal;
            }
            else if (text.Equals("Metoda Simpsona"))
            {
                algoritm = IntegratingModuleType.Simpsons;
            }

            if (measurement != null)
            {
                factory = new CalculatorFactory();
                calculator = factory.CreateCalculator(measurement, algoritm);
            }
        }

        private void Slicer1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Slicer1.Value > maxSlicer)
            {
                Slicer1.Value = maxSlicer;
            }
            else if (Slicer1.Value < minSlicer)
            {
                Slicer1.Value = minSlicer;
            }

            int var = (int)Slicer1.Value;
            if (Slicer1.Value > Slicer2.Value)
            {
                this.calculator.CurveEnd = var;
            }
            else
            {
                this.calculator.CurveBeginning = var;
            }
        }

        private void Slicer2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Slicer2.Value > maxSlicer)
            {
                Slicer2.Value = maxSlicer;
            }
            else if (Slicer2.Value < minSlicer)
            {
                Slicer2.Value = minSlicer;
            }

            int var = (int)Slicer2.Value;
            if (Slicer2.Value > Slicer1.Value)
            {
                this.calculator.CurveEnd = var;
            }
            else
            {
                this.calculator.CurveBeginning = var;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (Calibration.Text != null)
            {
               this.calculator.Coefficent = this.calculator.Calibrate(double.Parse(CalibText));
            }
        }

        private void Calibration_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalibText = Calibration.Text;
            if (Kalibracja.IsChecked.Value && Calibration.Text != null)
            {
                this.calculator.Coefficent = this.calculator.Calibrate(double.Parse(CalibText));
            }
        }

        private void Oblicz_Click(object sender, RoutedEventArgs e)
        {
            if (!Kalibracja.IsChecked.Value)
            {
                this.calculator.Coefficent = 1;
            }
           Wynik.Text = this.calculator.CalculateHeat().ToString();
           //this.LineChart.Series[0] = this.MyChart;
           //foreach (Sample sample in measurement.GetSamples())
           //{
           //    this.LineChart.Series["Ciepło"].Points.AddXY(sample.Value, sample.Time)
           //}
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveExperiment(object sender, RoutedEventArgs e)
        {
            if (experiment != null)
            {
                ExperimentExporterDialog dialog = new ExperimentExporterDialog();
                dialog.ExporterFilter = ExporterFactory<Experiment>.GetAllExporters();
                dialog.Dialog.ShowDialog();
                Exporter<Experiment> exporter = dialog.GetExporter();
                if (exporter != null)
                {
                    exporter.Export(experiment);
                }
            }
        }

        private void SaveMeasurement(object sender, RoutedEventArgs e)
        {
            if (measurement != null)
            {
                MeasurementExporterDialog dialog = new MeasurementExporterDialog();
                dialog.ExporterFilter = ExporterFactory<Measurement>.GetAllExporters();
                dialog.Dialog.ShowDialog();
                Exporter<Measurement> exporter = dialog.GetExporter();
                if (exporter != null)
                {
                    exporter.Export(measurement);
                }
            }
        }
        private void Chart_Changed(object sender, RoutedEventArgs e)
        {

        }

    }
}
