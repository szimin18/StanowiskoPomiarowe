﻿using System;
using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class Measurements : DAO, IMeasurements
    {

        public Measurements(SQLiteDatabase db)
            : base(db)
        {
        }

        public void Add(Measurement measurement, Experiment experiment)
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

        public void Update(Measurement measurement, Experiment experiment)
        {
            var data = new Dictionary<String, String>
                {
                    {"ID", measurement.Id.ToString()},
                    {"experiment", experiment.Id.ToString()},
                    {"result", experiment.Result.ToString()}
                };

            try
            {
                _db.Update("Measurements", data, String.Format("MEASUREMENTS.ID = {0}", measurement.Id));
            }
            catch (Exception)
            {
            }
        }
    }
}
