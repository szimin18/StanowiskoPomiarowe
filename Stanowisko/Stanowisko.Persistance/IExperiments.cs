using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    interface IExperiments
    {
        void Add(Experiment e);

        void Update(Experiment e);

        List<Experiment> GetAll();

    }


}
