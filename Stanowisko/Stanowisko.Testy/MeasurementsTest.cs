using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.Persistance;
using Stanowisko.SharedClasses;

namespace Stanowisko.Testy
{
    [TestClass]
    class MeasurementsTest
    {
        internal class DataBaseStub : DBConnection
        {
            public List<Experiment> Experiments = new List<Experiment>();
            public List<Measurement> Measurements = new List<Measurement>(); 
            public List<Sample> Samples = new List<Sample>();
        }

        private DataBaseStub _dbStub;
        private Measurements _measurementDAO ;


        public void SetUp()
        {
            _dbStub = new DataBaseStub();
            _measurementDAO = new Measurements(_dbStub);
        }

        [TestMethod]
        public void SaveMeasurementToExistingExperiment()
        {
            SetUp();
            var m = new Measurement();
            var e = new Experiment("e");

            _dbStub.Experiments.Add(e);

            _measurementDAO.Add(e, m);
            Assert.IsTrue(_dbStub.Measurements.Contains(m));
        }

        [TestMethod]
        public void SaveMeasurementToUnexistingExperiment()
        {
            SetUp();
            var m = new Measurement();
            var e = new Experiment("Unexisting");
            
            _measurementDAO.Add(e, m);
            Assert.IsFalse(_dbStub.Measurements.Contains(m));
        }

        [TestMethod]
        public void RemoveSampleSpan()
        {
            SetUp();
            var m = new Measurement();
            var samples1 = Enumerable.Range(1, 7).Select(t => new Sample(t, 0));
            var samples2 = Enumerable.Range(9, 20).Select(t => new Sample(t, 0));
            _dbStub.Samples.AddRange(samples1);
            _dbStub.Samples.AddRange(samples2);

            _measurementDAO.RemoveSamples(m, Enumerable.Range(5, 10));

            Assert.IsTrue(Enumerable.Range(5, 10)
                .Select(t => _dbStub.Samples.Contains(new Sample(t, 4)))
                .All(x => x == false));
            Assert.IsTrue(Enumerable.Range(1, 4)
                .Select(t => _dbStub.Samples.Contains(new Sample(t, 2)))
                .All(x => x));
            Assert.IsTrue(Enumerable.Range(11, 20)
                .Select(t => _dbStub.Samples.Contains(new Sample(t, 15)))
                .All(x => x));
        }

        [TestMethod]
        public void RemoveMeasurement()
        {
            SetUp();
            var m = new Measurement();
            _dbStub.Measurements.Add(m);

            _measurementDAO.Remove(m);

            Assert.IsFalse(_dbStub.Measurements.Contains(m));
        }

    }
}
