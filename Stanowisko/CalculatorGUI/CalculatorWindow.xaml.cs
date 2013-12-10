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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

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

    }
}
