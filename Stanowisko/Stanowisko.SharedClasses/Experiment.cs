using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stanowisko.SharedClasses
{
    public class Experiment : IEquatable<Experiment>
    {
        private Dictionary<string, string> _parameters = new Dictionary<string, string>();

        private readonly List<Measurement> _measurements = new List<Measurement>();

        public int Id { get; set; }

        public string Name { get; private set; }

        public string Description { get; set; }

        public string Goal { get; set; }

        public double Result { get; set; }

        public string Summary { get; set; }

        public Dictionary<string, string> Parameters
        {
            set { _parameters = value; }
            get { return _parameters; }
        }

        public Experiment(int id, String name)
        {
            Name = name;
            Id = id;
        }

        public Experiment(String name)
        {
            Name = name;
        }

        public void AddMeasurements(List<Measurement> ms)
        {
            _measurements.AddRange(ms);
        }

        public void RemoveMeasurements(List<Measurement> ms)
        {
            if (ms != null) _measurements.RemoveAll(ms.Contains);
        }

        public void AddParameters(string name, string desc)
        {
            _parameters.Add(name, desc);
        }

        public void RemoveParameter(string name)
        {
            _parameters.Remove(name);
        }

        public Dictionary<string, string> GetParameters()
        {
            return new Dictionary<string, string>(_parameters);
        }

        public List<Measurement> GetMeasurements()
        {
            return new List<Measurement>(_measurements);
        }

        public bool Equals(Experiment e)
        {
            return e.Id == Id &&
                     e.Name.Equals(Name) &&
                     e.Result.Equals(Result) &&
                     e.Summary.Equals(Summary) &&
                     e.Goal.Equals(Goal) &&
                     e.Description.Equals(Description) &&
                     e._measurements.All(m => _measurements.Contains(m)) &&
                     e.Parameters.Equals(Parameters);
        }
    }
}
