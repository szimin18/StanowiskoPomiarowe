using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.Persistance;
using Stanowisko.SharedClasses;

namespace Stanowisko.Testy
{
    [TestClass]
    class SamplesTest
    {
        internal class DataBaseStub : SQLiteDatabase
        {
            public List<Dictionary<string, string>> Samples = new List<Dictionary<string, string>>();


        }

        private DataBaseStub db;

        private Samples samplesDAO;

        public void SetUp()
        {
            db = new DataBaseStub();
            samplesDAO = new Samples(db);
        }

        [TestMethod]
        public void AddingNewSampleTest()
        {
            SetUp();
            var s = new Sample(1, 10, 3.14);
            var m = new Measurement(10);
            samplesDAO.Add(s, m);

            var d = new Dictionary<string, string>
                {
                    {"ID", "1"},
                    {"measurement", ""},
                    {"value", "10"},
                    {"time", "3.14"}
                };

            Assert.IsTrue(db.Samples.Contains(d));
        }

        [TestMethod]
        public void GetAllTest()
        {
            SetUp();
            var m = new Measurement(1);
            var d1 = new Dictionary<string, string>
                {
                    {"ID", "1"},
                    {"measurement", "1"},
                    {"value", "1"},
                    {"time", "3.14"}
                };
            var s1 = new Sample(1, 1, 3.14);
            var d2 = new Dictionary<string, string>
                {
                    {"ID", "2"},
                    {"measurement", "1"},
                    {"value", "2"},
                    {"time", "6.28"}
                };
            var s2 = new Sample(2, 2, 6.28);
            var d3 = new Dictionary<string, string>
                {
                    {"ID", "3"},
                    {"measurement", "2"},
                    {"value", "3"},
                    {"time", "6.01"}
                };
            var s3 = new Sample(3, 3, 6.01);
            db.Samples = new List<Dictionary<string, string>> { d1, d2 };

            var res = samplesDAO.GetAll(m);

            Assert.IsTrue(res.Contains(s1) && res.Contains(s2) && !res.Contains(s3));

        }

    }
}
