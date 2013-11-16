using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    class Samples
    {
        readonly SQLiteDatabase _db = new SQLiteDatabase();

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
            catch
            {

            }
        }
        public List<Sample> GetAll(Measurement m)
        {
            var columns = new List<string> { "ID", "value", "time" };
            var data = _db.GetAll("Samples", "measurement", "m.Id", columns);
            
            var result = data.Select(row =>
                new Sample( Convert.ToInt32(row["Id"]),
                            Convert.ToDouble(row["value"]),
                            Convert.ToDouble(row["time"]))).ToList();
           
            return result;
        }
    }
}
