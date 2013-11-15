using System;
using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Measurements : DAO
    {
        readonly SQLiteDatabase _db = new SQLiteDatabase();
        public Measurements(DBConnection connection) : base(connection)
        {
        }

        public void Update(Experiment experiment, Measurement measurement)
        {
            var data = new Dictionary<String, String>
                {
                    {"ID", measurement.Id.ToString()},
                    {"experiment", experiment.Id.ToString()},
                    {"result", experiment.Result.ToString()}
                };
            try
            {
                _db.Update("Measurements", data, where: String.Format("MEASUREMENTS.ID = {0}", measurement.Id.ToString()));
            }
            catch (Exception)
            {
            }
        }

        public void Add(Experiment experiment, Measurement measurement)
        {
            var data = new Dictionary<String, String>
                {
                    {"ID", measurement.Id.ToString()},
                    {"experiment", experiment.Id.ToString()},
                    {"result", experiment.Result.ToString()}
                };
            try
            {
                _db.Insert("Measurements", data);
            }
            catch (Exception)
            {
            }
        }
    }
}
