using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.Calculator
{
    class IntegratingModuleCreator : IIntegratingModuleCreator
    {
        TrapezoidalIntegratingModule trapezoidal = null;
        SimpsonsIntegratingModule simpsons = null;

        public IIntegratingModule TrapezoidalIntegratingModuleCreate()
        {
            if (trapezoidal == null)
                trapezoidal = new TrapezoidalIntegratingModule();
            return trapezoidal;
        }

        public IIntegratingModule SimpsonsIntegratingModuleCreate()
        {
            if (simpsons == null)
                simpsons = new SimpsonsIntegratingModule();
            return simpsons;
        }
    }
}
