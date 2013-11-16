using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    interface ISamples
    {
        void Add(Sample sample, Measurement measurement);
        
        List<Sample> GetAll(Measurement m);

    }
}
