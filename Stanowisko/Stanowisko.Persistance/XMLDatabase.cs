using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Stanowisko.Persistance
{

    /**
     * When fetching from database - gets everything directly from inMemoryDatabase
     * When Adding or Updating - adds/updates inMemoryDatabase and updates .xml file
     * 
     */

    public class XMLDatabase : IDatabase
    {
        private readonly InMemoryDatabase _db = new InMemoryDatabase();
        private const string Path = "db.xml";

        public XMLDatabase()
        {
            Init();   //initializes InMemoryDatabase with data saved in .xml file
        }

        public List<Dictionary<string, string>> GetAll(string tableName, List<string> columns)
        {
            return _db.GetAll(tableName, columns);
        }

        public List<Dictionary<string, string>> GetAll(string tableName, string idName, string idValue, List<string> columns)
        {
            return _db.GetAll(tableName, idName, idValue, columns);
        }

        public List<Dictionary<string, string>> GetAll(string pTableName, string pIdName, string pIdValue, string sIdName, string sIdValue, List<string> columns)
        {
            return _db.GetAll(pTableName, pIdName, pIdValue, sIdName, sIdValue, columns);
        }

        public bool Update(string tableName, Dictionary<string, string> data, string @where)
        {
            var res = _db.Update(tableName, data, where);

            Update();

            return res;
        }

        public bool Insert(string tableName, Dictionary<string, string> data)
        {
            var res = _db.Insert(tableName, data);

            Update();

            return res;
        }

        public int GetNextExperimentID()
        {
            return _db.GetNextExperimentID();
        }

        public int GetNextMeasurementID(String eId)
        {
            return _db.GetNextMeasurementID(eId);
        }

        public int GetNextSampleID(String eId, String mId)
        {
            return _db.GetNextSampleID(eId, mId);
        }

        private void Update()
        {
            var root = new XElement("experiments");

            foreach (var e in _db.Experiments)
            {
                var eID = e["ID"];

                var experiment = new XElement("experiment");

                experiment.SetAttributeValue("ID", eID);
                experiment.SetAttributeValue("name", e["name"]);

                experiment.Add(new XElement("name", e["name"]));
                experiment.Add(new XElement("description", e["description"]));
                experiment.Add(new XElement("goal", e["goal"]));
                experiment.Add(new XElement("result", e["result"]));
                experiment.Add(new XElement("summary", e["summary"]));




                var parameters = new XElement("parameters");

                foreach (var p in _db.Parameters)
                {
                    if (p["experiment"] == eID)
                    {
                        var parameter = new XElement("parameter");

                        parameter.SetAttributeValue("name", p["name"]);
                        parameter.SetAttributeValue("value", p["value"]);

                        parameters.Add(parameter);
                    }
                }

                var measurements = new XElement("measurements");
                foreach (var m in _db.Measurements)
                {
                    if (m["experiment"] == eID)
                    {
                        var mID = m["ID"];


                        var measurement = new XElement("measurement");

                        measurement.SetAttributeValue("ID", mID);

                        measurement.Add(new XElement("result", m["result"]));
                        measurement.Add(new XElement("beginning", m["beginning"]));
                        measurement.Add(new XElement("end", m["end"]));

                        var samples = new XElement("samples");



                        foreach (var s in _db.Samples.Where(s => s["measurement"] == mID).Where(s => s["experiment"] == eID))
                        {

                            var sample = new XElement("sample");

                            sample.SetAttributeValue("ID", s["ID"]);
                            sample.SetAttributeValue("value", s["value"]);
                            sample.SetAttributeValue("time", s["time"]);

                            samples.Add(sample);
                        }
                        measurement.Add(samples);
                        measurements.Add(measurement);
                    }
                }

                experiment.Add(parameters);
                experiment.Add(measurements);
                root.Add(experiment);
            }

            root.Save(Path);

        }

        private void Init()
        {
            XDocument xmlDoc;
            try
            {
                xmlDoc = XDocument.Load(Path);
            }
            catch (Exception e)
            {
                return;     // wrong base gives us an empty base
            }

            var experiments = xmlDoc.Descendants("experiments").Elements();


            foreach (var e in experiments)
            {
                var eID = e.Attribute("ID").Value;

                _db.Experiments.Add(new Dictionary<string, string>
                    {
                        {"ID", eID},
                        {"name", e.Attribute("name").Value},
                        {"description", e.Element("description").Value},
                        {"goal", e.Element("goal").Value},
                        {"result", e.Element("result").Value},
                        {"summary", e.Element("summary").Value}
                    });

                var parameters = e.Descendants("parameters").Elements();
                var measurements = e.Descendants("measurements").Elements();

                foreach (var p in parameters)
                {

                    _db.Parameters.Add(new Dictionary<string, string>
                                {
                                    {"experiment", eID},
                                    {"value", p.Attribute("value").Value},
                                    {"name", p.Attribute("name").Value}
                                });
                }

                foreach (var m in measurements)
                {

                    var mID = m.Attribute("ID").Value;

                    _db.Measurements.Add(new Dictionary<string, string>
                            {
                                {"ID", mID},
                                {"experiment", eID},
                                {"result", m.Element("result").Value},
                                {"beginning", m.Element("beginning").Value},
                                {"end", m.Element("end").Value}
                            });

                    var samples = m.Descendants("samples").Elements();

                    foreach (var s in samples)
                    {
                        _db.Samples.Add(new Dictionary<string, string>
                                    {
                                        {"ID", s.Attribute("ID").Value},
                                        {"measurement", mID},
                                        {"experiment", eID},
                                        {"value", s.Attribute("value").Value},
                                        {"time", s.Attribute("time").Value}
                                    });

                    }
                }

            }
        }
    }
}
