using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    class CalculatorFactory : ICalculatorFactory
    {
        public IMeasurementCalculator CreateCalculator(Measurement measurement, IIntegratingModule integrator)
        {
            return new MeasurementCalculator(measurement, integrator);
        }
    }
}
