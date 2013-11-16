using System;
using System.Collections.Generic;
using System.IO;

namespace Stanowisko.SharedClasses
{

    public class Measurement
    {
        private readonly List<Sample> _samples = new List<Sample>();

        public int Id { get; private set; }

        public double Result { get; set; }

        public Sample Beginning { get; set; }

        public Sample End { get; set; }

        public Measurement()
        {
            Id = Convert.ToInt32(File.ReadAllText("../../MeasurementID.csv"));
            var i = Id + 1;
            File.WriteAllText("../../MeasurementID.csv", i.ToString());
        }

        public Measurement(int id)
        {
            Id = id;
        }

        public Measurement(List<Sample> samples)
        {
            Id = Convert.ToInt32(File.ReadAllText("../../MeasurementID.csv"));
            var i = Id + 1;
            File.WriteAllText("../../MeasurementID.csv", i.ToString());
            Add(samples);
        }

        public void Add(List<Sample> samples)
        {
            _samples.AddRange(samples);
        }

        public void Remove(List<Sample> samples)
        {
            if (_samples != null) _samples.RemoveAll(samples.Contains);
        }

        public List<Sample> GetSamples()
        {
            var res = new List<Sample>();
            if (_samples != null) res.AddRange(_samples);
            return res;
        }
    }
}
