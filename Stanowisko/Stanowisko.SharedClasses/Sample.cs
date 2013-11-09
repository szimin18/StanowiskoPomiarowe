using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.SharedClasses
{
    public class Sample
    {
        double _value;
        DateTime _readingTime;

        public Sample(double value, DateTime readingTime)
        {
            this._value = value;
            this._readingTime = readingTime;
        }

        public double Value
        {
            get
            {
                return _value;
            }
        }

        public DateTime ReadingTime
        {
            get
            {
                return _readingTime;
            }
        }
    }
}
