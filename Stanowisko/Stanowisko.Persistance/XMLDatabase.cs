using System;
using System.Collections.Generic;
using System.Xml;

namespace Stanowisko.Persistance
{

    /**
     * When fetching from database - gets everything directly from inMemoryDatabase
     * When Adding or Updating - adds/updates inMemoryDatabase and updates .xml file
     * 
     */
    class XMLDatabase : IDatabase
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

        private void Update()
        {
            var xmlDoc = new XmlDocument();

            var root = xmlDoc.CreateElement("experiments");
            xmlDoc.AppendChild(root);

            foreach (var e in _db.Experiments)
            {
                var eID = e["ID"];

                var experiment = xmlDoc.CreateElement("experiment");

                experiment.SetAttribute("ID", eID);
                experiment.SetAttribute("name", e["name"]);
                experiment.SetAttribute("description", e["description"]);
                experiment.SetAttribute("goal", e["goal"]);
                experiment.SetAttribute("result", e["result"]);
                experiment.SetAttribute("summary", e["summary"]);

                var parameters = xmlDoc.CreateElement("parameters");

                foreach (var p in _db.Parameters)
                {
                    if (p["experiment"] == eID)
                    {
                        var parameter = xmlDoc.CreateElement("parameter");

                        parameter.SetAttribute("name", p["name"]);
                        parameter.SetAttribute("value", p["value"]);

                        parameters.AppendChild(parameter);
                    }
                }

                var measurements = xmlDoc.CreateElement("measurements");
                foreach (var m in _db.Measurements)
                {
                    if (m["experiment"] == eID)
                    {
                        var mID = m["ID"];

                        var measurement = xmlDoc.CreateElement("measurement");

                        measurements.SetAttribute("ID", mID);
                        measurements.SetAttribute("result", m["result"]);
                        measurements.SetAttribute("beginning", m["beginning"]);
                        measurements.SetAttribute("end", m["end"]);

                        var samples = xmlDoc.CreateElement("samples");

                        foreach (var s in _db.Samples)
                        {
                            if(s["measurement"] == mID)
                            {
                                var sample = xmlDoc.CreateElement("sample");

                                sample.SetAttribute("ID", s["ID"]);
                                sample.SetAttribute("value", s["value"]);
                                sample.SetAttribute("time", s["time"]);

                                samples.AppendChild(sample);
                            }
                        }
                        measurement.AppendChild(samples);
                        measurements.AppendChild(measurement);
                    }
                }

                experiment.AppendChild(parameters);
                experiment.AppendChild(measurements);
                root.AppendChild(experiment);
            }

            xmlDoc.Save(Path);
        }

        private void Init()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);

            var experiments = xmlDoc.GetElementsByTagName("experiment");

            foreach (XmlNode e in experiments)
            {
                if (e.Attributes != null)
                {
                    var eID = e.Attributes["ID"].Value;

                    _db.Experiments.Add(new Dictionary<string, string>
                        {
                            {"ID", eID},
                            {"name", e.Attributes["name"].Value},
                            {"description", e.Attributes["description"].Value},
                            {"result", e.Attributes["result"].Value},
                            {"summary", e.Attributes["summary"].Value}
                        });

                    var parameters = e.SelectNodes(".//parameters");
                    var measurements = e.SelectNodes(".//measurement");

                    if (parameters != null)
                        foreach (XmlNode p in parameters)
                        {
                            if (p.Attributes != null)
                                _db.Parameters.Add(new Dictionary<string, string>
                                    {
                                        {"experiment", eID},
                                        {"value", p.Attributes["value"].Value},
                                        {"name", p.Attributes["name"].Value}
                                    });
                        }


                    if (measurements != null)
                        foreach (XmlNode m in measurements)
                        {
                            if (m.Attributes != null)
                            {
                                var mID = m.Attributes["ID"].Value;

                                _db.Measurements.Add(new Dictionary<string, string>
                                    {
                                        {"ID", mID},
                                        {"experiment", eID},
                                        {"result", m.Attributes["result"].Value},
                                        {"beginning", m.Attributes["beginning"].Value},
                                        {"end", m.Attributes["end"].Value}
                                    });

                                var samples = m.SelectNodes(".//sample");

                                if (samples != null)
                                    foreach (XmlNode s in samples)
                                    {
                                        if (s.Attributes != null)
                                        {
                                            _db.Samples.Add(new Dictionary<string, string>
                                                {
                                                    {"ID", s.Attributes["ID"].Value},
                                                    {"measurement", mID},
                                                    {"value", s.Attributes["value"].Value},
                                                    {"Time", s.Attributes["time"].Value}
                                                });
                                        }
                                    }
                            }
                        }
                }
            }
        }
    }
}
