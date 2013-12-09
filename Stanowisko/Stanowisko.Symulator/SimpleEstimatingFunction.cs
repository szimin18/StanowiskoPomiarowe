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

        public override double GetValue(double miliseconds, long sapmleInsertionDelay, long initialValue, long experimentDuration, long amplitude)
        {
            if (miliseconds < sapmleInsertionDelay || miliseconds > sapmleInsertionDelay + experimentDuration)
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
