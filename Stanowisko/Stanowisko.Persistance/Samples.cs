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

        public void Add(Sample s, Measurement m, Experiment e)
        {
            s.Id = Db.GetNextSampleID(m.Id, e.Id);

            var data = ToJSON(s, m, e);

            try
            {
                Db.Insert("Samples", data);
            }
            catch (Exception)
            {
                Console.WriteLine("Error while inserting sample");
            }
        }
        public List<Sample> GetAll(Measurement m, Experiment e)
        {
            var columns = new List<string> { "ID", "value", "time" };
            var data = Db.GetAll("Samples", "measurement", m.Id.ToString(),"experiment",e.Id.ToString(), columns);

            var result = data.Select(row =>
                new Sample(Convert.ToInt32(row["ID"]),
                            Convert.ToDouble(row["value"].Replace(".", ",")),
                            Convert.ToDouble(row["time"].Replace(".", ",")))).ToList();

            return result;
        }

        public Dictionary<string, string> ToJSON(Sample s, Measurement m, Experiment e)
        {
            return new Dictionary<string, string>
                {
                    {"ID", s.Id.ToString()},
                    {"measurement", m.Id.ToString()},
                    {"experiment",e.Id.ToString()},
                    {"value", s.Value.ToString()},
                    {"time", s.Time.ToString()}
                };
        }
    }
}
