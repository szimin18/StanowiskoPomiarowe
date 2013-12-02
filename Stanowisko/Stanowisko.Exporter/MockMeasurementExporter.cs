using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    class MockMeasurementExporter : IMeasurementExporter
    {
        public MockMeasurementExporter(string typeName, string typeExtesion)
        {
            TypeName = typeName;
            TypeExtension = typeExtesion;
        }

        public bool Export(SaveFileDialog saveFileDialog, Measurement measurement)
        {
            return true;
        }
        public string TypeName { get; private set; }
        public string TypeExtension { get; private set; }
    }
}
