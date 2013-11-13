using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.Calculator
{
    public interface IMeasurementCalculator
    {
        void Calibrate(double heat);
        double CalculateHeat();

    }
}
