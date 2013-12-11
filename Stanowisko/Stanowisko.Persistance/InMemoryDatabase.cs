using System;
using System.Collections.Generic;
using System.Linq;

namespace Stanowisko.Persistance
{
    public class InMemoryDatabase : IDatabase
    {
        public List<Dictionary<string, string>> Experiments = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> Measurements = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> Samples = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> Parameters = new List<Dictionary<string, string>>();

        private List<Dictionary<string, string>> _table;

        private void SetTable(string tableName)
        {
            switch (tableName)
            {
                case "Samples":
                    _table = Samples;
                    break;
                case "Measurements":
                    _table = Measurements;
                    break;
                case "Experiments":
                    _table = Experiments;
                    break;
                case "Parameters":
                    _table = Parameters;
                    break;
            }
        }

        public List<Dictionary<string, string>> GetAll(string tableName, List<string> columns)
        {
            SetTable(tableName);

            return (from row in _table
                    select columns.ToDictionary(column => column, column => row[column])).ToList();
        }

        public List<Dictionary<string, string>> GetAll(string tableName, string idName, string idValue, List<string> columns)
        {
            SetTable(tableName);

            return (from row in _table
                    where row[idName] == idValue
                    select columns.ToDictionary(column => column, column => row[column])).ToList();
        }

        public List<Dictionary<string, string>> GetParameters(string pTableName, string pIdName, string pIdValue, string sIdName, string sIdValue, List<string> columns)
        {
            SetTable(pTableName);

            return (from row in _table
                    where row[pIdName] == pIdValue
                    where row[sIdName] == sIdValue
                    select columns.ToDictionary(column => column, column => row[column])).ToList();
        }

        public bool Update(string tableName, Dictionary<string, string> data, string where)
        {
            SetTable(tableName);

            var e = (tableName == "Experiments") 
                               ? _table.Find(dictionary => dictionary["ID"] == data["ID"]) 
                               : _table.Find(dictionary => dictionary["ID"] == data["ID"] && dictionary["experiment"] == data["experiment"]);
            var i = _table.IndexOf(e);

            _table[i].Keys.ToList().ForEach(key => _table[i][key] = data[key]);

            return false;
        }

        public bool UpdateParameters(Dictionary<string, string> data, string where)
        {

            SetTable("Parameters");

            var e = Parameters.Find(dictionary => dictionary["experiment"] == data["experiment"]);
            var i = Parameters.IndexOf(e);

            _table[i] = data;

            return false;
        }

        public bool Insert(string tableName, Dictionary<string, string> data)
        {
            SetTable(tableName);

            _table.Add(data);

            return true;
        }

        public int GetNextExperimentID()
        {
            return Experiments.Count > 0 ? Experiments.Select(e => Convert.ToInt32(e["ID"])).Max() + 1 : 1;
        }

        public int GetNextMeasurementID(String eId)
        {
            var col = Measurements.Where(m => m["experiment"] == eId);
            return col.Any()
                       ? col.Select(m => Convert.ToInt32(m["ID"])).Max() + 1
                       : 1;
        }

        public int GetNextSampleID(String eId, String mId)
        {
            var col = Samples.Where(s => s["measurement"] == mId && s["experiment"] == eId);
            return col.Any()
                       ? col.Select(s => Convert.ToInt32(s["ID"])).Max() + 1
                       : 1;
        }
    }
}
