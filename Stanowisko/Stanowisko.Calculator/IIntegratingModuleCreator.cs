using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.Calculator
{
    interface IIntegratingModuleCreator
    {
        public TrapezoidalIntegratingModule TrapezoidalIntegratingModuleCreate();
        public SimpsonsIntegratingModule SimpsonsIntegratingModuleCreate();
    }
}
