using System;
using System.Collections.Generic;
using System.Linq;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Samples : DAO
    {

        public Samples(IDatabase db)
            : base(db)
        {
        }

        public void Add(Sample sample, Measurement measurement)
        {
            var data = ToJSON(sample, measurement);
            try
            {
                _db.Insert("Samples", data);
            }
            catch (Exception)
            {

            }
        }
        public List<Sample> GetAll(Measurement m)
        {
            var columns = new List<string> { "ID", "value", "time" };
            var data = _db.GetAll("Samples", "measurement", m.Id.ToString(), columns);
            
            var result = data.Select(row =>
                new Sample( Convert.ToInt32(row["ID"]),
                            Convert.ToDouble(row["value"].Replace(".", ",")),
                            Convert.ToDouble(row["time"].Replace(".", ",")))).ToList();
           
            return result;
        }

        public Dictionary<string, string> ToJSON(Sample s, Measurement m)
        {
            return new Dictionary<string, string>
                {
                    {"ID", s.Id.ToString()},
                    {"measurement", m.Id.ToString()},
                    {"value", s.Value.ToString()},
                    {"time", s.Time.ToString()}
                };
        }
    }
}
