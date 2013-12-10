using System;

namespace Stanowisko.Symulator
{
    class SimpleEstimatingFunction : IEstimatingFunction
    {
        public String Name
        {
            get
            {
                return "Simple Estimating Function";
            }
        }

        public double GetValue(double miliseconds, long sampleInsertionDelay, long initialValue, long experimentDuration, long amplitude)
        {
            if (miliseconds < sampleInsertionDelay || miliseconds > sampleInsertionDelay + experimentDuration)
            {
                return initialValue;
            }
            else
            {
                return (miliseconds - experimentDuration) * (experimentDuration - miliseconds) / 1000000 + initialValue + amplitude;
            }
        }
    }
}
