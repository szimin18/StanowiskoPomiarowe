﻿using System;
using System.IO;

namespace Stanowisko.SharedClasses
{
    public class Sample : IEquatable<Sample>
    {
        public int Id { get; set; }

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
            Value = value;
            Time = time;
        }

        public bool Equals(Sample s)
        {
            return s.Id == Id && s.Value == Value && s.Time == Time;
        }

    }
}
