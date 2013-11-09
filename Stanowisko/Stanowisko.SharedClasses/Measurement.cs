using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.SharedClasses
{
    public class Measurement
    {
        protected List<Sample> _samples;
        public Measurement(List<Sample> samples)
        {
            this._samples = samples;
        }

        public List<Sample> Samples
        {
            get
            {
                return _samples;
            }
        }
    }
}
