using System;
using System.Collections.Generic;
using System.Linq;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Experiments : DAO, IExperiments
    {
        private Measurements _measurementDAO;
        public Experiments(ISQLiteDatabase db)
            : base(db)
        {
            _measurementDAO = new Measurements(db);
        }

        public void Add(Experiment e)
        {
            var data = new Dictionary<String, String>
                {
                    {"ID", e.Id.ToString()},
                    {"name", e.Name},
                    {"description", e.Description},
                    {"goal", e.Goal},
                    {"result", e.Result.ToString()},
                    {"summary", e.Summary}
                };

            try
            {
                _db.Insert("Experiments", data);
                _db.Insert("Parameters", e.Parameters);
            }
            catch (Exception)
            {

            }

        }

        public void Update(Experiment e)
        {
            var data = new Dictionary<String, String>
                {
                    {"ID", e.Id.ToString()},
                    {"name", e.Name},
                    {"description", e.Description},
                    {"goal", e.Goal},
                    {"result", e.Result.ToString()},
                    {"summary", e.Summary}
                };

            var parameters = e.Parameters.Select(pair => new Dictionary<String, String>
                {
                    {"experiment", e.Id.ToString()}, {"name", pair.Key}, {"value", pair.Value}
                });

            try
            {
                _db.Update("Experiments", data, where: String.Format("Experiments.ID = {0}", e.Id.ToString()));
                foreach (var p in parameters.Where(p => p != null))
                {
                    _db.Update("Parameters", p, String.Format("Parameters.name = {0} and Parameters.value = {1}", p["Name"], p["Value"]));
                }
            }
            catch (Exception)
            {

            }
        }

        public List<Experiment> GetAll()
        {
            var columns = new List<string> {"ID", "name", "description", "goal", "result", "summary"};
            var experiments = _db.GetAll("Experiments", columns);
            var result = new List<Experiment>();

            foreach (var experiment in experiments)
            {
                var eId = Convert.ToInt32(experiment["ID"]);
                var name = experiment["name"];

                var paramColumns = new List<string> {"name", "value"};
                var ps = _db.GetAll("Parameters", "experiment", eId.ToString(), paramColumns);
                var parameters = ps.ToDictionary(parameter => parameter["name"], parameter => parameter["value"]);

                var e = new Experiment(eId, name)
                    {
                        Description = experiment["description"],
                        Goal = experiment["goal"],
                        Result = Convert.ToDouble(experiment["result"]),
                        Summary = experiment["summary"],
                        Parameters = parameters
                    };
                result.Add(e);
            }

            return result;
        }
    }
}
