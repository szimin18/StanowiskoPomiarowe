using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    public class TrapezoidalIntegratingModule : IIntegratingModule
    {
        public double Integrate(List<Sample> samples, int beggining, int end)
        {
            if (samples.Count > 1)
            {
                double value = 0.0;

                for (int i = beggining; i < end; ++i)
                {
                    value += (samples.ElementAt(i).Value + samples.ElementAt(i + 1).Value);
                }
                value *= (samples.ElementAt(1).Time - samples.ElementAt(0).Time) / 2;

                return value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
