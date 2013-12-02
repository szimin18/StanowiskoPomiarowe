using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.IO;
using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    class CSVExperimentExporter : IExperimentExporter
    {
        public CSVExperimentExporter()
        {
            TypeName = "CSV";
            TypeExtension = "*.csv";
        }

        public bool Export(SaveFileDialog saveFileDialog, Experiment experiment)
        {
            try
            {
                System.IO.FileStream fileStream =
                    (System.IO.FileStream)saveFileDialog.OpenFile();
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Id," + experiment.Id.ToString(CultureInfo.InvariantCulture));
                streamWriter.WriteLine("Name," + experiment.Name);
                streamWriter.WriteLine("Description," + experiment.Description);
                streamWriter.WriteLine("Goal," + experiment.Goal);
                streamWriter.WriteLine("Result," + experiment.Result.ToString(CultureInfo.InvariantCulture));
                streamWriter.WriteLine("Summary," + experiment.Summary);
                streamWriter.WriteLine();
                streamWriter.WriteLine("Key,Value");
                foreach (string key in experiment.GetParameters().Keys)
                {
                    streamWriter.WriteLine(key + experiment.GetParameters()[key]);
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine("Id,Result");
                foreach (Measurement measurement in experiment.GetMeasurements())
                {
                    streamWriter.WriteLine(measurement.Id.ToString(CultureInfo.InvariantCulture) +
                        "," + measurement.Result.ToString(CultureInfo.InvariantCulture));
                }
                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                return false;
            }
            return true;
        }

        public string TypeName { get; private set; }
        public string TypeExtension { get; private set; }
    }
}
