using System;
using System.Collections.Generic;
using System.Linq;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Measurements : DAO
    {
        private readonly Samples _samplesDAO;

        public Measurements(IDatabase db)
            : base(db)
        {
            _samplesDAO = new Samples(db);
        }

        public void Add(Measurement measurement, Experiment experiment)
        {
            var data = ToJSON(measurement, experiment);

            try
            {
                Db.Insert("Measurements", data);
                foreach (var sample in measurement.GetSamples())
                {
                    _samplesDAO.Add(sample, measurement, experiment);
                }
                
            }
            catch (Exception)
            {
            }
        }

        public void Update(Measurement measurement, Experiment experiment)
        {
            var data = ToJSON(measurement, experiment);

            try
            {
                Db.Update("Measurements", data, String.Format("MEASUREMENTS.ID = {0}", measurement.Id));
            }
            catch (Exception)
            {
            }
        }

        public List<Measurement> GetAll(Experiment experiment)
        {
            var columns = new List<string> { "ID", "result", "beginning", "end" };
            var data = Db.GetAll("Measurements", "experiment", experiment.Id.ToString(), columns);

            var res = new List<Measurement>();

            foreach (var row in data)
            {
                var m = new Measurement(Convert.ToInt32(row["ID"]));

                var samples = _samplesDAO.GetAll(m, experiment);

                var beginning = samples.Where(s => s.Id == Convert.ToInt32(row["beginning"])).ToList()[0];
                var end = samples.Where(s => s.Id == Convert.ToInt32(row["end"])).ToList()[0];
                var result = Convert.ToDouble(row["result"]);

                m.Beginning = beginning;
                m.End = end;
                m.Result = result;
                m.Add(samples);

                res.Add(m);
            }

            return res;
        }

        public Dictionary<string, string> ToJSON(Measurement m, Experiment e)
        {
            return new Dictionary<String, String>
                {
                    {"ID", m.Id.ToString()},
                    {"experiment", e.Id.ToString()},
                    {"result", m.Result != null ? m.Result.ToString() : ""},
                    {"beginning", m.Beginning != null ?m.Beginning.Id.ToString() : "0"},
                    {"end", m.End != null ?m.End.Id.ToString() : "0"}
                };
        }
    }
}
