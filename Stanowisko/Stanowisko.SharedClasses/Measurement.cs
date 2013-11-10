using System.Collections.Generic;

namespace Stanowisko.SharedClasses
{

    public class Measurement
    {
        private readonly List<Sample> _samples = new List<Sample>();

        private static int _nextId;

        public int Id { get; private set; }

        public double Result { get; set; }

        public Measurement()
        {
            Id = _nextId++;
        }

        public Measurement(List<Sample> samples)
        {
            Id = _nextId++;
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
