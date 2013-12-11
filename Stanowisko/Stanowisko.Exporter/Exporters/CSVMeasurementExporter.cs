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
    internal class CSVMeasurementExporterFactory : ExporterFactory<Measurement>
    {
        private static readonly string _fileType = "Comma separated values";
        private static readonly string _fileExtension = ".csv";

        public CSVMeasurementExporterFactory()
            : base(_fileType, _fileExtension, typeof(CSVMeasurementExporter))
        {
        }

        internal class CSVMeasurementExporter : Exporter<Measurement>
        {
            public CSVMeasurementExporter()
                : base(_fileType, _fileExtension)
            {
            }

            protected override void ExportToFile(Measurement measurement)
            {
                try
                {
                    FileStream fileStream = new FileStream(OutputFileName, FileMode.Create);
                    StreamWriter streamWriter = new StreamWriter(fileStream);

                    streamWriter.WriteLine("Id," + measurement.Id.ToString(CultureInfo.InvariantCulture));
                    streamWriter.WriteLine("Result," + measurement.Result.ToString(CultureInfo.InvariantCulture));
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Id,Time,Value");
                    foreach (Sample sample in measurement.GetSamples())
                    {
                        streamWriter.WriteLine(sample.Id.ToString(CultureInfo.InvariantCulture) +
                            "," + sample.Time.ToString(CultureInfo.InvariantCulture) +
                            "," + sample.Value.ToString(CultureInfo.InvariantCulture));
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
