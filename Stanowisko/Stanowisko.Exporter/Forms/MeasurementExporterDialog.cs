using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter.Forms
{
    class MeasurementExporterDialog : ExporterDialog<Measurement>
    {
        public override Measurement Exportee
        {
            get { return null; }
            set { }
        }
    }
}
