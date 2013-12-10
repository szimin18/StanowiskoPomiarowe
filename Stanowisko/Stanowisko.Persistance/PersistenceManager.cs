using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public class PersistenceManager : IPersistenceManager
    {
        private readonly Experiments _experiments;

        private readonly Measurements _measurements;

        private readonly Samples _samples;

        public PersistenceManager(IDatabase db)
        {
            _experiments = new Experiments(db);
            _measurements = new Measurements(db);
            _samples = new Samples(db);
        }


        public void AddExperiment(Experiment e)
        {
            _experiments.Add(e);
        }

        public void AddMeasurement(Measurement m, Experiment e)
        {
            _measurements.Add(m, e);
        }

        public void AddSample(Sample s, Measurement m, Experiment e)
        {
            _samples.Add(s, m, e);
        }

        public void UpdateExperiment(Experiment e)
        {
            _experiments.Update(e);
        }

        public void UpdateMeasurement(Measurement m, Experiment e)
        {
            _measurements.Update(m, e);
        }

        public List<Experiment> GetAllExperiments()
        {
            return _experiments.GetAll();
        }

        public List<Measurement> GetAllMeasurements(Experiment e)
        {
            return _measurements.GetAll(e);
        }

        public List<Sample> GetAllSamples(Measurement m, Experiment e)
        {
            return _samples.GetAll(m, e);
        }
    }
}
