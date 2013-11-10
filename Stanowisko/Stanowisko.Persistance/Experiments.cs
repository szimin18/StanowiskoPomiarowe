using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Experiments : DAO
    {
        public Experiments(DBConnection connection) : base(connection)
        {
            
        }

        public void Update(Experiment e)
        {
            throw new NotImplementedException();
        }

        public Experiment Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
