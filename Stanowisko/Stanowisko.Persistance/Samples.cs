using System;
using System.Collections.Generic;
using System.Linq;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Samples : DAO, ISamples
    {

        public Samples(ISQLiteDatabase db)
            : base(db)
        {
        }

        public void Add(Sample sample, Measurement measurement)
        {
            var data = new Dictionary<string, string>
                {
                    {"ID", sample.Id.ToString()},
                    {"measurement", measurement.Id.ToString()},
                    {"value", sample.Value.ToString()},
                    {"time", sample.Time.ToString()}
                };
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

    }
}
