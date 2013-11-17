using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Stanowisko.Persistance;
using Stanowisko.SharedClasses;

namespace Stanowisko.Testy
{
    [TestClass]
    class MeasurementsTest
    {
        internal class DataBaseStub : SQLiteDatabase
        {
            public List<Experiment> Experiments = new List<Experiment>();
            public List<Measurement> Measurements = new List<Measurement>();
            public List<Sample> Samples = new List<Sample>();
        }

        private DataBaseStub _dbStub;
        private Measurements _measurementDAO;


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

            _measurementDAO.Add(m, e);
            Assert.IsTrue(_dbStub.Measurements.Contains(m));
        }

        [TestMethod]
        public void SaveMeasurementToUnexistingExperiment()
        {
            SetUp();
            var m = new Measurement();
            var e = new Experiment("Unexisting");

            _measurementDAO.Add(m, e);
            Assert.IsFalse(_dbStub.Measurements.Contains(m));
        }

    }
}
