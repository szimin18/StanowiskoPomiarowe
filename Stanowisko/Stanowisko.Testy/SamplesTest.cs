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
        internal class DataBaseStub : ISQLiteDatabase
        {
            public List<Dictionary<string, string>> Samples = new List<Dictionary<string, string>>();

            public List<Dictionary<string, string>> GetAll(string tableName, List<string> columns)
            {
                return new List<Dictionary<string, string>>();
            }

            public List<Dictionary<string, string>> GetAll(String tableName, String idName, String idValue, List<String> columns)
            {
                var res = new List<Dictionary<string, string>>();

                if (tableName == "Samples")
                {
                    res.AddRange(from sample in Samples
                                 where sample[idName] == idValue
                                 select columns.ToDictionary(column => column, column => sample[column]));
                }

                return res;
            }

            public bool Update(string tableName, Dictionary<string, string> data, string @where)
            {
                return false;
            }

            public bool Delete(string tableName, string @where)
            {
                return false;
            }

            public bool Insert(string tableName, Dictionary<string, string> data)
            {
                switch (tableName)
                {
                    case "Samples":
                        Samples.Add(data);
                        return true;
                    default:
                        return false;
                }
            }
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
