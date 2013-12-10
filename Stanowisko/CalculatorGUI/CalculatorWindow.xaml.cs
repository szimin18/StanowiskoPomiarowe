using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Stanowisko.Persistance;
using Stanowisko.Calculator;
using Stanowisko.SharedClasses;

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

        private IIntegratingModule algoritm;
        private IMeasurementCalculator calculator;
        private string CalibText;

        public MainWindow()
        {
            InitializeComponent();
            experiments = persistence.GetAllExperiments();
            foreach (Experiment exp in experiments)
            {
                this.ExperimentComboBox.Items.Add(exp.Name);
            }
            measurement = null;
            algoritm = null;

            AlgoritmComboBox.Items.Add("Metoda Trapezow");
            AlgoritmComboBox.Items.Add("Metoda Simpsoma");

            CalibText = null;
        }

        private void ExperimentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ExperimentComboBox.SelectedItem.ToString();
            foreach (Experiment exp in experiments)
            {
                if (exp.Name.Equals(text))
                {
                    measurements = exp.GetMeasurements();
                    break;
                }
            }

            foreach (Measurement meas in measurements)
            {
                this.MeasurementComboBox.Items.Add(meas.Id.ToString());
            }

        }

        private void MeasurementComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = int.Parse(MeasurementComboBox.SelectedItem.ToString());
            foreach (Measurement meas in measurements)
            {
                if (id == meas.Id)
                {
                    measurement = meas;
                    break;
                }
            }

            if (algoritm == null)
            {
                calculator = new MeasurementCalculator(measurement);
            }
            else
            {
                calculator = new MeasurementCalculator(measurement, algoritm);
            }
            Slicer1.Value = this.calculator.CurveBeginning;
            Slicer2.Value = this.calculator.CurveEnd;
            this.calculator.Coefficent = 1;
        }

        private void AlgoritmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = AlgoritmComboBox.SelectedItem.ToString();
            if (text.Equals("Metoda Trapezow"))
            {
                algoritm = new TrapezoidalIntegratingModule();
            }
            else if (text.Equals("Metoda Simpsona"))
            {
                algoritm = new SimpsonsIntegratingModule();
            }

            if (measurement != null)
            {
                calculator = new MeasurementCalculator(measurement, algoritm);
            }
        }

        private void Slicer1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
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
               //this.calculator.Coefficent = this.calculator.Calibrate(double.Parse(CalibText));
            }
        }

        private void Calibration_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalibText = Calibration.Text;
        }

        private void Oblicz_Click(object sender, RoutedEventArgs e)
        {
            //Wynik.Text = this.calculator.CalculateHeat().ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
