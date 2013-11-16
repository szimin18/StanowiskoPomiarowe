using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    public static class Exporter
    {

        private static List<IMeasurementExporter> _measurementExporters;

        static Exporter()
        {
            _measurementExporters = new List<IMeasurementExporter>();
            _measurementExporters.Add(new MockMeasurementExporter("Txt", "*.txt"));
        }

        public static void ExportMeasurement(Measurement measurement)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            String filter = "";
            
            foreach (IMeasurementExporter exporter in _measurementExporters) 
            {
                filter += exporter.TypeName + "|" + exporter.TypeExtension + "|";
            }
            saveFileDialog1.Filter = filter.TrimEnd(new char[] { '|' });
            saveFileDialog1.Title = "Export measurement";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                fs.Close();
            }
        }

        public static void ExportExperiment(Experiment experiment)
        {
        }
    }
}
