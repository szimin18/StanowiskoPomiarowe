using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    class ICalculatorFactory
    {
        public IMeasurementCalculator CreateCalculator(Measurement measurement, IIntegratingModule integrator);
    }
}
