using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    interface ISamples
    {
        void Add(Sample sample, Measurement measurement);
        
        List<Sample> GetAll(Measurement m);
    }
}
