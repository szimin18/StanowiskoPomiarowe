using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.Calculator
{
    public interface IMeasurementCalculator
    {
        double Coefficent 
        { 
            set; 
            get; 
        }
        int CurveBeginning
        {
            set;
            get;
        }
        int CurveEnd
        {
            set;
            get;
        }
        Tuple<int, int> GetBoundaries();
        void SetBoundaries(Tuple<int, int> boundaries);
        void InitializeBoundaries();
        double Calibrate(double heat);
        double CalculateHeat();
    }
}
