using System;
using System.IO;

namespace Stanowisko.SharedClasses
{
    public class Sample
    {
        public int Id { get; private set; }

        public double Value { get; private set; }

        public double Time { get; private set; }

        public Sample(int id, double value, double time)
        {
            Id = id;
            Value = value;
            Time = time;
        }

        public Sample(double value, double time)
        {
            //Id = Convert.ToInt32(File.ReadAllText("../../SampleID.csv"));
            var i = Id + 1;
            //File.WriteAllText("../../SampleID.csv", i.ToString());
            Value = value;
            Time = time;
        }
    }
}
