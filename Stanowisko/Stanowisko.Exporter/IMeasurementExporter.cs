using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    interface IMeasurementExporter
    {
        void Export(Measurement measurement);
    }
}
