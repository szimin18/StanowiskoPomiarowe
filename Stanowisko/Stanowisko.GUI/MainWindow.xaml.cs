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

using Stanowisko.Exporter;
using Stanowisko.SharedClasses;

namespace Stanowisko.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Sample s1 = new Sample(1, 1.23, 5.12);
            Sample s2 = new Sample(2, 2.34, 6.03);
            Sample s3 = new Sample(3, 3.45, 6.77);
            Measurement tmpMeasurement = new Measurement(1);
            tmpMeasurement.Add(new List<Sample>() { s1, s2, s3 });
            tmpMeasurement.Result = 3.14;
            Exporter.Exporter.ExportMeasurement(tmpMeasurement);
        }


    }


}
