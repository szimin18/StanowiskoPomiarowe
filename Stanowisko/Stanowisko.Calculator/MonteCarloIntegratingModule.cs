using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    class MonteCarloIntegratingModule
    {
        public double Integrate(List<Sample> samples, int beggining, int end)
        {
            if (end - beggining >= 0 && end < samples.Count && beggining >= 0)
            {
                double value = 0.0;

                Random rnd = new Random();

                for (int i = beggining; i <= end; ++i)
                {
                    value += samples.ElementAt(rnd.Next(beggining,end)).Value;
                }
                value *= (samples.ElementAt(1).Time - samples.ElementAt(0).Time) / (end-beggining);

                return value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
