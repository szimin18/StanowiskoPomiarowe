using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    interface IMeasurements
    {
        void Add(Measurement measurement, Experiment experiment);

        void Update(Measurement measurement, Experiment experiment);
    }
}
