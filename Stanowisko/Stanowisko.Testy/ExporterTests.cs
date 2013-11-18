using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Stanowisko.SharedClasses;

namespace Stanowisko.Testy
{
    [TestClass]
    class ExporterTests
    {
        private Sample s1 = new Sample(1, 1, 1);
        private Sample s2 = new Sample(2, 2, 2);
        private Sample s3 = new Sample(3, 3, 3);
        private Sample s4 = new Sample(4, 4, 4);
        private Sample s5 = new Sample(5, 5, 5);
        private Sample s6 = new Sample(5, 5, 5);
        private Sample s7 = new Sample(7, 7, 7);
        private Sample s8 = new Sample(8, 8, 8);
        private Measurement emptyMeasurement = new Measurement();
        private Measurement measurement1 = new Measurement(new List<Sample>() {s1, s2, s3});
        private Measurement measurement2;
        private Experiment e = new Experiment(1, "e");
    }
}
