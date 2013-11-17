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
    public class ExperimentsTest
    {
        private InMemoryDatabase db;
        private Experiments experimentsDAO;

        Experiment e;
        Experiment e2;
        private Dictionary<string, string> d;
        private List<Dictionary<string, string>> p;

        private Sample s1 = new Sample(1, 1, 1);
        private Sample s2 = new Sample(2, 2, 2);
        private Sample s3 = new Sample(3, 3, 3);
        private Sample s4 = new Sample(4, 4, 4);
        private Sample s5 = new Sample(5, 5, 5);
        private Sample s6 = new Sample(5, 5, 5);
        private Sample s7 = new Sample(7, 7, 7);
        private Sample s8 = new Sample(8, 8, 8);
        Measurement m1 = new Measurement(1);
        Measurement m2 = new Measurement(2);
        Measurement m3 = new Measurement(3);

        public void SetUp()
        {
            db = new InMemoryDatabase();
            experimentsDAO = new Experiments(db);
            e = new Experiment(1, "e")
                {
                    Description = "d",
                    Result = 3.14,
                    Goal = "Sky",
                    Summary = "",
                    Parameters = new Dictionary<string, string>
                        {
                            {"a", "a"},
                            {"b", "b"}
                        }
                };
            e2 =  new Experiment(2, "e2")
            {
                Description = "d",
                Result = 3.14,
                Goal = "Sky",
                Summary = "",
                Parameters = new Dictionary<string, string>
                        {
                            {"c", "c"},
                            {"d", "d"}
                        }
            };
            d = new Dictionary<string, string>
                {
                    {"ID", e.Id.ToString()},
                    {"name", e.Name},
                    {"description", e.Description},
                    {"goal", e.Goal},
                    {"result", e.Result.ToString()},
                    {"summary", e.Summary}
                };
            var p1 = new Dictionary<string, string>
                {
                    {"experiment", e.Id.ToString()},
                    {"name", "a"},
                    {"value", "a"}
                };
            var p2 = new Dictionary<string, string>
                {
                    {"experiment", e.Id.ToString()},
                    {"name", "b"},
                    {"value", "b"}
                };
            m1 = new Measurement(1) { Result = 3.14, Beginning = s1, End = s2 };
            m2 = new Measurement(2) { Result = 6.28, Beginning = s4, End = s6 };
            m3 = new Measurement(3) { Result = 0, Beginning = s7, End = s8 };

            m1.Add(new List<Sample> { s1, s2, s3 });
            m2.Add(new List<Sample> { s4, s5, s6 });
            m3.Add(new List<Sample> { s7, s8 });

            p = new List<Dictionary<string, string>>  {p1, p2};
            e.AddMeasurements(new List<Measurement>{m1, m2});
            e2.AddMeasurements(new List<Measurement>{m3});
        }

        [TestMethod]
        public void AddExperiment()
        {
            SetUp();

            experimentsDAO.Add(e);

            var e2 = db.Experiments[0];
            Assert.IsTrue(e2.Count == d.Count && !e2.Except(d).Any());
        }

        [TestMethod]
        public void UpdateExperiment()
        {
            SetUp();
            experimentsDAO.Add(e);
            e.Summary = "summary";
            experimentsDAO.Update(e);

            d["summary"] = "summary";

            Assert.IsTrue(db.Experiments.Count() == 1);
            var e2 = db.Experiments[0];

            Assert.IsTrue(e2.Count == d.Count && !e2.Except(d).Any());
        }

    }
}
