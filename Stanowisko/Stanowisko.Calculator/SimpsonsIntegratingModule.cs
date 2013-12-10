using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    public class SimpsonsIntegratingModule : IIntegratingModule
    {
        public double Integrate(List<Sample> samples, int beggining, int end)
        {
            if (end - beggining > 2 && end < samples.Count && beggining >= 0)
            {
                double value = 0.0;

                for (int i = beggining; i < end - 1; i = i + 2)
                {
                    value += (samples.ElementAt(i).Value + 4 * samples.ElementAt(i + 1).Value + samples.ElementAt(i + 2).Value);
                }
                value *= (samples.ElementAt(1).Time - samples.ElementAt(0).Time) / 3;

                return value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}