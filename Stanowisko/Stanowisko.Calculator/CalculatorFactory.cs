using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    public class CalculatorFactory : ICalculatorFactory
    {
        public IMeasurementCalculator CreateCalculator(Measurement measurement, IntegratingModuleType integratorType)
        {
            if (integratorType == IntegratingModuleType.Simpsons)
            {
                return new MeasurementCalculator(measurement, new SimpsonsIntegratingModule());
            }
            else if (integratorType == IntegratingModuleType.Trapezoidal)
            {
                return new MeasurementCalculator(measurement, new TrapezoidalIntegratingModule());
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
