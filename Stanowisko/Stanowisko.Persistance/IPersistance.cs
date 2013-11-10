using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public interface IPersistance
    {
        void Save(Experiment experiment);

        void Save(int measurementId, Sample sample);

        void Save(int experimentId, Measurement measurement);

        void RemoveSamples(Measurement measurement, IEnumerable<int> range);

        void RemoveMeasurement(Measurement measurement);

        Experiment GetExperiment(object id);
    }
}
