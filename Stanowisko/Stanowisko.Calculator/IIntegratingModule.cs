using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Calculator
{
    public interface IIntegratingModule
    {
        double Integrate(List<Sample> samples, int beggining, int end);
    }
}
