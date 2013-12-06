using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public interface IPersistenceManager
    {
        void AddExperiment(Experiment e);

        void AddMeasurement(Measurement m, Experiment e);

        void AddSample(Sample s, Measurement m, Experiment e);

        void UpdateExperiment(Experiment e);

        void UpdateMeasurement(Measurement m, Experiment e);

        List<Experiment> GetAllExperiments();

        List<Measurement> GetAllMeasurements(Experiment e);

        List<Sample> GetAllSamples(Measurement m, Experiment e);
    }
}
