using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    public interface ICalculatorFactory
    {
        IMeasurementCalculator CreateCalculator(Measurement measurement, IntegratingModuleType integratorType);
    }
}
