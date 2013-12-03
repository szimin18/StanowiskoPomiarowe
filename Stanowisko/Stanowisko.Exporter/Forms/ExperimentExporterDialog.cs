using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter.Forms
{
    class ExperimentExporterDialog : ExporterDialog<Experiment>
    {
        public override Experiment Exportee
        {
            get { return null; }
            set { }
        }
    }
}
