﻿using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    interface IPersistenceManager
    {
        void AddExperiment(Experiment e);

        void AddMeasurement(Measurement m, Experiment e);

        void AddSample(Sample s, Measurement m);

        void UpdateExperiment(Experiment e);

        void UpdateMeasurement(Measurement m, Experiment e);

        List<Experiment> GetAllExperiments();

        List<Measurement> GetAllMeasurements(Experiment e);

        List<Sample> GetAllSamples(Measurement m);
    }
}
