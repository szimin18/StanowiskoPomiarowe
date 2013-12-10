using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stanowisko.SharedClasses
{

    public class Measurement : IEquatable<Measurement>
    {
        private readonly List<Sample> _samples = new List<Sample>();

        public int Id { get; set; }

        public double Result { get; set; }

        public Sample Beginning { get; set; }

        public Sample End { get; set; }

        public Measurement()
        {
        }

        public Measurement(int id)
        {
            Id = id;
        }

        public Measurement(List<Sample> samples)
        {
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
           return new List<Sample>(_samples);
        }

        public bool Equals(Measurement m)
        {
            return m.Id == Id && m.Result == Result 
                && m._samples.All(s => _samples.Contains(s));
        }
    }
}
