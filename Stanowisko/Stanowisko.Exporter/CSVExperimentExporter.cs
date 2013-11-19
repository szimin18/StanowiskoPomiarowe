using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Export(FileStream fileStream, Experiment experiment)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Name," + experiment.Name);
                streamWriter.WriteLine("Goal," + experiment.Goal);
                streamWriter.WriteLine("Result," + experiment.Result.ToString(CultureInfo.InvariantCulture));
                streamWriter.WriteLine("Description," + experiment.Description);
                streamWriter.WriteLine("Summary," + experiment.Summary);
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
