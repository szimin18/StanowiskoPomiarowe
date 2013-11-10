using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Measurements : DAO
    {
        public Measurements(DBConnection connection) : base(connection)
        {
        }

        public void Remove(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public void RemoveSamples(Measurement measurement, IEnumerable<int> range)
        {
            throw new NotImplementedException();
        }

        public void Add(Experiment experimentId, Measurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
