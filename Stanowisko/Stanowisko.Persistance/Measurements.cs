﻿using System;
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

        public void Add(Measurement m, Experiment e)
        {
            m.Id = Db.GetNextMeasurementID(e.Id.ToString());

            var data = ToJSON(m, e);

            Db.Insert("Measurements", data);
            foreach (var sample in m.GetSamples())
            {
                
                _samplesDAO.Add(sample, m, e);
            }


        }

        public void Update(Measurement m, Experiment e)
        {
            var data = ToJSON(m, e);

            Db.Update("Measurements", data, String.Format("MEASUREMENTS.ID = {0}", m.Id));
        }

        public List<Measurement> GetAll(Experiment e)
        {
            var columns = new List<string> { "ID", "result", "beginning", "end" };
            var data = Db.GetAll("Measurements", "experiment", e.Id.ToString(), columns);

            var res = new List<Measurement>();

            foreach (var row in data)
            {
                var m = new Measurement(Convert.ToInt32(row["ID"]));

                var samples = _samplesDAO.GetAll(m, e);

                var bs = samples.Where(s => s.Id == Convert.ToInt32(row["beginning"])).ToList();
                var es = samples.Where(s => s.Id == Convert.ToInt32(row["end"])).ToList();
                var beginning = bs.Any() ? bs[0] : null;
                var end = es.Any() ? es[0] : null;
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
