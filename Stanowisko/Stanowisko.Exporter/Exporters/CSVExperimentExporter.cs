using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter.Exporters
{
    internal class CSVExperimentExporterFactory : ExporterFactory<Experiment>
    {
        private static readonly string _fileType = "Comma Separated Values";
        private static readonly string _fileExtension = "*.csv";

        public CSVExperimentExporterFactory()
            : base(_fileType, _fileExtension, typeof(CSVExperimentExporter))
        {
        }

        internal class CSVExperimentExporter : Exporter<Experiment>
        {
            public CSVExperimentExporter()
                : base(_fileType, _fileExtension)
            {
            }

            public override void ExportToFile(Experiment experiment)
            {
                try
                {
                    FileStream fileStream = new FileStream(OutputFileName, FileMode.Create);
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
                }
            }
        }
    }
}
