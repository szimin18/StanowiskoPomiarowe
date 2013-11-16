using System;
using System.Collections.Generic;
using System.Linq;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Experiments : DAO
    {

        public Experiments(SQLiteDatabase db)
            : base(db)
        {
        }
        public void Add(Experiment e)
        {
            var data = new Dictionary<String, String>
                {
                    {"ID", e.Id.ToString()},
                    {"Name", e.Name},
                    {"Description", e.Description},
                    {"Goal", e.Description},
                    {"result", e.Result.ToString()},
                    {"Summary", e.Summary}
                };

            try
            {
                _db.Insert("Experiments", data);
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
                    {"Name", e.Name},
                    {"Description", e.Description},
                    {"Goal", e.Description},
                    {"result", e.Result.ToString()},
                    {"Summary", e.Summary}
                };

            var parameters = e.Parameters.Select(pair => new Dictionary<String, String>
                {
                    {"Experiment", e.Id.ToString()}, {"Name", pair.Key}, {"Value", pair.Value}
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

        public Experiment Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
