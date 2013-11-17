using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.Persistance;
using Stanowisko.SharedClasses;

namespace Stanowisko.Testy
{
    [TestClass]
    public class SamplesTest
    {

        private InMemoryDatabase db;

        private Samples samplesDAO;

        public void SetUp()
        {
            db = new InMemoryDatabase();
            samplesDAO = new Samples(db);
        }

        [TestMethod]
        public void AddingNewSampleTest()
        {
            SetUp();
            var s = new Sample(1, 10, 3.14);
            var m = new Measurement(10);
            samplesDAO.Add(s, m);

            var d1 = new Dictionary<string, string>
                {
                    {"ID", "1"},
                    {"measurement", "10"},
                    {"value", "10"},
                    {"time", "3,14"}
                };

            var d2 = db.Samples[0];
            Assert.IsTrue(d2.Count == d1.Count && !d2.Except(d1).Any());
        }

        [TestMethod]
        public void GetAllTest()
        {
            SetUp();

            var m = new Measurement(1);
            var s1 = new Sample(1, 1, 3.14);
            var s2 = new Sample(2, 2, 6.28);
            var s3 = new Sample(3, 3, 6.01);

            samplesDAO.Add(s1, m);
            samplesDAO.Add(s2, m);
            samplesDAO.Add(s3, new Measurement(2));

            var res = samplesDAO.GetAll(m);
            Assert.IsTrue(res.Contains(s1) );

        }

    }
}
