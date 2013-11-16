using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    class CSVExperimentExporter : IExperimentExporter
    {
        public CSVExperimentExporter()//string typeName, string typeExtesion)
        {
            //TypeName = typeName;
            //TypeExtension = typeExtesion;
        }

        public bool Export(FileStream fileStream, Experiment experiment)
        {
            return true;
        }

        public string TypeName { get; private set; }
        public string TypeExtension { get; private set; }
    }
}
