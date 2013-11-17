using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Stanowisko.Persistance;
using Stanowisko.SharedClasses;

namespace Stanowisko.Testy
{
    [TestClass]
    public class MeasurementsTest
    {
        internal class DataBaseStub : ISQLiteDatabase
        {
            public List<Dictionary<string, string>> Measurements = new List<Dictionary<string, string>>();
            public List<Dictionary<string, string>> Samples = new List<Dictionary<string, string>>();


            public List<Dictionary<string, string>> GetAll(string tableName, List<string> columns)
            {
                return new List<Dictionary<string, string>>();
            }

            public List<Dictionary<string, string>> GetAll(string tableName, string idName, string idValue, List<string> columns)
            {
                var table = new List<Dictionary<string, string>>();
                switch (tableName)
                {
                    case "Samples":
                        table = Samples;
                        break;
                    case "Measurements":
                        table = Measurements;
                        break;
                }
               
                return (from row in table
                        where row[idName] == idValue
                        select columns.ToDictionary(column => column, column => row[column])).ToList();
            }

            public bool Update(string tableName, Dictionary<string, string> data, string where)
            {
                var table = new List<Dictionary<string, string>>();
                switch (tableName)
                {
                    case "Samples":
                        table = Samples;
                        break;
                    case "Measurements":
                        table = Measurements;
                        break;
                }

                var e = table.Find(dictionary => dictionary["ID"] == data["ID"]);
                var i = table.IndexOf(e);

                table[i] = data;

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
                    case "Measurements":
                        Measurements.Add(data);
                        return true;
                    default:
                        return false;
                }
            }
        }

        private DataBaseStub db;

        private Measurements measurementDAO;

        private Sample s1 = new Sample(1, 1, 1);
        private Sample s2 = new Sample(2, 2, 2);
        private Sample s3 = new Sample(3, 3, 3);
        private Sample s4 = new Sample(4, 4, 4);
        private Sample s5 = new Sample(5, 5, 5);
        private Sample s6 = new Sample(5, 5, 5);
        private Sample s7 = new Sample(7, 7, 7);
        private Sample s8 = new Sample(8, 8, 8);
        private Measurement m1;
        private Measurement m2;
        private Measurement m3;
        private Experiment e = new Experiment(1, "e");

        public void SetUp()
        {
            db = new DataBaseStub();
            measurementDAO = new Measurements(db);

            m1 = new Measurement(1) { Result = 3.14, Beginning = s1, End = s2 };
            m2 = new Measurement(2) { Result = 6.28, Beginning = s4, End = s6 };
            m3 = new Measurement(3) { Result = 0, Beginning = s7, End = s8 };

            m1.Add(new List<Sample> { s1, s2, s3 });
            m2.Add(new List<Sample> { s4, s5, s6 });
            m3.Add(new List<Sample> { s7, s8 });
        }

        [TestMethod]
        public void AddMeasurement()
        {
            SetUp();

            var d1 = new Dictionary<string, string>
                {
                    {"ID", m1.Id.ToString()},
                    {"experiment", e.Id.ToString()},
                    {"result", m1.Result.ToString()},
                    {"beginning", m1.Beginning.Id.ToString()},
                    {"end", m1.End.Id.ToString()}
                };

            measurementDAO.Add(m1, e);

            var m2 = db.Measurements[0];
            Assert.IsTrue(m2.Count == d1.Count && !m2.Except(d1).Any());
        }

        [TestMethod]
        public void UpdateMeasurement()
        {
            SetUp();

            measurementDAO.Add(m1, e);
            m2 = new Measurement(1) { Result = 6.28, Beginning = s4, End = s6 };
            measurementDAO.Update(m2, e);

            var d1 = new Dictionary<string, string>
                {
                    {"ID", m2.Id.ToString()},
                    {"experiment", e.Id.ToString()},
                    {"result", m2.Result.ToString()},
                    {"beginning", m2.Beginning.Id.ToString()},
                    {"end", m2.End.Id.ToString()}
                };

            Assert.IsTrue(db.Measurements.Count() == 1);

            var m = db.Measurements[0];
            Assert.IsTrue(m.Count == d1.Count && !m.Except(d1).Any());
        }

        [TestMethod]
        public void GetAllTest()
        {
            SetUp();

            var e2 = new Experiment(20, "e2");

            measurementDAO.Add(m1, e);
            measurementDAO.Add(m2, e);
            measurementDAO.Add(m3, e2);

            var ms = measurementDAO.GetAll(e);
            
            Assert.IsTrue(ms.Contains(m1) && ms.Contains(m2) && !ms.Contains(m3));
        }

    }
}
