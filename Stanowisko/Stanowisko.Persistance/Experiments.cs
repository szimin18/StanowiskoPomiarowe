using System;
using System.Collections.Generic;
using System.Linq;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Experiments : DAO
    {
        private readonly Measurements _measurementsDAO;

        public Experiments(IDatabase db)
            : base(db)
        {
            _measurementsDAO = new Measurements(db);
        }

        public void Add(Experiment e)
        {
            var data = ToJSON(e);

            var parameters = e.Parameters.Select(pair => new Dictionary<String, String>
                {
                    {"experiment", e.Id.ToString()}, {"name", pair.Key}, {"value", pair.Value}
                });

            try
            {
                Db.Insert("Experiments", data);

                foreach (var parameter in parameters)
                {
                    Db.Insert("Parameters", parameter);    
                }
                
                foreach (var m in e.GetMeasurements())
                {
                    _measurementsDAO.Add(m, e);
                }
            }
            catch (Exception)
            {

            }

        }

        public void Update(Experiment e)
        {
            var data = ToJSON(e);

            var parameters = e.Parameters.Select(pair => new Dictionary<String, String>
                {
                    {"experiment", e.Id.ToString()}, {"name", pair.Key}, {"value", pair.Value}
                });

            try
            {
                Db.Update("Experiments", data, where: String.Format("Experiments.ID = {0}", e.Id.ToString()));

                foreach (var p in parameters.Where(p => p != null))
                {
                    Db.Update("Parameters", p, String.Format("Parameters.name = {0} and Parameters.value = {1}", p["Name"], p["Value"]));
                }

                foreach (var m in e.GetMeasurements())
                {
                    _measurementsDAO.Update(m, e);
                }
            }
            catch (Exception)
            {

            }
        }

        public List<Experiment> GetAll()
        {
            var columns = new List<string> { "ID", "name", "description", "goal", "result", "summary" };
            var experiments = Db.GetAll("Experiments", columns);

            var es = (from experiment in experiments
                      let eId = Convert.ToInt32(experiment["ID"])
                      let name = experiment["name"]
                      let paramColumns = new List<string> { "name", "value" }
                      let ps = Db.GetAll("Parameters", "experiment", eId.ToString(), paramColumns)
                      let parameters = ps.ToDictionary(parameter => parameter["name"], parameter => parameter["value"])
                      select new Experiment(eId, name)
                          {
                              Description = experiment["description"],
                              Goal = experiment["goal"],
                              Result = Convert.ToDouble(experiment["result"]),
                              Summary = experiment["summary"],
                              Parameters = parameters
                          }).ToList();

            foreach (var e in es)
            {
                var measurements = _measurementsDAO.GetAll(e);
                e.AddMeasurements(measurements);
            }

            return es;
        }

        public Dictionary<string, string> ToJSON(Experiment e)
        {
            return new Dictionary<String, String>
                {
                    {"ID", e.Id.ToString()},
                    {"name", e.Name},
                    {"description", e.Description},
                    {"goal", e.Goal},
                    {"result", e.Result.ToString()},
                    {"summary", e.Summary}
                };

        }
    }
}
