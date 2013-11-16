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
    class ExperimentsTest
    {
        internal class DataBaseStub : SQLiteDatabase
        {
            public List<Experiment> Exs = new List<Experiment>();

        }

        private DataBaseStub _dbStub;
        private Experiments _es;

        public void SetUp()
        {
            _dbStub = new DataBaseStub();
            _es = new Experiments(_dbStub);
        }

        [TestMethod]
        public void AddUnexistingExperiment()
        {
            SetUp();
            var e = new Experiment("e");

            _es.Update(e);

            Assert.IsTrue(_dbStub.Exs.Contains(e));
        }

        [TestMethod]
        public void AddExistingExperiment()
        {
            SetUp();
            var e = new Experiment("e");
            _dbStub.Exs.Add(e);

            _es.Update(e);
            var res = _dbStub.Exs[_dbStub.Exs.IndexOf(e)];

            Assert.IsTrue(res == e);
        }

    }
}
