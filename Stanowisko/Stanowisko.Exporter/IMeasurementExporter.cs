using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    interface IMeasurementExporter
    {
        bool Export(SaveFileDialog saveFileDialog, Measurement measurement);
        string TypeName { get; }
        string TypeExtension { get; }
    }
}
