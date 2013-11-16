using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    interface IExperiments
    {
        void Add(Experiment e);

        void Update(Experiment e);

        Experiment Get(int id);
    }


}
