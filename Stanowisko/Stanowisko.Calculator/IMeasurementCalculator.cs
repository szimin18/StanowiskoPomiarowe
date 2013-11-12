using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    interface IMeasurementCalculator
    {
        private double Integration();
        private void initializeBoundaries();
        public void Calibrate(double heat);
        public double CalculateHeat();

    }
}
